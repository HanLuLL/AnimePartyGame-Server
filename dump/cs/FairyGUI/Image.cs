using UnityEngine;

namespace FairyGUI;

public class Image : DisplayObject, IMeshFactory
{
	protected Rect? _scale9Grid;

	protected bool _scaleByTile;

	protected Vector2 _textureScale;

	protected int _tileGridIndice;

	protected FillMesh _fillMesh;

	private static int[] TRIANGLES_9_GRID = new int[54]
	{
		4, 0, 1, 1, 5, 4, 5, 1, 2, 2,
		6, 5, 6, 2, 3, 3, 7, 6, 8, 4,
		5, 5, 9, 8, 9, 5, 6, 6, 10, 9,
		10, 6, 7, 7, 11, 10, 12, 8, 9, 9,
		13, 12, 13, 9, 10, 10, 14, 13, 14, 10,
		11, 11, 15, 14
	};

	private static int[] gridTileIndice = new int[9] { -1, 0, -1, 2, 4, 3, -1, 1, -1 };

	private static float[] gridX = new float[4];

	private static float[] gridY = new float[4];

	private static float[] gridTexX = new float[4];

	private static float[] gridTexY = new float[4];

	public NTexture texture
	{
		get
		{
			return base.graphics.texture;
		}
		set
		{
			UpdateTexture(value);
		}
	}

	public Vector2 textureScale
	{
		get
		{
			return _textureScale;
		}
		set
		{
			_textureScale = value;
			base.graphics.SetMeshDirty();
		}
	}

	public Color color
	{
		get
		{
			return base.graphics.color;
		}
		set
		{
			base.graphics.color = value;
			base.graphics.Tint();
		}
	}

	public FillMethod fillMethod
	{
		get
		{
			if (_fillMesh == null)
			{
				return FillMethod.None;
			}
			return _fillMesh.method;
		}
		set
		{
			if (_fillMesh == null)
			{
				if (value == FillMethod.None)
				{
					return;
				}
				_fillMesh = new FillMesh();
			}
			if (_fillMesh.method != value)
			{
				_fillMesh.method = value;
				base.graphics.SetMeshDirty();
			}
		}
	}

	public int fillOrigin
	{
		get
		{
			if (_fillMesh == null)
			{
				return 0;
			}
			return _fillMesh.origin;
		}
		set
		{
			if (_fillMesh == null)
			{
				_fillMesh = new FillMesh();
			}
			if (_fillMesh.origin != value)
			{
				_fillMesh.origin = value;
				base.graphics.SetMeshDirty();
			}
		}
	}

	public bool fillClockwise
	{
		get
		{
			if (_fillMesh == null)
			{
				return true;
			}
			return _fillMesh.clockwise;
		}
		set
		{
			if (_fillMesh == null)
			{
				_fillMesh = new FillMesh();
			}
			if (_fillMesh.clockwise != value)
			{
				_fillMesh.clockwise = value;
				base.graphics.SetMeshDirty();
			}
		}
	}

	public float fillAmount
	{
		get
		{
			if (_fillMesh == null)
			{
				return 0f;
			}
			return _fillMesh.amount;
		}
		set
		{
			if (_fillMesh == null)
			{
				_fillMesh = new FillMesh();
			}
			if (_fillMesh.amount != value)
			{
				_fillMesh.amount = value;
				base.graphics.SetMeshDirty();
			}
		}
	}

	public Rect? scale9Grid
	{
		get
		{
			return _scale9Grid;
		}
		set
		{
			if (_scale9Grid != value)
			{
				_scale9Grid = value;
				base.graphics.SetMeshDirty();
			}
		}
	}

	public bool scaleByTile
	{
		get
		{
			return _scaleByTile;
		}
		set
		{
			if (_scaleByTile != value)
			{
				_scaleByTile = value;
				base.graphics.SetMeshDirty();
			}
		}
	}

	public int tileGridIndice
	{
		get
		{
			return _tileGridIndice;
		}
		set
		{
			if (_tileGridIndice != value)
			{
				_tileGridIndice = value;
				base.graphics.SetMeshDirty();
			}
		}
	}

	public Image()
		: this(null)
	{
	}

	public Image(NTexture texture)
	{
		_flags |= Flags.TouchDisabled;
		CreateGameObject("Image");
		base.graphics = new NGraphics(base.gameObject);
		base.graphics.shader = ShaderConfig.imageShader;
		base.graphics.meshFactory = this;
		_textureScale = Vector2.one;
		if (texture != null)
		{
			UpdateTexture(texture);
		}
	}

	public void SetNativeSize()
	{
		if (base.graphics.texture != null)
		{
			SetSize(base.graphics.texture.width, base.graphics.texture.height);
		}
		else
		{
			SetSize(0f, 0f);
		}
	}

	protected virtual void UpdateTexture(NTexture value)
	{
		if (value != base.graphics.texture)
		{
			base.graphics.texture = value;
			_textureScale = Vector2.one;
			if (_contentRect.width == 0f)
			{
				SetNativeSize();
			}
			InvalidateBatchingState();
		}
	}

	public void OnPopulateMesh(VertexBuffer vb)
	{
		if (_fillMesh != null && _fillMesh.method != FillMethod.None)
		{
			_fillMesh.OnPopulateMesh(vb);
		}
		else if (_scaleByTile)
		{
			NTexture nTexture = base.graphics.texture;
			if (nTexture.root == nTexture && nTexture.nativeTexture != null && nTexture.nativeTexture.wrapMode == TextureWrapMode.Repeat)
			{
				Rect uvRect = vb.uvRect;
				uvRect.width *= vb.contentRect.width / (float)nTexture.width * _textureScale.x;
				uvRect.height *= vb.contentRect.height / (float)nTexture.height * _textureScale.y;
				vb.AddQuad(vb.contentRect, vb.vertexColor, uvRect);
				vb.AddTriangles();
			}
			else
			{
				Rect contentRect = vb.contentRect;
				contentRect.width *= _textureScale.x;
				contentRect.height *= _textureScale.y;
				TileFill(vb, contentRect, vb.uvRect, nTexture.width, nTexture.height);
				vb.AddTriangles();
			}
		}
		else if (_scale9Grid.HasValue)
		{
			SliceFill(vb);
		}
		else
		{
			base.graphics.OnPopulateMesh(vb);
		}
	}

