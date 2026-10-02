using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class PVEShopBuyC2S : IMessage<PVEShopBuyC2S>, IMessage, IEquatable<PVEShopBuyC2S>, IDeepCloneable<PVEShopBuyC2S>, IBufferMessage
{
	private static readonly MessageParser<PVEShopBuyC2S> _parser = new MessageParser<PVEShopBuyC2S>(() => new PVEShopBuyC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int CardsFieldNumber = 2;

	private static readonly FieldCodec<int> _repeated_cards_codec = FieldCodec.ForSFixed32(18u);

	private readonly RepeatedField<int> cards_ = new RepeatedField<int>();

	public const int FreeCardFieldNumber = 3;

	private int freeCard_;

	public const int GoldFieldNumber = 4;

	private int gold_;

	public const int AssistGoldFieldNumber = 5;

	private int assistGold_;

	public const int BuyCardsFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_buyCards_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> buyCards_ = new RepeatedField<int>();

	public const int AssistPlayerFieldNumber = 7;

	private long assistPlayer_;

	public const int FreeCardNumFieldNumber = 8;

	private int freeCardNum_;

	public const int IsRemoteFieldNumber = 9;

	private bool isRemote_;

	public const int AlreadysFieldNumber = 10;

	private static readonly FieldCodec<bool> _repeated_alreadys_codec = FieldCodec.ForBool(82u);

	private readonly RepeatedField<bool> alreadys_ = new RepeatedField<bool>();

	public const int IsCloseFieldNumber = 11;

	private bool isClose_;

	public const int IsBotFieldNumber = 12;

	private bool isBot_;

	public const int TalentSkillFreeCardFieldNumber = 13;

	private static readonly FieldCodec<bool> _repeated_talentSkillFreeCard_codec = FieldCodec.ForBool(106u);

	private readonly RepeatedField<bool> talentSkillFreeCard_ = new RepeatedField<bool>();

	public const int DisCountGoldFieldNumber = 14;

	private int disCountGold_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PVEShopBuyC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[255];

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
	public RepeatedField<int> Cards => cards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FreeCard
	{
		get
		{
			return freeCard_;
		}
		set
		{
			freeCard_ = value;
		}
	}

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
	public int AssistGold
	{
		get
		{
			return assistGold_;
		}
		set
		{
			assistGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuyCards => buyCards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long AssistPlayer
	{
		get
		{
			return assistPlayer_;
		}
		set
		{
			assistPlayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FreeCardNum
	{
		get
		{
			return freeCardNum_;
		}
		set
		{
			freeCardNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsRemote
	{
		get
		{
			return isRemote_;
		}
		set
		{
			isRemote_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<bool> Alreadys => alreadys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsClose
	{
		get
		{
			return isClose_;
		}
		set
		{
			isClose_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsBot
	{
		get
		{
			return isBot_;
		}
		set
		{
			isBot_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<bool> TalentSkillFreeCard => talentSkillFreeCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DisCountGold
	{
		get
		{
			return disCountGold_;
		}
		set
		{
			disCountGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEShopBuyC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEShopBuyC2S(PVEShopBuyC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		cards_ = other.cards_.Clone();
		freeCard_ = other.freeCard_;
		gold_ = other.gold_;
		assistGold_ = other.assistGold_;
		buyCards_ = other.buyCards_.Clone();
		assistPlayer_ = other.assistPlayer_;
		freeCardNum_ = other.freeCardNum_;
		isRemote_ = other.isRemote_;
		alreadys_ = other.alreadys_.Clone();
		isClose_ = other.isClose_;
		isBot_ = other.isBot_;
		talentSkillFreeCard_ = other.talentSkillFreeCard_.Clone();
		disCountGold_ = other.disCountGold_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEShopBuyC2S Clone()
	{
		return new PVEShopBuyC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PVEShopBuyC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PVEShopBuyC2S other)
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
		if (!cards_.Equals(other.cards_))
		{
			return false;
		}
		if (FreeCard != other.FreeCard)
		{
			return false;
		}
		if (Gold != other.Gold)
		{
			return false;
		}
		if (AssistGold != other.AssistGold)
		{
			return false;
		}
		if (!buyCards_.Equals(other.buyCards_))
		{
			return false;
		}
		if (AssistPlayer != other.AssistPlayer)
		{
			return false;
		}
		if (FreeCardNum != other.FreeCardNum)
		{
			return false;
		}
		if (IsRemote != other.IsRemote)
		{
			return false;
		}
		if (!alreadys_.Equals(other.alreadys_))
		{
			return false;
		}
		if (IsClose != other.IsClose)
		{
			return false;
		}
		if (IsBot != other.IsBot)
		{
			return false;
		}
		if (!talentSkillFreeCard_.Equals(other.talentSkillFreeCard_))
		{
			return false;
		}
		if (DisCountGold != other.DisCountGold)
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
		num ^= cards_.GetHashCode();
		if (FreeCard != 0)
		{
			num ^= FreeCard.GetHashCode();
		}
		if (Gold != 0)
		{
			num ^= Gold.GetHashCode();
		}
		if (AssistGold != 0)
		{
			num ^= AssistGold.GetHashCode();
		}
		num ^= buyCards_.GetHashCode();
		if (AssistPlayer != 0L)
		{
			num ^= AssistPlayer.GetHashCode();
		}
		if (FreeCardNum != 0)
		{
			num ^= FreeCardNum.GetHashCode();
		}
		if (IsRemote)
		{
			num ^= IsRemote.GetHashCode();
		}
		num ^= alreadys_.GetHashCode();
		if (IsClose)
		{
			num ^= IsClose.GetHashCode();
		}
		if (IsBot)
		{
			num ^= IsBot.GetHashCode();
		}
		num ^= talentSkillFreeCard_.GetHashCode();
		if (DisCountGold != 0)
		{
			num ^= DisCountGold.GetHashCode();
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
		cards_.WriteTo(ref output, _repeated_cards_codec);
		if (FreeCard != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(FreeCard);
		}
		if (Gold != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Gold);
		}
		if (AssistGold != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(AssistGold);
		}
		buyCards_.WriteTo(ref output, _repeated_buyCards_codec);
		if (AssistPlayer != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(AssistPlayer);
		}
		if (FreeCardNum != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(FreeCardNum);
		}
		if (IsRemote)
		{
			output.WriteRawTag(72);
			output.WriteBool(IsRemote);
		}
		alreadys_.WriteTo(ref output, _repeated_alreadys_codec);
		if (IsClose)
		{
			output.WriteRawTag(88);
			output.WriteBool(IsClose);
		}
		if (IsBot)
		{
			output.WriteRawTag(96);
			output.WriteBool(IsBot);
		}
		talentSkillFreeCard_.WriteTo(ref output, _repeated_talentSkillFreeCard_codec);
		if (DisCountGold != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(DisCountGold);
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
		num += cards_.CalculateSize(_repeated_cards_codec);
		if (FreeCard != 0)
		{
			num += 5;
		}
		if (Gold != 0)
		{
			num += 5;
		}
		if (AssistGold != 0)
		{
			num += 5;
		}
		num += buyCards_.CalculateSize(_repeated_buyCards_codec);
		if (AssistPlayer != 0L)
		{
			num += 9;
		}
		if (FreeCardNum != 0)
		{
			num += 5;
		}
		if (IsRemote)
		{
			num += 2;
		}
		num += alreadys_.CalculateSize(_repeated_alreadys_codec);
		if (IsClose)
		{
			num += 2;
		}
		if (IsBot)
		{
			num += 2;
		}
		num += talentSkillFreeCard_.CalculateSize(_repeated_talentSkillFreeCard_codec);
		if (DisCountGold != 0)
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
	public void MergeFrom(PVEShopBuyC2S other)
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
		cards_.Add(other.cards_);
		if (other.FreeCard != 0)
		{
			FreeCard = other.FreeCard;
		}
		if (other.Gold != 0)
		{
			Gold = other.Gold;
		}
		if (other.AssistGold != 0)
		{
			AssistGold = other.AssistGold;
		}
		buyCards_.Add(other.buyCards_);
		if (other.AssistPlayer != 0L)
		{
			AssistPlayer = other.AssistPlayer;
		}
		if (other.FreeCardNum != 0)
		{
			FreeCardNum = other.FreeCardNum;
		}
		if (other.IsRemote)
		{
			IsRemote = other.IsRemote;
		}
		alreadys_.Add(other.alreadys_);
		if (other.IsClose)
		{
			IsClose = other.IsClose;
		}
		if (other.IsBot)
		{
			IsBot = other.IsBot;
		}
		talentSkillFreeCard_.Add(other.talentSkillFreeCard_);
		if (other.DisCountGold != 0)
		{
			DisCountGold = other.DisCountGold;
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
			case 18u:
			case 21u:
				cards_.AddEntriesFrom(ref input, _repeated_cards_codec);
				break;
			case 29u:
				FreeCard = input.ReadSFixed32();
				break;
			case 37u:
				Gold = input.ReadSFixed32();
				break;
			case 45u:
				AssistGold = input.ReadSFixed32();
				break;
			case 50u:
			case 53u:
				buyCards_.AddEntriesFrom(ref input, _repeated_buyCards_codec);
				break;
			case 57u:
				AssistPlayer = input.ReadSFixed64();
				break;
			case 69u:
				FreeCardNum = input.ReadSFixed32();
				break;
			case 72u:
				IsRemote = input.ReadBool();
				break;
			case 80u:
			case 82u:
				alreadys_.AddEntriesFrom(ref input, _repeated_alreadys_codec);
				break;
			case 88u:
				IsClose = input.ReadBool();
				break;
			case 96u:
				IsBot = input.ReadBool();
				break;
			case 104u:
			case 106u:
				talentSkillFreeCard_.AddEntriesFrom(ref input, _repeated_talentSkillFreeCard_codec);
				break;
			case 117u:
				DisCountGold = input.ReadSFixed32();
				break;
			}
		}
	}
}
