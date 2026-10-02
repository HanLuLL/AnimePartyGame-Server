using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class SinglePlayerMonsterConfigure : IMessage<SinglePlayerMonsterConfigure>, IMessage, IEquatable<SinglePlayerMonsterConfigure>, IDeepCloneable<SinglePlayerMonsterConfigure>, IBufferMessage
{
	private static readonly MessageParser<SinglePlayerMonsterConfigure> _parser = new MessageParser<SinglePlayerMonsterConfigure>(() => new SinglePlayerMonsterConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int NameIDFieldNumber = 2;

	private int nameID_;

	public const int DesiIDFieldNumber = 3;

	private int desiID_;

	public const int IconFieldNumber = 4;

	private string icon_ = "";

	public const int ModelFieldNumber = 5;

	private string model_ = "";

	public const int MonsterRadiusFieldNumber = 6;

	private float monsterRadius_;

	public const int CanAttackFieldNumber = 7;

	private bool canAttack_;

	public const int MonsterAttackTimelineFieldNumber = 8;

	private string monsterAttackTimeline_ = "";

	public const int MaxHpFieldNumber = 9;

	private int maxHp_;

	public const int AttackFieldNumber = 10;

	private int attack_;

	public const int DefenseFieldNumber = 11;

	private int defense_;

	public const int TriggerPointFieldNumber = 12;

	private static readonly FieldCodec<int> _repeated_triggerPoint_codec = FieldCodec.ForSFixed32(98u);

	private readonly RepeatedField<int> triggerPoint_ = new RepeatedField<int>();

	public const int TriggerParamFieldNumber = 13;

	private static readonly FieldCodec<int> _repeated_triggerParam_codec = FieldCodec.ForSFixed32(106u);

	private readonly RepeatedField<int> triggerParam_ = new RepeatedField<int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SinglePlayerMonsterConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SinglePlayerReflection.Descriptor.MessageTypes[6];

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
	public int NameID
	{
		get
		{
			return nameID_;
		}
		private set
		{
			nameID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DesiID
	{
		get
		{
			return desiID_;
		}
		private set
		{
			desiID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Icon
	{
		get
		{
			return icon_;
		}
		private set
		{
			icon_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Model
	{
		get
		{
			return model_;
		}
		private set
		{
			model_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public float MonsterRadius
	{
		get
		{
			return monsterRadius_;
		}
		private set
		{
			monsterRadius_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool CanAttack
	{
		get
		{
			return canAttack_;
		}
		private set
		{
			canAttack_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string MonsterAttackTimeline
	{
		get
		{
			return monsterAttackTimeline_;
		}
		private set
		{
			monsterAttackTimeline_ = ProtoPreconditions.CheckNotNull(value, "value");
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
		private set
		{
			maxHp_ = value;
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
	public RepeatedField<int> TriggerPoint => triggerPoint_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TriggerParam => triggerParam_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerMonsterConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerMonsterConfigure(SinglePlayerMonsterConfigure other)
		: this()
	{
		id_ = other.id_;
		nameID_ = other.nameID_;
		desiID_ = other.desiID_;
		icon_ = other.icon_;
		model_ = other.model_;
		monsterRadius_ = other.monsterRadius_;
		canAttack_ = other.canAttack_;
		monsterAttackTimeline_ = other.monsterAttackTimeline_;
		maxHp_ = other.maxHp_;
		attack_ = other.attack_;
		defense_ = other.defense_;
		triggerPoint_ = other.triggerPoint_.Clone();
		triggerParam_ = other.triggerParam_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SinglePlayerMonsterConfigure Clone()
	{
		return new SinglePlayerMonsterConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SinglePlayerMonsterConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SinglePlayerMonsterConfigure other)
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
		if (NameID != other.NameID)
		{
			return false;
		}
		if (DesiID != other.DesiID)
		{
			return false;
		}
		if (Icon != other.Icon)
		{
			return false;
		}
		if (Model != other.Model)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(MonsterRadius, other.MonsterRadius))
		{
			return false;
		}
		if (CanAttack != other.CanAttack)
		{
			return false;
		}
		if (MonsterAttackTimeline != other.MonsterAttackTimeline)
		{
			return false;
		}
		if (MaxHp != other.MaxHp)
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
		if (!triggerPoint_.Equals(other.triggerPoint_))
		{
			return false;
		}
		if (!triggerParam_.Equals(other.triggerParam_))
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
		if (NameID != 0)
		{
			num ^= NameID.GetHashCode();
		}
		if (DesiID != 0)
		{
			num ^= DesiID.GetHashCode();
		}
		if (Icon.Length != 0)
		{
			num ^= Icon.GetHashCode();
		}
		if (Model.Length != 0)
		{
			num ^= Model.GetHashCode();
		}
		if (MonsterRadius != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(MonsterRadius);
		}
		if (CanAttack)
		{
			num ^= CanAttack.GetHashCode();
		}
		if (MonsterAttackTimeline.Length != 0)
		{
			num ^= MonsterAttackTimeline.GetHashCode();
		}
		if (MaxHp != 0)
		{
			num ^= MaxHp.GetHashCode();
		}
		if (Attack != 0)
		{
			num ^= Attack.GetHashCode();
		}
		if (Defense != 0)
		{
			num ^= Defense.GetHashCode();
		}
		num ^= triggerPoint_.GetHashCode();
		num ^= triggerParam_.GetHashCode();
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
		if (NameID != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(NameID);
		}
		if (DesiID != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(DesiID);
		}
		if (Icon.Length != 0)
		{
			output.WriteRawTag(34);
			output.WriteString(Icon);
		}
		if (Model.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(Model);
		}
		if (MonsterRadius != 0f)
		{
			output.WriteRawTag(53);
			output.WriteFloat(MonsterRadius);
		}
		if (CanAttack)
		{
			output.WriteRawTag(56);
			output.WriteBool(CanAttack);
		}
		if (MonsterAttackTimeline.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(MonsterAttackTimeline);
		}
		if (MaxHp != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(MaxHp);
		}
		if (Attack != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(Attack);
		}
		if (Defense != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(Defense);
		}
		triggerPoint_.WriteTo(ref output, _repeated_triggerPoint_codec);
		triggerParam_.WriteTo(ref output, _repeated_triggerParam_codec);
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
		if (NameID != 0)
		{
			num += 5;
		}
		if (DesiID != 0)
		{
			num += 5;
		}
		if (Icon.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Icon);
		}
		if (Model.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Model);
		}
		if (MonsterRadius != 0f)
		{
			num += 5;
		}
		if (CanAttack)
		{
			num += 2;
		}
		if (MonsterAttackTimeline.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(MonsterAttackTimeline);
		}
		if (MaxHp != 0)
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
		num += triggerPoint_.CalculateSize(_repeated_triggerPoint_codec);
		num += triggerParam_.CalculateSize(_repeated_triggerParam_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SinglePlayerMonsterConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.NameID != 0)
			{
				NameID = other.NameID;
			}
			if (other.DesiID != 0)
			{
				DesiID = other.DesiID;
			}
			if (other.Icon.Length != 0)
			{
				Icon = other.Icon;
			}
			if (other.Model.Length != 0)
			{
				Model = other.Model;
			}
			if (other.MonsterRadius != 0f)
			{
				MonsterRadius = other.MonsterRadius;
			}
			if (other.CanAttack)
			{
				CanAttack = other.CanAttack;
			}
			if (other.MonsterAttackTimeline.Length != 0)
			{
				MonsterAttackTimeline = other.MonsterAttackTimeline;
			}
			if (other.MaxHp != 0)
			{
				MaxHp = other.MaxHp;
			}
			if (other.Attack != 0)
			{
				Attack = other.Attack;
			}
			if (other.Defense != 0)
			{
				Defense = other.Defense;
			}
			triggerPoint_.Add(other.triggerPoint_);
			triggerParam_.Add(other.triggerParam_);
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
				NameID = input.ReadSFixed32();
				break;
			case 29u:
				DesiID = input.ReadSFixed32();
				break;
			case 34u:
				Icon = input.ReadString();
				break;
			case 42u:
				Model = input.ReadString();
				break;
			case 53u:
				MonsterRadius = input.ReadFloat();
				break;
			case 56u:
				CanAttack = input.ReadBool();
				break;
			case 66u:
				MonsterAttackTimeline = input.ReadString();
				break;
			case 77u:
				MaxHp = input.ReadSFixed32();
				break;
			case 85u:
				Attack = input.ReadSFixed32();
				break;
			case 93u:
				Defense = input.ReadSFixed32();
				break;
			case 98u:
			case 101u:
				triggerPoint_.AddEntriesFrom(ref input, _repeated_triggerPoint_codec);
				break;
			case 106u:
			case 109u:
				triggerParam_.AddEntriesFrom(ref input, _repeated_triggerParam_codec);
				break;
			}
		}
	}
}
