using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class HeroHpChangeS2C : IMessage<HeroHpChangeS2C>, IMessage, IEquatable<HeroHpChangeS2C>, IDeepCloneable<HeroHpChangeS2C>, IBufferMessage
{
	private static readonly MessageParser<HeroHpChangeS2C> _parser = new MessageParser<HeroHpChangeS2C>(() => new HeroHpChangeS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int ChangeHpFieldNumber = 2;

	private int changeHp_;

	public const int OriHpFieldNumber = 3;

	private int oriHp_;

	public const int CurrHpFieldNumber = 4;

	private int currHp_;

	public const int RealChangeHpFieldNumber = 5;

	private int realChangeHp_;

	public const int RealHpFieldNumber = 6;

	private int realHp_;

	public const int MaxHpFieldNumber = 7;

	private int maxHp_;

	public const int DamageTypeFieldNumber = 8;

	private int damageType_;

	public const int KillerFieldNumber = 9;

	private long killer_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroHpChangeS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[405];

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
	public int ChangeHp
	{
		get
		{
			return changeHp_;
		}
		set
		{
			changeHp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int OriHp
	{
		get
		{
			return oriHp_;
		}
		set
		{
			oriHp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CurrHp
	{
		get
		{
			return currHp_;
		}
		set
		{
			currHp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RealChangeHp
	{
		get
		{
			return realChangeHp_;
		}
		set
		{
			realChangeHp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RealHp
	{
		get
		{
			return realHp_;
		}
		set
		{
			realHp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int MaxHp
	{
		get
		{
			return maxHp_;
		}
		set
		{
			maxHp_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DamageType
	{
		get
		{
			return damageType_;
		}
		set
		{
			damageType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public long Killer
	{
		get
		{
			return killer_;
		}
		set
		{
			killer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroHpChangeS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroHpChangeS2C(HeroHpChangeS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		changeHp_ = other.changeHp_;
		oriHp_ = other.oriHp_;
		currHp_ = other.currHp_;
		realChangeHp_ = other.realChangeHp_;
		realHp_ = other.realHp_;
		maxHp_ = other.maxHp_;
		damageType_ = other.damageType_;
		killer_ = other.killer_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroHpChangeS2C Clone()
	{
		return new HeroHpChangeS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroHpChangeS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroHpChangeS2C other)
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
		if (ChangeHp != other.ChangeHp)
		{
			return false;
		}
		if (OriHp != other.OriHp)
		{
			return false;
		}
		if (CurrHp != other.CurrHp)
		{
			return false;
		}
		if (RealChangeHp != other.RealChangeHp)
		{
			return false;
		}
		if (RealHp != other.RealHp)
		{
			return false;
		}
		if (MaxHp != other.MaxHp)
		{
			return false;
		}
		if (DamageType != other.DamageType)
		{
			return false;
		}
		if (Killer != other.Killer)
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
		if (ChangeHp != 0)
		{
			num ^= ChangeHp.GetHashCode();
		}
		if (OriHp != 0)
		{
			num ^= OriHp.GetHashCode();
		}
		if (CurrHp != 0)
		{
			num ^= CurrHp.GetHashCode();
		}
		if (RealChangeHp != 0)
		{
			num ^= RealChangeHp.GetHashCode();
		}
		if (RealHp != 0)
		{
			num ^= RealHp.GetHashCode();
		}
		if (MaxHp != 0)
		{
			num ^= MaxHp.GetHashCode();
		}
		if (DamageType != 0)
		{
			num ^= DamageType.GetHashCode();
		}
		if (Killer != 0L)
		{
			num ^= Killer.GetHashCode();
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
		if (ChangeHp != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(ChangeHp);
		}
		if (OriHp != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(OriHp);
		}
		if (CurrHp != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(CurrHp);
		}
		if (RealChangeHp != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(RealChangeHp);
		}
		if (RealHp != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(RealHp);
		}
		if (MaxHp != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(MaxHp);
		}
		if (DamageType != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(DamageType);
		}
		if (Killer != 0L)
		{
			output.WriteRawTag(73);
			output.WriteSFixed64(Killer);
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
		if (ChangeHp != 0)
		{
			num += 5;
		}
		if (OriHp != 0)
		{
			num += 5;
		}
		if (CurrHp != 0)
		{
			num += 5;
		}
		if (RealChangeHp != 0)
		{
			num += 5;
		}
		if (RealHp != 0)
		{
			num += 5;
		}
		if (MaxHp != 0)
		{
			num += 5;
		}
		if (DamageType != 0)
		{
			num += 5;
		}
		if (Killer != 0L)
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
	public void MergeFrom(HeroHpChangeS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.ChangeHp != 0)
			{
				ChangeHp = other.ChangeHp;
			}
			if (other.OriHp != 0)
			{
				OriHp = other.OriHp;
			}
			if (other.CurrHp != 0)
			{
				CurrHp = other.CurrHp;
			}
			if (other.RealChangeHp != 0)
			{
				RealChangeHp = other.RealChangeHp;
			}
			if (other.RealHp != 0)
			{
				RealHp = other.RealHp;
			}
			if (other.MaxHp != 0)
			{
				MaxHp = other.MaxHp;
			}
			if (other.DamageType != 0)
			{
				DamageType = other.DamageType;
			}
			if (other.Killer != 0L)
			{
				Killer = other.Killer;
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
				ChangeHp = input.ReadSFixed32();
				break;
			case 29u:
				OriHp = input.ReadSFixed32();
				break;
			case 37u:
				CurrHp = input.ReadSFixed32();
				break;
			case 45u:
				RealChangeHp = input.ReadSFixed32();
				break;
			case 53u:
				RealHp = input.ReadSFixed32();
				break;
			case 61u:
				MaxHp = input.ReadSFixed32();
				break;
			case 69u:
				DamageType = input.ReadSFixed32();
				break;
			case 73u:
				Killer = input.ReadSFixed64();
				break;
			}
		}
	}
}
