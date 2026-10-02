using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class Card_20033 : Card
{
	private readonly List<Effect> targetLandEffects = new List<Effect>();

	private readonly string effectKey;

	public Card_20033()
	{
		cardId = 20033;
		config = cardId.GetCardConfigure();
		effectKey = 29.GetEffectDataConfigure().EffectName;
	}

	public override async UniTask CardAction(long _Sn)
	{
		await base.CardAction(_Sn);
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		SimpleSingletonProvider<UIManager>.inst.cardWindow.RefreshCardInfo_SelectLand(1, config.Params[0] + selfPlayerData.Property.CardAddDistance.Value, _Sn);
	}

	public override async UniTask CardCallBack(long PlayerId, RepeatedField<long> TargetIds, bool reverse, int OriginalCardId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult(config, PlayerId, TargetIds, reverse);
	}

	public override async void CardScope(bool state)
	{
		DeleteLandEffect();
		if (!state)
		{
			return;
		}
		BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
		if (!(selfPlayerData.CharacterInst == null))
		{
			List<int> lands = SimpleSingletonProvider<LandManager>.inst.GetLandsByScope(config.Params[0] + selfPlayerData.Property.CardAddDistance.Value);
			for (int i = 0; i < lands.Count; i++)
			{
				UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(lands[i]);
				Effect item = await SimpleSingletonProvider<EffectManager>.inst.PlayByName(effectKey, Vector3.zero, Quaternion.identity, landById.transform);
				targetLandEffects.Add(item);
			}
		}
	}

	private void DeleteLandEffect()
	{
		if (targetLandEffects.Count == 0)
		{
			return;
		}
		for (int i = 0; i < targetLandEffects.Count; i++)
		{
			if (targetLandEffects[i] != null)
			{
				targetLandEffects[i].ReleaseEffect();
			}
		}
		targetLandEffects.Clear();
	}

	public override async UniTask ActiveSummon(long playerId)
	{
		await (await SimpleSingletonProvider<UIManager>.inst.cardWindow.ShowCardResult()).RefreshResult(config, playerId, null, reverse: false);
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		if (playerDataById != null && playerDataById.CharacterInst != null)
		{
			await playerDataById.CharacterInst.StopMove();
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
		if (value <= 0)
		{
			return text;
		}
		return text.Replace($"range={config.Params[0]}", $"range=[color=#94FF46]{config.Params[0] + value}[/color]");
	}
}
