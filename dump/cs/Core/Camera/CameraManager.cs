using System.Collections.Generic;
using Cinemachine;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Core.Camera;

public class CameraManager : SimpleSingletonProvider<CameraManager>
{
	private readonly List<CharacterCamera> _cameras = new List<CharacterCamera>();

	private readonly Dictionary<CinemachineImpulseSource, CinemachineImpulseManager.ImpulseEvent> _activeImpulseEvents = new Dictionary<CinemachineImpulseSource, CinemachineImpulseManager.ImpulseEvent>();

	private readonly List<CinemachineImpulseSource> _invalidImpulseSources = new List<CinemachineImpulseSource>();

	protected override void InstanceInit()
	{
		base.InstanceInit();
	}

	public void Register(CharacterCamera camera)
	{
		if (!_cameras.Contains(camera))
		{
			_cameras.Add(camera);
		}
	}

	public void UnRegister(CharacterCamera camera)
	{
		if (_cameras.Contains(camera))
		{
			_cameras.Remove(camera);
		}
	}

	public void ResetCameraPriority()
	{
		foreach (CharacterCamera camera in _cameras)
		{
			camera.vCamera.Priority = 0;
		}
	}

	public CharacterCamera GetCurPlayerCamera()
	{
		foreach (CharacterCamera camera in _cameras)
		{
			if (camera.vCamera.Priority != 0)
			{
				return camera;
			}
		}
		return null;
	}

	public async UniTask<bool> SwitchCamera(CharacterCamera camera)
	{
		if ((CinemachineVirtualCamera)BattleSceneController.inst.cinemachineBrain.ActiveVirtualCamera == camera.vCamera)
		{
			camera.vCamera.Priority = 10;
			return true;
		}
		if (BattleSceneController.inst.cinemachineBrain.IsBlending)
		{
			return true;
		}
		FreeCameraObject freeObject = BattleSceneController.inst.freeObject;
		if ((object)freeObject == null || freeObject.status == FreeCameraStatus.MapSignal || !freeObject.CloseFreeCamera(camera))
		{
			return true;
		}
		if (!camera.vCamera.gameObject.activeInHierarchy)
		{
			camera.vCamera.gameObject.SetActive(value: true);
		}
		ResetCameraPriority();
		camera.vCamera.Priority = 10;
		return !(await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => (CinemachineVirtualCamera)BattleSceneController.inst.cinemachineBrain.ActiveVirtualCamera != camera.vCamera && BattleSceneController.inst.cinemachineBrain.IsBlending));
	}

	public void EnableFreeCamera(CharacterCamera camera)
	{
		if (!BattleSceneController.inst.cinemachineBrain.IsBlending)
		{
			ResetCameraPriority();
			BattleSceneController.inst.freeObject.ActiveCamera(camera);
		}
	}

	public void EnableShowCamera(Vector3 pos)
	{
		if (BattleSceneController.inst.freeObject.status != FreeCameraStatus.Player)
		{
			ControlFreeCamera(pos);
		}
	}

	public void ControlFreeCamera(Vector3 pos)
	{
		if (!BattleSceneController.inst.cinemachineBrain.IsBlending)
		{
			ResetCameraPriority();
			BattleSceneController battleSceneController = BattleSceneController.inst;
			if ((object)battleSceneController != null && (object)battleSceneController.freeObject != null)
			{
				battleSceneController.freeObject.ActiveCamera(pos);
			}
		}
	}

	public void HostFreeCamera(bool state)
	{
		BattleSceneController battleSceneController = BattleSceneController.inst;
		if ((object)battleSceneController != null && (object)battleSceneController.freeObject != null)
		{
			battleSceneController.freeObject.SetHostCamera(state);
		}
	}

	public bool GetHostStatus()
	{
		return BattleSceneController.inst.freeObject.GetHostStatus();
	}

	public void CancelFreeStatus()
	{
		BattleSceneController.inst.freeObject.CancelFreeStatus();
	}

	public void Dispose()
	{
		CancelTrackedImpulseEvents();
		_cameras.Clear();
		OnDestroyInstance();
	}

	public void GenerateImpulse(CinemachineImpulseSource impulseSource, CinemachineImpulseAsset impulseAsset)
	{
		if (!(impulseSource == null) && !(impulseAsset == null))
		{
			CleanupInvalidImpulseEvents();
			StopImpulse(impulseSource);
			impulseSource.m_ImpulseDefinition.m_AmplitudeGain = impulseAsset.amplitudeGain;
			impulseSource.m_ImpulseDefinition.m_FrequencyGain = impulseAsset.frequencyGain;
			impulseSource.m_ImpulseDefinition.m_RawSignal = impulseAsset.noiseSettings;
			impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_AttackTime = impulseAsset.noiseBlendInTime;
			impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = impulseAsset.noiseDurationTime;
			impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = impulseAsset.noiseBlendOutTime;
			CinemachineImpulseManager.ImpulseEvent impulseEvent = impulseSource.m_ImpulseDefinition.CreateAndReturnEvent(impulseSource.transform.position, impulseSource.m_DefaultVelocity);
			if (impulseEvent != null)
			{
				_activeImpulseEvents[impulseSource] = impulseEvent;
			}
		}
	}

	private void StopImpulse(CinemachineImpulseSource impulseSource)
	{
		if (!(impulseSource == null) && _activeImpulseEvents.TryGetValue(impulseSource, out var value) && value != null)
		{
			value.Cancel(CinemachineImpulseManager.Instance.CurrentTime, forceNoDecay: true);
			_activeImpulseEvents.Remove(impulseSource);
		}
	}

	private void CleanupInvalidImpulseEvents()
	{
		if (_activeImpulseEvents.Count == 0)
		{
			return;
		}
		_invalidImpulseSources.Clear();
		foreach (KeyValuePair<CinemachineImpulseSource, CinemachineImpulseManager.ImpulseEvent> activeImpulseEvent in _activeImpulseEvents)
		{
			if (!(activeImpulseEvent.Key != null))
			{
				if (activeImpulseEvent.Value != null)
				{
					activeImpulseEvent.Value.Cancel(CinemachineImpulseManager.Instance.CurrentTime, forceNoDecay: true);
				}
				_invalidImpulseSources.Add(activeImpulseEvent.Key);
			}
		}
		foreach (CinemachineImpulseSource invalidImpulseSource in _invalidImpulseSources)
		{
			_activeImpulseEvents.Remove(invalidImpulseSource);
		}
		_invalidImpulseSources.Clear();
	}

	private void CancelTrackedImpulseEvents()
	{
		if (_activeImpulseEvents.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<CinemachineImpulseSource, CinemachineImpulseManager.ImpulseEvent> activeImpulseEvent in _activeImpulseEvents)
		{
			if (activeImpulseEvent.Value != null)
			{
				activeImpulseEvent.Value.Cancel(CinemachineImpulseManager.Instance.CurrentTime, forceNoDecay: true);
			}
		}
		_activeImpulseEvents.Clear();
	}

	public void MoveCameraToPlayer(Character character)
	{
		if (!(character == null) && !(character.standLand == null))
		{
			ControlFreeCamera(character.standLand.transform.position);
		}
	}
}
