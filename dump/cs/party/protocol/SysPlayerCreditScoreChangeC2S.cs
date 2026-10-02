using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SysPlayerCreditScoreChangeC2S : IMessage<SysPlayerCreditScoreChangeC2S>, IMessage, IEquatable<SysPlayerCreditScoreChangeC2S>, IDeepCloneable<SysPlayerCreditScoreChangeC2S>, IBufferMessage
{
	private static readonly MessageParser<SysPlayerCreditScoreChangeC2S> _parser = new MessageParser<SysPlayerCreditScoreChangeC2S>(() => new SysPlayerCreditScoreChangeC2S());

	private UnknownFieldSet _unknownFields;

	public const int ChangeScoreFieldNumber = 1;

	private int changeScore_;

	public const int RemarkFieldNumber = 2;

	private string remark_ = "";

	public const int ActionTypeFieldNumber = 3;

	private int actionType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysPlayerCreditScoreChangeC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[508];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ChangeScore
	{
		get
		{
			return changeScore_;
		}
		set
		{
			changeScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Remark
	{
		get
		{
			return remark_;
		}
		set
		{
			remark_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActionType
	{
		get
		{
			return actionType_;
		}
		set
		{
			actionType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPlayerCreditScoreChangeC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPlayerCreditScoreChangeC2S(SysPlayerCreditScoreChangeC2S other)
		: this()
	{
		changeScore_ = other.changeScore_;
		remark_ = other.remark_;
		actionType_ = other.actionType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPlayerCreditScoreChangeC2S Clone()
	{
		return new SysPlayerCreditScoreChangeC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysPlayerCreditScoreChangeC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysPlayerCreditScoreChangeC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ChangeScore != other.ChangeScore)
		{
			return false;
		}
		if (Remark != other.Remark)
		{
			return false;
		}
		if (ActionType != other.ActionType)
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
		if (ChangeScore != 0)
		{
			num ^= ChangeScore.GetHashCode();
		}
		if (Remark.Length != 0)
		{
			num ^= Remark.GetHashCode();
		}
		if (ActionType != 0)
		{
			num ^= ActionType.GetHashCode();
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
		if (ChangeScore != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ChangeScore);
		}
		if (Remark.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Remark);
		}
		if (ActionType != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ActionType);
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
		if (ChangeScore != 0)
		{
			num += 5;
		}
		if (Remark.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Remark);
		}
		if (ActionType != 0)
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
	public void MergeFrom(SysPlayerCreditScoreChangeC2S other)
	{
		if (other != null)
		{
			if (other.ChangeScore != 0)
			{
				ChangeScore = other.ChangeScore;
			}
			if (other.Remark.Length != 0)
			{
				Remark = other.Remark;
			}
			if (other.ActionType != 0)
			{
				ActionType = other.ActionType;
			}
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
			case 13u:
				ChangeScore = input.ReadSFixed32();
				break;
			case 18u:
				Remark = input.ReadString();
				break;
			case 29u:
				ActionType = input.ReadSFixed32();
				break;
			}
		}
	}
}
