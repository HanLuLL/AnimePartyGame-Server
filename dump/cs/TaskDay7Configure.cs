using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class TaskDay7Configure : IMessage<TaskDay7Configure>, IMessage, IEquatable<TaskDay7Configure>, IDeepCloneable<TaskDay7Configure>, IBufferMessage
{
	private static readonly MessageParser<TaskDay7Configure> _parser = new MessageParser<TaskDay7Configure>(() => new TaskDay7Configure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int DayFieldNumber = 2;

	private int day_;

	public const int ConditionTypeFieldNumber = 3;

	private ConditionType conditionType_;

	public const int ParamFieldNumber = 4;

	private int param_;

	public const int RefFieldNumber = 5;

	private int ref_;

	public const int NameIDFieldNumber = 6;

	private int nameID_;

	public const int DescIdFieldNumber = 7;

	private int descId_;

	public const int RewardFieldNumber = 8;

	private static readonly MapField<int, int>.Codec _map_reward_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 66u);

	private readonly MapField<int, int> reward_ = new MapField<int, int>();

	public const int WayFieldNumber = 9;

	private int way_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TaskDay7Configure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => TaskReflection.Descriptor.MessageTypes[3];

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
	public int Day
	{
		get
		{
			return day_;
		}
		private set
		{
			day_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConditionType ConditionType
	{
		get
		{
			return conditionType_;
		}
		private set
		{
			conditionType_ = value;
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
	public int Ref
	{
		get
		{
			return ref_;
		}
		private set
		{
			ref_ = value;
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
	public TaskDay7Configure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskDay7Configure(TaskDay7Configure other)
		: this()
	{
		id_ = other.id_;
		day_ = other.day_;
		conditionType_ = other.conditionType_;
		param_ = other.param_;
		ref_ = other.ref_;
		nameID_ = other.nameID_;
		descId_ = other.descId_;
		reward_ = other.reward_.Clone();
		way_ = other.way_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskDay7Configure Clone()
	{
		return new TaskDay7Configure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TaskDay7Configure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TaskDay7Configure other)
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
		if (Day != other.Day)
		{
			return false;
		}
		if (ConditionType != other.ConditionType)
		{
			return false;
		}
		if (Param != other.Param)
		{
			return false;
		}
		if (Ref != other.Ref)
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
		if (Day != 0)
		{
			num ^= Day.GetHashCode();
		}
		if (ConditionType != ConditionType.None)
		{
			num ^= ConditionType.GetHashCode();
		}
		if (Param != 0)
		{
			num ^= Param.GetHashCode();
		}
		if (Ref != 0)
		{
			num ^= Ref.GetHashCode();
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
		if (Day != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Day);
		}
		if (ConditionType != ConditionType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)ConditionType);
		}
		if (Param != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Param);
		}
		if (Ref != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Ref);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(NameID);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(DescId);
		}
		reward_.WriteTo(ref output, _map_reward_codec);
		if (Way != 0)
		{
			output.WriteRawTag(77);
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
		if (Day != 0)
		{
			num += 5;
		}
		if (ConditionType != ConditionType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ConditionType);
		}
		if (Param != 0)
		{
			num += 5;
		}
		if (Ref != 0)
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
	public void MergeFrom(TaskDay7Configure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.Day != 0)
			{
				Day = other.Day;
			}
			if (other.ConditionType != ConditionType.None)
			{
				ConditionType = other.ConditionType;
			}
			if (other.Param != 0)
			{
				Param = other.Param;
			}
			if (other.Ref != 0)
			{
				Ref = other.Ref;
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
				Day = input.ReadSFixed32();
				break;
			case 24u:
				ConditionType = (ConditionType)input.ReadEnum();
				break;
			case 37u:
				Param = input.ReadSFixed32();
				break;
			case 45u:
				Ref = input.ReadSFixed32();
				break;
			case 53u:
				NameID = input.ReadSFixed32();
				break;
			case 61u:
				DescId = input.ReadSFixed32();
				break;
			case 66u:
				reward_.AddEntriesFrom(ref input, _map_reward_codec);
				break;
			case 77u:
				Way = input.ReadSFixed32();
				break;
			}
		}
	}
}
