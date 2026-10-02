using System;
using System.Collections.Generic;
using FairyGUI.Utils;
using UI;
using UnityEngine;

namespace FairyGUI;

public class GGraph : GObject, IColorGear
{
	public string VideoKey;

	public bool DisposeCirMovie;

	public string RenderKey;

	private Image _renderImage;

	public RenderTexture RenderTexture;

	public Transform RenderModelRoot;

	private Camera _renderCamera;

	private Shape _shape;

	public Color color
	{
		get
		{
			if (_shape != null)
			{
				return _shape.color;
			}
			return Color.clear;
		}
		set
		{
			if (_shape != null && _shape.color != value)
			{
				_shape.color = value;
				UpdateGear(4);
			}
		}
	}

	public Shape shape => _shape;

	public void FullScreen()
	{
		SetPivot(0.5f, 0.5f, asAnchor: true);
		(float, float) tuple = UIHelper.ExpandToAspectRatio(GRoot.inst.width, GRoot.inst.height);
		SetSize(tuple.Item1, tuple.Item2);
		Center();
	}

	public void DisposeVideo()
	{
		DisposeCirMovie = true;
		color = Color.clear;
		data = null;
	}

	public void TryCreateDisplayRender(string key, Camera renderCamera)
	{
		RenderKey = key;
		_renderCamera = renderCamera;
		if (_renderImage == null)
		{
			_renderImage = new Image();
			SetNativeObject(_renderImage);
			_renderImage.SetSize(base.width, base.height);
			_renderImage.SetXY(base.x, base.y);
		}
		if (RenderTexture == null)
		{
			CreateTexture();
		}
		if (RenderModelRoot == null)
		{
			RenderModelRoot = new GameObject(key).transform;
		}
	}

	public void RotateModel(float rotateValue)
	{
		if (rotateValue != 0f && RenderModelRoot != null)
		{
			Vector3 eulerAngles = RenderModelRoot.localRotation.eulerAngles;
			eulerAngles.y += rotateValue;
			RenderModelRoot.localRotation = Quaternion.Euler(eulerAngles);
		}
	}

	public void RotateChildrenModel(float rotateValue)
	{
		if (rotateValue != 0f && RenderModelRoot != null && RenderModelRoot.childCount > 0)
		{
			Transform child = RenderModelRoot.GetChild(0);
			if (!(child == null))
			{
				Vector3 eulerAngles = child.localRotation.eulerAngles;
				eulerAngles.y += rotateValue;
				child.localRotation = Quaternion.Euler(eulerAngles);
			}
		}
	}

	public void StartRender(bool update)
	{
		if (update)
		{
			Timers.inst.AddUpdate(Render);
		}
		else
		{
			Timers.inst.Remove(Render);
		}
		Render();
	}

	public void Render(object param = null)
	{
		if (!(RenderTexture == null) && !(_renderCamera == null))
		{
			_renderCamera.targetTexture = RenderTexture;
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = RenderTexture;
			GL.Clear(clearDepth: true, clearColor: true, Color.clear);
			_renderCamera.Render();
			RenderTexture.active = active;
		}
	}

	private void CreateTexture()
	{
		if (!(RenderTexture != null))
		{
			RenderTexture = new RenderTexture((int)_width, (int)_height, 24, RenderTextureFormat.ARGB32)
			{
				antiAliasing = 1,
				filterMode = FilterMode.Bilinear,
				anisoLevel = 0,
				useMipMap = false
			};
			_renderImage.texture = new NTexture(RenderTexture);
			_renderImage.blendMode = BlendMode.Normal;
		}
	}

	public void DestroyRenderTexture()
	{
		DestroyRenderModel();
		if (RenderTexture != null)
		{
			UnityEngine.Object.Destroy(RenderTexture);
			RenderTexture = null;
		}
		if (_renderImage != null)
		{
			if (_renderImage.texture != null)
			{
				_renderImage.texture.Dispose();
				_renderImage.texture = null;
			}
			_renderImage.Dispose();
			_renderImage = null;
		}
		Timers.inst.Remove(Render);
	}

	public void DestroyRenderModel()
	{
		if (RenderModelRoot != null)
		{
			UnityEngine.Object.Destroy(RenderModelRoot.gameObject);
			RenderModelRoot = null;
		}
	}

	public override void Dispose()
	{
		DestroyRenderTexture();
		base.Dispose();
	}

	protected override void CreateDisplayObject()
	{
		_shape = new Shape();
		_shape.gOwner = this;
		base.displayObject = _shape;
	}

