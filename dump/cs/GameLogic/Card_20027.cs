using Core;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class Card_20027 : Card
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_20027()
	{
		cardId = 20027;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.cardWindow.RefreshCardInfo_SelectPlayer(GetTargetPlayers(), 1, 0, _Sn);
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

	public override async UniTask CardAttrShow(UpdateHeroAttrS2C model)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		int performId = config.TargetDefaultPerform;
		if (playerDataById.player.Hero.HeroId == 123 && playerDataById.player.standingPainting.ItemID == 100123004)
		{
			playerDataById.Property.cardUseData.TryGetValue(cardId, out var value);
			if (value > 2)
			{
				performId = ((value < 5) ? 2002702 : 2002703);
			}
			else if (value > 0)
			{
				performId = 2002701;
			}
			else
			{
				Debug.LogError("流程错误！ 当前使用卡牌数为0");
			}
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			if (model.PlayerId != model.EffectDatas[i].PlayerId || model.EffectDatas[i].UseCardNum == null)
			{
				await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, performId, "福卡-卡牌牌演出2-目标玩家");
				if (perform.isCancel)
				{
					break;
				}
			}
		}
	}
}
