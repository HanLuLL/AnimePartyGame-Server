using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class ReturnSignIn : IMessage<ReturnSignIn>, IMessage, IEquatable<ReturnSignIn>, IDeepCloneable<ReturnSignIn>, IBufferMessage
{
	private static readonly MessageParser<ReturnSignIn> _parser = new MessageParser<ReturnSignIn>(() => new ReturnSignIn());

	private UnknownFieldSet _unknownFields;

	public const int UnlockDayFieldNumber = 1;

	private int unlockDay_;

	public const int LastUnlockTimeFieldNumber = 2;

	private long lastUnlockTime_;

	public const int FreeClaimedDaysFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_freeClaimedDays_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> freeClaimedDays_ = new RepeatedField<int>();

	public const int AdvClaimedDaysFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_advClaimedDays_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> advClaimedDays_ = new RepeatedField<int>();

	public const int AdvUnlockedFieldNumber = 5;

	private bool advUnlocked_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ReturnSignIn> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[3];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UnlockDay
	{
		get
		{
			return unlockDay_;
		}
		set
		{
			unlockDay_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long LastUnlockTime
	{
		get
		{
			return lastUnlockTime_;
		}
		set
		{
			lastUnlockTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> FreeClaimedDays => freeClaimedDays_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> AdvClaimedDays => advClaimedDays_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool AdvUnlocked
	{
		get
		{
			return advUnlocked_;
		}
		set
		{
			advUnlocked_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignIn()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignIn(ReturnSignIn other)
		: this()
	{
		unlockDay_ = other.unlockDay_;
		lastUnlockTime_ = other.lastUnlockTime_;
		freeClaimedDays_ = other.freeClaimedDays_.Clone();
		advClaimedDays_ = other.advClaimedDays_.Clone();
		advUnlocked_ = other.advUnlocked_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignIn Clone()
	{
		return new ReturnSignIn(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ReturnSignIn);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ReturnSignIn other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (UnlockDay != other.UnlockDay)
		{
			return false;
		}
		if (LastUnlockTime != other.LastUnlockTime)
		{
			return false;
		}
		if (!freeClaimedDays_.Equals(other.freeClaimedDays_))
		{
			return false;
		}
		if (!advClaimedDays_.Equals(other.advClaimedDays_))
		{
			return false;
		}
		if (AdvUnlocked != other.AdvUnlocked)
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
		if (UnlockDay != 0)
		{
			num ^= UnlockDay.GetHashCode();
		}
		if (LastUnlockTime != 0L)
		{
			num ^= LastUnlockTime.GetHashCode();
		}
		num ^= freeClaimedDays_.GetHashCode();
		num ^= advClaimedDays_.GetHashCode();
		if (AdvUnlocked)
		{
			num ^= AdvUnlocked.GetHashCode();
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
		if (UnlockDay != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(UnlockDay);
		}
		if (LastUnlockTime != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(LastUnlockTime);
		}
		freeClaimedDays_.WriteTo(ref output, _repeated_freeClaimedDays_codec);
		advClaimedDays_.WriteTo(ref output, _repeated_advClaimedDays_codec);
		if (AdvUnlocked)
		{
			output.WriteRawTag(40);
			output.WriteBool(AdvUnlocked);
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
		if (UnlockDay != 0)
		{
			num += 5;
		}
		if (LastUnlockTime != 0L)
		{
			num += 9;
		}
		num += freeClaimedDays_.CalculateSize(_repeated_freeClaimedDays_codec);
		num += advClaimedDays_.CalculateSize(_repeated_advClaimedDays_codec);
		if (AdvUnlocked)
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
	public void MergeFrom(ReturnSignIn other)
	{
		if (other != null)
		{
			if (other.UnlockDay != 0)
			{
				UnlockDay = other.UnlockDay;
			}
			if (other.LastUnlockTime != 0L)
			{
				LastUnlockTime = other.LastUnlockTime;
			}
			freeClaimedDays_.Add(other.freeClaimedDays_);
			advClaimedDays_.Add(other.advClaimedDays_);
			if (other.AdvUnlocked)
			{
				AdvUnlocked = other.AdvUnlocked;
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
				UnlockDay = input.ReadSFixed32();
				break;
			case 17u:
				LastUnlockTime = input.ReadSFixed64();
				break;
			case 26u:
			case 29u:
				freeClaimedDays_.AddEntriesFrom(ref input, _repeated_freeClaimedDays_codec);
				break;
			case 34u:
			case 37u:
				advClaimedDays_.AddEntriesFrom(ref input, _repeated_advClaimedDays_codec);
				break;
			case 40u:
				AdvUnlocked = input.ReadBool();
				break;
			}
		}
	}
}
