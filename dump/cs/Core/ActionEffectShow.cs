using System;
using System.Collections.Generic;
using Cinemachine;
using Core.Camera;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.protocol;

namespace Core;

public class ActionEffectShow
{
	public bool isCancel;

	private UniTaskCompletionSource attrChangeTsc;

	private HeroAttrEffect SingleHeroAttribute;

	private PerformInfoConfigure GetPerformConfig(int performId)
	{
		if (StaticConfigure.Perform.InfoDict.TryGetValue(performId, out var value))
		{
			return value;
		}
		return null;
	}

	public UniTask PlayPlayerShow(long PlayerId, int PerformId, string actionName = "", bool ignoreDuration = false, HeroAttrEffect singleAttribute = null)
	{
		isCancel = false;
		SingleHeroAttribute = singleAttribute;
		if (PlayerId != 0L && PerformId != 0)
		{
			LimitCamera(state: true);
			PerformInfoConfigure performConfig = GetPerformConfig(PerformId);
			if (!ignoreDuration)
			{
				UniTask uniTask = SetTimer(performConfig.TotalTime, delegate
				{
					if (BattleSceneController.inst != null && BattleSceneController.inst.cinemachineBrain != null)
					{
						BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = (float)StaticGlobalData.GAME_CAMERA_SWITCH_TIME / 1000f;
					}
					LimitCamera(state: false);
				});
				Show(PlayerId, performConfig);
				if (attrChangeTsc != null)
				{
					return UniTask.WhenAll(uniTask, attrChangeTsc.Task);
				}
				return uniTask;
			}
			Show(PlayerId, performConfig);
			LimitCamera(state: false);
			if (attrChangeTsc != null)
			{
				return attrChangeTsc.Task;
			}
		}
		return UniTask.CompletedTask;
	}

