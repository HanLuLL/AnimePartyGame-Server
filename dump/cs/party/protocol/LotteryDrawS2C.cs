using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class LotteryDrawS2C : IMessage<LotteryDrawS2C>, IMessage, IEquatable<LotteryDrawS2C>, IDeepCloneable<LotteryDrawS2C>, IBufferMessage
{
	private static readonly MessageParser<LotteryDrawS2C> _parser = new MessageParser<LotteryDrawS2C>(() => new LotteryDrawS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdsFieldNumber = 1;

	private static readonly FieldCodec<long> _repeated_playerIds_codec = FieldCodec.ForSFixed64(10u);

	private readonly RepeatedField<long> playerIds_ = new RepeatedField<long>();

	public const int ValFieldNumber = 2;

	private int val_;

	public const int AwardGoldFieldNumber = 3;

	private int awardGold_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LotteryDrawS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[378];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> PlayerIds => playerIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Val
	{
		get
		{
			return val_;
		}
		set
		{
			val_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AwardGold
	{
		get
		{
			return awardGold_;
		}
		set
		{
			awardGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LotteryDrawS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LotteryDrawS2C(LotteryDrawS2C other)
		: this()
	{
		playerIds_ = other.playerIds_.Clone();
		val_ = other.val_;
		awardGold_ = other.awardGold_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LotteryDrawS2C Clone()
	{
		return new LotteryDrawS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LotteryDrawS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LotteryDrawS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!playerIds_.Equals(other.playerIds_))
		{
			return false;
		}
		if (Val != other.Val)
		{
			return false;
		}
		if (AwardGold != other.AwardGold)
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
		num ^= playerIds_.GetHashCode();
		if (Val != 0)
		{
			num ^= Val.GetHashCode();
		}
		if (AwardGold != 0)
		{
			num ^= AwardGold.GetHashCode();
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
		playerIds_.WriteTo(ref output, _repeated_playerIds_codec);
		if (Val != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Val);
		}
		if (AwardGold != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(AwardGold);
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
		num += playerIds_.CalculateSize(_repeated_playerIds_codec);
		if (Val != 0)
		{
			num += 5;
		}
		if (AwardGold != 0)
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
	public void MergeFrom(LotteryDrawS2C other)
	{
		if (other != null)
		{
			playerIds_.Add(other.playerIds_);
			if (other.Val != 0)
			{
				Val = other.Val;
			}
			if (other.AwardGold != 0)
			{
				AwardGold = other.AwardGold;
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
			case 9u:
			case 10u:
				playerIds_.AddEntriesFrom(ref input, _repeated_playerIds_codec);
				break;
			case 21u:
				Val = input.ReadSFixed32();
				break;
			case 29u:
				AwardGold = input.ReadSFixed32();
				break;
			}
		}
	}
}
