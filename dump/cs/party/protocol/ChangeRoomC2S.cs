using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChangeRoomC2S : IMessage<ChangeRoomC2S>, IMessage, IEquatable<ChangeRoomC2S>, IDeepCloneable<ChangeRoomC2S>, IBufferMessage
{
	private static readonly MessageParser<ChangeRoomC2S> _parser = new MessageParser<ChangeRoomC2S>(() => new ChangeRoomC2S());

	private UnknownFieldSet _unknownFields;

	public const int PwdFieldNumber = 1;

	private string pwd_ = "";

	public const int MapIdFieldNumber = 2;

	private int mapId_;

	public const int MaxTimeFieldNumber = 3;

	private int maxTime_;

	public const int UpgradePlanFieldNumber = 4;

	private int upgradePlan_;

	public const int TimePlanFieldNumber = 5;

	private int timePlan_;

	public const int ModeFieldNumber = 6;

	private int mode_;

	public const int SpeedTypeFieldNumber = 7;

	private int speedType_;

	public const int DifficultyFieldNumber = 8;

	private int difficulty_;

	public const int SkipStoryFieldNumber = 9;

	private bool skipStory_;

	public const int RoomLabelFieldNumber = 10;

	private int roomLabel_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChangeRoomC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[35];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Pwd
	{
		get
		{
			return pwd_;
		}
		set
		{
			pwd_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MapId
	{
		get
		{
			return mapId_;
		}
		set
		{
			mapId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxTime
	{
		get
		{
			return maxTime_;
		}
		set
		{
			maxTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UpgradePlan
	{
		get
		{
			return upgradePlan_;
		}
		set
		{
			upgradePlan_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TimePlan
	{
		get
		{
			return timePlan_;
		}
		set
		{
			timePlan_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Mode
	{
		get
		{
			return mode_;
		}
		set
		{
			mode_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SpeedType
	{
		get
		{
			return speedType_;
		}
		set
		{
			speedType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Difficulty
	{
		get
		{
			return difficulty_;
		}
		set
		{
			difficulty_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool SkipStory
	{
		get
		{
			return skipStory_;
		}
		set
		{
			skipStory_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoomLabel
	{
		get
		{
			return roomLabel_;
		}
		set
		{
			roomLabel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeRoomC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeRoomC2S(ChangeRoomC2S other)
		: this()
	{
		pwd_ = other.pwd_;
		mapId_ = other.mapId_;
		maxTime_ = other.maxTime_;
		upgradePlan_ = other.upgradePlan_;
		timePlan_ = other.timePlan_;
		mode_ = other.mode_;
		speedType_ = other.speedType_;
		difficulty_ = other.difficulty_;
		skipStory_ = other.skipStory_;
		roomLabel_ = other.roomLabel_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeRoomC2S Clone()
	{
		return new ChangeRoomC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChangeRoomC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChangeRoomC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Pwd != other.Pwd)
		{
			return false;
		}
		if (MapId != other.MapId)
		{
			return false;
		}
		if (MaxTime != other.MaxTime)
		{
			return false;
		}
		if (UpgradePlan != other.UpgradePlan)
		{
			return false;
		}
		if (TimePlan != other.TimePlan)
		{
			return false;
		}
		if (Mode != other.Mode)
		{
			return false;
		}
		if (SpeedType != other.SpeedType)
		{
			return false;
		}
		if (Difficulty != other.Difficulty)
		{
			return false;
		}
		if (SkipStory != other.SkipStory)
		{
			return false;
		}
		if (RoomLabel != other.RoomLabel)
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
		if (Pwd.Length != 0)
		{
			num ^= Pwd.GetHashCode();
		}
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (MaxTime != 0)
		{
			num ^= MaxTime.GetHashCode();
		}
		if (UpgradePlan != 0)
		{
			num ^= UpgradePlan.GetHashCode();
		}
		if (TimePlan != 0)
		{
			num ^= TimePlan.GetHashCode();
		}
		if (Mode != 0)
		{
			num ^= Mode.GetHashCode();
		}
		if (SpeedType != 0)
		{
			num ^= SpeedType.GetHashCode();
		}
		if (Difficulty != 0)
		{
			num ^= Difficulty.GetHashCode();
		}
		if (SkipStory)
		{
			num ^= SkipStory.GetHashCode();
		}
		if (RoomLabel != 0)
		{
			num ^= RoomLabel.GetHashCode();
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
		if (Pwd.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(Pwd);
		}
		if (MapId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MapId);
		}
		if (MaxTime != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(MaxTime);
		}
		if (UpgradePlan != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(UpgradePlan);
		}
		if (TimePlan != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(TimePlan);
		}
		if (Mode != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Mode);
		}
		if (SpeedType != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(SpeedType);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(Difficulty);
		}
		if (SkipStory)
		{
			output.WriteRawTag(72);
			output.WriteBool(SkipStory);
		}
		if (RoomLabel != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(RoomLabel);
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
		if (Pwd.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Pwd);
		}
		if (MapId != 0)
		{
			num += 5;
		}
		if (MaxTime != 0)
		{
			num += 5;
		}
		if (UpgradePlan != 0)
		{
			num += 5;
		}
		if (TimePlan != 0)
		{
			num += 5;
		}
		if (Mode != 0)
		{
			num += 5;
		}
		if (SpeedType != 0)
		{
			num += 5;
		}
		if (Difficulty != 0)
		{
			num += 5;
		}
		if (SkipStory)
		{
			num += 2;
		}
		if (RoomLabel != 0)
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
	public void MergeFrom(ChangeRoomC2S other)
	{
		if (other != null)
		{
			if (other.Pwd.Length != 0)
			{
				Pwd = other.Pwd;
			}
			if (other.MapId != 0)
			{
				MapId = other.MapId;
			}
			if (other.MaxTime != 0)
			{
				MaxTime = other.MaxTime;
			}
			if (other.UpgradePlan != 0)
			{
				UpgradePlan = other.UpgradePlan;
			}
			if (other.TimePlan != 0)
			{
				TimePlan = other.TimePlan;
			}
			if (other.Mode != 0)
			{
				Mode = other.Mode;
			}
			if (other.SpeedType != 0)
			{
				SpeedType = other.SpeedType;
			}
			if (other.Difficulty != 0)
			{
				Difficulty = other.Difficulty;
			}
			if (other.SkipStory)
			{
				SkipStory = other.SkipStory;
			}
			if (other.RoomLabel != 0)
			{
				RoomLabel = other.RoomLabel;
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
			case 10u:
				Pwd = input.ReadString();
				break;
			case 21u:
				MapId = input.ReadSFixed32();
				break;
			case 29u:
				MaxTime = input.ReadSFixed32();
				break;
			case 37u:
				UpgradePlan = input.ReadSFixed32();
				break;
			case 45u:
				TimePlan = input.ReadSFixed32();
				break;
			case 53u:
				Mode = input.ReadSFixed32();
				break;
			case 61u:
				SpeedType = input.ReadSFixed32();
				break;
			case 69u:
				Difficulty = input.ReadSFixed32();
				break;
			case 72u:
				SkipStory = input.ReadBool();
				break;
			case 85u:
				RoomLabel = input.ReadSFixed32();
				break;
			}
		}
	}
}
