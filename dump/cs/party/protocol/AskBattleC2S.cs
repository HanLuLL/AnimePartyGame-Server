using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class AskBattleC2S : IMessage<AskBattleC2S>, IMessage, IEquatable<AskBattleC2S>, IDeepCloneable<AskBattleC2S>, IBufferMessage
{
	private static readonly MessageParser<AskBattleC2S> _parser = new MessageParser<AskBattleC2S>(() => new AskBattleC2S());

	private UnknownFieldSet _unknownFields;

	public const int InfoFieldNumber = 1;

	private ActionInfo info_;

	public const int AskPlayerIdFieldNumber = 2;

	private long askPlayerId_;

	public const int IsBattleFieldNumber = 3;

	private bool isBattle_;

	public const int IsPursuitFieldNumber = 4;

	private bool isPursuit_;

	public const int FightBackFieldNumber = 5;

	private bool fightBack_;

	public const int SkillPlayerIdFieldNumber = 6;

	private long skillPlayerId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<AskBattleC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[289];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionInfo Info
	{
		get
		{
			return info_;
		}
		set
		{
			info_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long AskPlayerId
	{
		get
		{
			return askPlayerId_;
		}
		set
		{
			askPlayerId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsBattle
	{
		get
		{
			return isBattle_;
		}
		set
		{
			isBattle_ = value;
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
	public AskBattleC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AskBattleC2S(AskBattleC2S other)
		: this()
	{
		info_ = ((other.info_ != null) ? other.info_.Clone() : null);
		askPlayerId_ = other.askPlayerId_;
		isBattle_ = other.isBattle_;
		isPursuit_ = other.isPursuit_;
		fightBack_ = other.fightBack_;
		skillPlayerId_ = other.skillPlayerId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AskBattleC2S Clone()
	{
		return new AskBattleC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AskBattleC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(AskBattleC2S other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Info, other.Info))
		{
			return false;
		}
		if (AskPlayerId != other.AskPlayerId)
		{
			return false;
		}
		if (IsBattle != other.IsBattle)
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
		if (info_ != null)
		{
			num ^= Info.GetHashCode();
		}
		if (AskPlayerId != 0L)
		{
			num ^= AskPlayerId.GetHashCode();
		}
		if (IsBattle)
		{
			num ^= IsBattle.GetHashCode();
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
		if (info_ != null)
		{
			output.WriteRawTag(10);
			output.WriteMessage(Info);
		}
		if (AskPlayerId != 0L)
		{
			output.WriteRawTag(17);
			output.WriteSFixed64(AskPlayerId);
		}
		if (IsBattle)
		{
			output.WriteRawTag(24);
			output.WriteBool(IsBattle);
		}
		if (IsPursuit)
		{
			output.WriteRawTag(32);
			output.WriteBool(IsPursuit);
		}
		if (FightBack)
		{
			output.WriteRawTag(40);
			output.WriteBool(FightBack);
		}
		if (SkillPlayerId != 0L)
		{
			output.WriteRawTag(49);
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
		if (info_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Info);
		}
		if (AskPlayerId != 0L)
		{
			num += 9;
		}
		if (IsBattle)
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
	public void MergeFrom(AskBattleC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.info_ != null)
		{
			if (info_ == null)
			{
				Info = new ActionInfo();
			}
			Info.MergeFrom(other.Info);
		}
		if (other.AskPlayerId != 0L)
		{
			AskPlayerId = other.AskPlayerId;
		}
		if (other.IsBattle)
		{
			IsBattle = other.IsBattle;
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
			case 10u:
				if (info_ == null)
				{
					Info = new ActionInfo();
				}
				input.ReadMessage(Info);
				break;
			case 17u:
				AskPlayerId = input.ReadSFixed64();
				break;
			case 24u:
				IsBattle = input.ReadBool();
				break;
			case 32u:
				IsPursuit = input.ReadBool();
				break;
			case 40u:
				FightBack = input.ReadBool();
				break;
			case 49u:
				SkillPlayerId = input.ReadSFixed64();
				break;
			}
		}
	}
}
