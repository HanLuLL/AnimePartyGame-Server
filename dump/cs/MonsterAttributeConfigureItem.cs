using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class MonsterAttributeConfigureItem : IMessage<MonsterAttributeConfigureItem>, IMessage, IEquatable<MonsterAttributeConfigureItem>, IDeepCloneable<MonsterAttributeConfigureItem>, IBufferMessage
{
	private static readonly MessageParser<MonsterAttributeConfigureItem> _parser = new MessageParser<MonsterAttributeConfigureItem>(() => new MonsterAttributeConfigureItem());

	private UnknownFieldSet _unknownFields;

	public const int IndexFieldNumber = 1;

	private int index_;

	public const int BloodFieldNumber = 2;

	private int blood_;

	public const int AttackFieldNumber = 3;

	private int attack_;

	public const int DefenseFieldNumber = 4;

	private int defense_;

	public const int PveActiveSkillFieldNumber = 5;

	private int pveActiveSkill_;

	public const int PveActiveSkillExtraFieldNumber = 6;

	private static readonly FieldCodec<int> _repeated_pveActiveSkillExtra_codec = FieldCodec.ForSFixed32(50u);

	private readonly RepeatedField<int> pveActiveSkillExtra_ = new RepeatedField<int>();

	public const int PvePassiveSkillsFieldNumber = 7;

	private static readonly FieldCodec<int> _repeated_pvePassiveSkills_codec = FieldCodec.ForSFixed32(58u);

	private readonly RepeatedField<int> pvePassiveSkills_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MonsterAttributeConfigureItem> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MonsterReflection.Descriptor.MessageTypes[2];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Index
	{
		get
		{
			return index_;
		}
		private set
		{
			index_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Blood
	{
		get
		{
			return blood_;
		}
		private set
		{
			blood_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Attack
	{
		get
		{
			return attack_;
		}
		private set
		{
			attack_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Defense
	{
		get
		{
			return defense_;
		}
		private set
		{
			defense_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PveActiveSkill
	{
		get
		{
			return pveActiveSkill_;
		}
		private set
		{
			pveActiveSkill_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PveActiveSkillExtra => pveActiveSkillExtra_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> PvePassiveSkills => pvePassiveSkills_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterAttributeConfigureItem()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterAttributeConfigureItem(MonsterAttributeConfigureItem other)
		: this()
	{
		index_ = other.index_;
		blood_ = other.blood_;
		attack_ = other.attack_;
		defense_ = other.defense_;
		pveActiveSkill_ = other.pveActiveSkill_;
		pveActiveSkillExtra_ = other.pveActiveSkillExtra_.Clone();
		pvePassiveSkills_ = other.pvePassiveSkills_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterAttributeConfigureItem Clone()
	{
		return new MonsterAttributeConfigureItem(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MonsterAttributeConfigureItem);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MonsterAttributeConfigureItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Index != other.Index)
		{
			return false;
		}
		if (Blood != other.Blood)
		{
			return false;
		}
		if (Attack != other.Attack)
		{
			return false;
		}
		if (Defense != other.Defense)
		{
			return false;
		}
		if (PveActiveSkill != other.PveActiveSkill)
		{
			return false;
		}
		if (!pveActiveSkillExtra_.Equals(other.pveActiveSkillExtra_))
		{
			return false;
		}
		if (!pvePassiveSkills_.Equals(other.pvePassiveSkills_))
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
		if (Index != 0)
		{
			num ^= Index.GetHashCode();
		}
		if (Blood != 0)
		{
			num ^= Blood.GetHashCode();
		}
		if (Attack != 0)
		{
			num ^= Attack.GetHashCode();
		}
		if (Defense != 0)
		{
			num ^= Defense.GetHashCode();
		}
		if (PveActiveSkill != 0)
		{
			num ^= PveActiveSkill.GetHashCode();
		}
		num ^= pveActiveSkillExtra_.GetHashCode();
		num ^= pvePassiveSkills_.GetHashCode();
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
		if (Index != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Index);
		}
		if (Blood != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Blood);
		}
		if (Attack != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(Attack);
		}
		if (Defense != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(Defense);
		}
		if (PveActiveSkill != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(PveActiveSkill);
		}
		pveActiveSkillExtra_.WriteTo(ref output, _repeated_pveActiveSkillExtra_codec);
		pvePassiveSkills_.WriteTo(ref output, _repeated_pvePassiveSkills_codec);
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
		if (Index != 0)
		{
			num += 5;
		}
		if (Blood != 0)
		{
			num += 5;
		}
		if (Attack != 0)
		{
			num += 5;
		}
		if (Defense != 0)
		{
			num += 5;
		}
		if (PveActiveSkill != 0)
		{
			num += 5;
		}
		num += pveActiveSkillExtra_.CalculateSize(_repeated_pveActiveSkillExtra_codec);
		num += pvePassiveSkills_.CalculateSize(_repeated_pvePassiveSkills_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MonsterAttributeConfigureItem other)
	{
		if (other != null)
		{
			if (other.Index != 0)
			{
				Index = other.Index;
			}
			if (other.Blood != 0)
			{
				Blood = other.Blood;
			}
			if (other.Attack != 0)
			{
				Attack = other.Attack;
			}
			if (other.Defense != 0)
			{
				Defense = other.Defense;
			}
			if (other.PveActiveSkill != 0)
			{
				PveActiveSkill = other.PveActiveSkill;
			}
			pveActiveSkillExtra_.Add(other.pveActiveSkillExtra_);
			pvePassiveSkills_.Add(other.pvePassiveSkills_);
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
				Index = input.ReadSFixed32();
				break;
			case 21u:
				Blood = input.ReadSFixed32();
				break;
			case 29u:
				Attack = input.ReadSFixed32();
				break;
			case 37u:
				Defense = input.ReadSFixed32();
				break;
			case 45u:
				PveActiveSkill = input.ReadSFixed32();
				break;
			case 50u:
			case 53u:
				pveActiveSkillExtra_.AddEntriesFrom(ref input, _repeated_pveActiveSkillExtra_codec);
				break;
			case 58u:
			case 61u:
				pvePassiveSkills_.AddEntriesFrom(ref input, _repeated_pvePassiveSkills_codec);
				break;
			}
		}
	}
}
