using System.Linq;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class Card_21018 : Card, IChoiceEffectCard
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_21018()
	{
		cardId = 21018;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowCardVailMonsterTarget(GetTargetPlayers(), cardId, 1, _Sn);
	}

	protected override bool CheckVailDistance(BattlePlayerData _self, BattlePlayerData _target)
	{
		if (_target.player.Hero.HeroId == 1001)
		{
			return false;
		}
		return true;
	}

	public override async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult(config, PlayerId, TargetIds, reverse);
	}

	public override async void CardScope(bool state)
	{
		showPlayers = GetTargetPlayers();
		if (showPlayers.Count == 0)
		{
			SimpleSingletonProvider<UIManager>.inst.battlePreMonster.HideImmediately();
		}
		else if (state)
		{
			await SimpleSingletonProvider<UIManager>.inst.battlePreMonster.PreviewMonsterTarget(showPlayers);
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.battlePreMonster.HideImmediately();
		}
	}

	public async UniTask ActiveEffectCardAfterChoose(int ChoiceCardId, long actionSn)
	{
		int num = ChoiceCardId.GetCardConfigure().Params.ToList().FindIndex((int x) => x == cardId);
		if (num >= 0)
		{
			await SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowCardVailMonsterTargetAfterChooseCard(GetTargetPlayers(), ChoiceCardId, num + 1, 1, actionSn);
		}
		else
		{
			Debug.LogError($"触发抉择卡{ChoiceCardId}的效果卡{cardId}，发现此效果卡并不是抉择卡的效果");
		}
	}
}
