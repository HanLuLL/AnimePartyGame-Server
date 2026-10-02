using Core.Tutorial;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_20014 : Card
{
	public Card_20014()
	{
		cardId = 20014;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.cardWindow.RequestUseEffectCard(_Sn);
	}

	public override async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult(config, PlayerId, TargetIds, reverse);
	}

	public override async UniTask<bool> TutorialCardEffect(long actionPlayer, UseEffectCardC2S msg)
	{
		if (!(await base.TutorialCardEffect(actionPlayer, msg)))
		{
			return false;
		}
		int safeByIndex = config.Params.GetSafeByIndex(0);
		HeroAttrEffect hpUpdate = TutorialGame.GetSystem<TutorialBoardManager>().characterManager.GetHpUpdate(actionPlayer, safeByIndex);
		await TutorialGame.GetSystem<TutorialBoardManager>().characterManager.CreateUpdateAttrData(new UpdateHeroAttrS2C
		{
			Cause = new CauseOrigin
			{
				S = CauseOrigin.Types.source.Card,
				Id = cardId
			},
			PlayerId = actionPlayer,
			EffectDatas = { hpUpdate }
		});
		await TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.Idle);
		return true;
	}
}
