using UnityEngine;

namespace FairyGUI;

public class PlaneMesh : IMeshFactory
{
	public int gridSize = 30;

	public void OnPopulateMesh(VertexBuffer vb)
	{
		float width = vb.contentRect.width;
		float height = vb.contentRect.height;
		float xMax = vb.contentRect.xMax;
		float yMax = vb.contentRect.yMax;
		int num = Mathf.Min(Mathf.CeilToInt(width / (float)gridSize), 9);
		int num2 = Mathf.Min(Mathf.CeilToInt(height / (float)gridSize), 9);
		int num3 = Mathf.FloorToInt(width / (float)num);
		int num4 = Mathf.FloorToInt(height / (float)num2);
		for (int i = 0; i <= num2; i++)
		{
			float y = ((i != num2) ? (vb.contentRect.y + (float)(i * num4)) : yMax);
			for (int j = 0; j <= num; j++)
			{
				float x = ((j != num) ? (vb.contentRect.x + (float)(j * num3)) : xMax);
				vb.AddVert(new Vector3(x, y, 0f));
			}
		}
		for (int k = 0; k < num2; k++)
		{
			int num5 = k * (num + 1);
			for (int l = 1; l <= num; l++)
			{
				int num6 = num5 + l;
				vb.AddTriangle(num6 - 1, num6, num6 + num);
				vb.AddTriangle(num6, num6 + num + 1, num6 + num);
			}
		}
	}
}
