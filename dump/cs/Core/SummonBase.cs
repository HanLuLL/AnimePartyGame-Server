using Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace Core;

public abstract class SummonBase : MonoBehaviour
{
	[Header("依赖对象")]
	public Transform characterObject;

	[SerializeField]
	public Transform effectContainer;

	[SerializeField]
	public Animator animator;

	[SerializeField]
	public PlayableDirector director;

	public ActionSummonShow summonShow = new ActionSummonShow();

	public string SummonName { get; protected set; } = "";

	public int LandId { get; protected set; }

	public abstract void InitComponent(string prefabName);

	public virtual void ReleaseSummon()
	{
		SimpleSingletonProvider<SummonManager>.inst.ReleaseSummon(this);
	}

	public virtual void HideObject()
	{
		base.transform.localScale = Vector3.zero;
	}

	public virtual void ShowObject()
	{
		base.transform.localScale = Vector3.one;
	}
}
