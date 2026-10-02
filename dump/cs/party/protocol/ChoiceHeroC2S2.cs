using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class ChoiceHeroC2S2 : IMessage<ChoiceHeroC2S2>, IMessage, IEquatable<ChoiceHeroC2S2>, IDeepCloneable<ChoiceHeroC2S2>, IBufferMessage
{
	private static readonly MessageParser<ChoiceHeroC2S2> _parser = new MessageParser<ChoiceHeroC2S2>(() => new ChoiceHeroC2S2());

	private UnknownFieldSet _unknownFields;

	public const int HeroIdFieldNumber = 1;

	private int heroId_;

	public const int HasHeroFieldNumber = 2;

	private bool hasHero_;

	public const int UseAdornFieldNumber = 3;

	private int useAdorn_;

	public const int PveLevelFieldNumber = 4;

	private int pveLevel_;

	public const int SkinPendantFieldNumber = 5;

	private int skinPendant_;

	public const int PveTalentIdFieldNumber = 6;

	private int pveTalentId_;

	public const int SportGameDataFieldNumber = 7;

	private static readonly MapField<int, int>.Codec _map_sportGameData_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 58u);

	private readonly MapField<int, int> sportGameData_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<ChoiceHeroC2S2> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[176];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int HeroId
	{
		get
		{
			return heroId_;
		}
		set
		{
			heroId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool HasHero
	{
		get
		{
			return hasHero_;
		}
		set
		{
			hasHero_ = value;
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
	public int PveLevel
	{
		get
		{
			return pveLevel_;
		}
		set
		{
			pveLevel_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int SkinPendant
	{
		get
		{
			return skinPendant_;
		}
		set
		{
			skinPendant_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int PveTalentId
	{
		get
		{
			return pveTalentId_;
		}
		set
		{
			pveTalentId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> SportGameData => sportGameData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoiceHeroC2S2()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoiceHeroC2S2(ChoiceHeroC2S2 other)
		: this()
	{
		heroId_ = other.heroId_;
		hasHero_ = other.hasHero_;
		useAdorn_ = other.useAdorn_;
		pveLevel_ = other.pveLevel_;
		skinPendant_ = other.skinPendant_;
		pveTalentId_ = other.pveTalentId_;
		sportGameData_ = other.sportGameData_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoiceHeroC2S2 Clone()
	{
		return new ChoiceHeroC2S2(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChoiceHeroC2S2);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(ChoiceHeroC2S2 other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (HasHero != other.HasHero)
		{
			return false;
		}
		if (UseAdorn != other.UseAdorn)
		{
			return false;
		}
		if (PveLevel != other.PveLevel)
		{
			return false;
		}
		if (SkinPendant != other.SkinPendant)
		{
			return false;
		}
		if (PveTalentId != other.PveTalentId)
		{
			return false;
		}
		if (!SportGameData.Equals(other.SportGameData))
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
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (HasHero)
		{
			num ^= HasHero.GetHashCode();
		}
		if (UseAdorn != 0)
		{
			num ^= UseAdorn.GetHashCode();
		}
		if (PveLevel != 0)
		{
			num ^= PveLevel.GetHashCode();
		}
		if (SkinPendant != 0)
		{
			num ^= SkinPendant.GetHashCode();
		}
		if (PveTalentId != 0)
		{
			num ^= PveTalentId.GetHashCode();
		}
		num ^= SportGameData.GetHashCode();
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
		if (HeroId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(HeroId);
		}
		if (HasHero)
		{
			output.WriteRawTag(16);
			output.WriteBool(HasHero);
		}
		if (UseAdorn != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(UseAdorn);
		}
		if (PveLevel != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(PveLevel);
		}
		if (SkinPendant != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(SkinPendant);
		}
		if (PveTalentId != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(PveTalentId);
		}
		sportGameData_.WriteTo(ref output, _map_sportGameData_codec);
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
		if (HeroId != 0)
		{
			num += 5;
		}
		if (HasHero)
		{
			num += 2;
		}
		if (UseAdorn != 0)
		{
			num += 5;
		}
		if (PveLevel != 0)
		{
			num += 5;
		}
		if (SkinPendant != 0)
		{
			num += 5;
		}
		if (PveTalentId != 0)
		{
			num += 5;
		}
		num += sportGameData_.CalculateSize(_map_sportGameData_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChoiceHeroC2S2 other)
	{
		if (other != null)
		{
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
			}
			if (other.HasHero)
			{
				HasHero = other.HasHero;
			}
			if (other.UseAdorn != 0)
			{
				UseAdorn = other.UseAdorn;
			}
			if (other.PveLevel != 0)
			{
				PveLevel = other.PveLevel;
			}
			if (other.SkinPendant != 0)
			{
				SkinPendant = other.SkinPendant;
			}
			if (other.PveTalentId != 0)
			{
				PveTalentId = other.PveTalentId;
			}
			sportGameData_.MergeFrom(other.sportGameData_);
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
				HeroId = input.ReadSFixed32();
				break;
			case 16u:
				HasHero = input.ReadBool();
				break;
			case 29u:
				UseAdorn = input.ReadSFixed32();
				break;
			case 37u:
				PveLevel = input.ReadSFixed32();
				break;
			case 45u:
				SkinPendant = input.ReadSFixed32();
				break;
			case 53u:
				PveTalentId = input.ReadSFixed32();
				break;
			case 58u:
				sportGameData_.AddEntriesFrom(ref input, _map_sportGameData_codec);
				break;
			}
		}
	}
}
