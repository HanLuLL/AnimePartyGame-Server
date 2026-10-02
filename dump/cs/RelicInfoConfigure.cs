using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class RelicInfoConfigure : IMessage<RelicInfoConfigure>, IMessage, IEquatable<RelicInfoConfigure>, IDeepCloneable<RelicInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<RelicInfoConfigure> _parser = new MessageParser<RelicInfoConfigure>(() => new RelicInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int RelicQualityTypeFieldNumber = 2;

	private RelicQualityType relicQualityType_;

	public const int TagTypeFieldNumber = 3;

	private static readonly FieldCodec<CharacterTagType> _repeated_tagType_codec = FieldCodec.ForEnum(26u, (CharacterTagType x) => (int)x, (int x) => (CharacterTagType)x);

	private readonly RepeatedField<CharacterTagType> tagType_ = new RepeatedField<CharacterTagType>();

	public const int KeyWordTypeFieldNumber = 4;

	private RelicKeyWordType keyWordType_;

	public const int MapLimitFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_mapLimit_codec = FieldCodec.ForSInt32(42u);

	private readonly RepeatedField<int> mapLimit_ = new RepeatedField<int>();

	public const int IconFieldNumber = 6;

	private string icon_ = "";

	public const int NameIDFieldNumber = 7;

	private int nameID_;

	public const int DescIDFieldNumber = 8;

	private int descID_;

	public const int ParamsFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(74u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	public const int BuffIdFieldNumber = 10;

	private int buffId_;

	public const int PerformTargetFieldNumber = 11;

	private int performTarget_;

	public const int PerformSelfFieldNumber = 12;

	private int performSelf_;

	public const int OrderWeightFieldNumber = 13;

	private int orderWeight_;

	public const int AttackPtFieldNumber = 14;

	private int attackPt_;

	public const int CardPtFieldNumber = 15;

	private int cardPt_;

	public const int SupportPtFieldNumber = 16;

	private int supportPt_;

	public const int TankPtFieldNumber = 17;

	private int tankPt_;

	public const int RelicRecBaseScoreFieldNumber = 18;

	private int relicRecBaseScore_;

	public const int RelicRecGrowthFieldNumber = 19;

	private int relicRecGrowth_;

	public const int RelicRecRoleNormalFieldNumber = 20;

	private static readonly FieldCodec<int> _repeated_relicRecRoleNormal_codec = FieldCodec.ForSInt32(162u);

	private readonly RepeatedField<int> relicRecRoleNormal_ = new RepeatedField<int>();

	public const int RelicRecRoleFieldNumber = 21;

	private static readonly FieldCodec<int> _repeated_relicRecRole_codec = FieldCodec.ForSInt32(170u);

	private readonly RepeatedField<int> relicRecRole_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RelicInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RelicReflection.Descriptor.MessageTypes[0];

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
	public RelicQualityType RelicQualityType
	{
		get
		{
			return relicQualityType_;
		}
		private set
		{
			relicQualityType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<CharacterTagType> TagType => tagType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicKeyWordType KeyWordType
	{
		get
		{
			return keyWordType_;
		}
		private set
		{
			keyWordType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MapLimit => mapLimit_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Icon
	{
		get
		{
			return icon_;
		}
		private set
		{
			icon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescID
	{
		get
		{
			return descID_;
		}
		private set
		{
			descID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuffId
	{
		get
		{
			return buffId_;
		}
		private set
		{
			buffId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PerformTarget
	{
		get
		{
			return performTarget_;
		}
		private set
		{
			performTarget_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PerformSelf
	{
		get
		{
			return performSelf_;
		}
		private set
		{
			performSelf_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OrderWeight
	{
		get
		{
			return orderWeight_;
		}
		private set
		{
			orderWeight_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AttackPt
	{
		get
		{
			return attackPt_;
		}
		private set
		{
			attackPt_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardPt
	{
		get
		{
			return cardPt_;
		}
		private set
		{
			cardPt_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SupportPt
	{
		get
		{
			return supportPt_;
		}
		private set
		{
			supportPt_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TankPt
	{
		get
		{
			return tankPt_;
		}
		private set
		{
			tankPt_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecBaseScore
	{
		get
		{
			return relicRecBaseScore_;
		}
		private set
		{
			relicRecBaseScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RelicRecGrowth
	{
		get
		{
			return relicRecGrowth_;
		}
		private set
		{
			relicRecGrowth_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RelicRecRoleNormal => relicRecRoleNormal_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RelicRecRole => relicRecRole_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicInfoConfigure(RelicInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		relicQualityType_ = other.relicQualityType_;
		tagType_ = other.tagType_.Clone();
		keyWordType_ = other.keyWordType_;
		mapLimit_ = other.mapLimit_.Clone();
		icon_ = other.icon_;
		nameID_ = other.nameID_;
		descID_ = other.descID_;
		params_ = other.params_.Clone();
		buffId_ = other.buffId_;
		performTarget_ = other.performTarget_;
		performSelf_ = other.performSelf_;
		orderWeight_ = other.orderWeight_;
		attackPt_ = other.attackPt_;
		cardPt_ = other.cardPt_;
		supportPt_ = other.supportPt_;
		tankPt_ = other.tankPt_;
		relicRecBaseScore_ = other.relicRecBaseScore_;
		relicRecGrowth_ = other.relicRecGrowth_;
		relicRecRoleNormal_ = other.relicRecRoleNormal_.Clone();
		relicRecRole_ = other.relicRecRole_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicInfoConfigure Clone()
	{
		return new RelicInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RelicInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RelicInfoConfigure other)
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
		if (RelicQualityType != other.RelicQualityType)
		{
			return false;
		}
		if (!tagType_.Equals(other.tagType_))
		{
			return false;
		}
		if (KeyWordType != other.KeyWordType)
		{
			return false;
		}
		if (!mapLimit_.Equals(other.mapLimit_))
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (DescID != other.DescID)
		{
			return false;
		}
		if (!params_.Equals(other.params_))
		{
			return false;
		}
		if (BuffId != other.BuffId)
		{
			return false;
		}
		if (PerformTarget != other.PerformTarget)
		{
			return false;
		}
		if (PerformSelf != other.PerformSelf)
		{
			return false;
		}
		if (OrderWeight != other.OrderWeight)
		{
			return false;
		}
		if (AttackPt != other.AttackPt)
		{
			return false;
		}
		if (CardPt != other.CardPt)
		{
			return false;
		}
		if (SupportPt != other.SupportPt)
		{
			return false;
		}
		if (TankPt != other.TankPt)
		{
			return false;
		}
		if (RelicRecBaseScore != other.RelicRecBaseScore)
		{
			return false;
		}
		if (RelicRecGrowth != other.RelicRecGrowth)
		{
			return false;
		}
		if (!relicRecRoleNormal_.Equals(other.relicRecRoleNormal_))
		{
			return false;
		}
		if (!relicRecRole_.Equals(other.relicRecRole_))
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
		if (RelicQualityType != RelicQualityType.None)
		{
			num ^= RelicQualityType.GetHashCode();
		}
		num ^= tagType_.GetHashCode();
		if (KeyWordType != RelicKeyWordType.None)
		{
			num ^= KeyWordType.GetHashCode();
		}
		num ^= mapLimit_.GetHashCode();
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (DescID != 0)
		{
			num ^= DescID.GetHashCode();
		}
		num ^= params_.GetHashCode();
		if (BuffId != 0)
		{
			num ^= BuffId.GetHashCode();
		}
		if (PerformTarget != 0)
		{
			num ^= PerformTarget.GetHashCode();
		}
		if (PerformSelf != 0)
		{
			num ^= PerformSelf.GetHashCode();
		}
		if (OrderWeight != 0)
		{
			num ^= OrderWeight.GetHashCode();
		}
		if (AttackPt != 0)
		{
			num ^= AttackPt.GetHashCode();
		}
		if (CardPt != 0)
		{
			num ^= CardPt.GetHashCode();
		}
		if (SupportPt != 0)
		{
			num ^= SupportPt.GetHashCode();
		}
		if (TankPt != 0)
		{
			num ^= TankPt.GetHashCode();
		}
		if (RelicRecBaseScore != 0)
		{
			num ^= RelicRecBaseScore.GetHashCode();
		}
		if (RelicRecGrowth != 0)
		{
			num ^= RelicRecGrowth.GetHashCode();
		}
		num ^= relicRecRoleNormal_.GetHashCode();
		num ^= relicRecRole_.GetHashCode();
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
		if (RelicQualityType != RelicQualityType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)RelicQualityType);
		}
		tagType_.WriteTo(ref output, _repeated_tagType_codec);
		if (KeyWordType != RelicKeyWordType.None)
		{
			output.WriteRawTag(32);
			output.WriteEnum((int)KeyWordType);
		}
		mapLimit_.WriteTo(ref output, _repeated_mapLimit_codec);
		if (Icon.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(Icon);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(NameID);
		}
		if (DescID != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(DescID);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
		if (BuffId != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(BuffId);
		}
		if (PerformTarget != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(PerformTarget);
		}
		if (PerformSelf != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(PerformSelf);
		}
		if (OrderWeight != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(OrderWeight);
		}
		if (AttackPt != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(AttackPt);
		}
		if (CardPt != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(CardPt);
		}
		if (SupportPt != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(SupportPt);
		}
		if (TankPt != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(TankPt);
		}
		if (RelicRecBaseScore != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(RelicRecBaseScore);
		}
		if (RelicRecGrowth != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(RelicRecGrowth);
		}
		relicRecRoleNormal_.WriteTo(ref output, _repeated_relicRecRoleNormal_codec);
		relicRecRole_.WriteTo(ref output, _repeated_relicRecRole_codec);
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
		if (RelicQualityType != RelicQualityType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)RelicQualityType);
		}
		num += tagType_.CalculateSize(_repeated_tagType_codec);
		if (KeyWordType != RelicKeyWordType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)KeyWordType);
		}
		num += mapLimit_.CalculateSize(_repeated_mapLimit_codec);
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (DescID != 0)
		{
			num += 5;
		}
		num += params_.CalculateSize(_repeated_params_codec);
		if (BuffId != 0)
		{
			num += 5;
		}
		if (PerformTarget != 0)
		{
			num += 5;
		}
		if (PerformSelf != 0)
		{
			num += 5;
		}
		if (OrderWeight != 0)
		{
			num += 5;
		}
		if (AttackPt != 0)
		{
			num += 5;
		}
		if (CardPt != 0)
		{
			num += 5;
		}
		if (SupportPt != 0)
		{
			num += 6;
		}
		if (TankPt != 0)
		{
			num += 6;
		}
		if (RelicRecBaseScore != 0)
		{
			num += 6;
		}
		if (RelicRecGrowth != 0)
		{
			num += 6;
		}
		num += relicRecRoleNormal_.CalculateSize(_repeated_relicRecRoleNormal_codec);
		num += relicRecRole_.CalculateSize(_repeated_relicRecRole_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(RelicInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.RelicQualityType != RelicQualityType.None)
			{
				RelicQualityType = other.RelicQualityType;
			}
			tagType_.Add(other.tagType_);
			if (other.KeyWordType != RelicKeyWordType.None)
			{
				KeyWordType = other.KeyWordType;
			}
			mapLimit_.Add(other.mapLimit_);
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.DescID != 0)
			{
				DescID = other.DescID;
			}
			params_.Add(other.params_);
			if (other.BuffId != 0)
			{
				BuffId = other.BuffId;
			}
			if (other.PerformTarget != 0)
			{
				PerformTarget = other.PerformTarget;
			}
			if (other.PerformSelf != 0)
			{
				PerformSelf = other.PerformSelf;
			}
			if (other.OrderWeight != 0)
			{
				OrderWeight = other.OrderWeight;
			}
			if (other.AttackPt != 0)
			{
				AttackPt = other.AttackPt;
			}
			if (other.CardPt != 0)
			{
				CardPt = other.CardPt;
			}
			if (other.SupportPt != 0)
			{
				SupportPt = other.SupportPt;
			}
			if (other.TankPt != 0)
			{
				TankPt = other.TankPt;
			}
			if (other.RelicRecBaseScore != 0)
			{
				RelicRecBaseScore = other.RelicRecBaseScore;
			}
			if (other.RelicRecGrowth != 0)
			{
				RelicRecGrowth = other.RelicRecGrowth;
			}
			relicRecRoleNormal_.Add(other.relicRecRoleNormal_);
			relicRecRole_.Add(other.relicRecRole_);
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
				RelicQualityType = (RelicQualityType)input.ReadEnum();
				break;
			case 24u:
			case 26u:
				tagType_.AddEntriesFrom(ref input, _repeated_tagType_codec);
				break;
			case 32u:
				KeyWordType = (RelicKeyWordType)input.ReadEnum();
				break;
			case 40u:
			case 42u:
				mapLimit_.AddEntriesFrom(ref input, _repeated_mapLimit_codec);
				break;
			case 50u:
				Icon = input.ReadString();
				break;
			case 61u:
				NameID = input.ReadSFixed32();
				break;
			case 69u:
				DescID = input.ReadSFixed32();
				break;
			case 72u:
			case 74u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 85u:
				BuffId = input.ReadSFixed32();
				break;
			case 93u:
				PerformTarget = input.ReadSFixed32();
				break;
			case 101u:
				PerformSelf = input.ReadSFixed32();
				break;
			case 109u:
				OrderWeight = input.ReadSFixed32();
				break;
			case 117u:
				AttackPt = input.ReadSFixed32();
				break;
			case 125u:
				CardPt = input.ReadSFixed32();
				break;
			case 133u:
				SupportPt = input.ReadSFixed32();
				break;
			case 141u:
				TankPt = input.ReadSFixed32();
				break;
			case 149u:
				RelicRecBaseScore = input.ReadSFixed32();
				break;
			case 157u:
				RelicRecGrowth = input.ReadSFixed32();
				break;
			case 160u:
			case 162u:
				relicRecRoleNormal_.AddEntriesFrom(ref input, _repeated_relicRecRoleNormal_codec);
				break;
			case 168u:
			case 170u:
				relicRecRole_.AddEntriesFrom(ref input, _repeated_relicRecRole_codec);
				break;
			}
		}
	}
}
