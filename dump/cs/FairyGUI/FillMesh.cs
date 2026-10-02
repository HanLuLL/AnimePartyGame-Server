using System;
using UnityEngine;

namespace FairyGUI;

public class FillMesh : IMeshFactory
{
	public FillMethod method;

	public int origin;

	public float amount;

	public bool clockwise;

	public FillMesh()
	{
		clockwise = true;
		amount = 1f;
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		float num = Mathf.Clamp01(amount);
		switch (method)
		{
		case FillMethod.Horizontal:
			FillHorizontal(vb, vb.contentRect, origin, num);
			break;
		case FillMethod.Vertical:
			FillVertical(vb, vb.contentRect, origin, num);
			break;
		case FillMethod.Radial90:
			FillRadial90(vb, vb.contentRect, (Origin90)origin, num, clockwise);
			break;
		case FillMethod.Radial180:
			FillRadial180(vb, vb.contentRect, (Origin180)origin, num, clockwise);
			break;
		case FillMethod.Radial360:
			FillRadial360(vb, vb.contentRect, (Origin360)origin, num, clockwise);
			break;
		}
	}

	private static void FillHorizontal(VertexBuffer vb, Rect vertRect, int origin, float amount)
	{
		float num = vertRect.width * amount;
		if (origin == 1 || origin == 1)
		{
			vertRect.x += vertRect.width - num;
		}
		vertRect.width = num;
		vb.AddQuad(vertRect);
		vb.AddTriangles();
	}

	private static void FillVertical(VertexBuffer vb, Rect vertRect, int origin, float amount)
	{
		float num = vertRect.height * amount;
		if (origin == 1 || origin == 1)
		{
			vertRect.y += vertRect.height - num;
		}
		vertRect.height = num;
		vb.AddQuad(vertRect);
		vb.AddTriangles();
	}

	private static void FillRadial90(VertexBuffer vb, Rect vertRect, Origin90 origin, float amount, bool clockwise)
	{
		bool num = origin == Origin90.TopRight || origin == Origin90.BottomRight;
		bool flag = origin == Origin90.BottomLeft || origin == Origin90.BottomRight;
		if (num != flag)
		{
			clockwise = !clockwise;
		}
		float num2 = (clockwise ? amount : (1f - amount));
		float num3 = Mathf.Tan((float)Math.PI / 2f * num2);
		bool flag2 = false;
		if (num2 != 1f)
		{
			flag2 = vertRect.height / vertRect.width - num3 > 0f;
		}
		if (!clockwise)
		{
			flag2 = !flag2;
		}
		float num4 = vertRect.x + ((num2 == 0f) ? float.MaxValue : (vertRect.height / num3));
		float num5 = vertRect.y + ((num2 == 1f) ? float.MaxValue : (vertRect.width * num3));
		float x = num4;
		float y = num5;
		if (num)
		{
			x = vertRect.width - num4;
		}
		if (flag)
		{
			y = vertRect.height - num5;
		}
		float x2 = (num ? (vertRect.width - vertRect.x) : vertRect.xMin);
		float y2 = (flag ? (vertRect.height - vertRect.y) : vertRect.yMin);
		float x3 = (num ? (0f - vertRect.xMin) : vertRect.xMax);
		float y3 = (flag ? (0f - vertRect.yMin) : vertRect.yMax);
		vb.AddVert(new Vector3(x2, y2, 0f));
		if (clockwise)
		{
			vb.AddVert(new Vector3(x3, y2, 0f));
		}
		if (num5 > vertRect.yMax)
		{
			if (flag2)
			{
				vb.AddVert(new Vector3(x, y3, 0f));
			}
			else
			{
				vb.AddVert(new Vector3(x3, y3, 0f));
			}
		}
		else
		{
			vb.AddVert(new Vector3(x3, y, 0f));
		}
		if (num4 > vertRect.xMax)
		{
			if (flag2)
			{
				vb.AddVert(new Vector3(x3, y, 0f));
			}
			else
			{
				vb.AddVert(new Vector3(x3, y3, 0f));
			}
		}
		else
		{
			vb.AddVert(new Vector3(x, y3, 0f));
		}
		if (!clockwise)
		{
			vb.AddVert(new Vector3(x2, y3, 0f));
		}
		if (num == flag)
		{
			vb.AddTriangle(0, 1, 2);
			vb.AddTriangle(0, 2, 3);
		}
		else
		{
			vb.AddTriangle(2, 1, 0);
			vb.AddTriangle(3, 2, 0);
		}
	}

