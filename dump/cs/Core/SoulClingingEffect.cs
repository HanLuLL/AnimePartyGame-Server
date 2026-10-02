using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Core;

public class SoulClingingEffect : GameEffect
{
	[SerializeField]
	private List<ClingingVariety> _clingingVarietySlots;

	public void PlayClinging(int value)
	{
		DecodeSlot(value, out var l, out var l2, out var l3);
		ClingingVariety safeByIndex = _clingingVarietySlots.GetSafeByIndex(0);
		if (safeByIndex != null)
		{
			safeByIndex.ShowClinging(l).Forget();
		}
		ClingingVariety safeByIndex2 = _clingingVarietySlots.GetSafeByIndex(1);
		if (safeByIndex2 != null)
		{
			safeByIndex2.ShowClinging(l2).Forget();
		}
		ClingingVariety safeByIndex3 = _clingingVarietySlots.GetSafeByIndex(2);
		if (safeByIndex3 != null)
		{
			safeByIndex3.ShowClinging(l3).Forget();
		}
	}

	private void DecodeSlot(int code, out int l1, out int l2, out int l3)
	{
		l1 = code / 9;
		code %= 9;
		l2 = code / 3;
		l3 = code % 3;
	}
}
