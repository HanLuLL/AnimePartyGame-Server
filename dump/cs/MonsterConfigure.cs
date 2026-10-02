using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MonsterConfigure : IMessage<MonsterConfigure>, IMessage, IEquatable<MonsterConfigure>, IDeepCloneable<MonsterConfigure>, IBufferMessage
{
	private static readonly MessageParser<MonsterConfigure> _parser = new MessageParser<MonsterConfigure>(() => new MonsterConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<MonsterInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, MonsterInfoConfigure.Parser);

	private readonly RepeatedField<MonsterInfoConfigure> infos_ = new RepeatedField<MonsterInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, MonsterInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, MonsterInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MonsterInfoConfigure.Parser), 18u);

	private readonly MapField<int, MonsterInfoConfigure> infoDict_ = new MapField<int, MonsterInfoConfigure>();

	public const int AttributesFieldNumber = 3;

	private static readonly FieldCodec<MonsterAttributeConfigure> _repeated_attributes_codec = FieldCodec.ForMessage(26u, MonsterAttributeConfigure.Parser);

	private readonly RepeatedField<MonsterAttributeConfigure> attributes_ = new RepeatedField<MonsterAttributeConfigure>();

	public const int AttributeDictFieldNumber = 4;

	private static readonly MapField<int, MonsterAttributeConfigure>.Codec _map_attributeDict_codec = new MapField<int, MonsterAttributeConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MonsterAttributeConfigure.Parser), 34u);

	private readonly MapField<int, MonsterAttributeConfigure> attributeDict_ = new MapField<int, MonsterAttributeConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MonsterConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MonsterReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MonsterInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MonsterInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MonsterAttributeConfigure> Attributes => attributes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MonsterAttributeConfigure> AttributeDict => attributeDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterConfigure(MonsterConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		attributes_ = other.attributes_.Clone();
		attributeDict_ = other.attributeDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterConfigure Clone()
	{
		return new MonsterConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MonsterConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MonsterConfigure other)
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
		if (!attributes_.Equals(other.attributes_))
		{
			return false;
		}
		if (!AttributeDict.Equals(other.AttributeDict))
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
		num ^= attributes_.GetHashCode();
		num ^= AttributeDict.GetHashCode();
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
		attributes_.WriteTo(ref output, _repeated_attributes_codec);
		attributeDict_.WriteTo(ref output, _map_attributeDict_codec);
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
		num += attributes_.CalculateSize(_repeated_attributes_codec);
		num += attributeDict_.CalculateSize(_map_attributeDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MonsterConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			attributes_.Add(other.attributes_);
			attributeDict_.MergeFrom(other.attributeDict_);
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
				attributes_.AddEntriesFrom(ref input, _repeated_attributes_codec);
				break;
			case 34u:
				attributeDict_.AddEntriesFrom(ref input, _map_attributeDict_codec);
				break;
			}
		}
	}
}
