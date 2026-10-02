using System;
using Core.Scene;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class StopFrameBehaviour : PlayableBehaviour
{
	[SerializeField]
	private float attackFrameDuration;

	[SerializeField]
	private float attackFrameMultiplier = 1f;

	[SerializeField]
	private float victimFrameDuration;

	[SerializeField]
	private float victimFrameMultiplier = 1f;

	private PlayableDirector _director;

	[NonSerialized]
	public double StartTime;

	private bool _alreadyPlayed;

	private double _triggerTime;

	private double _lastDirectorTime;

	public override void OnPlayableCreate(Playable playable)
	{
		base.OnPlayableCreate(playable);
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetCustomComponent<PlayableDirector>();
		}
		_triggerTime = StartTime;
		_alreadyPlayed = false;
		_lastDirectorTime = (((UnityEngine.Object)(object)_director != null) ? _director.time : 0.0);
	}

	public override void OnGraphStop(Playable playable)
	{
		if (!_alreadyPlayed)
		{
			TryTrigger();
		}
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if (!_alreadyPlayed)
		{
			TryTrigger();
		}
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetCustomComponent<PlayableDirector>();
		}
		if (!((UnityEngine.Object)(object)_director == null))
		{
			double time = _director.time;
			double lastDirectorTime = _lastDirectorTime;
			if (!_alreadyPlayed && ((lastDirectorTime < _triggerTime && time >= _triggerTime) || Math.Abs(time - _triggerTime) < 0.0001))
			{
				TryTrigger();
			}
			_lastDirectorTime = time;
		}
	}

	private void TryTrigger()
	{
		_alreadyPlayed = true;
		if (BattleSceneController.inst == null)
		{
			return;
		}
		BattleShowDirector directorManager = BattleSceneController.inst.directorManager;
		if (!(directorManager == null))
		{
			if (attackFrameDuration > 0f && attackFrameMultiplier > 0f && directorManager.attacker != null)
			{
				directorManager.attacker.PlayStopped(attackFrameDuration, attackFrameMultiplier);
			}
			if (victimFrameDuration > 0f && victimFrameMultiplier > 0f && directorManager.victim != null)
			{
				directorManager.victim.PlayStopped(victimFrameDuration, victimFrameMultiplier);
			}
		}
	}
}
