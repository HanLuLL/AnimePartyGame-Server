using System;
using UnityEngine;

namespace Core;

[Serializable]
public class CustomRange : MonoBehaviour
{
	[SerializeField]
	public float range = 5f;

	[SerializeField]
	public float inner = 2f;

	private Vector3 top => base.transform.position;

	private Vector3 bottom => base.transform.position + base.transform.forward * range;

	public (Vector3, Vector3) GetRandom()
	{
		float num = UnityEngine.Random.Range(0f, range);
		float num2 = UnityEngine.Random.Range(0f, (float)Math.PI * 2f);
		float num3 = (1f - num / range) * inner;
		Vector3 vector = bottom + Quaternion.Euler(0f, num2 * 57.29578f, 0f) * (Vector3.right * num3);
		return (vector, vector - top);
	}
}
