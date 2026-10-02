using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using UnityEngine;

public sealed class ActivityInfoConfigure : IMessage<ActivityInfoConfigure>, IMessage, IEquatable<ActivityInfoConfigure>, IDeepCloneable<ActivityInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<ActivityInfoConfigure> _parser = new MessageParser<ActivityInfoConfigure>(() => new ActivityInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int UiTypeFieldNumber = 2;

	private UIType uiType_;

	public const int PanelTypeFieldNumber = 3;

	private UIPanelType panelType_;

	public const int UiTabFieldNumber = 4;

	private int uiTab_;

	public const int CurrencyBarFieldNumber = 5;

	private int currencyBar_;

	public const int TitleImagesFieldNumber = 6;

	private static readonly FieldCodec<string> _repeated_titleImages_codec = FieldCodec.ForString(50u);

	private readonly RepeatedField<string> titleImages_ = new RepeatedField<string>();

	public const int BgListFieldNumber = 7;

	private static readonly FieldCodec<string> _repeated_bgList_codec = FieldCodec.ForString(58u);

	private readonly RepeatedField<string> bgList_ = new RepeatedField<string>();

	public const int ActivityImageFieldNumber = 8;

	private string activityImage_ = "";

	public const int ActivityImageSFWFieldNumber = 9;

	private string activityImageSFW_ = "";

	public const int NameIDFieldNumber = 10;

	private int nameID_;

	public const int TitleIDFieldNumber = 11;

	private int titleID_;

	public const int DescriptionIDFieldNumber = 12;

	private int descriptionID_;

	public const int BeginTimeFieldNumber = 13;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 14;

	private Timestamp endTime_;

	public const int TaskIDsFieldNumber = 15;

	private static readonly FieldCodec<int> _repeated_taskIDs_codec = FieldCodec.ForSFixed32(122u);

	private readonly RepeatedField<int> taskIDs_ = new RepeatedField<int>();

	public const int MissionIDsFieldNumber = 16;

	private static readonly FieldCodec<int> _repeated_missionIDs_codec = FieldCodec.ForSFixed32(130u);

	private readonly RepeatedField<int> missionIDs_ = new RepeatedField<int>();

	public const int ExpBonusFieldNumber = 17;

	private float expBonus_;

	public const int StarcoinBonusFieldNumber = 18;

	private float starcoinBonus_;

	public const int GachaIDFieldNumber = 19;

	private int gachaID_;

	public const int ShopTabTypeFieldNumber = 20;

	private ShopTabType shopTabType_;

	public const int TaskTargetIDFieldNumber = 21;

	private int taskTargetID_;

	public const int BGMConfigIDFieldNumber = 22;

	private int bGMConfigID_;

	public const int TaskTargetNumbFieldNumber = 23;

	private int taskTargetNumb_;

	public const int SuperRewardFieldNumber = 24;

	private int superReward_;

	public const int SwitchActivityFieldNumber = 25;

	private int switchActivity_;

	private ActivityGachaConfigure _GachaConfig;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ActivityInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ActivityReflection.Descriptor.MessageTypes[1];

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
	public UIType UiType
	{
		get
		{
			return uiType_;
		}
		private set
		{
			uiType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UIPanelType PanelType
	{
		get
		{
			return panelType_;
		}
		private set
		{
			panelType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UiTab
	{
		get
		{
			return uiTab_;
		}
		private set
		{
			uiTab_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrencyBar
	{
		get
		{
			return currencyBar_;
		}
		private set
		{
			currencyBar_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> TitleImages => titleImages_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<string> BgList => bgList_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ActivityImage
	{
		get
		{
			return activityImage_;
		}
		private set
		{
			activityImage_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ActivityImageSFW
	{
		get
		{
			return activityImageSFW_;
		}
		private set
		{
			activityImageSFW_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public int TitleID
	{
		get
		{
			return titleID_;
		}
		private set
		{
			titleID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DescriptionID
	{
		get
		{
			return descriptionID_;
		}
		private set
		{
			descriptionID_ = value;
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
	public RepeatedField<int> TaskIDs => taskIDs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MissionIDs => missionIDs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float ExpBonus
	{
		get
		{
			return expBonus_;
		}
		private set
		{
			expBonus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float StarcoinBonus
	{
		get
		{
			return starcoinBonus_;
		}
		private set
		{
			starcoinBonus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GachaID
	{
		get
		{
			return gachaID_;
		}
		private set
		{
			gachaID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopTabType ShopTabType
	{
		get
		{
			return shopTabType_;
		}
		private set
		{
			shopTabType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TaskTargetID
	{
		get
		{
			return taskTargetID_;
		}
		private set
		{
			taskTargetID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BGMConfigID
	{
		get
		{
			return bGMConfigID_;
		}
		private set
		{
			bGMConfigID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TaskTargetNumb
	{
		get
		{
			return taskTargetNumb_;
		}
		private set
		{
			taskTargetNumb_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SuperReward
	{
		get
		{
			return superReward_;
		}
		private set
		{
			superReward_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SwitchActivity
	{
		get
		{
			return switchActivity_;
		}
		private set
		{
			switchActivity_ = value;
		}
	}

	public ActivityGachaConfigure GachaConfig
	{
		get
		{
			if (_GachaConfig == null && !StaticConfigure.Activity.GachaDict.TryGetValue(GachaID, out _GachaConfig))
			{
				Debug.LogError($"无法从Activity.GachaDict中取出ID:{GachaID}的数据");
			}
			return _GachaConfig;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityInfoConfigure(ActivityInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		uiType_ = other.uiType_;
		panelType_ = other.panelType_;
		uiTab_ = other.uiTab_;
		currencyBar_ = other.currencyBar_;
		titleImages_ = other.titleImages_.Clone();
		bgList_ = other.bgList_.Clone();
		activityImage_ = other.activityImage_;
		activityImageSFW_ = other.activityImageSFW_;
		nameID_ = other.nameID_;
		titleID_ = other.titleID_;
		descriptionID_ = other.descriptionID_;
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		taskIDs_ = other.taskIDs_.Clone();
		missionIDs_ = other.missionIDs_.Clone();
		expBonus_ = other.expBonus_;
		starcoinBonus_ = other.starcoinBonus_;
		gachaID_ = other.gachaID_;
		shopTabType_ = other.shopTabType_;
		taskTargetID_ = other.taskTargetID_;
		bGMConfigID_ = other.bGMConfigID_;
		taskTargetNumb_ = other.taskTargetNumb_;
		superReward_ = other.superReward_;
		switchActivity_ = other.switchActivity_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityInfoConfigure Clone()
	{
		return new ActivityInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ActivityInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ActivityInfoConfigure other)
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
		if (UiType != other.UiType)
		{
			return false;
		}
		if (PanelType != other.PanelType)
		{
			return false;
		}
		if (UiTab != other.UiTab)
		{
			return false;
		}
		if (CurrencyBar != other.CurrencyBar)
		{
			return false;
		}
		if (!titleImages_.Equals(other.titleImages_))
		{
			return false;
		}
		if (!bgList_.Equals(other.bgList_))
		{
			return false;
		}
		if (ActivityImage != other.ActivityImage)
		{
			return false;
		}
		if (ActivityImageSFW != other.ActivityImageSFW)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (TitleID != other.TitleID)
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
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
		if (!taskIDs_.Equals(other.taskIDs_))
		{
			return false;
		}
		if (!missionIDs_.Equals(other.missionIDs_))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(ExpBonus, other.ExpBonus))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(StarcoinBonus, other.StarcoinBonus))
		{
			return false;
		}
		if (GachaID != other.GachaID)
		{
			return false;
		}
		if (ShopTabType != other.ShopTabType)
		{
			return false;
		}
		if (TaskTargetID != other.TaskTargetID)
		{
			return false;
		}
		if (BGMConfigID != other.BGMConfigID)
		{
			return false;
		}
		if (TaskTargetNumb != other.TaskTargetNumb)
		{
			return false;
		}
		if (SuperReward != other.SuperReward)
		{
			return false;
		}
		if (SwitchActivity != other.SwitchActivity)
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
		if (UiType != UIType.None)
		{
			num ^= UiType.GetHashCode();
		}
		if (PanelType != UIPanelType.None)
		{
			num ^= PanelType.GetHashCode();
		}
		if (UiTab != 0)
		{
			num ^= UiTab.GetHashCode();
		}
		if (CurrencyBar != 0)
		{
			num ^= CurrencyBar.GetHashCode();
		}
		num ^= titleImages_.GetHashCode();
		num ^= bgList_.GetHashCode();
		if (ActivityImage.Length != 0)
		{
			num ^= ActivityImage.GetHashCode();
		}
		if (ActivityImageSFW.Length != 0)
		{
			num ^= ActivityImageSFW.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (TitleID != 0)
		{
			num ^= TitleID.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		num ^= taskIDs_.GetHashCode();
		num ^= missionIDs_.GetHashCode();
		if (ExpBonus != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(ExpBonus);
		}
		if (StarcoinBonus != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(StarcoinBonus);
		}
		if (GachaID != 0)
		{
			num ^= GachaID.GetHashCode();
		}
		if (ShopTabType != ShopTabType.None)
		{
			num ^= ShopTabType.GetHashCode();
		}
		if (TaskTargetID != 0)
		{
			num ^= TaskTargetID.GetHashCode();
		}
		if (BGMConfigID != 0)
		{
			num ^= BGMConfigID.GetHashCode();
		}
		if (TaskTargetNumb != 0)
		{
			num ^= TaskTargetNumb.GetHashCode();
		}
		if (SuperReward != 0)
		{
			num ^= SuperReward.GetHashCode();
		}
		if (SwitchActivity != 0)
		{
			num ^= SwitchActivity.GetHashCode();
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
		if (UiType != UIType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)UiType);
		}
		if (PanelType != UIPanelType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)PanelType);
		}
		if (UiTab != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(UiTab);
		}
		if (CurrencyBar != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CurrencyBar);
		}
		titleImages_.WriteTo(ref output, _repeated_titleImages_codec);
		bgList_.WriteTo(ref output, _repeated_bgList_codec);
		if (ActivityImage.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(ActivityImage);
		}
		if (ActivityImageSFW.Length != 0)
		{
			output.WriteRawTag(74);
			output.WriteString(ActivityImageSFW);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(NameID);
		}
		if (TitleID != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(TitleID);
		}
		if (DescriptionID != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(DescriptionID);
		}
		if (beginTime_ != null)
		{
			output.WriteRawTag(106);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(114);
			output.WriteMessage(EndTime);
		}
		taskIDs_.WriteTo(ref output, _repeated_taskIDs_codec);
		missionIDs_.WriteTo(ref output, _repeated_missionIDs_codec);
		if (ExpBonus != 0f)
		{
			output.WriteRawTag(141, 1);
			output.WriteFloat(ExpBonus);
		}
		if (StarcoinBonus != 0f)
		{
			output.WriteRawTag(149, 1);
			output.WriteFloat(StarcoinBonus);
		}
		if (GachaID != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(GachaID);
		}
		if (ShopTabType != ShopTabType.None)
		{
			output.WriteRawTag(160, 1);
			output.WriteEnum((int)ShopTabType);
		}
		if (TaskTargetID != 0)
		{
			output.WriteRawTag(173, 1);
			output.WriteSFixed32(TaskTargetID);
		}
		if (BGMConfigID != 0)
		{
			output.WriteRawTag(181, 1);
			output.WriteSFixed32(BGMConfigID);
		}
		if (TaskTargetNumb != 0)
		{
			output.WriteRawTag(189, 1);
			output.WriteSFixed32(TaskTargetNumb);
		}
		if (SuperReward != 0)
		{
			output.WriteRawTag(197, 1);
			output.WriteSFixed32(SuperReward);
		}
		if (SwitchActivity != 0)
		{
			output.WriteRawTag(205, 1);
			output.WriteSFixed32(SwitchActivity);
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
		if (UiType != UIType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)UiType);
		}
		if (PanelType != UIPanelType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PanelType);
		}
		if (UiTab != 0)
		{
			num += 5;
		}
		if (CurrencyBar != 0)
		{
			num += 5;
		}
		num += titleImages_.CalculateSize(_repeated_titleImages_codec);
		num += bgList_.CalculateSize(_repeated_bgList_codec);
		if (ActivityImage.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ActivityImage);
		}
		if (ActivityImageSFW.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ActivityImageSFW);
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (TitleID != 0)
		{
			num += 5;
		}
		if (DescriptionID != 0)
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
		num += taskIDs_.CalculateSize(_repeated_taskIDs_codec);
		num += missionIDs_.CalculateSize(_repeated_missionIDs_codec);
		if (ExpBonus != 0f)
		{
			num += 6;
		}
		if (StarcoinBonus != 0f)
		{
			num += 6;
		}
		if (GachaID != 0)
		{
			num += 6;
		}
		if (ShopTabType != ShopTabType.None)
		{
			num += 2 + CodedOutputStream.ComputeEnumSize((int)ShopTabType);
		}
		if (TaskTargetID != 0)
		{
			num += 6;
		}
		if (BGMConfigID != 0)
		{
			num += 6;
		}
		if (TaskTargetNumb != 0)
		{
			num += 6;
		}
		if (SuperReward != 0)
		{
			num += 6;
		}
		if (SwitchActivity != 0)
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
	public void MergeFrom(ActivityInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
		if (other.UiType != UIType.None)
		{
			UiType = other.UiType;
		}
		if (other.PanelType != UIPanelType.None)
		{
			PanelType = other.PanelType;
		}
		if (other.UiTab != 0)
		{
			UiTab = other.UiTab;
		}
		if (other.CurrencyBar != 0)
		{
			CurrencyBar = other.CurrencyBar;
		}
		titleImages_.Add(other.titleImages_);
		bgList_.Add(other.bgList_);
		if (other.ActivityImage.Length != 0)
		{
			ActivityImage = other.ActivityImage;
		}
		if (other.ActivityImageSFW.Length != 0)
		{
			ActivityImageSFW = other.ActivityImageSFW;
		}
		if (other.NameID != 0)
		{
			NameID = other.NameID;
		}
		if (other.TitleID != 0)
		{
			TitleID = other.TitleID;
		}
		if (other.DescriptionID != 0)
		{
			DescriptionID = other.DescriptionID;
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
		taskIDs_.Add(other.taskIDs_);
		missionIDs_.Add(other.missionIDs_);
		if (other.ExpBonus != 0f)
		{
			ExpBonus = other.ExpBonus;
		}
		if (other.StarcoinBonus != 0f)
		{
			StarcoinBonus = other.StarcoinBonus;
		}
		if (other.GachaID != 0)
		{
			GachaID = other.GachaID;
		}
		if (other.ShopTabType != ShopTabType.None)
		{
			ShopTabType = other.ShopTabType;
		}
		if (other.TaskTargetID != 0)
		{
			TaskTargetID = other.TaskTargetID;
		}
		if (other.BGMConfigID != 0)
		{
			BGMConfigID = other.BGMConfigID;
		}
		if (other.TaskTargetNumb != 0)
		{
			TaskTargetNumb = other.TaskTargetNumb;
		}
		if (other.SuperReward != 0)
		{
			SuperReward = other.SuperReward;
		}
		if (other.SwitchActivity != 0)
		{
			SwitchActivity = other.SwitchActivity;
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
			case 16u:
				UiType = (UIType)input.ReadEnum();
				break;
			case 24u:
				PanelType = (UIPanelType)input.ReadEnum();
				break;
			case 37u:
				UiTab = input.ReadSFixed32();
				break;
			case 45u:
				CurrencyBar = input.ReadSFixed32();
				break;
			case 50u:
				titleImages_.AddEntriesFrom(ref input, _repeated_titleImages_codec);
				break;
			case 58u:
				bgList_.AddEntriesFrom(ref input, _repeated_bgList_codec);
				break;
			case 66u:
				ActivityImage = input.ReadString();
				break;
			case 74u:
				ActivityImageSFW = input.ReadString();
				break;
			case 85u:
				NameID = input.ReadSFixed32();
				break;
			case 93u:
				TitleID = input.ReadSFixed32();
				break;
			case 101u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 106u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 114u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 122u:
			case 125u:
				taskIDs_.AddEntriesFrom(ref input, _repeated_taskIDs_codec);
				break;
			case 130u:
			case 133u:
				missionIDs_.AddEntriesFrom(ref input, _repeated_missionIDs_codec);
				break;
			case 141u:
				ExpBonus = input.ReadFloat();
				break;
			case 149u:
				StarcoinBonus = input.ReadFloat();
				break;
			case 157u:
				GachaID = input.ReadSFixed32();
				break;
			case 160u:
				ShopTabType = (ShopTabType)input.ReadEnum();
				break;
			case 173u:
				TaskTargetID = input.ReadSFixed32();
				break;
			case 181u:
				BGMConfigID = input.ReadSFixed32();
				break;
			case 189u:
				TaskTargetNumb = input.ReadSFixed32();
				break;
			case 197u:
				SuperReward = input.ReadSFixed32();
				break;
			case 205u:
				SwitchActivity = input.ReadSFixed32();
				break;
			}
		}
	}
}
