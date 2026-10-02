using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

public sealed class GameModeConfigure : IMessage<GameModeConfigure>, IMessage, IEquatable<GameModeConfigure>, IDeepCloneable<GameModeConfigure>, IBufferMessage
{
	private static readonly MessageParser<GameModeConfigure> _parser = new MessageParser<GameModeConfigure>(() => new GameModeConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<GameModeInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, GameModeInfoConfigure.Parser);

	private readonly RepeatedField<GameModeInfoConfigure> infos_ = new RepeatedField<GameModeInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, GameModeInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, GameModeInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GameModeInfoConfigure.Parser), 18u);

	private readonly MapField<int, GameModeInfoConfigure> infoDict_ = new MapField<int, GameModeInfoConfigure>();

	public const int NPCPlayersFieldNumber = 3;

	private static readonly FieldCodec<GameModeNPCPlayerConfigure> _repeated_nPCPlayers_codec = FieldCodec.ForMessage(26u, GameModeNPCPlayerConfigure.Parser);

	private readonly RepeatedField<GameModeNPCPlayerConfigure> nPCPlayers_ = new RepeatedField<GameModeNPCPlayerConfigure>();

	public const int NPCPlayerDictFieldNumber = 4;

	private static readonly MapField<int, GameModeNPCPlayerConfigure>.Codec _map_nPCPlayerDict_codec = new MapField<int, GameModeNPCPlayerConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GameModeNPCPlayerConfigure.Parser), 34u);

	private readonly MapField<int, GameModeNPCPlayerConfigure> nPCPlayerDict_ = new MapField<int, GameModeNPCPlayerConfigure>();

	public const int DifficultyDatasFieldNumber = 5;

	private static readonly FieldCodec<GameModeDifficultyDataConfigure> _repeated_difficultyDatas_codec = FieldCodec.ForMessage(42u, GameModeDifficultyDataConfigure.Parser);

	private readonly RepeatedField<GameModeDifficultyDataConfigure> difficultyDatas_ = new RepeatedField<GameModeDifficultyDataConfigure>();

	public const int DifficultyDataDictFieldNumber = 6;

	private static readonly MapField<int, GameModeDifficultyDataConfigure>.Codec _map_difficultyDataDict_codec = new MapField<int, GameModeDifficultyDataConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GameModeDifficultyDataConfigure.Parser), 50u);

	private readonly MapField<int, GameModeDifficultyDataConfigure> difficultyDataDict_ = new MapField<int, GameModeDifficultyDataConfigure>();

	public const int AsymmetricalBattlesFieldNumber = 7;

	private static readonly FieldCodec<GameModeAsymmetricalBattleConfigure> _repeated_asymmetricalBattles_codec = FieldCodec.ForMessage(58u, GameModeAsymmetricalBattleConfigure.Parser);

	private readonly RepeatedField<GameModeAsymmetricalBattleConfigure> asymmetricalBattles_ = new RepeatedField<GameModeAsymmetricalBattleConfigure>();

	public const int AsymmetricalBattleDictFieldNumber = 8;

	private static readonly MapField<int, GameModeAsymmetricalBattleConfigure>.Codec _map_asymmetricalBattleDict_codec = new MapField<int, GameModeAsymmetricalBattleConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GameModeAsymmetricalBattleConfigure.Parser), 66u);

	private readonly MapField<int, GameModeAsymmetricalBattleConfigure> asymmetricalBattleDict_ = new MapField<int, GameModeAsymmetricalBattleConfigure>();

	public const int MutatorPVEsFieldNumber = 9;

	private static readonly FieldCodec<GameModeMutatorPVEConfigure> _repeated_mutatorPVEs_codec = FieldCodec.ForMessage(74u, GameModeMutatorPVEConfigure.Parser);

	private readonly RepeatedField<GameModeMutatorPVEConfigure> mutatorPVEs_ = new RepeatedField<GameModeMutatorPVEConfigure>();

	public const int MutatorPVEDictFieldNumber = 10;

	private static readonly MapField<int, GameModeMutatorPVEConfigure>.Codec _map_mutatorPVEDict_codec = new MapField<int, GameModeMutatorPVEConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GameModeMutatorPVEConfigure.Parser), 82u);

	private readonly MapField<int, GameModeMutatorPVEConfigure> mutatorPVEDict_ = new MapField<int, GameModeMutatorPVEConfigure>();

	public const int GalleryMapsFieldNumber = 11;

	private static readonly FieldCodec<GameModeGalleryMapConfigure> _repeated_galleryMaps_codec = FieldCodec.ForMessage(90u, GameModeGalleryMapConfigure.Parser);

	private readonly RepeatedField<GameModeGalleryMapConfigure> galleryMaps_ = new RepeatedField<GameModeGalleryMapConfigure>();

	public const int GalleryMapDictFieldNumber = 12;

	private static readonly MapField<int, GameModeGalleryMapConfigure>.Codec _map_galleryMapDict_codec = new MapField<int, GameModeGalleryMapConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, GameModeGalleryMapConfigure.Parser), 98u);

	private readonly MapField<int, GameModeGalleryMapConfigure> galleryMapDict_ = new MapField<int, GameModeGalleryMapConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<GameModeConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => GameModeReflection.Descriptor.MessageTypes[7];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GameModeInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GameModeInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GameModeNPCPlayerConfigure> NPCPlayers => nPCPlayers_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GameModeNPCPlayerConfigure> NPCPlayerDict => nPCPlayerDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GameModeDifficultyDataConfigure> DifficultyDatas => difficultyDatas_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GameModeDifficultyDataConfigure> DifficultyDataDict => difficultyDataDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GameModeAsymmetricalBattleConfigure> AsymmetricalBattles => asymmetricalBattles_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GameModeAsymmetricalBattleConfigure> AsymmetricalBattleDict => asymmetricalBattleDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GameModeMutatorPVEConfigure> MutatorPVEs => mutatorPVEs_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GameModeMutatorPVEConfigure> MutatorPVEDict => mutatorPVEDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<GameModeGalleryMapConfigure> GalleryMaps => galleryMaps_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, GameModeGalleryMapConfigure> GalleryMapDict => galleryMapDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeConfigure(GameModeConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		nPCPlayers_ = other.nPCPlayers_.Clone();
		nPCPlayerDict_ = other.nPCPlayerDict_.Clone();
		difficultyDatas_ = other.difficultyDatas_.Clone();
		difficultyDataDict_ = other.difficultyDataDict_.Clone();
		asymmetricalBattles_ = other.asymmetricalBattles_.Clone();
		asymmetricalBattleDict_ = other.asymmetricalBattleDict_.Clone();
		mutatorPVEs_ = other.mutatorPVEs_.Clone();
		mutatorPVEDict_ = other.mutatorPVEDict_.Clone();
		galleryMaps_ = other.galleryMaps_.Clone();
		galleryMapDict_ = other.galleryMapDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameModeConfigure Clone()
	{
		return new GameModeConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GameModeConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(GameModeConfigure other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!infos_.Equals(other.infos_))
		{
			return false;
		}
		if (!InfoDict.Equals(other.InfoDict))
		{
			return false;
		}
		if (!nPCPlayers_.Equals(other.nPCPlayers_))
		{
			return false;
		}
		if (!NPCPlayerDict.Equals(other.NPCPlayerDict))
		{
			return false;
		}
		if (!difficultyDatas_.Equals(other.difficultyDatas_))
		{
			return false;
		}
		if (!DifficultyDataDict.Equals(other.DifficultyDataDict))
		{
			return false;
		}
		if (!asymmetricalBattles_.Equals(other.asymmetricalBattles_))
		{
			return false;
		}
		if (!AsymmetricalBattleDict.Equals(other.AsymmetricalBattleDict))
		{
			return false;
		}
		if (!mutatorPVEs_.Equals(other.mutatorPVEs_))
		{
			return false;
		}
		if (!MutatorPVEDict.Equals(other.MutatorPVEDict))
		{
			return false;
		}
		if (!galleryMaps_.Equals(other.galleryMaps_))
		{
			return false;
		}
		if (!GalleryMapDict.Equals(other.GalleryMapDict))
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
		num ^= infos_.GetHashCode();
		num ^= InfoDict.GetHashCode();
		num ^= nPCPlayers_.GetHashCode();
		num ^= NPCPlayerDict.GetHashCode();
		num ^= difficultyDatas_.GetHashCode();
		num ^= DifficultyDataDict.GetHashCode();
		num ^= asymmetricalBattles_.GetHashCode();
		num ^= AsymmetricalBattleDict.GetHashCode();
		num ^= mutatorPVEs_.GetHashCode();
		num ^= MutatorPVEDict.GetHashCode();
		num ^= galleryMaps_.GetHashCode();
		num ^= GalleryMapDict.GetHashCode();
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
		infos_.WriteTo(ref output, _repeated_infos_codec);
		infoDict_.WriteTo(ref output, _map_infoDict_codec);
		nPCPlayers_.WriteTo(ref output, _repeated_nPCPlayers_codec);
		nPCPlayerDict_.WriteTo(ref output, _map_nPCPlayerDict_codec);
		difficultyDatas_.WriteTo(ref output, _repeated_difficultyDatas_codec);
		difficultyDataDict_.WriteTo(ref output, _map_difficultyDataDict_codec);
		asymmetricalBattles_.WriteTo(ref output, _repeated_asymmetricalBattles_codec);
		asymmetricalBattleDict_.WriteTo(ref output, _map_asymmetricalBattleDict_codec);
		mutatorPVEs_.WriteTo(ref output, _repeated_mutatorPVEs_codec);
		mutatorPVEDict_.WriteTo(ref output, _map_mutatorPVEDict_codec);
		galleryMaps_.WriteTo(ref output, _repeated_galleryMaps_codec);
		galleryMapDict_.WriteTo(ref output, _map_galleryMapDict_codec);
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
		num += infos_.CalculateSize(_repeated_infos_codec);
		num += infoDict_.CalculateSize(_map_infoDict_codec);
		num += nPCPlayers_.CalculateSize(_repeated_nPCPlayers_codec);
		num += nPCPlayerDict_.CalculateSize(_map_nPCPlayerDict_codec);
		num += difficultyDatas_.CalculateSize(_repeated_difficultyDatas_codec);
		num += difficultyDataDict_.CalculateSize(_map_difficultyDataDict_codec);
		num += asymmetricalBattles_.CalculateSize(_repeated_asymmetricalBattles_codec);
		num += asymmetricalBattleDict_.CalculateSize(_map_asymmetricalBattleDict_codec);
		num += mutatorPVEs_.CalculateSize(_repeated_mutatorPVEs_codec);
		num += mutatorPVEDict_.CalculateSize(_map_mutatorPVEDict_codec);
		num += galleryMaps_.CalculateSize(_repeated_galleryMaps_codec);
		num += galleryMapDict_.CalculateSize(_map_galleryMapDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(GameModeConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			nPCPlayers_.Add(other.nPCPlayers_);
			nPCPlayerDict_.MergeFrom(other.nPCPlayerDict_);
			difficultyDatas_.Add(other.difficultyDatas_);
			difficultyDataDict_.MergeFrom(other.difficultyDataDict_);
			asymmetricalBattles_.Add(other.asymmetricalBattles_);
			asymmetricalBattleDict_.MergeFrom(other.asymmetricalBattleDict_);
			mutatorPVEs_.Add(other.mutatorPVEs_);
			mutatorPVEDict_.MergeFrom(other.mutatorPVEDict_);
			galleryMaps_.Add(other.galleryMaps_);
			galleryMapDict_.MergeFrom(other.galleryMapDict_);
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
			case 10u:
				infos_.AddEntriesFrom(ref input, _repeated_infos_codec);
				break;
			case 18u:
				infoDict_.AddEntriesFrom(ref input, _map_infoDict_codec);
				break;
			case 26u:
				nPCPlayers_.AddEntriesFrom(ref input, _repeated_nPCPlayers_codec);
				break;
			case 34u:
				nPCPlayerDict_.AddEntriesFrom(ref input, _map_nPCPlayerDict_codec);
				break;
			case 42u:
				difficultyDatas_.AddEntriesFrom(ref input, _repeated_difficultyDatas_codec);
				break;
			case 50u:
				difficultyDataDict_.AddEntriesFrom(ref input, _map_difficultyDataDict_codec);
				break;
			case 58u:
				asymmetricalBattles_.AddEntriesFrom(ref input, _repeated_asymmetricalBattles_codec);
				break;
			case 66u:
				asymmetricalBattleDict_.AddEntriesFrom(ref input, _map_asymmetricalBattleDict_codec);
				break;
			case 74u:
				mutatorPVEs_.AddEntriesFrom(ref input, _repeated_mutatorPVEs_codec);
				break;
			case 82u:
				mutatorPVEDict_.AddEntriesFrom(ref input, _map_mutatorPVEDict_codec);
				break;
			case 90u:
				galleryMaps_.AddEntriesFrom(ref input, _repeated_galleryMaps_codec);
				break;
			case 98u:
				galleryMapDict_.AddEntriesFrom(ref input, _map_galleryMapDict_codec);
				break;
			}
		}
	}

	public void Fix(FixGameModeConfigure FixGameMode)
	{
		if (FixGameMode == null)
		{
			return;
		}
		MapField<int, FixGameModeInfoConfigure> infoDict = FixGameMode.InfoDict;
		if (infoDict == null || infoDict.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < infos_.Count; i++)
		{
			if (infoDict.TryGetValue((int)infos_[i].MapModeType, out var value))
			{
				infos_[i].FixTime(value);
				infoDict_[(int)infos_[i].MapModeType].FixTime(value);
				infos_[i].MapID.Clear();
				infos_[i].MapID.AddRange(value.MapID);
				infoDict_[(int)infos_[i].MapModeType].MapID.Clear();
				infoDict_[(int)infos_[i].MapModeType].MapID.AddRange(value.MapID);
			}
		}
	}
}
