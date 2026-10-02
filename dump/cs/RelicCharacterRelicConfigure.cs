using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

public sealed class RelicCharacterRelicConfigure : IMessage<RelicCharacterRelicConfigure>, IMessage, IEquatable<RelicCharacterRelicConfigure>, IDeepCloneable<RelicCharacterRelicConfigure>, IBufferMessage
{
	private static readonly MessageParser<RelicCharacterRelicConfigure> _parser = new MessageParser<RelicCharacterRelicConfigure>(() => new RelicCharacterRelicConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int AttackPtFieldNumber = 2;

	private int attackPt_;

	public const int CardPtFieldNumber = 3;

	private int cardPt_;

	public const int SupportPtFieldNumber = 4;

	private int supportPt_;

	public const int TankPtFieldNumber = 5;

	private int tankPt_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RelicCharacterRelicConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RelicReflection.Descriptor.MessageTypes[3];

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
	public int AttackPt
	{
		get
		{
			return attackPt_;
		}
		private set
		{
			attackPt_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardPt
	{
		get
		{
			return cardPt_;
		}
		private set
		{
			cardPt_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SupportPt
	{
		get
		{
			return supportPt_;
		}
		private set
		{
			supportPt_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TankPt
	{
		get
		{
			return tankPt_;
		}
		private set
		{
			tankPt_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicCharacterRelicConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicCharacterRelicConfigure(RelicCharacterRelicConfigure other)
		: this()
	{
		id_ = other.id_;
		attackPt_ = other.attackPt_;
		cardPt_ = other.cardPt_;
		supportPt_ = other.supportPt_;
		tankPt_ = other.tankPt_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RelicCharacterRelicConfigure Clone()
	{
		return new RelicCharacterRelicConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RelicCharacterRelicConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RelicCharacterRelicConfigure other)
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
		if (AttackPt != other.AttackPt)
		{
			return false;
		}
		if (CardPt != other.CardPt)
		{
			return false;
		}
		if (SupportPt != other.SupportPt)
		{
			return false;
		}
		if (TankPt != other.TankPt)
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
		if (AttackPt != 0)
		{
			num ^= AttackPt.GetHashCode();
		}
		if (CardPt != 0)
		{
			num ^= CardPt.GetHashCode();
		}
		if (SupportPt != 0)
		{
			num ^= SupportPt.GetHashCode();
		}
		if (TankPt != 0)
		{
			num ^= TankPt.GetHashCode();
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
		if (AttackPt != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(AttackPt);
		}
		if (CardPt != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(CardPt);
		}
		if (SupportPt != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(SupportPt);
		}
		if (TankPt != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(TankPt);
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
		if (AttackPt != 0)
		{
			num += 5;
		}
		if (CardPt != 0)
		{
			num += 5;
		}
		if (SupportPt != 0)
		{
			num += 5;
		}
		if (TankPt != 0)
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
	public void MergeFrom(RelicCharacterRelicConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.AttackPt != 0)
			{
				AttackPt = other.AttackPt;
			}
			if (other.CardPt != 0)
			{
				CardPt = other.CardPt;
			}
			if (other.SupportPt != 0)
			{
				SupportPt = other.SupportPt;
			}
			if (other.TankPt != 0)
			{
				TankPt = other.TankPt;
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
				AttackPt = input.ReadSFixed32();
				break;
			case 29u:
				CardPt = input.ReadSFixed32();
				break;
			case 37u:
				SupportPt = input.ReadSFixed32();
				break;
			case 45u:
				TankPt = input.ReadSFixed32();
				break;
			}
		}
	}
}
