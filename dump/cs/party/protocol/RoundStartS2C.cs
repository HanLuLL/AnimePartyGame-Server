using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class RoundStartS2C : IMessage<RoundStartS2C>, IMessage, IEquatable<RoundStartS2C>, IDeepCloneable<RoundStartS2C>, IBufferMessage
{
	private static readonly MessageParser<RoundStartS2C> _parser = new MessageParser<RoundStartS2C>(() => new RoundStartS2C());

	private UnknownFieldSet _unknownFields;

	public const int RoundFieldNumber = 1;

	private int round_;

	public const int SkillCdsFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_skillCds_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> skillCds_ = new MapField<int, int>();

	public const int GoldFieldNumber = 3;

	private int gold_;

	public const int CardNumFieldNumber = 4;

	private int cardNum_;

	public const int PlayerIdFieldNumber = 5;

	private long playerId_;

	public const int UseCardMaxNumFieldNumber = 6;

	private int useCardMaxNum_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RoundStartS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[383];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Round
	{
		get
		{
			return round_;
		}
		set
		{
			round_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> SkillCds => skillCds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Gold
	{
		get
		{
			return gold_;
		}
		set
		{
			gold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardNum
	{
		get
		{
			return cardNum_;
		}
		set
		{
			cardNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseCardMaxNum
	{
		get
		{
			return useCardMaxNum_;
		}
		set
		{
			useCardMaxNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoundStartS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoundStartS2C(RoundStartS2C other)
		: this()
	{
		round_ = other.round_;
		skillCds_ = other.skillCds_.Clone();
		gold_ = other.gold_;
		cardNum_ = other.cardNum_;
		playerId_ = other.playerId_;
		useCardMaxNum_ = other.useCardMaxNum_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoundStartS2C Clone()
	{
		return new RoundStartS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RoundStartS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RoundStartS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Round != other.Round)
		{
			return false;
		}
		if (!SkillCds.Equals(other.SkillCds))
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (CardNum != other.CardNum)
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (UseCardMaxNum != other.UseCardMaxNum)
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
		if (Round != 0)
		{
			num ^= Round.GetHashCode();
		}
		num ^= SkillCds.GetHashCode();
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		if (CardNum != 0)
		{
			num ^= CardNum.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (UseCardMaxNum != 0)
		{
			num ^= UseCardMaxNum.GetHashCode();
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
		if (Round != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Round);
		}
		skillCds_.WriteTo(ref output, _map_skillCds_codec);
		if (Gold != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Gold);
		}
		if (CardNum != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CardNum);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(PlayerId);
		}
		if (UseCardMaxNum != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(UseCardMaxNum);
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
		if (Round != 0)
		{
			num += 5;
		}
		num += skillCds_.CalculateSize(_map_skillCds_codec);
		if (Gold != 0)
		{
			num += 5;
		}
		if (CardNum != 0)
		{
			num += 5;
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (UseCardMaxNum != 0)
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
	public void MergeFrom(RoundStartS2C other)
	{
		if (other != null)
		{
			if (other.Round != 0)
			{
				Round = other.Round;
			}
			skillCds_.MergeFrom(other.skillCds_);
			if (other.Gold != 0)
			{
				Gold = other.Gold;
			}
			if (other.CardNum != 0)
			{
				CardNum = other.CardNum;
			}
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.UseCardMaxNum != 0)
			{
				UseCardMaxNum = other.UseCardMaxNum;
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
				Round = input.ReadSFixed32();
				break;
			case 18u:
				skillCds_.AddEntriesFrom(ref input, _map_skillCds_codec);
				break;
			case 29u:
				Gold = input.ReadSFixed32();
				break;
			case 37u:
				CardNum = input.ReadSFixed32();
				break;
			case 41u:
				PlayerId = input.ReadSFixed64();
				break;
			case 53u:
				UseCardMaxNum = input.ReadSFixed32();
				break;
			}
		}
	}
}
