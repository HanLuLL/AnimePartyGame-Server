using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Day7Reward : IMessage<Day7Reward>, IMessage, IEquatable<Day7Reward>, IDeepCloneable<Day7Reward>, IBufferMessage
{
	private static readonly MessageParser<Day7Reward> _parser = new MessageParser<Day7Reward>(() => new Day7Reward());

	private UnknownFieldSet _unknownFields;

	public const int GoodsIdFieldNumber = 1;

	private int goodsId_;

	public const int CreateTimeFieldNumber = 2;

	private long createTime_;

	public const int MaxRewardDayFieldNumber = 3;

	private int maxRewardDay_;

	public const int RewardDayFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_rewardDay_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> rewardDay_ = new RepeatedField<int>();

	public const int TodayRewardFieldNumber = 5;

	private bool todayReward_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Day7Reward> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[22];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GoodsId
	{
		get
		{
			return goodsId_;
		}
		set
		{
			goodsId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long CreateTime
	{
		get
		{
			return createTime_;
		}
		set
		{
			createTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxRewardDay
	{
		get
		{
			return maxRewardDay_;
		}
		set
		{
			maxRewardDay_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> RewardDay => rewardDay_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool TodayReward
	{
		get
		{
			return todayReward_;
		}
		set
		{
			todayReward_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7Reward()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7Reward(Day7Reward other)
		: this()
	{
		goodsId_ = other.goodsId_;
		createTime_ = other.createTime_;
		maxRewardDay_ = other.maxRewardDay_;
		rewardDay_ = other.rewardDay_.Clone();
		todayReward_ = other.todayReward_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7Reward Clone()
	{
		return new Day7Reward(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Day7Reward);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Day7Reward other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (GoodsId != other.GoodsId)
		{
			return false;
		}
		if (CreateTime != other.CreateTime)
		{
			return false;
		}
		if (MaxRewardDay != other.MaxRewardDay)
		{
			return false;
		}
		if (!rewardDay_.Equals(other.rewardDay_))
		{
			return false;
		}
		if (TodayReward != other.TodayReward)
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
		if (GoodsId != 0)
		{
			num ^= GoodsId.GetHashCode();
		}
		if (CreateTime != 0L)
		{
			num ^= CreateTime.GetHashCode();
		}
		if (MaxRewardDay != 0)
		{
			num ^= MaxRewardDay.GetHashCode();
		}
		num ^= rewardDay_.GetHashCode();
		if (TodayReward)
		{
			num ^= TodayReward.GetHashCode();
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
		if (GoodsId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(GoodsId);
		}
		if (CreateTime != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(CreateTime);
		}
		if (MaxRewardDay != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MaxRewardDay);
		}
		rewardDay_.WriteTo(ref output, _repeated_rewardDay_codec);
		if (TodayReward)
		{
			output.WriteRawTag(40);
			output.WriteBool(TodayReward);
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
		if (GoodsId != 0)
		{
			num += 5;
		}
		if (CreateTime != 0L)
		{
			num += 9;
		}
		if (MaxRewardDay != 0)
		{
			num += 5;
		}
		num += rewardDay_.CalculateSize(_repeated_rewardDay_codec);
		if (TodayReward)
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
	public void MergeFrom(Day7Reward other)
	{
		if (other != null)
		{
			if (other.GoodsId != 0)
			{
				GoodsId = other.GoodsId;
			}
			if (other.CreateTime != 0L)
			{
				CreateTime = other.CreateTime;
			}
			if (other.MaxRewardDay != 0)
			{
				MaxRewardDay = other.MaxRewardDay;
			}
			rewardDay_.Add(other.rewardDay_);
			if (other.TodayReward)
			{
				TodayReward = other.TodayReward;
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
				GoodsId = input.ReadSFixed32();
				break;
			case 17u:
				CreateTime = input.ReadSFixed64();
				break;
			case 29u:
				MaxRewardDay = input.ReadSFixed32();
				break;
			case 34u:
			case 37u:
				rewardDay_.AddEntriesFrom(ref input, _repeated_rewardDay_codec);
				break;
			case 40u:
				TodayReward = input.ReadBool();
				break;
			}
		}
	}
}
