using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class UseEffectCardS2C : IMessage<UseEffectCardS2C>, IMessage, IEquatable<UseEffectCardS2C>, IDeepCloneable<UseEffectCardS2C>, IBufferMessage
{
	private static readonly MessageParser<UseEffectCardS2C> _parser = new MessageParser<UseEffectCardS2C>(() => new UseEffectCardS2C());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int CardIdFieldNumber = 2;

	private int cardId_;

	public const int TargetIdsFieldNumber = 3;

	private static readonly FieldCodec<long> _repeated_targetIds_codec = FieldCodec.ForSFixed64(26u);

	private readonly RepeatedField<long> targetIds_ = new RepeatedField<long>();

	public const int TargetNodeIdsFieldNumber = 4;

	private static readonly FieldCodec<int> _repeated_targetNodeIds_codec = FieldCodec.ForSFixed32(34u);

	private readonly RepeatedField<int> targetNodeIds_ = new RepeatedField<int>();

	public const int UseSkillFieldNumber = 8;

	private bool useSkill_;

	public const int SkillIdFieldNumber = 9;

	private int skillId_;

	public const int SkillCdsFieldNumber = 10;

	private static readonly MapField<int, int>.Codec _map_skillCds_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<int, int> skillCds_ = new MapField<int, int>();

	public const int EnterReverseFieldNumber = 11;

	private bool enterReverse_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<UseEffectCardS2C> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[324];

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
	public int CardId
	{
		get
		{
			return cardId_;
		}
		set
		{
			cardId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<long> TargetIds => targetIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> TargetNodeIds => targetNodeIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool UseSkill
	{
		get
		{
			return useSkill_;
		}
		set
		{
			useSkill_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkillId
	{
		get
		{
			return skillId_;
		}
		set
		{
			skillId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> SkillCds => skillCds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool EnterReverse
	{
		get
		{
			return enterReverse_;
		}
		set
		{
			enterReverse_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseEffectCardS2C()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseEffectCardS2C(UseEffectCardS2C other)
		: this()
	{
		playerId_ = other.playerId_;
		cardId_ = other.cardId_;
		targetIds_ = other.targetIds_.Clone();
		targetNodeIds_ = other.targetNodeIds_.Clone();
		useSkill_ = other.useSkill_;
		skillId_ = other.skillId_;
		skillCds_ = other.skillCds_.Clone();
		enterReverse_ = other.enterReverse_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseEffectCardS2C Clone()
	{
		return new UseEffectCardS2C(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UseEffectCardS2C);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(UseEffectCardS2C other)
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
		if (CardId != other.CardId)
		{
			return false;
		}
		if (!targetIds_.Equals(other.targetIds_))
		{
			return false;
		}
		if (!targetNodeIds_.Equals(other.targetNodeIds_))
		{
			return false;
		}
		if (UseSkill != other.UseSkill)
		{
			return false;
		}
		if (SkillId != other.SkillId)
		{
			return false;
		}
		if (!SkillCds.Equals(other.SkillCds))
		{
			return false;
		}
		if (EnterReverse != other.EnterReverse)
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
		if (CardId != 0)
		{
			num ^= CardId.GetHashCode();
		}
		num ^= targetIds_.GetHashCode();
		num ^= targetNodeIds_.GetHashCode();
		if (UseSkill)
		{
			num ^= UseSkill.GetHashCode();
		}
		if (SkillId != 0)
		{
			num ^= SkillId.GetHashCode();
		}
		num ^= SkillCds.GetHashCode();
		if (EnterReverse)
		{
			num ^= EnterReverse.GetHashCode();
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
		if (CardId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(CardId);
		}
		targetIds_.WriteTo(ref output, _repeated_targetIds_codec);
		targetNodeIds_.WriteTo(ref output, _repeated_targetNodeIds_codec);
		if (UseSkill)
		{
			output.WriteRawTag(64);
			output.WriteBool(UseSkill);
		}
		if (SkillId != 0)
		{
			output.WriteRawTag(77);
			output.WriteSFixed32(SkillId);
		}
		skillCds_.WriteTo(ref output, _map_skillCds_codec);
		if (EnterReverse)
		{
			output.WriteRawTag(88);
			output.WriteBool(EnterReverse);
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
		if (CardId != 0)
		{
			num += 5;
		}
		num += targetIds_.CalculateSize(_repeated_targetIds_codec);
		num += targetNodeIds_.CalculateSize(_repeated_targetNodeIds_codec);
		if (UseSkill)
		{
			num += 2;
		}
		if (SkillId != 0)
		{
			num += 5;
		}
		num += skillCds_.CalculateSize(_map_skillCds_codec);
		if (EnterReverse)
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
	public void MergeFrom(UseEffectCardS2C other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.CardId != 0)
			{
				CardId = other.CardId;
			}
			targetIds_.Add(other.targetIds_);
			targetNodeIds_.Add(other.targetNodeIds_);
			if (other.UseSkill)
			{
				UseSkill = other.UseSkill;
			}
			if (other.SkillId != 0)
			{
				SkillId = other.SkillId;
			}
			skillCds_.MergeFrom(other.skillCds_);
			if (other.EnterReverse)
			{
				EnterReverse = other.EnterReverse;
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
				CardId = input.ReadSFixed32();
				break;
			case 25u:
			case 26u:
				targetIds_.AddEntriesFrom(ref input, _repeated_targetIds_codec);
				break;
			case 34u:
			case 37u:
				targetNodeIds_.AddEntriesFrom(ref input, _repeated_targetNodeIds_codec);
				break;
			case 64u:
				UseSkill = input.ReadBool();
				break;
			case 77u:
				SkillId = input.ReadSFixed32();
				break;
			case 82u:
				skillCds_.AddEntriesFrom(ref input, _map_skillCds_codec);
				break;
			case 88u:
				EnterReverse = input.ReadBool();
				break;
			}
		}
	}
}
