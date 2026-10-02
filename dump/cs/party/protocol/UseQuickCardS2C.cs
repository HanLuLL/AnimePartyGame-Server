using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class UseQuickCardS2C : IMessage<UseQuickCardS2C>, IMessage, IEquatable<UseQuickCardS2C>, IDeepCloneable<UseQuickCardS2C>, IBufferMessage
{
	private static readonly MessageParser<UseQuickCardS2C> _parser = new MessageParser<UseQuickCardS2C>(() => new UseQuickCardS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int CardIdFieldNumber = 2;

	private int cardId_;

	public const int OriginalCardIdFieldNumber = 3;

	private int originalCardId_;

	public const int TargetIdFieldNumber = 4;

	private long targetId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UseQuickCardS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[338];

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
	public int OriginalCardId
	{
		get
		{
			return originalCardId_;
		}
		set
		{
			originalCardId_ = value;
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
	public UseQuickCardS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseQuickCardS2C(UseQuickCardS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		cardId_ = other.cardId_;
		originalCardId_ = other.originalCardId_;
		targetId_ = other.targetId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseQuickCardS2C Clone()
	{
		return new UseQuickCardS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UseQuickCardS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UseQuickCardS2C other)
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
		if (CardId != other.CardId)
		{
			return false;
		}
		if (OriginalCardId != other.OriginalCardId)
		{
			return false;
		}
		if (TargetId != other.TargetId)
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
		if (CardId != 0)
		{
			num ^= CardId.GetHashCode();
		}
		if (OriginalCardId != 0)
		{
			num ^= OriginalCardId.GetHashCode();
		}
		if (TargetId != 0L)
		{
			num ^= TargetId.GetHashCode();
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
		if (CardId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CardId);
		}
		if (OriginalCardId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(OriginalCardId);
		}
		if (TargetId != 0L)
		{
			output.WriteRawTag(33);
			output.WriteSFixed64(TargetId);
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
		if (CardId != 0)
		{
			num += 5;
		}
		if (OriginalCardId != 0)
		{
			num += 5;
		}
		if (TargetId != 0L)
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
	public void MergeFrom(UseQuickCardS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.CardId != 0)
			{
				CardId = other.CardId;
			}
			if (other.OriginalCardId != 0)
			{
				OriginalCardId = other.OriginalCardId;
			}
			if (other.TargetId != 0L)
			{
				TargetId = other.TargetId;
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
			case 21u:
				CardId = input.ReadSFixed32();
				break;
			case 29u:
				OriginalCardId = input.ReadSFixed32();
				break;
			case 33u:
				TargetId = input.ReadSFixed64();
				break;
			}
		}
	}
}
