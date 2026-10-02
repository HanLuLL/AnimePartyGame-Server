using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class BattlePass : IMessage<BattlePass>, IMessage, IEquatable<BattlePass>, IDeepCloneable<BattlePass>, IBufferMessage
{
	private static readonly MessageParser<BattlePass> _parser = new MessageParser<BattlePass>(() => new BattlePass());

	private UnknownFieldSet _unknownFields;

	public const int DefIdFieldNumber = 1;

	private int defId_;

	public const int LvFieldNumber = 2;

	private int lv_;

	public const int ExpFieldNumber = 3;

	private int exp_;

	public const int GearFieldNumber = 4;

	private int gear_;

	public const int RewardIdsFieldNumber = 5;

	private static readonly MapField<int, int>.Codec _map_rewardIds_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 42u);

	private readonly MapField<int, int> rewardIds_ = new MapField<int, int>();

	public const int TaskFieldNumber = 10;

	private static readonly MapField<int, int>.Codec _map_task_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<int, int> task_ = new MapField<int, int>();

	public const int TaskRewardIsFieldNumber = 11;

	private static readonly FieldCodec<int> _repeated_taskRewardIs_codec = FieldCodec.ForSFixed32(90u);

	private readonly RepeatedField<int> taskRewardIs_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePass> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[16];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DefId
	{
		get
		{
			return defId_;
		}
		set
		{
			defId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Lv
	{
		get
		{
			return lv_;
		}
		set
		{
			lv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Exp
	{
		get
		{
			return exp_;
		}
		set
		{
			exp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Gear
	{
		get
		{
			return gear_;
		}
		set
		{
			gear_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> RewardIds => rewardIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Task => task_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TaskRewardIs => taskRewardIs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePass()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePass(BattlePass other)
		: this()
	{
		defId_ = other.defId_;
		lv_ = other.lv_;
		exp_ = other.exp_;
		gear_ = other.gear_;
		rewardIds_ = other.rewardIds_.Clone();
		task_ = other.task_.Clone();
		taskRewardIs_ = other.taskRewardIs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePass Clone()
	{
		return new BattlePass(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePass);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePass other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DefId != other.DefId)
		{
			return false;
		}
		if (Lv != other.Lv)
		{
			return false;
		}
		if (Exp != other.Exp)
		{
			return false;
		}
		if (Gear != other.Gear)
		{
			return false;
		}
		if (!RewardIds.Equals(other.RewardIds))
		{
			return false;
		}
		if (!Task.Equals(other.Task))
		{
			return false;
		}
		if (!taskRewardIs_.Equals(other.taskRewardIs_))
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
		if (DefId != 0)
		{
			num ^= DefId.GetHashCode();
		}
		if (Lv != 0)
		{
			num ^= Lv.GetHashCode();
		}
		if (Exp != 0)
		{
			num ^= Exp.GetHashCode();
		}
		if (Gear != 0)
		{
			num ^= Gear.GetHashCode();
		}
		num ^= RewardIds.GetHashCode();
		num ^= Task.GetHashCode();
		num ^= taskRewardIs_.GetHashCode();
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
		if (DefId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(DefId);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Lv);
		}
		if (Exp != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Exp);
		}
		if (Gear != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Gear);
		}
		rewardIds_.WriteTo(ref output, _map_rewardIds_codec);
		task_.WriteTo(ref output, _map_task_codec);
		taskRewardIs_.WriteTo(ref output, _repeated_taskRewardIs_codec);
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
		if (DefId != 0)
		{
			num += 5;
		}
		if (Lv != 0)
		{
			num += 5;
		}
		if (Exp != 0)
		{
			num += 5;
		}
		if (Gear != 0)
		{
			num += 5;
		}
		num += rewardIds_.CalculateSize(_map_rewardIds_codec);
		num += task_.CalculateSize(_map_task_codec);
		num += taskRewardIs_.CalculateSize(_repeated_taskRewardIs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattlePass other)
	{
		if (other != null)
		{
			if (other.DefId != 0)
			{
				DefId = other.DefId;
			}
			if (other.Lv != 0)
			{
				Lv = other.Lv;
			}
			if (other.Exp != 0)
			{
				Exp = other.Exp;
			}
			if (other.Gear != 0)
			{
				Gear = other.Gear;
			}
			rewardIds_.MergeFrom(other.rewardIds_);
			task_.MergeFrom(other.task_);
			taskRewardIs_.Add(other.taskRewardIs_);
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
				DefId = input.ReadSFixed32();
				break;
			case 21u:
				Lv = input.ReadSFixed32();
				break;
			case 29u:
				Exp = input.ReadSFixed32();
				break;
			case 37u:
				Gear = input.ReadSFixed32();
				break;
			case 42u:
				rewardIds_.AddEntriesFrom(ref input, _map_rewardIds_codec);
				break;
			case 82u:
				task_.AddEntriesFrom(ref input, _map_task_codec);
				break;
			case 90u:
			case 93u:
				taskRewardIs_.AddEntriesFrom(ref input, _repeated_taskRewardIs_codec);
				break;
			}
		}
	}
}
