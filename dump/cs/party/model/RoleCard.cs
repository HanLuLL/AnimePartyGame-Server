using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class RoleCard : IMessage<RoleCard>, IMessage, IEquatable<RoleCard>, IDeepCloneable<RoleCard>, IBufferMessage
{
	private static readonly MessageParser<RoleCard> _parser = new MessageParser<RoleCard>(() => new RoleCard());

	private UnknownFieldSet _unknownFields;

	public const int DefIdFieldNumber = 1;

	private int defId_;

	public const int ExpFieldNumber = 2;

	private int exp_;

	public const int LvFieldNumber = 3;

	private int lv_;

	public const int UseAdornFieldNumber = 4;

	private int useAdorn_;

	public const int IsBreakThroughFieldNumber = 5;

	private bool isBreakThrough_;

	public const int FightCountFieldNumber = 6;

	private int fightCount_;

	public const int PveStrengthenFieldNumber = 7;

	private PveHeroStrengthen pveStrengthen_;

	public const int CollectedFieldNumber = 8;

	private bool collected_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<RoleCard> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[46];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DefId
	{
		get
		{
			return defId_;
		}
		set
		{
			defId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Exp
	{
		get
		{
			return exp_;
		}
		set
		{
			exp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Lv
	{
		get
		{
			return lv_;
		}
		set
		{
			lv_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int UseAdorn
	{
		get
		{
			return useAdorn_;
		}
		set
		{
			useAdorn_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsBreakThrough
	{
		get
		{
			return isBreakThrough_;
		}
		set
		{
			isBreakThrough_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int FightCount
	{
		get
		{
			return fightCount_;
		}
		set
		{
			fightCount_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroStrengthen PveStrengthen
	{
		get
		{
			return pveStrengthen_;
		}
		set
		{
			pveStrengthen_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Collected
	{
		get
		{
			return collected_;
		}
		set
		{
			collected_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCard()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCard(RoleCard other)
		: this()
	{
		defId_ = other.defId_;
		exp_ = other.exp_;
		lv_ = other.lv_;
		useAdorn_ = other.useAdorn_;
		isBreakThrough_ = other.isBreakThrough_;
		fightCount_ = other.fightCount_;
		pveStrengthen_ = ((other.pveStrengthen_ != null) ? other.pveStrengthen_.Clone() : null);
		collected_ = other.collected_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCard Clone()
	{
		return new RoleCard(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RoleCard);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(RoleCard other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DefId != other.DefId)
		{
			return false;
		}
		if (Exp != other.Exp)
		{
			return false;
		}
		if (Lv != other.Lv)
		{
			return false;
		}
		if (UseAdorn != other.UseAdorn)
		{
			return false;
		}
		if (IsBreakThrough != other.IsBreakThrough)
		{
			return false;
		}
		if (FightCount != other.FightCount)
		{
			return false;
		}
		if (!object.Equals(PveStrengthen, other.PveStrengthen))
		{
			return false;
		}
		if (Collected != other.Collected)
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
		if (DefId != 0)
		{
			num ^= DefId.GetHashCode();
		}
		if (Exp != 0)
		{
			num ^= Exp.GetHashCode();
		}
		if (Lv != 0)
		{
			num ^= Lv.GetHashCode();
		}
		if (UseAdorn != 0)
		{
			num ^= UseAdorn.GetHashCode();
		}
		if (IsBreakThrough)
		{
			num ^= IsBreakThrough.GetHashCode();
		}
		if (FightCount != 0)
		{
			num ^= FightCount.GetHashCode();
		}
		if (pveStrengthen_ != null)
		{
			num ^= PveStrengthen.GetHashCode();
		}
		if (Collected)
		{
			num ^= Collected.GetHashCode();
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
		if (DefId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(DefId);
		}
		if (Exp != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Exp);
		}
		if (Lv != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Lv);
		}
		if (UseAdorn != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(UseAdorn);
		}
		if (IsBreakThrough)
		{
			output.WriteRawTag(40);
			output.WriteBool(IsBreakThrough);
		}
		if (FightCount != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(FightCount);
		}
		if (pveStrengthen_ != null)
		{
			output.WriteRawTag(58);
			output.WriteMessage(PveStrengthen);
		}
		if (Collected)
		{
			output.WriteRawTag(64);
			output.WriteBool(Collected);
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
		if (DefId != 0)
		{
			num += 5;
		}
		if (Exp != 0)
		{
			num += 5;
		}
		if (Lv != 0)
		{
			num += 5;
		}
		if (UseAdorn != 0)
		{
			num += 5;
		}
		if (IsBreakThrough)
		{
			num += 2;
		}
		if (FightCount != 0)
		{
			num += 5;
		}
		if (pveStrengthen_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(PveStrengthen);
		}
		if (Collected)
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
	public void MergeFrom(RoleCard other)
	{
		if (other == null)
		{
			return;
		}
		if (other.DefId != 0)
		{
			DefId = other.DefId;
		}
		if (other.Exp != 0)
		{
			Exp = other.Exp;
		}
		if (other.Lv != 0)
		{
			Lv = other.Lv;
		}
		if (other.UseAdorn != 0)
		{
			UseAdorn = other.UseAdorn;
		}
		if (other.IsBreakThrough)
		{
			IsBreakThrough = other.IsBreakThrough;
		}
		if (other.FightCount != 0)
		{
			FightCount = other.FightCount;
		}
		if (other.pveStrengthen_ != null)
		{
			if (pveStrengthen_ == null)
			{
				PveStrengthen = new PveHeroStrengthen();
			}
			PveStrengthen.MergeFrom(other.PveStrengthen);
		}
		if (other.Collected)
		{
			Collected = other.Collected;
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
			case 13u:
				DefId = input.ReadSFixed32();
				break;
			case 21u:
				Exp = input.ReadSFixed32();
				break;
			case 29u:
				Lv = input.ReadSFixed32();
				break;
			case 37u:
				UseAdorn = input.ReadSFixed32();
				break;
			case 40u:
				IsBreakThrough = input.ReadBool();
				break;
			case 53u:
				FightCount = input.ReadSFixed32();
				break;
			case 58u:
				if (pveStrengthen_ == null)
				{
					PveStrengthen = new PveHeroStrengthen();
				}
				input.ReadMessage(PveStrengthen);
				break;
			case 64u:
				Collected = input.ReadBool();
				break;
			}
		}
	}
}
