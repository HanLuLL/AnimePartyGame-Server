using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class HeroSpecialScoreChangeS2C : IMessage<HeroSpecialScoreChangeS2C>, IMessage, IEquatable<HeroSpecialScoreChangeS2C>, IDeepCloneable<HeroSpecialScoreChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<HeroSpecialScoreChangeS2C> _parser = new MessageParser<HeroSpecialScoreChangeS2C>(() => new HeroSpecialScoreChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int ChangeScoreFieldNumber = 2;

	private int changeScore_;

	public const int OriScoreFieldNumber = 3;

	private int oriScore_;

	public const int CurrScoreFieldNumber = 4;

	private int currScore_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroSpecialScoreChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[427];

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
	public int ChangeScore
	{
		get
		{
			return changeScore_;
		}
		set
		{
			changeScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OriScore
	{
		get
		{
			return oriScore_;
		}
		set
		{
			oriScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrScore
	{
		get
		{
			return currScore_;
		}
		set
		{
			currScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroSpecialScoreChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroSpecialScoreChangeS2C(HeroSpecialScoreChangeS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		changeScore_ = other.changeScore_;
		oriScore_ = other.oriScore_;
		currScore_ = other.currScore_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroSpecialScoreChangeS2C Clone()
	{
		return new HeroSpecialScoreChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroSpecialScoreChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroSpecialScoreChangeS2C other)
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
		if (ChangeScore != other.ChangeScore)
		{
			return false;
		}
		if (OriScore != other.OriScore)
		{
			return false;
		}
		if (CurrScore != other.CurrScore)
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
		if (ChangeScore != 0)
		{
			num ^= ChangeScore.GetHashCode();
		}
		if (OriScore != 0)
		{
			num ^= OriScore.GetHashCode();
		}
		if (CurrScore != 0)
		{
			num ^= CurrScore.GetHashCode();
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
		if (ChangeScore != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ChangeScore);
		}
		if (OriScore != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(OriScore);
		}
		if (CurrScore != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CurrScore);
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
		if (ChangeScore != 0)
		{
			num += 5;
		}
		if (OriScore != 0)
		{
			num += 5;
		}
		if (CurrScore != 0)
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
	public void MergeFrom(HeroSpecialScoreChangeS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.ChangeScore != 0)
			{
				ChangeScore = other.ChangeScore;
			}
			if (other.OriScore != 0)
			{
				OriScore = other.OriScore;
			}
			if (other.CurrScore != 0)
			{
				CurrScore = other.CurrScore;
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
				ChangeScore = input.ReadSFixed32();
				break;
			case 29u:
				OriScore = input.ReadSFixed32();
				break;
			case 37u:
				CurrScore = input.ReadSFixed32();
				break;
			}
		}
	}
}
