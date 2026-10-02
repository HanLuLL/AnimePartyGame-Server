using System;
using Cinemachine;
using Core.Scene;
using Tools;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Core.Unit.BattleShow;

[Serializable]
public class PKTempCameraBehaviour : PlayableBehaviour
{
	private GameObject _Camera;

	[SerializeField]
	public AnimationClip clipAsset;

	private PlayableGraph playableGraph;

	public AnimationClipPlayable playClip { get; set; }

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		PlayableDirector customComponent = playable.GetCustomComponent<PlayableDirector>();
		if (_Camera == null)
		{
			GameObject original = (GameObject)SimpleSingletonProvider<InternalAssetManager>.inst.TryGetOperationHandle("BattleChainCamera");
			if ((UnityEngine.Object)(object)customComponent != null)
			{
				_Camera = UnityEngine.Object.Instantiate(original, ((Component)(object)customComponent).transform.parent);
				CinemachineVirtualCamera component = _Camera.GetComponent<CinemachineVirtualCamera>();
				BattleShowDirector directorManager = BattleSceneController.inst.directorManager;
				if (directorManager != null)
				{
					component.Follow = directorManager.BattleEffect.SceneCamLock;
					directorManager.PKCamera.CamerasCache.Add(_Camera);
				}
			}
		}
		if (!(_Camera == null) && !((UnityEngine.Object)(object)clipAsset == null))
		{
			playClip = AnimationPlayableUtilities.PlayClip(_Camera.GetComponent<Animator>(), clipAsset, ref playableGraph);
			base.OnBehaviourPlay(playable, info);
		}
	}

	public override void PrepareFrame(Playable playable, FrameData info)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		base.PrepareFrame(playable, info);
		if (playableGraph.IsValid() && playClip.IsValid<AnimationClipPlayable>())
		{
			playClip.SetSpeed<AnimationClipPlayable>((double)info.effectiveSpeed);
			Debug.Log(info.effectiveSpeed);
		}
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		if (playableGraph.IsValid())
		{
			playableGraph.Destroy();
		}
		_Camera = null;
		base.OnBehaviourPause(playable, info);
	}
}
