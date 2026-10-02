using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace party.model;

public sealed class SingleGameData : IMessage<SingleGameData>, IMessage, IEquatable<SingleGameData>, IDeepCloneable<SingleGameData>, IBufferMessage
{
	private static readonly MessageParser<SingleGameData> _parser = new MessageParser<SingleGameData>(() => new SingleGameData());

	private UnknownFieldSet _unknownFields;

	public const int LevelIdFieldNumber = 1;

	private int levelId_;

	public const int RoundFieldNumber = 2;

	private int round_;

	public const int GameProgressFieldNumber = 3;

	private int gameProgress_;

	public const int HeroFieldNumber = 5;

	private SingleHero hero_;

	public const int CardDataFieldNumber = 6;

	private SingleCardData cardData_;

	public const int BuildingDataFieldNumber = 7;

	private SingleBuildingData buildingData_;

	public const int ClientGenerateUIDFieldNumber = 10;

	private int clientGenerateUID_;

	public const int GameStatusFieldNumber = 11;

	private int gameStatus_;

	public const int MapDataFieldNumber = 12;

	private SingleMapData mapData_;

	public const int RoundTimingFieldNumber = 13;

	private int roundTiming_;

	public const int SelectCardIdsFieldNumber = 14;

	private static readonly FieldCodec<int> _repeated_selectCardIds_codec = FieldCodec.ForSFixed32(114u);

	private readonly RepeatedField<int> selectCardIds_ = new RepeatedField<int>();

	public const int StageIdFieldNumber = 15;

	private int stageId_;

	public const int StageLevelIdFieldNumber = 16;

