using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class TrialConfigure : IMessage<TrialConfigure>, IMessage, IEquatable<TrialConfigure>, IDeepCloneable<TrialConfigure>, IBufferMessage
{
	private static readonly MessageParser<TrialConfigure> _parser = new MessageParser<TrialConfigure>(() => new TrialConfigure());

	private UnknownFieldSet _unknownFields;

	public const int ActivitysFieldNumber = 1;

	private static readonly FieldCodec<TrialActivityConfigure> _repeated_activitys_codec = FieldCodec.ForMessage(10u, TrialActivityConfigure.Parser);

	private readonly RepeatedField<TrialActivityConfigure> activitys_ = new RepeatedField<TrialActivityConfigure>();

	public const int ActivityDictFieldNumber = 2;

	private static readonly MapField<int, TrialActivityConfigure>.Codec _map_activityDict_codec = new MapField<int, TrialActivityConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TrialActivityConfigure.Parser), 18u);

	private readonly MapField<int, TrialActivityConfigure> activityDict_ = new MapField<int, TrialActivityConfigure>();

	public const int ParamssFieldNumber = 3;

	private static readonly FieldCodec<TrialParamsConfigure> _repeated_paramss_codec = FieldCodec.ForMessage(26u, TrialParamsConfigure.Parser);

	private readonly RepeatedField<TrialParamsConfigure> paramss_ = new RepeatedField<TrialParamsConfigure>();

	public const int ParamsDictFieldNumber = 4;

	private static readonly MapField<int, TrialParamsConfigure>.Codec _map_paramsDict_codec = new MapField<int, TrialParamsConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, TrialParamsConfigure.Parser), 34u);

	private readonly MapField<int, TrialParamsConfigure> paramsDict_ = new MapField<int, TrialParamsConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TrialConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => TrialReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TrialActivityConfigure> Activitys => activitys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TrialActivityConfigure> ActivityDict => activityDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<TrialParamsConfigure> Paramss => paramss_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, TrialParamsConfigure> ParamsDict => paramsDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TrialConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TrialConfigure(TrialConfigure other)
		: this()
	{
		activitys_ = other.activitys_.Clone();
		activityDict_ = other.activityDict_.Clone();
		paramss_ = other.paramss_.Clone();
		paramsDict_ = other.paramsDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TrialConfigure Clone()
	{
		return new TrialConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TrialConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TrialConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!activitys_.Equals(other.activitys_))
		{
			return false;
		}
		if (!ActivityDict.Equals(other.ActivityDict))
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
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= activitys_.GetHashCode();
		num ^= ActivityDict.GetHashCode();
		num ^= paramss_.GetHashCode();
		num ^= ParamsDict.GetHashCode();
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
		activitys_.WriteTo(ref output, _repeated_activitys_codec);
		activityDict_.WriteTo(ref output, _map_activityDict_codec);
		paramss_.WriteTo(ref output, _repeated_paramss_codec);
		paramsDict_.WriteTo(ref output, _map_paramsDict_codec);
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
		num += activitys_.CalculateSize(_repeated_activitys_codec);
		num += activityDict_.CalculateSize(_map_activityDict_codec);
		num += paramss_.CalculateSize(_repeated_paramss_codec);
		num += paramsDict_.CalculateSize(_map_paramsDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TrialConfigure other)
	{
		if (other != null)
		{
			activitys_.Add(other.activitys_);
			activityDict_.MergeFrom(other.activityDict_);
			paramss_.Add(other.paramss_);
			paramsDict_.MergeFrom(other.paramsDict_);
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
				activitys_.AddEntriesFrom(ref input, _repeated_activitys_codec);
				break;
			case 18u:
				activityDict_.AddEntriesFrom(ref input, _map_activityDict_codec);
				break;
			case 26u:
				paramss_.AddEntriesFrom(ref input, _repeated_paramss_codec);
				break;
			case 34u:
				paramsDict_.AddEntriesFrom(ref input, _map_paramsDict_codec);
				break;
			}
		}
	}
}
