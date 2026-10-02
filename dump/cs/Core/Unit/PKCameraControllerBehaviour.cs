using System;
using Core.Scene;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class PKCameraControllerBehaviour : PlayableBehaviour
{
	[SerializeField]
	public PKCameraType cameraType;

	[SerializeField]
	public float blendTime;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		base.OnBehaviourPlay(playable, info);
		BattleShowDirector directorManager = BattleSceneController.inst.directorManager;
		if (directorManager != null)
		{
			directorManager.PKCamera.SwitchVCamera(cameraType, blendTime);
		}
	}
}
