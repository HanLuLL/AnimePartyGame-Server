using System;
using Core.Unit;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class OffsetBehaviour : PlayableBehaviour
{
	[SerializeField]
	private AnimationCurve _offsetXCurve;

	[SerializeField]
	private AnimationCurve _offsetYCurve;

	[SerializeField]
	private AnimationCurve _offsetZCurve;

	private PlayableDirector _director;

	private BattleActor _actor;

	private Vector3 initPos;

	private bool isInitialized;

	private float _triggerTime;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetCustomComponent<PlayableDirector>();
		}
		Initialize(info.output.GetUserData());
		_triggerTime = (float)_director.time;
	}

	private void Initialize(UnityEngine.Object userData)
	{
		if (!isInitialized)
		{
			_actor = userData as BattleActor;
			if ((object)_actor != null)
			{
				initPos = _actor.transform.localPosition;
			}
			isInitialized = true;
		}
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if (!((double)_triggerTime > _director.time))
		{
			float time = (float)_director.time - _triggerTime;
			Vector3 zero = Vector3.zero;
			AnimationCurve offsetXCurve = _offsetXCurve;
			if (offsetXCurve != null && offsetXCurve.length > 0)
			{
				zero.x = _offsetXCurve.Evaluate(time);
			}
			offsetXCurve = _offsetYCurve;
			if (offsetXCurve != null && offsetXCurve.length > 0)
			{
				zero.y = _offsetYCurve.Evaluate(time);
			}
			offsetXCurve = _offsetZCurve;
			if (offsetXCurve != null && offsetXCurve.length > 0)
			{
				zero.z = _offsetZCurve.Evaluate(time);
			}
			if ((object)_actor != null)
			{
				_actor.transform.localPosition = initPos + zero;
			}
		}
	}

	public override void OnGraphStart(Playable playable)
	{
		base.OnGraphStart(playable);
	}

	public override void PrepareData(Playable playable, FrameData info)
	{
		base.PrepareData(playable, info);
	}

	public override void OnGraphStop(Playable playable)
	{
		base.OnGraphStop(playable);
	}

	public override void OnPlayableDestroy(Playable playable)
	{
		base.OnPlayableDestroy(playable);
	}
}
