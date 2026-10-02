using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MutatorConfigure : IMessage<MutatorConfigure>, IMessage, IEquatable<MutatorConfigure>, IDeepCloneable<MutatorConfigure>, IBufferMessage
{
	private static readonly MessageParser<MutatorConfigure> _parser = new MessageParser<MutatorConfigure>(() => new MutatorConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<MutatorInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, MutatorInfoConfigure.Parser);

	private readonly RepeatedField<MutatorInfoConfigure> infos_ = new RepeatedField<MutatorInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, MutatorInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, MutatorInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MutatorInfoConfigure.Parser), 18u);

	private readonly MapField<int, MutatorInfoConfigure> infoDict_ = new MapField<int, MutatorInfoConfigure>();

	public const int PoolsFieldNumber = 3;

	private static readonly FieldCodec<MutatorPoolConfigure> _repeated_pools_codec = FieldCodec.ForMessage(26u, MutatorPoolConfigure.Parser);

	private readonly RepeatedField<MutatorPoolConfigure> pools_ = new RepeatedField<MutatorPoolConfigure>();

	public const int PoolDictFieldNumber = 4;

	private static readonly MapField<int, MutatorPoolConfigure>.Codec _map_poolDict_codec = new MapField<int, MutatorPoolConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MutatorPoolConfigure.Parser), 34u);

	private readonly MapField<int, MutatorPoolConfigure> poolDict_ = new MapField<int, MutatorPoolConfigure>();

	public const int PoolComposesFieldNumber = 5;

	private static readonly FieldCodec<MutatorPoolComposeConfigure> _repeated_poolComposes_codec = FieldCodec.ForMessage(42u, MutatorPoolComposeConfigure.Parser);

	private readonly RepeatedField<MutatorPoolComposeConfigure> poolComposes_ = new RepeatedField<MutatorPoolComposeConfigure>();

	public const int PoolComposeDictFieldNumber = 6;

	private static readonly MapField<int, MutatorPoolComposeConfigure>.Codec _map_poolComposeDict_codec = new MapField<int, MutatorPoolComposeConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MutatorPoolComposeConfigure.Parser), 50u);

	private readonly MapField<int, MutatorPoolComposeConfigure> poolComposeDict_ = new MapField<int, MutatorPoolComposeConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MutatorConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MutatorReflection.Descriptor.MessageTypes[5];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MutatorInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MutatorInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MutatorPoolConfigure> Pools => pools_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MutatorPoolConfigure> PoolDict => poolDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MutatorPoolComposeConfigure> PoolComposes => poolComposes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MutatorPoolComposeConfigure> PoolComposeDict => poolComposeDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MutatorConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MutatorConfigure(MutatorConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		pools_ = other.pools_.Clone();
		poolDict_ = other.poolDict_.Clone();
		poolComposes_ = other.poolComposes_.Clone();
		poolComposeDict_ = other.poolComposeDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MutatorConfigure Clone()
	{
		return new MutatorConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MutatorConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MutatorConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (!InfoDict.Equals(other.InfoDict))
		{
			return false;
		}
		if (!pools_.Equals(other.pools_))
		{
			return false;
		}
		if (!PoolDict.Equals(other.PoolDict))
		{
			return false;
		}
		if (!poolComposes_.Equals(other.poolComposes_))
		{
			return false;
		}
		if (!PoolComposeDict.Equals(other.PoolComposeDict))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
		num ^= pools_.GetHashCode();
		num ^= PoolDict.GetHashCode();
		num ^= poolComposes_.GetHashCode();
		num ^= PoolComposeDict.GetHashCode();
		if (_unknownFields != null)
		{
			num ^= _unknownFields.GetHashCode();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override string ToString()
	{
		return JsonFormatter.ToDiagnosticString(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void WriteTo(CodedOutputStream output)
	{
		output.WriteRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalWriteTo(ref WriteContext output)
	{
		infos_.WriteTo(ref output, _repeated_infos_codec);
		infoDict_.WriteTo(ref output, _map_infoDict_codec);
		pools_.WriteTo(ref output, _repeated_pools_codec);
		poolDict_.WriteTo(ref output, _map_poolDict_codec);
		poolComposes_.WriteTo(ref output, _repeated_poolComposes_codec);
		poolComposeDict_.WriteTo(ref output, _map_poolComposeDict_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref output);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += infos_.CalculateSize(_repeated_infos_codec);
		num += infoDict_.CalculateSize(_map_infoDict_codec);
		num += pools_.CalculateSize(_repeated_pools_codec);
		num += poolDict_.CalculateSize(_map_poolDict_codec);
		num += poolComposes_.CalculateSize(_repeated_poolComposes_codec);
		num += poolComposeDict_.CalculateSize(_map_poolComposeDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MutatorConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			pools_.Add(other.pools_);
			poolDict_.MergeFrom(other.poolDict_);
			poolComposes_.Add(other.poolComposes_);
			poolComposeDict_.MergeFrom(other.poolComposeDict_);
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CodedInputStream input)
	{
		input.ReadRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalMergeFrom(ref ParseContext input)
	{
		uint num;
		while ((num = input.ReadTag()) != 0)
		{
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				break;
			case 10u:
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 18u:
				infoDict_.AddEntriesFrom(ref input, _map_infoDict_codec);
				break;
			case 26u:
				pools_.AddEntriesFrom(ref input, _repeated_pools_codec);
				break;
			case 34u:
				poolDict_.AddEntriesFrom(ref input, _map_poolDict_codec);
				break;
			case 42u:
				poolComposes_.AddEntriesFrom(ref input, _repeated_poolComposes_codec);
				break;
			case 50u:
				poolComposeDict_.AddEntriesFrom(ref input, _map_poolComposeDict_codec);
				break;
			}
		}
	}
}
