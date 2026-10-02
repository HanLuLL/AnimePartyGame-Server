using Core.Unit;
using FairyGUI;
using Tools;
using UnityEngine;

namespace Core;

public class DiceAnimatorEvent : Core.Unit.Unit
{
	private Transform blankDice;

	private Transform pointDice;

	private FashionDiceConfigure diceConfig;

	private readonly Vector3 initPos = new Vector3(0f, 100000f, 0f);

	public void UpdateData(Transform _blankDice, Transform _pointDice, FashionDiceConfigure _diceConfig)
	{
		blankDice = _blankDice;
		pointDice = _pointDice;
		diceConfig = _diceConfig;
	}

	public void ShowBlankDice()
	{
		if (!(blankDice == null))
		{
			blankDice.localPosition = Vector3.zero;
		}
	}

	public void DiceChange()
	{
		if (!(blankDice == null) && !(pointDice == null))
		{
			blankDice.localPosition = initPos;
			pointDice.localPosition = Vector3.zero;
		}
	}

	public async void DiceVfx()
	{
		await SimpleSingletonProvider<EffectManager>.inst.PlayByName(diceConfig.DiceVfx, base.transform.position, Quaternion.identity);
	}

	public void DiceSFX()
	{
		if (diceConfig != null && diceConfig.DiceSFX != 0)
		{
			Stage.inst.PlayOneShotSound(diceConfig.DiceSFX);
		}
	}
}
