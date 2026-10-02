using Core;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_21024 : Card
{
	public Card_21024()
	{
		cardId = 21024;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.cardWindow.RefreshCardInfo_SelectPlayer(GetTargetPlayers(), 1, -1, _Sn);
	}

	public override async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult(config, PlayerId, TargetIds, reverse);
	}

	protected override bool CheckVailDistance(BattlePlayerData _self, BattlePlayerData _target)
	{
		return true;
	}

	public override async UniTask CardAttrShow(UpdateHeroAttrS2C model)
	{
		int performId = config.TargetDefaultPerform;
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			if (model.PlayerId != model.EffectDatas[i].PlayerId || model.EffectDatas[i].UseCardNum == null)
			{
				await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, performId, "思维基质-卡牌牌演出-目标玩家");
				if (perform.isCancel)
				{
					break;
				}
			}
		}
	}
}
