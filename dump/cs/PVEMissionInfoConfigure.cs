using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PVEMissionInfoConfigure : IMessage<PVEMissionInfoConfigure>, IMessage, IEquatable<PVEMissionInfoConfigure>, IDeepCloneable<PVEMissionInfoConfigure>, IBufferMessage, IPVEMissionTargetConfigs
{
	private static readonly MessageParser<PVEMissionInfoConfigure> _parser = new MessageParser<PVEMissionInfoConfigure>(() => new PVEMissionInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int PveMissionTriggerTypeFieldNumber = 2;

	private PVEMissionTriggerType pveMissionTriggerType_;

	public const int TriggerparamsFieldNumber = 3;

	private static readonly FieldCodec<int> _repeated_triggerparams_codec = FieldCodec.ForSFixed32(26u);

	private readonly RepeatedField<int> triggerparams_ = new RepeatedField<int>();

	public const int MissionDescIDFieldNumber = 4;

	private int missionDescID_;

	public const int MissionTargetConfigsFieldNumber = 5;

	private static readonly MapField<int, PVEMissionInfoConfigureMissionTargetConfigss>.Codec _map_missionTargetConfigs_codec = new MapField<int, PVEMissionInfoConfigureMissionTargetConfigss>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVEMissionInfoConfigureMissionTargetConfigss.Parser), 42u);

	private readonly MapField<int, PVEMissionInfoConfigureMissionTargetConfigss> missionTargetConfigs_ = new MapField<int, PVEMissionInfoConfigureMissionTargetConfigss>();

	public const int MissionFailedConfigsFieldNumber = 6;

	private static readonly MapField<int, PVEMissionInfoConfigureMissionFailedConfigss>.Codec _map_missionFailedConfigs_codec = new MapField<int, PVEMissionInfoConfigureMissionFailedConfigss>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVEMissionInfoConfigureMissionFailedConfigss.Parser), 50u);

	private readonly MapField<int, PVEMissionInfoConfigureMissionFailedConfigss> missionFailedConfigs_ = new MapField<int, PVEMissionInfoConfigureMissionFailedConfigss>();

	public const int MissionTargetFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_missionTarget_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> missionTarget_ = new RepeatedField<int>();

	public const int MissionParamFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_missionParam_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> missionParam_ = new RepeatedField<int>();

	public const int RewardDescIDFieldNumber = 9;

	private int rewardDescID_;

	public const int FailedDescIDFieldNumber = 10;

	private int failedDescID_;

	public const int RewardParamFieldNumber = 11;

	private static readonly FieldCodec<int> _repeated_rewardParam_codec = FieldCodec.ForSFixed32(90u);

	private readonly RepeatedField<int> rewardParam_ = new RepeatedField<int>();

	public const int RewardConfigsFieldNumber = 12;

	private static readonly MapField<int, PVEMissionInfoConfigureRewardConfigss>.Codec _map_rewardConfigs_codec = new MapField<int, PVEMissionInfoConfigureRewardConfigss>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVEMissionInfoConfigureRewardConfigss.Parser), 98u);

	private readonly MapField<int, PVEMissionInfoConfigureRewardConfigss> rewardConfigs_ = new MapField<int, PVEMissionInfoConfigureRewardConfigss>();

	public const int RewardFailedConfigsFieldNumber = 13;

	private static readonly MapField<int, PVEMissionInfoConfigureRewardFailedConfigss>.Codec _map_rewardFailedConfigs_codec = new MapField<int, PVEMissionInfoConfigureRewardFailedConfigss>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVEMissionInfoConfigureRewardFailedConfigss.Parser), 106u);

	private readonly MapField<int, PVEMissionInfoConfigureRewardFailedConfigss> rewardFailedConfigs_ = new MapField<int, PVEMissionInfoConfigureRewardFailedConfigss>();

	private MapField<int, RepeatedField<int>> _targetConfigsCache;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PVEMissionInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PVEMissionReflection.Descriptor.MessageTypes[0];

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
	public PVEMissionTriggerType PveMissionTriggerType
	{
		get
		{
			return pveMissionTriggerType_;
		}
		private set
		{
			pveMissionTriggerType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> Triggerparams => triggerparams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MissionDescID
	{
		get
		{
			return missionDescID_;
		}
		private set
		{
			missionDescID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVEMissionInfoConfigureMissionTargetConfigss> MissionTargetConfigs => missionTargetConfigs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVEMissionInfoConfigureMissionFailedConfigss> MissionFailedConfigs => missionFailedConfigs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MissionTarget => missionTarget_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> MissionParam => missionParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RewardDescID
	{
		get
		{
			return rewardDescID_;
		}
		private set
		{
			rewardDescID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FailedDescID
	{
		get
		{
			return failedDescID_;
		}
		private set
		{
			failedDescID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RewardParam => rewardParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVEMissionInfoConfigureRewardConfigss> RewardConfigs => rewardConfigs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, PVEMissionInfoConfigureRewardFailedConfigss> RewardFailedConfigs => rewardFailedConfigs_;

	public MapField<int, RepeatedField<int>> TargetConfigs
	{
		get
		{
			if (_targetConfigsCache == null)
			{
				_targetConfigsCache = new MapField<int, RepeatedField<int>>();
				foreach (int key in MissionTargetConfigs.Keys)
				{
					_targetConfigsCache[key] = MissionTargetConfigs[key].Values;
				}
			}
			return _targetConfigsCache;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEMissionInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEMissionInfoConfigure(PVEMissionInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		pveMissionTriggerType_ = other.pveMissionTriggerType_;
		triggerparams_ = other.triggerparams_.Clone();
		missionDescID_ = other.missionDescID_;
		missionTargetConfigs_ = other.missionTargetConfigs_.Clone();
		missionFailedConfigs_ = other.missionFailedConfigs_.Clone();
		missionTarget_ = other.missionTarget_.Clone();
		missionParam_ = other.missionParam_.Clone();
		rewardDescID_ = other.rewardDescID_;
		failedDescID_ = other.failedDescID_;
		rewardParam_ = other.rewardParam_.Clone();
		rewardConfigs_ = other.rewardConfigs_.Clone();
		rewardFailedConfigs_ = other.rewardFailedConfigs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEMissionInfoConfigure Clone()
	{
		return new PVEMissionInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PVEMissionInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PVEMissionInfoConfigure other)
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
		if (PveMissionTriggerType != other.PveMissionTriggerType)
		{
			return false;
		}
		if (!triggerparams_.Equals(other.triggerparams_))
		{
			return false;
		}
		if (MissionDescID != other.MissionDescID)
		{
			return false;
		}
		if (!MissionTargetConfigs.Equals(other.MissionTargetConfigs))
		{
			return false;
		}
		if (!MissionFailedConfigs.Equals(other.MissionFailedConfigs))
		{
			return false;
		}
		if (!missionTarget_.Equals(other.missionTarget_))
		{
			return false;
		}
		if (!missionParam_.Equals(other.missionParam_))
		{
			return false;
		}
		if (RewardDescID != other.RewardDescID)
		{
			return false;
		}
		if (FailedDescID != other.FailedDescID)
		{
			return false;
		}
		if (!rewardParam_.Equals(other.rewardParam_))
		{
			return false;
		}
		if (!RewardConfigs.Equals(other.RewardConfigs))
		{
			return false;
		}
		if (!RewardFailedConfigs.Equals(other.RewardFailedConfigs))
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
		if (PveMissionTriggerType != PVEMissionTriggerType.None)
		{
			num ^= PveMissionTriggerType.GetHashCode();
		}
		num ^= triggerparams_.GetHashCode();
		if (MissionDescID != 0)
		{
			num ^= MissionDescID.GetHashCode();
		}
		num ^= MissionTargetConfigs.GetHashCode();
		num ^= MissionFailedConfigs.GetHashCode();
		num ^= missionTarget_.GetHashCode();
		num ^= missionParam_.GetHashCode();
		if (RewardDescID != 0)
		{
			num ^= RewardDescID.GetHashCode();
		}
		if (FailedDescID != 0)
		{
			num ^= FailedDescID.GetHashCode();
		}
		num ^= rewardParam_.GetHashCode();
		num ^= RewardConfigs.GetHashCode();
		num ^= RewardFailedConfigs.GetHashCode();
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
		if (PveMissionTriggerType != PVEMissionTriggerType.None)
		{
			output.WriteRawTag(16);
			output.WriteEnum((int)PveMissionTriggerType);
		}
		triggerparams_.WriteTo(ref output, _repeated_triggerparams_codec);
		if (MissionDescID != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(MissionDescID);
		}
		missionTargetConfigs_.WriteTo(ref output, _map_missionTargetConfigs_codec);
		missionFailedConfigs_.WriteTo(ref output, _map_missionFailedConfigs_codec);
		missionTarget_.WriteTo(ref output, _repeated_missionTarget_codec);
		missionParam_.WriteTo(ref output, _repeated_missionParam_codec);
		if (RewardDescID != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(RewardDescID);
		}
		if (FailedDescID != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(FailedDescID);
		}
		rewardParam_.WriteTo(ref output, _repeated_rewardParam_codec);
		rewardConfigs_.WriteTo(ref output, _map_rewardConfigs_codec);
		rewardFailedConfigs_.WriteTo(ref output, _map_rewardFailedConfigs_codec);
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
		if (PveMissionTriggerType != PVEMissionTriggerType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PveMissionTriggerType);
		}
		num += triggerparams_.CalculateSize(_repeated_triggerparams_codec);
		if (MissionDescID != 0)
		{
			num += 5;
		}
		num += missionTargetConfigs_.CalculateSize(_map_missionTargetConfigs_codec);
		num += missionFailedConfigs_.CalculateSize(_map_missionFailedConfigs_codec);
		num += missionTarget_.CalculateSize(_repeated_missionTarget_codec);
		num += missionParam_.CalculateSize(_repeated_missionParam_codec);
		if (RewardDescID != 0)
		{
			num += 5;
		}
		if (FailedDescID != 0)
		{
			num += 5;
		}
		num += rewardParam_.CalculateSize(_repeated_rewardParam_codec);
		num += rewardConfigs_.CalculateSize(_map_rewardConfigs_codec);
		num += rewardFailedConfigs_.CalculateSize(_map_rewardFailedConfigs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(PVEMissionInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.PveMissionTriggerType != PVEMissionTriggerType.None)
			{
				PveMissionTriggerType = other.PveMissionTriggerType;
			}
			triggerparams_.Add(other.triggerparams_);
			if (other.MissionDescID != 0)
			{
				MissionDescID = other.MissionDescID;
			}
			missionTargetConfigs_.MergeFrom(other.missionTargetConfigs_);
			missionFailedConfigs_.MergeFrom(other.missionFailedConfigs_);
			missionTarget_.Add(other.missionTarget_);
			missionParam_.Add(other.missionParam_);
			if (other.RewardDescID != 0)
			{
				RewardDescID = other.RewardDescID;
			}
			if (other.FailedDescID != 0)
			{
				FailedDescID = other.FailedDescID;
			}
			rewardParam_.Add(other.rewardParam_);
			rewardConfigs_.MergeFrom(other.rewardConfigs_);
			rewardFailedConfigs_.MergeFrom(other.rewardFailedConfigs_);
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
				PveMissionTriggerType = (PVEMissionTriggerType)input.ReadEnum();
				break;
			case 26u:
			case 29u:
				triggerparams_.AddEntriesFrom(ref input, _repeated_triggerparams_codec);
				break;
			case 37u:
				MissionDescID = input.ReadSFixed32();
				break;
			case 42u:
				missionTargetConfigs_.AddEntriesFrom(ref input, _map_missionTargetConfigs_codec);
				break;
			case 50u:
				missionFailedConfigs_.AddEntriesFrom(ref input, _map_missionFailedConfigs_codec);
				break;
			case 58u:
			case 61u:
				missionTarget_.AddEntriesFrom(ref input, _repeated_missionTarget_codec);
				break;
			case 66u:
			case 69u:
				missionParam_.AddEntriesFrom(ref input, _repeated_missionParam_codec);
				break;
			case 77u:
				RewardDescID = input.ReadSFixed32();
				break;
			case 85u:
				FailedDescID = input.ReadSFixed32();
				break;
			case 90u:
			case 93u:
				rewardParam_.AddEntriesFrom(ref input, _repeated_rewardParam_codec);
				break;
			case 98u:
				rewardConfigs_.AddEntriesFrom(ref input, _map_rewardConfigs_codec);
				break;
			case 106u:
				rewardFailedConfigs_.AddEntriesFrom(ref input, _map_rewardFailedConfigs_codec);
				break;
			}
		}
	}
}
