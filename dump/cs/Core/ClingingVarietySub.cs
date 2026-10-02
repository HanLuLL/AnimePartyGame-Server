using Core.Unit;
using UnityEngine;

namespace Core;

public class ClingingVarietySub : Core.Unit.Unit
{
	[SerializeField]
	public Transform Loop;

	[SerializeField]
	public Transform Explosion;

	private void OnEnable()
	{
		if (Explosion != null)
		{
			Explosion.gameObject.SetActive(value: false);
		}
		if (Loop != null)
		{
			Loop.gameObject.SetActive(value: true);
		}
	}

	public void PlayExplosion()
	{
		if (Loop != null)
		{
			Loop.gameObject.SetActive(value: false);
		}
		if (Explosion != null)
		{
			Explosion.gameObject.SetActive(value: true);
		}
	}
}
