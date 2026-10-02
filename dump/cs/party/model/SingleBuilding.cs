using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SingleBuilding : IMessage<SingleBuilding>, IMessage, IEquatable<SingleBuilding>, IDeepCloneable<SingleBuilding>, IBufferMessage
{
	private static readonly MessageParser<SingleBuilding> _parser = new MessageParser<SingleBuilding>(() => new SingleBuilding());

	private UnknownFieldSet _unknownFields;

	public const int BuildingIdFieldNumber = 1;

	private int buildingId_;

	public const int CardUIDFieldNumber = 2;

	private int cardUID_;

	public const int BuildingFoundationIdFieldNumber = 3;

	private int buildingFoundationId_;

	public const int OperateBonusFieldNumber = 4;

	private static readonly MapField<int, int>.Codec _map_operateBonus_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 34u);

	private readonly MapField<int, int> operateBonus_ = new MapField<int, int>();

	public const int AttributeBonusFieldNumber = 5;

	private static readonly MapField<int, int>.Codec _map_attributeBonus_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 42u);

	private readonly MapField<int, int> attributeBonus_ = new MapField<int, int>();

	public const int ThrowDiceTriggerCountFieldNumber = 6;

	private int throwDiceTriggerCount_;

	public const int PassTriggerCountFieldNumber = 7;

	private int passTriggerCount_;

	public const int StayTriggerCountFieldNumber = 8;

	private int stayTriggerCount_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SingleBuilding> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[109];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuildingId
	{
		get
		{
			return buildingId_;
		}
		set
		{
			buildingId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardUID
	{
		get
		{
			return cardUID_;
		}
		set
		{
			cardUID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuildingFoundationId
	{
		get
		{
			return buildingFoundationId_;
		}
		set
		{
			buildingFoundationId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> OperateBonus => operateBonus_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> AttributeBonus => attributeBonus_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ThrowDiceTriggerCount
	{
		get
		{
			return throwDiceTriggerCount_;
		}
		set
		{
			throwDiceTriggerCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PassTriggerCount
	{
		get
		{
			return passTriggerCount_;
		}
		set
		{
			passTriggerCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int StayTriggerCount
	{
		get
		{
			return stayTriggerCount_;
		}
		set
		{
			stayTriggerCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleBuilding()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleBuilding(SingleBuilding other)
		: this()
	{
		buildingId_ = other.buildingId_;
		cardUID_ = other.cardUID_;
		buildingFoundationId_ = other.buildingFoundationId_;
		operateBonus_ = other.operateBonus_.Clone();
		attributeBonus_ = other.attributeBonus_.Clone();
		throwDiceTriggerCount_ = other.throwDiceTriggerCount_;
		passTriggerCount_ = other.passTriggerCount_;
		stayTriggerCount_ = other.stayTriggerCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleBuilding Clone()
	{
		return new SingleBuilding(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SingleBuilding);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SingleBuilding other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (BuildingId != other.BuildingId)
		{
			return false;
		}
		if (CardUID != other.CardUID)
		{
			return false;
		}
		if (BuildingFoundationId != other.BuildingFoundationId)
		{
			return false;
		}
		if (!OperateBonus.Equals(other.OperateBonus))
		{
			return false;
		}
		if (!AttributeBonus.Equals(other.AttributeBonus))
		{
			return false;
		}
		if (ThrowDiceTriggerCount != other.ThrowDiceTriggerCount)
		{
			return false;
		}
		if (PassTriggerCount != other.PassTriggerCount)
		{
			return false;
		}
		if (StayTriggerCount != other.StayTriggerCount)
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
		if (BuildingId != 0)
		{
			num ^= BuildingId.GetHashCode();
		}
		if (CardUID != 0)
		{
			num ^= CardUID.GetHashCode();
		}
		if (BuildingFoundationId != 0)
		{
			num ^= BuildingFoundationId.GetHashCode();
		}
		num ^= OperateBonus.GetHashCode();
		num ^= AttributeBonus.GetHashCode();
		if (ThrowDiceTriggerCount != 0)
		{
			num ^= ThrowDiceTriggerCount.GetHashCode();
		}
		if (PassTriggerCount != 0)
		{
			num ^= PassTriggerCount.GetHashCode();
		}
		if (StayTriggerCount != 0)
		{
			num ^= StayTriggerCount.GetHashCode();
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
		if (BuildingId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(BuildingId);
		}
		if (CardUID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CardUID);
		}
		if (BuildingFoundationId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(BuildingFoundationId);
		}
		operateBonus_.WriteTo(ref output, _map_operateBonus_codec);
		attributeBonus_.WriteTo(ref output, _map_attributeBonus_codec);
		if (ThrowDiceTriggerCount != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(ThrowDiceTriggerCount);
		}
		if (PassTriggerCount != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(PassTriggerCount);
		}
		if (StayTriggerCount != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(StayTriggerCount);
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
		if (BuildingId != 0)
		{
			num += 5;
		}
		if (CardUID != 0)
		{
			num += 5;
		}
		if (BuildingFoundationId != 0)
		{
			num += 5;
		}
		num += operateBonus_.CalculateSize(_map_operateBonus_codec);
		num += attributeBonus_.CalculateSize(_map_attributeBonus_codec);
		if (ThrowDiceTriggerCount != 0)
		{
			num += 5;
		}
		if (PassTriggerCount != 0)
		{
			num += 5;
		}
		if (StayTriggerCount != 0)
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
	public void MergeFrom(SingleBuilding other)
	{
		if (other != null)
		{
			if (other.BuildingId != 0)
			{
				BuildingId = other.BuildingId;
			}
			if (other.CardUID != 0)
			{
				CardUID = other.CardUID;
			}
			if (other.BuildingFoundationId != 0)
			{
				BuildingFoundationId = other.BuildingFoundationId;
			}
			operateBonus_.MergeFrom(other.operateBonus_);
			attributeBonus_.MergeFrom(other.attributeBonus_);
			if (other.ThrowDiceTriggerCount != 0)
			{
				ThrowDiceTriggerCount = other.ThrowDiceTriggerCount;
			}
			if (other.PassTriggerCount != 0)
			{
				PassTriggerCount = other.PassTriggerCount;
			}
			if (other.StayTriggerCount != 0)
			{
				StayTriggerCount = other.StayTriggerCount;
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
				BuildingId = input.ReadSFixed32();
				break;
			case 21u:
				CardUID = input.ReadSFixed32();
				break;
			case 29u:
				BuildingFoundationId = input.ReadSFixed32();
				break;
			case 34u:
				operateBonus_.AddEntriesFrom(ref input, _map_operateBonus_codec);
				break;
			case 42u:
				attributeBonus_.AddEntriesFrom(ref input, _map_attributeBonus_codec);
				break;
			case 53u:
				ThrowDiceTriggerCount = input.ReadSFixed32();
				break;
			case 61u:
				PassTriggerCount = input.ReadSFixed32();
				break;
			case 69u:
				StayTriggerCount = input.ReadSFixed32();
				break;
			}
		}
	}
}
