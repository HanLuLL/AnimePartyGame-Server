using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class LuckyStarBattleInfoConfigure : IMessage<LuckyStarBattleInfoConfigure>, IMessage, IEquatable<LuckyStarBattleInfoConfigure>, IDeepCloneable<LuckyStarBattleInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<LuckyStarBattleInfoConfigure> _parser = new MessageParser<LuckyStarBattleInfoConfigure>(() => new LuckyStarBattleInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int DescIdFieldNumber = 3;

	private int descId_;

	public const int RewardDescIdFieldNumber = 4;

	private int rewardDescId_;

	public const int ImageFieldNumber = 5;

	private string image_ = "";

	public const int LuckyStarMissionTypeFieldNumber = 6;

	private LuckyStarMissionType luckyStarMissionType_;

	public const int MissionParamFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_missionParam_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> missionParam_ = new RepeatedField<int>();

	public const int RewardFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_reward_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> reward_ = new RepeatedField<int>();

	public const int LuckyStarRewardFieldNumber = 9;

	private int luckyStarReward_;

	public const int BuffIdsFieldNumber = 10;

	private static readonly FieldCodec<int> _repeated_buffIds_codec = FieldCodec.ForSFixed32(82u);

	private readonly RepeatedField<int> buffIds_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<LuckyStarBattleInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => LuckyStarBattleReflection.Descriptor.MessageTypes[0];

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
	public int RewardDescId
	{
		get
		{
			return rewardDescId_;
		}
		private set
		{
			rewardDescId_ = value;
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
	public LuckyStarMissionType LuckyStarMissionType
	{
		get
		{
			return luckyStarMissionType_;
		}
		private set
		{
			luckyStarMissionType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MissionParam => missionParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Reward => reward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LuckyStarReward
	{
		get
		{
			return luckyStarReward_;
		}
		private set
		{
			luckyStarReward_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> BuffIds => buffIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarBattleInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarBattleInfoConfigure(LuckyStarBattleInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		nameID_ = other.nameID_;
		descId_ = other.descId_;
		rewardDescId_ = other.rewardDescId_;
		image_ = other.image_;
		luckyStarMissionType_ = other.luckyStarMissionType_;
		missionParam_ = other.missionParam_.Clone();
		reward_ = other.reward_.Clone();
		luckyStarReward_ = other.luckyStarReward_;
		buffIds_ = other.buffIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarBattleInfoConfigure Clone()
	{
		return new LuckyStarBattleInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as LuckyStarBattleInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(LuckyStarBattleInfoConfigure other)
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
		if (DescId != other.DescId)
		{
			return false;
		}
		if (RewardDescId != other.RewardDescId)
		{
			return false;
		}
		if (Image != other.Image)
		{
			return false;
		}
		if (LuckyStarMissionType != other.LuckyStarMissionType)
		{
			return false;
		}
		if (!missionParam_.Equals(other.missionParam_))
		{
			return false;
		}
		if (!reward_.Equals(other.reward_))
		{
			return false;
		}
		if (LuckyStarReward != other.LuckyStarReward)
		{
			return false;
		}
		if (!buffIds_.Equals(other.buffIds_))
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
		if (DescId != 0)
		{
			num ^= DescId.GetHashCode();
		}
		if (RewardDescId != 0)
		{
			num ^= RewardDescId.GetHashCode();
		}
		if (Image.Length != 0)
		{
			num ^= Image.GetHashCode();
		}
		if (LuckyStarMissionType != LuckyStarMissionType.None)
		{
			num ^= LuckyStarMissionType.GetHashCode();
		}
		num ^= missionParam_.GetHashCode();
		num ^= reward_.GetHashCode();
		if (LuckyStarReward != 0)
		{
			num ^= LuckyStarReward.GetHashCode();
		}
		num ^= buffIds_.GetHashCode();
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
		if (DescId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DescId);
		}
		if (RewardDescId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(RewardDescId);
		}
		if (Image.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Image);
		}
		if (LuckyStarMissionType != LuckyStarMissionType.None)
		{
			output.WriteRawTag(48);
			output.WriteEnum((int)LuckyStarMissionType);
		}
		missionParam_.WriteTo(ref output, _repeated_missionParam_codec);
		reward_.WriteTo(ref output, _repeated_reward_codec);
		if (LuckyStarReward != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(LuckyStarReward);
		}
		buffIds_.WriteTo(ref output, _repeated_buffIds_codec);
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
		if (DescId != 0)
		{
			num += 5;
		}
		if (RewardDescId != 0)
		{
			num += 5;
		}
		if (Image.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Image);
		}
		if (LuckyStarMissionType != LuckyStarMissionType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)LuckyStarMissionType);
		}
		num += missionParam_.CalculateSize(_repeated_missionParam_codec);
		num += reward_.CalculateSize(_repeated_reward_codec);
		if (LuckyStarReward != 0)
		{
			num += 5;
		}
		num += buffIds_.CalculateSize(_repeated_buffIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(LuckyStarBattleInfoConfigure other)
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
			if (other.DescId != 0)
			{
				DescId = other.DescId;
			}
			if (other.RewardDescId != 0)
			{
				RewardDescId = other.RewardDescId;
			}
			if (other.Image.Length != 0)
			{
				Image = other.Image;
			}
			if (other.LuckyStarMissionType != LuckyStarMissionType.None)
			{
				LuckyStarMissionType = other.LuckyStarMissionType;
			}
			missionParam_.Add(other.missionParam_);
			reward_.Add(other.reward_);
			if (other.LuckyStarReward != 0)
			{
				LuckyStarReward = other.LuckyStarReward;
			}
			buffIds_.Add(other.buffIds_);
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
				DescId = input.ReadSFixed32();
				break;
			case 37u:
				RewardDescId = input.ReadSFixed32();
				break;
			case 42u:
				Image = input.ReadString();
				break;
			case 48u:
				LuckyStarMissionType = (LuckyStarMissionType)input.ReadEnum();
				break;
			case 58u:
			case 61u:
				missionParam_.AddEntriesFrom(ref input, _repeated_missionParam_codec);
				break;
			case 66u:
			case 69u:
				reward_.AddEntriesFrom(ref input, _repeated_reward_codec);
				break;
			case 77u:
				LuckyStarReward = input.ReadSFixed32();
				break;
			case 82u:
			case 85u:
				buffIds_.AddEntriesFrom(ref input, _repeated_buffIds_codec);
				break;
			}
		}
	}
}
