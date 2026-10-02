using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RelicConfigure : IMessage<RelicConfigure>, IMessage, IEquatable<RelicConfigure>, IDeepCloneable<RelicConfigure>, IBufferMessage
{
	private static readonly MessageParser<RelicConfigure> _parser = new MessageParser<RelicConfigure>(() => new RelicConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<RelicInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, RelicInfoConfigure.Parser);

	private readonly RepeatedField<RelicInfoConfigure> infos_ = new RepeatedField<RelicInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, RelicInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, RelicInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RelicInfoConfigure.Parser), 18u);

	private readonly MapField<int, RelicInfoConfigure> infoDict_ = new MapField<int, RelicInfoConfigure>();

	public const int ParamssFieldNumber = 3;

	private static readonly FieldCodec<RelicParamsConfigure> _repeated_paramss_codec = FieldCodec.ForMessage(26u, RelicParamsConfigure.Parser);

	private readonly RepeatedField<RelicParamsConfigure> paramss_ = new RepeatedField<RelicParamsConfigure>();

	public const int ParamsDictFieldNumber = 4;

	private static readonly MapField<int, RelicParamsConfigure>.Codec _map_paramsDict_codec = new MapField<int, RelicParamsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RelicParamsConfigure.Parser), 34u);

	private readonly MapField<int, RelicParamsConfigure> paramsDict_ = new MapField<int, RelicParamsConfigure>();

	public const int KeywordssFieldNumber = 5;

	private static readonly FieldCodec<RelicKeywordsConfigure> _repeated_keywordss_codec = FieldCodec.ForMessage(42u, RelicKeywordsConfigure.Parser);

	private readonly RepeatedField<RelicKeywordsConfigure> keywordss_ = new RepeatedField<RelicKeywordsConfigure>();

	public const int KeywordsDictFieldNumber = 6;

	private static readonly MapField<int, RelicKeywordsConfigure>.Codec _map_keywordsDict_codec = new MapField<int, RelicKeywordsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RelicKeywordsConfigure.Parser), 50u);

	private readonly MapField<int, RelicKeywordsConfigure> keywordsDict_ = new MapField<int, RelicKeywordsConfigure>();

	public const int CharacterRelicsFieldNumber = 7;

	private static readonly FieldCodec<RelicCharacterRelicConfigure> _repeated_characterRelics_codec = FieldCodec.ForMessage(58u, RelicCharacterRelicConfigure.Parser);

	private readonly RepeatedField<RelicCharacterRelicConfigure> characterRelics_ = new RepeatedField<RelicCharacterRelicConfigure>();

	public const int CharacterRelicDictFieldNumber = 8;

	private static readonly MapField<int, RelicCharacterRelicConfigure>.Codec _map_characterRelicDict_codec = new MapField<int, RelicCharacterRelicConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, RelicCharacterRelicConfigure.Parser), 66u);

	private readonly MapField<int, RelicCharacterRelicConfigure> characterRelicDict_ = new MapField<int, RelicCharacterRelicConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RelicConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RelicReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RelicInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RelicInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RelicParamsConfigure> Paramss => paramss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RelicParamsConfigure> ParamsDict => paramsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RelicKeywordsConfigure> Keywordss => keywordss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RelicKeywordsConfigure> KeywordsDict => keywordsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<RelicCharacterRelicConfigure> CharacterRelics => characterRelics_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RelicCharacterRelicConfigure> CharacterRelicDict => characterRelicDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicConfigure(RelicConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		paramss_ = other.paramss_.Clone();
		paramsDict_ = other.paramsDict_.Clone();
		keywordss_ = other.keywordss_.Clone();
		keywordsDict_ = other.keywordsDict_.Clone();
		characterRelics_ = other.characterRelics_.Clone();
		characterRelicDict_ = other.characterRelicDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicConfigure Clone()
	{
		return new RelicConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RelicConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RelicConfigure other)
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
		if (!paramss_.Equals(other.paramss_))
		{
			return false;
		}
		if (!ParamsDict.Equals(other.ParamsDict))
		{
			return false;
		}
		if (!keywordss_.Equals(other.keywordss_))
		{
			return false;
		}
		if (!KeywordsDict.Equals(other.KeywordsDict))
		{
			return false;
		}
		if (!characterRelics_.Equals(other.characterRelics_))
		{
			return false;
		}
		if (!CharacterRelicDict.Equals(other.CharacterRelicDict))
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
		num ^= paramss_.GetHashCode();
		num ^= ParamsDict.GetHashCode();
		num ^= keywordss_.GetHashCode();
		num ^= KeywordsDict.GetHashCode();
		num ^= characterRelics_.GetHashCode();
		num ^= CharacterRelicDict.GetHashCode();
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
		paramss_.WriteTo(ref output, _repeated_paramss_codec);
		paramsDict_.WriteTo(ref output, _map_paramsDict_codec);
		keywordss_.WriteTo(ref output, _repeated_keywordss_codec);
		keywordsDict_.WriteTo(ref output, _map_keywordsDict_codec);
		characterRelics_.WriteTo(ref output, _repeated_characterRelics_codec);
		characterRelicDict_.WriteTo(ref output, _map_characterRelicDict_codec);
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
		num += paramss_.CalculateSize(_repeated_paramss_codec);
		num += paramsDict_.CalculateSize(_map_paramsDict_codec);
		num += keywordss_.CalculateSize(_repeated_keywordss_codec);
		num += keywordsDict_.CalculateSize(_map_keywordsDict_codec);
		num += characterRelics_.CalculateSize(_repeated_characterRelics_codec);
		num += characterRelicDict_.CalculateSize(_map_characterRelicDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RelicConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			paramss_.Add(other.paramss_);
			paramsDict_.MergeFrom(other.paramsDict_);
			keywordss_.Add(other.keywordss_);
			keywordsDict_.MergeFrom(other.keywordsDict_);
			characterRelics_.Add(other.characterRelics_);
			characterRelicDict_.MergeFrom(other.characterRelicDict_);
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
				paramss_.AddEntriesFrom(ref input, _repeated_paramss_codec);
				break;
			case 34u:
				paramsDict_.AddEntriesFrom(ref input, _map_paramsDict_codec);
				break;
			case 42u:
				keywordss_.AddEntriesFrom(ref input, _repeated_keywordss_codec);
				break;
			case 50u:
				keywordsDict_.AddEntriesFrom(ref input, _map_keywordsDict_codec);
				break;
			case 58u:
				characterRelics_.AddEntriesFrom(ref input, _repeated_characterRelics_codec);
				break;
			case 66u:
				characterRelicDict_.AddEntriesFrom(ref input, _map_characterRelicDict_codec);
				break;
			}
		}
	}
}
