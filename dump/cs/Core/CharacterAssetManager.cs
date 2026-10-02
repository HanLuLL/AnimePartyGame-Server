using System.Collections.Generic;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Timeline;

namespace Core;

public class CharacterAssetManager : SimpleSingletonProvider<CharacterAssetManager>
{
	private readonly Dictionary<int, CharacterAsset> _cacheCharacterAssetDict = new Dictionary<int, CharacterAsset>();

	private readonly Dictionary<string, GameObject> _cacheInstantiate = new Dictionary<string, GameObject>();

	private readonly HashSet<GameObject> _cacheAnimationInstances = new HashSet<GameObject>();

	private readonly Dictionary<int, CharacterExpression> _cacheExpressions = new Dictionary<int, CharacterExpression>();

	private readonly Dictionary<int, List<int>> _roleBankDict = new Dictionary<int, List<int>>();

	private readonly Dictionary<string, AsyncOperationHandle<Texture>> _cacheTextureHandleDict = new Dictionary<string, AsyncOperationHandle<Texture>>();

	private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _cacheBattlePlatformHandleDict = new Dictionary<string, AsyncOperationHandle<GameObject>>();

	public async UniTask LoadCharacter(long playerId, int roleId, SkinStandingPaintingConfigureItem standingPainting, CharacterType type)
	{
		string characterLabel = standingPainting.GetCharacterLabel();
		await LoadCharacterAsset(roleId, characterLabel);
		await LoadExpression(roleId, type);
		await LoadHeroVoiceBank(roleId, standingPainting.Voice);
		await LoadBanks(roleId, standingPainting.Soundbankid);
		await LoadDiceAsset(playerId);
		string battlePlatform = standingPainting.GetBattlePlatform();
		await LoadBattlePlatformAsset(battlePlatform);
		await LoadTextureAsset(roleId, standingPainting);
	}

	public async UniTask LoadCharacterVideo(SkinStandingPaintingConfigureItem standingPainting)
	{
		await SimpleSingletonProvider<CriMovieManager>.inst.Load(standingPainting.GetSkillVideo());
		RepeatedField<int> fightVideos = standingPainting.FightVideos;
		for (int i = 0; i < fightVideos.Count; i++)
		{
			string videoKey = fightVideos[i].GetVideoKey();
			await SimpleSingletonProvider<CriMovieManager>.inst.Load(videoKey);
		}
	}

	private async UniTask LoadExpression(int roleId, CharacterType type)
	{
		if (type == CharacterType.Hero)
		{
			if (_cacheExpressions.TryGetValue(roleId, out var value))
			{
				value.Dispose();
				_cacheExpressions.Remove(roleId);
			}
			CharacterExpression _container = new CharacterExpression(roleId);
			await _container.ReadyExpression(checkStatus: false);
			_cacheExpressions.TryAdd(roleId, _container);
		}
	}

	public ExpressionData GetExpressionTexture(int roleId, int ItemId)
	{
		return GetExpression(roleId)?.GetExpressionData(ItemId);
	}

	public CharacterExpression GetExpression(int roleId)
	{
		return _cacheExpressions.GetValueOrDefault(roleId);
	}

	private async UniTask LoadCharacterAsset(int heroId, string roleLabel)
	{
		if (!_cacheCharacterAssetDict.ContainsKey(heroId))
		{
			string platformLabel = AddressableLabel.GetPlatformLabel();
			string key = roleLabel + "_" + platformLabel;
			AsyncOperationHandle<object> loadHandle = await AddressableHelper.LoadAssetAsync<object>(key);
			if (loadHandle.Result is CharacterAsset characterAsset)
			{
				characterAsset.loadHandle = loadHandle;
				_cacheCharacterAssetDict.TryAdd(heroId, characterAsset);
			}
			else
			{
				Debug.LogError("通过" + key + "尝试获取CharacterAsset失败");
			}
		}
	}

	public GameObject InstantiateCharacter(long playerId, int roleId)
	{
		GameObject characterInstance = SimpleSingletonProvider<InternalAssetManager>.inst.GetCharacterInstance();
		string key = (characterInstance.name = $"{playerId}{roleId}");
		if (_cacheCharacterAssetDict.TryGetValue(roleId, out var value))
		{
			characterInstance.GetComponentInChildren<Animator>().runtimeAnimatorController = value.animationController;
		}
		else
		{
			Debug.LogError($"资源缓存中 配置id{roleId} 无法找到动画控制器资源");
		}
		if (_cacheInstantiate.ContainsKey(key))
		{
			Object.Destroy(_cacheInstantiate[key]);
			_cacheInstantiate.Remove(key);
		}
		_cacheInstantiate.Add(key, characterInstance);
		return characterInstance;
	}

