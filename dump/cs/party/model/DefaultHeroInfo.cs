using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class DefaultHeroInfo : IMessage<DefaultHeroInfo>, IMessage, IEquatable<DefaultHeroInfo>, IDeepCloneable<DefaultHeroInfo>, IBufferMessage
{
	private static readonly MessageParser<DefaultHeroInfo> _parser = new MessageParser<DefaultHeroInfo>(() => new DefaultHeroInfo());

	private UnknownFieldSet _unknownFields;

	public const int HeroIdFieldNumber = 1;

	private int heroId_;

	public const int UseAdornFieldNumber = 2;

	private int useAdorn_;

	public const int PveLevelFieldNumber = 3;

	private int pveLevel_;

	public const int SkinPendantFieldNumber = 4;

	private int skinPendant_;

	public const int PveTalentIdFieldNumber = 5;

	private int pveTalentId_;

	public const int GameDataFieldNumber = 6;

	private static readonly MapField<int, int>.Codec _map_gameData_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 50u);

	private readonly MapField<int, int> gameData_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<DefaultHeroInfo> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[99];

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
	public MapField<int, int> GameData => gameData_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DefaultHeroInfo()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DefaultHeroInfo(DefaultHeroInfo other)
		: this()
	{
		heroId_ = other.heroId_;
		useAdorn_ = other.useAdorn_;
		pveLevel_ = other.pveLevel_;
		skinPendant_ = other.skinPendant_;
		pveTalentId_ = other.pveTalentId_;
		gameData_ = other.gameData_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DefaultHeroInfo Clone()
	{
		return new DefaultHeroInfo(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DefaultHeroInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(DefaultHeroInfo other)
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
		if (!GameData.Equals(other.GameData))
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
		num ^= GameData.GetHashCode();
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
		if (UseAdorn != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(UseAdorn);
		}
		if (PveLevel != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(PveLevel);
		}
		if (SkinPendant != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(SkinPendant);
		}
		if (PveTalentId != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(PveTalentId);
		}
		gameData_.WriteTo(ref output, _map_gameData_codec);
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
		num += gameData_.CalculateSize(_map_gameData_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(DefaultHeroInfo other)
	{
		if (other != null)
		{
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
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
			gameData_.MergeFrom(other.gameData_);
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
			case 21u:
				UseAdorn = input.ReadSFixed32();
				break;
			case 29u:
				PveLevel = input.ReadSFixed32();
				break;
			case 37u:
				SkinPendant = input.ReadSFixed32();
				break;
			case 45u:
				PveTalentId = input.ReadSFixed32();
				break;
			case 50u:
				gameData_.AddEntriesFrom(ref input, _map_gameData_codec);
				break;
			}
		}
	}
}
