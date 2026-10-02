using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class HeroUseCardNumChangeS2C : IMessage<HeroUseCardNumChangeS2C>, IMessage, IEquatable<HeroUseCardNumChangeS2C>, IDeepCloneable<HeroUseCardNumChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<HeroUseCardNumChangeS2C> _parser = new MessageParser<HeroUseCardNumChangeS2C>(() => new HeroUseCardNumChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int UseCardNumFieldNumber = 2;

	private int useCardNum_;

	public const int UseCardMaxNumFieldNumber = 3;

	private int useCardMaxNum_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroUseCardNumChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[403];

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
	public int UseCardNum
	{
		get
		{
			return useCardNum_;
		}
		set
		{
			useCardNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseCardMaxNum
	{
		get
		{
			return useCardMaxNum_;
		}
		set
		{
			useCardMaxNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroUseCardNumChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroUseCardNumChangeS2C(HeroUseCardNumChangeS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		useCardNum_ = other.useCardNum_;
		useCardMaxNum_ = other.useCardMaxNum_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroUseCardNumChangeS2C Clone()
	{
		return new HeroUseCardNumChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroUseCardNumChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroUseCardNumChangeS2C other)
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
		if (UseCardNum != other.UseCardNum)
		{
			return false;
		}
		if (UseCardMaxNum != other.UseCardMaxNum)
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
		if (UseCardNum != 0)
		{
			num ^= UseCardNum.GetHashCode();
		}
		if (UseCardMaxNum != 0)
		{
			num ^= UseCardMaxNum.GetHashCode();
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
		if (UseCardNum != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(UseCardNum);
		}
		if (UseCardMaxNum != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(UseCardMaxNum);
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
		if (UseCardNum != 0)
		{
			num += 5;
		}
		if (UseCardMaxNum != 0)
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
	public void MergeFrom(HeroUseCardNumChangeS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.UseCardNum != 0)
			{
				UseCardNum = other.UseCardNum;
			}
			if (other.UseCardMaxNum != 0)
			{
				UseCardMaxNum = other.UseCardMaxNum;
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
				UseCardNum = input.ReadSFixed32();
				break;
			case 29u:
				UseCardMaxNum = input.ReadSFixed32();
				break;
			}
		}
	}
}
