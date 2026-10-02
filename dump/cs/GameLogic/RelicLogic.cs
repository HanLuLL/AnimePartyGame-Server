using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public class RelicLogic : IRPCSync
{
	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SelectRelicS2C.OnSelectRelicS2CServerCallBackAsync = OnSelectRelicS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SyncRelicsS2C.OnSyncRelicsS2CServerCallBackAsync = OnSyncRelicsS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SelectRelicS2C.OnSelectRelicS2CServerCallBackAsync = null;
	}

	public void RequestSelectRelicC2S(long _sn, int index)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		MonoSingletonProvider<NetManager>.inst.RPC.SelectRelicC2S.SelectRelicC2SCall(new SelectRelicC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			Idx = index,
			IsReroll = false
		});
	}

	public void RequestResetRelic(long _sn)
	{
		OperationTimer.CancelOperatTimer(_sn);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.RecordFinishSn(_sn);
		MonoSingletonProvider<NetManager>.inst.RPC.SelectRelicC2S.SelectRelicC2SCall(new SelectRelicC2S
		{
			Info = new ActionInfo
			{
				Sn = _sn,
				UseTime = OperationTimer.GetExtraTime()
			},
			IsReroll = true
		});
	}

	private async UniTask OnSelectRelicS2CServerCallBack(SelectRelicS2C model, int errid, bool isdispatch)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING && errid == 0 && !model.IsReroll)
		{
			if (model.RelicId != 0)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId).UpdateSelectedRelic(model.RelicId);
			}
			if (SimpleSingletonProvider<UIManager>.inst.relic.isShowing && SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(model.PlayerId))
			{
				SimpleSingletonProvider<UIManager>.inst.relic.Hide();
			}
			SimpleSingletonProvider<UIManager>.inst.tips.HideMultiplePlayerThink(model.PlayerId, 11016);
			await UniTask.CompletedTask;
		}
	}

	private async UniTask OnSyncRelicsS2CServerCallBack(SyncRelicsS2C model, int errId, bool isDispatch)
	{
		if (errId == 0 && SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle && SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType == RoomStateType.RUNNING)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
			if (playerDataById != null)
			{
				playerDataById.player.UpdateSelectedRelic(model.SelectRelics);
				await UniTask.CompletedTask;
			}
		}
	}

	public async void DealRelic(Action action)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(action.PlayerId);
		if (playerDataById.CharacterInst != null)
		{
			await playerDataById.CharacterInst.SwitchCamera();
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
		{
			SelectRelicC2S relicData = ByteBuf.ReadObject<SelectRelicC2S>(action.Data.ToByteArray());
			SimpleSingletonProvider<UIManager>.inst.relic.ShowRelicData(action, relicData);
		}
		else
		{
			await SimpleSingletonProvider<UIManager>.inst.tips.ShowMultiplePlayerThink(action.PlayerId, 11016);
		}
	}
}