	public void DestroyRole(long playerId, int roleId)
	{
		string key = $"{playerId}{roleId}";
		if (_cacheInstantiate.TryGetValue(key, out var value))
		{
			Animator componentInChildren = value.GetComponentInChildren<Animator>();
			if ((Object)(object)componentInChildren != null)
			{
				componentInChildren.runtimeAnimatorController = null;
			}
			Object.Destroy(value);
			_cacheInstantiate.Remove(key);
		}
	}

	public CharacterAnimator PlayAnimation(BattlePlayerData playerData, string animeName, GGraph graph, Vector2 scale)
	{
		StopAnimation(graph);
		graph.visible = true;
		CharacterAnimator component = SimpleSingletonProvider<InternalAssetManager>.inst.GetAnimationInstance().GetComponent<CharacterAnimator>();
		int heroId = playerData.player.Hero.HeroId;
		if (_cacheCharacterAssetDict.TryGetValue(heroId, out var value))
		{
			component.animator.runtimeAnimatorController = value.animationController;
		}
		else
		{
			Debug.LogError($"资源缓存中 配置id{heroId} 无法找到动画控制器资源");
		}
		if (graph.displayObject is GoWrapper goWrapper)
		{
			goWrapper.wrapTarget = component.gameObject;
		}
		else
		{
			graph.SetNativeObject(new GoWrapper(component.gameObject));
		}
		graph.displayObject.scale = scale;
		component.defaultAnimationScale = playerData.CharacterInst.AnimationDefaultScale;
		component.TriggerAnime(animeName);
		_cacheAnimationInstances.Add(component.gameObject);
		return component;
	}

	public void StopAnimation(GGraph graph)
	{
		if (graph?.displayObject is GoWrapper goWrapper && !(goWrapper.wrapTarget == null))
		{
			if (goWrapper.wrapTarget.TryGetComponent<Animator>(out var component))
			{
				component.runtimeAnimatorController = null;
			}
			_cacheAnimationInstances.Remove(goWrapper.wrapTarget);
			Object.Destroy(goWrapper.wrapTarget);
			goWrapper.wrapTarget = null;
			graph.visible = false;
		}
	}

	public List<TimelineAsset> GetTimelineAsset(int _heroId)
	{
		if (_cacheCharacterAssetDict.TryGetValue(_heroId, out var value))
		{
			return value.timelineAssets;
		}
		Debug.LogError($"无法通过HeroId:{_heroId} 获取对应的角色资源");
		return null;
	}

	public async UniTask LoadHeroVoiceBank(int roleId, int voiceConfigId)
	{
		if (voiceConfigId != 0)
		{
			CharacterVoiceConfigure voiceConfigure = voiceConfigId.GetVoiceConfigure();
			if (voiceConfigure != null)
			{
				await LoadBank(roleId, voiceConfigure.SoundBank);
			}
		}
	}

	private async UniTask LoadBank(int roleId, int SoundBankId)
	{
		if (SoundBankId == 0)
		{
			return;
		}
		if (_roleBankDict.TryGetValue(roleId, out var value))
		{
			if (value.Contains(SoundBankId))
			{
				return;
			}
		}
		else
		{
			value = new List<int> { SoundBankId };
			_roleBankDict.Add(roleId, value);
		}
		await SimpleSingletonProvider<AudioManager>.inst.LoadBank(SoundBankId);
	}

	private async UniTask LoadBanks(int roleId, RepeatedField<int> SoundBankIds)
	{
		if (SoundBankIds.Count != 0)
		{
			for (int i = 0; i < SoundBankIds.Count; i++)
			{
				await LoadBank(roleId, SoundBankIds[i]);
			}
		}
	}

