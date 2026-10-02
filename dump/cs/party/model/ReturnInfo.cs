using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ReturnInfo : IMessage<ReturnInfo>, IMessage, IEquatable<ReturnInfo>, IDeepCloneable<ReturnInfo>, IBufferMessage
{
	private static readonly MessageParser<ReturnInfo> _parser = new MessageParser<ReturnInfo>(() => new ReturnInfo());

	private UnknownFieldSet _unknownFields;

	public const int TriggerTimeFieldNumber = 1;

	private long triggerTime_;

	public const int EndTimeFieldNumber = 2;

	private long endTime_;

	public const int FreeGiftClaimedFieldNumber = 3;

	private bool freeGiftClaimed_;

	public const int SurveyStateFieldNumber = 4;

	private int surveyState_;

	public const int SignInFieldNumber = 5;

	private ReturnSignIn signIn_;

	public const int TriggerCountFieldNumber = 6;

	private int triggerCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ReturnInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long TriggerTime
	{
		get
		{
			return triggerTime_;
		}
		set
		{
			triggerTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long EndTime
	{
		get
		{
			return endTime_;
		}
		set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool FreeGiftClaimed
	{
		get
		{
			return freeGiftClaimed_;
		}
		set
		{
			freeGiftClaimed_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SurveyState
	{
		get
		{
			return surveyState_;
		}
		set
		{
			surveyState_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignIn SignIn
	{
		get
		{
			return signIn_;
		}
		set
		{
			signIn_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TriggerCount
	{
		get
		{
			return triggerCount_;
		}
		set
		{
			triggerCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnInfo(ReturnInfo other)
		: this()
	{
		triggerTime_ = other.triggerTime_;
		endTime_ = other.endTime_;
		freeGiftClaimed_ = other.freeGiftClaimed_;
		surveyState_ = other.surveyState_;
		signIn_ = ((other.signIn_ != null) ? other.signIn_.Clone() : null);
		triggerCount_ = other.triggerCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnInfo Clone()
	{
		return new ReturnInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ReturnInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ReturnInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (TriggerTime != other.TriggerTime)
		{
			return false;
		}
		if (EndTime != other.EndTime)
		{
			return false;
		}
		if (FreeGiftClaimed != other.FreeGiftClaimed)
		{
			return false;
		}
		if (SurveyState != other.SurveyState)
		{
			return false;
		}
		if (!object.Equals(SignIn, other.SignIn))
		{
			return false;
		}
		if (TriggerCount != other.TriggerCount)
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
		if (TriggerTime != 0L)
		{
			num ^= TriggerTime.GetHashCode();
		}
		if (EndTime != 0L)
		{
			num ^= EndTime.GetHashCode();
		}
		if (FreeGiftClaimed)
		{
			num ^= FreeGiftClaimed.GetHashCode();
		}
		if (SurveyState != 0)
		{
			num ^= SurveyState.GetHashCode();
		}
		if (signIn_ != null)
		{
			num ^= SignIn.GetHashCode();
		}
		if (TriggerCount != 0)
		{
			num ^= TriggerCount.GetHashCode();
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
		if (TriggerTime != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(TriggerTime);
		}
		if (EndTime != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(EndTime);
		}
		if (FreeGiftClaimed)
		{
			output.WriteRawTag(24);
			output.WriteBool(FreeGiftClaimed);
		}
		if (SurveyState != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(SurveyState);
		}
		if (signIn_ != null)
		{
			output.WriteRawTag(42);
			output.WriteMessage(SignIn);
		}
		if (TriggerCount != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(TriggerCount);
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
		if (TriggerTime != 0L)
		{
			num += 9;
		}
		if (EndTime != 0L)
		{
			num += 9;
		}
		if (FreeGiftClaimed)
		{
			num += 2;
		}
		if (SurveyState != 0)
		{
			num += 5;
		}
		if (signIn_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SignIn);
		}
		if (TriggerCount != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ReturnInfo other)
	{
		if (other == null)
		{
			return;
		}
		if (other.TriggerTime != 0L)
		{
			TriggerTime = other.TriggerTime;
		}
		if (other.EndTime != 0L)
		{
			EndTime = other.EndTime;
		}
		if (other.FreeGiftClaimed)
		{
			FreeGiftClaimed = other.FreeGiftClaimed;
		}
		if (other.SurveyState != 0)
		{
			SurveyState = other.SurveyState;
		}
		if (other.signIn_ != null)
		{
			if (signIn_ == null)
			{
				SignIn = new ReturnSignIn();
			}
			SignIn.MergeFrom(other.SignIn);
		}
		if (other.TriggerCount != 0)
		{
			TriggerCount = other.TriggerCount;
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
			case 9u:
				TriggerTime = input.ReadSFixed64();
				break;
			case 17u:
				EndTime = input.ReadSFixed64();
				break;
			case 24u:
				FreeGiftClaimed = input.ReadBool();
				break;
			case 37u:
				SurveyState = input.ReadSFixed32();
				break;
			case 42u:
				if (signIn_ == null)
				{
					SignIn = new ReturnSignIn();
				}
				input.ReadMessage(SignIn);
				break;
			case 53u:
				TriggerCount = input.ReadSFixed32();
				break;
			}
		}
	}
}
