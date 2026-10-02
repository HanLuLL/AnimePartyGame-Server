using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class SkillTriggerNotifyS2C : IMessage<SkillTriggerNotifyS2C>, IMessage, IEquatable<SkillTriggerNotifyS2C>, IDeepCloneable<SkillTriggerNotifyS2C>, IBufferMessage
{
	private static readonly MessageParser<SkillTriggerNotifyS2C> _parser = new MessageParser<SkillTriggerNotifyS2C>(() => new SkillTriggerNotifyS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int SkillIdFieldNumber = 2;

	private int skillId_;

	public const int BuffIdFieldNumber = 3;

	private int buffId_;

	public const int TriggerFieldNumber = 4;

	private int trigger_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SkillTriggerNotifyS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[381];

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
	public int SkillId
	{
		get
		{
			return skillId_;
		}
		set
		{
			skillId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BuffId
	{
		get
		{
			return buffId_;
		}
		set
		{
			buffId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Trigger
	{
		get
		{
			return trigger_;
		}
		set
		{
			trigger_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkillTriggerNotifyS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkillTriggerNotifyS2C(SkillTriggerNotifyS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		skillId_ = other.skillId_;
		buffId_ = other.buffId_;
		trigger_ = other.trigger_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SkillTriggerNotifyS2C Clone()
	{
		return new SkillTriggerNotifyS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SkillTriggerNotifyS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SkillTriggerNotifyS2C other)
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
		if (SkillId != other.SkillId)
		{
			return false;
		}
		if (BuffId != other.BuffId)
		{
			return false;
		}
		if (Trigger != other.Trigger)
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
		if (SkillId != 0)
		{
			num ^= SkillId.GetHashCode();
		}
		if (BuffId != 0)
		{
			num ^= BuffId.GetHashCode();
		}
		if (Trigger != 0)
		{
			num ^= Trigger.GetHashCode();
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
		if (SkillId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(SkillId);
		}
		if (BuffId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(BuffId);
		}
		if (Trigger != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Trigger);
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
		if (SkillId != 0)
		{
			num += 5;
		}
		if (BuffId != 0)
		{
			num += 5;
		}
		if (Trigger != 0)
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
	public void MergeFrom(SkillTriggerNotifyS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.SkillId != 0)
			{
				SkillId = other.SkillId;
			}
			if (other.BuffId != 0)
			{
				BuffId = other.BuffId;
			}
			if (other.Trigger != 0)
			{
				Trigger = other.Trigger;
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
				SkillId = input.ReadSFixed32();
				break;
			case 29u:
				BuffId = input.ReadSFixed32();
				break;
			case 37u:
				Trigger = input.ReadSFixed32();
				break;
			}
		}
	}
}
