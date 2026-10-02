using System;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace Core.Unit;

[Serializable]
public class SignalBehaviour : PlayableBehaviour
{
	[SerializeField]
	public BattleTimelineSignalType signalType;

	[SerializeField]
	public int EffectId;

	[FormerlySerializedAs("isFollow")]
	[SerializeField]
	public bool IsFollow;

	private PlayableDirector _director;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if ((UnityEngine.Object)(object)_director == null)
		{
			_director = playable.GetCustomComponent<PlayableDirector>();
		}
		BattleSignal battleSignal = SimpleSingletonProvider<GameLogicManager>.inst.battle?.signal;
		if (battleSignal != null)
		{
			if (signalType == BattleTimelineSignalType.CrabHit)
			{
				battleSignal.CrabHit.Dispatch();
			}
			else if (signalType == BattleTimelineSignalType.StartLoopEffect)
			{
				battleSignal.ShowTimelineEffect.Dispatch(EffectId, IsFollow, ((Component)(object)_director).transform);
			}
			else if (signalType == BattleTimelineSignalType.EndLoopEffect)
			{
				battleSignal.HideTimelineEffect.Dispatch(EffectId);
			}
		}
		Debug.Log("广播消息类型：" + signalType);
	}
}
