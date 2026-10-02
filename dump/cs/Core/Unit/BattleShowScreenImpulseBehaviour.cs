using System;
using Core.Camera;
using Core.Scene;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class BattleShowScreenImpulseBehaviour : PlayableBehaviour
{
	private PlayableDirector _director;

	[SerializeField]
	private CinemachineImpulseAsset impulseAsset;

	[SerializeField]
	private bool _tempChangeFunc;

	[SerializeField]
	private AnimationCurve _offsetXCurve;

	[SerializeField]
	private AnimationCurve _offsetYCurve;

	[SerializeField]
	private AnimationCurve _offsetZCurve;

	[SerializeField]
	private PKCampType pkCampType;

	private BattleActor _target;

	private Vector3 _initPosition;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.fight != null)
		{
			if ((UnityEngine.Object)(object)_director == null)
			{
				_director = playable.GetCustomComponent<PlayableDirector>();
			}
			if ((object)impulseAsset != null)
			{
				BattleSceneController.inst.directorManager.GenerateImpulse(impulseAsset);
			}
			InitData();
		}
	}

	private void InitData()
	{
		if (_tempChangeFunc && !(_target == null))
		{
			_initPosition = _target.transform.localPosition;
		}
	}

	public override void OnGraphStart(Playable playable)
	{
		base.OnGraphStart(playable);
		if (_tempChangeFunc && !(BattleSceneController.inst == null) && !(BattleSceneController.inst.directorManager == null))
		{
			switch (pkCampType)
			{
			case PKCampType.Attacker:
				_target = BattleSceneController.inst.directorManager.attacker;
				break;
			case PKCampType.Defender:
				_target = BattleSceneController.inst.directorManager.victim;
				break;
			}
		}
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		base.ProcessFrame(playable, info, playerData);
		if (_tempChangeFunc && !(_target == null))
		{
			float time = (float)playable.GetTime();
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
			_target.transform.localPosition = _initPosition + zero;
		}
	}
}
