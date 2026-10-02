using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class FixMapConfigure : IMessage<FixMapConfigure>, IMessage, IEquatable<FixMapConfigure>, IDeepCloneable<FixMapConfigure>, IBufferMessage
{
	private static readonly MessageParser<FixMapConfigure> _parser = new MessageParser<FixMapConfigure>(() => new FixMapConfigure());

	private UnknownFieldSet _unknownFields;

	public const int MapLevelsFieldNumber = 1;

	private static readonly FieldCodec<FixMapMapLevelConfigure> _repeated_mapLevels_codec = FieldCodec.ForMessage(10u, FixMapMapLevelConfigure.Parser);

	private readonly RepeatedField<FixMapMapLevelConfigure> mapLevels_ = new RepeatedField<FixMapMapLevelConfigure>();

	public const int MapLevelDictFieldNumber = 2;

	private static readonly MapField<int, FixMapMapLevelConfigure>.Codec _map_mapLevelDict_codec = new MapField<int, FixMapMapLevelConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, FixMapMapLevelConfigure.Parser), 18u);

	private readonly MapField<int, FixMapMapLevelConfigure> mapLevelDict_ = new MapField<int, FixMapMapLevelConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<FixMapConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FixMapReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FixMapMapLevelConfigure> MapLevels => mapLevels_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, FixMapMapLevelConfigure> MapLevelDict => mapLevelDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixMapConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixMapConfigure(FixMapConfigure other)
		: this()
	{
		mapLevels_ = other.mapLevels_.Clone();
		mapLevelDict_ = other.mapLevelDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FixMapConfigure Clone()
	{
		return new FixMapConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FixMapConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(FixMapConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!mapLevels_.Equals(other.mapLevels_))
		{
			return false;
		}
		if (!MapLevelDict.Equals(other.MapLevelDict))
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
		num ^= mapLevels_.GetHashCode();
		num ^= MapLevelDict.GetHashCode();
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
		mapLevels_.WriteTo(ref output, _repeated_mapLevels_codec);
		mapLevelDict_.WriteTo(ref output, _map_mapLevelDict_codec);
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
		num += mapLevels_.CalculateSize(_repeated_mapLevels_codec);
		num += mapLevelDict_.CalculateSize(_map_mapLevelDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(FixMapConfigure other)
	{
		if (other != null)
		{
			mapLevels_.Add(other.mapLevels_);
			mapLevelDict_.MergeFrom(other.mapLevelDict_);
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
				mapLevels_.AddEntriesFrom(ref input, _repeated_mapLevels_codec);
				break;
			case 18u:
				mapLevelDict_.AddEntriesFrom(ref input, _map_mapLevelDict_codec);
				break;
			}
		}
	}
}
