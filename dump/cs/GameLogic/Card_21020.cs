using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.protocol;

namespace GameLogic;

public class Card_21020 : Card
{
	private readonly string effectKey;

	private readonly List<Effect> targetLandEffects = new List<Effect>();

	public Card_21020()
	{
		cardId = 21020;
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
		if (state && !(SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData().CharacterInst == null))
		{
			List<int> lands = SimpleSingletonProvider<LandManager>.inst.GetLandsByScope(config.Params[0]);
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
	}

	public override async UniTask CardAttrShow(UpdateHeroAttrS2C model)
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(model.PlayerId);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerData.CharacterInst, willMove: true);
		ActionEffectShow perform = SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform();
		HeroAttrEffect placeAttr = model.EffectDatas.FirstOrDefault((HeroAttrEffect x) => x.Place != null);
		if (placeAttr == null)
		{
			return;
		}
		await perform.PlayPlayerShow(model.PlayerId, config.PerformTarget[0], "传送 开始", ignoreDuration: false, placeAttr);
		if (!perform.isCancel)
		{
			playerData.CharacterInst.SendCharacter(placeAttr.Place.Place.NodeId, placeAttr.Place.Place.FrontNodeIds);
			await perform.PlayPlayerShow(model.PlayerId, config.PerformTarget[1], "传送 结束", ignoreDuration: false, placeAttr);
			if (!perform.isCancel)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.battle.DeployPlayers(playerData.CharacterInst, willMove: false);
				playerData.CharacterInst.ResetFromLandId(-1);
				playerData.CharacterInst.ShowWalkDirections(null);
			}
		}
	}
}
