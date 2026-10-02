using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class HeroBar : IMessage<HeroBar>, IMessage, IEquatable<HeroBar>, IDeepCloneable<HeroBar>, IBufferMessage
{
	private static readonly MessageParser<HeroBar> _parser = new MessageParser<HeroBar>(() => new HeroBar());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int HeroIdFieldNumber = 2;

	private int heroId_;

	public const int AffirmFieldNumber = 3;

	private bool affirm_;

	public const int UseAdornFieldNumber = 4;

	private int useAdorn_;

	public const int PveLevelFieldNumber = 5;

	private int pveLevel_;

	public const int IsTrialFieldNumber = 6;

	private bool isTrial_;

	public const int SkinPendantFieldNumber = 7;

	private int skinPendant_;

	public const int AffirmedSkinFieldNumber = 8;

	private bool affirmedSkin_;

	public const int PveTalentIdFieldNumber = 9;

	private int pveTalentId_;

	public const int GameDataFieldNumber = 10;

	private static readonly MapField<int, int>.Codec _map_gameData_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<int, int> gameData_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<HeroBar> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[53];

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
	public bool Affirm
	{
		get
		{
			return affirm_;
		}
		set
		{
			affirm_ = value;
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
	public bool IsTrial
	{
		get
		{
			return isTrial_;
		}
		set
		{
			isTrial_ = value;
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
	public bool AffirmedSkin
	{
		get
		{
			return affirmedSkin_;
		}
		set
		{
			affirmedSkin_ = value;
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
	public HeroBar()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroBar(HeroBar other)
		: this()
	{
		playerId_ = other.playerId_;
		heroId_ = other.heroId_;
		affirm_ = other.affirm_;
		useAdorn_ = other.useAdorn_;
		pveLevel_ = other.pveLevel_;
		isTrial_ = other.isTrial_;
		skinPendant_ = other.skinPendant_;
		affirmedSkin_ = other.affirmedSkin_;
		pveTalentId_ = other.pveTalentId_;
		gameData_ = other.gameData_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroBar Clone()
	{
		return new HeroBar(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HeroBar);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(HeroBar other)
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
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (Affirm != other.Affirm)
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
		if (IsTrial != other.IsTrial)
		{
			return false;
		}
		if (SkinPendant != other.SkinPendant)
		{
			return false;
		}
		if (AffirmedSkin != other.AffirmedSkin)
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
		if (PlayerId != 0L)
		{
			num ^= PlayerId.GetHashCode();
		}
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (Affirm)
		{
			num ^= Affirm.GetHashCode();
		}
		if (UseAdorn != 0)
		{
			num ^= UseAdorn.GetHashCode();
		}
		if (PveLevel != 0)
		{
			num ^= PveLevel.GetHashCode();
		}
		if (IsTrial)
		{
			num ^= IsTrial.GetHashCode();
		}
		if (SkinPendant != 0)
		{
			num ^= SkinPendant.GetHashCode();
		}
		if (AffirmedSkin)
		{
			num ^= AffirmedSkin.GetHashCode();
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
		if (PlayerId != 0L)
		{
			output.WriteRawTag(9);
			output.WriteSFixed64(PlayerId);
		}
		if (HeroId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(HeroId);
		}
		if (Affirm)
		{
			output.WriteRawTag(24);
			output.WriteBool(Affirm);
		}
		if (UseAdorn != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(UseAdorn);
		}
		if (PveLevel != 0)
		{
			output.WriteRawTag(45);
			output.WriteSFixed32(PveLevel);
		}
		if (IsTrial)
		{
			output.WriteRawTag(48);
			output.WriteBool(IsTrial);
		}
		if (SkinPendant != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(SkinPendant);
		}
		if (AffirmedSkin)
		{
			output.WriteRawTag(64);
			output.WriteBool(AffirmedSkin);
		}
		if (PveTalentId != 0)
		{
			output.WriteRawTag(77);
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
		if (PlayerId != 0L)
		{
			num += 9;
		}
		if (HeroId != 0)
		{
			num += 5;
		}
		if (Affirm)
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
		if (IsTrial)
		{
			num += 2;
		}
		if (SkinPendant != 0)
		{
			num += 5;
		}
		if (AffirmedSkin)
		{
			num += 2;
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
	public void MergeFrom(HeroBar other)
	{
		if (other != null)
		{
			if (other.PlayerId != 0L)
			{
				PlayerId = other.PlayerId;
			}
			if (other.HeroId != 0)
			{
				HeroId = other.HeroId;
			}
			if (other.Affirm)
			{
				Affirm = other.Affirm;
			}
			if (other.UseAdorn != 0)
			{
				UseAdorn = other.UseAdorn;
			}
			if (other.PveLevel != 0)
			{
				PveLevel = other.PveLevel;
			}
			if (other.IsTrial)
			{
				IsTrial = other.IsTrial;
			}
			if (other.SkinPendant != 0)
			{
				SkinPendant = other.SkinPendant;
			}
			if (other.AffirmedSkin)
			{
				AffirmedSkin = other.AffirmedSkin;
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 21u:
				HeroId = input.ReadSFixed32();
				break;
			case 24u:
				Affirm = input.ReadBool();
				break;
			case 37u:
				UseAdorn = input.ReadSFixed32();
				break;
			case 45u:
				PveLevel = input.ReadSFixed32();
				break;
			case 48u:
				IsTrial = input.ReadBool();
				break;
			case 61u:
				SkinPendant = input.ReadSFixed32();
				break;
			case 64u:
				AffirmedSkin = input.ReadBool();
				break;
			case 77u:
				PveTalentId = input.ReadSFixed32();
				break;
			case 82u:
				gameData_.AddEntriesFrom(ref input, _map_gameData_codec);
				break;
			}
		}
	}
}
