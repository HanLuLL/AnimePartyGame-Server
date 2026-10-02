using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class MatchCreditActionConfigure : IMessage<MatchCreditActionConfigure>, IMessage, IEquatable<MatchCreditActionConfigure>, IDeepCloneable<MatchCreditActionConfigure>, IBufferMessage
{
	private static readonly MessageParser<MatchCreditActionConfigure> _parser = new MessageParser<MatchCreditActionConfigure>(() => new MatchCreditActionConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int CreditActionTypeFieldNumber = 2;

	private CreditActionType creditActionType_;

	public const int ScoreChangeFieldNumber = 3;

	private int scoreChange_;

	public const int ActionNameFieldNumber = 4;

	private int actionName_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MatchCreditActionConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MatchReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreditActionType CreditActionType
	{
		get
		{
			return creditActionType_;
		}
		private set
		{
			creditActionType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ScoreChange
	{
		get
		{
			return scoreChange_;
		}
		private set
		{
			scoreChange_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActionName
	{
		get
		{
			return actionName_;
		}
		private set
		{
			actionName_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchCreditActionConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchCreditActionConfigure(MatchCreditActionConfigure other)
		: this()
	{
		id_ = other.id_;
		creditActionType_ = other.creditActionType_;
		scoreChange_ = other.scoreChange_;
		actionName_ = other.actionName_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchCreditActionConfigure Clone()
	{
		return new MatchCreditActionConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MatchCreditActionConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MatchCreditActionConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (CreditActionType != other.CreditActionType)
		{
			return false;
		}
		if (ScoreChange != other.ScoreChange)
		{
			return false;
		}
		if (ActionName != other.ActionName)
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (CreditActionType != CreditActionType.None)
		{
			num ^= CreditActionType.GetHashCode();
		}
		if (ScoreChange != 0)
		{
			num ^= ScoreChange.GetHashCode();
		}
		if (ActionName != 0)
		{
			num ^= ActionName.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (CreditActionType != CreditActionType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)CreditActionType);
		}
		if (ScoreChange != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(ScoreChange);
		}
		if (ActionName != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ActionName);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (CreditActionType != CreditActionType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)CreditActionType);
		}
		if (ScoreChange != 0)
		{
			num += 5;
		}
		if (ActionName != 0)
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
	public void MergeFrom(MatchCreditActionConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.CreditActionType != CreditActionType.None)
			{
				CreditActionType = other.CreditActionType;
			}
			if (other.ScoreChange != 0)
			{
				ScoreChange = other.ScoreChange;
			}
			if (other.ActionName != 0)
			{
				ActionName = other.ActionName;
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
				Id = input.ReadSFixed32();
				break;
			case 16u:
				CreditActionType = (CreditActionType)input.ReadEnum();
				break;
			case 29u:
				ScoreChange = input.ReadSFixed32();
				break;
			case 37u:
				ActionName = input.ReadSFixed32();
				break;
			}
		}
	}
}