	private static void FillRadial180(VertexBuffer vb, Rect vertRect, Origin180 origin, float amount, bool clockwise)
	{
		switch (origin)
		{
		case Origin180.Top:
			if (amount <= 0.5f)
			{
				vertRect.width /= 2f;
				if (clockwise)
				{
					vertRect.x += vertRect.width;
				}
				FillRadial90(vb, vertRect, (!clockwise) ? Origin90.TopRight : Origin90.TopLeft, amount / 0.5f, clockwise);
				Vector3 position3 = vb.GetPosition(-4);
				vb.AddQuad(new Rect(position3.x, position3.y, 0f, 0f));
				vb.AddTriangles(-4);
				break;
			}
			vertRect.width /= 2f;
			if (!clockwise)
			{
				vertRect.x += vertRect.width;
			}
			FillRadial90(vb, vertRect, clockwise ? Origin90.TopRight : Origin90.TopLeft, (amount - 0.5f) / 0.5f, clockwise);
			if (clockwise)
			{
				vertRect.x += vertRect.width;
			}
			else
			{
				vertRect.x -= vertRect.width;
			}
			vb.AddQuad(vertRect);
			vb.AddTriangles(-4);
			break;
		case Origin180.Bottom:
			if (amount <= 0.5f)
			{
				vertRect.width /= 2f;
				if (!clockwise)
				{
					vertRect.x += vertRect.width;
				}
				FillRadial90(vb, vertRect, clockwise ? Origin90.BottomRight : Origin90.BottomLeft, amount / 0.5f, clockwise);
				Vector3 position4 = vb.GetPosition(-4);
				vb.AddQuad(new Rect(position4.x, position4.y, 0f, 0f));
				vb.AddTriangles(-4);
				break;
			}
			vertRect.width /= 2f;
			if (clockwise)
			{
				vertRect.x += vertRect.width;
			}
			FillRadial90(vb, vertRect, clockwise ? Origin90.BottomLeft : Origin90.BottomRight, (amount - 0.5f) / 0.5f, clockwise);
			if (clockwise)
			{
				vertRect.x -= vertRect.width;
			}
			else
			{
				vertRect.x += vertRect.width;
			}
			vb.AddQuad(vertRect);
			vb.AddTriangles(-4);
			break;
		case Origin180.Left:
			if (amount <= 0.5f)
			{
				vertRect.height /= 2f;
				if (!clockwise)
				{
					vertRect.y += vertRect.height;
				}
				FillRadial90(vb, vertRect, clockwise ? Origin90.BottomLeft : Origin90.TopLeft, amount / 0.5f, clockwise);
				Vector3 position2 = vb.GetPosition(-4);
				vb.AddQuad(new Rect(position2.x, position2.y, 0f, 0f));
				vb.AddTriangles(-4);
				break;
			}
			vertRect.height /= 2f;
			if (clockwise)
			{
				vertRect.y += vertRect.height;
			}
			FillRadial90(vb, vertRect, (!clockwise) ? Origin90.BottomLeft : Origin90.TopLeft, (amount - 0.5f) / 0.5f, clockwise);
			if (clockwise)
			{
				vertRect.y -= vertRect.height;
			}
			else
			{
				vertRect.y += vertRect.height;
			}
			vb.AddQuad(vertRect);
			vb.AddTriangles(-4);
			break;
		case Origin180.Right:
			if (amount <= 0.5f)
			{
				vertRect.height /= 2f;
				if (clockwise)
				{
					vertRect.y += vertRect.height;
				}
				FillRadial90(vb, vertRect, clockwise ? Origin90.TopRight : Origin90.BottomRight, amount / 0.5f, clockwise);
				Vector3 position = vb.GetPosition(-4);
				vb.AddQuad(new Rect(position.x, position.y, 0f, 0f));
				vb.AddTriangles(-4);
				break;
			}
			vertRect.height /= 2f;
			if (!clockwise)
			{
				vertRect.y += vertRect.height;
			}
			FillRadial90(vb, vertRect, (!clockwise) ? Origin90.TopRight : Origin90.BottomRight, (amount - 0.5f) / 0.5f, clockwise);
			if (clockwise)
			{
				vertRect.y += vertRect.height;
			}
			else
			{
				vertRect.y -= vertRect.height;
			}
			vb.AddQuad(vertRect);
			vb.AddTriangles(-4);
			break;
		}
	}

