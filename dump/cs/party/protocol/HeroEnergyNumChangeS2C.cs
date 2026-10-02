using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class HeroEnergyNumChangeS2C : IMessage<HeroEnergyNumChangeS2C>, IMessage, IEquatable<HeroEnergyNumChangeS2C>, IDeepCloneable<HeroEnergyNumChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<HeroEnergyNumChangeS2C> _parser = new MessageParser<HeroEnergyNumChangeS2C>(() => new HeroEnergyNumChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int ChangeNumFieldNumber = 2;

	private int changeNum_;

	public const int OriNumFieldNumber = 3;

	private int oriNum_;

	public const int CurrNumFieldNumber = 4;

	private int currNum_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroEnergyNumChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[410];

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
	public int ChangeNum
	{
		get
		{
			return changeNum_;
		}
		set
		{
			changeNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OriNum
	{
		get
		{
			return oriNum_;
		}
		set
		{
			oriNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrNum
	{
		get
		{
			return currNum_;
		}
		set
		{
			currNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroEnergyNumChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroEnergyNumChangeS2C(HeroEnergyNumChangeS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		changeNum_ = other.changeNum_;
		oriNum_ = other.oriNum_;
		currNum_ = other.currNum_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroEnergyNumChangeS2C Clone()
	{
		return new HeroEnergyNumChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroEnergyNumChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroEnergyNumChangeS2C other)
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
		if (ChangeNum != other.ChangeNum)
		{
			return false;
		}
		if (OriNum != other.OriNum)
		{
			return false;
		}
		if (CurrNum != other.CurrNum)
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
		if (ChangeNum != 0)
		{
			num ^= ChangeNum.GetHashCode();
		}
		if (OriNum != 0)
		{
			num ^= OriNum.GetHashCode();
		}
		if (CurrNum != 0)
		{
			num ^= CurrNum.GetHashCode();
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
		if (ChangeNum != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ChangeNum);
		}
		if (OriNum != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(OriNum);
		}
		if (CurrNum != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CurrNum);
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
		if (ChangeNum != 0)
		{
			num += 5;
		}
		if (OriNum != 0)
		{
			num += 5;
		}
		if (CurrNum != 0)
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
	public void MergeFrom(HeroEnergyNumChangeS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.ChangeNum != 0)
			{
				ChangeNum = other.ChangeNum;
			}
			if (other.OriNum != 0)
			{
				OriNum = other.OriNum;
			}
			if (other.CurrNum != 0)
			{
				CurrNum = other.CurrNum;
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
				ChangeNum = input.ReadSFixed32();
				break;
			case 29u:
				OriNum = input.ReadSFixed32();
				break;
			case 37u:
				CurrNum = input.ReadSFixed32();
				break;
			}
		}
	}
}
