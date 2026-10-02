using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SummonInfoConfigure : IMessage<SummonInfoConfigure>, IMessage, IEquatable<SummonInfoConfigure>, IDeepCloneable<SummonInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<SummonInfoConfigure> _parser = new MessageParser<SummonInfoConfigure>(() => new SummonInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int SummonTypeFieldNumber = 2;

	private SummonType summonType_;

	public const int NameIDFieldNumber = 3;

	private int nameID_;

	public const int SummonProfilePhotoFieldNumber = 4;

	private string summonProfilePhoto_ = "";

	public const int BornEffectFieldNumber = 5;

	private int bornEffect_;

	public const int PrefabNameFieldNumber = 6;

	private string prefabName_ = "";

	public const int CommonPerformsFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_commonPerforms_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> commonPerforms_ = new RepeatedField<int>();

	public const int PerformShowFieldNumber = 8;

	private int performShow_;

	public const int DebuffPerformsFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_debuffPerforms_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> debuffPerforms_ = new RepeatedField<int>();

	public const int BuffPerformsFieldNumber = 10;

	private static readonly FieldCodec<int> _repeated_buffPerforms_codec = FieldCodec.ForSFixed32(82u);

	private readonly RepeatedField<int> buffPerforms_ = new RepeatedField<int>();

	public const int MapcardIDFieldNumber = 11;

	private int mapcardID_;

	public const int ParamsFieldNumber = 12;

	private static readonly FieldCodec<int> _repeated_params_codec = FieldCodec.ForSInt32(98u);

	private readonly RepeatedField<int> params_ = new RepeatedField<int>();

	public const int BuffIdsFieldNumber = 13;

	private static readonly FieldCodec<int> _repeated_buffIds_codec = FieldCodec.ForSFixed32(106u);

	private readonly RepeatedField<int> buffIds_ = new RepeatedField<int>();

	public const int IsFollowSummoningFieldNumber = 14;

	private bool isFollowSummoning_;

	public const int IsPerformGroupFieldNumber = 15;

	private bool isPerformGroup_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SummonInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SummonReflection.Descriptor.MessageTypes[0];

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
	public SummonType SummonType
	{
		get
		{
			return summonType_;
		}
		private set
		{
			summonType_ = value;
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
	public string SummonProfilePhoto
	{
		get
		{
			return summonProfilePhoto_;
		}
		private set
		{
			summonProfilePhoto_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BornEffect
	{
		get
		{
			return bornEffect_;
		}
		private set
		{
			bornEffect_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PrefabName
	{
		get
		{
			return prefabName_;
		}
		private set
		{
			prefabName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> CommonPerforms => commonPerforms_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PerformShow
	{
		get
		{
			return performShow_;
		}
		private set
		{
			performShow_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> DebuffPerforms => debuffPerforms_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuffPerforms => buffPerforms_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapcardID
	{
		get
		{
			return mapcardID_;
		}
		private set
		{
			mapcardID_ = value;
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
	public bool IsFollowSummoning
	{
		get
		{
			return isFollowSummoning_;
		}
		private set
		{
			isFollowSummoning_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsPerformGroup
	{
		get
		{
			return isPerformGroup_;
		}
		private set
		{
			isPerformGroup_ = value;
		}
	}

	public int PerformDebuff => DebuffPerforms[0];

	public int PerformBuff => BuffPerforms[0];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SummonInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SummonInfoConfigure(SummonInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		summonType_ = other.summonType_;
		nameID_ = other.nameID_;
		summonProfilePhoto_ = other.summonProfilePhoto_;
		bornEffect_ = other.bornEffect_;
		prefabName_ = other.prefabName_;
		commonPerforms_ = other.commonPerforms_.Clone();
		performShow_ = other.performShow_;
		debuffPerforms_ = other.debuffPerforms_.Clone();
		buffPerforms_ = other.buffPerforms_.Clone();
		mapcardID_ = other.mapcardID_;
		params_ = other.params_.Clone();
		buffIds_ = other.buffIds_.Clone();
		isFollowSummoning_ = other.isFollowSummoning_;
		isPerformGroup_ = other.isPerformGroup_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SummonInfoConfigure Clone()
	{
		return new SummonInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SummonInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SummonInfoConfigure other)
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
		if (SummonType != other.SummonType)
		{
			return false;
		}
		if (NameID != other.NameID)
		{
			return false;
		}
		if (SummonProfilePhoto != other.SummonProfilePhoto)
		{
			return false;
		}
		if (BornEffect != other.BornEffect)
		{
			return false;
		}
		if (PrefabName != other.PrefabName)
		{
			return false;
		}
		if (!commonPerforms_.Equals(other.commonPerforms_))
		{
			return false;
		}
		if (PerformShow != other.PerformShow)
		{
			return false;
		}
		if (!debuffPerforms_.Equals(other.debuffPerforms_))
		{
			return false;
		}
		if (!buffPerforms_.Equals(other.buffPerforms_))
		{
			return false;
		}
		if (MapcardID != other.MapcardID)
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
		if (IsFollowSummoning != other.IsFollowSummoning)
		{
			return false;
		}
		if (IsPerformGroup != other.IsPerformGroup)
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
		if (SummonType != SummonType.None)
		{
			num ^= SummonType.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (SummonProfilePhoto.Length != 0)
		{
			num ^= SummonProfilePhoto.GetHashCode();
		}
		if (BornEffect != 0)
		{
			num ^= BornEffect.GetHashCode();
		}
		if (PrefabName.Length != 0)
		{
			num ^= PrefabName.GetHashCode();
		}
		num ^= commonPerforms_.GetHashCode();
		if (PerformShow != 0)
		{
			num ^= PerformShow.GetHashCode();
		}
		num ^= debuffPerforms_.GetHashCode();
		num ^= buffPerforms_.GetHashCode();
		if (MapcardID != 0)
		{
			num ^= MapcardID.GetHashCode();
		}
		num ^= params_.GetHashCode();
		num ^= buffIds_.GetHashCode();
		if (IsFollowSummoning)
		{
			num ^= IsFollowSummoning.GetHashCode();
		}
		if (IsPerformGroup)
		{
			num ^= IsPerformGroup.GetHashCode();
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
		if (SummonType != SummonType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)SummonType);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(NameID);
		}
		if (SummonProfilePhoto.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(SummonProfilePhoto);
		}
		if (BornEffect != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(BornEffect);
		}
		if (PrefabName.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(PrefabName);
		}
		commonPerforms_.WriteTo(ref output, _repeated_commonPerforms_codec);
		if (PerformShow != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(PerformShow);
		}
		debuffPerforms_.WriteTo(ref output, _repeated_debuffPerforms_codec);
		buffPerforms_.WriteTo(ref output, _repeated_buffPerforms_codec);
		if (MapcardID != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(MapcardID);
		}
		params_.WriteTo(ref output, _repeated_params_codec);
		buffIds_.WriteTo(ref output, _repeated_buffIds_codec);
		if (IsFollowSummoning)
		{
			output.WriteRawTag(112);
			output.WriteBool(IsFollowSummoning);
		}
		if (IsPerformGroup)
		{
			output.WriteRawTag(120);
			output.WriteBool(IsPerformGroup);
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
		if (SummonType != SummonType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)SummonType);
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (SummonProfilePhoto.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(SummonProfilePhoto);
		}
		if (BornEffect != 0)
		{
			num += 5;
		}
		if (PrefabName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PrefabName);
		}
		num += commonPerforms_.CalculateSize(_repeated_commonPerforms_codec);
		if (PerformShow != 0)
		{
			num += 5;
		}
		num += debuffPerforms_.CalculateSize(_repeated_debuffPerforms_codec);
		num += buffPerforms_.CalculateSize(_repeated_buffPerforms_codec);
		if (MapcardID != 0)
		{
			num += 5;
		}
		num += params_.CalculateSize(_repeated_params_codec);
		num += buffIds_.CalculateSize(_repeated_buffIds_codec);
		if (IsFollowSummoning)
		{
			num += 2;
		}
		if (IsPerformGroup)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SummonInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.SummonType != SummonType.None)
			{
				SummonType = other.SummonType;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.SummonProfilePhoto.Length != 0)
			{
				SummonProfilePhoto = other.SummonProfilePhoto;
			}
			if (other.BornEffect != 0)
			{
				BornEffect = other.BornEffect;
			}
			if (other.PrefabName.Length != 0)
			{
				PrefabName = other.PrefabName;
			}
			commonPerforms_.Add(other.commonPerforms_);
			if (other.PerformShow != 0)
			{
				PerformShow = other.PerformShow;
			}
			debuffPerforms_.Add(other.debuffPerforms_);
			buffPerforms_.Add(other.buffPerforms_);
			if (other.MapcardID != 0)
			{
				MapcardID = other.MapcardID;
			}
			params_.Add(other.params_);
			buffIds_.Add(other.buffIds_);
			if (other.IsFollowSummoning)
			{
				IsFollowSummoning = other.IsFollowSummoning;
			}
			if (other.IsPerformGroup)
			{
				IsPerformGroup = other.IsPerformGroup;
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
				SummonType = (SummonType)input.ReadEnum();
				break;
			case 29u:
				NameID = input.ReadSFixed32();
				break;
			case 34u:
				SummonProfilePhoto = input.ReadString();
				break;
			case 45u:
				BornEffect = input.ReadSFixed32();
				break;
			case 50u:
				PrefabName = input.ReadString();
				break;
			case 58u:
			case 61u:
				commonPerforms_.AddEntriesFrom(ref input, _repeated_commonPerforms_codec);
				break;
			case 69u:
				PerformShow = input.ReadSFixed32();
				break;
			case 74u:
			case 77u:
				debuffPerforms_.AddEntriesFrom(ref input, _repeated_debuffPerforms_codec);
				break;
			case 82u:
			case 85u:
				buffPerforms_.AddEntriesFrom(ref input, _repeated_buffPerforms_codec);
				break;
			case 93u:
				MapcardID = input.ReadSFixed32();
				break;
			case 96u:
			case 98u:
				params_.AddEntriesFrom(ref input, _repeated_params_codec);
				break;
			case 106u:
			case 109u:
				buffIds_.AddEntriesFrom(ref input, _repeated_buffIds_codec);
				break;
			case 112u:
				IsFollowSummoning = input.ReadBool();
				break;
			case 120u:
				IsPerformGroup = input.ReadBool();
				break;
			}
		}
	}
}