	private static void FillRadial360(VertexBuffer vb, Rect vertRect, Origin360 origin, float amount, bool clockwise)
	{
		switch (origin)
		{
		case Origin360.Top:
			if (amount < 0.5f)
			{
				vertRect.width /= 2f;
				if (clockwise)
				{
					vertRect.x += vertRect.width;
				}
				FillRadial180(vb, vertRect, clockwise ? Origin180.Left : Origin180.Right, amount / 0.5f, clockwise);
				Vector3 position3 = vb.GetPosition(-8);
				vb.AddQuad(new Rect(position3.x, position3.y, 0f, 0f));
				vb.AddTriangles(-4);
				break;
			}
			vertRect.width /= 2f;
			if (!clockwise)
			{
				vertRect.x += vertRect.width;
			}
			FillRadial180(vb, vertRect, clockwise ? Origin180.Right : Origin180.Left, (amount - 0.5f) / 0.5f, clockwise);
			if (clockwise)
			{
				vertRect.x += vertRect.width;
			}
			else
			{
				vertRect.x -= vertRect.width;
			}
			vb.AddQuad(vertRect);
			vb.AddTriangles(-4);
			break;
		case Origin360.Bottom:
			if (amount < 0.5f)
			{
				vertRect.width /= 2f;
				if (!clockwise)
				{
					vertRect.x += vertRect.width;
				}
				FillRadial180(vb, vertRect, clockwise ? Origin180.Right : Origin180.Left, amount / 0.5f, clockwise);
				Vector3 position4 = vb.GetPosition(-8);
				vb.AddQuad(new Rect(position4.x, position4.y, 0f, 0f));
				vb.AddTriangles(-4);
				break;
			}
			vertRect.width /= 2f;
			if (clockwise)
			{
				vertRect.x += vertRect.width;
			}
			FillRadial180(vb, vertRect, clockwise ? Origin180.Left : Origin180.Right, (amount - 0.5f) / 0.5f, clockwise);
			if (clockwise)
			{
				vertRect.x -= vertRect.width;
			}
			else
			{
				vertRect.x += vertRect.width;
			}
			vb.AddQuad(vertRect);
			vb.AddTriangles(-4);
			break;
		case Origin360.Left:
			if (amount < 0.5f)
			{
				vertRect.height /= 2f;
				if (!clockwise)
				{
					vertRect.y += vertRect.height;
				}
				FillRadial180(vb, vertRect, clockwise ? Origin180.Bottom : Origin180.Top, amount / 0.5f, clockwise);
				Vector3 position2 = vb.GetPosition(-8);
				vb.AddQuad(new Rect(position2.x, position2.y, 0f, 0f));
				vb.AddTriangles(-4);
				break;
			}
			vertRect.height /= 2f;
			if (clockwise)
			{
				vertRect.y += vertRect.height;
			}
			FillRadial180(vb, vertRect, (!clockwise) ? Origin180.Bottom : Origin180.Top, (amount - 0.5f) / 0.5f, clockwise);
			if (clockwise)
			{
				vertRect.y -= vertRect.height;
			}
			else
			{
				vertRect.y += vertRect.height;
			}
			vb.AddQuad(vertRect);
			vb.AddTriangles(-4);
			break;
		case Origin360.Right:
			if (amount < 0.5f)
			{
				vertRect.height /= 2f;
				if (clockwise)
				{
					vertRect.y += vertRect.height;
				}
				FillRadial180(vb, vertRect, (!clockwise) ? Origin180.Bottom : Origin180.Top, amount / 0.5f, clockwise);
				Vector3 position = vb.GetPosition(-8);
				vb.AddQuad(new Rect(position.x, position.y, 0f, 0f));
				vb.AddTriangles(-4);
				break;
			}
			vertRect.height /= 2f;
			if (!clockwise)
			{
				vertRect.y += vertRect.height;
			}
			FillRadial180(vb, vertRect, clockwise ? Origin180.Bottom : Origin180.Top, (amount - 0.5f) / 0.5f, clockwise);
			if (clockwise)
			{
				vertRect.y += vertRect.height;
			}
			else
			{
				vertRect.y -= vertRect.height;
			}
			vb.AddQuad(vertRect);
			vb.AddTriangles(-4);
			break;
		}
	}
}
