using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class UseQuickCardC2S : IMessage<UseQuickCardC2S>, IMessage, IEquatable<UseQuickCardC2S>, IDeepCloneable<UseQuickCardC2S>, IBufferMessage
{
	private static readonly MessageParser<UseQuickCardC2S> _parser = new MessageParser<UseQuickCardC2S>(() => new UseQuickCardC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int PrevPlayerIdFieldNumber = 2;

	private long prevPlayerId_;

	public const int PrevCardIdFieldNumber = 3;

	private int prevCardId_;

	public const int PrevTargetIdFieldNumber = 6;

	private long prevTargetId_;

	public const int CardIdFieldNumber = 4;

	private int cardId_;

	public const int TargetIdFieldNumber = 5;

	private long targetId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UseQuickCardC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[337];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
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
	public UseQuickCardC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseQuickCardC2S(UseQuickCardC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		prevPlayerId_ = other.prevPlayerId_;
		prevCardId_ = other.prevCardId_;
		prevTargetId_ = other.prevTargetId_;
		cardId_ = other.cardId_;
		targetId_ = other.targetId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseQuickCardC2S Clone()
	{
		return new UseQuickCardC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UseQuickCardC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UseQuickCardC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (PrevPlayerId != other.PrevPlayerId)
		{
			return false;
		}
		if (PrevCardId != other.PrevCardId)
		{
			return false;
		}
		if (PrevTargetId != other.PrevTargetId)
		{
			return false;
		}
		if (CardId != other.CardId)
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		if (PrevPlayerId != 0L)
		{
			num ^= PrevPlayerId.GetHashCode();
		}
		if (PrevCardId != 0)
		{
			num ^= PrevCardId.GetHashCode();
		}
		if (PrevTargetId != 0L)
		{
			num ^= PrevTargetId.GetHashCode();
		}
		if (CardId != 0)
		{
			num ^= CardId.GetHashCode();
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		if (PrevPlayerId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(PrevPlayerId);
		}
		if (PrevCardId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(PrevCardId);
		}
		if (CardId != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CardId);
		}
		if (TargetId != 0L)
		{
			output.WriteRawTag(41);
			output.WriteSFixed64(TargetId);
		}
		if (PrevTargetId != 0L)
		{
			output.WriteRawTag(49);
			output.WriteSFixed64(PrevTargetId);
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		if (PrevPlayerId != 0L)
		{
			num += 9;
		}
		if (PrevCardId != 0)
		{
			num += 5;
		}
		if (PrevTargetId != 0L)
		{
			num += 9;
		}
		if (CardId != 0)
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
	public void MergeFrom(UseQuickCardC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		if (other.PrevPlayerId != 0L)
		{
			PrevPlayerId = other.PrevPlayerId;
		}
		if (other.PrevCardId != 0)
		{
			PrevCardId = other.PrevCardId;
		}
		if (other.PrevTargetId != 0L)
		{
			PrevTargetId = other.PrevTargetId;
		}
		if (other.CardId != 0)
		{
			CardId = other.CardId;
		}
		if (other.TargetId != 0L)
		{
			TargetId = other.TargetId;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 17u:
				PrevPlayerId = input.ReadSFixed64();
				break;
			case 29u:
				PrevCardId = input.ReadSFixed32();
				break;
			case 37u:
				CardId = input.ReadSFixed32();
				break;
			case 41u:
				TargetId = input.ReadSFixed64();
				break;
			case 49u:
				PrevTargetId = input.ReadSFixed64();
				break;
			}
		}
	}
}
