using Core.Tutorial;
using Core.Tutorial.Tools;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public class Card_21004 : Card
{
	public Card_21004()
	{
		cardId = 21004;
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
		foreach (int buffId in config.BuffIds)
		{
			Buff buff = new Buff
			{
				UniqueId = UIDGenerator.NextUID(),
				BuffId = buffId,
				Source = new buff_source
				{
					S = buff_source.Types.source.Card,
					Id = cardId
				}
			};
			buff.InitBuffData(actionPlayer);
			await TutorialGame.GetSystem<TutorialBoardManager>().buffManager.AddBuff(actionPlayer, buff);
		}
		await TutorialGame.GetSystem<TutorialPlayerActionFSM>().SwitchState(PlayerActionType.Idle);
		return true;
	}
}
