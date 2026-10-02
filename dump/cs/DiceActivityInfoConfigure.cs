using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class DiceActivityInfoConfigure : IMessage<DiceActivityInfoConfigure>, IMessage, IEquatable<DiceActivityInfoConfigure>, IDeepCloneable<DiceActivityInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<DiceActivityInfoConfigure> _parser = new MessageParser<DiceActivityInfoConfigure>(() => new DiceActivityInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int MapIDFieldNumber = 2;

	private int mapID_;

	public const int DiceCostFieldNumber = 3;

	private int diceCost_;

	public const int EXPFieldNumber = 4;

	private int eXP_;

	public const int EXPRepeatFieldNumber = 5;

	private int eXPRepeat_;

	public const int HeadlineFieldNumber = 6;

	private int headline_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<DiceActivityInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DiceActivityReflection.Descriptor.MessageTypes[0];

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
	public int MapID
	{
		get
		{
			return mapID_;
		}
		private set
		{
			mapID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DiceCost
	{
		get
		{
			return diceCost_;
		}
		private set
		{
			diceCost_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EXP
	{
		get
		{
			return eXP_;
		}
		private set
		{
			eXP_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int EXPRepeat
	{
		get
		{
			return eXPRepeat_;
		}
		private set
		{
			eXPRepeat_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Headline
	{
		get
		{
			return headline_;
		}
		private set
		{
			headline_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceActivityInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceActivityInfoConfigure(DiceActivityInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		mapID_ = other.mapID_;
		diceCost_ = other.diceCost_;
		eXP_ = other.eXP_;
		eXPRepeat_ = other.eXPRepeat_;
		headline_ = other.headline_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DiceActivityInfoConfigure Clone()
	{
		return new DiceActivityInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DiceActivityInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(DiceActivityInfoConfigure other)
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
		if (MapID != other.MapID)
		{
			return false;
		}
		if (DiceCost != other.DiceCost)
		{
			return false;
		}
		if (EXP != other.EXP)
		{
			return false;
		}
		if (EXPRepeat != other.EXPRepeat)
		{
			return false;
		}
		if (Headline != other.Headline)
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
		if (MapID != 0)
		{
			num ^= MapID.GetHashCode();
		}
		if (DiceCost != 0)
		{
			num ^= DiceCost.GetHashCode();
		}
		if (EXP != 0)
		{
			num ^= EXP.GetHashCode();
		}
		if (EXPRepeat != 0)
		{
			num ^= EXPRepeat.GetHashCode();
		}
		if (Headline != 0)
		{
			num ^= Headline.GetHashCode();
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
		if (MapID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MapID);
		}
		if (DiceCost != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DiceCost);
		}
		if (EXP != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(EXP);
		}
		if (EXPRepeat != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(EXPRepeat);
		}
		if (Headline != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Headline);
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
		if (MapID != 0)
		{
			num += 5;
		}
		if (DiceCost != 0)
		{
			num += 5;
		}
		if (EXP != 0)
		{
			num += 5;
		}
		if (EXPRepeat != 0)
		{
			num += 5;
		}
		if (Headline != 0)
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
	public void MergeFrom(DiceActivityInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.MapID != 0)
			{
				MapID = other.MapID;
			}
			if (other.DiceCost != 0)
			{
				DiceCost = other.DiceCost;
			}
			if (other.EXP != 0)
			{
				EXP = other.EXP;
			}
			if (other.EXPRepeat != 0)
			{
				EXPRepeat = other.EXPRepeat;
			}
			if (other.Headline != 0)
			{
				Headline = other.Headline;
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
				MapID = input.ReadSFixed32();
				break;
			case 29u:
				DiceCost = input.ReadSFixed32();
				break;
			case 37u:
				EXP = input.ReadSFixed32();
				break;
			case 45u:
				EXPRepeat = input.ReadSFixed32();
				break;
			case 53u:
				Headline = input.ReadSFixed32();
				break;
			}
		}
	}
}
