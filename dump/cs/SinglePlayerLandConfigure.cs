using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerLandConfigure : IMessage<SinglePlayerLandConfigure>, IMessage, IEquatable<SinglePlayerLandConfigure>, IDeepCloneable<SinglePlayerLandConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerLandConfigure> _parser = new MessageParser<SinglePlayerLandConfigure>(() => new SinglePlayerLandConfigure());

	private UnknownFieldSet _unknownFields;

	public const int SinglePlayerLandTypeFieldNumber = 1;

	private SinglePlayerLandType singlePlayerLandType_;

	public const int LandPrefabFieldNumber = 2;

	private string landPrefab_ = "";

	public const int LandPerformTimelineFieldNumber = 3;

	private string landPerformTimeline_ = "";

	public const int ParamsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(34u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerLandConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[16];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLandType SinglePlayerLandType
	{
		get
		{
			return singlePlayerLandType_;
		}
		private set
		{
			singlePlayerLandType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LandPrefab
	{
		get
		{
			return landPrefab_;
		}
		private set
		{
			landPrefab_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string LandPerformTimeline
	{
		get
		{
			return landPerformTimeline_;
		}
		private set
		{
			landPerformTimeline_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLandConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLandConfigure(SinglePlayerLandConfigure other)
		: this()
	{
		singlePlayerLandType_ = other.singlePlayerLandType_;
		landPrefab_ = other.landPrefab_;
		landPerformTimeline_ = other.landPerformTimeline_;
		params_ = other.params_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerLandConfigure Clone()
	{
		return new SinglePlayerLandConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerLandConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerLandConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (SinglePlayerLandType != other.SinglePlayerLandType)
		{
			return false;
		}
		if (LandPrefab != other.LandPrefab)
		{
			return false;
		}
		if (LandPerformTimeline != other.LandPerformTimeline)
		{
			return false;
		}
		if (!params_.Equals(other.params_))
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
		if (SinglePlayerLandType != SinglePlayerLandType.None)
		{
			num ^= SinglePlayerLandType.GetHashCode();
		}
		if (LandPrefab.Length != 0)
		{
			num ^= LandPrefab.GetHashCode();
		}
		if (LandPerformTimeline.Length != 0)
		{
			num ^= LandPerformTimeline.GetHashCode();
		}
		num ^= params_.GetHashCode();
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
		if (SinglePlayerLandType != SinglePlayerLandType.None)
		{
			output.WriteRawTag(8);
			output.WriteEnum((int)SinglePlayerLandType);
		}
		if (LandPrefab.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(LandPrefab);
		}
		if (LandPerformTimeline.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(LandPerformTimeline);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
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
		if (SinglePlayerLandType != SinglePlayerLandType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)SinglePlayerLandType);
		}
		if (LandPrefab.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LandPrefab);
		}
		if (LandPerformTimeline.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(LandPerformTimeline);
		}
		num += params_.CalculateSize(_repeated_params_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerLandConfigure other)
	{
		if (other != null)
		{
			if (other.SinglePlayerLandType != SinglePlayerLandType.None)
			{
				SinglePlayerLandType = other.SinglePlayerLandType;
			}
			if (other.LandPrefab.Length != 0)
			{
				LandPrefab = other.LandPrefab;
			}
			if (other.LandPerformTimeline.Length != 0)
			{
				LandPerformTimeline = other.LandPerformTimeline;
			}
			params_.Add(other.params_);
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
			case 8u:
				SinglePlayerLandType = (SinglePlayerLandType)input.ReadEnum();
				break;
			case 18u:
				LandPrefab = input.ReadString();
				break;
			case 26u:
				LandPerformTimeline = input.ReadString();
				break;
			case 32u:
			case 34u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			}
		}
	}
}
