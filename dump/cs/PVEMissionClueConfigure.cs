using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class PVEMissionClueConfigure : IMessage<PVEMissionClueConfigure>, IMessage, IEquatable<PVEMissionClueConfigure>, IDeepCloneable<PVEMissionClueConfigure>, IBufferMessage, IPVEMissionTargetConfigs
{
	private static readonly MessageParser<PVEMissionClueConfigure> _parser = new MessageParser<PVEMissionClueConfigure>(() => new PVEMissionClueConfigure());

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

	private static readonly MapField<int, PVEMissionClueConfigureMissionTargetConfigss>.Codec _map_missionTargetConfigs_codec = new MapField<int, PVEMissionClueConfigureMissionTargetConfigss>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, PVEMissionClueConfigureMissionTargetConfigss.Parser), 42u);

	private readonly MapField<int, PVEMissionClueConfigureMissionTargetConfigss> missionTargetConfigs_ = new MapField<int, PVEMissionClueConfigureMissionTargetConfigss>();

	public const int RewardConfigsFieldNumber = 6;

	private int rewardConfigs_;

	private MapField<int, RepeatedField<int>> _targetConfigsCache;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<PVEMissionClueConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PVEMissionReflection.Descriptor.MessageTypes[6];

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
	public MapField<int, PVEMissionClueConfigureMissionTargetConfigss> MissionTargetConfigs => missionTargetConfigs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RewardConfigs
	{
		get
		{
			return rewardConfigs_;
		}
		private set
		{
			rewardConfigs_ = value;
		}
	}

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
	public PVEMissionClueConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEMissionClueConfigure(PVEMissionClueConfigure other)
		: this()
	{
		id_ = other.id_;
		pveMissionTriggerType_ = other.pveMissionTriggerType_;
		triggerparams_ = other.triggerparams_.Clone();
		missionDescID_ = other.missionDescID_;
		missionTargetConfigs_ = other.missionTargetConfigs_.Clone();
		rewardConfigs_ = other.rewardConfigs_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEMissionClueConfigure Clone()
	{
		return new PVEMissionClueConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PVEMissionClueConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(PVEMissionClueConfigure other)
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
		if (RewardConfigs != other.RewardConfigs)
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
		if (RewardConfigs != 0)
		{
			num ^= RewardConfigs.GetHashCode();
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
		if (RewardConfigs != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(RewardConfigs);
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
		if (RewardConfigs != 0)
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
	public void MergeFrom(PVEMissionClueConfigure other)
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
			if (other.RewardConfigs != 0)
			{
				RewardConfigs = other.RewardConfigs;
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
			case 53u:
				RewardConfigs = input.ReadSFixed32();
				break;
			}
		}
	}
}
