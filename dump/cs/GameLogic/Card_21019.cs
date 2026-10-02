using System.Linq;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class Card_21019 : Card, IChoiceEffectCard
{
	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_21019()
	{
		cardId = 21019;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowCardVailMonsterTarget(GetTargetPlayers(), cardId, 1, _Sn);
	}

	protected override bool CheckVailDistance(BattlePlayerData _self, BattlePlayerData _target)
	{
		if (_self.CharacterInst == null || _target.CharacterInst == null)
		{
			return false;
		}
		LandManager inst = SimpleSingletonProvider<LandManager>.inst;
		int id = _self.CharacterInst.standLand.Id;
		int id2 = _target.CharacterInst.standLand.Id;
		RepeatedField<int> repeatedField = config.Params;
		return inst.CheckDistance(id, id2, repeatedField[repeatedField.Count - 1] + _self.Property.CardAddDistance.Value, 0);
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

	public override bool VailStatus()
	{
		return GetTargetPlayers().Count > 0;
	}

	public override string CardDescription(long playerId)
	{
		string text = base.CardDescription(playerId);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById == null)
		{
			return text;
		}
		int value = playerDataById.Property.CardAddDistance.Value;
		if (value <= 0)
		{
			return text;
		}
		RepeatedField<int> repeatedField = config.Params;
		string oldValue = $"range={repeatedField[repeatedField.Count - 1]}";
		RepeatedField<int> repeatedField2 = config.Params;
		return text.Replace(oldValue, $"range=[color=#94FF46]{repeatedField2[repeatedField2.Count - 1] + value}[/color]");
	}
}
