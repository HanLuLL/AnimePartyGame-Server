using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SendChatC2S : IMessage<SendChatC2S>, IMessage, IEquatable<SendChatC2S>, IDeepCloneable<SendChatC2S>, IBufferMessage
{
	private static readonly MessageParser<SendChatC2S> _parser = new MessageParser<SendChatC2S>(() => new SendChatC2S());

	private UnknownFieldSet _unknownFields;

	public const int ExpressionIdFieldNumber = 1;

	private int expressionId_;

	public const int PlayerIdsFieldNumber = 2;

	private static readonly FieldCodec<long> _repeated_playerIds_codec = FieldCodec.ForSFixed64(18u);

	private readonly RepeatedField<long> playerIds_ = new RepeatedField<long>();

	public const int PlayerIdFieldNumber = 3;

	private long playerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SendChatC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[347];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ExpressionId
	{
		get
		{
			return expressionId_;
		}
		set
		{
			expressionId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> PlayerIds => playerIds_;

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
	public SendChatC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SendChatC2S(SendChatC2S other)
		: this()
	{
		expressionId_ = other.expressionId_;
		playerIds_ = other.playerIds_.Clone();
		playerId_ = other.playerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SendChatC2S Clone()
	{
		return new SendChatC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SendChatC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SendChatC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ExpressionId != other.ExpressionId)
		{
			return false;
		}
		if (!playerIds_.Equals(other.playerIds_))
		{
			return false;
		}
		if (PlayerId != other.PlayerId)
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
		if (ExpressionId != 0)
		{
			num ^= ExpressionId.GetHashCode();
		}
		num ^= playerIds_.GetHashCode();
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
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
		if (ExpressionId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(ExpressionId);
		}
		playerIds_.WriteTo(ref output, _repeated_playerIds_codec);
		if (PlayerId != 0L)
		{
			output.WriteRawTag(25);
			output.WriteSFixed64(PlayerId);
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
		if (ExpressionId != 0)
		{
			num += 5;
		}
		num += playerIds_.CalculateSize(_repeated_playerIds_codec);
		if (PlayerId != 0L)
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
	public void MergeFrom(SendChatC2S other)
	{
		if (other != null)
		{
			if (other.ExpressionId != 0)
			{
				ExpressionId = other.ExpressionId;
			}
			playerIds_.Add(other.playerIds_);
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
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
				ExpressionId = input.ReadSFixed32();
				break;
			case 17u:
			case 18u:
				playerIds_.AddEntriesFrom(ref input, _repeated_playerIds_codec);
				break;
			case 25u:
				PlayerId = input.ReadSFixed64();
				break;
			}
		}
	}
}
