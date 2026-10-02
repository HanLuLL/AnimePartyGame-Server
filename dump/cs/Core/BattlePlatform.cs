using System.Collections.Generic;
using Core.Unit;
using GameLogic;
using Render.Runtime;
using Tools;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public class BattlePlatform : Core.Unit.Unit
{
	private GameObject CurrentPlatform;

	private readonly Dictionary<string, GameObject> FightBackGroundDict = new Dictionary<string, GameObject>();

	private readonly Dictionary<long, Effect> _timelineEffects = new Dictionary<long, Effect>();

	private MaterialPropertyBlock mpb;

	protected override void Awake()
	{
		mpb = new MaterialPropertyBlock();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.ShowTimelineEffect.AddListener(ShowTimelineEffect);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.HideTimelineEffect.AddListener(HideTimelineEffect);
		base.Awake();
	}

	public void InitBattlePlatform()
	{
		foreach (KeyValuePair<string, AsyncOperationHandle<GameObject>> battlePlatformAsset in SimpleSingletonProvider<CharacterAssetManager>.inst.GetBattlePlatformAssets())
		{
			if (!FightBackGroundDict.ContainsKey(battlePlatformAsset.Key) && battlePlatformAsset.Value.IsDone)
			{
				GameObject gameObject = Object.Instantiate(battlePlatformAsset.Value.Result, base.transform);
				gameObject.gameObject.SetActiveEx(active: false);
				FightBackGroundDict.TryAdd(battlePlatformAsset.Key, gameObject);
			}
		}
	}

	public void OpenFightBackGround(long attacker)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(attacker);
		if (playerDataById == null)
		{
			return;
		}
		SkinStandingPaintingConfigureItem standingPainting = playerDataById.player.standingPainting;
		SimpleSingletonProvider<GameLogicManager>.inst.battle.BattleBGM?.PlayFightBGM(standingPainting.FightBGM);
		string battlePlatform = standingPainting.GetBattlePlatform();
		if (FightBackGroundDict.TryGetValue(battlePlatform, out var value))
		{
			if (CurrentPlatform != null && !CurrentPlatform.Equals(value))
			{
				CloseFightBackGround();
			}
			CurrentPlatform = value;
			value.gameObject.SetActiveEx(active: true);
		}
	}

	public void SetCurrentFightBackGroundBlurBg(Texture2D blurTex)
	{
		if (!(CurrentPlatform == null) && !(blurTex == null))
		{
			Renderer renderer = CurrentPlatform.transform.Find("blur_bg")?.GetComponent<Renderer>();
			if (!(renderer == null))
			{
				mpb.SetTexture(ShaderConstant._Main_Tex, blurTex);
				renderer.SetPropertyBlock(mpb);
			}
		}
	}

	public void CloseFightBackGround()
	{
		if (!(CurrentPlatform == null))
		{
			CurrentPlatform.gameObject.SetActiveEx(active: false);
			CurrentPlatform = null;
		}
	}

	protected override void OnDestroy()
	{
		BattleLogic battle = SimpleSingletonProvider<GameLogicManager>.inst.battle;
		if (battle != null)
		{
			battle.signal.ShowTimelineEffect.RemoveListener(ShowTimelineEffect);
			battle.signal.HideTimelineEffect.RemoveListener(HideTimelineEffect);
		}
		_timelineEffects.Clear();
		foreach (KeyValuePair<string, GameObject> item in FightBackGroundDict)
		{
			if (item.Value != null)
			{
				Object.Destroy(item.Value);
			}
		}
		FightBackGroundDict.Clear();
		base.OnDestroy();
	}

	private async void ShowTimelineEffect(int effectId, bool follow, Transform parent)
	{
		if (!_timelineEffects.ContainsKey(effectId))
		{
			Effect value = ((!follow) ? (await SimpleSingletonProvider<EffectManager>.inst.PlayById(effectId, parent.position, parent.rotation)) : (await SimpleSingletonProvider<EffectManager>.inst.PlayById(effectId, Vector3.zero, Quaternion.identity, parent)));
			_timelineEffects.Add(effectId, value);
		}
	}

	private void HideTimelineEffect(int effectId)
	{
		if (_timelineEffects.Remove(effectId, out var value))
		{
			value.ReleaseEffect();
		}
	}
}