	private async UniTask SetTimer(int DelayTime, Action complete, Action cancelAction = null)
	{
		if (DelayTime > 0)
		{
			float num = (float)DelayTime / BattleConfig.OtherSpeed;
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay((int)num, delegate
			{
				cancelAction?.Invoke();
				isCancel = true;
			}))
			{
				return;
			}
		}
		complete?.Invoke();
	}

	private void LimitCamera(bool state)
	{
		if (SimpleSingletonProvider<CameraManager>.hasInstance)
		{
			SimpleSingletonProvider<CameraManager>.inst.HostFreeCamera(state);
			SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.limitCameraControl.Dispatch(SimpleSingletonProvider<CameraManager>.inst.GetHostStatus());
		}
	}

	public async UniTask PlayLandShow(int landId, int PerformId, string actionName = "")
	{
		PerformInfoConfigure performConfig = GetPerformConfig(PerformId);
		if (performConfig != null)
		{
			LimitCamera(state: true);
			LandShow(landId, performConfig);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(performConfig.TotalTime);
			LimitCamera(state: false);
		}
	}

	private void Show(long _playerId, PerformInfoConfigure _config)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_playerId);
		if (playerDataById != null)
		{
			SwitchCamera(playerDataById, _config);
			PlayAnimation(playerDataById, _config);
			PlayEffectShow(playerDataById, _config);
			PlayAudioShow(playerDataById, _config);
			PlayCustomShow(playerDataById, _config);
			PlayAttrChange(playerDataById, _config);
			PlayImpulseShow(playerDataById, _config);
		}
	}

	private void SwitchCamera(BattlePlayerData _data, PerformInfoConfigure _config)
	{
		if (_config.IsFollow)
		{
			if (BattleSceneController.inst != null && BattleSceneController.inst.cinemachineBrain != null)
			{
				BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = (float)_config.SwitchTime / 1000f;
			}
			if (_data?.CharacterInst != null)
			{
				_data.CharacterInst.SwitchCamera().Forget();
			}
		}
	}

	private void PlayAnimation(BattlePlayerData _data, PerformInfoConfigure _config)
	{
		if (_config.Animations == null || _config.Animations.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<string, int> kvp in _config.Animations)
		{
			SetTimer(kvp.Value, delegate
			{
				if (_data != null && _data.CharacterInst != null)
				{
					_data.CharacterInst.characterAnimator.TriggerAnime(kvp.Key);
				}
			});
		}
	}

	private void PlayCustomShow(BattlePlayerData data, PerformInfoConfigure _config)
	{
		if (_config.CustomShows == null || _config.CustomShows.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<string, int> customShow in _config.CustomShows)
		{
			Action<long> methodInfo = ActionCustomShow.Get(customShow.Key);
			if (methodInfo == null)
			{
				continue;
			}
			SetTimer(customShow.Value, delegate
			{
				if (data != null)
				{
					methodInfo(data.player.Id);
				}
			});
		}
	}

	private void PlayEffectShow(BattlePlayerData _data, PerformInfoConfigure _config)
	{
		if (_config.Effects == null || _config.Effects.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, int> kvp in _config.Effects)
		{
			SetTimer(kvp.Value, delegate
			{
				if (SimpleSingletonProvider<EffectManager>.hasInstance && _data != null && _data.CharacterInst != null)
				{
					_data.CharacterInst.PlayCharacterEffect(kvp.Key).Forget();
				}
			});
		}
	}

	private void PlayAudioShow(BattlePlayerData _data, PerformInfoConfigure _config)
	{
		if (_config.Audio == null || _config.Audio.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, int> kvp in _config.Audio)
		{
			SetTimer(kvp.Value, delegate
			{
				Stage.inst.PlayOneShotSound(kvp.Key);
			});
		}
	}

	private async void PlayImpulseShow(BattlePlayerData _data, PerformInfoConfigure _config)
	{
		if (_config.ScreenPump == null || _config.ScreenPump.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<string, int> kvp in _config.ScreenPump)
		{
			CinemachineImpulseAsset impulseAsset = await SimpleSingletonProvider<InternalAssetManager>.inst.GetImpulseAsset(kvp.Key);
			if ((object)impulseAsset == null)
			{
				return;
			}
			SetTimer(kvp.Value, delegate
			{
				if (_data != null && !(_data.CharacterInst == null))
				{
					CharacterCamera characterCamera = _data.CharacterInst.GetCharacterCamera();
					if (characterCamera != null)
					{
						SimpleSingletonProvider<CameraManager>.inst.GenerateImpulse(characterCamera.impulseSource, impulseAsset);
					}
				}
			});
		}
	}

	private void PlayAttrChange(BattlePlayerData _data, PerformInfoConfigure _config)
	{
		if (_config.UpdateAttribute == 0 || _data == null)
		{
			return;
		}
		RepeatedField<HeroAttrEffect> attr = null;
		if (SingleHeroAttribute == null)
		{
			attr = SimpleSingletonProvider<GameLogicManager>.inst.battle.TakeOutHeroAttr(_data.player.Id);
			if (attr == null)
			{
				return;
			}
		}
		attrChangeTsc = new UniTaskCompletionSource();
		SetTimer(_config.UpdateAttribute, delegate
		{
			if (SingleHeroAttribute != null)
			{
				FinishSingleHeroAttrChange(_data);
			}
			else
			{
				FinishHeroAttrChange(_data, attr);
			}
		}, delegate
		{
			attrChangeTsc.TrySetResult();
		});
	}

	private async void FinishSingleHeroAttrChange(BattlePlayerData _data)
	{
		await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateBaseAttr(_data, SingleHeroAttribute);
		SingleHeroAttribute = null;
		attrChangeTsc.TrySetResult();
	}

	private async void FinishHeroAttrChange(BattlePlayerData _data, RepeatedField<HeroAttrEffect> _Attrs)
	{
		if (_Attrs != null)
		{
			await SimpleSingletonProvider<GameLogicManager>.inst.battle.UpdateAttr(_data, _Attrs);
			attrChangeTsc.TrySetResult();
		}
	}

	private void LandShow(int _landId, PerformInfoConfigure _config)
	{
		UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(_landId);
		if ((object)landById != null && _config != null)
		{
			SwitchCamera(landById.transform.position, _config);
			PlayEffectShow(landById, _config);
			PlayAudioShow(landById, _config);
			PlayImpulseShow(landById, _config);
		}
	}

	private void SwitchCamera(Vector3 pos, PerformInfoConfigure _config)
	{
		if (_config.IsFollow && BattleSceneController.inst != null && BattleSceneController.inst.cinemachineBrain != null)
		{
			BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = (float)_config.SwitchTime / 1000f;
		}
		SimpleSingletonProvider<CameraManager>.inst.EnableShowCamera(pos);
	}

	private void PlayEffectShow(UnitLand _land, PerformInfoConfigure _config)
	{
		if (_config.Effects == null || _config.Effects.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, int> kvp in _config.Effects)
		{
			SetTimer(kvp.Value, delegate
			{
				if (SimpleSingletonProvider<EffectManager>.hasInstance && _land != null)
				{
					_land.PlayById(kvp.Key, Vector3.zero, Quaternion.identity).Forget();
				}
			});
		}
	}

	private void PlayAudioShow(UnitLand land, PerformInfoConfigure _config)
	{
		if (_config.Audio == null || _config.Audio.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, int> kvp in _config.Audio)
		{
			SetTimer(kvp.Value, delegate
			{
				Stage.inst.PlayOneShotSound(kvp.Key);
			});
		}
	}

	private async void PlayImpulseShow(UnitLand land, PerformInfoConfigure _config)
	{
		if (_config.ScreenPump == null || _config.ScreenPump.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<string, int> kvp in _config.ScreenPump)
		{
			CinemachineImpulseAsset impulseAsset = await SimpleSingletonProvider<InternalAssetManager>.inst.GetImpulseAsset(kvp.Key);
			if ((object)impulseAsset == null)
			{
				return;
			}
			SetTimer(kvp.Value, delegate
			{
				if ((object)land != null)
				{
					UnityEngine.Camera main = UnityEngine.Camera.main;
					if (!(main == null))
					{
						GameObject gameObject = (main.GetComponent<CinemachineBrain>()?.ActiveVirtualCamera)?.VirtualCameraGameObject;
						if (!(gameObject == null))
						{
							CinemachineImpulseSource componentInChildren = gameObject.GetComponentInChildren<CinemachineImpulseSource>();
							if (!(componentInChildren == null))
							{
								SimpleSingletonProvider<CameraManager>.inst.GenerateImpulse(componentInChildren, impulseAsset);
							}
						}
					}
				}
			});
		}
	}
}
