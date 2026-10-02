using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Core.Unit;

public class PKCameraManager : Unit
{
	[SerializeField]
	public UnityEngine.Camera PKMainCamera;

	[SerializeField]
	public List<CinemachineVirtualCamera> PKCameras = new List<CinemachineVirtualCamera>();

	private CinemachineVirtualCamera _currentCamera;

	private CinemachineBrain _currentBrain;

	public List<GameObject> CamerasCache = new List<GameObject>();

	protected override void Awake()
	{
		_currentBrain = GetComponent<CinemachineBrain>();
		base.Awake();
	}

	public void SwitchVCamera(PKCameraType type, float blendTime)
	{
		if (PKCameras.Count > (int)type)
		{
			if (_currentBrain != null)
			{
				_currentBrain.m_DefaultBlend.m_Time = blendTime;
			}
			if (_currentCamera != null)
			{
				_currentCamera.Priority = 50;
			}
			_currentCamera = PKCameras[(int)type];
			_currentCamera.Priority = 51;
		}
	}

	public void EnablePKCamera()
	{
		PKMainCamera.enabled = true;
		foreach (CinemachineVirtualCamera pKCamera in PKCameras)
		{
			pKCamera.Priority = 50;
		}
		SwitchVCamera(PKCameraType.Init, 0f);
	}

	public void DisablePKCamera()
	{
		PKMainCamera.enabled = false;
		if (CamerasCache.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < CamerasCache.Count; i++)
		{
			if (CamerasCache[i] != null)
			{
				Object.Destroy(CamerasCache[i]);
			}
		}
		CamerasCache.Clear();
	}
}
