using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SqlPlayerDataByte : IMessage<SqlPlayerDataByte>, IMessage, IEquatable<SqlPlayerDataByte>, IDeepCloneable<SqlPlayerDataByte>, IBufferMessage
{
	private static readonly MessageParser<SqlPlayerDataByte> _parser = new MessageParser<SqlPlayerDataByte>(() => new SqlPlayerDataByte());

	private UnknownFieldSet _unknownFields;

	public const int BagItemsFieldNumber = 1;

	private static readonly FieldCodec<ItemEtc> _repeated_bagItems_codec = FieldCodec.ForMessage(10u, ItemEtc.Parser);

	private readonly RepeatedField<ItemEtc> bagItems_ = new RepeatedField<ItemEtc>();

	public const int GachaCountFieldNumber = 2;

	private static readonly MapField<int, GachaCount>.Codec _map_gachaCount_codec = new MapField<int, GachaCount>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.GachaCount.Parser), 18u);

	private readonly MapField<int, GachaCount> gachaCount_ = new MapField<int, GachaCount>();

	public const int FashionPlanFieldNumber = 3;

	private static readonly FieldCodec<FashionPlan> _repeated_fashionPlan_codec = FieldCodec.ForMessage(26u, party.model.FashionPlan.Parser);

	private readonly RepeatedField<FashionPlan> fashionPlan_ = new RepeatedField<FashionPlan>();

	public const int RoleCardFieldNumber = 4;

	private static readonly MapField<int, RoleCard>.Codec _map_roleCard_codec = new MapField<int, RoleCard>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.RoleCard.Parser), 34u);

	private readonly MapField<int, RoleCard> roleCard_ = new MapField<int, RoleCard>();

	public const int WeeklyLimitsFieldNumber = 5;

	private static readonly MapField<int, int>.Codec _map_weeklyLimits_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 42u);

	private readonly MapField<int, int> weeklyLimits_ = new MapField<int, int>();

	public const int IsDayPlayPVEFieldNumber = 6;

	private bool isDayPlayPVE_;

	public const int CampaignPassFieldNumber = 7;

	private static readonly MapField<int, bool>.Codec _map_campaignPass_codec = new MapField<int, bool>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForBool(16u, defaultValue: false), 58u);

	private readonly MapField<int, bool> campaignPass_ = new MapField<int, bool>();

	public const int PayAmountInfoFieldNumber = 8;

	private PayAmountInfo payAmountInfo_;

	public const int LightGiftFieldNumber = 9;

	private static readonly MapField<int, LightGift>.Codec _map_lightGift_codec = new MapField<int, LightGift>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, party.model.LightGift.Parser), 74u);

	private readonly MapField<int, LightGift> lightGift_ = new MapField<int, LightGift>();

	public const int PayStarDiscFieldNumber = 10;

	private static readonly MapField<string, int>.Codec _map_payStarDisc_codec = new MapField<string, int>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<string, int> payStarDisc_ = new MapField<string, int>();

	public const int WinCountFieldNumber = 11;

	private int winCount_;

	public const int UnLockDifficultyFieldNumber = 12;

	private int unLockDifficulty_;

	public const int RecoupBagItemsFieldNumber = 13;

	private static readonly FieldCodec<ItemEtc> _repeated_recoupBagItems_codec = FieldCodec.ForMessage(106u, ItemEtc.Parser);

	private readonly RepeatedField<ItemEtc> recoupBagItems_ = new RepeatedField<ItemEtc>();

	public const int IsDayPlayPVPFieldNumber = 14;

	private bool isDayPlayPVP_;

	public const int DailyPraiseCountFieldNumber = 15;

	private int dailyPraiseCount_;

	public const int MapModeCountFieldNumber = 16;

	private static readonly MapField<int, int>.Codec _map_mapModeCount_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 130u);

	private readonly MapField<int, int> mapModeCount_ = new MapField<int, int>();

	public const int WinMapFieldNumber = 17;

	private static readonly MapField<int, int>.Codec _map_winMap_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 138u);

	private readonly MapField<int, int> winMap_ = new MapField<int, int>();

	public const int MapModeWinCountFieldNumber = 18;

	private static readonly MapField<int, int>.Codec _map_mapModeWinCount_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 146u);

	private readonly MapField<int, int> mapModeWinCount_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SqlPlayerDataByte> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[31];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemEtc> BagItems => bagItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GachaCount> GachaCount => gachaCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<FashionPlan> FashionPlan => fashionPlan_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, RoleCard> RoleCard => roleCard_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> WeeklyLimits => weeklyLimits_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDayPlayPVE
	{
		get
		{
			return isDayPlayPVE_;
		}
		set
		{
			isDayPlayPVE_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, bool> CampaignPass => campaignPass_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayAmountInfo PayAmountInfo
	{
		get
		{
			return payAmountInfo_;
		}
		set
		{
			payAmountInfo_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, LightGift> LightGift => lightGift_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<string, int> PayStarDisc => payStarDisc_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WinCount
	{
		get
		{
			return winCount_;
		}
		set
		{
			winCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UnLockDifficulty
	{
		get
		{
			return unLockDifficulty_;
		}
		set
		{
			unLockDifficulty_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<ItemEtc> RecoupBagItems => recoupBagItems_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDayPlayPVP
	{
		get
		{
			return isDayPlayPVP_;
		}
		set
		{
			isDayPlayPVP_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DailyPraiseCount
	{
		get
		{
			return dailyPraiseCount_;
		}
		set
		{
			dailyPraiseCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> MapModeCount => mapModeCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> WinMap => winMap_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> MapModeWinCount => mapModeWinCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerDataByte()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerDataByte(SqlPlayerDataByte other)
		: this()
	{
		bagItems_ = other.bagItems_.Clone();
		gachaCount_ = other.gachaCount_.Clone();
		fashionPlan_ = other.fashionPlan_.Clone();
		roleCard_ = other.roleCard_.Clone();
		weeklyLimits_ = other.weeklyLimits_.Clone();
		isDayPlayPVE_ = other.isDayPlayPVE_;
		campaignPass_ = other.campaignPass_.Clone();
		payAmountInfo_ = ((other.payAmountInfo_ != null) ? other.payAmountInfo_.Clone() : null);
		lightGift_ = other.lightGift_.Clone();
		payStarDisc_ = other.payStarDisc_.Clone();
		winCount_ = other.winCount_;
		unLockDifficulty_ = other.unLockDifficulty_;
		recoupBagItems_ = other.recoupBagItems_.Clone();
		isDayPlayPVP_ = other.isDayPlayPVP_;
		dailyPraiseCount_ = other.dailyPraiseCount_;
		mapModeCount_ = other.mapModeCount_.Clone();
		winMap_ = other.winMap_.Clone();
		mapModeWinCount_ = other.mapModeWinCount_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SqlPlayerDataByte Clone()
	{
		return new SqlPlayerDataByte(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SqlPlayerDataByte);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SqlPlayerDataByte other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!bagItems_.Equals(other.bagItems_))
		{
			return false;
		}
		if (!GachaCount.Equals(other.GachaCount))
		{
			return false;
		}
		if (!fashionPlan_.Equals(other.fashionPlan_))
		{
			return false;
		}
		if (!RoleCard.Equals(other.RoleCard))
		{
			return false;
		}
		if (!WeeklyLimits.Equals(other.WeeklyLimits))
		{
			return false;
		}
		if (IsDayPlayPVE != other.IsDayPlayPVE)
		{
			return false;
		}
		if (!CampaignPass.Equals(other.CampaignPass))
		{
			return false;
		}
		if (!object.Equals(PayAmountInfo, other.PayAmountInfo))
		{
			return false;
		}
		if (!LightGift.Equals(other.LightGift))
		{
			return false;
		}
		if (!PayStarDisc.Equals(other.PayStarDisc))
		{
			return false;
		}
		if (WinCount != other.WinCount)
		{
			return false;
		}
		if (UnLockDifficulty != other.UnLockDifficulty)
		{
			return false;
		}
		if (!recoupBagItems_.Equals(other.recoupBagItems_))
		{
			return false;
		}
		if (IsDayPlayPVP != other.IsDayPlayPVP)
		{
			return false;
		}
		if (DailyPraiseCount != other.DailyPraiseCount)
		{
			return false;
		}
		if (!MapModeCount.Equals(other.MapModeCount))
		{
			return false;
		}
		if (!WinMap.Equals(other.WinMap))
		{
			return false;
		}
		if (!MapModeWinCount.Equals(other.MapModeWinCount))
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
		num ^= bagItems_.GetHashCode();
		num ^= GachaCount.GetHashCode();
		num ^= fashionPlan_.GetHashCode();
		num ^= RoleCard.GetHashCode();
		num ^= WeeklyLimits.GetHashCode();
		if (IsDayPlayPVE)
		{
			num ^= IsDayPlayPVE.GetHashCode();
		}
		num ^= CampaignPass.GetHashCode();
		if (payAmountInfo_ != null)
		{
			num ^= PayAmountInfo.GetHashCode();
		}
		num ^= LightGift.GetHashCode();
		num ^= PayStarDisc.GetHashCode();
		if (WinCount != 0)
		{
			num ^= WinCount.GetHashCode();
		}
		if (UnLockDifficulty != 0)
		{
			num ^= UnLockDifficulty.GetHashCode();
		}
		num ^= recoupBagItems_.GetHashCode();
		if (IsDayPlayPVP)
		{
			num ^= IsDayPlayPVP.GetHashCode();
		}
		if (DailyPraiseCount != 0)
		{
			num ^= DailyPraiseCount.GetHashCode();
		}
		num ^= MapModeCount.GetHashCode();
		num ^= WinMap.GetHashCode();
		num ^= MapModeWinCount.GetHashCode();
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
		bagItems_.WriteTo(ref output, _repeated_bagItems_codec);
		gachaCount_.WriteTo(ref output, _map_gachaCount_codec);
		fashionPlan_.WriteTo(ref output, _repeated_fashionPlan_codec);
		roleCard_.WriteTo(ref output, _map_roleCard_codec);
		weeklyLimits_.WriteTo(ref output, _map_weeklyLimits_codec);
		if (IsDayPlayPVE)
		{
			output.WriteRawTag(48);
			output.WriteBool(IsDayPlayPVE);
		}
		campaignPass_.WriteTo(ref output, _map_campaignPass_codec);
		if (payAmountInfo_ != null)
		{
			output.WriteRawTag(66);
			output.WriteMessage(PayAmountInfo);
		}
		lightGift_.WriteTo(ref output, _map_lightGift_codec);
		payStarDisc_.WriteTo(ref output, _map_payStarDisc_codec);
		if (WinCount != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(WinCount);
		}
		if (UnLockDifficulty != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(UnLockDifficulty);
		}
		recoupBagItems_.WriteTo(ref output, _repeated_recoupBagItems_codec);
		if (IsDayPlayPVP)
		{
			output.WriteRawTag(112);
			output.WriteBool(IsDayPlayPVP);
		}
		if (DailyPraiseCount != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(DailyPraiseCount);
		}
		mapModeCount_.WriteTo(ref output, _map_mapModeCount_codec);
		winMap_.WriteTo(ref output, _map_winMap_codec);
		mapModeWinCount_.WriteTo(ref output, _map_mapModeWinCount_codec);
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
		num += bagItems_.CalculateSize(_repeated_bagItems_codec);
		num += gachaCount_.CalculateSize(_map_gachaCount_codec);
		num += fashionPlan_.CalculateSize(_repeated_fashionPlan_codec);
		num += roleCard_.CalculateSize(_map_roleCard_codec);
		num += weeklyLimits_.CalculateSize(_map_weeklyLimits_codec);
		if (IsDayPlayPVE)
		{
			num += 2;
		}
		num += campaignPass_.CalculateSize(_map_campaignPass_codec);
		if (payAmountInfo_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(PayAmountInfo);
		}
		num += lightGift_.CalculateSize(_map_lightGift_codec);
		num += payStarDisc_.CalculateSize(_map_payStarDisc_codec);
		if (WinCount != 0)
		{
			num += 5;
		}
		if (UnLockDifficulty != 0)
		{
			num += 5;
		}
		num += recoupBagItems_.CalculateSize(_repeated_recoupBagItems_codec);
		if (IsDayPlayPVP)
		{
			num += 2;
		}
		if (DailyPraiseCount != 0)
		{
			num += 5;
		}
		num += mapModeCount_.CalculateSize(_map_mapModeCount_codec);
		num += winMap_.CalculateSize(_map_winMap_codec);
		num += mapModeWinCount_.CalculateSize(_map_mapModeWinCount_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SqlPlayerDataByte other)
	{
		if (other == null)
		{
			return;
		}
		bagItems_.Add(other.bagItems_);
		gachaCount_.MergeFrom(other.gachaCount_);
		fashionPlan_.Add(other.fashionPlan_);
		roleCard_.MergeFrom(other.roleCard_);
		weeklyLimits_.MergeFrom(other.weeklyLimits_);
		if (other.IsDayPlayPVE)
		{
			IsDayPlayPVE = other.IsDayPlayPVE;
		}
		campaignPass_.MergeFrom(other.campaignPass_);
		if (other.payAmountInfo_ != null)
		{
			if (payAmountInfo_ == null)
			{
				PayAmountInfo = new PayAmountInfo();
			}
			PayAmountInfo.MergeFrom(other.PayAmountInfo);
		}
		lightGift_.MergeFrom(other.lightGift_);
		payStarDisc_.MergeFrom(other.payStarDisc_);
		if (other.WinCount != 0)
		{
			WinCount = other.WinCount;
		}
		if (other.UnLockDifficulty != 0)
		{
			UnLockDifficulty = other.UnLockDifficulty;
		}
		recoupBagItems_.Add(other.recoupBagItems_);
		if (other.IsDayPlayPVP)
		{
			IsDayPlayPVP = other.IsDayPlayPVP;
		}
		if (other.DailyPraiseCount != 0)
		{
			DailyPraiseCount = other.DailyPraiseCount;
		}
		mapModeCount_.MergeFrom(other.mapModeCount_);
		winMap_.MergeFrom(other.winMap_);
		mapModeWinCount_.MergeFrom(other.mapModeWinCount_);
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
				bagItems_.AddEntriesFrom(ref input, _repeated_bagItems_codec);
				break;
			case 18u:
				gachaCount_.AddEntriesFrom(ref input, _map_gachaCount_codec);
				break;
			case 26u:
				fashionPlan_.AddEntriesFrom(ref input, _repeated_fashionPlan_codec);
				break;
			case 34u:
				roleCard_.AddEntriesFrom(ref input, _map_roleCard_codec);
				break;
			case 42u:
				weeklyLimits_.AddEntriesFrom(ref input, _map_weeklyLimits_codec);
				break;
			case 48u:
				IsDayPlayPVE = input.ReadBool();
				break;
			case 58u:
				campaignPass_.AddEntriesFrom(ref input, _map_campaignPass_codec);
				break;
			case 66u:
				if (payAmountInfo_ == null)
				{
					PayAmountInfo = new PayAmountInfo();
				}
				input.ReadMessage(PayAmountInfo);
				break;
			case 74u:
				lightGift_.AddEntriesFrom(ref input, _map_lightGift_codec);
				break;
			case 82u:
				payStarDisc_.AddEntriesFrom(ref input, _map_payStarDisc_codec);
				break;
			case 93u:
				WinCount = input.ReadSFixed32();
				break;
			case 101u:
				UnLockDifficulty = input.ReadSFixed32();
				break;
			case 106u:
				recoupBagItems_.AddEntriesFrom(ref input, _repeated_recoupBagItems_codec);
				break;
			case 112u:
				IsDayPlayPVP = input.ReadBool();
				break;
			case 125u:
				DailyPraiseCount = input.ReadSFixed32();
				break;
			case 130u:
				mapModeCount_.AddEntriesFrom(ref input, _map_mapModeCount_codec);
				break;
			case 138u:
				winMap_.AddEntriesFrom(ref input, _map_winMap_codec);
				break;
			case 146u:
				mapModeWinCount_.AddEntriesFrom(ref input, _map_mapModeWinCount_codec);
				break;
			}
		}
	}
}
