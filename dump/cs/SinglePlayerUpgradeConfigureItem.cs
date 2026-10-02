using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerUpgradeConfigureItem : IMessage<SinglePlayerUpgradeConfigureItem>, IMessage, IEquatable<SinglePlayerUpgradeConfigureItem>, IDeepCloneable<SinglePlayerUpgradeConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerUpgradeConfigureItem> _parser = new MessageParser<SinglePlayerUpgradeConfigureItem>(() => new SinglePlayerUpgradeConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int StarFieldNumber = 1;

	private int star_;

	public const int GoldcostFieldNumber = 2;

	private int goldcost_;

	public const int CardWeightsFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_cardWeights_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> cardWeights_ = new RepeatedField<int>();

	public const int CharacterMaxHpFieldNumber = 4;

	private int characterMaxHp_;

	public const int CharacterDefFieldNumber = 5;

	private int characterDef_;

	public const int UpgradeCardFieldNumber = 6;

	private int upgradeCard_;

	public const int CharacterAtkFieldNumber = 7;

	private int characterAtk_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerUpgradeConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Star
	{
		get
		{
			return star_;
		}
		private set
		{
			star_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Goldcost
	{
		get
		{
			return goldcost_;
		}
		private set
		{
			goldcost_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CardWeights => cardWeights_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CharacterMaxHp
	{
		get
		{
			return characterMaxHp_;
		}
		private set
		{
			characterMaxHp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CharacterDef
	{
		get
		{
			return characterDef_;
		}
		private set
		{
			characterDef_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UpgradeCard
	{
		get
		{
			return upgradeCard_;
		}
		private set
		{
			upgradeCard_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CharacterAtk
	{
		get
		{
			return characterAtk_;
		}
		private set
		{
			characterAtk_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerUpgradeConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerUpgradeConfigureItem(SinglePlayerUpgradeConfigureItem other)
		: this()
	{
		star_ = other.star_;
		goldcost_ = other.goldcost_;
		cardWeights_ = other.cardWeights_.Clone();
		characterMaxHp_ = other.characterMaxHp_;
		characterDef_ = other.characterDef_;
		upgradeCard_ = other.upgradeCard_;
		characterAtk_ = other.characterAtk_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerUpgradeConfigureItem Clone()
	{
		return new SinglePlayerUpgradeConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerUpgradeConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerUpgradeConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Star != other.Star)
		{
			return false;
		}
		if (Goldcost != other.Goldcost)
		{
			return false;
		}
		if (!cardWeights_.Equals(other.cardWeights_))
		{
			return false;
		}
		if (CharacterMaxHp != other.CharacterMaxHp)
		{
			return false;
		}
		if (CharacterDef != other.CharacterDef)
		{
			return false;
		}
		if (UpgradeCard != other.UpgradeCard)
		{
			return false;
		}
		if (CharacterAtk != other.CharacterAtk)
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
		if (Star != 0)
		{
			num ^= Star.GetHashCode();
		}
		if (Goldcost != 0)
		{
			num ^= Goldcost.GetHashCode();
		}
		num ^= cardWeights_.GetHashCode();
		if (CharacterMaxHp != 0)
		{
			num ^= CharacterMaxHp.GetHashCode();
		}
		if (CharacterDef != 0)
		{
			num ^= CharacterDef.GetHashCode();
		}
		if (UpgradeCard != 0)
		{
			num ^= UpgradeCard.GetHashCode();
		}
		if (CharacterAtk != 0)
		{
			num ^= CharacterAtk.GetHashCode();
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
		if (Star != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Star);
		}
		if (Goldcost != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Goldcost);
		}
		cardWeights_.WriteTo(ref output, _repeated_cardWeights_codec);
		if (CharacterMaxHp != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CharacterMaxHp);
		}
		if (CharacterDef != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CharacterDef);
		}
		if (UpgradeCard != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(UpgradeCard);
		}
		if (CharacterAtk != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(CharacterAtk);
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
		if (Star != 0)
		{
			num += 5;
		}
		if (Goldcost != 0)
		{
			num += 5;
		}
		num += cardWeights_.CalculateSize(_repeated_cardWeights_codec);
		if (CharacterMaxHp != 0)
		{
			num += 5;
		}
		if (CharacterDef != 0)
		{
			num += 5;
		}
		if (UpgradeCard != 0)
		{
			num += 5;
		}
		if (CharacterAtk != 0)
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
	public void MergeFrom(SinglePlayerUpgradeConfigureItem other)
	{
		if (other != null)
		{
			if (other.Star != 0)
			{
				Star = other.Star;
			}
			if (other.Goldcost != 0)
			{
				Goldcost = other.Goldcost;
			}
			cardWeights_.Add(other.cardWeights_);
			if (other.CharacterMaxHp != 0)
			{
				CharacterMaxHp = other.CharacterMaxHp;
			}
			if (other.CharacterDef != 0)
			{
				CharacterDef = other.CharacterDef;
			}
			if (other.UpgradeCard != 0)
			{
				UpgradeCard = other.UpgradeCard;
			}
			if (other.CharacterAtk != 0)
			{
				CharacterAtk = other.CharacterAtk;
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
				Star = input.ReadSFixed32();
				break;
			case 21u:
				Goldcost = input.ReadSFixed32();
				break;
			case 26u:
			case 29u:
				cardWeights_.AddEntriesFrom(ref input, _repeated_cardWeights_codec);
				break;
			case 37u:
				CharacterMaxHp = input.ReadSFixed32();
				break;
			case 45u:
				CharacterDef = input.ReadSFixed32();
				break;
			case 53u:
				UpgradeCard = input.ReadSFixed32();
				break;
			case 61u:
				CharacterAtk = input.ReadSFixed32();
				break;
			}
		}
	}
}
