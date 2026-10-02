using System.Collections.Generic;
using Core.Scene;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using party.model;

namespace Core;

public class ActionListener : SimpleSingletonProvider<ActionListener>
{
	private List<Action> actionsList;

	protected override void InstanceInit()
	{
		base.InstanceInit();
		actionsList = new List<Action>();
	}

	public async UniTask EnqueueActionsList(Action action)
	{
		if (action.NotBro)
		{
			return;
		}
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null && curRoomInfo.State == Room.Types.State.Running)
		{
			actionsList.Add(action);
			if (actionsList.Count == 1)
			{
				await ProcessNextPushRPCMsg();
			}
		}
	}

	private async UniTask ProcessNextPushRPCMsg()
	{
		await UniTask.WaitUntil(() => SimpleSingletonProvider<GameLogicManager>.inst.battle.clientFinishReady);
		if (actionsList.Count == 0)
		{
			return;
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo._finishSn.Contains(actionsList[0].Sn))
		{
			actionsList.RemoveAt(0);
			if (actionsList.Count > 0)
			{
				await ProcessNextPushRPCMsg();
			}
			return;
		}
		await DealActionCall(actionsList[0]);
		if (actionsList.Count > 0)
		{
			actionsList.RemoveAt(0);
		}
		if (actionsList.Count > 0)
		{
			await ProcessNextPushRPCMsg();
		}
	}

	private async UniTask DealActionCall(Action action)
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle || SimpleSingletonProvider<GameLogicManager>.inst.room.roomController.roomStateType != RoomStateType.RUNNING)
		{
			return;
		}
		switch (action.Id)
		{
		case 5021:
			SimpleSingletonProvider<GameLogicManager>.inst.action.DealThrowDice(action);
			break;
		case 5027:
			await SimpleSingletonProvider<GameLogicManager>.inst.action.DealMove(action);
			break;
		case 5055:
			SimpleSingletonProvider<GameLogicManager>.inst.campaign.TryShowTutorial(action.PlayerId, action.Id);
			SimpleSingletonProvider<GameLogicManager>.inst.action.DealUseEffectCard(action);
			break;
		case 5073:
			SimpleSingletonProvider<GameLogicManager>.inst.action.DealQuickCard(action);
			break;
		case 5075:
			SimpleSingletonProvider<GameLogicManager>.inst.action.DealAbandonCard(action);
			break;
		case 5059:
			SimpleSingletonProvider<GameLogicManager>.inst.action.DealBombThrowDice(action);
			break;
		case 5047:
			await SimpleSingletonProvider<GameLogicManager>.inst.fight.AskFight(action);
			break;
		case 5035:
			await SimpleSingletonProvider<GameLogicManager>.inst.fight.ReadyFightUseCard(action);
			break;
		case 5037:
			await SimpleSingletonProvider<GameLogicManager>.inst.fight.ReadyFightThrowDice(action);
			break;
		case 5039:
			await SimpleSingletonProvider<GameLogicManager>.inst.fight.ReadyFightChoice(action);
			break;
		case 5029:
			SimpleSingletonProvider<GameLogicManager>.inst.land.DealPVPCardShop(action);
			break;
		case 5215:
			SimpleSingletonProvider<GameLogicManager>.inst.campaign.TryShowTutorial(action.PlayerId, action.Id);
			SimpleSingletonProvider<GameLogicManager>.inst.land.DealPVECardShop(action);
			break;
		case 5033:
			SimpleSingletonProvider<UIManager>.inst.landPursuit.DealLand_Pursuit(action);
			break;
		case 5041:
			SimpleSingletonProvider<UIManager>.inst.landLottery.DealLand_Lottery(action);
			break;
		case 5049:
			SimpleSingletonProvider<UIManager>.inst.landRollGold.DealLand_RollGold(action);
			break;
		case 5081:
			await SimpleSingletonProvider<UIManager>.inst.landGamble.DealLand_Gamble(action);
			break;
		case 5083:
			await SimpleSingletonProvider<UIManager>.inst.landGamble.DealLand_GambleDice(action);
			break;
		case 5069:
			await SimpleSingletonProvider<UIManager>.inst.landDivination.DealLand_Divination(action);
			break;
		case 5063:
			SimpleSingletonProvider<UIManager>.inst.landBattery.DealLand_LandChoiceTarget(action);
			break;
		case 5077:
			SimpleSingletonProvider<UIManager>.inst.landFillingStation.DealLand_StopOrContinue(action);
			break;
		case 5093:
			SimpleSingletonProvider<UIManager>.inst.landHospital.DealLand_TriggerHospital(action);
			break;
		case 5053:
			SimpleSingletonProvider<GameLogicManager>.inst.land.DealLand_EventTigger(action);
			break;
		case 5071:
			SimpleSingletonProvider<UIManager>.inst.landEvent.DealLand_Destiny(action);
			break;
		case 5043:
			SimpleSingletonProvider<GameLogicManager>.inst.action.DealMoveAgain(action);
			break;
		case 5213:
			await SimpleSingletonProvider<GameLogicManager>.inst.land.DealMonsterPursuit(action);
			break;
		case 5233:
			await SimpleSingletonProvider<GameLogicManager>.inst.land.DealAskReviveTeammate(action);
			break;
		case 5249:
			await SimpleSingletonProvider<GameLogicManager>.inst.land.DealAskPurchaseRelic(action);
			break;
		case 5259:
			await SimpleSingletonProvider<GameLogicManager>.inst.land.DealAskSelectMechanism(action);
			break;
		case 5317:
			await SimpleSingletonProvider<GameLogicManager>.inst.land.DealSelectEventC2S(action);
			break;
		case 5323:
			await SimpleSingletonProvider<GameLogicManager>.inst.land.DealAskVendorBuyCard(action);
			break;
		case 5067:
			if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(action.PlayerId))
			{
				(await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCard()).RefreshCardInfo_ControlMoveCard(action);
			}
			break;
		case 5211:
			SimpleSingletonProvider<GameLogicManager>.inst.relic.DealRelic(action);
			break;
		case 5313:
			await SimpleSingletonProvider<GameLogicManager>.inst.story.OpenStoryByServer(action);
			break;
		case 5309:
			await SimpleSingletonProvider<GameLogicManager>.inst.assistVote.TryShowAssistVote(action);
			break;
		case 5377:
			await SimpleSingletonProvider<UIManager>.inst.ChooseRoundCard.ShowRoundCard(action);
			break;
		}
	}

	public void Dispose()
	{
		actionsList?.Clear();
		OnDestroyInstance();
	}
}
