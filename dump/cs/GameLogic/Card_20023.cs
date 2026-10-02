using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;

namespace GameLogic;

public class Card_20023 : Card
{
	private readonly List<Effect> targetLandEffects = new List<Effect>();

	private readonly string effectKey;

	public Card_20023()
	{
		cardId = 20023;
		config = cardId.GetCardConfigure();
		effectKey = 29.GetEffectDataConfigure().EffectName;
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

	public override async void CardScope(bool state)
	{
		DeleteLandEffect();
		if (state)
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
}
