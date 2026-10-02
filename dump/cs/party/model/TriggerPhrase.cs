using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class TriggerPhrase : IMessage<TriggerPhrase>, IMessage, IEquatable<TriggerPhrase>, IDeepCloneable<TriggerPhrase>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum Type
		{
			[OriginalName("none")]
			None,
			[OriginalName("transfer_gold")]
			TransferGold,
			[OriginalName("give_card")]
			GiveCard,
			[OriginalName("cure_friend")]
			CureFriend,
			[OriginalName("kill_boss")]
			KillBoss
		}
	}

	private static readonly MessageParser<TriggerPhrase> _parser = new MessageParser<TriggerPhrase>(() => new TriggerPhrase());

	private UnknownFieldSet _unknownFields;

	public const int ActivePlayerIdFieldNumber = 1;

	private long activePlayerId_;

	public const int PassivePlayerIdFieldNumber = 2;

	private long passivePlayerId_;

	public const int TriggerTypeFieldNumber = 3;

	private Types.Type triggerType_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<TriggerPhrase> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[66];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long ActivePlayerId
	{
		get
		{
			return activePlayerId_;
		}
		set
		{
			activePlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long PassivePlayerId
	{
		get
		{
			return passivePlayerId_;
		}
		set
		{
			passivePlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.Type TriggerType
	{
		get
		{
			return triggerType_;
		}
		set
		{
			triggerType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerPhrase()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerPhrase(TriggerPhrase other)
		: this()
	{
		activePlayerId_ = other.activePlayerId_;
		passivePlayerId_ = other.passivePlayerId_;
		triggerType_ = other.triggerType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerPhrase Clone()
	{
		return new TriggerPhrase(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as TriggerPhrase);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(TriggerPhrase other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ActivePlayerId != other.ActivePlayerId)
		{
			return false;
		}
		if (PassivePlayerId != other.PassivePlayerId)
		{
			return false;
		}
		if (TriggerType != other.TriggerType)
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
		if (ActivePlayerId != 0L)
		{
			num ^= ActivePlayerId.GetHashCode();
		}
		if (PassivePlayerId != 0L)
		{
			num ^= PassivePlayerId.GetHashCode();
		}
		if (TriggerType != Types.Type.None)
		{
			num ^= TriggerType.GetHashCode();
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
		if (ActivePlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(ActivePlayerId);
		}
		if (PassivePlayerId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(PassivePlayerId);
		}
		if (TriggerType != Types.Type.None)
		{
			output.WriteRawTag(24);
			output.WriteEnum((int)TriggerType);
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
		if (ActivePlayerId != 0L)
		{
			num += 9;
		}
		if (PassivePlayerId != 0L)
		{
			num += 9;
		}
		if (TriggerType != Types.Type.None)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)TriggerType);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(TriggerPhrase other)
	{
		if (other != null)
		{
			if (other.ActivePlayerId != 0L)
			{
				ActivePlayerId = other.ActivePlayerId;
			}
			if (other.PassivePlayerId != 0L)
			{
				PassivePlayerId = other.PassivePlayerId;
			}
			if (other.TriggerType != Types.Type.None)
			{
				TriggerType = other.TriggerType;
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
				ActivePlayerId = input.ReadSFixed64();
				break;
			case 17u:
				PassivePlayerId = input.ReadSFixed64();
				break;
			case 24u:
				TriggerType = (Types.Type)input.ReadEnum();
				break;
			}
		}
	}
}
