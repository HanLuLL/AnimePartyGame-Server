using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using UnityEngine;

public sealed class BattlePassInfoConfigure : IMessage<BattlePassInfoConfigure>, IMessage, IEquatable<BattlePassInfoConfigure>, IDeepCloneable<BattlePassInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<BattlePassInfoConfigure> _parser = new MessageParser<BattlePassInfoConfigure>(() => new BattlePassInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int BeginTimeFieldNumber = 2;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 3;

	private Timestamp endTime_;

	public const int TaskGroupIDFieldNumber = 4;

	private int taskGroupID_;

	public const int RewardGroupIDFieldNumber = 5;

	private int rewardGroupID_;

	public const int RewardAdsGroupIDFieldNumber = 6;

	private int rewardAdsGroupID_;

	public const int RewardDetailWindowGroupIDFieldNumber = 7;

	private int rewardDetailWindowGroupID_;

	public const int ChooseRoleEffectFieldNumber = 8;

	private string chooseRoleEffect_ = "";

	public const int PremiumOfferLevelsFieldNumber = 9;

	private int premiumOfferLevels_;

	public const int CostPerLvFieldNumber = 10;

	private static readonly MapField<int, int>.Codec _map_costPerLv_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<int, int> costPerLv_ = new MapField<int, int>();

	public const int ExpPerLvFieldNumber = 11;

	private int expPerLv_;

	public const int RewardPerLvAfterMaxFieldNumber = 12;

	private static readonly MapField<int, int>.Codec _map_rewardPerLvAfterMax_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 98u);

	private readonly MapField<int, int> rewardPerLvAfterMax_ = new MapField<int, int>();

	public const int TopicFieldNumber = 13;

	private int topic_;

	public const int NormalNameFieldNumber = 14;

	private int normalName_;

	public const int PremiumNameFieldNumber = 15;

	private int premiumName_;

	public const int BattlePassIconFieldNumber = 16;

	private string battlePassIcon_ = "";

	public const int SkinIDFieldNumber = 17;

	private int skinID_;

	public const int AccountBackgroundIDFieldNumber = 18;

	private int accountBackgroundID_;

	public const int MainBGFieldNumber = 19;

	private string mainBG_ = "";

	public const int ButtonTextureFieldNumber = 20;

	private string buttonTexture_ = "";

	public const int TaskBGFieldNumber = 21;

	private string taskBG_ = "";

	public const int EntranceFoldFieldNumber = 22;

	private string entranceFold_ = "";

	public const int EntranceUnfoldFieldNumber = 23;

	private string entranceUnfold_ = "";

	private BattlePassTaskConfigure _BattlePassTaskConfig;

	private BattlePassRewardConfigure _BattlePassRewardConfig;

	private BattlePassRewardAdsConfigure _BattlePassRewardAdsConfig;

	private BattlePassRewardDetailConfigure _BattlePassRewardDetailConfig;

	private BattlePassGoodsConfigure _BattlePassGoodsConfig;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassReflection.Descriptor.MessageTypes[0];

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
	public Timestamp BeginTime
	{
		get
		{
			return beginTime_;
		}
		private set
		{
			beginTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndTime
	{
		get
		{
			return endTime_;
		}
		private set
		{
			endTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TaskGroupID
	{
		get
		{
			return taskGroupID_;
		}
		private set
		{
			taskGroupID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RewardGroupID
	{
		get
		{
			return rewardGroupID_;
		}
		private set
		{
			rewardGroupID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RewardAdsGroupID
	{
		get
		{
			return rewardAdsGroupID_;
		}
		private set
		{
			rewardAdsGroupID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RewardDetailWindowGroupID
	{
		get
		{
			return rewardDetailWindowGroupID_;
		}
		private set
		{
			rewardDetailWindowGroupID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ChooseRoleEffect
	{
		get
		{
			return chooseRoleEffect_;
		}
		private set
		{
			chooseRoleEffect_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PremiumOfferLevels
	{
		get
		{
			return premiumOfferLevels_;
		}
		private set
		{
			premiumOfferLevels_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> CostPerLv => costPerLv_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ExpPerLv
	{
		get
		{
			return expPerLv_;
		}
		private set
		{
			expPerLv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> RewardPerLvAfterMax => rewardPerLvAfterMax_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Topic
	{
		get
		{
			return topic_;
		}
		private set
		{
			topic_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NormalName
	{
		get
		{
			return normalName_;
		}
		private set
		{
			normalName_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PremiumName
	{
		get
		{
			return premiumName_;
		}
		private set
		{
			premiumName_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BattlePassIcon
	{
		get
		{
			return battlePassIcon_;
		}
		private set
		{
			battlePassIcon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkinID
	{
		get
		{
			return skinID_;
		}
		private set
		{
			skinID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AccountBackgroundID
	{
		get
		{
			return accountBackgroundID_;
		}
		private set
		{
			accountBackgroundID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string MainBG
	{
		get
		{
			return mainBG_;
		}
		private set
		{
			mainBG_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ButtonTexture
	{
		get
		{
			return buttonTexture_;
		}
		private set
		{
			buttonTexture_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string TaskBG
	{
		get
		{
			return taskBG_;
		}
		private set
		{
			taskBG_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string EntranceFold
	{
		get
		{
			return entranceFold_;
		}
		private set
		{
			entranceFold_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string EntranceUnfold
	{
		get
		{
			return entranceUnfold_;
		}
		private set
		{
			entranceUnfold_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	public BattlePassTaskConfigure BattlePassTaskConfig
	{
		get
		{
			if (_BattlePassTaskConfig == null && !StaticConfigure.BattlePass.TaskDict.TryGetValue(TaskGroupID, out _BattlePassTaskConfig))
			{
				Debug.LogError("在SBattlePass.Task表里并没有找到任务配置：" + TaskGroupID);
			}
			return _BattlePassTaskConfig;
		}
	}

	public BattlePassRewardConfigure BattlePassRewardConfig
	{
		get
		{
			if (_BattlePassRewardConfig == null && !StaticConfigure.BattlePass.RewardDict.TryGetValue(RewardGroupID, out _BattlePassRewardConfig))
			{
				Debug.LogError("在SBattlePass.Reward表里并没有找到奖励配置：" + RewardGroupID);
			}
			return _BattlePassRewardConfig;
		}
	}

	public BattlePassRewardAdsConfigure BattlePassRewardAdsConfig
	{
		get
		{
			if (_BattlePassRewardAdsConfig == null && !StaticConfigure.BattlePass.RewardAdsDict.TryGetValue(RewardAdsGroupID, out _BattlePassRewardAdsConfig))
			{
				Debug.LogError("在SBattlePass.RewardAds表里并没有找到奖励配置：" + RewardAdsGroupID);
			}
			return _BattlePassRewardAdsConfig;
		}
	}

	public BattlePassRewardDetailConfigure BattlePassRewardDetailConfig
	{
		get
		{
			if (_BattlePassRewardDetailConfig == null && !StaticConfigure.BattlePass.RewardDetailDict.TryGetValue(RewardDetailWindowGroupID, out _BattlePassRewardDetailConfig))
			{
				Debug.LogError("在SBattlePass.RewardDetailWindow表里并没有找到奖励配置：" + RewardDetailWindowGroupID);
			}
			return _BattlePassRewardDetailConfig;
		}
	}

	public BattlePassGoodsConfigure BattlePassGoodsConfig
	{
		get
		{
			if (_BattlePassGoodsConfig == null && !StaticConfigure.BattlePass.GoodsDict.TryGetValue(1, out _BattlePassGoodsConfig))
			{
				Debug.LogError("在BattlePass.GoodsDict表里并没有找到商品配置：" + 1);
			}
			return _BattlePassGoodsConfig;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassInfoConfigure(BattlePassInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		taskGroupID_ = other.taskGroupID_;
		rewardGroupID_ = other.rewardGroupID_;
		rewardAdsGroupID_ = other.rewardAdsGroupID_;
		rewardDetailWindowGroupID_ = other.rewardDetailWindowGroupID_;
		chooseRoleEffect_ = other.chooseRoleEffect_;
		premiumOfferLevels_ = other.premiumOfferLevels_;
		costPerLv_ = other.costPerLv_.Clone();
		expPerLv_ = other.expPerLv_;
		rewardPerLvAfterMax_ = other.rewardPerLvAfterMax_.Clone();
		topic_ = other.topic_;
		normalName_ = other.normalName_;
		premiumName_ = other.premiumName_;
		battlePassIcon_ = other.battlePassIcon_;
		skinID_ = other.skinID_;
		accountBackgroundID_ = other.accountBackgroundID_;
		mainBG_ = other.mainBG_;
		buttonTexture_ = other.buttonTexture_;
		taskBG_ = other.taskBG_;
		entranceFold_ = other.entranceFold_;
		entranceUnfold_ = other.entranceUnfold_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassInfoConfigure Clone()
	{
		return new BattlePassInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassInfoConfigure other)
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
		if (!object.Equals(BeginTime, other.BeginTime))
		{
			return false;
		}
		if (!object.Equals(EndTime, other.EndTime))
		{
			return false;
		}
		if (TaskGroupID != other.TaskGroupID)
		{
			return false;
		}
		if (RewardGroupID != other.RewardGroupID)
		{
			return false;
		}
		if (RewardAdsGroupID != other.RewardAdsGroupID)
		{
			return false;
		}
		if (RewardDetailWindowGroupID != other.RewardDetailWindowGroupID)
		{
			return false;
		}
		if (ChooseRoleEffect != other.ChooseRoleEffect)
		{
			return false;
		}
		if (PremiumOfferLevels != other.PremiumOfferLevels)
		{
			return false;
		}
		if (!CostPerLv.Equals(other.CostPerLv))
		{
			return false;
		}
		if (ExpPerLv != other.ExpPerLv)
		{
			return false;
		}
		if (!RewardPerLvAfterMax.Equals(other.RewardPerLvAfterMax))
		{
			return false;
		}
		if (Topic != other.Topic)
		{
			return false;
		}
		if (NormalName != other.NormalName)
		{
			return false;
		}
		if (PremiumName != other.PremiumName)
		{
			return false;
		}
		if (BattlePassIcon != other.BattlePassIcon)
		{
			return false;
		}
		if (SkinID != other.SkinID)
		{
			return false;
		}
		if (AccountBackgroundID != other.AccountBackgroundID)
		{
			return false;
		}
		if (MainBG != other.MainBG)
		{
			return false;
		}
		if (ButtonTexture != other.ButtonTexture)
		{
			return false;
		}
		if (TaskBG != other.TaskBG)
		{
			return false;
		}
		if (EntranceFold != other.EntranceFold)
		{
			return false;
		}
		if (EntranceUnfold != other.EntranceUnfold)
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
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (TaskGroupID != 0)
		{
			num ^= TaskGroupID.GetHashCode();
		}
		if (RewardGroupID != 0)
		{
			num ^= RewardGroupID.GetHashCode();
		}
		if (RewardAdsGroupID != 0)
		{
			num ^= RewardAdsGroupID.GetHashCode();
		}
		if (RewardDetailWindowGroupID != 0)
		{
			num ^= RewardDetailWindowGroupID.GetHashCode();
		}
		if (ChooseRoleEffect.Length != 0)
		{
			num ^= ChooseRoleEffect.GetHashCode();
		}
		if (PremiumOfferLevels != 0)
		{
			num ^= PremiumOfferLevels.GetHashCode();
		}
		num ^= CostPerLv.GetHashCode();
		if (ExpPerLv != 0)
		{
			num ^= ExpPerLv.GetHashCode();
		}
		num ^= RewardPerLvAfterMax.GetHashCode();
		if (Topic != 0)
		{
			num ^= Topic.GetHashCode();
		}
		if (NormalName != 0)
		{
			num ^= NormalName.GetHashCode();
		}
		if (PremiumName != 0)
		{
			num ^= PremiumName.GetHashCode();
		}
		if (BattlePassIcon.Length != 0)
		{
			num ^= BattlePassIcon.GetHashCode();
		}
		if (SkinID != 0)
		{
			num ^= SkinID.GetHashCode();
		}
		if (AccountBackgroundID != 0)
		{
			num ^= AccountBackgroundID.GetHashCode();
		}
		if (MainBG.Length != 0)
		{
			num ^= MainBG.GetHashCode();
		}
		if (ButtonTexture.Length != 0)
		{
			num ^= ButtonTexture.GetHashCode();
		}
		if (TaskBG.Length != 0)
		{
			num ^= TaskBG.GetHashCode();
		}
		if (EntranceFold.Length != 0)
		{
			num ^= EntranceFold.GetHashCode();
		}
		if (EntranceUnfold.Length != 0)
		{
			num ^= EntranceUnfold.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(EndTime);
		}
		if (TaskGroupID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(TaskGroupID);
		}
		if (RewardGroupID != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(RewardGroupID);
		}
		if (RewardAdsGroupID != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(RewardAdsGroupID);
		}
		if (RewardDetailWindowGroupID != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(RewardDetailWindowGroupID);
		}
		if (ChooseRoleEffect.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(ChooseRoleEffect);
		}
		if (PremiumOfferLevels != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(PremiumOfferLevels);
		}
		costPerLv_.WriteTo(ref output, _map_costPerLv_codec);
		if (ExpPerLv != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(ExpPerLv);
		}
		rewardPerLvAfterMax_.WriteTo(ref output, _map_rewardPerLvAfterMax_codec);
		if (Topic != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(Topic);
		}
		if (NormalName != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(NormalName);
		}
		if (PremiumName != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(PremiumName);
		}
		if (BattlePassIcon.Length != 0)
		{
			output.WriteRawTag(130, 1);
			output.WriteString(BattlePassIcon);
		}
		if (SkinID != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(SkinID);
		}
		if (AccountBackgroundID != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(AccountBackgroundID);
		}
		if (MainBG.Length != 0)
		{
			output.WriteRawTag(154, 1);
			output.WriteString(MainBG);
		}
		if (ButtonTexture.Length != 0)
		{
			output.WriteRawTag(162, 1);
			output.WriteString(ButtonTexture);
		}
		if (TaskBG.Length != 0)
		{
			output.WriteRawTag(170, 1);
			output.WriteString(TaskBG);
		}
		if (EntranceFold.Length != 0)
		{
			output.WriteRawTag(178, 1);
			output.WriteString(EntranceFold);
		}
		if (EntranceUnfold.Length != 0)
		{
			output.WriteRawTag(186, 1);
			output.WriteString(EntranceUnfold);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (TaskGroupID != 0)
		{
			num += 5;
		}
		if (RewardGroupID != 0)
		{
			num += 5;
		}
		if (RewardAdsGroupID != 0)
		{
			num += 5;
		}
		if (RewardDetailWindowGroupID != 0)
		{
			num += 5;
		}
		if (ChooseRoleEffect.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ChooseRoleEffect);
		}
		if (PremiumOfferLevels != 0)
		{
			num += 5;
		}
		num += costPerLv_.CalculateSize(_map_costPerLv_codec);
		if (ExpPerLv != 0)
		{
			num += 5;
		}
		num += rewardPerLvAfterMax_.CalculateSize(_map_rewardPerLvAfterMax_codec);
		if (Topic != 0)
		{
			num += 5;
		}
		if (NormalName != 0)
		{
			num += 5;
		}
		if (PremiumName != 0)
		{
			num += 5;
		}
		if (BattlePassIcon.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BattlePassIcon);
		}
		if (SkinID != 0)
		{
			num += 6;
		}
		if (AccountBackgroundID != 0)
		{
			num += 6;
		}
		if (MainBG.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(MainBG);
		}
		if (ButtonTexture.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(ButtonTexture);
		}
		if (TaskBG.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(TaskBG);
		}
		if (EntranceFold.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(EntranceFold);
		}
		if (EntranceUnfold.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(EntranceUnfold);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattlePassInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
		if (other.beginTime_ != null)
		{
			if (beginTime_ == null)
			{
				BeginTime = new Timestamp();
			}
			BeginTime.MergeFrom(other.BeginTime);
		}
		if (other.endTime_ != null)
		{
			if (endTime_ == null)
			{
				EndTime = new Timestamp();
			}
			EndTime.MergeFrom(other.EndTime);
		}
		if (other.TaskGroupID != 0)
		{
			TaskGroupID = other.TaskGroupID;
		}
		if (other.RewardGroupID != 0)
		{
			RewardGroupID = other.RewardGroupID;
		}
		if (other.RewardAdsGroupID != 0)
		{
			RewardAdsGroupID = other.RewardAdsGroupID;
		}
		if (other.RewardDetailWindowGroupID != 0)
		{
			RewardDetailWindowGroupID = other.RewardDetailWindowGroupID;
		}
		if (other.ChooseRoleEffect.Length != 0)
		{
			ChooseRoleEffect = other.ChooseRoleEffect;
		}
		if (other.PremiumOfferLevels != 0)
		{
			PremiumOfferLevels = other.PremiumOfferLevels;
		}
		costPerLv_.MergeFrom(other.costPerLv_);
		if (other.ExpPerLv != 0)
		{
			ExpPerLv = other.ExpPerLv;
		}
		rewardPerLvAfterMax_.MergeFrom(other.rewardPerLvAfterMax_);
		if (other.Topic != 0)
		{
			Topic = other.Topic;
		}
		if (other.NormalName != 0)
		{
			NormalName = other.NormalName;
		}
		if (other.PremiumName != 0)
		{
			PremiumName = other.PremiumName;
		}
		if (other.BattlePassIcon.Length != 0)
		{
			BattlePassIcon = other.BattlePassIcon;
		}
		if (other.SkinID != 0)
		{
			SkinID = other.SkinID;
		}
		if (other.AccountBackgroundID != 0)
		{
			AccountBackgroundID = other.AccountBackgroundID;
		}
		if (other.MainBG.Length != 0)
		{
			MainBG = other.MainBG;
		}
		if (other.ButtonTexture.Length != 0)
		{
			ButtonTexture = other.ButtonTexture;
		}
		if (other.TaskBG.Length != 0)
		{
			TaskBG = other.TaskBG;
		}
		if (other.EntranceFold.Length != 0)
		{
			EntranceFold = other.EntranceFold;
		}
		if (other.EntranceUnfold.Length != 0)
		{
			EntranceUnfold = other.EntranceUnfold;
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
			case 13u:
				Id = input.ReadSFixed32();
				break;
			case 18u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 26u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 37u:
				TaskGroupID = input.ReadSFixed32();
				break;
			case 45u:
				RewardGroupID = input.ReadSFixed32();
				break;
			case 53u:
				RewardAdsGroupID = input.ReadSFixed32();
				break;
			case 61u:
				RewardDetailWindowGroupID = input.ReadSFixed32();
				break;
			case 66u:
				ChooseRoleEffect = input.ReadString();
				break;
			case 77u:
				PremiumOfferLevels = input.ReadSFixed32();
				break;
			case 82u:
				costPerLv_.AddEntriesFrom(ref input, _map_costPerLv_codec);
				break;
			case 93u:
				ExpPerLv = input.ReadSFixed32();
				break;
			case 98u:
				rewardPerLvAfterMax_.AddEntriesFrom(ref input, _map_rewardPerLvAfterMax_codec);
				break;
			case 109u:
				Topic = input.ReadSFixed32();
				break;
			case 117u:
				NormalName = input.ReadSFixed32();
				break;
			case 125u:
				PremiumName = input.ReadSFixed32();
				break;
			case 130u:
				BattlePassIcon = input.ReadString();
				break;
			case 141u:
				SkinID = input.ReadSFixed32();
				break;
			case 149u:
				AccountBackgroundID = input.ReadSFixed32();
				break;
			case 154u:
				MainBG = input.ReadString();
				break;
			case 162u:
				ButtonTexture = input.ReadString();
				break;
			case 170u:
				TaskBG = input.ReadString();
				break;
			case 178u:
				EntranceFold = input.ReadString();
				break;
			case 186u:
				EntranceUnfold = input.ReadString();
				break;
			}
		}
	}

	public void FixTime(FixBattlePassInfoConfigure data)
	{
		beginTime_ = data.BeginTime;
		endTime_ = data.EndTime;
	}
}
