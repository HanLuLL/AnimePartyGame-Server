using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI;

public sealed class VertexBuffer
{
	public Rect contentRect;

	public Rect uvRect;

	public Color32 vertexColor;

	public Vector2 textureSize;

	public readonly List<Vector3> vertices;

	public readonly List<Color32> colors;

	public readonly List<Vector2> uvs;

	public readonly List<Vector2> uvs2;

	public readonly List<int> triangles;

	public static Vector2[] NormalizedUV = new Vector2[4]
	{
		new Vector2(0f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f),
		new Vector2(1f, 0f)
	};

	public static Vector2[] NormalizedPosition = new Vector2[4]
	{
		new Vector2(0f, 1f),
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(1f, 1f)
	};

	internal bool _alphaInVertexColor;

	internal bool _isArbitraryQuad;

	private static Stack<VertexBuffer> _pool = new Stack<VertexBuffer>();

	private static List<Vector4> helperV4List = new List<Vector4>(4)
	{
		Vector4.zero,
		Vector4.zero,
		Vector4.zero,
		Vector4.zero
	};

	public int currentVertCount => vertices.Count;

	public static VertexBuffer Begin()
	{
		if (_pool.Count > 0)
		{
			VertexBuffer vertexBuffer = _pool.Pop();
			vertexBuffer.Clear();
			return vertexBuffer;
		}
		return new VertexBuffer();
	}

	public static VertexBuffer Begin(VertexBuffer source)
	{
		VertexBuffer vertexBuffer = Begin();
		vertexBuffer.contentRect = source.contentRect;
		vertexBuffer.uvRect = source.uvRect;
		vertexBuffer.vertexColor = source.vertexColor;
		vertexBuffer.textureSize = source.textureSize;
		return vertexBuffer;
	}

	private VertexBuffer()
	{
		vertices = new List<Vector3>();
		colors = new List<Color32>();
		uvs = new List<Vector2>();
		uvs2 = new List<Vector2>();
		triangles = new List<int>();
	}

	public void End()
	{
		_pool.Push(this);
	}

	public void Clear()
	{
		vertices.Clear();
		colors.Clear();
		uvs.Clear();
		uvs2.Clear();
		triangles.Clear();
		_isArbitraryQuad = false;
		_alphaInVertexColor = false;
	}

	public void AddVert(Vector3 position)
	{
		position.y = 0f - position.y;
		vertices.Add(position);
		colors.Add(vertexColor);
		if (vertexColor.a != byte.MaxValue)
		{
			_alphaInVertexColor = true;
		}
		uvs.Add(new Vector2(Mathf.Lerp(uvRect.xMin, uvRect.xMax, (position.x - contentRect.xMin) / contentRect.width), Mathf.Lerp(uvRect.yMax, uvRect.yMin, (0f - position.y - contentRect.yMin) / contentRect.height)));
	}

	public void AddVert(Vector3 position, Color32 color)
	{
		position.y = 0f - position.y;
		vertices.Add(position);
		colors.Add(color);
		if (color.a != byte.MaxValue)
		{
			_alphaInVertexColor = true;
		}
		uvs.Add(new Vector2(Mathf.Lerp(uvRect.xMin, uvRect.xMax, (position.x - contentRect.xMin) / contentRect.width), Mathf.Lerp(uvRect.yMax, uvRect.yMin, (0f - position.y - contentRect.yMin) / contentRect.height)));
	}

	public void AddVert(Vector3 position, Color32 color, Vector2 uv)
	{
		position.y = 0f - position.y;
		vertices.Add(position);
		uvs.Add(new Vector2(uv.x, uv.y));
		colors.Add(color);
		if (color.a != byte.MaxValue)
		{
			_alphaInVertexColor = true;
		}
	}

	public void AddQuad(Rect vertRect)
	{
		AddVert(new Vector3(vertRect.xMin, vertRect.yMax, 0f));
		AddVert(new Vector3(vertRect.xMin, vertRect.yMin, 0f));
		AddVert(new Vector3(vertRect.xMax, vertRect.yMin, 0f));
		AddVert(new Vector3(vertRect.xMax, vertRect.yMax, 0f));
	}

	public void AddQuad(Rect vertRect, Color32 color)
	{
		AddVert(new Vector3(vertRect.xMin, vertRect.yMax, 0f), color);
		AddVert(new Vector3(vertRect.xMin, vertRect.yMin, 0f), color);
		AddVert(new Vector3(vertRect.xMax, vertRect.yMin, 0f), color);
		AddVert(new Vector3(vertRect.xMax, vertRect.yMax, 0f), color);
	}

	public void AddQuad(Rect vertRect, Color32 color, Rect uvRect)
	{
		vertices.Add(new Vector3(vertRect.xMin, 0f - vertRect.yMax, 0f));
		vertices.Add(new Vector3(vertRect.xMin, 0f - vertRect.yMin, 0f));
		vertices.Add(new Vector3(vertRect.xMax, 0f - vertRect.yMin, 0f));
		vertices.Add(new Vector3(vertRect.xMax, 0f - vertRect.yMax, 0f));
		uvs.Add(new Vector2(uvRect.xMin, uvRect.yMin));
		uvs.Add(new Vector2(uvRect.xMin, uvRect.yMax));
		uvs.Add(new Vector2(uvRect.xMax, uvRect.yMax));
		uvs.Add(new Vector2(uvRect.xMax, uvRect.yMin));
		colors.Add(color);
		colors.Add(color);
		colors.Add(color);
		colors.Add(color);
		if (color.a != byte.MaxValue)
		{
			_alphaInVertexColor = true;
		}
	}

