using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class BattlePassTaskConfigureItem : IMessage<BattlePassTaskConfigureItem>, IMessage, IEquatable<BattlePassTaskConfigureItem>, IDeepCloneable<BattlePassTaskConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<BattlePassTaskConfigureItem> _parser = new MessageParser<BattlePassTaskConfigureItem>(() => new BattlePassTaskConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int OrderWeightFieldNumber = 2;

	private int orderWeight_;

	public const int TaskRefreshTypeFieldNumber = 3;

	private TaskRefreshType taskRefreshType_;

	public const int ConditionTypeFieldNumber = 4;

	private ConditionType conditionType_;

	public const int ParamFieldNumber = 5;

	private int param_;

	public const int RefFieldNumber = 6;

	private int ref_;

	public const int NameIDFieldNumber = 7;

	private int nameID_;

	public const int DescIdFieldNumber = 8;

	private int descId_;

	public const int ExpFieldNumber = 9;

	private int exp_;

	public const int IconFieldNumber = 10;

	private string icon_ = "";

	public const int WayFieldNumber = 11;

	private int way_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattlePassTaskConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattlePassReflection.Descriptor.MessageTypes[3];

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
	public int Exp
	{
		get
		{
			return exp_;
		}
		private set
		{
			exp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Icon
	{
		get
		{
			return icon_;
		}
		private set
		{
			icon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

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
	public BattlePassTaskConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassTaskConfigureItem(BattlePassTaskConfigureItem other)
		: this()
	{
		id_ = other.id_;
		orderWeight_ = other.orderWeight_;
		taskRefreshType_ = other.taskRefreshType_;
		conditionType_ = other.conditionType_;
		param_ = other.param_;
		ref_ = other.ref_;
		nameID_ = other.nameID_;
		descId_ = other.descId_;
		exp_ = other.exp_;
		icon_ = other.icon_;
		way_ = other.way_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassTaskConfigureItem Clone()
	{
		return new BattlePassTaskConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattlePassTaskConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattlePassTaskConfigureItem other)
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
		if (TaskRefreshType != other.TaskRefreshType)
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
		if (Exp != other.Exp)
		{
			return false;
		}
		if (Icon != other.Icon)
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
		if (TaskRefreshType != TaskRefreshType.None)
		{
			num ^= TaskRefreshType.GetHashCode();
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
		if (Exp != 0)
		{
			num ^= Exp.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
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
		if (TaskRefreshType != TaskRefreshType.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)TaskRefreshType);
		}
		if (ConditionType != ConditionType.None)
		{
			output.WriteRawTag(32);
			output.WriteEnum((int)ConditionType);
		}
		if (Param != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(Param);
		}
		if (Ref != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Ref);
		}
		if (NameID != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(NameID);
		}
		if (DescId != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(DescId);
		}
		if (Exp != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(Exp);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(82);
			output.WriteString(Icon);
		}
		if (Way != 0)
		{
			output.WriteRawTag(93);
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
		if (TaskRefreshType != TaskRefreshType.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)TaskRefreshType);
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
		if (Exp != 0)
		{
			num += 5;
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
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
	public void MergeFrom(BattlePassTaskConfigureItem other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.OrderWeight != 0)
			{
				OrderWeight = other.OrderWeight;
			}
			if (other.TaskRefreshType != TaskRefreshType.None)
			{
				TaskRefreshType = other.TaskRefreshType;
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
			if (other.Exp != 0)
			{
				Exp = other.Exp;
			}
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
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
				OrderWeight = input.ReadSFixed32();
				break;
			case 24u:
				TaskRefreshType = (TaskRefreshType)input.ReadEnum();
				break;
			case 32u:
				ConditionType = (ConditionType)input.ReadEnum();
				break;
			case 45u:
				Param = input.ReadSFixed32();
				break;
			case 53u:
				Ref = input.ReadSFixed32();
				break;
			case 61u:
				NameID = input.ReadSFixed32();
				break;
			case 69u:
				DescId = input.ReadSFixed32();
				break;
			case 77u:
				Exp = input.ReadSFixed32();
				break;
			case 82u:
				Icon = input.ReadString();
				break;
			case 93u:
				Way = input.ReadSFixed32();
				break;
			}
		}
	}
}
