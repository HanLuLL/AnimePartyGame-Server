using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Core;

public class ClingingVariety : Core.Unit.Unit
{
	[SerializeField]
	public ClingingVarietySub DefaultClinging;

	[SerializeField]
	public ClingingVarietySub SlaughterClinging;

	[SerializeField]
	public ClingingVarietySub RestlessClinging;

	[SerializeField]
	public float ExplosionDuration = 0.5f;

	public async UniTask ShowClinging(int v)
	{
		if (((!DefaultClinging.gameObject.activeSelf && SlaughterClinging.gameObject.activeSelf) || RestlessClinging.gameObject.activeSelf) && v == 0)
		{
			await PlayExplosion();
		}
		if (DefaultClinging != null)
		{
			DefaultClinging.gameObject.SetActive(v == 0);
		}
		if (SlaughterClinging != null)
		{
			SlaughterClinging.gameObject.SetActive(v == 1);
		}
		if (RestlessClinging != null)
		{
			RestlessClinging.gameObject.SetActive(v == 2);
		}
	}

	private async UniTask PlayExplosion()
	{
		if (SlaughterClinging.gameObject.activeSelf)
		{
			SlaughterClinging.PlayExplosion();
		}
		if (RestlessClinging.gameObject.activeSelf)
		{
			RestlessClinging.PlayExplosion();
		}
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay((int)(ExplosionDuration * 1000f));
	}
}
