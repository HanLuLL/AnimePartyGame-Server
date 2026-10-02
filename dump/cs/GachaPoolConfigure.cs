using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GachaPoolConfigure : IMessage<GachaPoolConfigure>, IMessage, IEquatable<GachaPoolConfigure>, IDeepCloneable<GachaPoolConfigure>, IBufferMessage
{
	private static readonly MessageParser<GachaPoolConfigure> _parser = new MessageParser<GachaPoolConfigure>(() => new GachaPoolConfigure());

	private UnknownFieldSet _unknownFields;

	public const int PoolIDFieldNumber = 1;

	private int poolID_;

	public const int CombDefaultFieldNumber = 2;

	private int combDefault_;

	public const int CombGuaranteeFieldNumber = 3;

	private int combGuarantee_;

	public const int CombRoleFieldNumber = 4;

	private int combRole_;

	public const int CombUPFieldNumber = 5;

	private int combUP_;

	public const int NameIDFieldNumber = 6;

	private int nameID_;

	public const int PublicityIDFieldNumber = 7;

	private int publicityID_;

	public const int RuleDecriptionIDFieldNumber = 8;

	private int ruleDecriptionID_;

	public const int RoleDecriptionIDFieldNumber = 9;

	private int roleDecriptionID_;

	public const int FashionDecriptionIDFieldNumber = 10;

	private int fashionDecriptionID_;

	public const int GiftDecriptionIDFieldNumber = 11;

	private int giftDecriptionID_;

	public const int OtherDecriptionIDFieldNumber = 12;

	private int otherDecriptionID_;

	public const int UpItemFieldNumber = 13;

	private static readonly FieldCodec<int> _repeated_upItem_codec = FieldCodec.ForSFixed32(106u);

	private readonly RepeatedField<int> upItem_ = new RepeatedField<int>();

	public const int SkinCharacterFieldNumber = 14;

	private int skinCharacter_;

	public const int SkinNameFieldNumber = 15;

	private int skinName_;

	public const int SeasonSkinRerunNotUpItemFieldNumber = 16;

	private static readonly FieldCodec<int> _repeated_seasonSkinRerunNotUpItem_codec = FieldCodec.ForSFixed32(130u);

	private readonly RepeatedField<int> seasonSkinRerunNotUpItem_ = new RepeatedField<int>();

	public const int SeasonSkinRerunCharacterFieldNumber = 17;

	private static readonly FieldCodec<int> _repeated_seasonSkinRerunCharacter_codec = FieldCodec.ForSFixed32(138u);

	private readonly RepeatedField<int> seasonSkinRerunCharacter_ = new RepeatedField<int>();

	public const int SeasonSkinRerunNameFieldNumber = 18;

	private static readonly FieldCodec<int> _repeated_seasonSkinRerunName_codec = FieldCodec.ForSFixed32(146u);

	private readonly RepeatedField<int> seasonSkinRerunName_ = new RepeatedField<int>();

	public const int BackgroundCNFieldNumber = 19;

	private string backgroundCN_ = "";

	public const int BackgroundCNSFWFieldNumber = 20;

	private string backgroundCNSFW_ = "";

	public const int BackgroundENFieldNumber = 21;

	private string backgroundEN_ = "";

	public const int BackgroundENSFWFieldNumber = 22;

	private string backgroundENSFW_ = "";

	public const int BackgroundJPFieldNumber = 23;

	private string backgroundJP_ = "";

	public const int BackgroundJPSFWFieldNumber = 24;

	private string backgroundJPSFW_ = "";

	public const int BackgroundTCFieldNumber = 25;

	private string backgroundTC_ = "";

	public const int BackgroundTCSFWFieldNumber = 26;

	private string backgroundTCSFW_ = "";

	public const int ProgressRewardFieldNumber = 27;

	private int progressReward_;

	public const int ExtraRewardFieldNumber = 28;

	private static readonly MapField<int, int>.Codec _map_extraReward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 226u);

	private readonly MapField<int, int> extraReward_ = new MapField<int, int>();

	public const int WayFieldNumber = 29;

	private int way_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GachaPoolConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GachaReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PoolID
	{
		get
		{
			return poolID_;
		}
		private set
		{
			poolID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CombDefault
	{
		get
		{
			return combDefault_;
		}
		private set
		{
			combDefault_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CombGuarantee
	{
		get
		{
			return combGuarantee_;
		}
		private set
		{
			combGuarantee_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CombRole
	{
		get
		{
			return combRole_;
		}
		private set
		{
			combRole_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CombUP
	{
		get
		{
			return combUP_;
		}
		private set
		{
			combUP_ = value;
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
	public int PublicityID
	{
		get
		{
			return publicityID_;
		}
		private set
		{
			publicityID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RuleDecriptionID
	{
		get
		{
			return ruleDecriptionID_;
		}
		private set
		{
			ruleDecriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoleDecriptionID
	{
		get
		{
			return roleDecriptionID_;
		}
		private set
		{
			roleDecriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FashionDecriptionID
	{
		get
		{
			return fashionDecriptionID_;
		}
		private set
		{
			fashionDecriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GiftDecriptionID
	{
		get
		{
			return giftDecriptionID_;
		}
		private set
		{
			giftDecriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OtherDecriptionID
	{
		get
		{
			return otherDecriptionID_;
		}
		private set
		{
			otherDecriptionID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> UpItem => upItem_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkinCharacter
	{
		get
		{
			return skinCharacter_;
		}
		private set
		{
			skinCharacter_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkinName
	{
		get
		{
			return skinName_;
		}
		private set
		{
			skinName_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> SeasonSkinRerunNotUpItem => seasonSkinRerunNotUpItem_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> SeasonSkinRerunCharacter => seasonSkinRerunCharacter_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> SeasonSkinRerunName => seasonSkinRerunName_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundCN
	{
		get
		{
			return backgroundCN_;
		}
		private set
		{
			backgroundCN_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundCNSFW
	{
		get
		{
			return backgroundCNSFW_;
		}
		private set
		{
			backgroundCNSFW_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundEN
	{
		get
		{
			return backgroundEN_;
		}
		private set
		{
			backgroundEN_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundENSFW
	{
		get
		{
			return backgroundENSFW_;
		}
		private set
		{
			backgroundENSFW_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundJP
	{
		get
		{
			return backgroundJP_;
		}
		private set
		{
			backgroundJP_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundJPSFW
	{
		get
		{
			return backgroundJPSFW_;
		}
		private set
		{
			backgroundJPSFW_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundTC
	{
		get
		{
			return backgroundTC_;
		}
		private set
		{
			backgroundTC_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BackgroundTCSFW
	{
		get
		{
			return backgroundTCSFW_;
		}
		private set
		{
			backgroundTCSFW_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ProgressReward
	{
		get
		{
			return progressReward_;
		}
		private set
		{
			progressReward_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> ExtraReward => extraReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Way
	{
		get
		{
			return way_;
		}
		private set
		{
			way_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaPoolConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaPoolConfigure(GachaPoolConfigure other)
		: this()
	{
		poolID_ = other.poolID_;
		combDefault_ = other.combDefault_;
		combGuarantee_ = other.combGuarantee_;
		combRole_ = other.combRole_;
		combUP_ = other.combUP_;
		nameID_ = other.nameID_;
		publicityID_ = other.publicityID_;
		ruleDecriptionID_ = other.ruleDecriptionID_;
		roleDecriptionID_ = other.roleDecriptionID_;
		fashionDecriptionID_ = other.fashionDecriptionID_;
		giftDecriptionID_ = other.giftDecriptionID_;
		otherDecriptionID_ = other.otherDecriptionID_;
		upItem_ = other.upItem_.Clone();
		skinCharacter_ = other.skinCharacter_;
		skinName_ = other.skinName_;
		seasonSkinRerunNotUpItem_ = other.seasonSkinRerunNotUpItem_.Clone();
		seasonSkinRerunCharacter_ = other.seasonSkinRerunCharacter_.Clone();
		seasonSkinRerunName_ = other.seasonSkinRerunName_.Clone();
		backgroundCN_ = other.backgroundCN_;
		backgroundCNSFW_ = other.backgroundCNSFW_;
		backgroundEN_ = other.backgroundEN_;
		backgroundENSFW_ = other.backgroundENSFW_;
		backgroundJP_ = other.backgroundJP_;
		backgroundJPSFW_ = other.backgroundJPSFW_;
		backgroundTC_ = other.backgroundTC_;
		backgroundTCSFW_ = other.backgroundTCSFW_;
		progressReward_ = other.progressReward_;
		extraReward_ = other.extraReward_.Clone();
		way_ = other.way_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaPoolConfigure Clone()
	{
		return new GachaPoolConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GachaPoolConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GachaPoolConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PoolID != other.PoolID)
		{
			return false;
		}
		if (CombDefault != other.CombDefault)
		{
			return false;
		}
		if (CombGuarantee != other.CombGuarantee)
		{
			return false;
		}
		if (CombRole != other.CombRole)
		{
			return false;
		}
		if (CombUP != other.CombUP)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (PublicityID != other.PublicityID)
		{
			return false;
		}
		if (RuleDecriptionID != other.RuleDecriptionID)
		{
			return false;
		}
		if (RoleDecriptionID != other.RoleDecriptionID)
		{
			return false;
		}
		if (FashionDecriptionID != other.FashionDecriptionID)
		{
			return false;
		}
		if (GiftDecriptionID != other.GiftDecriptionID)
		{
			return false;
		}
		if (OtherDecriptionID != other.OtherDecriptionID)
		{
			return false;
		}
		if (!upItem_.Equals(other.upItem_))
		{
			return false;
		}
		if (SkinCharacter != other.SkinCharacter)
		{
			return false;
		}
		if (SkinName != other.SkinName)
		{
			return false;
		}
		if (!seasonSkinRerunNotUpItem_.Equals(other.seasonSkinRerunNotUpItem_))
		{
			return false;
		}
		if (!seasonSkinRerunCharacter_.Equals(other.seasonSkinRerunCharacter_))
		{
			return false;
		}
		if (!seasonSkinRerunName_.Equals(other.seasonSkinRerunName_))
		{
			return false;
		}
		if (BackgroundCN != other.BackgroundCN)
		{
			return false;
		}
		if (BackgroundCNSFW != other.BackgroundCNSFW)
		{
			return false;
		}
		if (BackgroundEN != other.BackgroundEN)
		{
			return false;
		}
		if (BackgroundENSFW != other.BackgroundENSFW)
		{
			return false;
		}
		if (BackgroundJP != other.BackgroundJP)
		{
			return false;
		}
		if (BackgroundJPSFW != other.BackgroundJPSFW)
		{
			return false;
		}
		if (BackgroundTC != other.BackgroundTC)
		{
			return false;
		}
		if (BackgroundTCSFW != other.BackgroundTCSFW)
		{
			return false;
		}
		if (ProgressReward != other.ProgressReward)
		{
			return false;
		}
		if (!ExtraReward.Equals(other.ExtraReward))
		{
			return false;
		}
		if (Way != other.Way)
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
		if (PoolID != 0)
		{
			num ^= PoolID.GetHashCode();
		}
		if (CombDefault != 0)
		{
			num ^= CombDefault.GetHashCode();
		}
		if (CombGuarantee != 0)
		{
			num ^= CombGuarantee.GetHashCode();
		}
		if (CombRole != 0)
		{
			num ^= CombRole.GetHashCode();
		}
		if (CombUP != 0)
		{
			num ^= CombUP.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (PublicityID != 0)
		{
			num ^= PublicityID.GetHashCode();
		}
		if (RuleDecriptionID != 0)
		{
			num ^= RuleDecriptionID.GetHashCode();
		}
		if (RoleDecriptionID != 0)
		{
			num ^= RoleDecriptionID.GetHashCode();
		}
		if (FashionDecriptionID != 0)
		{
			num ^= FashionDecriptionID.GetHashCode();
		}
		if (GiftDecriptionID != 0)
		{
			num ^= GiftDecriptionID.GetHashCode();
		}
		if (OtherDecriptionID != 0)
		{
			num ^= OtherDecriptionID.GetHashCode();
		}
		num ^= upItem_.GetHashCode();
		if (SkinCharacter != 0)
		{
			num ^= SkinCharacter.GetHashCode();
		}
		if (SkinName != 0)
		{
			num ^= SkinName.GetHashCode();
		}
		num ^= seasonSkinRerunNotUpItem_.GetHashCode();
		num ^= seasonSkinRerunCharacter_.GetHashCode();
		num ^= seasonSkinRerunName_.GetHashCode();
		if (BackgroundCN.Length != 0)
		{
			num ^= BackgroundCN.GetHashCode();
		}
		if (BackgroundCNSFW.Length != 0)
		{
			num ^= BackgroundCNSFW.GetHashCode();
		}
		if (BackgroundEN.Length != 0)
		{
			num ^= BackgroundEN.GetHashCode();
		}
		if (BackgroundENSFW.Length != 0)
		{
			num ^= BackgroundENSFW.GetHashCode();
		}
		if (BackgroundJP.Length != 0)
		{
			num ^= BackgroundJP.GetHashCode();
		}
		if (BackgroundJPSFW.Length != 0)
		{
			num ^= BackgroundJPSFW.GetHashCode();
		}
		if (BackgroundTC.Length != 0)
		{
			num ^= BackgroundTC.GetHashCode();
		}
		if (BackgroundTCSFW.Length != 0)
		{
			num ^= BackgroundTCSFW.GetHashCode();
		}
		if (ProgressReward != 0)
		{
			num ^= ProgressReward.GetHashCode();
		}
		num ^= ExtraReward.GetHashCode();
		if (Way != 0)
		{
			num ^= Way.GetHashCode();
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
		if (PoolID != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(PoolID);
		}
		if (CombDefault != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CombDefault);
		}
		if (CombGuarantee != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(CombGuarantee);
		}
		if (CombRole != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CombRole);
		}
		if (CombUP != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CombUP);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(NameID);
		}
		if (PublicityID != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(PublicityID);
		}
		if (RuleDecriptionID != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(RuleDecriptionID);
		}
		if (RoleDecriptionID != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(RoleDecriptionID);
		}
		if (FashionDecriptionID != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(FashionDecriptionID);
		}
		if (GiftDecriptionID != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(GiftDecriptionID);
		}
		if (OtherDecriptionID != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(OtherDecriptionID);
		}
		upItem_.WriteTo(ref output, _repeated_upItem_codec);
		if (SkinCharacter != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(SkinCharacter);
		}
		if (SkinName != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(SkinName);
		}
		seasonSkinRerunNotUpItem_.WriteTo(ref output, _repeated_seasonSkinRerunNotUpItem_codec);
		seasonSkinRerunCharacter_.WriteTo(ref output, _repeated_seasonSkinRerunCharacter_codec);
		seasonSkinRerunName_.WriteTo(ref output, _repeated_seasonSkinRerunName_codec);
		if (BackgroundCN.Length != 0)
		{
			output.WriteRawTag(154, 1);
			output.WriteString(BackgroundCN);
		}
		if (BackgroundCNSFW.Length != 0)
		{
			output.WriteRawTag(162, 1);
			output.WriteString(BackgroundCNSFW);
		}
		if (BackgroundEN.Length != 0)
		{
			output.WriteRawTag(170, 1);
			output.WriteString(BackgroundEN);
		}
		if (BackgroundENSFW.Length != 0)
		{
			output.WriteRawTag(178, 1);
			output.WriteString(BackgroundENSFW);
		}
		if (BackgroundJP.Length != 0)
		{
			output.WriteRawTag(186, 1);
			output.WriteString(BackgroundJP);
		}
		if (BackgroundJPSFW.Length != 0)
		{
			output.WriteRawTag(194, 1);
			output.WriteString(BackgroundJPSFW);
		}
		if (BackgroundTC.Length != 0)
		{
			output.WriteRawTag(202, 1);
			output.WriteString(BackgroundTC);
		}
		if (BackgroundTCSFW.Length != 0)
		{
			output.WriteRawTag(210, 1);
			output.WriteString(BackgroundTCSFW);
		}
		if (ProgressReward != 0)
		{
			output.WriteRawTag(221, 1);
			output.WriteSFixed32(ProgressReward);
		}
		extraReward_.WriteTo(ref output, _map_extraReward_codec);
		if (Way != 0)
		{
			output.WriteRawTag(237, 1);
			output.WriteSFixed32(Way);
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
		if (PoolID != 0)
		{
			num += 5;
		}
		if (CombDefault != 0)
		{
			num += 5;
		}
		if (CombGuarantee != 0)
		{
			num += 5;
		}
		if (CombRole != 0)
		{
			num += 5;
		}
		if (CombUP != 0)
		{
			num += 5;
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (PublicityID != 0)
		{
			num += 5;
		}
		if (RuleDecriptionID != 0)
		{
			num += 5;
		}
		if (RoleDecriptionID != 0)
		{
			num += 5;
		}
		if (FashionDecriptionID != 0)
		{
			num += 5;
		}
		if (GiftDecriptionID != 0)
		{
			num += 5;
		}
		if (OtherDecriptionID != 0)
		{
			num += 5;
		}
		num += upItem_.CalculateSize(_repeated_upItem_codec);
		if (SkinCharacter != 0)
		{
			num += 5;
		}
		if (SkinName != 0)
		{
			num += 5;
		}
		num += seasonSkinRerunNotUpItem_.CalculateSize(_repeated_seasonSkinRerunNotUpItem_codec);
		num += seasonSkinRerunCharacter_.CalculateSize(_repeated_seasonSkinRerunCharacter_codec);
		num += seasonSkinRerunName_.CalculateSize(_repeated_seasonSkinRerunName_codec);
		if (BackgroundCN.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BackgroundCN);
		}
		if (BackgroundCNSFW.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BackgroundCNSFW);
		}
		if (BackgroundEN.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BackgroundEN);
		}
		if (BackgroundENSFW.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BackgroundENSFW);
		}
		if (BackgroundJP.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BackgroundJP);
		}
		if (BackgroundJPSFW.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BackgroundJPSFW);
		}
		if (BackgroundTC.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BackgroundTC);
		}
		if (BackgroundTCSFW.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BackgroundTCSFW);
		}
		if (ProgressReward != 0)
		{
			num += 6;
		}
		num += extraReward_.CalculateSize(_map_extraReward_codec);
		if (Way != 0)
		{
			num += 6;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GachaPoolConfigure other)
	{
		if (other != null)
		{
			if (other.PoolID != 0)
			{
				PoolID = other.PoolID;
			}
			if (other.CombDefault != 0)
			{
				CombDefault = other.CombDefault;
			}
			if (other.CombGuarantee != 0)
			{
				CombGuarantee = other.CombGuarantee;
			}
			if (other.CombRole != 0)
			{
				CombRole = other.CombRole;
			}
			if (other.CombUP != 0)
			{
				CombUP = other.CombUP;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.PublicityID != 0)
			{
				PublicityID = other.PublicityID;
			}
			if (other.RuleDecriptionID != 0)
			{
				RuleDecriptionID = other.RuleDecriptionID;
			}
			if (other.RoleDecriptionID != 0)
			{
				RoleDecriptionID = other.RoleDecriptionID;
			}
			if (other.FashionDecriptionID != 0)
			{
				FashionDecriptionID = other.FashionDecriptionID;
			}
			if (other.GiftDecriptionID != 0)
			{
				GiftDecriptionID = other.GiftDecriptionID;
			}
			if (other.OtherDecriptionID != 0)
			{
				OtherDecriptionID = other.OtherDecriptionID;
			}
			upItem_.Add(other.upItem_);
			if (other.SkinCharacter != 0)
			{
				SkinCharacter = other.SkinCharacter;
			}
			if (other.SkinName != 0)
			{
				SkinName = other.SkinName;
			}
			seasonSkinRerunNotUpItem_.Add(other.seasonSkinRerunNotUpItem_);
			seasonSkinRerunCharacter_.Add(other.seasonSkinRerunCharacter_);
			seasonSkinRerunName_.Add(other.seasonSkinRerunName_);
			if (other.BackgroundCN.Length != 0)
			{
				BackgroundCN = other.BackgroundCN;
			}
			if (other.BackgroundCNSFW.Length != 0)
			{
				BackgroundCNSFW = other.BackgroundCNSFW;
			}
			if (other.BackgroundEN.Length != 0)
			{
				BackgroundEN = other.BackgroundEN;
			}
			if (other.BackgroundENSFW.Length != 0)
			{
				BackgroundENSFW = other.BackgroundENSFW;
			}
			if (other.BackgroundJP.Length != 0)
			{
				BackgroundJP = other.BackgroundJP;
			}
			if (other.BackgroundJPSFW.Length != 0)
			{
				BackgroundJPSFW = other.BackgroundJPSFW;
			}
			if (other.BackgroundTC.Length != 0)
			{
				BackgroundTC = other.BackgroundTC;
			}
			if (other.BackgroundTCSFW.Length != 0)
			{
				BackgroundTCSFW = other.BackgroundTCSFW;
			}
			if (other.ProgressReward != 0)
			{
				ProgressReward = other.ProgressReward;
			}
			extraReward_.MergeFrom(other.extraReward_);
			if (other.Way != 0)
			{
				Way = other.Way;
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
				PoolID = input.ReadSFixed32();
				break;
			case 21u:
				CombDefault = input.ReadSFixed32();
				break;
			case 29u:
				CombGuarantee = input.ReadSFixed32();
				break;
			case 37u:
				CombRole = input.ReadSFixed32();
				break;
			case 45u:
				CombUP = input.ReadSFixed32();
				break;
			case 53u:
				NameID = input.ReadSFixed32();
				break;
			case 61u:
				PublicityID = input.ReadSFixed32();
				break;
			case 69u:
				RuleDecriptionID = input.ReadSFixed32();
				break;
			case 77u:
				RoleDecriptionID = input.ReadSFixed32();
				break;
			case 85u:
				FashionDecriptionID = input.ReadSFixed32();
				break;
			case 93u:
				GiftDecriptionID = input.ReadSFixed32();
				break;
			case 101u:
				OtherDecriptionID = input.ReadSFixed32();
				break;
			case 106u:
			case 109u:
				upItem_.AddEntriesFrom(ref input, _repeated_upItem_codec);
				break;
			case 117u:
				SkinCharacter = input.ReadSFixed32();
				break;
			case 125u:
				SkinName = input.ReadSFixed32();
				break;
			case 130u:
			case 133u:
				seasonSkinRerunNotUpItem_.AddEntriesFrom(ref input, _repeated_seasonSkinRerunNotUpItem_codec);
				break;
			case 138u:
			case 141u:
				seasonSkinRerunCharacter_.AddEntriesFrom(ref input, _repeated_seasonSkinRerunCharacter_codec);
				break;
			case 146u:
			case 149u:
				seasonSkinRerunName_.AddEntriesFrom(ref input, _repeated_seasonSkinRerunName_codec);
				break;
			case 154u:
				BackgroundCN = input.ReadString();
				break;
			case 162u:
				BackgroundCNSFW = input.ReadString();
				break;
			case 170u:
				BackgroundEN = input.ReadString();
				break;
			case 178u:
				BackgroundENSFW = input.ReadString();
				break;
			case 186u:
				BackgroundJP = input.ReadString();
				break;
			case 194u:
				BackgroundJPSFW = input.ReadString();
				break;
			case 202u:
				BackgroundTC = input.ReadString();
				break;
			case 210u:
				BackgroundTCSFW = input.ReadString();
				break;
			case 221u:
				ProgressReward = input.ReadSFixed32();
				break;
			case 226u:
				extraReward_.AddEntriesFrom(ref input, _map_extraReward_codec);
				break;
			case 237u:
				Way = input.ReadSFixed32();
				break;
			}
		}
	}
}
