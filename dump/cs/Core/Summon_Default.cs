using UnityEngine;
using UnityEngine.Playables;

namespace Core;

public class Summon_Default : SummonBase
{
	public override void InitComponent(string prefabName)
	{
		base.SummonName = prefabName;
		if (effectContainer == null)
		{
			effectContainer = base.gameObject.transform.GetChild(0);
		}
		if ((Object)(object)animator == null)
		{
			animator = effectContainer.GetComponentInChildren<Animator>();
		}
		if ((Object)(object)director == null)
		{
			director = effectContainer.GetComponentInChildren<PlayableDirector>();
		}
	}
}
