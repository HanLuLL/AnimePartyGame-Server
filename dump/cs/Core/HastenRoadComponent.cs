using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Tools;
using UnityEngine;

namespace Core;

[Serializable]
public class HastenRoadComponent : MonoBehaviour
{
	private HastenRoadManager parent;

	[Header("元素")]
	[SerializeField]
	public Transform arrow;

	[SerializeField]
	public Transform particle;

	public RoadComponentStatus status;

	private TweenerCore<Color, Color, ColorOptions> flashTweener;

	private void Awake()
	{
		parent = base.transform.parent.GetComponent<HastenRoadManager>();
	}

	public void Update()
	{
		if (!(parent == null) && !(parent.MovingTransform == null))
		{
			float num = Vector3.Distance(new Vector3(base.transform.position.x, 0f, base.transform.position.z), new Vector3(parent.MovingTransform.position.x, 0f, parent.MovingTransform.position.z));
			if (status == RoadComponentStatus.NULL && num < parent.ComponentRadius)
			{
				status = RoadComponentStatus.ENTERING;
				flashTweener = particle.GetComponent<MeshRenderer>().material.DOColor(parent.ComponentFlashColor, "_EmissionColor", parent.FallTime);
				flashTweener.SetAutoKill(autoKillOnCompletion: false);
				SimpleSingletonProvider<AudioManager>.inst.SendEvent(55, parent.gameObject);
				DOMove(parent.FallDistance, RoadComponentStatus.ENTERED);
			}
			if (status == RoadComponentStatus.ENTERED && num > parent.ComponentRadius)
			{
				status = RoadComponentStatus.EXIT;
				flashTweener?.PlayBackwards();
				DOMove(0f, RoadComponentStatus.NULL);
			}
		}
	}

	private void DOMove(float offsetY, RoadComponentStatus targetStatus)
	{
		base.transform.DOLocalMove(new Vector3(base.transform.localPosition.x, offsetY, base.transform.localPosition.z), parent.FallTime).SetEase(parent.FallEase).OnComplete(delegate
		{
			status = targetStatus;
		});
	}

	private void OnDestroy()
	{
		flashTweener?.Kill();
		flashTweener = null;
	}
}
