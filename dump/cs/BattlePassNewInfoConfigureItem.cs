using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BattlePassNewInfoConfigureItem : IMessage<BattlePassNewInfoConfigureItem>, IMessage, IEquatable<BattlePassNewInfoConfigureItem>, IDeepCloneable<BattlePassNewInfoConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<BattlePassNewInfoConfigureItem> _parser = new MessageParser<BattlePassNewInfoConfigureItem>(() => new BattlePassNewInfoConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int TaskIdFieldNumber = 1;

	private int taskId_;

	public const int FreeRewardsFieldNumber = 2;

	private static readonly MapField<int, int>.Codec _map_freeRewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 18u);

	private readonly MapField<int, int> freeRewards_ = new MapField<int, int>();

	public const int PremiumRewardsFieldNumber = 3;

	private static readonly MapField<int, int>.Codec _map_premiumRewards_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 26u);

	private readonly MapField<int, int> premiumRewards_ = new MapField<int, int>();

	public const int ActivityIdFieldNumber = 4;

	private int activityId_;

	public const int ParamFieldNumber = 5;

	private int param_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassNewInfoConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassNewReflection.Descriptor.MessageTypes[1];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TaskId
	{
		get
		{
			return taskId_;
		}
		private set
		{
			taskId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> FreeRewards => freeRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> PremiumRewards => premiumRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActivityId
	{
		get
		{
			return activityId_;
		}
		private set
		{
			activityId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Param
	{
		get
		{
			return param_;
		}
		private set
		{
			param_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassNewInfoConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassNewInfoConfigureItem(BattlePassNewInfoConfigureItem other)
		: this()
	{
		taskId_ = other.taskId_;
		freeRewards_ = other.freeRewards_.Clone();
		premiumRewards_ = other.premiumRewards_.Clone();
		activityId_ = other.activityId_;
		param_ = other.param_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassNewInfoConfigureItem Clone()
	{
		return new BattlePassNewInfoConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassNewInfoConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassNewInfoConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (TaskId != other.TaskId)
		{
			return false;
		}
		if (!FreeRewards.Equals(other.FreeRewards))
		{
			return false;
		}
		if (!PremiumRewards.Equals(other.PremiumRewards))
		{
			return false;
		}
		if (ActivityId != other.ActivityId)
		{
			return false;
		}
		if (Param != other.Param)
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
		if (TaskId != 0)
		{
			num ^= TaskId.GetHashCode();
		}
		num ^= FreeRewards.GetHashCode();
		num ^= PremiumRewards.GetHashCode();
		if (ActivityId != 0)
		{
			num ^= ActivityId.GetHashCode();
		}
		if (Param != 0)
		{
			num ^= Param.GetHashCode();
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
		if (TaskId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(TaskId);
		}
		freeRewards_.WriteTo(ref output, _map_freeRewards_codec);
		premiumRewards_.WriteTo(ref output, _map_premiumRewards_codec);
		if (ActivityId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(ActivityId);
		}
		if (Param != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Param);
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
		if (TaskId != 0)
		{
			num += 5;
		}
		num += freeRewards_.CalculateSize(_map_freeRewards_codec);
		num += premiumRewards_.CalculateSize(_map_premiumRewards_codec);
		if (ActivityId != 0)
		{
			num += 5;
		}
		if (Param != 0)
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
	public void MergeFrom(BattlePassNewInfoConfigureItem other)
	{
		if (other != null)
		{
			if (other.TaskId != 0)
			{
				TaskId = other.TaskId;
			}
			freeRewards_.MergeFrom(other.freeRewards_);
			premiumRewards_.MergeFrom(other.premiumRewards_);
			if (other.ActivityId != 0)
			{
				ActivityId = other.ActivityId;
			}
			if (other.Param != 0)
			{
				Param = other.Param;
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
				TaskId = input.ReadSFixed32();
				break;
			case 18u:
				freeRewards_.AddEntriesFrom(ref input, _map_freeRewards_codec);
				break;
			case 26u:
				premiumRewards_.AddEntriesFrom(ref input, _map_premiumRewards_codec);
				break;
			case 37u:
				ActivityId = input.ReadSFixed32();
				break;
			case 45u:
				Param = input.ReadSFixed32();
				break;
			}
		}
	}
}
