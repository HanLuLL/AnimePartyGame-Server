using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class MonsterNotUseSkillS2C : IMessage<MonsterNotUseSkillS2C>, IMessage, IEquatable<MonsterNotUseSkillS2C>, IDeepCloneable<MonsterNotUseSkillS2C>, IBufferMessage
{
	private static readonly MessageParser<MonsterNotUseSkillS2C> _parser = new MessageParser<MonsterNotUseSkillS2C>(() => new MonsterNotUseSkillS2C());

	private UnknownFieldSet _unknownFields;

	public const int NotUseSkillFieldNumber = 1;

	private bool notUseSkill_;

	public const int PlayerIdFieldNumber = 2;

	private long playerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MonsterNotUseSkillS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[266];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool NotUseSkill
	{
		get
		{
			return notUseSkill_;
		}
		set
		{
			notUseSkill_ = value;
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
	public MonsterNotUseSkillS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterNotUseSkillS2C(MonsterNotUseSkillS2C other)
		: this()
	{
		notUseSkill_ = other.notUseSkill_;
		playerId_ = other.playerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterNotUseSkillS2C Clone()
	{
		return new MonsterNotUseSkillS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MonsterNotUseSkillS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MonsterNotUseSkillS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (NotUseSkill != other.NotUseSkill)
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
		if (NotUseSkill)
		{
			num ^= NotUseSkill.GetHashCode();
		}
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
		if (NotUseSkill)
		{
			output.WriteRawTag(8);
			output.WriteBool(NotUseSkill);
		}
		if (PlayerId != 0L)
		{
			output.WriteRawTag(17);
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
		if (NotUseSkill)
		{
			num += 2;
		}
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
	public void MergeFrom(MonsterNotUseSkillS2C other)
	{
		if (other != null)
		{
			if (other.NotUseSkill)
			{
				NotUseSkill = other.NotUseSkill;
			}
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
			case 8u:
				NotUseSkill = input.ReadBool();
				break;
			case 17u:
				PlayerId = input.ReadSFixed64();
				break;
			}
		}
	}
}
