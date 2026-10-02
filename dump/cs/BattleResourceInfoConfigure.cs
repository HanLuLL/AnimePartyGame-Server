using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class BattleResourceInfoConfigure : IMessage<BattleResourceInfoConfigure>, IMessage, IEquatable<BattleResourceInfoConfigure>, IDeepCloneable<BattleResourceInfoConfigure>, IBufferMessage
{
	private static readonly MessageParser<BattleResourceInfoConfigure> _parser = new MessageParser<BattleResourceInfoConfigure>(() => new BattleResourceInfoConfigure());

	private UnknownFieldSet _unknownFields;

	public const int IdFieldNumber = 1;

	private int id_;

	public const int InfoLayerFieldNumber = 2;

	private int infoLayer_;

	public const int AtkLayerFieldNumber = 3;

	private int atkLayer_;

	public const int DefLayerFieldNumber = 4;

	private int defLayer_;

	public const int HitDirectFieldNumber = 5;

	private string hitDirect_ = "";

	public const int ApproachDirectFieldNumber = 6;

	private string approachDirect_ = "";

	public const int DetachDirectFieldNumber = 7;

	private string detachDirect_ = "";

	public const int ChainAttackDirectFieldNumber = 8;

	private string chainAttackDirect_ = "";

	public const int ActorPositionAtkFieldNumber = 9;

	private int actorPositionAtk_;

	public const int ActorPositionDefFieldNumber = 10;

	private int actorPositionDef_;

	public const int ActorPositionChainFieldNumber = 11;

	private int actorPositionChain_;

	public const int BattleFanfareAttackFieldNumber = 12;

	private string battleFanfareAttack_ = "";

	public const int BattleIdleAttackFieldNumber = 13;

	private string battleIdleAttack_ = "";

	public const int BattleIdleattackEndFieldNumber = 14;

	private string battleIdleattackEnd_ = "";

	public const int BattleChainAttackFieldNumber = 15;

	private string battleChainAttack_ = "";

	public const int BattleIdleDefenseFieldNumber = 16;

	private string battleIdleDefense_ = "";

	public const int MovestartFieldNumber = 17;

	private string movestart_ = "";

	public const int MoveFieldNumber = 18;

	private string move_ = "";

	public const int AttackFieldNumber = 19;

	private static readonly MapField<int, string>.Codec _map_attack_codec = new MapField<int, string>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForString(18u, ""), 154u);

	private readonly MapField<int, string> attack_ = new MapField<int, string>();

	public const int HurtFieldNumber = 20;

	private string hurt_ = "";

	public const int DodgeFieldNumber = 21;

	private string dodge_ = "";

	public const int WinFieldNumber = 22;

	private string win_ = "";

	public const int DeadFieldNumber = 23;

	private string dead_ = "";

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<BattleResourceInfoConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => BattleResourceReflection.Descriptor.MessageTypes[0];

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
	public int InfoLayer
	{
		get
		{
			return infoLayer_;
		}
		private set
		{
			infoLayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int AtkLayer
	{
		get
		{
			return atkLayer_;
		}
		private set
		{
			atkLayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int DefLayer
	{
		get
		{
			return defLayer_;
		}
		private set
		{
			defLayer_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string HitDirect
	{
		get
		{
			return hitDirect_;
		}
		private set
		{
			hitDirect_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ApproachDirect
	{
		get
		{
			return approachDirect_;
		}
		private set
		{
			approachDirect_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string DetachDirect
	{
		get
		{
			return detachDirect_;
		}
		private set
		{
			detachDirect_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string ChainAttackDirect
	{
		get
		{
			return chainAttackDirect_;
		}
		private set
		{
			chainAttackDirect_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActorPositionAtk
	{
		get
		{
			return actorPositionAtk_;
		}
		private set
		{
			actorPositionAtk_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActorPositionDef
	{
		get
		{
			return actorPositionDef_;
		}
		private set
		{
			actorPositionDef_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ActorPositionChain
	{
		get
		{
			return actorPositionChain_;
		}
		private set
		{
			actorPositionChain_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BattleFanfareAttack
	{
		get
		{
			return battleFanfareAttack_;
		}
		private set
		{
			battleFanfareAttack_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BattleIdleAttack
	{
		get
		{
			return battleIdleAttack_;
		}
		private set
		{
			battleIdleAttack_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BattleIdleattackEnd
	{
		get
		{
			return battleIdleattackEnd_;
		}
		private set
		{
			battleIdleattackEnd_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BattleChainAttack
	{
		get
		{
			return battleChainAttack_;
		}
		private set
		{
			battleChainAttack_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string BattleIdleDefense
	{
		get
		{
			return battleIdleDefense_;
		}
		private set
		{
			battleIdleDefense_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Movestart
	{
		get
		{
			return movestart_;
		}
		private set
		{
			movestart_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Move
	{
		get
		{
			return move_;
		}
		private set
		{
			move_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, string> Attack => attack_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Hurt
	{
		get
		{
			return hurt_;
		}
		private set
		{
			hurt_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Dodge
	{
		get
		{
			return dodge_;
		}
		private set
		{
			dodge_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Win
	{
		get
		{
			return win_;
		}
		private set
		{
			win_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public string Dead
	{
		get
		{
			return dead_;
		}
		private set
		{
			dead_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleResourceInfoConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleResourceInfoConfigure(BattleResourceInfoConfigure other)
		: this()
	{
		id_ = other.id_;
		infoLayer_ = other.infoLayer_;
		atkLayer_ = other.atkLayer_;
		defLayer_ = other.defLayer_;
		hitDirect_ = other.hitDirect_;
		approachDirect_ = other.approachDirect_;
		detachDirect_ = other.detachDirect_;
		chainAttackDirect_ = other.chainAttackDirect_;
		actorPositionAtk_ = other.actorPositionAtk_;
		actorPositionDef_ = other.actorPositionDef_;
		actorPositionChain_ = other.actorPositionChain_;
		battleFanfareAttack_ = other.battleFanfareAttack_;
		battleIdleAttack_ = other.battleIdleAttack_;
		battleIdleattackEnd_ = other.battleIdleattackEnd_;
		battleChainAttack_ = other.battleChainAttack_;
		battleIdleDefense_ = other.battleIdleDefense_;
		movestart_ = other.movestart_;
		move_ = other.move_;
		attack_ = other.attack_.Clone();
		hurt_ = other.hurt_;
		dodge_ = other.dodge_;
		win_ = other.win_;
		dead_ = other.dead_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleResourceInfoConfigure Clone()
	{
		return new BattleResourceInfoConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as BattleResourceInfoConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(BattleResourceInfoConfigure other)
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
		if (InfoLayer != other.InfoLayer)
		{
			return false;
		}
		if (AtkLayer != other.AtkLayer)
		{
			return false;
		}
		if (DefLayer != other.DefLayer)
		{
			return false;
		}
		if (HitDirect != other.HitDirect)
		{
			return false;
		}
		if (ApproachDirect != other.ApproachDirect)
		{
			return false;
		}
		if (DetachDirect != other.DetachDirect)
		{
			return false;
		}
		if (ChainAttackDirect != other.ChainAttackDirect)
		{
			return false;
		}
		if (ActorPositionAtk != other.ActorPositionAtk)
		{
			return false;
		}
		if (ActorPositionDef != other.ActorPositionDef)
		{
			return false;
		}
		if (ActorPositionChain != other.ActorPositionChain)
		{
			return false;
		}
		if (BattleFanfareAttack != other.BattleFanfareAttack)
		{
			return false;
		}
		if (BattleIdleAttack != other.BattleIdleAttack)
		{
			return false;
		}
		if (BattleIdleattackEnd != other.BattleIdleattackEnd)
		{
			return false;
		}
		if (BattleChainAttack != other.BattleChainAttack)
		{
			return false;
		}
		if (BattleIdleDefense != other.BattleIdleDefense)
		{
			return false;
		}
		if (Movestart != other.Movestart)
		{
			return false;
		}
		if (Move != other.Move)
		{
			return false;
		}
		if (!Attack.Equals(other.Attack))
		{
			return false;
		}
		if (Hurt != other.Hurt)
		{
			return false;
		}
		if (Dodge != other.Dodge)
		{
			return false;
		}
		if (Win != other.Win)
		{
			return false;
		}
		if (Dead != other.Dead)
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
		if (InfoLayer != 0)
		{
			num ^= InfoLayer.GetHashCode();
		}
		if (AtkLayer != 0)
		{
			num ^= AtkLayer.GetHashCode();
		}
		if (DefLayer != 0)
		{
			num ^= DefLayer.GetHashCode();
		}
		if (HitDirect.Length != 0)
		{
			num ^= HitDirect.GetHashCode();
		}
		if (ApproachDirect.Length != 0)
		{
			num ^= ApproachDirect.GetHashCode();
		}
		if (DetachDirect.Length != 0)
		{
			num ^= DetachDirect.GetHashCode();
		}
		if (ChainAttackDirect.Length != 0)
		{
			num ^= ChainAttackDirect.GetHashCode();
		}
		if (ActorPositionAtk != 0)
		{
			num ^= ActorPositionAtk.GetHashCode();
		}
		if (ActorPositionDef != 0)
		{
			num ^= ActorPositionDef.GetHashCode();
		}
		if (ActorPositionChain != 0)
		{
			num ^= ActorPositionChain.GetHashCode();
		}
		if (BattleFanfareAttack.Length != 0)
		{
			num ^= BattleFanfareAttack.GetHashCode();
		}
		if (BattleIdleAttack.Length != 0)
		{
			num ^= BattleIdleAttack.GetHashCode();
		}
		if (BattleIdleattackEnd.Length != 0)
		{
			num ^= BattleIdleattackEnd.GetHashCode();
		}
		if (BattleChainAttack.Length != 0)
		{
			num ^= BattleChainAttack.GetHashCode();
		}
		if (BattleIdleDefense.Length != 0)
		{
			num ^= BattleIdleDefense.GetHashCode();
		}
		if (Movestart.Length != 0)
		{
			num ^= Movestart.GetHashCode();
		}
		if (Move.Length != 0)
		{
			num ^= Move.GetHashCode();
		}
		num ^= Attack.GetHashCode();
		if (Hurt.Length != 0)
		{
			num ^= Hurt.GetHashCode();
		}
		if (Dodge.Length != 0)
		{
			num ^= Dodge.GetHashCode();
		}
		if (Win.Length != 0)
		{
			num ^= Win.GetHashCode();
		}
		if (Dead.Length != 0)
		{
			num ^= Dead.GetHashCode();
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
		if (Id != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(Id);
		}
		if (InfoLayer != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(InfoLayer);
		}
		if (AtkLayer != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(AtkLayer);
		}
		if (DefLayer != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(DefLayer);
		}
		if (HitDirect.Length != 0)
		{
			output.WriteRawTag(42);
			output.WriteString(HitDirect);
		}
		if (ApproachDirect.Length != 0)
		{
			output.WriteRawTag(50);
			output.WriteString(ApproachDirect);
		}
		if (DetachDirect.Length != 0)
		{
			output.WriteRawTag(58);
			output.WriteString(DetachDirect);
		}
		if (ChainAttackDirect.Length != 0)
		{
			output.WriteRawTag(66);
			output.WriteString(ChainAttackDirect);
		}
		if (ActorPositionAtk != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(ActorPositionAtk);
		}
		if (ActorPositionDef != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(ActorPositionDef);
		}
		if (ActorPositionChain != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(ActorPositionChain);
		}
		if (BattleFanfareAttack.Length != 0)
		{
			output.WriteRawTag(98);
			output.WriteString(BattleFanfareAttack);
		}
		if (BattleIdleAttack.Length != 0)
		{
			output.WriteRawTag(106);
			output.WriteString(BattleIdleAttack);
		}
		if (BattleIdleattackEnd.Length != 0)
		{
			output.WriteRawTag(114);
			output.WriteString(BattleIdleattackEnd);
		}
		if (BattleChainAttack.Length != 0)
		{
			output.WriteRawTag(122);
			output.WriteString(BattleChainAttack);
		}
		if (BattleIdleDefense.Length != 0)
		{
			output.WriteRawTag(130, 1);
			output.WriteString(BattleIdleDefense);
		}
		if (Movestart.Length != 0)
		{
			output.WriteRawTag(138, 1);
			output.WriteString(Movestart);
		}
		if (Move.Length != 0)
		{
			output.WriteRawTag(146, 1);
			output.WriteString(Move);
		}
		attack_.WriteTo(ref output, _map_attack_codec);
		if (Hurt.Length != 0)
		{
			output.WriteRawTag(162, 1);
			output.WriteString(Hurt);
		}
		if (Dodge.Length != 0)
		{
			output.WriteRawTag(170, 1);
			output.WriteString(Dodge);
		}
		if (Win.Length != 0)
		{
			output.WriteRawTag(178, 1);
			output.WriteString(Win);
		}
		if (Dead.Length != 0)
		{
			output.WriteRawTag(186, 1);
			output.WriteString(Dead);
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
		if (Id != 0)
		{
			num += 5;
		}
		if (InfoLayer != 0)
		{
			num += 5;
		}
		if (AtkLayer != 0)
		{
			num += 5;
		}
		if (DefLayer != 0)
		{
			num += 5;
		}
		if (HitDirect.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(HitDirect);
		}
		if (ApproachDirect.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ApproachDirect);
		}
		if (DetachDirect.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DetachDirect);
		}
		if (ChainAttackDirect.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ChainAttackDirect);
		}
		if (ActorPositionAtk != 0)
		{
			num += 5;
		}
		if (ActorPositionDef != 0)
		{
			num += 5;
		}
		if (ActorPositionChain != 0)
		{
			num += 5;
		}
		if (BattleFanfareAttack.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BattleFanfareAttack);
		}
		if (BattleIdleAttack.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BattleIdleAttack);
		}
		if (BattleIdleattackEnd.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BattleIdleattackEnd);
		}
		if (BattleChainAttack.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BattleChainAttack);
		}
		if (BattleIdleDefense.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(BattleIdleDefense);
		}
		if (Movestart.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Movestart);
		}
		if (Move.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Move);
		}
		num += attack_.CalculateSize(_map_attack_codec);
		if (Hurt.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Hurt);
		}
		if (Dodge.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Dodge);
		}
		if (Win.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Win);
		}
		if (Dead.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Dead);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(BattleResourceInfoConfigure other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.InfoLayer != 0)
			{
				InfoLayer = other.InfoLayer;
			}
			if (other.AtkLayer != 0)
			{
				AtkLayer = other.AtkLayer;
			}
			if (other.DefLayer != 0)
			{
				DefLayer = other.DefLayer;
			}
			if (other.HitDirect.Length != 0)
			{
				HitDirect = other.HitDirect;
			}
			if (other.ApproachDirect.Length != 0)
			{
				ApproachDirect = other.ApproachDirect;
			}
			if (other.DetachDirect.Length != 0)
			{
				DetachDirect = other.DetachDirect;
			}
			if (other.ChainAttackDirect.Length != 0)
			{
				ChainAttackDirect = other.ChainAttackDirect;
			}
			if (other.ActorPositionAtk != 0)
			{
				ActorPositionAtk = other.ActorPositionAtk;
			}
			if (other.ActorPositionDef != 0)
			{
				ActorPositionDef = other.ActorPositionDef;
			}
			if (other.ActorPositionChain != 0)
			{
				ActorPositionChain = other.ActorPositionChain;
			}
			if (other.BattleFanfareAttack.Length != 0)
			{
				BattleFanfareAttack = other.BattleFanfareAttack;
			}
			if (other.BattleIdleAttack.Length != 0)
			{
				BattleIdleAttack = other.BattleIdleAttack;
			}
			if (other.BattleIdleattackEnd.Length != 0)
			{
				BattleIdleattackEnd = other.BattleIdleattackEnd;
			}
			if (other.BattleChainAttack.Length != 0)
			{
				BattleChainAttack = other.BattleChainAttack;
			}
			if (other.BattleIdleDefense.Length != 0)
			{
				BattleIdleDefense = other.BattleIdleDefense;
			}
			if (other.Movestart.Length != 0)
			{
				Movestart = other.Movestart;
			}
			if (other.Move.Length != 0)
			{
				Move = other.Move;
			}
			attack_.MergeFrom(other.attack_);
			if (other.Hurt.Length != 0)
			{
				Hurt = other.Hurt;
			}
			if (other.Dodge.Length != 0)
			{
				Dodge = other.Dodge;
			}
			if (other.Win.Length != 0)
			{
				Win = other.Win;
			}
			if (other.Dead.Length != 0)
			{
				Dead = other.Dead;
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
				Id = input.ReadSFixed32();
				break;
			case 21u:
				InfoLayer = input.ReadSFixed32();
				break;
			case 29u:
				AtkLayer = input.ReadSFixed32();
				break;
			case 37u:
				DefLayer = input.ReadSFixed32();
				break;
			case 42u:
				HitDirect = input.ReadString();
				break;
			case 50u:
				ApproachDirect = input.ReadString();
				break;
			case 58u:
				DetachDirect = input.ReadString();
				break;
			case 66u:
				ChainAttackDirect = input.ReadString();
				break;
			case 77u:
				ActorPositionAtk = input.ReadSFixed32();
				break;
			case 85u:
				ActorPositionDef = input.ReadSFixed32();
				break;
			case 93u:
				ActorPositionChain = input.ReadSFixed32();
				break;
			case 98u:
				BattleFanfareAttack = input.ReadString();
				break;
			case 106u:
				BattleIdleAttack = input.ReadString();
				break;
			case 114u:
				BattleIdleattackEnd = input.ReadString();
				break;
			case 122u:
				BattleChainAttack = input.ReadString();
				break;
			case 130u:
				BattleIdleDefense = input.ReadString();
				break;
			case 138u:
				Movestart = input.ReadString();
				break;
			case 146u:
				Move = input.ReadString();
				break;
			case 154u:
				attack_.AddEntriesFrom(ref input, _map_attack_codec);
				break;
			case 162u:
				Hurt = input.ReadString();
				break;
			case 170u:
				Dodge = input.ReadString();
				break;
			case 178u:
				Win = input.ReadString();
				break;
			case 186u:
				Dead = input.ReadString();
				break;
			}
		}
	}
}
