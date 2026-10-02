#!/usr/bin/env python3
"""Decode Unity TextAsset protobuf tables using this repository's config schemas."""

from __future__ import annotations

import argparse
import base64
import json
import sys
import tempfile
from collections import Counter
from pathlib import Path
from typing import Iterator


def textasset_files(bundle: Path | None, asset_dir: Path | None) -> Iterator[tuple[str, bytes]]:
    if asset_dir is not None:
        for path in sorted(asset_dir.glob("*.bin")):
            parts = path.stem.split("_", 1)
            if len(parts) != 2:
                raise ValueError(f"expected an index and asset name in {path.name!r}")
            yield parts[1], path.read_bytes()
        return

    try:
        import UnityPy
    except ImportError as exc:
        raise RuntimeError("bundle mode requires UnityPy; install it with `python -m pip install UnityPy`") from exc

    assert bundle is not None
    environment = UnityPy.load(str(bundle))
    for index, obj in enumerate(environment.objects):
        if obj.type.name != "TextAsset":
            continue
        asset = obj.read()
        name = asset.m_Name or f"unnamed_{index}"
        script = asset.m_Script
        raw = script if isinstance(script, bytes) else script.encode("utf-8", "surrogateescape")
        yield name, raw


def make_pool(schema_dir: Path):
    try:
        from grpc_tools import protoc
        import grpc_tools
        from google.protobuf import descriptor_pb2, descriptor_pool, message_factory
    except ImportError as exc:
        raise RuntimeError("schema mode requires grpcio-tools; install it with `python -m pip install grpcio-tools`") from exc

    sources = sorted(schema_dir.glob("*.proto"))
    if not sources:
        raise FileNotFoundError(f"no .proto files found under {schema_dir}")

    with tempfile.TemporaryDirectory(prefix="party-config-proto-") as temp_dir:
        descriptor_path = Path(temp_dir) / "config.protoset"
        include_dir = Path(grpc_tools.__file__).parent / "_proto"
        args = [
            "protoc",
            f"-I{schema_dir}",
            f"-I{include_dir}",
            "--include_imports",
            f"--descriptor_set_out={descriptor_path}",
            *(str(path) for path in sources),
        ]
        result = protoc.main(args)
        if result != 0:
            raise RuntimeError(f"protoc failed with exit code {result}")
        descriptor_set = descriptor_pb2.FileDescriptorSet.FromString(descriptor_path.read_bytes())

    pool = descriptor_pool.DescriptorPool()
    pending = list(descriptor_set.file)
    while pending:
        later = []
        progress = False
        for file_descriptor in pending:
            try:
                pool.AddSerializedFile(file_descriptor.SerializeToString())
                progress = True
            except Exception:
                later.append(file_descriptor)
        if not progress:
            unresolved = ", ".join(item.name for item in later[:10])
            raise RuntimeError(f"could not load config schema dependencies: {unresolved}")
        pending = later
    return pool, message_factory


def convert_scalar(value, field):
    if field.enum_type is not None:
        number = int(value)
        enum_value = field.enum_type.values_by_number.get(number)
        return {"value": number, "name": enum_value.name if enum_value else str(number)}
    if field.type == field.TYPE_BYTES:
        return base64.b64encode(value).decode("ascii")
    if field.type == field.TYPE_MESSAGE:
        return convert_message(value)
    return value


def convert_message(message):
    if message.DESCRIPTOR.full_name == "google.protobuf.Timestamp":
        fields = {}
        if message.seconds:
            fields["field_1"] = [message.seconds]
        if message.nanos:
            fields["field_2"] = [message.nanos]
        return {"_unknown": fields} if fields else {}

    result = {}
    for field, value in message.ListFields():
        if field.is_repeated:
            if field.message_type is not None and field.message_type.GetOptions().map_entry:
                value_field = field.message_type.fields_by_name["value"]
                result[field.json_name] = {
                    str(key): convert_scalar(item, value_field)
                    for key, item in sorted(value.items(), key=lambda pair: str(pair[0]))
                }
            else:
                result[field.json_name] = [convert_scalar(item, field) for item in value]
        else:
            result[field.json_name] = convert_scalar(value, field)
    return result


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    source = parser.add_mutually_exclusive_group(required=True)
    source.add_argument("--bundle", type=Path, help="UnityFS bundle containing config TextAssets")
    source.add_argument("--input", type=Path, help="directory of raw TextAsset .bin files")
    parser.add_argument("--output", required=True, type=Path, help="resource Data directory to merge into")
    parser.add_argument(
        "--schema-dir",
        type=Path,
        default=Path(__file__).resolve().parents[1] / "internal" / "protocol" / "schema" / "config",
        help="directory containing the normalized config .proto files",
    )
    args = parser.parse_args()

    pool, message_factory = make_pool(args.schema_dir.resolve())
    assets = list(textasset_files(args.bundle, args.input))
    output_dir = args.output.resolve()
    output_dir.mkdir(parents=True, exist_ok=True)
    existing = {path.name.casefold(): path for path in output_dir.glob("*.json")}
    collisions = [name for name, count in Counter(asset for asset, _ in assets).items() if count > 1]
    if collisions:
        raise ValueError("duplicate TextAsset names: " + ", ".join(collisions))

    exported = 0
    empty = 0
    unknown = []
    table_count = 0
    for name, raw in assets:
        if not raw:
            empty += 1
            continue
        try:
            descriptor = pool.FindMessageTypeByName(f"config.{name}Configure")
        except KeyError:
            unknown.append(name)
            continue
        message_class = message_factory.GetMessageClass(descriptor)
        message = message_class.FromString(raw)
        exported += 1
        for field in message.DESCRIPTOR.fields:
            if not field.is_repeated:
                continue
            if field.message_type is not None and field.message_type.GetOptions().map_entry:
                continue
            values = getattr(message, field.name)
            if not values:
                continue
            table_name = field.name[:1].lower() + field.name[1:]
            filename = f"{name}_{table_name}.json"
            destination = existing.get(filename.casefold(), output_dir / filename)
            rows = [convert_scalar(value, field) for value in values]
            destination.write_text(json.dumps(rows, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
            existing[destination.name.casefold()] = destination
            table_count += 1

    if unknown:
        raise RuntimeError("TextAssets without matching config roots: " + ", ".join(unknown))
    print(f"decoded {exported} config roots from {len(assets)} TextAssets; empty={empty}; tables={table_count}; output={output_dir}")
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except Exception as exc:
        print(f"error: {exc}", file=sys.stderr)
        raise SystemExit(1)
