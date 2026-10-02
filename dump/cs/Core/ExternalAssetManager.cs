using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core;

public class ExternalAssetManager : SimpleSingletonProvider<ExternalAssetManager>
{
	private readonly Dictionary<string, Object> AssetsDict = new Dictionary<string, Object>();

	private readonly Dictionary<string, AsyncOperationHandle<Object>> animationAssetDict = new Dictionary<string, AsyncOperationHandle<Object>>();

	public async UniTask LoadDependAssets()
	{
		await SimpleSingletonProvider<UIManager>.inst.TryShowBackground();
		await CommonUIManager.RegisterCommonExternalPackage();
		string homePanelKV = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetHomePanelKV();
		await SimpleSingletonProvider<CriMovieManager>.inst.Load(homePanelKV);
		await PreLoadAssetsByKey("ExternalAnimationInstance");
	}

	public async UniTask PreLoadAssetsByKey(string key)
	{
		if (!AssetsDict.TryGetValue(key, out var value))
		{
			value = await Addressables.LoadAssetAsync<Object>(key);
			AssetsDict.TryAdd(key, value);
		}
	}

	public async UniTask<CharacterAnimator> PlayAnimationInUI(SkinStandingPaintingConfigureItem standingPainting, string animeName, GGraph graph, float scale)
	{
		StopAnimationInUI(graph);
		graph.visible = true;
		if (!AssetsDict.TryGetValue("ExternalAnimationInstance", out var value))
		{
			value = await Addressables.LoadAssetAsync<Object>("ExternalAnimationInstance");
			AssetsDict.TryAdd("ExternalAnimationInstance", value);
		}
		CharacterAnimator characterAnimator = Object.Instantiate(value as GameObject).GetComponent<CharacterAnimator>();
		if (graph.displayObject is GoWrapper goWrapper)
		{
			goWrapper.wrapTarget = characterAnimator.gameObject;
		}
		else
		{
			graph.SetNativeObject(new GoWrapper(characterAnimator.gameObject));
		}
		graph.displayObject.scale = Vector2.one * scale;
		characterAnimator.defaultAnimationScale = standingPainting.SkinScale[0] * 4f;
		string assetKey = standingPainting.GetCharacterLabel() + animeName;
		if (!animationAssetDict.TryGetValue(assetKey, out var value2))
		{
			value2 = await GetAnimationAsset(standingPainting.GetCharacterLabel(), animeName);
			animationAssetDict.TryAdd(assetKey, value2);
		}
		Object result = value2.Result;
		AnimationClip val = (AnimationClip)(object)((result is AnimationClip) ? result : null);
		if (val != null)
		{
			characterAnimator.PlayAnimationByPlayable(val);
		}
		return characterAnimator;
	}

	public async UniTask<AsyncOperationHandle<Object>> GetAnimationAsset(string _roleLabel, string _animeName)
	{
		string platformLabel = AddressableLabel.GetPlatformLabel();
		return await AddressableHelper.LoadAssetAsync<Object>(_roleLabel + "_" + _animeName + "_" + platformLabel);
	}

	public void StopAnimationInUI(GGraph graph)
	{
		if (graph?.displayObject is GoWrapper goWrapper && !(goWrapper.wrapTarget == null))
		{
			Object.Destroy(goWrapper.wrapTarget);
			graph.visible = false;
		}
	}

	public void Dispose()
	{
		foreach (KeyValuePair<string, AsyncOperationHandle<Object>> item in animationAssetDict)
		{
			if (item.Value.IsValid())
			{
				Addressables.Release(item.Value);
			}
		}
		animationAssetDict.Clear();
		foreach (KeyValuePair<string, Object> item2 in AssetsDict)
		{
			Addressables.Release(item2.Value);
		}
		AssetsDict.Clear();
		CommonUIManager.RemoveCommonExternalPackage();
	}
}
