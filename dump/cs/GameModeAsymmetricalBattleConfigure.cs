using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GameModeAsymmetricalBattleConfigure : IMessage<GameModeAsymmetricalBattleConfigure>, IMessage, IEquatable<GameModeAsymmetricalBattleConfigure>, IDeepCloneable<GameModeAsymmetricalBattleConfigure>, IBufferMessage
{
	private static readonly MessageParser<GameModeAsymmetricalBattleConfigure> _parser = new MessageParser<GameModeAsymmetricalBattleConfigure>(() => new GameModeAsymmetricalBattleConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int AsymmetricalDefenderWinRoundFieldNumber = 2;

	private int asymmetricalDefenderWinRound_;

	public const int AsymmetricalAttackerWinScoreFieldNumber = 3;

	private int asymmetricalAttackerWinScore_;

	public const int AsymmetricalAttackerReviveRoundFieldNumber = 4;

	private int asymmetricalAttackerReviveRound_;

	public const int AsymmetricalSpeedRoundFieldNumber = 5;

	private int asymmetricalSpeedRound_;

	public const int SpeedRoundAttrUpFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_speedRoundAttrUp_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> speedRoundAttrUp_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameModeAsymmetricalBattleConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GameModeReflection.Descriptor.MessageTypes[4];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		private set
		{
			id_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AsymmetricalDefenderWinRound
	{
		get
		{
			return asymmetricalDefenderWinRound_;
		}
		private set
		{
			asymmetricalDefenderWinRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AsymmetricalAttackerWinScore
	{
		get
		{
			return asymmetricalAttackerWinScore_;
		}
		private set
		{
			asymmetricalAttackerWinScore_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AsymmetricalAttackerReviveRound
	{
		get
		{
			return asymmetricalAttackerReviveRound_;
		}
		private set
		{
			asymmetricalAttackerReviveRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AsymmetricalSpeedRound
	{
		get
		{
			return asymmetricalSpeedRound_;
		}
		private set
		{
			asymmetricalSpeedRound_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> SpeedRoundAttrUp => speedRoundAttrUp_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeAsymmetricalBattleConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeAsymmetricalBattleConfigure(GameModeAsymmetricalBattleConfigure other)
		: this()
	{
		id_ = other.id_;
		asymmetricalDefenderWinRound_ = other.asymmetricalDefenderWinRound_;
		asymmetricalAttackerWinScore_ = other.asymmetricalAttackerWinScore_;
		asymmetricalAttackerReviveRound_ = other.asymmetricalAttackerReviveRound_;
		asymmetricalSpeedRound_ = other.asymmetricalSpeedRound_;
		speedRoundAttrUp_ = other.speedRoundAttrUp_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeAsymmetricalBattleConfigure Clone()
	{
		return new GameModeAsymmetricalBattleConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameModeAsymmetricalBattleConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameModeAsymmetricalBattleConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (AsymmetricalDefenderWinRound != other.AsymmetricalDefenderWinRound)
		{
			return false;
		}
		if (AsymmetricalAttackerWinScore != other.AsymmetricalAttackerWinScore)
		{
			return false;
		}
		if (AsymmetricalAttackerReviveRound != other.AsymmetricalAttackerReviveRound)
		{
			return false;
		}
		if (AsymmetricalSpeedRound != other.AsymmetricalSpeedRound)
		{
			return false;
		}
		if (!speedRoundAttrUp_.Equals(other.speedRoundAttrUp_))
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (AsymmetricalDefenderWinRound != 0)
		{
			num ^= AsymmetricalDefenderWinRound.GetHashCode();
		}
		if (AsymmetricalAttackerWinScore != 0)
		{
			num ^= AsymmetricalAttackerWinScore.GetHashCode();
		}
		if (AsymmetricalAttackerReviveRound != 0)
		{
			num ^= AsymmetricalAttackerReviveRound.GetHashCode();
		}
		if (AsymmetricalSpeedRound != 0)
		{
			num ^= AsymmetricalSpeedRound.GetHashCode();
		}
		num ^= speedRoundAttrUp_.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (AsymmetricalDefenderWinRound != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(AsymmetricalDefenderWinRound);
		}
		if (AsymmetricalAttackerWinScore != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(AsymmetricalAttackerWinScore);
		}
		if (AsymmetricalAttackerReviveRound != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(AsymmetricalAttackerReviveRound);
		}
		if (AsymmetricalSpeedRound != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(AsymmetricalSpeedRound);
		}
		speedRoundAttrUp_.WriteTo(ref output, _repeated_speedRoundAttrUp_codec);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (AsymmetricalDefenderWinRound != 0)
		{
			num += 5;
		}
		if (AsymmetricalAttackerWinScore != 0)
		{
			num += 5;
		}
		if (AsymmetricalAttackerReviveRound != 0)
		{
			num += 5;
		}
		if (AsymmetricalSpeedRound != 0)
		{
			num += 5;
		}
		num += speedRoundAttrUp_.CalculateSize(_repeated_speedRoundAttrUp_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GameModeAsymmetricalBattleConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.AsymmetricalDefenderWinRound != 0)
			{
				AsymmetricalDefenderWinRound = other.AsymmetricalDefenderWinRound;
			}
			if (other.AsymmetricalAttackerWinScore != 0)
			{
				AsymmetricalAttackerWinScore = other.AsymmetricalAttackerWinScore;
			}
			if (other.AsymmetricalAttackerReviveRound != 0)
			{
				AsymmetricalAttackerReviveRound = other.AsymmetricalAttackerReviveRound;
			}
			if (other.AsymmetricalSpeedRound != 0)
			{
				AsymmetricalSpeedRound = other.AsymmetricalSpeedRound;
			}
			speedRoundAttrUp_.Add(other.speedRoundAttrUp_);
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
				Id = input.ReadSFixed32();
				break;
			case 21u:
				AsymmetricalDefenderWinRound = input.ReadSFixed32();
				break;
			case 29u:
				AsymmetricalAttackerWinScore = input.ReadSFixed32();
				break;
			case 37u:
				AsymmetricalAttackerReviveRound = input.ReadSFixed32();
				break;
			case 45u:
				AsymmetricalSpeedRound = input.ReadSFixed32();
				break;
			case 50u:
			case 53u:
				speedRoundAttrUp_.AddEntriesFrom(ref input, _repeated_speedRoundAttrUp_codec);
				break;
			}
		}
	}
}
