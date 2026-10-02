using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Gamble : IMessage<Gamble>, IMessage, IEquatable<Gamble>, IDeepCloneable<Gamble>, IBufferMessage
{
	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static class Types
	{
		public enum state
		{
			[OriginalName("guess")]
			Guess,
			[OriginalName("throw")]
			Throw,
			[OriginalName("result")]
			Result
		}
	}

	private static readonly MessageParser<Gamble> _parser = new MessageParser<Gamble>(() => new Gamble());

	private UnknownFieldSet _unknownFields;

	public const int BaseGoldFieldNumber = 1;

	private int baseGold_;

	public const int BetGoldFieldNumber = 2;

	private int betGold_;

	public const int RolesFieldNumber = 3;

	private static readonly FieldCodec<GambleRole> _repeated_roles_codec = FieldCodec.ForMessage(26u, GambleRole.Parser);

	private readonly RepeatedField<GambleRole> roles_ = new RepeatedField<GambleRole>();

	public const int SFieldNumber = 4;

	private Types.state s_;

	public const int IsOddFieldNumber = 5;

	private bool isOdd_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Gamble> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[93];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BaseGold
	{
		get
		{
			return baseGold_;
		}
		set
		{
			baseGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int BetGold
	{
		get
		{
			return betGold_;
		}
		set
		{
			betGold_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GambleRole> Roles => roles_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Types.state S
	{
		get
		{
			return s_;
		}
		set
		{
			s_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsOdd
	{
		get
		{
			return isOdd_;
		}
		set
		{
			isOdd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Gamble()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Gamble(Gamble other)
		: this()
	{
		baseGold_ = other.baseGold_;
		betGold_ = other.betGold_;
		roles_ = other.roles_.Clone();
		s_ = other.s_;
		isOdd_ = other.isOdd_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Gamble Clone()
	{
		return new Gamble(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Gamble);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Gamble other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (BaseGold != other.BaseGold)
		{
			return false;
		}
		if (BetGold != other.BetGold)
		{
			return false;
		}
		if (!roles_.Equals(other.roles_))
		{
			return false;
		}
		if (S != other.S)
		{
			return false;
		}
		if (IsOdd != other.IsOdd)
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
		if (BaseGold != 0)
		{
			num ^= BaseGold.GetHashCode();
		}
		if (BetGold != 0)
		{
			num ^= BetGold.GetHashCode();
		}
		num ^= roles_.GetHashCode();
		if (S != Types.state.Guess)
		{
			num ^= S.GetHashCode();
		}
		if (IsOdd)
		{
			num ^= IsOdd.GetHashCode();
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
		if (BaseGold != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(BaseGold);
		}
		if (BetGold != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(BetGold);
		}
		roles_.WriteTo(ref output, _repeated_roles_codec);
		if (S != Types.state.Guess)
		{
			output.WriteRawTag(32);
			output.WriteEnum((int)S);
		}
		if (IsOdd)
		{
			output.WriteRawTag(40);
			output.WriteBool(IsOdd);
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
		if (BaseGold != 0)
		{
			num += 5;
		}
		if (BetGold != 0)
		{
			num += 5;
		}
		num += roles_.CalculateSize(_repeated_roles_codec);
		if (S != Types.state.Guess)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)S);
		}
		if (IsOdd)
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
	public void MergeFrom(Gamble other)
	{
		if (other != null)
		{
			if (other.BaseGold != 0)
			{
				BaseGold = other.BaseGold;
			}
			if (other.BetGold != 0)
			{
				BetGold = other.BetGold;
			}
			roles_.Add(other.roles_);
			if (other.S != Types.state.Guess)
			{
				S = other.S;
			}
			if (other.IsOdd)
			{
				IsOdd = other.IsOdd;
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
				BaseGold = input.ReadSFixed32();
				break;
			case 21u:
				BetGold = input.ReadSFixed32();
				break;
			case 26u:
				roles_.AddEntriesFrom(ref input, _repeated_roles_codec);
				break;
			case 32u:
				S = (Types.state)input.ReadEnum();
				break;
			case 40u:
				IsOdd = input.ReadBool();
				break;
			}
		}
	}
}
