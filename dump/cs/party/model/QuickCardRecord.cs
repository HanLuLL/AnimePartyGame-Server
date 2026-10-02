using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class QuickCardRecord : IMessage<QuickCardRecord>, IMessage, IEquatable<QuickCardRecord>, IDeepCloneable<QuickCardRecord>, IBufferMessage
{
	private static readonly MessageParser<QuickCardRecord> _parser = new MessageParser<QuickCardRecord>(() => new QuickCardRecord());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int PrevTargetIdFieldNumber = 2;

	private long prevTargetId_;

	public const int PrevCardIdFieldNumber = 3;

	private int prevCardId_;

	public const int TargetIdFieldNumber = 4;

	private long targetId_;

	public const int CardIdFieldNumber = 5;

	private int cardId_;

	public const int CardExistFieldNumber = 6;

	private bool cardExist_;

	public const int PrevPlayerIdFieldNumber = 7;

	private long prevPlayerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<QuickCardRecord> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[61];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public long PrevTargetId
	{
		get
		{
			return prevTargetId_;
		}
		set
		{
			prevTargetId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PrevCardId
	{
		get
		{
			return prevCardId_;
		}
		set
		{
			prevCardId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long TargetId
	{
		get
		{
			return targetId_;
		}
		set
		{
			targetId_ = value;
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
	public bool CardExist
	{
		get
		{
			return cardExist_;
		}
		set
		{
			cardExist_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PrevPlayerId
	{
		get
		{
			return prevPlayerId_;
		}
		set
		{
			prevPlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickCardRecord()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickCardRecord(QuickCardRecord other)
		: this()
	{
		playerId_ = other.playerId_;
		prevTargetId_ = other.prevTargetId_;
		prevCardId_ = other.prevCardId_;
		targetId_ = other.targetId_;
		cardId_ = other.cardId_;
		cardExist_ = other.cardExist_;
		prevPlayerId_ = other.prevPlayerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickCardRecord Clone()
	{
		return new QuickCardRecord(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as QuickCardRecord);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(QuickCardRecord other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerId != other.PlayerId)
		{
			return false;
		}
		if (PrevTargetId != other.PrevTargetId)
		{
			return false;
		}
		if (PrevCardId != other.PrevCardId)
		{
			return false;
		}
		if (TargetId != other.TargetId)
		{
			return false;
		}
		if (CardId != other.CardId)
		{
			return false;
		}
		if (CardExist != other.CardExist)
		{
			return false;
		}
		if (PrevPlayerId != other.PrevPlayerId)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (PrevTargetId != 0L)
		{
			num ^= PrevTargetId.GetHashCode();
		}
		if (PrevCardId != 0)
		{
			num ^= PrevCardId.GetHashCode();
		}
		if (TargetId != 0L)
		{
			num ^= TargetId.GetHashCode();
		}
		if (CardId != 0)
		{
			num ^= CardId.GetHashCode();
		}
		if (CardExist)
		{
			num ^= CardExist.GetHashCode();
		}
		if (PrevPlayerId != 0L)
		{
			num ^= PrevPlayerId.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (PrevTargetId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(PrevTargetId);
		}
		if (PrevCardId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(PrevCardId);
		}
		if (TargetId != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(TargetId);
		}
		if (CardId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CardId);
		}
		if (CardExist)
		{
			output.WriteRawTag(48);
			output.WriteBool(CardExist);
		}
		if (PrevPlayerId != 0L)
		{
			output.WriteRawTag(57);
			output.WriteSFixed64(PrevPlayerId);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (PrevTargetId != 0L)
		{
			num += 9;
		}
		if (PrevCardId != 0)
		{
			num += 5;
		}
		if (TargetId != 0L)
		{
			num += 9;
		}
		if (CardId != 0)
		{
			num += 5;
		}
		if (CardExist)
		{
			num += 2;
		}
		if (PrevPlayerId != 0L)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(QuickCardRecord other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.PrevTargetId != 0L)
			{
				PrevTargetId = other.PrevTargetId;
			}
			if (other.PrevCardId != 0)
			{
				PrevCardId = other.PrevCardId;
			}
			if (other.TargetId != 0L)
			{
				TargetId = other.TargetId;
			}
			if (other.CardId != 0)
			{
				CardId = other.CardId;
			}
			if (other.CardExist)
			{
				CardExist = other.CardExist;
			}
			if (other.PrevPlayerId != 0L)
			{
				PrevPlayerId = other.PrevPlayerId;
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
				PlayerId = input.ReadSFixed64();
				break;
			case 17u:
				PrevTargetId = input.ReadSFixed64();
				break;
			case 29u:
				PrevCardId = input.ReadSFixed32();
				break;
			case 33u:
				TargetId = input.ReadSFixed64();
				break;
			case 45u:
				CardId = input.ReadSFixed32();
				break;
			case 48u:
				CardExist = input.ReadBool();
				break;
			case 57u:
				PrevPlayerId = input.ReadSFixed64();
				break;
			}
		}
	}
}
