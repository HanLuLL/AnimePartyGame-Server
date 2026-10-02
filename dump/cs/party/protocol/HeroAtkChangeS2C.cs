using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class HeroAtkChangeS2C : IMessage<HeroAtkChangeS2C>, IMessage, IEquatable<HeroAtkChangeS2C>, IDeepCloneable<HeroAtkChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<HeroAtkChangeS2C> _parser = new MessageParser<HeroAtkChangeS2C>(() => new HeroAtkChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int CurrAtkFieldNumber = 5;

	private int currAtk_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroAtkChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[431];

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
	public int CurrAtk
	{
		get
		{
			return currAtk_;
		}
		set
		{
			currAtk_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroAtkChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroAtkChangeS2C(HeroAtkChangeS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		currAtk_ = other.currAtk_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroAtkChangeS2C Clone()
	{
		return new HeroAtkChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroAtkChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroAtkChangeS2C other)
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
		if (CurrAtk != other.CurrAtk)
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
		if (CurrAtk != 0)
		{
			num ^= CurrAtk.GetHashCode();
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
		if (CurrAtk != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(CurrAtk);
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
		if (CurrAtk != 0)
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
	public void MergeFrom(HeroAtkChangeS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.CurrAtk != 0)
			{
				CurrAtk = other.CurrAtk;
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
			case 45u:
				CurrAtk = input.ReadSFixed32();
				break;
			}
		}
	}
}
