using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Tools;

public sealed class MapConfigure : IMessage<MapConfigure>, IMessage, IEquatable<MapConfigure>, IDeepCloneable<MapConfigure>, IBufferMessage
{
	private static readonly MessageParser<MapConfigure> _parser = new MessageParser<MapConfigure>(() => new MapConfigure());

	private UnknownFieldSet _unknownFields;

	public const int InfosFieldNumber = 1;

	private static readonly FieldCodec<MapInfoConfigure> _repeated_infos_codec = FieldCodec.ForMessage(10u, MapInfoConfigure.Parser);

	private readonly RepeatedField<MapInfoConfigure> infos_ = new RepeatedField<MapInfoConfigure>();

	public const int InfoDictFieldNumber = 2;

	private static readonly MapField<int, MapInfoConfigure>.Codec _map_infoDict_codec = new MapField<int, MapInfoConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MapInfoConfigure.Parser), 18u);

	private readonly MapField<int, MapInfoConfigure> infoDict_ = new MapField<int, MapInfoConfigure>();

	public const int ScenesFieldNumber = 3;

	private static readonly FieldCodec<MapSceneConfigure> _repeated_scenes_codec = FieldCodec.ForMessage(26u, MapSceneConfigure.Parser);

	private readonly RepeatedField<MapSceneConfigure> scenes_ = new RepeatedField<MapSceneConfigure>();

	public const int SceneDictFieldNumber = 4;

	private static readonly MapField<int, MapSceneConfigure>.Codec _map_sceneDict_codec = new MapField<int, MapSceneConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MapSceneConfigure.Parser), 34u);

	private readonly MapField<int, MapSceneConfigure> sceneDict_ = new MapField<int, MapSceneConfigure>();

	public const int GameDifficultysFieldNumber = 5;

	private static readonly FieldCodec<MapGameDifficultyConfigure> _repeated_gameDifficultys_codec = FieldCodec.ForMessage(42u, MapGameDifficultyConfigure.Parser);

	private readonly RepeatedField<MapGameDifficultyConfigure> gameDifficultys_ = new RepeatedField<MapGameDifficultyConfigure>();

	public const int GameDifficultyDictFieldNumber = 6;

	private static readonly MapField<int, MapGameDifficultyConfigure>.Codec _map_gameDifficultyDict_codec = new MapField<int, MapGameDifficultyConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MapGameDifficultyConfigure.Parser), 50u);

	private readonly MapField<int, MapGameDifficultyConfigure> gameDifficultyDict_ = new MapField<int, MapGameDifficultyConfigure>();

	public const int MapRewardsFieldNumber = 7;

	private static readonly FieldCodec<MapMapRewardConfigure> _repeated_mapRewards_codec = FieldCodec.ForMessage(58u, MapMapRewardConfigure.Parser);

	private readonly RepeatedField<MapMapRewardConfigure> mapRewards_ = new RepeatedField<MapMapRewardConfigure>();

	public const int MapRewardDictFieldNumber = 8;

	private static readonly MapField<int, MapMapRewardConfigure>.Codec _map_mapRewardDict_codec = new MapField<int, MapMapRewardConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MapMapRewardConfigure.Parser), 66u);

	private readonly MapField<int, MapMapRewardConfigure> mapRewardDict_ = new MapField<int, MapMapRewardConfigure>();

	public const int MapLevelsFieldNumber = 9;

	private static readonly FieldCodec<MapMapLevelConfigure> _repeated_mapLevels_codec = FieldCodec.ForMessage(74u, MapMapLevelConfigure.Parser);

	private readonly RepeatedField<MapMapLevelConfigure> mapLevels_ = new RepeatedField<MapMapLevelConfigure>();

	public const int MapLevelDictFieldNumber = 10;

	private static readonly MapField<int, MapMapLevelConfigure>.Codec _map_mapLevelDict_codec = new MapField<int, MapMapLevelConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MapMapLevelConfigure.Parser), 82u);

	private readonly MapField<int, MapMapLevelConfigure> mapLevelDict_ = new MapField<int, MapMapLevelConfigure>();

	public const int MapPoolsFieldNumber = 11;

	private static readonly FieldCodec<MapMapPoolConfigure> _repeated_mapPools_codec = FieldCodec.ForMessage(90u, MapMapPoolConfigure.Parser);

	private readonly RepeatedField<MapMapPoolConfigure> mapPools_ = new RepeatedField<MapMapPoolConfigure>();

	public const int MapPoolDictFieldNumber = 12;

	private static readonly MapField<int, MapMapPoolConfigure>.Codec _map_mapPoolDict_codec = new MapField<int, MapMapPoolConfigure>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForMessage(18u, MapMapPoolConfigure.Parser), 98u);

	private readonly MapField<int, MapMapPoolConfigure> mapPoolDict_ = new MapField<int, MapMapPoolConfigure>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<MapConfigure> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MapReflection.Descriptor.MessageTypes[8];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapInfoConfigure> Infos => infos_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapInfoConfigure> InfoDict => infoDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapSceneConfigure> Scenes => scenes_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapSceneConfigure> SceneDict => sceneDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapGameDifficultyConfigure> GameDifficultys => gameDifficultys_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapGameDifficultyConfigure> GameDifficultyDict => gameDifficultyDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapMapRewardConfigure> MapRewards => mapRewards_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapMapRewardConfigure> MapRewardDict => mapRewardDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapMapLevelConfigure> MapLevels => mapLevels_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapMapLevelConfigure> MapLevelDict => mapLevelDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<MapMapPoolConfigure> MapPools => mapPools_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, MapMapPoolConfigure> MapPoolDict => mapPoolDict_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapConfigure()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapConfigure(MapConfigure other)
		: this()
	{
		infos_ = other.infos_.Clone();
		infoDict_ = other.infoDict_.Clone();
		scenes_ = other.scenes_.Clone();
		sceneDict_ = other.sceneDict_.Clone();
		gameDifficultys_ = other.gameDifficultys_.Clone();
		gameDifficultyDict_ = other.gameDifficultyDict_.Clone();
		mapRewards_ = other.mapRewards_.Clone();
		mapRewardDict_ = other.mapRewardDict_.Clone();
		mapLevels_ = other.mapLevels_.Clone();
		mapLevelDict_ = other.mapLevelDict_.Clone();
		mapPools_ = other.mapPools_.Clone();
		mapPoolDict_ = other.mapPoolDict_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapConfigure Clone()
	{
		return new MapConfigure(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MapConfigure);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(MapConfigure other)
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
		if (!scenes_.Equals(other.scenes_))
		{
			return false;
		}
		if (!SceneDict.Equals(other.SceneDict))
		{
			return false;
		}
		if (!gameDifficultys_.Equals(other.gameDifficultys_))
		{
			return false;
		}
		if (!GameDifficultyDict.Equals(other.GameDifficultyDict))
		{
			return false;
		}
		if (!mapRewards_.Equals(other.mapRewards_))
		{
			return false;
		}
		if (!MapRewardDict.Equals(other.MapRewardDict))
		{
			return false;
		}
		if (!mapLevels_.Equals(other.mapLevels_))
		{
			return false;
		}
		if (!MapLevelDict.Equals(other.MapLevelDict))
		{
			return false;
		}
		if (!mapPools_.Equals(other.mapPools_))
		{
			return false;
		}
		if (!MapPoolDict.Equals(other.MapPoolDict))
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
		num ^= scenes_.GetHashCode();
		num ^= SceneDict.GetHashCode();
		num ^= gameDifficultys_.GetHashCode();
		num ^= GameDifficultyDict.GetHashCode();
		num ^= mapRewards_.GetHashCode();
		num ^= MapRewardDict.GetHashCode();
		num ^= mapLevels_.GetHashCode();
		num ^= MapLevelDict.GetHashCode();
		num ^= mapPools_.GetHashCode();
		num ^= MapPoolDict.GetHashCode();
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
		scenes_.WriteTo(ref output, _repeated_scenes_codec);
		sceneDict_.WriteTo(ref output, _map_sceneDict_codec);
		gameDifficultys_.WriteTo(ref output, _repeated_gameDifficultys_codec);
		gameDifficultyDict_.WriteTo(ref output, _map_gameDifficultyDict_codec);
		mapRewards_.WriteTo(ref output, _repeated_mapRewards_codec);
		mapRewardDict_.WriteTo(ref output, _map_mapRewardDict_codec);
		mapLevels_.WriteTo(ref output, _repeated_mapLevels_codec);
		mapLevelDict_.WriteTo(ref output, _map_mapLevelDict_codec);
		mapPools_.WriteTo(ref output, _repeated_mapPools_codec);
		mapPoolDict_.WriteTo(ref output, _map_mapPoolDict_codec);
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
		num += scenes_.CalculateSize(_repeated_scenes_codec);
		num += sceneDict_.CalculateSize(_map_sceneDict_codec);
		num += gameDifficultys_.CalculateSize(_repeated_gameDifficultys_codec);
		num += gameDifficultyDict_.CalculateSize(_map_gameDifficultyDict_codec);
		num += mapRewards_.CalculateSize(_repeated_mapRewards_codec);
		num += mapRewardDict_.CalculateSize(_map_mapRewardDict_codec);
		num += mapLevels_.CalculateSize(_repeated_mapLevels_codec);
		num += mapLevelDict_.CalculateSize(_map_mapLevelDict_codec);
		num += mapPools_.CalculateSize(_repeated_mapPools_codec);
		num += mapPoolDict_.CalculateSize(_map_mapPoolDict_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(MapConfigure other)
	{
		if (other != null)
		{
			infos_.Add(other.infos_);
			infoDict_.MergeFrom(other.infoDict_);
			scenes_.Add(other.scenes_);
			sceneDict_.MergeFrom(other.sceneDict_);
			gameDifficultys_.Add(other.gameDifficultys_);
			gameDifficultyDict_.MergeFrom(other.gameDifficultyDict_);
			mapRewards_.Add(other.mapRewards_);
			mapRewardDict_.MergeFrom(other.mapRewardDict_);
			mapLevels_.Add(other.mapLevels_);
			mapLevelDict_.MergeFrom(other.mapLevelDict_);
			mapPools_.Add(other.mapPools_);
			mapPoolDict_.MergeFrom(other.mapPoolDict_);
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
				scenes_.AddEntriesFrom(ref input, _repeated_scenes_codec);
				break;
			case 34u:
				sceneDict_.AddEntriesFrom(ref input, _map_sceneDict_codec);
				break;
			case 42u:
				gameDifficultys_.AddEntriesFrom(ref input, _repeated_gameDifficultys_codec);
				break;
			case 50u:
				gameDifficultyDict_.AddEntriesFrom(ref input, _map_gameDifficultyDict_codec);
				break;
			case 58u:
				mapRewards_.AddEntriesFrom(ref input, _repeated_mapRewards_codec);
				break;
			case 66u:
				mapRewardDict_.AddEntriesFrom(ref input, _map_mapRewardDict_codec);
				break;
			case 74u:
				mapLevels_.AddEntriesFrom(ref input, _repeated_mapLevels_codec);
				break;
			case 82u:
				mapLevelDict_.AddEntriesFrom(ref input, _map_mapLevelDict_codec);
				break;
			case 90u:
				mapPools_.AddEntriesFrom(ref input, _repeated_mapPools_codec);
				break;
			case 98u:
				mapPoolDict_.AddEntriesFrom(ref input, _map_mapPoolDict_codec);
				break;
			}
		}
	}

	public void Fix(FixMapConfigure FixMap)
	{
		if (FixMap == null)
		{
			return;
		}
		MapField<int, FixMapMapLevelConfigure> mapLevelDict = FixMap.MapLevelDict;
		if (mapLevelDict == null || mapLevelDict.Count <= 0)
		{
			return;
		}
		for (int num = mapLevels_.Count - 1; num >= 0; num--)
		{
			if (mapLevelDict.TryGetValue(mapLevels_[num].Id, out var value))
			{
				mapLevels_[num].FixData(value);
				mapLevelDict_[mapLevels_[num].Id].FixData(value);
			}
		}
	}

	public RepeatedField<MapInfoConfigure> GetMapInfoConfigs()
	{
		RepeatedField<MapInfoConfigure> repeatedField = new RepeatedField<MapInfoConfigure>();
		for (int i = 0; i < infos_.Count; i++)
		{
			if (TimeHelper.ValidityTime(infos_[i].BeginTimeMap, infos_[i].EndTimeMap))
			{
				repeatedField.Add(infos_[i]);
			}
		}
		return repeatedField;
	}
}
