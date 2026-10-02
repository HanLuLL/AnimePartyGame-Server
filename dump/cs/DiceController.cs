using System;
using System.Linq;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using UI;
using UnityEngine;

[Serializable]
public class DiceController : Unit
{
	[SerializeField]
	public GameObject Container;

	private Transform blankDice;

	private Transform pointDice;

	private string diceAnimation;

	private Animator _animator;

	private DiceAnimatorEvent _event;

	private string blankDiceKey;

	private string pointDiceKey;

	protected override void Awake()
	{
		_event = Container.GetComponent<DiceAnimatorEvent>();
		_animator = Container.GetComponent<Animator>();
	}

	public void ReadyDiceData(long playerId, int point, Vector3 vect)
	{
		base.transform.position = vect;
		FashionDiceConfigure fashionDiceConfigure = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.PlayerDiceConfig(playerId);
		blankDiceKey = fashionDiceConfigure.DiceModel + "_0";
		pointDiceKey = $"{fashionDiceConfigure.DiceModel}_{point}";
		diceAnimation = fashionDiceConfigure.DiceAnim;
		blankDice = SimpleSingletonProvider<DiceManager>.inst.GetDiceObject(blankDiceKey)?.transform;
		pointDice = SimpleSingletonProvider<DiceManager>.inst.GetDiceObject(pointDiceKey)?.transform;
		_event.UpdateData(blankDice, pointDice, fashionDiceConfigure);
	}

	public void ReadyDiceData(int point, Vector3 vect)
	{
		base.transform.position = vect;
		FashionDiceConfigure fashionDiceConfigure = StaticConfigure.Fashion.Dices[0].Id.GetItemInfoConfigure().SubMeterID.GetFashionDiceConfigure();
		blankDiceKey = fashionDiceConfigure.DiceModel + "_0";
		pointDiceKey = $"{fashionDiceConfigure.DiceModel}_{point}";
		diceAnimation = fashionDiceConfigure.DiceAnim;
		blankDice = SimpleSingletonProvider<DiceManager>.inst.GetDiceObject(blankDiceKey)?.transform;
		pointDice = SimpleSingletonProvider<DiceManager>.inst.GetDiceObject(pointDiceKey)?.transform;
		_event.UpdateData(blankDice, pointDice, fashionDiceConfigure);
	}

	public async UniTask<bool> ShowDice()
	{
		ResetTransform();
		((Behaviour)(object)_animator).enabled = true;
		_animator.speed = BattleConfig.DiceAnimatorSpeed;
		_animator.Play(diceAnimation, 0, 0f);
		if (blankDice != null && pointDice != null)
		{
			bool result = await WaitForAnimationComplete(diceAnimation);
			SimpleSingletonProvider<DiceManager>.inst.StopDice(blankDiceKey, blankDice.gameObject);
			SimpleSingletonProvider<DiceManager>.inst.StopDice(pointDiceKey, pointDice.gameObject);
			SimpleSingletonProvider<DiceManager>.inst.StopDiceController(this);
			return result;
		}
		return false;
	}

	private async UniTask<bool> WaitForAnimationComplete(string animName)
	{
		if ((UnityEngine.Object)(object)_animator == null)
		{
			return false;
		}
		AnimationClip val = _animator.runtimeAnimatorController.animationClips.FirstOrDefault((AnimationClip clip) => ((UnityEngine.Object)(object)clip).name == animName);
		if ((UnityEngine.Object)(object)val != null)
		{
			float num = val.length / _animator.speed;
			if (await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(TimeSpan.FromSeconds(num)))
			{
				((Behaviour)(object)_animator).enabled = false;
				return true;
			}
		}
		return false;
	}

	private void ResetTransform()
	{
		if (Container != null)
		{
			Container.transform.localPosition = Vector3.zero;
			if (blankDice != null)
			{
				blankDice.parent = Container.transform;
				pointDice.parent = Container.transform;
				blankDice.localRotation = Quaternion.identity;
			}
			if (pointDice != null)
			{
				pointDice.localRotation = Quaternion.identity;
			}
		}
	}
}
