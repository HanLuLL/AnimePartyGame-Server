using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class GambleRole : IMessage<GambleRole>, IMessage, IEquatable<GambleRole>, IDeepCloneable<GambleRole>, IBufferMessage
{
	private static readonly MessageParser<GambleRole> _parser = new MessageParser<GambleRole>(() => new GambleRole());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int HeroIdFieldNumber = 2;

	private int heroId_;

	public const int IsDieFieldNumber = 3;

	private bool isDie_;

	public const int GoldLackFieldNumber = 4;

	private bool goldLack_;

	public const int BetGoldFieldNumber = 5;

	private int betGold_;

	public const int GuessCodeFieldNumber = 6;

	private int guessCode_;

	public const int PointFieldNumber = 7;

	private int point_;

	public const int GoldChangeFieldNumber = 8;

	private int goldChange_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GambleRole> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[94];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public int HeroId
	{
		get
		{
			return heroId_;
		}
		set
		{
			heroId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDie
	{
		get
		{
			return isDie_;
		}
		set
		{
			isDie_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool GoldLack
	{
		get
		{
			return goldLack_;
		}
		set
		{
			goldLack_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BetGold
	{
		get
		{
			return betGold_;
		}
		set
		{
			betGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GuessCode
	{
		get
		{
			return guessCode_;
		}
		set
		{
			guessCode_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Point
	{
		get
		{
			return point_;
		}
		set
		{
			point_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoldChange
	{
		get
		{
			return goldChange_;
		}
		set
		{
			goldChange_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GambleRole()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GambleRole(GambleRole other)
		: this()
	{
		playerId_ = other.playerId_;
		heroId_ = other.heroId_;
		isDie_ = other.isDie_;
		goldLack_ = other.goldLack_;
		betGold_ = other.betGold_;
		guessCode_ = other.guessCode_;
		point_ = other.point_;
		goldChange_ = other.goldChange_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GambleRole Clone()
	{
		return new GambleRole(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GambleRole);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GambleRole other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (IsDie != other.IsDie)
		{
			return false;
		}
		if (GoldLack != other.GoldLack)
		{
			return false;
		}
		if (BetGold != other.BetGold)
		{
			return false;
		}
		if (GuessCode != other.GuessCode)
		{
			return false;
		}
		if (Point != other.Point)
		{
			return false;
		}
		if (GoldChange != other.GoldChange)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (IsDie)
		{
			num ^= IsDie.GetHashCode();
		}
		if (GoldLack)
		{
			num ^= GoldLack.GetHashCode();
		}
		if (BetGold != 0)
		{
			num ^= BetGold.GetHashCode();
		}
		if (GuessCode != 0)
		{
			num ^= GuessCode.GetHashCode();
		}
		if (Point != 0)
		{
			num ^= Point.GetHashCode();
		}
		if (GoldChange != 0)
		{
			num ^= GoldChange.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (HeroId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(HeroId);
		}
		if (IsDie)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsDie);
		}
		if (GoldLack)
		{
			output.WriteRawTag(32);
			output.WriteBool(GoldLack);
		}
		if (BetGold != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(BetGold);
		}
		if (GuessCode != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(GuessCode);
		}
		if (Point != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Point);
		}
		if (GoldChange != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(GoldChange);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (HeroId != 0)
		{
			num += 5;
		}
		if (IsDie)
		{
			num += 2;
		}
		if (GoldLack)
		{
			num += 2;
		}
		if (BetGold != 0)
		{
			num += 5;
		}
		if (GuessCode != 0)
		{
			num += 5;
		}
		if (Point != 0)
		{
			num += 5;
		}
		if (GoldChange != 0)
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
	public void MergeFrom(GambleRole other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
			}
			if (other.IsDie)
			{
				IsDie = other.IsDie;
			}
			if (other.GoldLack)
			{
				GoldLack = other.GoldLack;
			}
			if (other.BetGold != 0)
			{
				BetGold = other.BetGold;
			}
			if (other.GuessCode != 0)
			{
				GuessCode = other.GuessCode;
			}
			if (other.Point != 0)
			{
				Point = other.Point;
			}
			if (other.GoldChange != 0)
			{
				GoldChange = other.GoldChange;
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
				PlayerId = input.ReadSFixed64();
				break;
			case 21u:
				HeroId = input.ReadSFixed32();
				break;
			case 24u:
				IsDie = input.ReadBool();
				break;
			case 32u:
				GoldLack = input.ReadBool();
				break;
			case 45u:
				BetGold = input.ReadSFixed32();
				break;
			case 53u:
				GuessCode = input.ReadSFixed32();
				break;
			case 61u:
				Point = input.ReadSFixed32();
				break;
			case 69u:
				GoldChange = input.ReadSFixed32();
				break;
			}
		}
	}
}
