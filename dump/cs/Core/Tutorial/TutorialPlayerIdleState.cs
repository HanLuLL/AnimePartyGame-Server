using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;

namespace Core.Tutorial;

public class TutorialPlayerIdleState : TutorialPlayerActionState
{
	public TutorialPlayerIdleState(PlayerActionType type, TutorialPlayerActionFSM fsm)
		: base(type, fsm)
	{
	}

	public override async UniTask OnEnter()
	{
		await base.OnEnter();
		BattlePlayerData actionPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_fsm.PlayerId);
		if (actionPlayer == null || actionPlayer.Property.HP.Value <= 0)
		{
			_fsm.OnActionFinished();
			return;
		}
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.PlayActionHandler(actionPlayer);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_fsm.PlayerId))
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.handCard?.ChangeSuggestMove(suggest: true);
			List<HandCardData> handCards = actionPlayer.cardContainer._HandCards;
			if (actionPlayer.Property.cardUseTimes.Value < actionPlayer.Property.CardMaxVailUseCount.Value && handCards != null && handCards.Any(delegate(HandCardData x)
			{
				EffectType effectType2 = x.Config.EffectType;
				return effectType2 != EffectType.Attack && effectType2 != EffectType.Defense && effectType2 != EffectType.Quick;
			}))
			{
				List<int> list = new List<int>();
				for (int num = 0; num < handCards.Count; num++)
				{
					EffectType effectType = handCards[num].Config.EffectType;
					if (effectType != EffectType.Attack && effectType != EffectType.Defense && effectType != EffectType.Quick && SimpleSingletonProvider<GameLogicManager>.inst.card.cardActions.TryGetValue(handCards[num].CardId, out var value) && value.VailStatus())
					{
						list.Add(handCards[num].CardId);
					}
				}
				if (list.Count > 0)
				{
					TutorialGame.GetSystem<TutorialBoardManager>().characterManager.DealUseEffectCard(_fsm.PlayerId, list);
					return;
				}
			}
			TutorialGame.GetSystem<TutorialBoardManager>().characterManager.DealThrowDice(_fsm.PlayerId);
		}
		else
		{
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.RequestThrowDiceC2S(_fsm.PlayerId);
		}
	}
}
