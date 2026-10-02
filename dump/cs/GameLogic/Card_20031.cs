using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public class Card_20031 : Card
{
	private const int Threshold = 5;

	private RepeatedField<long> showPlayers = new RepeatedField<long>();

	public Card_20031()
	{
		cardId = 20031;
		config = cardId.GetCardConfigure();
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		SimpleSingletonProvider<UIManager>.inst.battleSelectMonster.ShowCardVailMonsterTarget(GetTargetPlayers(), cardId, config.Params[1], _Sn);
	}

	protected override bool CheckVailDistance(BattlePlayerData _self, BattlePlayerData _target)
	{
		if (_self.CharacterInst == null || _target.CharacterInst == null)
		{
			return false;
		}
		return SimpleSingletonProvider<LandManager>.inst.CheckDistance(_self.CharacterInst.standLand.Id, _target.CharacterInst.standLand.Id, config.Params[0] + _self.Property.CardAddDistance.Value, 0);
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

	public override string CardDescription(long playerId)
	{
		string text = base.CardDescription(playerId);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById == null)
		{
			return text;
		}
		int value = playerDataById.Property.CardAddDistance.Value;
		string result = text;
		if (value > 0)
		{
			result = text.Replace($"range={config.Params[0]}", $"range=[color=#94FF46]{config.Params[0] + value}[/color]");
		}
		return result;
	}

	public override async UniTask CardAttrShow(UpdateHeroAttrS2C model)
	{
		Buff buff = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId).buffContainer.GetBuff(1211201);
		int preformId = ((buff != null && buff.Progress > 5) ? config.PerformTarget[1] : config.PerformTarget[0]);
		BattlePlayerData playerDataByHeroId = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataByHeroId(121);
		if (playerDataByHeroId != null && playerDataByHeroId.player.standingPainting.ItemID == 100121004)
		{
			preformId++;
		}
		else if (playerDataByHeroId != null && playerDataByHeroId.player.standingPainting.ItemID == 100121005)
		{
			preformId += 2;
		}
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		List<long> _list = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetChangeAttrPlayerIds();
		for (int i = 0; i < _list.Count; i++)
		{
			if (model.PlayerId != _list[i])
			{
				await perform.PlayPlayerShow(_list[i], preformId, "卡牌演出2-目标玩家");
				if (perform.isCancel)
				{
					break;
				}
			}
		}
	}
}
