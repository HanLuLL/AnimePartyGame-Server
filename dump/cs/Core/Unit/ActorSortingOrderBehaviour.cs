using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Unit;

[Serializable]
public class ActorSortingOrderBehaviour : PlayableBehaviour
{
	private BattleActor battleActor;

	[SerializeField]
	private int TargetSortingOrder;

	private int DefaultSortingOrder;

	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		if (battleActor == null && ((Component)(object)playable.GetCustomComponent<PlayableDirector>()).TryGetComponent(out battleActor))
		{
			DefaultSortingOrder = battleActor.GetSortingOrder();
			battleActor.SetSortingOrder(TargetSortingOrder);
		}
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		if (battleActor != null)
		{
			battleActor.SetSortingOrder(DefaultSortingOrder);
		}
	}
}
