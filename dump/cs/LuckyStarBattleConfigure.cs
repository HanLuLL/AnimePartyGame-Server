using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class LuckyStarBattleConfigure : IMessage<LuckyStarBattleConfigure>, IMessage, IEquatable<LuckyStarBattleConfigure>, IDeepCloneable<LuckyStarBattleConfigure>, IBufferMessage
{
	private static readonly MessageParser<LuckyStarBattleConfigure> _parser = new MessageParser<LuckyStarBattleConfigure>(() => new LuckyStarBattleConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<LuckyStarBattleInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, LuckyStarBattleInfoConfigure.Parser);

	private readonly RepeatedField<LuckyStarBattleInfoConfigure> infos_ = new RepeatedField<LuckyStarBattleInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, LuckyStarBattleInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, LuckyStarBattleInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, LuckyStarBattleInfoConfigure.Parser), 18u);

	private readonly MapField<int, LuckyStarBattleInfoConfigure> infoDict_ = new MapField<int, LuckyStarBattleInfoConfigure>();

	public const int ParamsFieldNumber = 3;

	private static readonly FieldCodec<LuckyStarBattleParamConfigure> _repeated_params_codec = FieldCodec.ForMessage(26u, LuckyStarBattleParamConfigure.Parser);

	private readonly RepeatedField<LuckyStarBattleParamConfigure> params_ = new RepeatedField<LuckyStarBattleParamConfigure>();

	public const int ParamDictFieldNumber = 4;

	private static readonly MapField<int, LuckyStarBattleParamConfigure>.Codec _map_paramDict_codec = new MapField<int, LuckyStarBattleParamConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, LuckyStarBattleParamConfigure.Parser), 34u);

	private readonly MapField<int, LuckyStarBattleParamConfigure> paramDict_ = new MapField<int, LuckyStarBattleParamConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LuckyStarBattleConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => LuckyStarBattleReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<LuckyStarBattleInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, LuckyStarBattleInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<LuckyStarBattleParamConfigure> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, LuckyStarBattleParamConfigure> ParamDict => paramDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarBattleConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarBattleConfigure(LuckyStarBattleConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		params_ = other.params_.Clone();
		paramDict_ = other.paramDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarBattleConfigure Clone()
	{
		return new LuckyStarBattleConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LuckyStarBattleConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LuckyStarBattleConfigure other)
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
		if (!params_.Equals(other.params_))
		{
			return false;
		}
		if (!ParamDict.Equals(other.ParamDict))
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
		num ^= params_.GetHashCode();
		num ^= ParamDict.GetHashCode();
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
		params_.WriteTo(ref output, _repeated_params_codec);
		paramDict_.WriteTo(ref output, _map_paramDict_codec);
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
		num += params_.CalculateSize(_repeated_params_codec);
		num += paramDict_.CalculateSize(_map_paramDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LuckyStarBattleConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			params_.Add(other.params_);
			paramDict_.MergeFrom(other.paramDict_);
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
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 34u:
				paramDict_.AddEntriesFrom(ref input, _map_paramDict_codec);
				break;
			}
		}
	}
}
