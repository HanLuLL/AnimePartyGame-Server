using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class BattleChoiceS2C : IMessage<BattleChoiceS2C>, IMessage, IEquatable<BattleChoiceS2C>, IDeepCloneable<BattleChoiceS2C>, IBufferMessage
{
	private static readonly MessageParser<BattleChoiceS2C> _parser = new MessageParser<BattleChoiceS2C>(() => new BattleChoiceS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int ValFieldNumber = 2;

	private int val_;

	public const int DodgeFieldNumber = 3;

	private bool dodge_;

	public const int ExistFightBackFieldNumber = 4;

	private bool existFightBack_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattleChoiceS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[284];

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
	public int Val
	{
		get
		{
			return val_;
		}
		set
		{
			val_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Dodge
	{
		get
		{
			return dodge_;
		}
		set
		{
			dodge_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool ExistFightBack
	{
		get
		{
			return existFightBack_;
		}
		set
		{
			existFightBack_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleChoiceS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleChoiceS2C(BattleChoiceS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		val_ = other.val_;
		dodge_ = other.dodge_;
		existFightBack_ = other.existFightBack_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleChoiceS2C Clone()
	{
		return new BattleChoiceS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattleChoiceS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattleChoiceS2C other)
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
		if (Val != other.Val)
		{
			return false;
		}
		if (Dodge != other.Dodge)
		{
			return false;
		}
		if (ExistFightBack != other.ExistFightBack)
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
		if (Val != 0)
		{
			num ^= Val.GetHashCode();
		}
		if (Dodge)
		{
			num ^= Dodge.GetHashCode();
		}
		if (ExistFightBack)
		{
			num ^= ExistFightBack.GetHashCode();
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
		if (Val != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Val);
		}
		if (Dodge)
		{
			output.WriteRawTag(24);
			output.WriteBool(Dodge);
		}
		if (ExistFightBack)
		{
			output.WriteRawTag(32);
			output.WriteBool(ExistFightBack);
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
		if (Val != 0)
		{
			num += 5;
		}
		if (Dodge)
		{
			num += 2;
		}
		if (ExistFightBack)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattleChoiceS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.Val != 0)
			{
				Val = other.Val;
			}
			if (other.Dodge)
			{
				Dodge = other.Dodge;
			}
			if (other.ExistFightBack)
			{
				ExistFightBack = other.ExistFightBack;
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
				Val = input.ReadSFixed32();
				break;
			case 24u:
				Dodge = input.ReadBool();
				break;
			case 32u:
				ExistFightBack = input.ReadBool();
				break;
			}
		}
	}
}
