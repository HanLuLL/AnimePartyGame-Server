using System;
using System.Collections.Generic;
using Core.Camera;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core;

public class ActionSummonShow
{
	public bool isCancel;

	public UniTask PlaySummonShow(Summon summon, int PerformId, string actionName = "", bool ignoreDuration = false)
	{
		isCancel = false;
		if (summon != null && summon.summonInstance != null && PerformId != 0)
		{
			PerformInfoConfigure performConfig = GetPerformConfig(PerformId);
			if (performConfig == null)
			{
				return UniTask.CompletedTask;
			}
			LimitCamera(state: true);
			if (!ignoreDuration)
			{
				UniTask result = SetTimer(performConfig.TotalTime, delegate
				{
					if (BattleSceneController.inst != null && BattleSceneController.inst.cinemachineBrain != null)
					{
						BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = (float)StaticGlobalData.GAME_CAMERA_SWITCH_TIME / 1000f;
					}
					LimitCamera(state: false);
				});
				Show(summon.summonInstance, performConfig);
				return result;
			}
			Show(summon.summonInstance, performConfig);
			LimitCamera(state: false);
		}
		return UniTask.CompletedTask;
	}

	private void Show(SummonBase summon, PerformInfoConfigure _config)
	{
		if ((object)summon != null)
		{
			SwitchCamera(summon, _config);
			PlayAnimation(summon, _config);
			PlayEffectShow(summon, _config);
			PlayAudioShow(summon, _config);
			PlayCustomShow(summon, _config);
		}
	}

	private void SwitchCamera(SummonBase _data, PerformInfoConfigure _config)
	{
		if (_config.IsFollow)
		{
			if (BattleSceneController.inst != null && BattleSceneController.inst.cinemachineBrain != null)
			{
				BattleSceneController.inst.cinemachineBrain.m_DefaultBlend.m_Time = (float)_config.SwitchTime / 1000f;
			}
			if (_data != null && _data.LandId != 0)
			{
				UnitLand landById = SimpleSingletonProvider<LandManager>.inst.GetLandById(_data.LandId);
				SimpleSingletonProvider<CameraManager>.inst.EnableShowCamera(landById.transform.position);
			}
		}
	}

	private void PlayAnimation(SummonBase _data, PerformInfoConfigure _config)
	{
		if (_config.Animations == null || _config.Animations.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<string, int> kvp in _config.Animations)
		{
			SetTimer(kvp.Value, delegate
			{
				if ((object)_data != null && (UnityEngine.Object)(object)_data.animator != null)
				{
					_data.animator.SetTrigger(kvp.Key);
				}
			});
		}
	}

	private void PlayEffectShow(SummonBase _data, PerformInfoConfigure _config)
	{
		if (_config.Effects == null || _config.Effects.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, int> kvp in _config.Effects)
		{
			SetTimer(kvp.Value, delegate
			{
				if (SimpleSingletonProvider<EffectManager>.hasInstance && (object)_data != null && _data.effectContainer != null)
				{
					SimpleSingletonProvider<EffectManager>.inst.PlayById(kvp.Key, Vector3.zero, Quaternion.identity, _data.effectContainer).Forget();
				}
			});
		}
	}

	private void PlayAudioShow(SummonBase _data, PerformInfoConfigure _config)
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

	private void PlayCustomShow(SummonBase summon, PerformInfoConfigure _config)
	{
		if (_config.CustomShows == null || _config.CustomShows.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<string, int> customShow in _config.CustomShows)
		{
			Action<SummonBase> methodInfo = ActionCustomShow.GetSummon(customShow.Key);
			if (methodInfo == null)
			{
				continue;
			}
			SetTimer(customShow.Value, delegate
			{
				if ((object)summon != null)
				{
					methodInfo(summon);
				}
			});
		}
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

	private PerformInfoConfigure GetPerformConfig(int performId)
	{
		if (StaticConfigure.Perform.InfoDict.TryGetValue(performId, out var value))
		{
			return value;
		}
		return null;
	}
}