	public void SliceFill(VertexBuffer vb)
	{
		NTexture nTexture = base.graphics.texture;
		Rect value = _scale9Grid.Value;
		Rect contentRect = vb.contentRect;
		contentRect.width *= _textureScale.x;
		contentRect.height *= _textureScale.y;
		Rect uvRect = vb.uvRect;
		float num = nTexture.width;
		float num2 = nTexture.height;
		if (base.graphics.flip != FlipType.None)
		{
			if (base.graphics.flip == FlipType.Horizontal || base.graphics.flip == FlipType.Both)
			{
				value.x = num - value.xMax;
				value.xMax = value.x + value.width;
			}
			if (base.graphics.flip == FlipType.Vertical || base.graphics.flip == FlipType.Both)
			{
				value.y = num2 - value.yMax;
				value.yMax = value.y + value.height;
			}
		}
		float num3 = uvRect.width / num;
		float num4 = uvRect.height / num2;
		float xMax = uvRect.xMax;
		float yMax = uvRect.yMax;
		float xMax2 = value.xMax;
		float yMax2 = value.yMax;
		gridTexX[0] = uvRect.x;
		gridTexX[1] = uvRect.x + value.x * num3;
		gridTexX[2] = uvRect.x + xMax2 * num3;
		gridTexX[3] = xMax;
		gridTexY[0] = yMax;
		gridTexY[1] = yMax - value.y * num4;
		gridTexY[2] = yMax - yMax2 * num4;
		gridTexY[3] = uvRect.y;
		if (contentRect.width >= num - value.width)
		{
			gridX[1] = value.x;
			gridX[2] = contentRect.width - (num - xMax2);
			gridX[3] = contentRect.width;
		}
		else
		{
			float num5 = value.x / (num - xMax2);
			num5 = contentRect.width * num5 / (1f + num5);
			gridX[1] = num5;
			gridX[2] = num5;
			gridX[3] = contentRect.width;
		}
		if (contentRect.height >= num2 - value.height)
		{
			gridY[1] = value.y;
			gridY[2] = contentRect.height - (num2 - yMax2);
			gridY[3] = contentRect.height;
		}
		else
		{
			float num6 = value.y / (num2 - yMax2);
			num6 = contentRect.height * num6 / (1f + num6);
			gridY[1] = num6;
			gridY[2] = num6;
			gridY[3] = contentRect.height;
		}
		if (_tileGridIndice == 0)
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					vb.AddVert(new Vector2(gridX[j] / _textureScale.x, gridY[i] / _textureScale.y), vb.vertexColor, new Vector2(gridTexX[j], gridTexY[i]));
				}
			}
			vb.AddTriangles(TRIANGLES_9_GRID);
			return;
		}
		for (int k = 0; k < 9; k++)
		{
			int num7 = k % 3;
			int num8 = k / 3;
			int num9 = gridTileIndice[k];
			Rect rect = Rect.MinMaxRect(gridX[num7], gridY[num8], gridX[num7 + 1], gridY[num8 + 1]);
			Rect uvRect2 = Rect.MinMaxRect(gridTexX[num7], gridTexY[num8 + 1], gridTexX[num7 + 1], gridTexY[num8]);
			if (num9 != -1 && (_tileGridIndice & (1 << num9)) != 0)
			{
				TileFill(vb, rect, uvRect2, (num9 == 0 || num9 == 1 || num9 == 4) ? value.width : rect.width, (num9 == 2 || num9 == 3 || num9 == 4) ? value.height : rect.height);
				continue;
			}
			rect.x /= _textureScale.x;
			rect.y /= _textureScale.y;
			rect.width /= _textureScale.x;
			rect.height /= _textureScale.y;
			vb.AddQuad(rect, vb.vertexColor, uvRect2);
		}
		vb.AddTriangles();
	}

	private void TileFill(VertexBuffer vb, Rect contentRect, Rect uvRect, float sourceW, float sourceH)
	{
		int num = Mathf.CeilToInt(contentRect.width / sourceW);
		int num2 = Mathf.CeilToInt(contentRect.height / sourceH);
		float num3 = contentRect.width - (float)(num - 1) * sourceW;
		float num4 = contentRect.height - (float)(num2 - 1) * sourceH;
		float xMax = uvRect.xMax;
		float yMax = uvRect.yMax;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				Rect uvRect2 = uvRect;
				if (i == num - 1)
				{
					uvRect2.xMax = Mathf.Lerp(uvRect.x, xMax, num3 / sourceW);
				}
				if (j == num2 - 1)
				{
					uvRect2.yMin = Mathf.Lerp(uvRect.y, yMax, 1f - num4 / sourceH);
				}
				Rect vertRect = new Rect(contentRect.x + (float)i * sourceW, contentRect.y + (float)j * sourceH, (i == num - 1) ? num3 : sourceW, (j == num2 - 1) ? num4 : sourceH);
				vertRect.x /= _textureScale.x;
				vertRect.y /= _textureScale.y;
				vertRect.width /= _textureScale.x;
				vertRect.height /= _textureScale.y;
				vb.AddQuad(vertRect, vb.vertexColor, uvRect2);
			}
		}
	}
}
