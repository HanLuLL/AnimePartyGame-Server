using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class HeroGoldChangeS2C : IMessage<HeroGoldChangeS2C>, IMessage, IEquatable<HeroGoldChangeS2C>, IDeepCloneable<HeroGoldChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<HeroGoldChangeS2C> _parser = new MessageParser<HeroGoldChangeS2C>(() => new HeroGoldChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int ChangeGoldFieldNumber = 2;

	private int changeGold_;

	public const int OriGoldFieldNumber = 3;

	private int oriGold_;

	public const int CurrGoldFieldNumber = 4;

	private int currGold_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroGoldChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[406];

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
	public int ChangeGold
	{
		get
		{
			return changeGold_;
		}
		set
		{
			changeGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OriGold
	{
		get
		{
			return oriGold_;
		}
		set
		{
			oriGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrGold
	{
		get
		{
			return currGold_;
		}
		set
		{
			currGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroGoldChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroGoldChangeS2C(HeroGoldChangeS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		changeGold_ = other.changeGold_;
		oriGold_ = other.oriGold_;
		currGold_ = other.currGold_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroGoldChangeS2C Clone()
	{
		return new HeroGoldChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroGoldChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroGoldChangeS2C other)
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
		if (ChangeGold != other.ChangeGold)
		{
			return false;
		}
		if (OriGold != other.OriGold)
		{
			return false;
		}
		if (CurrGold != other.CurrGold)
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
		if (ChangeGold != 0)
		{
			num ^= ChangeGold.GetHashCode();
		}
		if (OriGold != 0)
		{
			num ^= OriGold.GetHashCode();
		}
		if (CurrGold != 0)
		{
			num ^= CurrGold.GetHashCode();
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
		if (ChangeGold != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ChangeGold);
		}
		if (OriGold != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(OriGold);
		}
		if (CurrGold != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CurrGold);
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
		if (ChangeGold != 0)
		{
			num += 5;
		}
		if (OriGold != 0)
		{
			num += 5;
		}
		if (CurrGold != 0)
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
	public void MergeFrom(HeroGoldChangeS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.ChangeGold != 0)
			{
				ChangeGold = other.ChangeGold;
			}
			if (other.OriGold != 0)
			{
				OriGold = other.OriGold;
			}
			if (other.CurrGold != 0)
			{
				CurrGold = other.CurrGold;
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
				ChangeGold = input.ReadSFixed32();
				break;
			case 29u:
				OriGold = input.ReadSFixed32();
				break;
			case 37u:
				CurrGold = input.ReadSFixed32();
				break;
			}
		}
	}
}