	private static readonly MapField<int, int>.Codec _map_stageLevelId_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 130u);

	private readonly MapField<int, int> stageLevelId_ = new MapField<int, int>();

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SingleGameData> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ModelReflection.Descriptor.MessageTypes[104];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int LevelId
	{
		get
		{
			return levelId_;
		}
		set
		{
			levelId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Round
	{
		get
		{
			return round_;
		}
		set
		{
			round_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GameProgress
	{
		get
		{
			return gameProgress_;
		}
		set
		{
			gameProgress_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleHero Hero
	{
		get
		{
			return hero_;
		}
		set
		{
			hero_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleCardData CardData
	{
		get
		{
			return cardData_;
		}
		set
		{
			cardData_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleBuildingData BuildingData
	{
		get
		{
			return buildingData_;
		}
		set
		{
			buildingData_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int ClientGenerateUID
	{
		get
		{
			return clientGenerateUID_;
		}
		set
		{
			clientGenerateUID_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int GameStatus
	{
		get
		{
			return gameStatus_;
		}
		set
		{
			gameStatus_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleMapData MapData
	{
		get
		{
			return mapData_;
		}
		set
		{
			mapData_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int RoundTiming
	{
		get
		{
			return roundTiming_;
		}
		set
		{
			roundTiming_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RepeatedField<int> SelectCardIds => selectCardIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int StageId
	{
		get
		{
			return stageId_;
		}
		set
		{
			stageId_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> StageLevelId => stageLevelId_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleGameData()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleGameData(SingleGameData other)
		: this()
	{
		levelId_ = other.levelId_;
		round_ = other.round_;
		gameProgress_ = other.gameProgress_;
		hero_ = ((other.hero_ != null) ? other.hero_.Clone() : null);
		cardData_ = ((other.cardData_ != null) ? other.cardData_.Clone() : null);
		buildingData_ = ((other.buildingData_ != null) ? other.buildingData_.Clone() : null);
		clientGenerateUID_ = other.clientGenerateUID_;
		gameStatus_ = other.gameStatus_;
		mapData_ = ((other.mapData_ != null) ? other.mapData_.Clone() : null);
		roundTiming_ = other.roundTiming_;
		selectCardIds_ = other.selectCardIds_.Clone();
		stageId_ = other.stageId_;
		stageLevelId_ = other.stageLevelId_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleGameData Clone()
	{
		return new SingleGameData(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SingleGameData);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SingleGameData other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (LevelId != other.LevelId)
		{
			return false;
		}
		if (Round != other.Round)
		{
			return false;
		}
		if (GameProgress != other.GameProgress)
		{
			return false;
		}
		if (!object.Equals(Hero, other.Hero))
		{
			return false;
		}
		if (!object.Equals(CardData, other.CardData))
		{
			return false;
		}
		if (!object.Equals(BuildingData, other.BuildingData))
		{
			return false;
		}
		if (ClientGenerateUID != other.ClientGenerateUID)
		{
			return false;
		}
		if (GameStatus != other.GameStatus)
		{
			return false;
		}
		if (!object.Equals(MapData, other.MapData))
		{
			return false;
		}
		if (RoundTiming != other.RoundTiming)
		{
			return false;
		}
		if (!selectCardIds_.Equals(other.selectCardIds_))
		{
			return false;
		}
		if (StageId != other.StageId)
		{
			return false;
		}
		if (!StageLevelId.Equals(other.StageLevelId))
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
		if (LevelId != 0)
		{
			num ^= LevelId.GetHashCode();
		}
		if (Round != 0)
		{
			num ^= Round.GetHashCode();
		}
		if (GameProgress != 0)
		{
			num ^= GameProgress.GetHashCode();
		}
		if (hero_ != null)
		{
			num ^= Hero.GetHashCode();
		}
		if (cardData_ != null)
		{
			num ^= CardData.GetHashCode();
		}
		if (buildingData_ != null)
		{
			num ^= BuildingData.GetHashCode();
		}
		if (ClientGenerateUID != 0)
		{
			num ^= ClientGenerateUID.GetHashCode();
		}
		if (GameStatus != 0)
		{
			num ^= GameStatus.GetHashCode();
		}
		if (mapData_ != null)
		{
			num ^= MapData.GetHashCode();
		}
		if (RoundTiming != 0)
		{
			num ^= RoundTiming.GetHashCode();
		}
		num ^= selectCardIds_.GetHashCode();
		if (StageId != 0)
		{
			num ^= StageId.GetHashCode();
		}
		num ^= StageLevelId.GetHashCode();
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
		if (LevelId != 0)
		{
			output.WriteRawTag(13);
			output.WriteSFixed32(LevelId);
		}
		if (Round != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(Round);
		}
		if (GameProgress != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(GameProgress);
		}
		if (hero_ != null)
		{
			output.WriteRawTag(42);
			output.WriteMessage(Hero);
		}
		if (cardData_ != null)
		{
			output.WriteRawTag(50);
			output.WriteMessage(CardData);
		}
		if (buildingData_ != null)
		{
			output.WriteRawTag(58);
			output.WriteMessage(BuildingData);
		}
		if (ClientGenerateUID != 0)
		{
			output.WriteRawTag(85);
			output.WriteSFixed32(ClientGenerateUID);
		}
		if (GameStatus != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(GameStatus);
		}
		if (mapData_ != null)
		{
			output.WriteRawTag(98);
			output.WriteMessage(MapData);
		}
		if (RoundTiming != 0)
		{
			output.WriteRawTag(109);
			output.WriteSFixed32(RoundTiming);
		}
		selectCardIds_.WriteTo(ref output, _repeated_selectCardIds_codec);
		if (StageId != 0)
		{
			output.WriteRawTag(125);
			output.WriteSFixed32(StageId);
		}
		stageLevelId_.WriteTo(ref output, _map_stageLevelId_codec);
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
		if (LevelId != 0)
		{
			num += 5;
		}
		if (Round != 0)
		{
			num += 5;
		}
		if (GameProgress != 0)
		{
			num += 5;
		}
		if (hero_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Hero);
		}
		if (cardData_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CardData);
		}
		if (buildingData_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BuildingData);
		}
		if (ClientGenerateUID != 0)
		{
			num += 5;
		}
		if (GameStatus != 0)
		{
			num += 5;
		}
		if (mapData_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(MapData);
		}
		if (RoundTiming != 0)
		{
			num += 5;
		}
		num += selectCardIds_.CalculateSize(_repeated_selectCardIds_codec);
		if (StageId != 0)
		{
			num += 5;
		}
		num += stageLevelId_.CalculateSize(_map_stageLevelId_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SingleGameData other)
	{
		if (other == null)
		{
			return;
		}
		if (other.LevelId != 0)
		{
			LevelId = other.LevelId;
		}
		if (other.Round != 0)
		{
			Round = other.Round;
		}
		if (other.GameProgress != 0)
		{
			GameProgress = other.GameProgress;
		}
		if (other.hero_ != null)
		{
			if (hero_ == null)
			{
				Hero = new SingleHero();
			}
			Hero.MergeFrom(other.Hero);
		}
		if (other.cardData_ != null)
		{
			if (cardData_ == null)
			{
				CardData = new SingleCardData();
			}
			CardData.MergeFrom(other.CardData);
		}
		if (other.buildingData_ != null)
		{
			if (buildingData_ == null)
			{
				BuildingData = new SingleBuildingData();
			}
			BuildingData.MergeFrom(other.BuildingData);
		}
		if (other.ClientGenerateUID != 0)
		{
			ClientGenerateUID = other.ClientGenerateUID;
		}
		if (other.GameStatus != 0)
		{
			GameStatus = other.GameStatus;
		}
		if (other.mapData_ != null)
		{
			if (mapData_ == null)
			{
				MapData = new SingleMapData();
			}
			MapData.MergeFrom(other.MapData);
		}
		if (other.RoundTiming != 0)
		{
			RoundTiming = other.RoundTiming;
		}
		selectCardIds_.Add(other.selectCardIds_);
		if (other.StageId != 0)
		{
			StageId = other.StageId;
		}
		stageLevelId_.MergeFrom(other.stageLevelId_);
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
				LevelId = input.ReadSFixed32();
				break;
			case 21u:
				Round = input.ReadSFixed32();
				break;
			case 29u:
				GameProgress = input.ReadSFixed32();
				break;
			case 42u:
				if (hero_ == null)
				{
					Hero = new SingleHero();
				}
				input.ReadMessage(Hero);
				break;
			case 50u:
				if (cardData_ == null)
				{
					CardData = new SingleCardData();
				}
				input.ReadMessage(CardData);
				break;
			case 58u:
				if (buildingData_ == null)
				{
					BuildingData = new SingleBuildingData();
				}
				input.ReadMessage(BuildingData);
				break;
			case 85u:
				ClientGenerateUID = input.ReadSFixed32();
				break;
			case 93u:
				GameStatus = input.ReadSFixed32();
				break;
			case 98u:
				if (mapData_ == null)
				{
					MapData = new SingleMapData();
				}
				input.ReadMessage(MapData);
				break;
			case 109u:
				RoundTiming = input.ReadSFixed32();
				break;
			case 114u:
			case 117u:
				selectCardIds_.AddEntriesFrom(ref input, _repeated_selectCardIds_codec);
				break;
			case 125u:
				StageId = input.ReadSFixed32();
				break;
			case 130u:
				stageLevelId_.AddEntriesFrom(ref input, _map_stageLevelId_codec);
				break;
			}
		}
	}
}
