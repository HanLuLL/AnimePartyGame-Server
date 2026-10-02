using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Core.Tutorial.Buff;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Buff : IMessage<Buff>, IMessage, IEquatable<Buff>, IDeepCloneable<Buff>, IBufferMessage
{
	private static readonly MessageParser<Buff> _parser = new MessageParser<Buff>(() => new Buff());

	private UnknownFieldSet _unknownFields;

	public const int UniqueIdFieldNumber = 1;

	private long uniqueId_;

	public const int BuffIdFieldNumber = 2;

	private int buffId_;

	public const int ParamsFieldNumber = 3;

	private static readonly MapField<int, long>.Codec _map_params_codec = new MapField<int, long>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed64(17u, 0L), 26u);

	private readonly MapField<int, long> params_ = new MapField<int, long>();

	public const int RestoreParamsFieldNumber = 4;

	private static readonly MapField<int, long>.Codec _map_restoreParams_codec = new MapField<int, long>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed64(17u, 0L), 34u);

	private readonly MapField<int, long> restoreParams_ = new MapField<int, long>();

	public const int KeepRoundFieldNumber = 5;

	private int keepRound_;

	public const int DelayRoundFieldNumber = 6;

	private int delayRound_;

	public const int UseTimeFieldNumber = 7;

	private int useTime_;

	public const int TargetIdsFieldNumber = 8;

	private static readonly FieldCodec<long> _repeated_targetIds_codec = FieldCodec.ForSFixed64(66u);

	private readonly RepeatedField<long> targetIds_ = new RepeatedField<long>();

	public const int PriorityFieldNumber = 9;

	private int priority_;

	public const int NodeIdFieldNumber = 10;

	private int nodeId_;

	public const int ProgressFieldNumber = 11;

	private int progress_;

	public const int RelationIdFieldNumber = 12;

	private string relationId_ = "";

	public const int IsSakuraFieldNumber = 13;

	private bool isSakura_;

	public const int BuffIndexFieldNumber = 14;

	private int buffIndex_;

	public const int CanRangeDamageFieldNumber = 15;

	private bool canRangeDamage_;

	public const int RelativeBuffUidsFieldNumber = 18;

	private static readonly MapField<int, long>.Codec _map_relativeBuffUids_codec = new MapField<int, long>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed64(17u, 0L), 146u);

	private readonly MapField<int, long> relativeBuffUids_ = new MapField<int, long>();

	public const int ChainFieldNumber = 49;

	private static readonly FieldCodec<buff_source> _repeated_chain_codec = FieldCodec.ForMessage(394u, buff_source.Parser);

	private readonly RepeatedField<buff_source> chain_ = new RepeatedField<buff_source>();

	public const int SourceFieldNumber = 50;

	private buff_source source_;

	public const int KeyFieldNumber = 51;

	private string key_ = "";

	public long PlayerId;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Buff> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[86];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long UniqueId
	{
		get
		{
			return uniqueId_;
		}
		set
		{
			uniqueId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuffId
	{
		get
		{
			return buffId_;
		}
		set
		{
			buffId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, long> Params => params_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, long> RestoreParams => restoreParams_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int KeepRound
	{
		get
		{
			return keepRound_;
		}
		set
		{
			keepRound_ = value;
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
		set
		{
			delayRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseTime
	{
		get
		{
			return useTime_;
		}
		set
		{
			useTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> TargetIds => targetIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Priority
	{
		get
		{
			return priority_;
		}
		set
		{
			priority_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int NodeId
	{
		get
		{
			return nodeId_;
		}
		set
		{
			nodeId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Progress
	{
		get
		{
			return progress_;
		}
		set
		{
			progress_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string RelationId
	{
		get
		{
			return relationId_;
		}
		set
		{
			relationId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsSakura
	{
		get
		{
			return isSakura_;
		}
		set
		{
			isSakura_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuffIndex
	{
		get
		{
			return buffIndex_;
		}
		set
		{
			buffIndex_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanRangeDamage
	{
		get
		{
			return canRangeDamage_;
		}
		set
		{
			canRangeDamage_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, long> RelativeBuffUids => relativeBuffUids_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<buff_source> Chain => chain_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public buff_source Source
	{
		get
		{
			return source_;
		}
		set
		{
			source_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Key
	{
		get
		{
			return key_;
		}
		set
		{
			key_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	public BuffData BuffData { get; private set; }

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Buff()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Buff(Buff other)
		: this()
	{
		uniqueId_ = other.uniqueId_;
		buffId_ = other.buffId_;
		params_ = other.params_.Clone();
		restoreParams_ = other.restoreParams_.Clone();
		keepRound_ = other.keepRound_;
		delayRound_ = other.delayRound_;
		useTime_ = other.useTime_;
		targetIds_ = other.targetIds_.Clone();
		priority_ = other.priority_;
		nodeId_ = other.nodeId_;
		progress_ = other.progress_;
		relationId_ = other.relationId_;
		isSakura_ = other.isSakura_;
		buffIndex_ = other.buffIndex_;
		canRangeDamage_ = other.canRangeDamage_;
		relativeBuffUids_ = other.relativeBuffUids_.Clone();
		chain_ = other.chain_.Clone();
		source_ = ((other.source_ != null) ? other.source_.Clone() : null);
		key_ = other.key_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Buff Clone()
	{
		return new Buff(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Buff);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Buff other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (UniqueId != other.UniqueId)
		{
			return false;
		}
		if (BuffId != other.BuffId)
		{
			return false;
		}
		if (!Params.Equals(other.Params))
		{
			return false;
		}
		if (!RestoreParams.Equals(other.RestoreParams))
		{
			return false;
		}
		if (KeepRound != other.KeepRound)
		{
			return false;
		}
		if (DelayRound != other.DelayRound)
		{
			return false;
		}
		if (UseTime != other.UseTime)
		{
			return false;
		}
		if (!targetIds_.Equals(other.targetIds_))
		{
			return false;
		}
		if (Priority != other.Priority)
		{
			return false;
		}
		if (NodeId != other.NodeId)
		{
			return false;
		}
		if (Progress != other.Progress)
		{
			return false;
		}
		if (RelationId != other.RelationId)
		{
			return false;
		}
		if (IsSakura != other.IsSakura)
		{
			return false;
		}
		if (BuffIndex != other.BuffIndex)
		{
			return false;
		}
		if (CanRangeDamage != other.CanRangeDamage)
		{
			return false;
		}
		if (!RelativeBuffUids.Equals(other.RelativeBuffUids))
		{
			return false;
		}
		if (!chain_.Equals(other.chain_))
		{
			return false;
		}
		if (!object.Equals(Source, other.Source))
		{
			return false;
		}
		if (Key != other.Key)
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
		if (UniqueId != 0L)
		{
			num ^= UniqueId.GetHashCode();
		}
		if (BuffId != 0)
		{
			num ^= BuffId.GetHashCode();
		}
		num ^= Params.GetHashCode();
		num ^= RestoreParams.GetHashCode();
		if (KeepRound != 0)
		{
			num ^= KeepRound.GetHashCode();
		}
		if (DelayRound != 0)
		{
			num ^= DelayRound.GetHashCode();
		}
		if (UseTime != 0)
		{
			num ^= UseTime.GetHashCode();
		}
		num ^= targetIds_.GetHashCode();
		if (Priority != 0)
		{
			num ^= Priority.GetHashCode();
		}
		if (NodeId != 0)
		{
			num ^= NodeId.GetHashCode();
		}
		if (Progress != 0)
		{
			num ^= Progress.GetHashCode();
		}
		if (RelationId.Length != 0)
		{
			num ^= RelationId.GetHashCode();
		}
		if (IsSakura)
		{
			num ^= IsSakura.GetHashCode();
		}
		if (BuffIndex != 0)
		{
			num ^= BuffIndex.GetHashCode();
		}
		if (CanRangeDamage)
		{
			num ^= CanRangeDamage.GetHashCode();
		}
		num ^= RelativeBuffUids.GetHashCode();
		num ^= chain_.GetHashCode();
		if (source_ != null)
		{
			num ^= Source.GetHashCode();
		}
		if (Key.Length != 0)
		{
			num ^= Key.GetHashCode();
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
		if (UniqueId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(UniqueId);
		}
		if (BuffId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(BuffId);
		}
		params_.WriteTo(ref output, _map_params_codec);
		restoreParams_.WriteTo(ref output, _map_restoreParams_codec);
		if (KeepRound != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(KeepRound);
		}
		if (DelayRound != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(DelayRound);
		}
		if (UseTime != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(UseTime);
		}
		targetIds_.WriteTo(ref output, _repeated_targetIds_codec);
		if (Priority != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(Priority);
		}
		if (NodeId != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(NodeId);
		}
		if (Progress != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(Progress);
		}
		if (RelationId.Length != 0)
		{
			output.WriteRawTag(98);
			output.WriteString(RelationId);
		}
		if (IsSakura)
		{
			output.WriteRawTag(104);
			output.WriteBool(IsSakura);
		}
		if (BuffIndex != 0)
		{
			output.WriteRawTag(117);
			output.WriteSFixed32(BuffIndex);
		}
		if (CanRangeDamage)
		{
			output.WriteRawTag(120);
			output.WriteBool(CanRangeDamage);
		}
		relativeBuffUids_.WriteTo(ref output, _map_relativeBuffUids_codec);
		chain_.WriteTo(ref output, _repeated_chain_codec);
		if (source_ != null)
		{
			output.WriteRawTag(146, 3);
			output.WriteMessage(Source);
		}
		if (Key.Length != 0)
		{
			output.WriteRawTag(154, 3);
			output.WriteString(Key);
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
		if (UniqueId != 0L)
		{
			num += 9;
		}
		if (BuffId != 0)
		{
			num += 5;
		}
		num += params_.CalculateSize(_map_params_codec);
		num += restoreParams_.CalculateSize(_map_restoreParams_codec);
		if (KeepRound != 0)
		{
			num += 5;
		}
		if (DelayRound != 0)
		{
			num += 5;
		}
		if (UseTime != 0)
		{
			num += 5;
		}
		num += targetIds_.CalculateSize(_repeated_targetIds_codec);
		if (Priority != 0)
		{
			num += 5;
		}
		if (NodeId != 0)
		{
			num += 5;
		}
		if (Progress != 0)
		{
			num += 5;
		}
		if (RelationId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(RelationId);
		}
		if (IsSakura)
		{
			num += 2;
		}
		if (BuffIndex != 0)
		{
			num += 5;
		}
		if (CanRangeDamage)
		{
			num += 2;
		}
		num += relativeBuffUids_.CalculateSize(_map_relativeBuffUids_codec);
		num += chain_.CalculateSize(_repeated_chain_codec);
		if (source_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Source);
		}
		if (Key.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Key);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(Buff other)
	{
		if (other == null)
		{
			return;
		}
		if (other.UniqueId != 0L)
		{
			UniqueId = other.UniqueId;
		}
		if (other.BuffId != 0)
		{
			BuffId = other.BuffId;
		}
		params_.MergeFrom(other.params_);
		restoreParams_.MergeFrom(other.restoreParams_);
		if (other.KeepRound != 0)
		{
			KeepRound = other.KeepRound;
		}
		if (other.DelayRound != 0)
		{
			DelayRound = other.DelayRound;
		}
		if (other.UseTime != 0)
		{
			UseTime = other.UseTime;
		}
		targetIds_.Add(other.targetIds_);
		if (other.Priority != 0)
		{
			Priority = other.Priority;
		}
		if (other.NodeId != 0)
		{
			NodeId = other.NodeId;
		}
		if (other.Progress != 0)
		{
			Progress = other.Progress;
		}
		if (other.RelationId.Length != 0)
		{
			RelationId = other.RelationId;
		}
		if (other.IsSakura)
		{
			IsSakura = other.IsSakura;
		}
		if (other.BuffIndex != 0)
		{
			BuffIndex = other.BuffIndex;
		}
		if (other.CanRangeDamage)
		{
			CanRangeDamage = other.CanRangeDamage;
		}
		relativeBuffUids_.MergeFrom(other.relativeBuffUids_);
		chain_.Add(other.chain_);
		if (other.source_ != null)
		{
			if (source_ == null)
			{
				Source = new buff_source();
			}
			Source.MergeFrom(other.Source);
		}
		if (other.Key.Length != 0)
		{
			Key = other.Key;
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
			case 9u:
				UniqueId = input.ReadSFixed64();
				break;
			case 21u:
				BuffId = input.ReadSFixed32();
				break;
			case 26u:
				params_.AddEntriesFrom(ref input, _map_params_codec);
				break;
			case 34u:
				restoreParams_.AddEntriesFrom(ref input, _map_restoreParams_codec);
				break;
			case 45u:
				KeepRound = input.ReadSFixed32();
				break;
			case 53u:
				DelayRound = input.ReadSFixed32();
				break;
			case 61u:
				UseTime = input.ReadSFixed32();
				break;
			case 65u:
			case 66u:
				targetIds_.AddEntriesFrom(ref input, _repeated_targetIds_codec);
				break;
			case 77u:
				Priority = input.ReadSFixed32();
				break;
			case 85u:
				NodeId = input.ReadSFixed32();
				break;
			case 93u:
				Progress = input.ReadSFixed32();
				break;
			case 98u:
				RelationId = input.ReadString();
				break;
			case 104u:
				IsSakura = input.ReadBool();
				break;
			case 117u:
				BuffIndex = input.ReadSFixed32();
				break;
			case 120u:
				CanRangeDamage = input.ReadBool();
				break;
			case 146u:
				relativeBuffUids_.AddEntriesFrom(ref input, _map_relativeBuffUids_codec);
				break;
			case 394u:
				chain_.AddEntriesFrom(ref input, _repeated_chain_codec);
				break;
			case 402u:
				if (source_ == null)
				{
					Source = new buff_source();
				}
				input.ReadMessage(Source);
				break;
			case 410u:
				Key = input.ReadString();
				break;
			}
		}
	}

	public void InitBuffData(long playerId)
	{
		PlayerId = playerId;
		BuffData = ReflectionHelper.GetClassInstance<BuffData>($"Core.Tutorial.Buff.BuffData_{BuffId}");
		if (BuffData != null)
		{
			BuffData.Initialize(BuffId);
			keepRound_ = BuffData.Config.KeepRound;
			delayRound_ = BuffData.Config.DelayRound;
		}
	}
}