	internal List<Vector4> FixUVForArbitraryQuad()
	{
		Vector4 one = Vector4.one;
		Vector2 vector = vertices[2] - vertices[0];
		Vector2 vector2 = vertices[1] - vertices[3];
		Vector2 vector3 = vertices[0] - vertices[3];
		float num = vector.x * vector2.y - vector.y * vector2.x;
		if (num != 0f)
		{
			float num2 = (vector.x * vector3.y - vector.y * vector3.x) / num;
			if (num2 > 0f && num2 < 1f)
			{
				float num3 = (vector2.x * vector3.y - vector2.y * vector3.x) / num;
				if (num3 > 0f && num3 < 1f)
				{
					one.x = 1f / (1f - num3);
					one.y = 1f / num2;
					one.z = 1f / num3;
					one.w = 1f / (1f - num2);
				}
			}
		}
		for (int i = 0; i < 4; i++)
		{
			Vector4 value = uvs[i];
			float num4 = one[i];
			value.x *= num4;
			value.y *= num4;
			value.w = num4;
			helperV4List[i] = value;
		}
		return helperV4List;
	}

	public void RepeatColors(Color32[] value, int startIndex, int count)
	{
		int num = Mathf.Min(startIndex + count, vertices.Count);
		int num2 = value.Length;
		int num3 = 0;
		for (int i = startIndex; i < num; i++)
		{
			Color32 value2 = value[num3++ % num2];
			colors[i] = value2;
			if (value2.a != byte.MaxValue)
			{
				_alphaInVertexColor = true;
			}
		}
	}

	public void AddTriangle(int idx0, int idx1, int idx2)
	{
		triangles.Add(idx0);
		triangles.Add(idx1);
		triangles.Add(idx2);
	}

	public void AddTriangles(int[] idxList, int startVertexIndex = 0)
	{
		if (startVertexIndex != 0)
		{
			if (startVertexIndex < 0)
			{
				startVertexIndex = vertices.Count + startVertexIndex;
			}
			int num = idxList.Length;
			for (int i = 0; i < num; i++)
			{
				triangles.Add(idxList[i] + startVertexIndex);
			}
		}
		else
		{
			triangles.AddRange(idxList);
		}
	}

	public void AddTriangles(int startVertexIndex = 0)
	{
		int count = vertices.Count;
		if (startVertexIndex < 0)
		{
			startVertexIndex = count + startVertexIndex;
		}
		for (int i = startVertexIndex; i < count; i += 4)
		{
			triangles.Add(i);
			triangles.Add(i + 1);
			triangles.Add(i + 2);
			triangles.Add(i + 2);
			triangles.Add(i + 3);
			triangles.Add(i);
		}
	}

	public Vector3 GetPosition(int index)
	{
		if (index < 0)
		{
			index = vertices.Count + index;
		}
		Vector3 result = vertices[index];
		result.y = 0f - result.y;
		return result;
	}

	public Vector2 GetUVAtPosition(Vector2 position, bool usePercent)
	{
		if (usePercent)
		{
			return new Vector2(Mathf.Lerp(uvRect.xMin, uvRect.xMax, position.x), Mathf.Lerp(uvRect.yMax, uvRect.yMin, position.y));
		}
		return new Vector2(Mathf.Lerp(uvRect.xMin, uvRect.xMax, (position.x - contentRect.xMin) / contentRect.width), Mathf.Lerp(uvRect.yMax, uvRect.yMin, (position.y - contentRect.yMin) / contentRect.height));
	}

	public void Append(VertexBuffer vb)
	{
		int count = vertices.Count;
		vertices.AddRange(vb.vertices);
		uvs.AddRange(vb.uvs);
		uvs2.AddRange(vb.uvs2);
		colors.AddRange(vb.colors);
		if (count != 0)
		{
			int count2 = vb.triangles.Count;
			for (int i = 0; i < count2; i++)
			{
				triangles.Add(vb.triangles[i] + count);
			}
		}
		else
		{
			triangles.AddRange(vb.triangles);
		}
		if (vb._alphaInVertexColor)
		{
			_alphaInVertexColor = true;
		}
	}

	public void Insert(VertexBuffer vb)
	{
		vertices.InsertRange(0, vb.vertices);
		uvs.InsertRange(0, vb.uvs);
		uvs2.InsertRange(0, vb.uvs2);
		colors.InsertRange(0, vb.colors);
		int count = triangles.Count;
		if (count != 0)
		{
			int count2 = vb.vertices.Count;
			for (int i = 0; i < count; i++)
			{
				triangles[i] += count2;
			}
		}
		triangles.InsertRange(0, vb.triangles);
		if (vb._alphaInVertexColor)
		{
			_alphaInVertexColor = true;
		}
	}
}
