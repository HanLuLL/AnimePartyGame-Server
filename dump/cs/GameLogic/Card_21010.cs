using Core;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.protocol;

namespace GameLogic;

public class Card_21010 : Card
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_21010()
	{
		cardId = 21010;
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

	public override async UniTask CardAttrShow(UpdateHeroAttrS2C model)
	{
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		for (int i = 0; i < model.EffectDatas.Count; i++)
		{
			if (model.EffectDatas[i].Hp != null)
			{
				await perform.PlayPlayerShow(model.EffectDatas[i].PlayerId, config.TargetDefaultPerform, "卡牌演出-目标玩家");
				if (!perform.isCancel)
				{
					break;
				}
				return;
			}
		}
		for (int j = 0; j < model.EffectDatas.Count; j++)
		{
			HeroGoldChangeS2C gold = model.EffectDatas[j].Gold;
			if (gold != null && gold.ChangeGold > 0)
			{
				await perform.PlayPlayerShow(model.EffectDatas[j].PlayerId, config.ReleaseDefaultPerform, "卡牌使用者");
				_ = perform.isCancel;
				break;
			}
		}
	}
}
