using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_20032 : Card
{
	public Card_20032()
	{
		cardId = 20032;
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

	public override async UniTask CardAttrShow(UpdateHeroAttrS2C model)
	{
		int targetPerformId = config.TargetDefaultPerform;
		BattlePlayerData playerDataByHeroId = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataByHeroId(114);
		if (playerDataByHeroId != null && playerDataByHeroId.player.standingPainting.ItemID == 100114005)
		{
			targetPerformId++;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		await perform.PlayPlayerShow(model.PlayerId, config.ReleaseDefaultPerform, "卡牌使用者");
		if (perform.isCancel)
		{
			return;
		}
		List<long> playerIds = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < playerIds.Count; i++)
		{
			await perform.PlayPlayerShow(playerIds[i], targetPerformId, "卡牌演出2-目标玩家");
			if (perform.isCancel)
			{
				break;
			}
		}
	}
}
