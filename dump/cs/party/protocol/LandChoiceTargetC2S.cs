using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class LandChoiceTargetC2S : IMessage<LandChoiceTargetC2S>, IMessage, IEquatable<LandChoiceTargetC2S>, IDeepCloneable<LandChoiceTargetC2S>, IBufferMessage
{
	private static readonly MessageParser<LandChoiceTargetC2S> _parser = new MessageParser<LandChoiceTargetC2S>(() => new LandChoiceTargetC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int LandTypeFieldNumber = 2;

	private int landType_;

	public const int TargetNumFieldNumber = 3;

	private int targetNum_;

	public const int CanTargetIdsFieldNumber = 4;

	private static readonly MapField<long, bool>.Codec _map_canTargetIds_codec = new MapField<long, bool>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForBool(16u, defaultValue: false), 34u);

	private readonly MapField<long, bool> canTargetIds_ = new MapField<long, bool>();

	public const int TargetIdsFieldNumber = 5;

	private static readonly FieldCodec<long> _repeated_targetIds_codec = FieldCodec.ForSFixed64(42u);

	private readonly RepeatedField<long> targetIds_ = new RepeatedField<long>();

	public const int ExitFieldNumber = 6;

	private bool exit_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LandChoiceTargetC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[331];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LandType
	{
		get
		{
			return landType_;
		}
		set
		{
			landType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TargetNum
	{
		get
		{
			return targetNum_;
		}
		set
		{
			targetNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, bool> CanTargetIds => canTargetIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> TargetIds => targetIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Exit
	{
		get
		{
			return exit_;
		}
		set
		{
			exit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandChoiceTargetC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandChoiceTargetC2S(LandChoiceTargetC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		landType_ = other.landType_;
		targetNum_ = other.targetNum_;
		canTargetIds_ = other.canTargetIds_.Clone();
		targetIds_ = other.targetIds_.Clone();
		exit_ = other.exit_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandChoiceTargetC2S Clone()
	{
		return new LandChoiceTargetC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LandChoiceTargetC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LandChoiceTargetC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (LandType != other.LandType)
		{
			return false;
		}
		if (TargetNum != other.TargetNum)
		{
			return false;
		}
		if (!CanTargetIds.Equals(other.CanTargetIds))
		{
			return false;
		}
		if (!targetIds_.Equals(other.targetIds_))
		{
			return false;
		}
		if (Exit != other.Exit)
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		if (LandType != 0)
		{
			num ^= LandType.GetHashCode();
		}
		if (TargetNum != 0)
		{
			num ^= TargetNum.GetHashCode();
		}
		num ^= CanTargetIds.GetHashCode();
		num ^= targetIds_.GetHashCode();
		if (Exit)
		{
			num ^= Exit.GetHashCode();
		}
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		if (LandType != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(LandType);
		}
		if (TargetNum != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(TargetNum);
		}
		canTargetIds_.WriteTo(ref output, _map_canTargetIds_codec);
		targetIds_.WriteTo(ref output, _repeated_targetIds_codec);
		if (Exit)
		{
			output.WriteRawTag(48);
			output.WriteBool(Exit);
		}
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		if (LandType != 0)
		{
			num += 5;
		}
		if (TargetNum != 0)
		{
			num += 5;
		}
		num += canTargetIds_.CalculateSize(_map_canTargetIds_codec);
		num += targetIds_.CalculateSize(_repeated_targetIds_codec);
		if (Exit)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LandChoiceTargetC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		if (other.LandType != 0)
		{
			LandType = other.LandType;
		}
		if (other.TargetNum != 0)
		{
			TargetNum = other.TargetNum;
		}
		canTargetIds_.MergeFrom(other.canTargetIds_);
		targetIds_.Add(other.targetIds_);
		if (other.Exit)
		{
			Exit = other.Exit;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 21u:
				LandType = input.ReadSFixed32();
				break;
			case 29u:
				TargetNum = input.ReadSFixed32();
				break;
			case 34u:
				canTargetIds_.AddEntriesFrom(ref input, _map_canTargetIds_codec);
				break;
			case 41u:
			case 42u:
				targetIds_.AddEntriesFrom(ref input, _repeated_targetIds_codec);
				break;
			case 48u:
				Exit = input.ReadBool();
				break;
			}
		}
	}
}
