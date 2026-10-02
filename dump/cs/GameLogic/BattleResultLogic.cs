using System.Collections.Generic;
using Core;
using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic.Replay;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class BattleResultLogic : IRPCSync
{
	public int preExp;

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GameFinishS2C.OnGameFinishS2CServerCallBackAsync = OnGameFinishS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.PraisePlayerS2C.OnPraisePlayerS2CServerCallBackAsync = OnPraisePlayerS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangePraiseNumS2C.OnChangePraiseNumS2CServerCallBackAsync = OnChangePraiseNumS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.GameFinishS2C.OnGameFinishS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.PraisePlayerS2C.OnPraisePlayerS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.ChangePraiseNumS2C.OnChangePraiseNumS2CServerCallBackAsync = null;
	}

	private async UniTask OnGameFinishS2CServerCallBack(GameFinishS2C model, int errId, bool isdispatch)
	{
		if (errId != 0 || SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING)
		{
			return;
		}
		CommonUIManager.StopAllVideo();
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		roomController.SwitchRoomState(null, RoomStateType.SETTLEMENT);
		preExp = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Exp;
		CoroutineManager.CoroutineState coroutineState = SimpleSingletonProvider<LandManager>.inst._coroutineState;
		if (coroutineState != null && coroutineState.Running)
		{
			SimpleSingletonProvider<LandManager>.inst._coroutineState.Stop();
		}
		SimpleSingletonProvider<UIManager>.inst.BattleLandTip.HideLandTips();
		ReplaySession replaySession = SimpleSingletonProvider<GameLogicManager>.inst.replay?.Session;
		if (replaySession != null && replaySession.IsReplay)
		{
			SimpleSingletonProvider<UIManager>.inst.Replay.CloseReplay();
		}
		OperationTimer.Dispose();
		RoomInfo localRoom = roomController.localRoom;
		if (localRoom != null)
		{
			if (localRoom.IsPVE())
			{
				await SimpleSingletonProvider<UIManager>.inst.upgradeWindow.ShowPVETip(model.Winer != 0);
			}
			else if (localRoom.IsAsymmetricalBattle() || localRoom.IsLuckyStarBattle())
			{
				await SimpleSingletonProvider<UIManager>.inst.upgradeWindow.ShowTeamBattleTip(model.Winer);
			}
		}
		RoomInfo localRoom2 = roomController.localRoom;
		if (localRoom2 != null && localRoom2.StoryId != 0 && StaticConfigure.Story.StoryDict.TryGetValue(localRoom2.StoryId, out var value))
		{
			StoryData storyData = await SimpleSingletonProvider<InternalAssetManager>.inst.GetStoryDataAsset(value.StoryData);
			if (storyData != null && storyData.Root != null)
			{
				await SimpleSingletonProvider<GameLogicManager>.inst.story.TryShowStory(storyData, delegate
				{
					TryShowBattleSettlement(model);
				});
				return;
			}
		}
		TryShowBattleSettlement(model);
	}

	private void TryShowBattleSettlement(GameFinishS2C model)
	{
		SimpleSingletonProvider<UIManager>.inst.CloseAllUnFightWin();
		SimpleSingletonProvider<UIManager>.inst.Fight.CloseFightWin().Forget();
		List<BattlePlayerData> playerDatas = SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas;
		BattleSettlement battleSettlement = new BattleSettlement(model, playerDatas);
		SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.BattleSettlement, battleSettlement).Forget();
	}

	public void FinishGame()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.replay.Clear();
		SimpleSingletonProvider<InternalAssetManager>.inst.Release();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady = false;
		BattleSceneController.inst?.UnLoadPreLoadCharacterResources();
		SimpleSingletonProvider<SceneManager>.inst.LoadSceneAsync(SceneType.Home, "Home").Forget();
	}

	public RPCAsyncResult RequestPraisePlayerC2S(List<long> playerIds)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.PraisePlayerC2S.PraisePlayerC2SCall(new PraisePlayerC2S
		{
			PlayerId = { (IEnumerable<long>)playerIds }
		});
	}

	private async UniTask OnPraisePlayerS2CServerCallBack(PraisePlayerS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnChangePraiseNumS2CServerCallBack(ChangePraiseNumS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			await UniTask.CompletedTask;
		}
	}
}
