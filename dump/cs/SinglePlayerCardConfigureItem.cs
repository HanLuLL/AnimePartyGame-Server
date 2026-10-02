using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerCardConfigureItem : IMessage<SinglePlayerCardConfigureItem>, IMessage, IEquatable<SinglePlayerCardConfigureItem>, IDeepCloneable<SinglePlayerCardConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerCardConfigureItem> _parser = new MessageParser<SinglePlayerCardConfigureItem>(() => new SinglePlayerCardConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int LevelFieldNumber = 1;

	private int level_;

	public const int PlinthFieldNumber = 2;

	private string plinth_ = "";

	public const int BuildingFieldNumber = 3;

	private string building_ = "";

	public const int TriggerPointFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_triggerPoint_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> triggerPoint_ = new RepeatedField<int>();

	public const int TriggerParamFieldNumber = 5;

	private static readonly FieldCodec<int> _repeated_triggerParam_codec = FieldCodec.ForSFixed32(42u);

	private readonly RepeatedField<int> triggerParam_ = new RepeatedField<int>();

	public const int TriggerTimesFieldNumber = 6;

	private int triggerTimes_;

	public const int TriggerTLFieldNumber = 7;

	private string triggerTL_ = "";

	public const int WalkParamFieldNumber = 8;

	private static readonly FieldCodec<int> _repeated_walkParam_codec = FieldCodec.ForSFixed32(66u);

	private readonly RepeatedField<int> walkParam_ = new RepeatedField<int>();

	public const int WalkTimesFieldNumber = 9;

	private int walkTimes_;

	public const int WalkTLFieldNumber = 10;

	private string walkTL_ = "";

	public const int WalkPerformIDFieldNumber = 11;

	private int walkPerformID_;

	public const int StopParamFieldNumber = 12;

	private static readonly FieldCodec<int> _repeated_stopParam_codec = FieldCodec.ForSFixed32(98u);

	private readonly RepeatedField<int> stopParam_ = new RepeatedField<int>();

	public const int StopTimesFieldNumber = 13;

	private int stopTimes_;

	public const int StopTLFieldNumber = 14;

	private string stopTL_ = "";

	public const int OtherParamFieldNumber = 15;

	private static readonly FieldCodec<int> _repeated_otherParam_codec = FieldCodec.ForSFixed32(122u);

	private readonly RepeatedField<int> otherParam_ = new RepeatedField<int>();

	public const int AddPriceFieldNumber = 16;

	private int addPrice_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerCardConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[12];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Level
	{
		get
		{
			return level_;
		}
		private set
		{
			level_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Plinth
	{
		get
		{
			return plinth_;
		}
		private set
		{
			plinth_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Building
	{
		get
		{
			return building_;
		}
		private set
		{
			building_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TriggerPoint => triggerPoint_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TriggerParam => triggerParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TriggerTimes
	{
		get
		{
			return triggerTimes_;
		}
		private set
		{
			triggerTimes_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string TriggerTL
	{
		get
		{
			return triggerTL_;
		}
		private set
		{
			triggerTL_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> WalkParam => walkParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WalkTimes
	{
		get
		{
			return walkTimes_;
		}
		private set
		{
			walkTimes_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string WalkTL
	{
		get
		{
			return walkTL_;
		}
		private set
		{
			walkTL_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int WalkPerformID
	{
		get
		{
			return walkPerformID_;
		}
		private set
		{
			walkPerformID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> StopParam => stopParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int StopTimes
	{
		get
		{
			return stopTimes_;
		}
		private set
		{
			stopTimes_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string StopTL
	{
		get
		{
			return stopTL_;
		}
		private set
		{
			stopTL_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> OtherParam => otherParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AddPrice
	{
		get
		{
			return addPrice_;
		}
		private set
		{
			addPrice_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardConfigureItem(SinglePlayerCardConfigureItem other)
		: this()
	{
		level_ = other.level_;
		plinth_ = other.plinth_;
		building_ = other.building_;
		triggerPoint_ = other.triggerPoint_.Clone();
		triggerParam_ = other.triggerParam_.Clone();
		triggerTimes_ = other.triggerTimes_;
		triggerTL_ = other.triggerTL_;
		walkParam_ = other.walkParam_.Clone();
		walkTimes_ = other.walkTimes_;
		walkTL_ = other.walkTL_;
		walkPerformID_ = other.walkPerformID_;
		stopParam_ = other.stopParam_.Clone();
		stopTimes_ = other.stopTimes_;
		stopTL_ = other.stopTL_;
		otherParam_ = other.otherParam_.Clone();
		addPrice_ = other.addPrice_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerCardConfigureItem Clone()
	{
		return new SinglePlayerCardConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerCardConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerCardConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Level != other.Level)
		{
			return false;
		}
		if (Plinth != other.Plinth)
		{
			return false;
		}
		if (Building != other.Building)
		{
			return false;
		}
		if (!triggerPoint_.Equals(other.triggerPoint_))
		{
			return false;
		}
		if (!triggerParam_.Equals(other.triggerParam_))
		{
			return false;
		}
		if (TriggerTimes != other.TriggerTimes)
		{
			return false;
		}
		if (TriggerTL != other.TriggerTL)
		{
			return false;
		}
		if (!walkParam_.Equals(other.walkParam_))
		{
			return false;
		}
		if (WalkTimes != other.WalkTimes)
		{
			return false;
		}
		if (WalkTL != other.WalkTL)
		{
			return false;
		}
		if (WalkPerformID != other.WalkPerformID)
		{
			return false;
		}
		if (!stopParam_.Equals(other.stopParam_))
		{
			return false;
		}
		if (StopTimes != other.StopTimes)
		{
			return false;
		}
		if (StopTL != other.StopTL)
		{
			return false;
		}
		if (!otherParam_.Equals(other.otherParam_))
		{
			return false;
		}
		if (AddPrice != other.AddPrice)
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
		if (Level != 0)
		{
			num ^= Level.GetHashCode();
		}
		if (Plinth.Length != 0)
		{
			num ^= Plinth.GetHashCode();
		}
		if (Building.Length != 0)
		{
			num ^= Building.GetHashCode();
		}
		num ^= triggerPoint_.GetHashCode();
		num ^= triggerParam_.GetHashCode();
		if (TriggerTimes != 0)
		{
			num ^= TriggerTimes.GetHashCode();
		}
		if (TriggerTL.Length != 0)
		{
			num ^= TriggerTL.GetHashCode();
		}
		num ^= walkParam_.GetHashCode();
		if (WalkTimes != 0)
		{
			num ^= WalkTimes.GetHashCode();
		}
		if (WalkTL.Length != 0)
		{
			num ^= WalkTL.GetHashCode();
		}
		if (WalkPerformID != 0)
		{
			num ^= WalkPerformID.GetHashCode();
		}
		num ^= stopParam_.GetHashCode();
		if (StopTimes != 0)
		{
			num ^= StopTimes.GetHashCode();
		}
		if (StopTL.Length != 0)
		{
			num ^= StopTL.GetHashCode();
		}
		num ^= otherParam_.GetHashCode();
		if (AddPrice != 0)
		{
			num ^= AddPrice.GetHashCode();
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
		if (Level != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Level);
		}
		if (Plinth.Length != 0)
		{
			output.WriteRawTag(18);
			output.WriteString(Plinth);
		}
		if (Building.Length != 0)
		{
			output.WriteRawTag(26);
			output.WriteString(Building);
		}
		triggerPoint_.WriteTo(ref output, _repeated_triggerPoint_codec);
		triggerParam_.WriteTo(ref output, _repeated_triggerParam_codec);
		if (TriggerTimes != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(TriggerTimes);
		}
		if (TriggerTL.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(TriggerTL);
		}
		walkParam_.WriteTo(ref output, _repeated_walkParam_codec);
		if (WalkTimes != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(WalkTimes);
		}
		if (WalkTL.Length != 0)
		{
			output.WriteRawTag(82);
			output.WriteString(WalkTL);
		}
		if (WalkPerformID != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(WalkPerformID);
		}
		stopParam_.WriteTo(ref output, _repeated_stopParam_codec);
		if (StopTimes != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(StopTimes);
		}
		if (StopTL.Length != 0)
		{
			output.WriteRawTag(114);
			output.WriteString(StopTL);
		}
		otherParam_.WriteTo(ref output, _repeated_otherParam_codec);
		if (AddPrice != 0)
		{
			output.WriteRawTag(133, 1);
			output.WriteSFixed32(AddPrice);
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
		if (Level != 0)
		{
			num += 5;
		}
		if (Plinth.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Plinth);
		}
		if (Building.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Building);
		}
		num += triggerPoint_.CalculateSize(_repeated_triggerPoint_codec);
		num += triggerParam_.CalculateSize(_repeated_triggerParam_codec);
		if (TriggerTimes != 0)
		{
			num += 5;
		}
		if (TriggerTL.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(TriggerTL);
		}
		num += walkParam_.CalculateSize(_repeated_walkParam_codec);
		if (WalkTimes != 0)
		{
			num += 5;
		}
		if (WalkTL.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(WalkTL);
		}
		if (WalkPerformID != 0)
		{
			num += 5;
		}
		num += stopParam_.CalculateSize(_repeated_stopParam_codec);
		if (StopTimes != 0)
		{
			num += 5;
		}
		if (StopTL.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(StopTL);
		}
		num += otherParam_.CalculateSize(_repeated_otherParam_codec);
		if (AddPrice != 0)
		{
			num += 6;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerCardConfigureItem other)
	{
		if (other != null)
		{
			if (other.Level != 0)
			{
				Level = other.Level;
			}
			if (other.Plinth.Length != 0)
			{
				Plinth = other.Plinth;
			}
			if (other.Building.Length != 0)
			{
				Building = other.Building;
			}
			triggerPoint_.Add(other.triggerPoint_);
			triggerParam_.Add(other.triggerParam_);
			if (other.TriggerTimes != 0)
			{
				TriggerTimes = other.TriggerTimes;
			}
			if (other.TriggerTL.Length != 0)
			{
				TriggerTL = other.TriggerTL;
			}
			walkParam_.Add(other.walkParam_);
			if (other.WalkTimes != 0)
			{
				WalkTimes = other.WalkTimes;
			}
			if (other.WalkTL.Length != 0)
			{
				WalkTL = other.WalkTL;
			}
			if (other.WalkPerformID != 0)
			{
				WalkPerformID = other.WalkPerformID;
			}
			stopParam_.Add(other.stopParam_);
			if (other.StopTimes != 0)
			{
				StopTimes = other.StopTimes;
			}
			if (other.StopTL.Length != 0)
			{
				StopTL = other.StopTL;
			}
			otherParam_.Add(other.otherParam_);
			if (other.AddPrice != 0)
			{
				AddPrice = other.AddPrice;
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
				Level = input.ReadSFixed32();
				break;
			case 18u:
				Plinth = input.ReadString();
				break;
			case 26u:
				Building = input.ReadString();
				break;
			case 34u:
			case 37u:
				triggerPoint_.AddEntriesFrom(ref input, _repeated_triggerPoint_codec);
				break;
			case 42u:
			case 45u:
				triggerParam_.AddEntriesFrom(ref input, _repeated_triggerParam_codec);
				break;
			case 53u:
				TriggerTimes = input.ReadSFixed32();
				break;
			case 58u:
				TriggerTL = input.ReadString();
				break;
			case 66u:
			case 69u:
				walkParam_.AddEntriesFrom(ref input, _repeated_walkParam_codec);
				break;
			case 77u:
				WalkTimes = input.ReadSFixed32();
				break;
			case 82u:
				WalkTL = input.ReadString();
				break;
			case 93u:
				WalkPerformID = input.ReadSFixed32();
				break;
			case 98u:
			case 101u:
				stopParam_.AddEntriesFrom(ref input, _repeated_stopParam_codec);
				break;
			case 109u:
				StopTimes = input.ReadSFixed32();
				break;
			case 114u:
				StopTL = input.ReadString();
				break;
			case 122u:
			case 125u:
				otherParam_.AddEntriesFrom(ref input, _repeated_otherParam_codec);
				break;
			case 133u:
				AddPrice = input.ReadSFixed32();
				break;
			}
		}
	}
}
