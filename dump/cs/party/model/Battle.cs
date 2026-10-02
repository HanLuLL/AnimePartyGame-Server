using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class Battle : IMessage<Battle>, IMessage, IEquatable<Battle>, IDeepCloneable<Battle>, IBufferMessage
{
	private static readonly MessageParser<Battle> _parser = new MessageParser<Battle>(() => new Battle());

	private UnknownFieldSet _unknownFields;

	public const int BattleIdFieldNumber = 1;

	private long battleId_;

	public const int AttackerFieldNumber = 2;

	private BattleRole attacker_;

	public const int DefenderFieldNumber = 3;

	private BattleRole defender_;

	public const int CardUseStateFieldNumber = 4;

	private static readonly MapField<long, bool>.Codec _map_cardUseState_codec = new MapField<long, bool>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForBool(16u, defaultValue: false), 34u);

	private readonly MapField<long, bool> cardUseState_ = new MapField<long, bool>();

	public const int IsEndFieldNumber = 5;

	private bool isEnd_;

	public const int IfNoWinMustDieFieldNumber = 6;

	private bool ifNoWinMustDie_;

	public const int IsPursuitFieldNumber = 7;

	private bool isPursuit_;

	public const int FightBackFieldNumber = 8;

	private bool fightBack_;

	public const int SkillPlayerIdFieldNumber = 9;

	private long skillPlayerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<Battle> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[90];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long BattleId
	{
		get
		{
			return battleId_;
		}
		set
		{
			battleId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleRole Attacker
	{
		get
		{
			return attacker_;
		}
		set
		{
			attacker_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleRole Defender
	{
		get
		{
			return defender_;
		}
		set
		{
			defender_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, bool> CardUseState => cardUseState_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsEnd
	{
		get
		{
			return isEnd_;
		}
		set
		{
			isEnd_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IfNoWinMustDie
	{
		get
		{
			return ifNoWinMustDie_;
		}
		set
		{
			ifNoWinMustDie_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsPursuit
	{
		get
		{
			return isPursuit_;
		}
		set
		{
			isPursuit_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool FightBack
	{
		get
		{
			return fightBack_;
		}
		set
		{
			fightBack_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long SkillPlayerId
	{
		get
		{
			return skillPlayerId_;
		}
		set
		{
			skillPlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Battle()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Battle(Battle other)
		: this()
	{
		battleId_ = other.battleId_;
		attacker_ = ((other.attacker_ != null) ? other.attacker_.Clone() : null);
		defender_ = ((other.defender_ != null) ? other.defender_.Clone() : null);
		cardUseState_ = other.cardUseState_.Clone();
		isEnd_ = other.isEnd_;
		ifNoWinMustDie_ = other.ifNoWinMustDie_;
		isPursuit_ = other.isPursuit_;
		fightBack_ = other.fightBack_;
		skillPlayerId_ = other.skillPlayerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Battle Clone()
	{
		return new Battle(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as Battle);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(Battle other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (BattleId != other.BattleId)
		{
			return false;
		}
		if (!object.Equals(Attacker, other.Attacker))
		{
			return false;
		}
		if (!object.Equals(Defender, other.Defender))
		{
			return false;
		}
		if (!CardUseState.Equals(other.CardUseState))
		{
			return false;
		}
		if (IsEnd != other.IsEnd)
		{
			return false;
		}
		if (IfNoWinMustDie != other.IfNoWinMustDie)
		{
			return false;
		}
		if (IsPursuit != other.IsPursuit)
		{
			return false;
		}
		if (FightBack != other.FightBack)
		{
			return false;
		}
		if (SkillPlayerId != other.SkillPlayerId)
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
		if (BattleId != 0L)
		{
			num ^= BattleId.GetHashCode();
		}
		if (attacker_ != null)
		{
			num ^= Attacker.GetHashCode();
		}
		if (defender_ != null)
		{
			num ^= Defender.GetHashCode();
		}
		num ^= CardUseState.GetHashCode();
		if (IsEnd)
		{
			num ^= IsEnd.GetHashCode();
		}
		if (IfNoWinMustDie)
		{
			num ^= IfNoWinMustDie.GetHashCode();
		}
		if (IsPursuit)
		{
			num ^= IsPursuit.GetHashCode();
		}
		if (FightBack)
		{
			num ^= FightBack.GetHashCode();
		}
		if (SkillPlayerId != 0L)
		{
			num ^= SkillPlayerId.GetHashCode();
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
		if (BattleId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(BattleId);
		}
		if (attacker_ != null)
		{
			output.WriteRawTag(18);
			output.WriteMessage(Attacker);
		}
		if (defender_ != null)
		{
			output.WriteRawTag(26);
			output.WriteMessage(Defender);
		}
		cardUseState_.WriteTo(ref output, _map_cardUseState_codec);
		if (IsEnd)
		{
			output.WriteRawTag(40);
			output.WriteBool(IsEnd);
		}
		if (IfNoWinMustDie)
		{
			output.WriteRawTag(48);
			output.WriteBool(IfNoWinMustDie);
		}
		if (IsPursuit)
		{
			output.WriteRawTag(56);
			output.WriteBool(IsPursuit);
		}
		if (FightBack)
		{
			output.WriteRawTag(64);
			output.WriteBool(FightBack);
		}
		if (SkillPlayerId != 0L)
		{
			output.WriteRawTag(73);
			output.WriteSFixed64(SkillPlayerId);
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
		if (BattleId != 0L)
		{
			num += 9;
		}
		if (attacker_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Attacker);
		}
		if (defender_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Defender);
		}
		num += cardUseState_.CalculateSize(_map_cardUseState_codec);
		if (IsEnd)
		{
			num += 2;
		}
		if (IfNoWinMustDie)
		{
			num += 2;
		}
		if (IsPursuit)
		{
			num += 2;
		}
		if (FightBack)
		{
			num += 2;
		}
		if (SkillPlayerId != 0L)
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
	public void MergeFrom(Battle other)
	{
		if (other == null)
		{
			return;
		}
		if (other.BattleId != 0L)
		{
			BattleId = other.BattleId;
		}
		if (other.attacker_ != null)
		{
			if (attacker_ == null)
			{
				Attacker = new BattleRole();
			}
			Attacker.MergeFrom(other.Attacker);
		}
		if (other.defender_ != null)
		{
			if (defender_ == null)
			{
				Defender = new BattleRole();
			}
			Defender.MergeFrom(other.Defender);
		}
		cardUseState_.MergeFrom(other.cardUseState_);
		if (other.IsEnd)
		{
			IsEnd = other.IsEnd;
		}
		if (other.IfNoWinMustDie)
		{
			IfNoWinMustDie = other.IfNoWinMustDie;
		}
		if (other.IsPursuit)
		{
			IsPursuit = other.IsPursuit;
		}
		if (other.FightBack)
		{
			FightBack = other.FightBack;
		}
		if (other.SkillPlayerId != 0L)
		{
			SkillPlayerId = other.SkillPlayerId;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				BattleId = input.ReadSFixed64();
				break;
			case 18u:
				if (attacker_ == null)
				{
					Attacker = new BattleRole();
				}
				input.ReadMessage(Attacker);
				break;
			case 26u:
				if (defender_ == null)
				{
					Defender = new BattleRole();
				}
				input.ReadMessage(Defender);
				break;
			case 34u:
				cardUseState_.AddEntriesFrom(ref input, _map_cardUseState_codec);
				break;
			case 40u:
				IsEnd = input.ReadBool();
				break;
			case 48u:
				IfNoWinMustDie = input.ReadBool();
				break;
			case 56u:
				IsPursuit = input.ReadBool();
				break;
			case 64u:
				FightBack = input.ReadBool();
				break;
			case 73u:
				SkillPlayerId = input.ReadSFixed64();
				break;
			}
		}
	}
}
