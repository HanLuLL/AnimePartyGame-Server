using System;
using Cinemachine;
using Core.Camera;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace Core;

[Serializable]
public class SceneScreenImpulseBehaviour : PlayableBehaviour
{
	private PlayableDirector _director;

	private CinemachineImpulseSource _ImpulseSource;

	[SerializeField]
	private string _ImpulseAssetName;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetCustomComponent<PlayableDirector>();
		}
		if (_ImpulseSource == null)
		{
			_ImpulseSource = UnityEngine.Camera.main?.GetComponentInChildren<CinemachineImpulseSource>();
		}
		if ((object)_ImpulseSource != null)
		{
			PlayImpulse().Forget();
		}
	}

	private async UniTaskVoid PlayImpulse()
	{
		CinemachineImpulseAsset impulseAsset = await SimpleSingletonProvider<InternalAssetManager>.inst.GetImpulseAsset(_ImpulseAssetName);
		SimpleSingletonProvider<CameraManager>.inst.GenerateImpulse(_ImpulseSource, impulseAsset);
	}
}
