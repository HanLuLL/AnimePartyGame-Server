using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Bomb : IMessage<Bomb>, IMessage, IEquatable<Bomb>, IDeepCloneable<Bomb>, IBufferMessage
{
	private static readonly MessageParser<Bomb> _parser = new MessageParser<Bomb>(() => new Bomb());

	private UnknownFieldSet _unknownFields;

	public const int OnwerPlayerIdFieldNumber = 1;

	private long onwerPlayerId_;

	public const int PlayerIdFieldNumber = 2;

	private long playerId_;

	public const int IsOpenFieldNumber = 3;

	private bool isOpen_;

	public const int CardIdFieldNumber = 4;

	private int cardId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Bomb> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[82];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long OnwerPlayerId
	{
		get
		{
			return onwerPlayerId_;
		}
		set
		{
			onwerPlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PlayerId
	{
		get
		{
			return playerId_;
		}
		set
		{
			playerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsOpen
	{
		get
		{
			return isOpen_;
		}
		set
		{
			isOpen_ = value;
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
	public Bomb()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Bomb(Bomb other)
		: this()
	{
		onwerPlayerId_ = other.onwerPlayerId_;
		playerId_ = other.playerId_;
		isOpen_ = other.isOpen_;
		cardId_ = other.cardId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Bomb Clone()
	{
		return new Bomb(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Bomb);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Bomb other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (OnwerPlayerId != other.OnwerPlayerId)
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (IsOpen != other.IsOpen)
		{
			return false;
		}
		if (CardId != other.CardId)
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
		if (OnwerPlayerId != 0L)
		{
			num ^= OnwerPlayerId.GetHashCode();
		}
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (IsOpen)
		{
			num ^= IsOpen.GetHashCode();
		}
		if (CardId != 0)
		{
			num ^= CardId.GetHashCode();
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
		if (OnwerPlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(OnwerPlayerId);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(PlayerId);
		}
		if (IsOpen)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsOpen);
		}
		if (CardId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CardId);
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
		if (OnwerPlayerId != 0L)
		{
			num += 9;
		}
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (IsOpen)
		{
			num += 2;
		}
		if (CardId != 0)
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
	public void MergeFrom(Bomb other)
	{
		if (other != null)
		{
			if (other.OnwerPlayerId != 0L)
			{
				OnwerPlayerId = other.OnwerPlayerId;
			}
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.IsOpen)
			{
				IsOpen = other.IsOpen;
			}
			if (other.CardId != 0)
			{
				CardId = other.CardId;
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
			case 9u:
				OnwerPlayerId = input.ReadSFixed64();
				break;
			case 17u:
				PlayerId = input.ReadSFixed64();
				break;
			case 24u:
				IsOpen = input.ReadBool();
				break;
			case 37u:
				CardId = input.ReadSFixed32();
				break;
			}
		}
	}
}