	private async UniTask LoadDiceAsset(long playerId)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
		{
			FashionDiceConfigure fashionDiceConfigure = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.PlayerDiceConfig(playerId);
			await SimpleSingletonProvider<DiceManager>.inst.LoadDice(fashionDiceConfigure.DiceModel);
		}
	}

	private async UniTask LoadBattlePlatformAsset(string platformName)
	{
		if (!_cacheBattlePlatformHandleDict.ContainsKey(platformName))
		{
			AsyncOperationHandle<GameObject> value = await AddressableHelper.LoadAssetAsync<GameObject>(platformName);
			_cacheBattlePlatformHandleDict.TryAdd(platformName, value);
		}
	}

	public Dictionary<string, AsyncOperationHandle<GameObject>> GetBattlePlatformAssets()
	{
		return _cacheBattlePlatformHandleDict;
	}

	private async UniTask LoadTextureAsset(int roleId, SkinStandingPaintingConfigureItem standingPainting)
	{
		CharacterInfoConfigure heroCharacterConfigure = CharacterHandle.GetHeroCharacterConfigure(roleId);
		if (heroCharacterConfigure != null)
		{
			string landTex = heroCharacterConfigure.LandTex;
			if (HackerConfig.IsValid())
			{
				TryGetLandTexWithGM(ref landTex, standingPainting);
			}
			if (!string.IsNullOrEmpty(landTex) && !_cacheTextureHandleDict.ContainsKey(landTex))
			{
				AsyncOperationHandle<Texture> value = await AddressableHelper.LoadAssetAsync<Texture>(landTex);
				_cacheTextureHandleDict.Add(landTex, value);
			}
		}
	}

	public Texture GetTexture(string texName)
	{
		if (_cacheTextureHandleDict.TryGetValue(texName, out var value) && value.IsDone)
		{
			return value.Result;
		}
		return null;
	}

	public void TryGetLandTexWithGM(ref string landTexName, SkinStandingPaintingConfigureItem standingPainting)
	{
		foreach (SkinStandingPaintingConfigure standingPainting2 in StaticConfigure.Skin.StandingPaintings)
		{
			foreach (SkinStandingPaintingConfigureItem skinStandingPaintingConfigureItem in standingPainting2.SkinStandingPaintingConfigureItems)
			{
				if (skinStandingPaintingConfigureItem.ItemID == standingPainting.ItemID)
				{
					CharacterInfoConfigure characterConfigure = standingPainting2.Id.GetCharacterConfigure();
					if (characterConfigure != null)
					{
						landTexName = characterConfigure.LandTex;
						return;
					}
				}
			}
		}
	}

	public void Dispose()
	{
		foreach (KeyValuePair<int, CharacterExpression> cacheExpression in _cacheExpressions)
		{
			cacheExpression.Value.Dispose();
		}
		_cacheExpressions.Clear();
		if (_roleBankDict.Count > 0)
		{
			foreach (KeyValuePair<int, List<int>> item in _roleBankDict)
			{
				foreach (int item2 in item.Value)
				{
					SimpleSingletonProvider<AudioManager>.inst.UnloadBank(item2);
				}
			}
			_roleBankDict.Clear();
		}
		if (_cacheInstantiate.Count > 0)
		{
			foreach (KeyValuePair<string, GameObject> item3 in _cacheInstantiate)
			{
				if (!(item3.Value == null))
				{
					Animator componentInChildren = item3.Value.GetComponentInChildren<Animator>();
					if ((Object)(object)componentInChildren != null)
					{
						componentInChildren.runtimeAnimatorController = null;
					}
					Object.Destroy(item3.Value);
				}
			}
			_cacheInstantiate.Clear();
		}
		if (_cacheAnimationInstances.Count > 0)
		{
			foreach (GameObject cacheAnimationInstance in _cacheAnimationInstances)
			{
				if (!(cacheAnimationInstance == null))
				{
					if (cacheAnimationInstance.TryGetComponent<Animator>(out var component))
					{
						component.runtimeAnimatorController = null;
					}
					Object.Destroy(cacheAnimationInstance);
				}
			}
			_cacheAnimationInstances.Clear();
		}
		if (_cacheCharacterAssetDict.Count > 0)
		{
			foreach (KeyValuePair<int, CharacterAsset> item4 in _cacheCharacterAssetDict)
			{
				if (item4.Value.loadHandle.IsValid())
				{
					Addressables.Release(item4.Value.loadHandle);
				}
			}
			_cacheCharacterAssetDict.Clear();
		}
		if (_cacheBattlePlatformHandleDict.Count > 0)
		{
			foreach (KeyValuePair<string, AsyncOperationHandle<GameObject>> item5 in _cacheBattlePlatformHandleDict)
			{
				if (item5.Value.IsValid())
				{
					Addressables.Release(item5.Value);
				}
			}
			_cacheBattlePlatformHandleDict.Clear();
		}
		if (_cacheTextureHandleDict.Count > 0)
		{
			foreach (KeyValuePair<string, AsyncOperationHandle<Texture>> item6 in _cacheTextureHandleDict)
			{
				if (item6.Value.IsValid())
				{
					Addressables.Release(item6.Value);
				}
			}
			_cacheTextureHandleDict.Clear();
		}
		OnDestroyInstance();
	}
}
