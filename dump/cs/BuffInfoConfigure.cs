using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BuffInfoConfigure : IMessage<BuffInfoConfigure>, IMessage, IEquatable<BuffInfoConfigure>, IDeepCloneable<BuffInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<BuffInfoConfigure> _parser = new MessageParser<BuffInfoConfigure>(() => new BuffInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int BuffTypeFieldNumber = 2;

	private BuffType buffType_;

	public const int BuffTagTypeFieldNumber = 3;

	private static readonly FieldCodec<BuffTagType> _repeated_buffTagType_codec = FieldCodec.ForEnum(26u, (BuffTagType x) => (int)x, (int x) => (BuffTagType)x);

	private readonly RepeatedField<BuffTagType> buffTagType_ = new RepeatedField<BuffTagType>();

	public const int BuffRoundCountTypeFieldNumber = 4;

	private BuffRoundCountType buffRoundCountType_;

	public const int DelayRoundFieldNumber = 5;

	private int delayRound_;

	public const int KeepRoundFieldNumber = 6;

	private int keepRound_;

	public const int IsTriggerClearFieldNumber = 7;

	private bool isTriggerClear_;

	public const int IsDeathClearFieldNumber = 8;

	private bool isDeathClear_;

	public const int EffectCardIdsFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_effectCardIds_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> effectCardIds_ = new RepeatedField<int>();

	public const int BuffConflictManagementTypeFieldNumber = 10;

	private BuffConflictManagementType buffConflictManagementType_;

	public const int IsShowFieldNumber = 11;

	private bool isShow_;

	public const int IconFieldNumber = 12;

	private string icon_ = "";

	public const int IsShowIconProgressZeroFieldNumber = 13;

	private bool isShowIconProgressZero_;

	public const int NameIdFieldNumber = 14;

	private int nameId_;

	public const int DescIdFieldNumber = 15;

	private int descId_;

	public const int PerformStartFieldNumber = 16;

	private int performStart_;

	public const int PerformEndFieldNumber = 17;

	private int performEnd_;

	public const int PerformAwakeFieldNumber = 18;

	private int performAwake_;

	public const int PerformDestroyFieldNumber = 19;

	private int performDestroy_;

	public const int EffectIDFieldNumber = 20;

	private int effectID_;

	public const int SkinReplaceEffectIDFieldNumber = 21;

	private static readonly MapField<int, int>.Codec _map_skinReplaceEffectID_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 170u);

	private readonly MapField<int, int> skinReplaceEffectID_ = new MapField<int, int>();

	public const int IsShowEffectDuringPKFieldNumber = 22;

	private bool isShowEffectDuringPK_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BuffInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BuffReflection.Descriptor.MessageTypes[0];

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
	public BuffType BuffType
	{
		get
		{
			return buffType_;
		}
		private set
		{
			buffType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<BuffTagType> BuffTagType => buffTagType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuffRoundCountType BuffRoundCountType
	{
		get
		{
			return buffRoundCountType_;
		}
		private set
		{
			buffRoundCountType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DelayRound
	{
		get
		{
			return delayRound_;
		}
		private set
		{
			delayRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int KeepRound
	{
		get
		{
			return keepRound_;
		}
		private set
		{
			keepRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsTriggerClear
	{
		get
		{
			return isTriggerClear_;
		}
		private set
		{
			isTriggerClear_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsDeathClear
	{
		get
		{
			return isDeathClear_;
		}
		private set
		{
			isDeathClear_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> EffectCardIds => effectCardIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuffConflictManagementType BuffConflictManagementType
	{
		get
		{
			return buffConflictManagementType_;
		}
		private set
		{
			buffConflictManagementType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsShow
	{
		get
		{
			return isShow_;
		}
		private set
		{
			isShow_ = value;
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
	public bool IsShowIconProgressZero
	{
		get
		{
			return isShowIconProgressZero_;
		}
		private set
		{
			isShowIconProgressZero_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NameId
	{
		get
		{
			return nameId_;
		}
		private set
		{
			nameId_ = value;
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
	public int PerformStart
	{
		get
		{
			return performStart_;
		}
		private set
		{
			performStart_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PerformEnd
	{
		get
		{
			return performEnd_;
		}
		private set
		{
			performEnd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PerformAwake
	{
		get
		{
			return performAwake_;
		}
		private set
		{
			performAwake_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PerformDestroy
	{
		get
		{
			return performDestroy_;
		}
		private set
		{
			performDestroy_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EffectID
	{
		get
		{
			return effectID_;
		}
		private set
		{
			effectID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> SkinReplaceEffectID => skinReplaceEffectID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsShowEffectDuringPK
	{
		get
		{
			return isShowEffectDuringPK_;
		}
		private set
		{
			isShowEffectDuringPK_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuffInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuffInfoConfigure(BuffInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		buffType_ = other.buffType_;
		buffTagType_ = other.buffTagType_.Clone();
		buffRoundCountType_ = other.buffRoundCountType_;
		delayRound_ = other.delayRound_;
		keepRound_ = other.keepRound_;
		isTriggerClear_ = other.isTriggerClear_;
		isDeathClear_ = other.isDeathClear_;
		effectCardIds_ = other.effectCardIds_.Clone();
		buffConflictManagementType_ = other.buffConflictManagementType_;
		isShow_ = other.isShow_;
		icon_ = other.icon_;
		isShowIconProgressZero_ = other.isShowIconProgressZero_;
		nameId_ = other.nameId_;
		descId_ = other.descId_;
		performStart_ = other.performStart_;
		performEnd_ = other.performEnd_;
		performAwake_ = other.performAwake_;
		performDestroy_ = other.performDestroy_;
		effectID_ = other.effectID_;
		skinReplaceEffectID_ = other.skinReplaceEffectID_.Clone();
		isShowEffectDuringPK_ = other.isShowEffectDuringPK_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuffInfoConfigure Clone()
	{
		return new BuffInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BuffInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BuffInfoConfigure other)
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
		if (BuffType != other.BuffType)
		{
			return false;
		}
		if (!buffTagType_.Equals(other.buffTagType_))
		{
			return false;
		}
		if (BuffRoundCountType != other.BuffRoundCountType)
		{
			return false;
		}
		if (DelayRound != other.DelayRound)
		{
			return false;
		}
		if (KeepRound != other.KeepRound)
		{
			return false;
		}
		if (IsTriggerClear != other.IsTriggerClear)
		{
			return false;
		}
		if (IsDeathClear != other.IsDeathClear)
		{
			return false;
		}
		if (!effectCardIds_.Equals(other.effectCardIds_))
		{
			return false;
		}
		if (BuffConflictManagementType != other.BuffConflictManagementType)
		{
			return false;
		}
		if (IsShow != other.IsShow)
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (IsShowIconProgressZero != other.IsShowIconProgressZero)
		{
			return false;
		}
		if (NameId != other.NameId)
		{
			return false;
		}
		if (DescId != other.DescId)
		{
			return false;
		}
		if (PerformStart != other.PerformStart)
		{
			return false;
		}
		if (PerformEnd != other.PerformEnd)
		{
			return false;
		}
		if (PerformAwake != other.PerformAwake)
		{
			return false;
		}
		if (PerformDestroy != other.PerformDestroy)
		{
			return false;
		}
		if (EffectID != other.EffectID)
		{
			return false;
		}
		if (!SkinReplaceEffectID.Equals(other.SkinReplaceEffectID))
		{
			return false;
		}
		if (IsShowEffectDuringPK != other.IsShowEffectDuringPK)
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
		if (BuffType != BuffType.None)
		{
			num ^= BuffType.GetHashCode();
		}
		num ^= buffTagType_.GetHashCode();
		if (BuffRoundCountType != BuffRoundCountType.None)
		{
			num ^= BuffRoundCountType.GetHashCode();
		}
		if (DelayRound != 0)
		{
			num ^= DelayRound.GetHashCode();
		}
		if (KeepRound != 0)
		{
			num ^= KeepRound.GetHashCode();
		}
		if (IsTriggerClear)
		{
			num ^= IsTriggerClear.GetHashCode();
		}
		if (IsDeathClear)
		{
			num ^= IsDeathClear.GetHashCode();
		}
		num ^= effectCardIds_.GetHashCode();
		if (BuffConflictManagementType != BuffConflictManagementType.None)
		{
			num ^= BuffConflictManagementType.GetHashCode();
		}
		if (IsShow)
		{
			num ^= IsShow.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		if (IsShowIconProgressZero)
		{
			num ^= IsShowIconProgressZero.GetHashCode();
		}
		if (NameId != 0)
		{
			num ^= NameId.GetHashCode();
		}
		if (DescId != 0)
		{
			num ^= DescId.GetHashCode();
		}
		if (PerformStart != 0)
		{
			num ^= PerformStart.GetHashCode();
		}
		if (PerformEnd != 0)
		{
			num ^= PerformEnd.GetHashCode();
		}
		if (PerformAwake != 0)
		{
			num ^= PerformAwake.GetHashCode();
		}
		if (PerformDestroy != 0)
		{
			num ^= PerformDestroy.GetHashCode();
		}
		if (EffectID != 0)
		{
			num ^= EffectID.GetHashCode();
		}
		num ^= SkinReplaceEffectID.GetHashCode();
		if (IsShowEffectDuringPK)
		{
			num ^= IsShowEffectDuringPK.GetHashCode();
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
		if (BuffType != BuffType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)BuffType);
		}
		buffTagType_.WriteTo(ref output, _repeated_buffTagType_codec);
		if (BuffRoundCountType != BuffRoundCountType.None)
		{
			output.WriteRawTag(32);
			output.WriteEnum((int)BuffRoundCountType);
		}
		if (DelayRound != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(DelayRound);
		}
		if (KeepRound != 0)
		{
			output.WriteRawTag(48);
			output.WriteInt32(KeepRound);
		}
		if (IsTriggerClear)
		{
			output.WriteRawTag(56);
			output.WriteBool(IsTriggerClear);
		}
		if (IsDeathClear)
		{
			output.WriteRawTag(64);
			output.WriteBool(IsDeathClear);
		}
		effectCardIds_.WriteTo(ref output, _repeated_effectCardIds_codec);
		if (BuffConflictManagementType != BuffConflictManagementType.None)
		{
			output.WriteRawTag(80);
			output.WriteEnum((int)BuffConflictManagementType);
		}
		if (IsShow)
		{
			output.WriteRawTag(88);
			output.WriteBool(IsShow);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(98);
			output.WriteString(Icon);
		}
		if (IsShowIconProgressZero)
		{
			output.WriteRawTag(104);
			output.WriteBool(IsShowIconProgressZero);
		}
		if (NameId != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(NameId);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(DescId);
		}
		if (PerformStart != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(PerformStart);
		}
		if (PerformEnd != 0)
		{
			output.WriteRawTag(141, 1);
			output.WriteSFixed32(PerformEnd);
		}
		if (PerformAwake != 0)
		{
			output.WriteRawTag(149, 1);
			output.WriteSFixed32(PerformAwake);
		}
		if (PerformDestroy != 0)
		{
			output.WriteRawTag(157, 1);
			output.WriteSFixed32(PerformDestroy);
		}
		if (EffectID != 0)
		{
			output.WriteRawTag(165, 1);
			output.WriteSFixed32(EffectID);
		}
		skinReplaceEffectID_.WriteTo(ref output, _map_skinReplaceEffectID_codec);
		if (IsShowEffectDuringPK)
		{
			output.WriteRawTag(176, 1);
			output.WriteBool(IsShowEffectDuringPK);
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
		if (BuffType != BuffType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)BuffType);
		}
		num += buffTagType_.CalculateSize(_repeated_buffTagType_codec);
		if (BuffRoundCountType != BuffRoundCountType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)BuffRoundCountType);
		}
		if (DelayRound != 0)
		{
			num += 5;
		}
		if (KeepRound != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(KeepRound);
		}
		if (IsTriggerClear)
		{
			num += 2;
		}
		if (IsDeathClear)
		{
			num += 2;
		}
		num += effectCardIds_.CalculateSize(_repeated_effectCardIds_codec);
		if (BuffConflictManagementType != BuffConflictManagementType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)BuffConflictManagementType);
		}
		if (IsShow)
		{
			num += 2;
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		if (IsShowIconProgressZero)
		{
			num += 2;
		}
		if (NameId != 0)
		{
			num += 5;
		}
		if (DescId != 0)
		{
			num += 5;
		}
		if (PerformStart != 0)
		{
			num += 6;
		}
		if (PerformEnd != 0)
		{
			num += 6;
		}
		if (PerformAwake != 0)
		{
			num += 6;
		}
		if (PerformDestroy != 0)
		{
			num += 6;
		}
		if (EffectID != 0)
		{
			num += 6;
		}
		num += skinReplaceEffectID_.CalculateSize(_map_skinReplaceEffectID_codec);
		if (IsShowEffectDuringPK)
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
	public void MergeFrom(BuffInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.BuffType != BuffType.None)
			{
				BuffType = other.BuffType;
			}
			buffTagType_.Add(other.buffTagType_);
			if (other.BuffRoundCountType != BuffRoundCountType.None)
			{
				BuffRoundCountType = other.BuffRoundCountType;
			}
			if (other.DelayRound != 0)
			{
				DelayRound = other.DelayRound;
			}
			if (other.KeepRound != 0)
			{
				KeepRound = other.KeepRound;
			}
			if (other.IsTriggerClear)
			{
				IsTriggerClear = other.IsTriggerClear;
			}
			if (other.IsDeathClear)
			{
				IsDeathClear = other.IsDeathClear;
			}
			effectCardIds_.Add(other.effectCardIds_);
			if (other.BuffConflictManagementType != BuffConflictManagementType.None)
			{
				BuffConflictManagementType = other.BuffConflictManagementType;
			}
			if (other.IsShow)
			{
				IsShow = other.IsShow;
			}
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
			if (other.IsShowIconProgressZero)
			{
				IsShowIconProgressZero = other.IsShowIconProgressZero;
			}
			if (other.NameId != 0)
			{
				NameId = other.NameId;
			}
			if (other.DescId != 0)
			{
				DescId = other.DescId;
			}
			if (other.PerformStart != 0)
			{
				PerformStart = other.PerformStart;
			}
			if (other.PerformEnd != 0)
			{
				PerformEnd = other.PerformEnd;
			}
			if (other.PerformAwake != 0)
			{
				PerformAwake = other.PerformAwake;
			}
			if (other.PerformDestroy != 0)
			{
				PerformDestroy = other.PerformDestroy;
			}
			if (other.EffectID != 0)
			{
				EffectID = other.EffectID;
			}
			skinReplaceEffectID_.MergeFrom(other.skinReplaceEffectID_);
			if (other.IsShowEffectDuringPK)
			{
				IsShowEffectDuringPK = other.IsShowEffectDuringPK;
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
				Id = input.ReadSFixed32();
				break;
			case 16u:
				BuffType = (BuffType)input.ReadEnum();
				break;
			case 24u:
			case 26u:
				buffTagType_.AddEntriesFrom(ref input, _repeated_buffTagType_codec);
				break;
			case 32u:
				BuffRoundCountType = (BuffRoundCountType)input.ReadEnum();
				break;
			case 45u:
				DelayRound = input.ReadSFixed32();
				break;
			case 48u:
				KeepRound = input.ReadInt32();
				break;
			case 56u:
				IsTriggerClear = input.ReadBool();
				break;
			case 64u:
				IsDeathClear = input.ReadBool();
				break;
			case 74u:
			case 77u:
				effectCardIds_.AddEntriesFrom(ref input, _repeated_effectCardIds_codec);
				break;
			case 80u:
				BuffConflictManagementType = (BuffConflictManagementType)input.ReadEnum();
				break;
			case 88u:
				IsShow = input.ReadBool();
				break;
			case 98u:
				Icon = input.ReadString();
				break;
			case 104u:
				IsShowIconProgressZero = input.ReadBool();
				break;
			case 117u:
				NameId = input.ReadSFixed32();
				break;
			case 125u:
				DescId = input.ReadSFixed32();
				break;
			case 133u:
				PerformStart = input.ReadSFixed32();
				break;
			case 141u:
				PerformEnd = input.ReadSFixed32();
				break;
			case 149u:
				PerformAwake = input.ReadSFixed32();
				break;
			case 157u:
				PerformDestroy = input.ReadSFixed32();
				break;
			case 165u:
				EffectID = input.ReadSFixed32();
				break;
			case 170u:
				skinReplaceEffectID_.AddEntriesFrom(ref input, _map_skinReplaceEffectID_codec);
				break;
			case 176u:
				IsShowEffectDuringPK = input.ReadBool();
				break;
			}
		}
	}
}
