using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SyncPlayerCreditInfoS2C : IMessage<SyncPlayerCreditInfoS2C>, IMessage, IEquatable<SyncPlayerCreditInfoS2C>, IDeepCloneable<SyncPlayerCreditInfoS2C>, IBufferMessage
{
	private static readonly MessageParser<SyncPlayerCreditInfoS2C> _parser = new MessageParser<SyncPlayerCreditInfoS2C>(() => new SyncPlayerCreditInfoS2C());

	private UnknownFieldSet _unknownFields;

	public const int DeductScoreFieldNumber = 1;

	private int deductScore_;

	public const int LastExemptedTimeFieldNumber = 2;

	private long lastExemptedTime_;

	public const int LastExemptedActionTypeFieldNumber = 3;

	private int lastExemptedActionType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SyncPlayerCreditInfoS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[510];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DeductScore
	{
		get
		{
			return deductScore_;
		}
		set
		{
			deductScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastExemptedTime
	{
		get
		{
			return lastExemptedTime_;
		}
		set
		{
			lastExemptedTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LastExemptedActionType
	{
		get
		{
			return lastExemptedActionType_;
		}
		set
		{
			lastExemptedActionType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerCreditInfoS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerCreditInfoS2C(SyncPlayerCreditInfoS2C other)
		: this()
	{
		deductScore_ = other.deductScore_;
		lastExemptedTime_ = other.lastExemptedTime_;
		lastExemptedActionType_ = other.lastExemptedActionType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerCreditInfoS2C Clone()
	{
		return new SyncPlayerCreditInfoS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SyncPlayerCreditInfoS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SyncPlayerCreditInfoS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DeductScore != other.DeductScore)
		{
			return false;
		}
		if (LastExemptedTime != other.LastExemptedTime)
		{
			return false;
		}
		if (LastExemptedActionType != other.LastExemptedActionType)
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
		if (DeductScore != 0)
		{
			num ^= DeductScore.GetHashCode();
		}
		if (LastExemptedTime != 0L)
		{
			num ^= LastExemptedTime.GetHashCode();
		}
		if (LastExemptedActionType != 0)
		{
			num ^= LastExemptedActionType.GetHashCode();
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
		if (DeductScore != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(DeductScore);
		}
		if (LastExemptedTime != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(LastExemptedTime);
		}
		if (LastExemptedActionType != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(LastExemptedActionType);
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
		if (DeductScore != 0)
		{
			num += 5;
		}
		if (LastExemptedTime != 0L)
		{
			num += 9;
		}
		if (LastExemptedActionType != 0)
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
	public void MergeFrom(SyncPlayerCreditInfoS2C other)
	{
		if (other != null)
		{
			if (other.DeductScore != 0)
			{
				DeductScore = other.DeductScore;
			}
			if (other.LastExemptedTime != 0L)
			{
				LastExemptedTime = other.LastExemptedTime;
			}
			if (other.LastExemptedActionType != 0)
			{
				LastExemptedActionType = other.LastExemptedActionType;
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
				DeductScore = input.ReadSFixed32();
				break;
			case 17u:
				LastExemptedTime = input.ReadSFixed64();
				break;
			case 29u:
				LastExemptedActionType = input.ReadSFixed32();
				break;
			}
		}
	}
}
