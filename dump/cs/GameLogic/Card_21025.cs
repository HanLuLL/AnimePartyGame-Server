using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_21025 : Card
{
	public Card_21025()
	{
		cardId = 21025;
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
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		List<long> changeAttrPlayerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		foreach (long item in changeAttrPlayerIds)
		{
			int performId = ((model.PlayerId == item) ? config.ReleaseDefaultPerform : config.TargetDefaultPerform);
			await perform.PlayPlayerShow(item, performId, "罪证嫌疑-卡牌牌演出");
			if (perform.isCancel)
			{
				return;
			}
		}
	}
}
