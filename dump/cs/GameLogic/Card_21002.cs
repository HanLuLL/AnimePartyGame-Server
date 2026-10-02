using System.Collections.Generic;
using Core.Tutorial;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_21002 : Card
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_21002()
	{
		cardId = 21002;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.cardWindow.RefreshCardInfo_SelectPlayer(GetTargetPlayers(), 1, -1, _Sn);
	}

	protected override bool CheckVailDistance(BattlePlayerData _self, BattlePlayerData _target)
	{
		return true;
	}

	public override async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult(config, PlayerId, TargetIds, reverse);
	}

	public override void CardScope(bool state)
	{
		showPlayers = GetTargetPlayers();
		SimpleSingletonProvider<GameLogicManager>.inst.card.signal.showSelectPlayer.Dispatch(showPlayers, state);
	}

	public override bool VailStatus()
	{
		return GetTargetPlayers().Count > 0;
	}

	public override async UniTask<bool> TutorialCardEffect(long actionPlayer, UseEffectCardC2S msg)
	{
		if (!(await base.TutorialCardEffect(actionPlayer, msg)))
		{
			return false;
		}
		long safeByIndex = msg.TargetIds.GetSafeByIndex(0);
		await GiveCard(actionPlayer, safeByIndex);
		await GiveCard(actionPlayer, actionPlayer);
		await TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.Idle);
		return true;
	}

	private async UniTask GiveCard(long actionPlayer, long playerId)
	{
		List<HandCardData> collection = TutorialGame.GetSystem<TutorialBoardManager>().cardManager.TryGetCards(1);
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 && SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round == 1)
		{
			collection = ((!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(actionPlayer)) ? new List<HandCardData> { HandCardData.GetTutorialHandCardData(10001) } : new List<HandCardData> { HandCardData.GetTutorialHandCardData(20002) });
		}
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		playerDataById.cardContainer._HandCards.AddRange(collection);
		TutorialBoardCharacterManager characterManager = TutorialGame.GetSystem<TutorialBoardManager>().characterManager;
		HeroAttrEffect cardUpdate = characterManager.GetCardUpdate(playerId, playerDataById.cardContainer._CardInfos);
		await characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Card,
				Id = cardId
			},
			PlayerId = playerId,
			EffectDatas = { cardUpdate }
		});
	}
}
