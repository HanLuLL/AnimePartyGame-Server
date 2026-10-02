using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Core;
using GameLogic;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Tools;

public sealed class CardInfoConfigure : IMessage<CardInfoConfigure>, IMessage, IEquatable<CardInfoConfigure>, IDeepCloneable<CardInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<CardInfoConfigure> _parser = new MessageParser<CardInfoConfigure>(() => new CardInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int CardNumbFieldNumber = 3;

	private int cardNumb_;

	public const int DescIdFieldNumber = 4;

	private int descId_;

	public const int CommentIdFieldNumber = 5;

	private int commentId_;

	public const int ImageFieldNumber = 6;

	private string image_ = "";

	public const int ImageSfwFieldNumber = 7;

	private string imageSfw_ = "";

	public const int EffectTypeFieldNumber = 8;

	private EffectType effectType_;

	public const int CardTypeFieldNumber = 9;

	private CardType cardType_;

	public const int CardTargetTypeFieldNumber = 10;

	private CardTargetType cardTargetType_;

	public const int IsContainSelfFieldNumber = 11;

	private bool isContainSelf_;

	public const int IsContainHospitalPlayerFieldNumber = 12;

	private bool isContainHospitalPlayer_;

	public const int IsContainPlayerFieldNumber = 13;

	private bool isContainPlayer_;

	public const int IsHostileFieldNumber = 14;

	private bool isHostile_;

	public const int IsFriendlyFieldNumber = 15;

	private bool isFriendly_;

	public const int IsContainMonsterFieldNumber = 16;

	private bool isContainMonster_;

	public const int IsContainDeadTargetFieldNumber = 17;

	private bool isContainDeadTarget_;

	public const int IsPlayableFieldNumber = 18;

	private bool isPlayable_;

	public const int CostFieldNumber = 19;

	private int cost_;

	public const int ParamsFieldNumber = 20;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(162u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	public const int BuffIdsFieldNumber = 21;

	private static readonly FieldCodec<int> _repeated_buffIds_codec = FieldCodec.ForSFixed32(170u);

	private readonly RepeatedField<int> buffIds_ = new RepeatedField<int>();

	public const int CardIdsFieldNumber = 22;

	private static readonly FieldCodec<int> _repeated_cardIds_codec = FieldCodec.ForSFixed32(178u);

	private readonly RepeatedField<int> cardIds_ = new RepeatedField<int>();

	public const int PerformCastFieldNumber = 23;

	private static readonly FieldCodec<int> _repeated_performCast_codec = FieldCodec.ForSFixed32(186u);

	private readonly RepeatedField<int> performCast_ = new RepeatedField<int>();

	public const int PerformTargetFieldNumber = 24;

	private static readonly FieldCodec<int> _repeated_performTarget_codec = FieldCodec.ForSFixed32(194u);

	private readonly RepeatedField<int> performTarget_ = new RepeatedField<int>();

	public const int IsGalleryShowFieldNumber = 25;

	private bool isGalleryShow_;

	public const int IsDedicatedFieldNumber = 26;

	private bool isDedicated_;

	public const int RecomBaseFieldNumber = 27;

	private int recomBase_;

	public const int RecomPlusFieldNumber = 28;

	private static readonly FieldCodec<int> _repeated_recomPlus_codec = FieldCodec.ForSFixed32(226u);

	private readonly RepeatedField<int> recomPlus_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CardInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CardReflection.Descriptor.MessageTypes[0];

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
	public int CardNumb
	{
		get
		{
			return cardNumb_;
		}
		private set
		{
			cardNumb_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescId
	{
		get
		{
			return descId_;
		}
		private set
		{
			descId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CommentId
	{
		get
		{
			return commentId_;
		}
		private set
		{
			commentId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Image
	{
		get
		{
			return image_;
		}
		private set
		{
			image_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ImageSfw
	{
		get
		{
			return imageSfw_;
		}
		private set
		{
			imageSfw_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EffectType EffectType
	{
		get
		{
			return effectType_;
		}
		private set
		{
			effectType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardType CardType
	{
		get
		{
			return cardType_;
		}
		private set
		{
			cardType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardTargetType CardTargetType
	{
		get
		{
			return cardTargetType_;
		}
		private set
		{
			cardTargetType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsContainSelf
	{
		get
		{
			return isContainSelf_;
		}
		private set
		{
			isContainSelf_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsContainHospitalPlayer
	{
		get
		{
			return isContainHospitalPlayer_;
		}
		private set
		{
			isContainHospitalPlayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsContainPlayer
	{
		get
		{
			return isContainPlayer_;
		}
		private set
		{
			isContainPlayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsHostile
	{
		get
		{
			return isHostile_;
		}
		private set
		{
			isHostile_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsFriendly
	{
		get
		{
			return isFriendly_;
		}
		private set
		{
			isFriendly_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsContainMonster
	{
		get
		{
			return isContainMonster_;
		}
		private set
		{
			isContainMonster_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsContainDeadTarget
	{
		get
		{
			return isContainDeadTarget_;
		}
		private set
		{
			isContainDeadTarget_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsPlayable
	{
		get
		{
			return isPlayable_;
		}
		private set
		{
			isPlayable_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Cost
	{
		get
		{
			return cost_;
		}
		private set
		{
			cost_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuffIds => buffIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CardIds => cardIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PerformCast => performCast_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PerformTarget => performTarget_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsGalleryShow
	{
		get
		{
			return isGalleryShow_;
		}
		private set
		{
			isGalleryShow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDedicated
	{
		get
		{
			return isDedicated_;
		}
		private set
		{
			isDedicated_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RecomBase
	{
		get
		{
			return recomBase_;
		}
		private set
		{
			recomBase_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RecomPlus => recomPlus_;

	public int ReleaseDefaultPerform
	{
		get
		{
			if (performCast_.Count <= 0)
			{
				return 0;
			}
			return performCast_[0];
		}
	}

	public int TargetDefaultPerform
	{
		get
		{
			if (performTarget_.Count <= 0)
			{
				return 0;
			}
			return performTarget_[0];
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardInfoConfigure(CardInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		nameID_ = other.nameID_;
		cardNumb_ = other.cardNumb_;
		descId_ = other.descId_;
		commentId_ = other.commentId_;
		image_ = other.image_;
		imageSfw_ = other.imageSfw_;
		effectType_ = other.effectType_;
		cardType_ = other.cardType_;
		cardTargetType_ = other.cardTargetType_;
		isContainSelf_ = other.isContainSelf_;
		isContainHospitalPlayer_ = other.isContainHospitalPlayer_;
		isContainPlayer_ = other.isContainPlayer_;
		isHostile_ = other.isHostile_;
		isFriendly_ = other.isFriendly_;
		isContainMonster_ = other.isContainMonster_;
		isContainDeadTarget_ = other.isContainDeadTarget_;
		isPlayable_ = other.isPlayable_;
		cost_ = other.cost_;
		params_ = other.params_.Clone();
		buffIds_ = other.buffIds_.Clone();
		cardIds_ = other.cardIds_.Clone();
		performCast_ = other.performCast_.Clone();
		performTarget_ = other.performTarget_.Clone();
		isGalleryShow_ = other.isGalleryShow_;
		isDedicated_ = other.isDedicated_;
		recomBase_ = other.recomBase_;
		recomPlus_ = other.recomPlus_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardInfoConfigure Clone()
	{
		return new CardInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CardInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CardInfoConfigure other)
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
		if (NameID != other.NameID)
		{
			return false;
		}
		if (CardNumb != other.CardNumb)
		{
			return false;
		}
		if (DescId != other.DescId)
		{
			return false;
		}
		if (CommentId != other.CommentId)
		{
			return false;
		}
		if (Image != other.Image)
		{
			return false;
		}
		if (ImageSfw != other.ImageSfw)
		{
			return false;
		}
		if (EffectType != other.EffectType)
		{
			return false;
		}
		if (CardType != other.CardType)
		{
			return false;
		}
		if (CardTargetType != other.CardTargetType)
		{
			return false;
		}
		if (IsContainSelf != other.IsContainSelf)
		{
			return false;
		}
		if (IsContainHospitalPlayer != other.IsContainHospitalPlayer)
		{
			return false;
		}
		if (IsContainPlayer != other.IsContainPlayer)
		{
			return false;
		}
		if (IsHostile != other.IsHostile)
		{
			return false;
		}
		if (IsFriendly != other.IsFriendly)
		{
			return false;
		}
		if (IsContainMonster != other.IsContainMonster)
		{
			return false;
		}
		if (IsContainDeadTarget != other.IsContainDeadTarget)
		{
			return false;
		}
		if (IsPlayable != other.IsPlayable)
		{
			return false;
		}
		if (Cost != other.Cost)
		{
			return false;
		}
		if (!params_.Equals(other.params_))
		{
			return false;
		}
		if (!buffIds_.Equals(other.buffIds_))
		{
			return false;
		}
		if (!cardIds_.Equals(other.cardIds_))
		{
			return false;
		}
		if (!performCast_.Equals(other.performCast_))
		{
			return false;
		}
		if (!performTarget_.Equals(other.performTarget_))
		{
			return false;
		}
		if (IsGalleryShow != other.IsGalleryShow)
		{
			return false;
		}
		if (IsDedicated != other.IsDedicated)
		{
			return false;
		}
		if (RecomBase != other.RecomBase)
		{
			return false;
		}
		if (!recomPlus_.Equals(other.recomPlus_))
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
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (CardNumb != 0)
		{
			num ^= CardNumb.GetHashCode();
		}
		if (DescId != 0)
		{
			num ^= DescId.GetHashCode();
		}
		if (CommentId != 0)
		{
			num ^= CommentId.GetHashCode();
		}
		if (Image.Length != 0)
		{
			num ^= Image.GetHashCode();
		}
		if (ImageSfw.Length != 0)
		{
			num ^= ImageSfw.GetHashCode();
		}
		if (EffectType != EffectType.None)
		{
			num ^= EffectType.GetHashCode();
		}
		if (CardType != CardType.None)
		{
			num ^= CardType.GetHashCode();
		}
		if (CardTargetType != CardTargetType.None)
		{
			num ^= CardTargetType.GetHashCode();
		}
		if (IsContainSelf)
		{
			num ^= IsContainSelf.GetHashCode();
		}
		if (IsContainHospitalPlayer)
		{
			num ^= IsContainHospitalPlayer.GetHashCode();
		}
		if (IsContainPlayer)
		{
			num ^= IsContainPlayer.GetHashCode();
		}
		if (IsHostile)
		{
			num ^= IsHostile.GetHashCode();
		}
		if (IsFriendly)
		{
			num ^= IsFriendly.GetHashCode();
		}
		if (IsContainMonster)
		{
			num ^= IsContainMonster.GetHashCode();
		}
		if (IsContainDeadTarget)
		{
			num ^= IsContainDeadTarget.GetHashCode();
		}
		if (IsPlayable)
		{
			num ^= IsPlayable.GetHashCode();
		}
		if (Cost != 0)
		{
			num ^= Cost.GetHashCode();
		}
		num ^= params_.GetHashCode();
		num ^= buffIds_.GetHashCode();
		num ^= cardIds_.GetHashCode();
		num ^= performCast_.GetHashCode();
		num ^= performTarget_.GetHashCode();
		if (IsGalleryShow)
		{
			num ^= IsGalleryShow.GetHashCode();
		}
		if (IsDedicated)
		{
			num ^= IsDedicated.GetHashCode();
		}
		if (RecomBase != 0)
		{
			num ^= RecomBase.GetHashCode();
		}
		num ^= recomPlus_.GetHashCode();
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
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
		}
		if (CardNumb != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(CardNumb);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(DescId);
		}
		if (CommentId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CommentId);
		}
		if (Image.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(Image);
		}
		if (ImageSfw.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(ImageSfw);
		}
		if (EffectType != EffectType.None)
		{
			output.WriteRawTag(64);
			output.WriteEnum((int)EffectType);
		}
		if (CardType != CardType.None)
		{
			output.WriteRawTag(72);
			output.WriteEnum((int)CardType);
		}
		if (CardTargetType != CardTargetType.None)
		{
			output.WriteRawTag(80);
			output.WriteEnum((int)CardTargetType);
		}
		if (IsContainSelf)
		{
			output.WriteRawTag(88);
			output.WriteBool(IsContainSelf);
		}
		if (IsContainHospitalPlayer)
		{
			output.WriteRawTag(96);
			output.WriteBool(IsContainHospitalPlayer);
		}
		if (IsContainPlayer)
		{
			output.WriteRawTag(104);
			output.WriteBool(IsContainPlayer);
		}
		if (IsHostile)
		{
			output.WriteRawTag(112);
			output.WriteBool(IsHostile);
		}
		if (IsFriendly)
		{
			output.WriteRawTag(120);
			output.WriteBool(IsFriendly);
		}
		if (IsContainMonster)
		{
			output.WriteRawTag(128, 1);
			output.WriteBool(IsContainMonster);
		}
		if (IsContainDeadTarget)
		{
			output.WriteRawTag(136, 1);
			output.WriteBool(IsContainDeadTarget);
		}
		if (IsPlayable)
		{
			output.WriteRawTag(144, 1);
			output.WriteBool(IsPlayable);
		}
		if (Cost != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(Cost);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
		buffIds_.WriteTo(ref output, _repeated_buffIds_codec);
		cardIds_.WriteTo(ref output, _repeated_cardIds_codec);
		performCast_.WriteTo(ref output, _repeated_performCast_codec);
		performTarget_.WriteTo(ref output, _repeated_performTarget_codec);
		if (IsGalleryShow)
		{
			output.WriteRawTag(200, 1);
			output.WriteBool(IsGalleryShow);
		}
		if (IsDedicated)
		{
			output.WriteRawTag(208, 1);
			output.WriteBool(IsDedicated);
		}
		if (RecomBase != 0)
		{
			output.WriteRawTag(221, 1);
			output.WriteSFixed32(RecomBase);
		}
		recomPlus_.WriteTo(ref output, _repeated_recomPlus_codec);
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
		if (NameID != 0)
		{
			num += 5;
		}
		if (CardNumb != 0)
		{
			num += 5;
		}
		if (DescId != 0)
		{
			num += 5;
		}
		if (CommentId != 0)
		{
			num += 5;
		}
		if (Image.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Image);
		}
		if (ImageSfw.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ImageSfw);
		}
		if (EffectType != EffectType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)EffectType);
		}
		if (CardType != CardType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)CardType);
		}
		if (CardTargetType != CardTargetType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)CardTargetType);
		}
		if (IsContainSelf)
		{
			num += 2;
		}
		if (IsContainHospitalPlayer)
		{
			num += 2;
		}
		if (IsContainPlayer)
		{
			num += 2;
		}
		if (IsHostile)
		{
			num += 2;
		}
		if (IsFriendly)
		{
			num += 2;
		}
		if (IsContainMonster)
		{
			num += 3;
		}
		if (IsContainDeadTarget)
		{
			num += 3;
		}
		if (IsPlayable)
		{
			num += 3;
		}
		if (Cost != 0)
		{
			num += 6;
		}
		num += params_.CalculateSize(_repeated_params_codec);
		num += buffIds_.CalculateSize(_repeated_buffIds_codec);
		num += cardIds_.CalculateSize(_repeated_cardIds_codec);
		num += performCast_.CalculateSize(_repeated_performCast_codec);
		num += performTarget_.CalculateSize(_repeated_performTarget_codec);
		if (IsGalleryShow)
		{
			num += 3;
		}
		if (IsDedicated)
		{
			num += 3;
		}
		if (RecomBase != 0)
		{
			num += 6;
		}
		num += recomPlus_.CalculateSize(_repeated_recomPlus_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CardInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.CardNumb != 0)
			{
				CardNumb = other.CardNumb;
			}
			if (other.DescId != 0)
			{
				DescId = other.DescId;
			}
			if (other.CommentId != 0)
			{
				CommentId = other.CommentId;
			}
			if (other.Image.Length != 0)
			{
				Image = other.Image;
			}
			if (other.ImageSfw.Length != 0)
			{
				ImageSfw = other.ImageSfw;
			}
			if (other.EffectType != EffectType.None)
			{
				EffectType = other.EffectType;
			}
			if (other.CardType != CardType.None)
			{
				CardType = other.CardType;
			}
			if (other.CardTargetType != CardTargetType.None)
			{
				CardTargetType = other.CardTargetType;
			}
			if (other.IsContainSelf)
			{
				IsContainSelf = other.IsContainSelf;
			}
			if (other.IsContainHospitalPlayer)
			{
				IsContainHospitalPlayer = other.IsContainHospitalPlayer;
			}
			if (other.IsContainPlayer)
			{
				IsContainPlayer = other.IsContainPlayer;
			}
			if (other.IsHostile)
			{
				IsHostile = other.IsHostile;
			}
			if (other.IsFriendly)
			{
				IsFriendly = other.IsFriendly;
			}
			if (other.IsContainMonster)
			{
				IsContainMonster = other.IsContainMonster;
			}
			if (other.IsContainDeadTarget)
			{
				IsContainDeadTarget = other.IsContainDeadTarget;
			}
			if (other.IsPlayable)
			{
				IsPlayable = other.IsPlayable;
			}
			if (other.Cost != 0)
			{
				Cost = other.Cost;
			}
			params_.Add(other.params_);
			buffIds_.Add(other.buffIds_);
			cardIds_.Add(other.cardIds_);
			performCast_.Add(other.performCast_);
			performTarget_.Add(other.performTarget_);
			if (other.IsGalleryShow)
			{
				IsGalleryShow = other.IsGalleryShow;
			}
			if (other.IsDedicated)
			{
				IsDedicated = other.IsDedicated;
			}
			if (other.RecomBase != 0)
			{
				RecomBase = other.RecomBase;
			}
			recomPlus_.Add(other.recomPlus_);
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
			case 21u:
				NameID = input.ReadSFixed32();
				break;
			case 29u:
				CardNumb = input.ReadSFixed32();
				break;
			case 37u:
				DescId = input.ReadSFixed32();
				break;
			case 45u:
				CommentId = input.ReadSFixed32();
				break;
			case 50u:
				Image = input.ReadString();
				break;
			case 58u:
				ImageSfw = input.ReadString();
				break;
			case 64u:
				EffectType = (EffectType)input.ReadEnum();
				break;
			case 72u:
				CardType = (CardType)input.ReadEnum();
				break;
			case 80u:
				CardTargetType = (CardTargetType)input.ReadEnum();
				break;
			case 88u:
				IsContainSelf = input.ReadBool();
				break;
			case 96u:
				IsContainHospitalPlayer = input.ReadBool();
				break;
			case 104u:
				IsContainPlayer = input.ReadBool();
				break;
			case 112u:
				IsHostile = input.ReadBool();
				break;
			case 120u:
				IsFriendly = input.ReadBool();
				break;
			case 128u:
				IsContainMonster = input.ReadBool();
				break;
			case 136u:
				IsContainDeadTarget = input.ReadBool();
				break;
			case 144u:
				IsPlayable = input.ReadBool();
				break;
			case 157u:
				Cost = input.ReadSFixed32();
				break;
			case 160u:
			case 162u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 170u:
			case 173u:
				buffIds_.AddEntriesFrom(ref input, _repeated_buffIds_codec);
				break;
			case 178u:
			case 181u:
				cardIds_.AddEntriesFrom(ref input, _repeated_cardIds_codec);
				break;
			case 186u:
			case 189u:
				performCast_.AddEntriesFrom(ref input, _repeated_performCast_codec);
				break;
			case 194u:
			case 197u:
				performTarget_.AddEntriesFrom(ref input, _repeated_performTarget_codec);
				break;
			case 200u:
				IsGalleryShow = input.ReadBool();
				break;
			case 208u:
				IsDedicated = input.ReadBool();
				break;
			case 221u:
				RecomBase = input.ReadSFixed32();
				break;
			case 226u:
			case 229u:
				recomPlus_.AddEntriesFrom(ref input, _repeated_recomPlus_codec);
				break;
			}
		}
	}

	public string GetImage()
	{
		if (GameSettings.angelMode && !string.IsNullOrEmpty(imageSfw_))
		{
			return imageSfw_;
		}
		return image_;
	}

	public CardView GetMainPlayerCardView()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.TryGetAltArtCardId(Id, out var altArtCardId);
		return GetCardView(altArtCardId);
	}

	public CardView GetBattlePlayerCardView(long playerId)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		int altArtCardId = 0;
		playerDataById?.TryGetAltArtCardId(Id, out altArtCardId);
		return GetCardView(altArtCardId);
	}

	public CardView GetCardView(int altArtCardId = 0)
	{
		CardAltArtConfigureItem cardAltArtConfigureItem = null;
		CardView cardView = new CardView();
		cardView.CardId = Id;
		cardView.AltArtCardId = altArtCardId;
		cardView.CardType = 0;
		if ((cardView.IsAltArtCard = altArtCardId != 0 && altArtCardId != Id) && StaticConfigure.Card.AltArtDict.TryGetValue(Id, out var value))
		{
			foreach (CardAltArtConfigureItem cardAltArtConfigureItem2 in value.CardAltArtConfigureItems)
			{
				if (cardAltArtConfigureItem2.AltArtId == altArtCardId)
				{
					cardAltArtConfigureItem = cardAltArtConfigureItem2;
					break;
				}
			}
		}
		if (cardAltArtConfigureItem != null)
		{
			string accountBackgroundVideo = cardAltArtConfigureItem.AccountBackgroundVideo;
			string accountBackgroundVideoSfw = cardAltArtConfigureItem.AccountBackgroundVideoSfw;
			string accountBackground = cardAltArtConfigureItem.AccountBackground;
			string accountBackgroundSfw = cardAltArtConfigureItem.AccountBackgroundSfw;
			bool flag = !string.IsNullOrEmpty(accountBackgroundVideo);
			cardView.CardFrontTexKey = cardAltArtConfigureItem.CardFront;
			cardView.CardFrontMatKey = cardAltArtConfigureItem.CardFrontMaterial;
			cardView.IsVideo = flag;
			cardView.CardType = cardAltArtConfigureItem.Type;
			if (flag)
			{
				if (GameSettings.angelMode && !string.IsNullOrEmpty(accountBackgroundVideoSfw))
				{
					cardView.Key = accountBackgroundVideoSfw;
				}
				else
				{
					cardView.Key = accountBackgroundVideo;
				}
			}
			else if (GameSettings.angelMode && !string.IsNullOrEmpty(accountBackgroundSfw))
			{
				cardView.Key = accountBackgroundSfw;
			}
			else
			{
				cardView.Key = accountBackground;
			}
		}
		else
		{
			cardView.IsVideo = false;
			if (GameSettings.angelMode && !string.IsNullOrEmpty(imageSfw_))
			{
				cardView.Key = imageSfw_;
			}
			else
			{
				cardView.Key = image_;
			}
		}
		return cardView;
	}
}
