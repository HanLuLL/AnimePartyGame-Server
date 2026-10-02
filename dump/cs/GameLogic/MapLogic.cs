using Core.Net;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class MapLogic : IRPCSync
{
	public void ShowMapDetail(MapModeType mapModeType, MapInfoConfigure info)
	{
		switch (mapModeType)
		{
		case MapModeType.Standard:
			SimpleSingletonProvider<UIManager>.inst.explain.ShowPVPTip(1).Forget();
			break;
		case MapModeType.Pve:
		case MapModeType.LuckyStarBattle:
		case MapModeType.MutatorPve:
			SimpleSingletonProvider<UIManager>.inst.explain.ShowPVETutorial(info.Id).Forget();
			break;
		case MapModeType.AsymmetricalBattle:
			SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(1002).Forget();
			break;
		}
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.MapStatusChangeS2C.OnMapStatusChangeS2CServerCallBackAsync = OnMapStatusChangeS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MapIndexChangeS2C.OnMapIndexChangeS2CServerCallBackAsync = OnMapIndexChangeS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.MapStatusChangeS2C.OnMapStatusChangeS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MapIndexChangeS2C.OnMapIndexChangeS2CServerCallBackAsync = null;
	}

	private async UniTask OnMapStatusChangeS2CServerCallBack(MapStatusChangeS2C model, int errid, bool isdispatch)
	{
		if (errid != 0 || SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle)
		{
			return;
		}
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		if (roomController == null || roomController.roomStateType != RoomStateType.RUNNING)
		{
			return;
		}
		if (model.MapId != roomController.localRoom.MapId)
		{
			Debug.LogError($"Current MapId:{roomController.localRoom.MapId}, Server MapId:{model.MapId}, throw error");
			return;
		}
		MapGimmickManager mapGimmickManager = SimpleSingletonProvider<LandManager>.inst.MapGimmickManager;
		if (mapGimmickManager == null)
		{
			Debug.LogError($"MapId:{model.MapId}, hasn't been found MapGimmickManager Component, please check");
		}
		else
		{
			await mapGimmickManager.SwitchGimmick(model.GroupId, model.NewStatus);
		}
	}

	private async UniTask OnMapIndexChangeS2CServerCallBack(MapIndexChangeS2C model, int errid, bool isdispatch)
	{
		if (errid == 0 && SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
		{
			RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
			if (roomController != null && roomController.RoomValid && roomController.roomStateType == RoomStateType.RUNNING)
			{
				roomController.localRoom.UpdateMapId(model.MapId, model.NewIndex);
				await MapSceneChange();
			}
		}
	}

	public async UniTask MapSceneChange()
	{
		await SimpleSingletonProvider<UIManager>.inst.loading.CutIn();
		foreach (BattlePlayerData playerData in SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayerDatas)
		{
			playerData.Dispose();
		}
		RoomController roomController = SimpleSingletonProvider<GameLogicManager>.inst.room.roomController;
		await SimpleSingletonProvider<SceneManager>.inst.PreLoadBattleSceneAsync(roomController.localRoom);
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleStart(initialization: false);
		await SimpleSingletonProvider<UIManager>.inst.loading.CutOut();
	}
}
