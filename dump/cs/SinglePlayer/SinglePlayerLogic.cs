using System.Collections.Generic;
using Core;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using SinglePlayer.GamePlay;
using Tools;
using UnityEngine;
using party.model;
using party.protocol;

namespace SinglePlayer;

public class SinglePlayerLogic : IRPCSync
{
	private SingleGameData _serverGameData;

	public SinglePlayerLevelSelectInfo PlayerSelectInfoOnGameRestart = new SinglePlayerLevelSelectInfo();

	private const int REQUEST_CD_SECONDS = 60;

	private float _lastRequestFriendScoreTimeSeconds;

	public SingleGameData ServerGameData => _serverGameData;

	public bool IsReStartGame { get; private set; }

	public int HistoryScoreBase { get; private set; }

	public int HistoryRankBase { get; private set; }

	public Dictionary<int, int> MaxIdPassedDict { get; private set; } = new Dictionary<int, int>();

	public async UniTask EnterSinglePlayer()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
		{
			return;
		}
		if (_lastRequestFriendScoreTimeSeconds == 0f || Time.time - _lastRequestFriendScoreTimeSeconds >= 60f)
		{
			_lastRequestFriendScoreTimeSeconds = Time.time;
			await SimpleSingletonProvider<GameLogicManager>.inst.friend.RequestFriendListC2S();
		}
		RPCAsyncResult rPCAsyncResult = RequestSingleGameDataC2S();
		rPCAsyncResult.OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (result.errId == 0)
			{
				SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.SinglePlayer, "SinglePlayer");
				CacheHistoryBase();
			}
		});
		await rPCAsyncResult;
	}

	public void SetMaxIdPassedDict(Dictionary<int, int> dic)
	{
		MaxIdPassedDict.Clear();
		foreach (KeyValuePair<int, int> item in dic)
		{
			MaxIdPassedDict.Add(item.Key, item.Value);
		}
	}

	public void CacheHistoryBase()
	{
		HistoryRankBase = CalcHistoryRankBase();
	}

	private int CalcHistoryRankBase()
	{
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		if (playerInfo == null)
		{
			return 0;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.friend.friendScoresList == null || SimpleSingletonProvider<GameLogicManager>.inst.friend.friendScoresList.Count == 0)
		{
			return 0;
		}
		int num = 0;
		bool flag = false;
		for (int i = 0; i < SimpleSingletonProvider<GameLogicManager>.inst.friend.friendScoresList.Count; i++)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.friend.friendScoresList[i].playerId == playerInfo.Id)
			{
				num = SimpleSingletonProvider<GameLogicManager>.inst.friend.friendScoresList[i].Score;
				flag = true;
				HistoryScoreBase = num;
				break;
			}
		}
		if (!flag)
		{
			return 0;
		}
		int num2 = 1;
		foreach (FriendLeaderboardData friendScores in SimpleSingletonProvider<GameLogicManager>.inst.friend.friendScoresList)
		{
			if (friendScores.Score > num)
			{
				num2++;
			}
		}
		return num2;
	}

	public void ResetSinglePlayer()
	{
		_serverGameData = null;
		IsReStartGame = true;
		CacheHistoryBase();
		MonoSingletonProvider<NetManager>.inst.RPC.SyncSingleGameDataC2S.SyncSingleGameDataC2SCall(new SyncSingleGameDataC2S());
		SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.SinglePlayer, "SinglePlayer");
	}

	public void FinishRestartGame()
	{
		IsReStartGame = false;
	}

	public void ExitSinglePlayer()
	{
		IsReStartGame = false;
		SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.Home, "Home");
	}

	public void BackSinglePlayerStartPanel(bool clearData)
	{
		IsReStartGame = false;
		if (clearData)
		{
			_serverGameData = null;
			MonoSingletonProvider<NetManager>.inst.RPC.SyncSingleGameDataC2S.SyncSingleGameDataC2SCall(new SyncSingleGameDataC2S());
		}
		SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.Home, "SinglePlayer");
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SingleGameDataS2C.OnSingleGameDataS2CServerCallBackAsync = OnSingleGameDataS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.SyncSingleGameDataS2C.OnSyncSingleGameDataS2CServerCallBackAsync = OnSyncSingleGameDataS2CServerCallBackAsync;
		MonoSingletonProvider<NetManager>.inst.RPC.SingleGameScoreChangeS2C.OnSingleGameScoreChangeS2CServerCallBackAsync = OnSingleGameScoreChangeS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SingleGameDataS2C.OnSingleGameDataS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SyncSingleGameDataS2C.OnSyncSingleGameDataS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.SingleGameScoreChangeS2C.OnSingleGameScoreChangeS2CServerCallBackAsync = null;
	}

	private RPCAsyncResult RequestSingleGameDataC2S()
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.SingleGameDataC2S.SingleGameDataC2SCall(new SingleGameDataC2S());
	}

	private async UniTask OnSingleGameDataS2CServerCallBackAsync(SingleGameDataS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && model.SingleGameInfo.GameStatus == 0)
		{
			_serverGameData = model.SingleGameInfo;
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestSyncSingleGameDataC2S(GameStatus status, SingleGameData singleGameData)
	{
		if (IsReStartGame)
		{
			return null;
		}
		int levelId = singleGameData.LevelId;
		int stageId = singleGameData.StageId;
		SyncSingleGameDataC2S syncSingleGameDataC2S = new SyncSingleGameDataC2S
		{
			LevelId = ((status == GameStatus.Victory) ? levelId : 0),
			StageId = ((status == GameStatus.Victory) ? stageId : 0),
			SingleGameInfo = singleGameData,
			Score = ((status != GameStatus.Running) ? Game.GetModel<GameData>().heroProperty.CalculateScore() : 0)
		};
		foreach (KeyValuePair<int, int> item in singleGameData.StageLevelId)
		{
			syncSingleGameDataC2S.StageLevelId[item.Key] = item.Value;
		}
		_serverGameData = singleGameData;
		return MonoSingletonProvider<NetManager>.inst.RPC.SyncSingleGameDataC2S.SyncSingleGameDataC2SCall(syncSingleGameDataC2S);
	}

	private async UniTask OnSyncSingleGameDataS2CServerCallBackAsync(SyncSingleGameDataS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnSingleGameScoreChangeS2CServerCallBackAsync(SingleGameScoreChangeS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			return;
		}
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		if (playerInfo != null)
		{
			Player player = playerInfo;
			if (player.SingleInfo == null)
			{
				player.SingleInfo = new SingleInfo();
			}
			playerInfo.SingleInfo.MaxScore = model.MaxScore;
			await UniTask.CompletedTask;
		}
	}

	public void UploadLogData(GameStatus status)
	{
		GameData model = Game.GetModel<GameData>();
		int value = model.heroProperty.CalculateScore();
		int value2 = model.GameProgress.Value;
		SimpleSingletonProvider<WebServerManager>.inst.PostSinglePlayer(SinglePlayerLogType.End, status == GameStatus.Victory, value, value2);
	}

	public void UploadLogData(SinglePlayerLogType type)
	{
		SimpleSingletonProvider<WebServerManager>.inst.PostSinglePlayer(type);
	}
}
