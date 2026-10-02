using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Core;
using Core.Net;
using GameLogic;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using Tools;

public sealed class ItemInfoConfigure : IMessage<ItemInfoConfigure>, IMessage, IEquatable<ItemInfoConfigure>, IDeepCloneable<ItemInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<ItemInfoConfigure> _parser = new MessageParser<ItemInfoConfigure>(() => new ItemInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int ItemTypeFieldNumber = 2;

	private ItemType itemType_;

	public const int SubMeterIDFieldNumber = 3;

	private int subMeterID_;

	public const int IconFieldNumber = 4;

	private string icon_ = "";

	public const int IconSfwFieldNumber = 5;

	private string iconSfw_ = "";

	public const int IconENFieldNumber = 6;

	private string iconEN_ = "";

	public const int IconJPFieldNumber = 7;

	private string iconJP_ = "";

	public const int IconTCFieldNumber = 8;

	private string iconTC_ = "";

	public const int NameIDFieldNumber = 9;

	private int nameID_;

	public const int DescriptionIDFieldNumber = 10;

	private int descriptionID_;

	public const int QualityTypeFieldNumber = 11;

	private QualityType qualityType_;

	public const int IsAutoTransformFieldNumber = 12;

	private bool isAutoTransform_;

	public const int TransformFieldNumber = 13;

	private static readonly MapField<int, int>.Codec _map_transform_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 106u);

	private readonly MapField<int, int> transform_ = new MapField<int, int>();

	public const int EndDateTimeFieldNumber = 14;

	private Timestamp endDateTime_;

	public const int OuttimeTransformFieldNumber = 15;

	private static readonly MapField<int, int>.Codec _map_outtimeTransform_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 122u);

	private readonly MapField<int, int> outtimeTransform_ = new MapField<int, int>();

	public const int IsClientShowFieldNumber = 16;

	private bool isClientShow_;

	public const int WayListFieldNumber = 17;

	private static readonly FieldCodec<int> _repeated_wayList_codec = FieldCodec.ForSFixed32(138u);

	private readonly RepeatedField<int> wayList_ = new RepeatedField<int>();

	public const int IsAutoOpenFieldNumber = 18;

	private bool isAutoOpen_;

	private bool _isNeedCheck;

	private Timestamp _launchTime;

	private Timestamp _endTime;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ItemInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ItemReflection.Descriptor.MessageTypes[0];

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
	public ItemType ItemType
	{
		get
		{
			return itemType_;
		}
		private set
		{
			itemType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SubMeterID
	{
		get
		{
			return subMeterID_;
		}
		private set
		{
			subMeterID_ = value;
		}
	}

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
	public string IconSfw
	{
		get
		{
			return iconSfw_;
		}
		private set
		{
			iconSfw_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string IconEN
	{
		get
		{
			return iconEN_;
		}
		private set
		{
			iconEN_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string IconJP
	{
		get
		{
			return iconJP_;
		}
		private set
		{
			iconJP_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string IconTC
	{
		get
		{
			return iconTC_;
		}
		private set
		{
			iconTC_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public QualityType QualityType
	{
		get
		{
			return qualityType_;
		}
		private set
		{
			qualityType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsAutoTransform
	{
		get
		{
			return isAutoTransform_;
		}
		private set
		{
			isAutoTransform_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Transform => transform_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Timestamp EndDateTime
	{
		get
		{
			return endDateTime_;
		}
		private set
		{
			endDateTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> OuttimeTransform => outtimeTransform_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsClientShow
	{
		get
		{
			return isClientShow_;
		}
		private set
		{
			isClientShow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> WayList => wayList_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsAutoOpen
	{
		get
		{
			return isAutoOpen_;
		}
		private set
		{
			isAutoOpen_ = value;
		}
	}

	public string ShowIcon
	{
		get
		{
			string cn = ((GameSettings.angelMode && !string.IsNullOrEmpty(IconSfw)) ? IconSfw : Icon);
			string dataForLanguage = GameSettings.GetDataForLanguage(IconEN, IconJP, cn, IconTC);
			if (string.IsNullOrEmpty(dataForLanguage))
			{
				return Icon;
			}
			return dataForLanguage;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemInfoConfigure(ItemInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		itemType_ = other.itemType_;
		subMeterID_ = other.subMeterID_;
		icon_ = other.icon_;
		iconSfw_ = other.iconSfw_;
		iconEN_ = other.iconEN_;
		iconJP_ = other.iconJP_;
		iconTC_ = other.iconTC_;
		nameID_ = other.nameID_;
		descriptionID_ = other.descriptionID_;
		qualityType_ = other.qualityType_;
		isAutoTransform_ = other.isAutoTransform_;
		transform_ = other.transform_.Clone();
		endDateTime_ = ((other.endDateTime_ != null) ? other.endDateTime_.Clone() : null);
		outtimeTransform_ = other.outtimeTransform_.Clone();
		isClientShow_ = other.isClientShow_;
		wayList_ = other.wayList_.Clone();
		isAutoOpen_ = other.isAutoOpen_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ItemInfoConfigure Clone()
	{
		return new ItemInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ItemInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ItemInfoConfigure other)
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
		if (ItemType != other.ItemType)
		{
			return false;
		}
		if (SubMeterID != other.SubMeterID)
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (IconSfw != other.IconSfw)
		{
			return false;
		}
		if (IconEN != other.IconEN)
		{
			return false;
		}
		if (IconJP != other.IconJP)
		{
			return false;
		}
		if (IconTC != other.IconTC)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (DescriptionID != other.DescriptionID)
		{
			return false;
		}
		if (QualityType != other.QualityType)
		{
			return false;
		}
		if (IsAutoTransform != other.IsAutoTransform)
		{
			return false;
		}
		if (!Transform.Equals(other.Transform))
		{
			return false;
		}
		if (!object.Equals(EndDateTime, other.EndDateTime))
		{
			return false;
		}
		if (!OuttimeTransform.Equals(other.OuttimeTransform))
		{
			return false;
		}
		if (IsClientShow != other.IsClientShow)
		{
			return false;
		}
		if (!wayList_.Equals(other.wayList_))
		{
			return false;
		}
		if (IsAutoOpen != other.IsAutoOpen)
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
		if (ItemType != ItemType.None)
		{
			num ^= ItemType.GetHashCode();
		}
		if (SubMeterID != 0)
		{
			num ^= SubMeterID.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		if (IconSfw.Length != 0)
		{
			num ^= IconSfw.GetHashCode();
		}
		if (IconEN.Length != 0)
		{
			num ^= IconEN.GetHashCode();
		}
		if (IconJP.Length != 0)
		{
			num ^= IconJP.GetHashCode();
		}
		if (IconTC.Length != 0)
		{
			num ^= IconTC.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (DescriptionID != 0)
		{
			num ^= DescriptionID.GetHashCode();
		}
		if (QualityType != QualityType.None)
		{
			num ^= QualityType.GetHashCode();
		}
		if (IsAutoTransform)
		{
			num ^= IsAutoTransform.GetHashCode();
		}
		num ^= Transform.GetHashCode();
		if (endDateTime_ != null)
		{
			num ^= EndDateTime.GetHashCode();
		}
		num ^= OuttimeTransform.GetHashCode();
		if (IsClientShow)
		{
			num ^= IsClientShow.GetHashCode();
		}
		num ^= wayList_.GetHashCode();
		if (IsAutoOpen)
		{
			num ^= IsAutoOpen.GetHashCode();
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
		if (ItemType != ItemType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)ItemType);
		}
		if (SubMeterID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(SubMeterID);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Icon);
		}
		if (IconSfw.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(IconSfw);
		}
		if (IconEN.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(IconEN);
		}
		if (IconJP.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(IconJP);
		}
		if (IconTC.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(IconTC);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(NameID);
		}
		if (DescriptionID != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(DescriptionID);
		}
		if (QualityType != QualityType.None)
		{
			output.WriteRawTag(88);
			output.WriteEnum((int)QualityType);
		}
		if (IsAutoTransform)
		{
			output.WriteRawTag(96);
			output.WriteBool(IsAutoTransform);
		}
		transform_.WriteTo(ref output, _map_transform_codec);
		if (endDateTime_ != null)
		{
			output.WriteRawTag(114);
			output.WriteMessage(EndDateTime);
		}
		outtimeTransform_.WriteTo(ref output, _map_outtimeTransform_codec);
		if (IsClientShow)
		{
			output.WriteRawTag(128, 1);
			output.WriteBool(IsClientShow);
		}
		wayList_.WriteTo(ref output, _repeated_wayList_codec);
		if (IsAutoOpen)
		{
			output.WriteRawTag(144, 1);
			output.WriteBool(IsAutoOpen);
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
		if (ItemType != ItemType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ItemType);
		}
		if (SubMeterID != 0)
		{
			num += 5;
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		if (IconSfw.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IconSfw);
		}
		if (IconEN.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IconEN);
		}
		if (IconJP.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IconJP);
		}
		if (IconTC.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IconTC);
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (DescriptionID != 0)
		{
			num += 5;
		}
		if (QualityType != QualityType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)QualityType);
		}
		if (IsAutoTransform)
		{
			num += 2;
		}
		num += transform_.CalculateSize(_map_transform_codec);
		if (endDateTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndDateTime);
		}
		num += outtimeTransform_.CalculateSize(_map_outtimeTransform_codec);
		if (IsClientShow)
		{
			num += 3;
		}
		num += wayList_.CalculateSize(_repeated_wayList_codec);
		if (IsAutoOpen)
		{
			num += 3;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ItemInfoConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
		if (other.ItemType != ItemType.None)
		{
			ItemType = other.ItemType;
		}
		if (other.SubMeterID != 0)
		{
			SubMeterID = other.SubMeterID;
		}
		if (other.Icon.Length != 0)
		{
			Icon = other.Icon;
		}
		if (other.IconSfw.Length != 0)
		{
			IconSfw = other.IconSfw;
		}
		if (other.IconEN.Length != 0)
		{
			IconEN = other.IconEN;
		}
		if (other.IconJP.Length != 0)
		{
			IconJP = other.IconJP;
		}
		if (other.IconTC.Length != 0)
		{
			IconTC = other.IconTC;
		}
		if (other.NameID != 0)
		{
			NameID = other.NameID;
		}
		if (other.DescriptionID != 0)
		{
			DescriptionID = other.DescriptionID;
		}
		if (other.QualityType != QualityType.None)
		{
			QualityType = other.QualityType;
		}
		if (other.IsAutoTransform)
		{
			IsAutoTransform = other.IsAutoTransform;
		}
		transform_.MergeFrom(other.transform_);
		if (other.endDateTime_ != null)
		{
			if (endDateTime_ == null)
			{
				EndDateTime = new Timestamp();
			}
			EndDateTime.MergeFrom(other.EndDateTime);
		}
		outtimeTransform_.MergeFrom(other.outtimeTransform_);
		if (other.IsClientShow)
		{
			IsClientShow = other.IsClientShow;
		}
		wayList_.Add(other.wayList_);
		if (other.IsAutoOpen)
		{
			IsAutoOpen = other.IsAutoOpen;
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
				ItemType = (ItemType)input.ReadEnum();
				break;
			case 29u:
				SubMeterID = input.ReadSFixed32();
				break;
			case 34u:
				Icon = input.ReadString();
				break;
			case 42u:
				IconSfw = input.ReadString();
				break;
			case 50u:
				IconEN = input.ReadString();
				break;
			case 58u:
				IconJP = input.ReadString();
				break;
			case 66u:
				IconTC = input.ReadString();
				break;
			case 77u:
				NameID = input.ReadSFixed32();
				break;
			case 85u:
				DescriptionID = input.ReadSFixed32();
				break;
			case 88u:
				QualityType = (QualityType)input.ReadEnum();
				break;
			case 96u:
				IsAutoTransform = input.ReadBool();
				break;
			case 106u:
				transform_.AddEntriesFrom(ref input, _map_transform_codec);
				break;
			case 114u:
				if (endDateTime_ == null)
				{
					EndDateTime = new Timestamp();
				}
				input.ReadMessage(EndDateTime);
				break;
			case 122u:
				outtimeTransform_.AddEntriesFrom(ref input, _map_outtimeTransform_codec);
				break;
			case 128u:
				IsClientShow = input.ReadBool();
				break;
			case 138u:
			case 141u:
				wayList_.AddEntriesFrom(ref input, _repeated_wayList_codec);
				break;
			case 144u:
				IsAutoOpen = input.ReadBool();
				break;
			}
		}
	}

	public void FixEndTime(FixItemInfoConfigure data)
	{
		endDateTime_ = data.EndDateTime;
	}

	public void FixIcon(string icon)
	{
		icon_ = icon;
	}

	public void UpdateVailTime(Timestamp launchTime, Timestamp endTime)
	{
		_isNeedCheck = true;
		_launchTime = launchTime;
		_endTime = endTime;
	}

	public bool IsVailItem()
	{
		if (!_isNeedCheck)
		{
			return true;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(Id))
		{
			return true;
		}
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if (_launchTime != null && _endTime == null)
		{
			return serverTime >= _launchTime.ToDateTime();
		}
		if (_launchTime == null && _endTime != null)
		{
			return serverTime <= _endTime.ToDateTime();
		}
		if (_launchTime != null && _endTime != null)
		{
			if (serverTime >= _launchTime.ToDateTime())
			{
				return serverTime <= _endTime.ToDateTime();
			}
			return false;
		}
		return false;
	}
}
