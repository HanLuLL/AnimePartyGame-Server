using Core;
using FairyGUI;
using Tools;
using UnityEngine;

namespace UI;

public class UIDiceAnimationEvent : MonoBehaviour
{
	[SerializeField]
	private int diceSFX;

	[SerializeField]
	private string diceVFX;

	public async void DiceVfx()
	{
		await SimpleSingletonProvider<EffectManager>.inst.PlayByName(diceVFX, base.transform.position, Quaternion.identity);
	}

	public void DiceSFX()
	{
		if (diceSFX != 0)
		{
			Stage.inst.PlayOneShotSound(diceSFX);
		}
	}
}
