using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class CardInfo : IMessage<CardInfo>, IMessage, IEquatable<CardInfo>, IDeepCloneable<CardInfo>, IBufferMessage
{
	private static readonly MessageParser<CardInfo> _parser = new MessageParser<CardInfo>(() => new CardInfo());

	private UnknownFieldSet _unknownFields;

	public const int UniqueIdFieldNumber = 1;

	private int uniqueId_;

	public const int CardIdFieldNumber = 2;

	private int cardId_;

	public const int PurifyNumFieldNumber = 3;

	private int purifyNum_;

	public const int IsTempFieldNumber = 4;

	private bool isTemp_;

	public const int BattleCostFieldNumber = 5;

	private int battleCost_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<CardInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[57];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UniqueId
	{
		get
		{
			return uniqueId_;
		}
		set
		{
			uniqueId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CardId
	{
		get
		{
			return cardId_;
		}
		set
		{
			cardId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PurifyNum
	{
		get
		{
			return purifyNum_;
		}
		set
		{
			purifyNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsTemp
	{
		get
		{
			return isTemp_;
		}
		set
		{
			isTemp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BattleCost
	{
		get
		{
			return battleCost_;
		}
		set
		{
			battleCost_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardInfo(CardInfo other)
		: this()
	{
		uniqueId_ = other.uniqueId_;
		cardId_ = other.cardId_;
		purifyNum_ = other.purifyNum_;
		isTemp_ = other.isTemp_;
		battleCost_ = other.battleCost_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CardInfo Clone()
	{
		return new CardInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CardInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(CardInfo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (UniqueId != other.UniqueId)
		{
			return false;
		}
		if (CardId != other.CardId)
		{
			return false;
		}
		if (PurifyNum != other.PurifyNum)
		{
			return false;
		}
		if (IsTemp != other.IsTemp)
		{
			return false;
		}
		if (BattleCost != other.BattleCost)
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
		if (UniqueId != 0)
		{
			num ^= UniqueId.GetHashCode();
		}
		if (CardId != 0)
		{
			num ^= CardId.GetHashCode();
		}
		if (PurifyNum != 0)
		{
			num ^= PurifyNum.GetHashCode();
		}
		if (IsTemp)
		{
			num ^= IsTemp.GetHashCode();
		}
		if (BattleCost != 0)
		{
			num ^= BattleCost.GetHashCode();
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
		if (UniqueId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(UniqueId);
		}
		if (CardId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CardId);
		}
		if (PurifyNum != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(PurifyNum);
		}
		if (IsTemp)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsTemp);
		}
		if (BattleCost != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(BattleCost);
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
		if (UniqueId != 0)
		{
			num += 5;
		}
		if (CardId != 0)
		{
			num += 5;
		}
		if (PurifyNum != 0)
		{
			num += 5;
		}
		if (IsTemp)
		{
			num += 2;
		}
		if (BattleCost != 0)
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
	public void MergeFrom(CardInfo other)
	{
		if (other != null)
		{
			if (other.UniqueId != 0)
			{
				UniqueId = other.UniqueId;
			}
			if (other.CardId != 0)
			{
				CardId = other.CardId;
			}
			if (other.PurifyNum != 0)
			{
				PurifyNum = other.PurifyNum;
			}
			if (other.IsTemp)
			{
				IsTemp = other.IsTemp;
			}
			if (other.BattleCost != 0)
			{
				BattleCost = other.BattleCost;
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
				UniqueId = input.ReadSFixed32();
				break;
			case 21u:
				CardId = input.ReadSFixed32();
				break;
			case 29u:
				PurifyNum = input.ReadSFixed32();
				break;
			case 32u:
				IsTemp = input.ReadBool();
				break;
			case 45u:
				BattleCost = input.ReadSFixed32();
				break;
			}
		}
	}
}
