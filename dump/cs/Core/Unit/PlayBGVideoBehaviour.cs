using System;
using Core.Scene;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class PlayBGVideoBehaviour : PlayableBehaviour
{
	[SerializeField]
	public int VideoConfigId;

	[SerializeField]
	public bool KeepLastFrame;

	[SerializeField]
	public int SortingOrder;

	[SerializeField]
	public float ScaleRatio;

	[SerializeField]
	public Vector2 offsetPos;

	[SerializeField]
	public bool FullScreen = true;

	[SerializeField]
	public Vector2 movieSize;

	[SerializeField]
	public bool EnableFrameLag;

	private BattleFullScreenQuad videoQuad;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		base.OnBehaviourPlay(playable, info);
		if (videoQuad == null)
		{
			GameObject gameObject = new GameObject();
			BattleShowDirector battleShowDirector = BattleSceneController.inst?.directorManager;
			if (battleShowDirector != null)
			{
				gameObject.transform.parent = battleShowDirector.battlePlatform.transform;
				gameObject.layer = battleShowDirector.battlePlatform.gameObject.layer;
				battleShowDirector.BattleEffect.TryAddBattleElement(gameObject);
			}
			videoQuad = gameObject.AddComponent<BattleFullScreenQuad>();
		}
		string videoKey = VideoConfigId.GetVideoKey();
		videoQuad.TryPlayVideo(videoKey, SortingOrder, ScaleRatio, info.effectiveSpeed, offsetPos, KeepLastFrame, FullScreen, movieSize.x, movieSize.y).Forget();
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		base.OnBehaviourPause(playable, info);
		videoQuad = null;
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		base.ProcessFrame(playable, info, playerData);
		if (EnableFrameLag && !(videoQuad == null))
		{
			videoQuad.UpdateVideoSpeed(info.effectiveSpeed);
		}
	}
}
