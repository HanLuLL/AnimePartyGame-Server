using GameLogic;
using Tools;
using UnityEngine;

namespace Core.Unit;

public class PKElementAnimationTarget : Unit
{
	[SerializeField]
	public string Key;

	public Animator _animator;

	protected override void Awake()
	{
		_animator = GetComponent<Animator>();
		base.Awake();
	}

	private void Reset()
	{
		Key = base.gameObject.name;
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.battle == null)
		{
			ChangeAnimatorSpeed(1f);
		}
		else
		{
			ChangeAnimatorSpeed(BattleConfig.RoleAnimatorSpeed);
		}
	}

	public void ChangeAnimatorSpeed(float speed)
	{
		if ((Object)(object)_animator != null)
		{
			_animator.speed = speed;
		}
	}

	public void CrossFade(string animationState, float f)
	{
		if ((Object)(object)_animator != null)
		{
			_animator.CrossFade(animationState, f);
		}
	}
}
