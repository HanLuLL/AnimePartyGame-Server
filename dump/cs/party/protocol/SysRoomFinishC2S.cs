using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using party.model;

namespace party.protocol;

public sealed class SysRoomFinishC2S : IMessage<SysRoomFinishC2S>, IMessage, IEquatable<SysRoomFinishC2S>, IDeepCloneable<SysRoomFinishC2S>, IBufferMessage
{
	private static readonly MessageParser<SysRoomFinishC2S> _parser = new MessageParser<SysRoomFinishC2S>(() => new SysRoomFinishC2S());

	private UnknownFieldSet _unknownFields;

	public const int PlayerIdFieldNumber = 1;

	private long playerId_;

	public const int MapIdFieldNumber = 2;

	private int mapId_;

	public const int HeroIdFieldNumber = 3;

	private int heroId_;

	public const int MapTypeFieldNumber = 4;

	private int mapType_;

	public const int IsWinFieldNumber = 5;

	private bool isWin_;

	public const int DifficultyFieldNumber = 6;

	private int difficulty_;

	public const int ProgressFieldNumber = 7;

	private int progress_;

	public const int TotalPlayerDieFieldNumber = 8;

	private int totalPlayerDie_;

	public const int CondFieldNumber = 10;

	private static readonly MapField<int, int>.Codec _map_cond_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 82u);

	private readonly MapField<int, int> cond_ = new MapField<int, int>();

	public const int RoundFieldNumber = 11;

	private int round_;

	public const int GoldRelicNumFieldNumber = 12;

	private int goldRelicNum_;

	public const int RankFieldNumber = 13;

	private int rank_;

	public const int KillMonstersFieldNumber = 14;

	private static readonly MapField<int, int>.Codec _map_killMonsters_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 114u);

	private readonly MapField<int, int> killMonsters_ = new MapField<int, int>();

	public const int MissionCondFieldNumber = 15;

	private static readonly MapField<int, int>.Codec _map_missionCond_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 122u);

	private readonly MapField<int, int> missionCond_ = new MapField<int, int>();

	public const int TeammateKillMonstersFieldNumber = 16;

	private static readonly MapField<int, int>.Codec _map_teammateKillMonsters_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 130u);

	private readonly MapField<int, int> teammateKillMonsters_ = new MapField<int, int>();

	public const int IsMatchRoomFieldNumber = 17;

	private bool isMatchRoom_;

	public const int RoomPlayerIdsFieldNumber = 18;

	private static readonly MapField<long, long>.Codec _map_roomPlayerIds_codec = new MapField<long, long>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForSFixed64(17u, 0L), 146u);

	private readonly MapField<long, long> roomPlayerIds_ = new MapField<long, long>();

	public const int TeammateStarCoinFieldNumber = 19;

	private static readonly MapField<int, int>.Codec _map_teammateStarCoin_codec = new MapField<int, int>.Codec(FieldCodec.ForSFixed32(13u, 0), FieldCodec.ForSFixed32(21u, 0), 154u);

	private readonly MapField<int, int> teammateStarCoin_ = new MapField<int, int>();

	public const int KillPlayersFieldNumber = 20;

	private static readonly MapField<long, int>.Codec _map_killPlayers_codec = new MapField<long, int>.Codec(FieldCodec.ForSFixed64(9u, 0L), FieldCodec.ForSFixed32(21u, 0), 162u);

	private readonly MapField<long, int> killPlayers_ = new MapField<long, int>();

	public const int IsLuckyStarRoomFieldNumber = 21;

	private bool isLuckyStarRoom_;

	public const int GameStatsFieldNumber = 22;

	private GameStats gameStats_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<SysRoomFinishC2S> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[483];

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
	public int MapId
	{
		get
		{
			return mapId_;
		}
		set
		{
			mapId_ = value;
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
	public int MapType
	{
		get
		{
			return mapType_;
		}
		set
		{
			mapType_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsWin
	{
		get
		{
			return isWin_;
		}
		set
		{
			isWin_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Difficulty
	{
		get
		{
			return difficulty_;
		}
		set
		{
			difficulty_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Progress
	{
		get
		{
			return progress_;
		}
		set
		{
			progress_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int TotalPlayerDie
	{
		get
		{
			return totalPlayerDie_;
		}
		set
		{
			totalPlayerDie_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> Cond => cond_;

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
	public int GoldRelicNum
	{
		get
		{
			return goldRelicNum_;
		}
		set
		{
			goldRelicNum_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int Rank
	{
		get
		{
			return rank_;
		}
		set
		{
			rank_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> KillMonsters => killMonsters_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> MissionCond => missionCond_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> TeammateKillMonsters => teammateKillMonsters_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsMatchRoom
	{
		get
		{
			return isMatchRoom_;
		}
		set
		{
			isMatchRoom_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, long> RoomPlayerIds => roomPlayerIds_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<int, int> TeammateStarCoin => teammateStarCoin_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapField<long, int> KillPlayers => killPlayers_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool IsLuckyStarRoom
	{
		get
		{
			return isLuckyStarRoom_;
		}
		set
		{
			isLuckyStarRoom_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameStats GameStats
	{
		get
		{
			return gameStats_;
		}
		set
		{
			gameStats_ = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysRoomFinishC2S()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysRoomFinishC2S(SysRoomFinishC2S other)
		: this()
	{
		playerId_ = other.playerId_;
		mapId_ = other.mapId_;
		heroId_ = other.heroId_;
		mapType_ = other.mapType_;
		isWin_ = other.isWin_;
		difficulty_ = other.difficulty_;
		progress_ = other.progress_;
		totalPlayerDie_ = other.totalPlayerDie_;
		cond_ = other.cond_.Clone();
		round_ = other.round_;
		goldRelicNum_ = other.goldRelicNum_;
		rank_ = other.rank_;
		killMonsters_ = other.killMonsters_.Clone();
		missionCond_ = other.missionCond_.Clone();
		teammateKillMonsters_ = other.teammateKillMonsters_.Clone();
		isMatchRoom_ = other.isMatchRoom_;
		roomPlayerIds_ = other.roomPlayerIds_.Clone();
		teammateStarCoin_ = other.teammateStarCoin_.Clone();
		killPlayers_ = other.killPlayers_.Clone();
		isLuckyStarRoom_ = other.isLuckyStarRoom_;
		gameStats_ = ((other.gameStats_ != null) ? other.gameStats_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysRoomFinishC2S Clone()
	{
		return new SysRoomFinishC2S(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SysRoomFinishC2S);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(SysRoomFinishC2S other)
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
		if (MapId != other.MapId)
		{
			return false;
		}
		if (HeroId != other.HeroId)
		{
			return false;
		}
		if (MapType != other.MapType)
		{
			return false;
		}
		if (IsWin != other.IsWin)
		{
			return false;
		}
		if (Difficulty != other.Difficulty)
		{
			return false;
		}
		if (Progress != other.Progress)
		{
			return false;
		}
		if (TotalPlayerDie != other.TotalPlayerDie)
		{
			return false;
		}
		if (!Cond.Equals(other.Cond))
		{
			return false;
		}
		if (Round != other.Round)
		{
			return false;
		}
		if (GoldRelicNum != other.GoldRelicNum)
		{
			return false;
		}
		if (Rank != other.Rank)
		{
			return false;
		}
		if (!KillMonsters.Equals(other.KillMonsters))
		{
			return false;
		}
		if (!MissionCond.Equals(other.MissionCond))
		{
			return false;
		}
		if (!TeammateKillMonsters.Equals(other.TeammateKillMonsters))
		{
			return false;
		}
		if (IsMatchRoom != other.IsMatchRoom)
		{
			return false;
		}
		if (!RoomPlayerIds.Equals(other.RoomPlayerIds))
		{
			return false;
		}
		if (!TeammateStarCoin.Equals(other.TeammateStarCoin))
		{
			return false;
		}
		if (!KillPlayers.Equals(other.KillPlayers))
		{
			return false;
		}
		if (IsLuckyStarRoom != other.IsLuckyStarRoom)
		{
			return false;
		}
		if (!object.Equals(GameStats, other.GameStats))
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
		if (MapId != 0)
		{
			num ^= MapId.GetHashCode();
		}
		if (HeroId != 0)
		{
			num ^= HeroId.GetHashCode();
		}
		if (MapType != 0)
		{
			num ^= MapType.GetHashCode();
		}
		if (IsWin)
		{
			num ^= IsWin.GetHashCode();
		}
		if (Difficulty != 0)
		{
			num ^= Difficulty.GetHashCode();
		}
		if (Progress != 0)
		{
			num ^= Progress.GetHashCode();
		}
		if (TotalPlayerDie != 0)
		{
			num ^= TotalPlayerDie.GetHashCode();
		}
		num ^= Cond.GetHashCode();
		if (Round != 0)
		{
			num ^= Round.GetHashCode();
		}
		if (GoldRelicNum != 0)
		{
			num ^= GoldRelicNum.GetHashCode();
		}
		if (Rank != 0)
		{
			num ^= Rank.GetHashCode();
		}
		num ^= KillMonsters.GetHashCode();
		num ^= MissionCond.GetHashCode();
		num ^= TeammateKillMonsters.GetHashCode();
		if (IsMatchRoom)
		{
			num ^= IsMatchRoom.GetHashCode();
		}
		num ^= RoomPlayerIds.GetHashCode();
		num ^= TeammateStarCoin.GetHashCode();
		num ^= KillPlayers.GetHashCode();
		if (IsLuckyStarRoom)
		{
			num ^= IsLuckyStarRoom.GetHashCode();
		}
		if (gameStats_ != null)
		{
			num ^= GameStats.GetHashCode();
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
		if (MapId != 0)
		{
			output.WriteRawTag(21);
			output.WriteSFixed32(MapId);
		}
		if (HeroId != 0)
		{
			output.WriteRawTag(29);
			output.WriteSFixed32(HeroId);
		}
		if (MapType != 0)
		{
			output.WriteRawTag(37);
			output.WriteSFixed32(MapType);
		}
		if (IsWin)
		{
			output.WriteRawTag(40);
			output.WriteBool(IsWin);
		}
		if (Difficulty != 0)
		{
			output.WriteRawTag(53);
			output.WriteSFixed32(Difficulty);
		}
		if (Progress != 0)
		{
			output.WriteRawTag(61);
			output.WriteSFixed32(Progress);
		}
		if (TotalPlayerDie != 0)
		{
			output.WriteRawTag(69);
			output.WriteSFixed32(TotalPlayerDie);
		}
		cond_.WriteTo(ref output, _map_cond_codec);
		if (Round != 0)
		{
			output.WriteRawTag(93);
			output.WriteSFixed32(Round);
		}
		if (GoldRelicNum != 0)
		{
			output.WriteRawTag(101);
			output.WriteSFixed32(GoldRelicNum);
		}
		if (Rank != 0)
		{
			output.WriteRawTag(104);
			output.WriteInt32(Rank);
		}
		killMonsters_.WriteTo(ref output, _map_killMonsters_codec);
		missionCond_.WriteTo(ref output, _map_missionCond_codec);
		teammateKillMonsters_.WriteTo(ref output, _map_teammateKillMonsters_codec);
		if (IsMatchRoom)
		{
			output.WriteRawTag(136, 1);
			output.WriteBool(IsMatchRoom);
		}
		roomPlayerIds_.WriteTo(ref output, _map_roomPlayerIds_codec);
		teammateStarCoin_.WriteTo(ref output, _map_teammateStarCoin_codec);
		killPlayers_.WriteTo(ref output, _map_killPlayers_codec);
		if (IsLuckyStarRoom)
		{
			output.WriteRawTag(168, 1);
			output.WriteBool(IsLuckyStarRoom);
		}
		if (gameStats_ != null)
		{
			output.WriteRawTag(178, 1);
			output.WriteMessage(GameStats);
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
		if (MapId != 0)
		{
			num += 5;
		}
		if (HeroId != 0)
		{
			num += 5;
		}
		if (MapType != 0)
		{
			num += 5;
		}
		if (IsWin)
		{
			num += 2;
		}
		if (Difficulty != 0)
		{
			num += 5;
		}
		if (Progress != 0)
		{
			num += 5;
		}
		if (TotalPlayerDie != 0)
		{
			num += 5;
		}
		num += cond_.CalculateSize(_map_cond_codec);
		if (Round != 0)
		{
			num += 5;
		}
		if (GoldRelicNum != 0)
		{
			num += 5;
		}
		if (Rank != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Rank);
		}
		num += killMonsters_.CalculateSize(_map_killMonsters_codec);
		num += missionCond_.CalculateSize(_map_missionCond_codec);
		num += teammateKillMonsters_.CalculateSize(_map_teammateKillMonsters_codec);
		if (IsMatchRoom)
		{
			num += 3;
		}
		num += roomPlayerIds_.CalculateSize(_map_roomPlayerIds_codec);
		num += teammateStarCoin_.CalculateSize(_map_teammateStarCoin_codec);
		num += killPlayers_.CalculateSize(_map_killPlayers_codec);
		if (IsLuckyStarRoom)
		{
			num += 3;
		}
		if (gameStats_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GameStats);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(SysRoomFinishC2S other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PlayerId != 0L)
		{
			PlayerId = other.PlayerId;
		}
		if (other.MapId != 0)
		{
			MapId = other.MapId;
		}
		if (other.HeroId != 0)
		{
			HeroId = other.HeroId;
		}
		if (other.MapType != 0)
		{
			MapType = other.MapType;
		}
		if (other.IsWin)
		{
			IsWin = other.IsWin;
		}
		if (other.Difficulty != 0)
		{
			Difficulty = other.Difficulty;
		}
		if (other.Progress != 0)
		{
			Progress = other.Progress;
		}
		if (other.TotalPlayerDie != 0)
		{
			TotalPlayerDie = other.TotalPlayerDie;
		}
		cond_.MergeFrom(other.cond_);
		if (other.Round != 0)
		{
			Round = other.Round;
		}
		if (other.GoldRelicNum != 0)
		{
			GoldRelicNum = other.GoldRelicNum;
		}
		if (other.Rank != 0)
		{
			Rank = other.Rank;
		}
		killMonsters_.MergeFrom(other.killMonsters_);
		missionCond_.MergeFrom(other.missionCond_);
		teammateKillMonsters_.MergeFrom(other.teammateKillMonsters_);
		if (other.IsMatchRoom)
		{
			IsMatchRoom = other.IsMatchRoom;
		}
		roomPlayerIds_.MergeFrom(other.roomPlayerIds_);
		teammateStarCoin_.MergeFrom(other.teammateStarCoin_);
		killPlayers_.MergeFrom(other.killPlayers_);
		if (other.IsLuckyStarRoom)
		{
			IsLuckyStarRoom = other.IsLuckyStarRoom;
		}
		if (other.gameStats_ != null)
		{
			if (gameStats_ == null)
			{
				GameStats = new GameStats();
			}
			GameStats.MergeFrom(other.GameStats);
		}
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
			case 9u:
				PlayerId = input.ReadSFixed64();
				break;
			case 21u:
				MapId = input.ReadSFixed32();
				break;
			case 29u:
				HeroId = input.ReadSFixed32();
				break;
			case 37u:
				MapType = input.ReadSFixed32();
				break;
			case 40u:
				IsWin = input.ReadBool();
				break;
			case 53u:
				Difficulty = input.ReadSFixed32();
				break;
			case 61u:
				Progress = input.ReadSFixed32();
				break;
			case 69u:
				TotalPlayerDie = input.ReadSFixed32();
				break;
			case 82u:
				cond_.AddEntriesFrom(ref input, _map_cond_codec);
				break;
			case 93u:
				Round = input.ReadSFixed32();
				break;
			case 101u:
				GoldRelicNum = input.ReadSFixed32();
				break;
			case 104u:
				Rank = input.ReadInt32();
				break;
			case 114u:
				killMonsters_.AddEntriesFrom(ref input, _map_killMonsters_codec);
				break;
			case 122u:
				missionCond_.AddEntriesFrom(ref input, _map_missionCond_codec);
				break;
			case 130u:
				teammateKillMonsters_.AddEntriesFrom(ref input, _map_teammateKillMonsters_codec);
				break;
			case 136u:
				IsMatchRoom = input.ReadBool();
				break;
			case 146u:
				roomPlayerIds_.AddEntriesFrom(ref input, _map_roomPlayerIds_codec);
				break;
			case 154u:
				teammateStarCoin_.AddEntriesFrom(ref input, _map_teammateStarCoin_codec);
				break;
			case 162u:
				killPlayers_.AddEntriesFrom(ref input, _map_killPlayers_codec);
				break;
			case 168u:
				IsLuckyStarRoom = input.ReadBool();
				break;
			case 178u:
				if (gameStats_ == null)
				{
					GameStats = new GameStats();
				}
				input.ReadMessage(GameStats);
				break;
			}
		}
	}
}
