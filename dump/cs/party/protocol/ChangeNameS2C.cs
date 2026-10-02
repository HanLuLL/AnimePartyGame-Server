using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChangeNameS2C : IMessage<ChangeNameS2C>, IMessage, IEquatable<ChangeNameS2C>, IDeepCloneable<ChangeNameS2C>, IBufferMessage
{
	private static readonly MessageParser<ChangeNameS2C> _parser = new MessageParser<ChangeNameS2C>(() => new ChangeNameS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerNameFieldNumber = 1;

	private string playerName_ = "";

	public const int NextChangeNameTimeFieldNumber = 2;

	private long nextChangeNameTime_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChangeNameS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[535];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string PlayerName
	{
		get
		{
			return playerName_;
		}
		set
		{
			playerName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long NextChangeNameTime
	{
		get
		{
			return nextChangeNameTime_;
		}
		set
		{
			nextChangeNameTime_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeNameS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeNameS2C(ChangeNameS2C other)
		: this()
	{
		playerName_ = other.playerName_;
		nextChangeNameTime_ = other.nextChangeNameTime_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeNameS2C Clone()
	{
		return new ChangeNameS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChangeNameS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChangeNameS2C other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PlayerName != other.PlayerName)
		{
			return false;
		}
		if (NextChangeNameTime != other.NextChangeNameTime)
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
		if (PlayerName.Length != 0)
		{
			num ^= PlayerName.GetHashCode();
		}
		if (NextChangeNameTime != 0L)
		{
			num ^= NextChangeNameTime.GetHashCode();
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
		if (PlayerName.Length != 0)
		{
			output.WriteRawTag(10);
			output.WriteString(PlayerName);
		}
		if (NextChangeNameTime != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(NextChangeNameTime);
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
		if (PlayerName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PlayerName);
		}
		if (NextChangeNameTime != 0L)
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
	public void MergeFrom(ChangeNameS2C other)
	{
		if (other != null)
		{
			if (other.PlayerName.Length != 0)
			{
				PlayerName = other.PlayerName;
			}
			if (other.NextChangeNameTime != 0L)
			{
				NextChangeNameTime = other.NextChangeNameTime;
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
			case 10u:
				PlayerName = input.ReadString();
				break;
			case 17u:
				NextChangeNameTime = input.ReadSFixed64();
				break;
			}
		}
	}
}
