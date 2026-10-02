using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public sealed class MissionDataConfigure : IMessage<MissionDataConfigure>, IMessage, IEquatable<MissionDataConfigure>, IDeepCloneable<MissionDataConfigure>, IBufferMessage
{
	private static readonly MessageParser<MissionDataConfigure> _parser = new MessageParser<MissionDataConfigure>(() => new MissionDataConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int OrderWeightFieldNumber = 2;

	private int orderWeight_;

	public const int NextidFieldNumber = 3;

	private int nextid_;

	public const int FrontIDFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_frontID_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> frontID_ = new RepeatedField<int>();

	public const int TaskRefreshTypeFieldNumber = 5;

	private TaskRefreshType taskRefreshType_;

	public const int MissionConditionTypeFieldNumber = 6;

	private MissionConditionType missionConditionType_;

	public const int ParamProgressFieldNumber = 7;

	private int paramProgress_;

	public const int ParamKeyFieldNumber = 8;

	private static readonly FieldCodec<MissionParamType> _repeated_paramKey_codec = FieldCodec.ForEnum(66u, (MissionParamType x) => (int)x, (int x) => (MissionParamType)x);

	private readonly RepeatedField<MissionParamType> paramKey_ = new RepeatedField<MissionParamType>();

	public const int ParamValueFieldNumber = 9;

	private static readonly FieldCodec<int> _repeated_paramValue_codec = FieldCodec.ForSFixed32(74u);

	private readonly RepeatedField<int> paramValue_ = new RepeatedField<int>();

	public const int BeginTimeFieldNumber = 10;

	private Timestamp beginTime_;

	public const int EndTimeFieldNumber = 11;

	private Timestamp endTime_;

	public const int NameIDFieldNumber = 12;

	private int nameID_;

	public const int DescIdFieldNumber = 13;

	private int descId_;

	public const int RewardFieldNumber = 14;

	private static readonly MapField<int, int>.Codec _map_reward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 114u);

	private readonly MapField<int, int> reward_ = new MapField<int, int>();

	public const int WayFieldNumber = 15;

	private int way_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MissionDataConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MissionReflection.Descriptor.MessageTypes[0];

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
	public int Nextid
	{
		get
		{
			return nextid_;
		}
		private set
		{
			nextid_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> FrontID => frontID_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskRefreshType TaskRefreshType
	{
		get
		{
			return taskRefreshType_;
		}
		private set
		{
			taskRefreshType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MissionConditionType MissionConditionType
	{
		get
		{
			return missionConditionType_;
		}
		private set
		{
			missionConditionType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ParamProgress
	{
		get
		{
			return paramProgress_;
		}
		private set
		{
			paramProgress_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MissionParamType> ParamKey => paramKey_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> ParamValue => paramValue_;

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
	public MapField<int, int> Reward => reward_;

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
	public MissionDataConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MissionDataConfigure(MissionDataConfigure other)
		: this()
	{
		id_ = other.id_;
		orderWeight_ = other.orderWeight_;
		nextid_ = other.nextid_;
		frontID_ = other.frontID_.Clone();
		taskRefreshType_ = other.taskRefreshType_;
		missionConditionType_ = other.missionConditionType_;
		paramProgress_ = other.paramProgress_;
		paramKey_ = other.paramKey_.Clone();
		paramValue_ = other.paramValue_.Clone();
		beginTime_ = ((other.beginTime_ != null) ? other.beginTime_.Clone() : null);
		endTime_ = ((other.endTime_ != null) ? other.endTime_.Clone() : null);
		nameID_ = other.nameID_;
		descId_ = other.descId_;
		reward_ = other.reward_.Clone();
		way_ = other.way_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MissionDataConfigure Clone()
	{
		return new MissionDataConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MissionDataConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MissionDataConfigure other)
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
		if (OrderWeight != other.OrderWeight)
		{
			return false;
		}
		if (Nextid != other.Nextid)
		{
			return false;
		}
		if (!frontID_.Equals(other.frontID_))
		{
			return false;
		}
		if (TaskRefreshType != other.TaskRefreshType)
		{
			return false;
		}
		if (MissionConditionType != other.MissionConditionType)
		{
			return false;
		}
		if (ParamProgress != other.ParamProgress)
		{
			return false;
		}
		if (!paramKey_.Equals(other.paramKey_))
		{
			return false;
		}
		if (!paramValue_.Equals(other.paramValue_))
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
		if (NameID != other.NameID)
		{
			return false;
		}
		if (DescId != other.DescId)
		{
			return false;
		}
		if (!Reward.Equals(other.Reward))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (OrderWeight != 0)
		{
			num ^= OrderWeight.GetHashCode();
		}
		if (Nextid != 0)
		{
			num ^= Nextid.GetHashCode();
		}
		num ^= frontID_.GetHashCode();
		if (TaskRefreshType != TaskRefreshType.None)
		{
			num ^= TaskRefreshType.GetHashCode();
		}
		if (MissionConditionType != MissionConditionType.None)
		{
			num ^= MissionConditionType.GetHashCode();
		}
		if (ParamProgress != 0)
		{
			num ^= ParamProgress.GetHashCode();
		}
		num ^= paramKey_.GetHashCode();
		num ^= paramValue_.GetHashCode();
		if (beginTime_ != null)
		{
			num ^= BeginTime.GetHashCode();
		}
		if (endTime_ != null)
		{
			num ^= EndTime.GetHashCode();
		}
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (DescId != 0)
		{
			num ^= DescId.GetHashCode();
		}
		num ^= Reward.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (OrderWeight != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(OrderWeight);
		}
		if (Nextid != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Nextid);
		}
		frontID_.WriteTo(ref output, _repeated_frontID_codec);
		if (TaskRefreshType != TaskRefreshType.None)
		{
			output.WriteRawTag(40);
			output.WriteEnum((int)TaskRefreshType);
		}
		if (MissionConditionType != MissionConditionType.None)
		{
			output.WriteRawTag(48);
			output.WriteEnum((int)MissionConditionType);
		}
		if (ParamProgress != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(ParamProgress);
		}
		paramKey_.WriteTo(ref output, _repeated_paramKey_codec);
		paramValue_.WriteTo(ref output, _repeated_paramValue_codec);
		if (beginTime_ != null)
		{
			output.WriteRawTag(82);
			output.WriteMessage(BeginTime);
		}
		if (endTime_ != null)
		{
			output.WriteRawTag(90);
			output.WriteMessage(EndTime);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(NameID);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(DescId);
		}
		reward_.WriteTo(ref output, _map_reward_codec);
		if (Way != 0)
		{
			output.WriteRawTag(125);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (OrderWeight != 0)
		{
			num += 5;
		}
		if (Nextid != 0)
		{
			num += 5;
		}
		num += frontID_.CalculateSize(_repeated_frontID_codec);
		if (TaskRefreshType != TaskRefreshType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)TaskRefreshType);
		}
		if (MissionConditionType != MissionConditionType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MissionConditionType);
		}
		if (ParamProgress != 0)
		{
			num += 5;
		}
		num += paramKey_.CalculateSize(_repeated_paramKey_codec);
		num += paramValue_.CalculateSize(_repeated_paramValue_codec);
		if (beginTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeginTime);
		}
		if (endTime_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EndTime);
		}
		if (NameID != 0)
		{
			num += 5;
		}
		if (DescId != 0)
		{
			num += 5;
		}
		num += reward_.CalculateSize(_map_reward_codec);
		if (Way != 0)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MissionDataConfigure other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Id != 0)
		{
			Id = other.Id;
		}
		if (other.OrderWeight != 0)
		{
			OrderWeight = other.OrderWeight;
		}
		if (other.Nextid != 0)
		{
			Nextid = other.Nextid;
		}
		frontID_.Add(other.frontID_);
		if (other.TaskRefreshType != TaskRefreshType.None)
		{
			TaskRefreshType = other.TaskRefreshType;
		}
		if (other.MissionConditionType != MissionConditionType.None)
		{
			MissionConditionType = other.MissionConditionType;
		}
		if (other.ParamProgress != 0)
		{
			ParamProgress = other.ParamProgress;
		}
		paramKey_.Add(other.paramKey_);
		paramValue_.Add(other.paramValue_);
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
		if (other.NameID != 0)
		{
			NameID = other.NameID;
		}
		if (other.DescId != 0)
		{
			DescId = other.DescId;
		}
		reward_.MergeFrom(other.reward_);
		if (other.Way != 0)
		{
			Way = other.Way;
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
			case 21u:
				OrderWeight = input.ReadSFixed32();
				break;
			case 29u:
				Nextid = input.ReadSFixed32();
				break;
			case 34u:
			case 37u:
				frontID_.AddEntriesFrom(ref input, _repeated_frontID_codec);
				break;
			case 40u:
				TaskRefreshType = (TaskRefreshType)input.ReadEnum();
				break;
			case 48u:
				MissionConditionType = (MissionConditionType)input.ReadEnum();
				break;
			case 61u:
				ParamProgress = input.ReadSFixed32();
				break;
			case 64u:
			case 66u:
				paramKey_.AddEntriesFrom(ref input, _repeated_paramKey_codec);
				break;
			case 74u:
			case 77u:
				paramValue_.AddEntriesFrom(ref input, _repeated_paramValue_codec);
				break;
			case 82u:
				if (beginTime_ == null)
				{
					BeginTime = new Timestamp();
				}
				input.ReadMessage(BeginTime);
				break;
			case 90u:
				if (endTime_ == null)
				{
					EndTime = new Timestamp();
				}
				input.ReadMessage(EndTime);
				break;
			case 101u:
				NameID = input.ReadSFixed32();
				break;
			case 109u:
				DescId = input.ReadSFixed32();
				break;
			case 114u:
				reward_.AddEntriesFrom(ref input, _map_reward_codec);
				break;
			case 125u:
				Way = input.ReadSFixed32();
				break;
			}
		}
	}

	public void FixTime(FixMissionDataConfigure data)
	{
		beginTime_ = data.BeginTime;
		endTime_ = data.EndTime;
	}
}