	public void ReplaceMe(GObject target)
	{
		if (base.parent == null)
		{
			throw new Exception("parent not set");
		}
		target.name = name;
		target.alpha = base.alpha;
		target.rotation = base.rotation;
		target.visible = base.visible;
		target.touchable = base.touchable;
		target.grayed = base.grayed;
		target.SetXY(base.x, base.y);
		target.SetSize(base.width, base.height);
		int childIndex = base.parent.GetChildIndex(this);
		base.parent.AddChildAt(target, childIndex);
		target.relations.CopyFrom(base.relations);
		base.parent.RemoveChild(this, dispose: true);
	}

	public void AddBeforeMe(GObject target)
	{
		if (base.parent == null)
		{
			throw new Exception("parent not set");
		}
		int childIndex = base.parent.GetChildIndex(this);
		base.parent.AddChildAt(target, childIndex);
	}

	public void AddAfterMe(GObject target)
	{
		if (base.parent == null)
		{
			throw new Exception("parent not set");
		}
		int childIndex = base.parent.GetChildIndex(this);
		childIndex++;
		base.parent.AddChildAt(target, childIndex);
	}

	public void SetNativeObject(DisplayObject obj)
	{
		if (base.displayObject == obj)
		{
			return;
		}
		if (_shape != null)
		{
			if (_shape.parent != null)
			{
				_shape.parent.RemoveChild(base.displayObject, dispose: true);
			}
			else
			{
				_shape.Dispose();
			}
			_shape.gOwner = null;
			_shape = null;
		}
		base.displayObject = obj;
		if (base.displayObject != null)
		{
			base.displayObject.alpha = base.alpha;
			base.displayObject.rotation = base.rotation;
			base.displayObject.visible = base.visible;
			base.displayObject.touchable = base.touchable;
			base.displayObject.gOwner = this;
		}
		if (base.parent != null)
		{
			base.parent.ChildStateChanged(this);
		}
		HandlePositionChanged();
	}

	public void DrawRect(float aWidth, float aHeight, int lineSize, Color lineColor, Color fillColor)
	{
		SetSize(aWidth, aHeight);
		_shape.DrawRect(lineSize, lineColor, fillColor);
	}

	public void DrawRoundRect(float aWidth, float aHeight, Color fillColor, float[] corner)
	{
		SetSize(aWidth, aHeight);
		shape.DrawRoundRect(0f, Color.white, fillColor, corner[0], corner[1], corner[2], corner[3]);
	}

	public void DrawEllipse(float aWidth, float aHeight, Color fillColor)
	{
		SetSize(aWidth, aHeight);
		_shape.DrawEllipse(fillColor);
	}

	public void DrawPolygon(float aWidth, float aHeight, IList<Vector2> points, Color fillColor)
	{
		SetSize(aWidth, aHeight);
		_shape.DrawPolygon(points, fillColor);
	}

	public void DrawPolygon(float aWidth, float aHeight, IList<Vector2> points, Color fillColor, float lineSize, Color lineColor)
	{
		SetSize(aWidth, aHeight);
		_shape.DrawPolygon(points, fillColor, lineSize, lineColor);
	}

	public override void Setup_BeforeAdd(ByteBuffer buffer, int beginPos)
	{
		base.Setup_BeforeAdd(buffer, beginPos);
		buffer.Seek(beginPos, 5);
		int num = buffer.ReadByte();
		if (num == 0)
		{
			return;
		}
		int num2 = buffer.ReadInt();
		Color lineColor = buffer.ReadColor();
		Color color = buffer.ReadColor();
		bool flag = buffer.ReadBool();
		Vector4 vector = default(Vector4);
		if (flag)
		{
			for (int i = 0; i < 4; i++)
			{
				vector[i] = buffer.ReadFloat();
			}
		}
		switch (num)
		{
		case 1:
			if (flag)
			{
				_shape.DrawRoundRect(num2, lineColor, color, vector.x, vector.y, vector.z, vector.w);
			}
			else
			{
				_shape.DrawRect(num2, lineColor, color);
			}
			break;
		case 2:
			_shape.DrawEllipse(num2, color, lineColor, color, 0f, 360f);
			break;
		case 3:
		{
			int num5 = buffer.ReadShort() / 2;
			Vector2[] array2 = new Vector2[num5];
			for (int k = 0; k < num5; k++)
			{
				array2[k].Set(buffer.ReadFloat(), buffer.ReadFloat());
			}
			_shape.DrawPolygon(array2, color, num2, lineColor);
			break;
		}
		case 4:
		{
			int sides = buffer.ReadShort();
			float num3 = buffer.ReadFloat();
			int num4 = buffer.ReadShort();
			float[] array = null;
			if (num4 > 0)
			{
				array = new float[num4];
				for (int j = 0; j < num4; j++)
				{
					array[j] = buffer.ReadFloat();
				}
			}
			_shape.DrawRegularPolygon(sides, num2, color, lineColor, color, num3, array);
			break;
		}
		}
	}
}
