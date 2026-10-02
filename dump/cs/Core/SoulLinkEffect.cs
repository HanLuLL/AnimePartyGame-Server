using System.Collections.Generic;
using UnityEngine;

namespace Core;

public class SoulLinkEffect : MonoBehaviour
{
	private LineRenderer[] lines;

	private Transform p1;

	private Transform p2;

	[SerializeField]
	private int vertexCount = 10;

	[SerializeField]
	private float minVertexHeight = 80f;

	[SerializeField]
	private float vertexHeightRatio = 0.5f;

	private List<Vector3> pointList;

	public void UpdateTarget(GameObject p1ID, GameObject p2ID)
	{
		p1 = p1ID.transform;
		p2 = p2ID.transform;
	}

	private void OnEnable()
	{
		lines = GetComponentsInChildren<LineRenderer>();
		pointList = new List<Vector3>();
	}

	private void Update()
	{
		if (p1 != null && p2 != null && lines.Length != 0)
		{
			Vector3 vector = (p1.transform.position + p2.transform.position) / 2f;
			vector.y = Mathf.Max(minVertexHeight, (p1.transform.position - p2.transform.position).magnitude * vertexHeightRatio);
			pointList.Clear();
			pointList.Add(p1.transform.position);
			for (float num = 0f; num <= 1f; num += 1f / (float)vertexCount)
			{
				Vector3 a = Vector3.Lerp(p1.transform.position, vector, num);
				Vector3 b = Vector3.Lerp(vector, p2.transform.position, num);
				Vector3 item = Vector3.Lerp(a, b, num);
				pointList.Add(item);
			}
			pointList.Add(p2.transform.position);
			for (int i = 0; i < lines.Length; i++)
			{
				lines[i].positionCount = pointList.Count;
				lines[i].SetPositions(pointList.ToArray());
			}
		}
	}
}
