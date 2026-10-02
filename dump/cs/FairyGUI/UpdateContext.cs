using System;
using System.Collections.Generic;
using FairyGUI.Utils;
using UnityEngine;

namespace FairyGUI;

public class UpdateContext
{
	public struct ClipInfo
	{
		public Rect rect;

		public Vector4 clipBox;

		public bool soft;

		public Vector4 softness;

		public uint clipId;

		public int rectMaskDepth;

		public int referenceValue;

		public bool reversed;
	}

	private Stack<ClipInfo> _clipStack;

	public bool clipped;

	public ClipInfo clipInfo;

	public int renderingOrder;

	public int batchingDepth;

	public int rectMaskDepth;

	public int stencilReferenceValue;

	public int stencilCompareValue;

	public float alpha;

	public bool grayed;

	public static UpdateContext current;

	public static bool working;

	private static Action _tmpBegin;

	public static event Action OnBegin;

	public static event Action OnEnd;

	public UpdateContext()
	{
		_clipStack = new Stack<ClipInfo>();
	}

	public void Begin()
	{
		current = this;
		renderingOrder = 0;
		batchingDepth = 0;
		rectMaskDepth = 0;
		stencilReferenceValue = 0;
		alpha = 1f;
		grayed = false;
		clipped = false;
		_clipStack.Clear();
		Stats.ObjectCount = 0;
		Stats.GraphicsCount = 0;
		_tmpBegin = UpdateContext.OnBegin;
		UpdateContext.OnBegin = null;
		while (_tmpBegin != null)
		{
			_tmpBegin();
			_tmpBegin = UpdateContext.OnBegin;
			UpdateContext.OnBegin = null;
		}
		working = true;
	}

	public void End()
	{
		working = false;
		if (UpdateContext.OnEnd != null)
		{
			UpdateContext.OnEnd();
		}
		UpdateContext.OnEnd = null;
	}

	public void EnterClipping(uint clipId, Rect clipRect, Vector4? softness)
	{
		_clipStack.Push(clipInfo);
		if (rectMaskDepth > 0)
		{
			clipRect = ToolSet.Intersection(ref clipInfo.rect, ref clipRect);
		}
		clipped = true;
		clipInfo.rectMaskDepth = ++rectMaskDepth;
		clipInfo.rect = clipRect;
		clipRect.x += clipRect.width * 0.5f;
		clipRect.y += clipRect.height * 0.5f;
		clipRect.width *= 0.5f;
		clipRect.height *= 0.5f;
		if (clipRect.width == 0f || clipRect.height == 0f)
		{
			clipInfo.clipBox = new Vector4(-2f, -2f, 0f, 0f);
		}
		else
		{
			clipInfo.clipBox = new Vector4((0f - clipRect.x) / clipRect.width, (0f - clipRect.y) / clipRect.height, 1f / clipRect.width, 1f / clipRect.height);
		}
		clipInfo.clipId = clipId;
		clipInfo.soft = softness.HasValue;
		if (clipInfo.soft)
		{
			clipInfo.softness = softness.Value;
			float num = clipInfo.rect.width * (float)Screen.height * 0.25f;
			float num2 = clipInfo.rect.height * (float)Screen.height * 0.25f;
			if (clipInfo.softness.x > 0f)
			{
				clipInfo.softness.x = num / clipInfo.softness.x;
			}
			else
			{
				clipInfo.softness.x = 10000f;
			}
			if (clipInfo.softness.y > 0f)
			{
				clipInfo.softness.y = num2 / clipInfo.softness.y;
			}
			else
			{
				clipInfo.softness.y = 10000f;
			}
			if (clipInfo.softness.z > 0f)
			{
				clipInfo.softness.z = num / clipInfo.softness.z;
			}
			else
			{
				clipInfo.softness.z = 10000f;
			}
			if (clipInfo.softness.w > 0f)
			{
				clipInfo.softness.w = num2 / clipInfo.softness.w;
			}
			else
			{
				clipInfo.softness.w = 10000f;
			}
		}
	}

	public void EnterClipping(uint clipId, bool reversedMask)
	{
		_clipStack.Push(clipInfo);
		if (stencilReferenceValue == 0)
		{
			stencilReferenceValue = 1;
		}
		else
		{
			stencilReferenceValue <<= 1;
		}
		if (reversedMask)
		{
			if (clipInfo.reversed)
			{
				stencilCompareValue = (stencilReferenceValue >> 1) - 1;
			}
			else
			{
				stencilCompareValue = stencilReferenceValue - 1;
			}
		}
		else
		{
			stencilCompareValue = (stencilReferenceValue << 1) - 1;
		}
		clipInfo.clipId = clipId;
		clipInfo.referenceValue = stencilReferenceValue;
		clipInfo.reversed = reversedMask;
		clipped = true;
	}

	public void LeaveClipping()
	{
		clipInfo = _clipStack.Pop();
		stencilReferenceValue = clipInfo.referenceValue;
		rectMaskDepth = clipInfo.rectMaskDepth;
		clipped = stencilReferenceValue != 0 || rectMaskDepth != 0;
	}

	public void EnterPaintingMode()
	{
		_clipStack.Push(clipInfo);
		clipInfo.rectMaskDepth = 0;
		clipInfo.referenceValue = 0;
		clipInfo.reversed = false;
		clipped = false;
	}

	public void LeavePaintingMode()
	{
		clipInfo = _clipStack.Pop();
		stencilReferenceValue = clipInfo.referenceValue;
		rectMaskDepth = clipInfo.rectMaskDepth;
		clipped = stencilReferenceValue != 0 || rectMaskDepth != 0;
	}

	public void ApplyClippingProperties(Material mat, bool isStdMaterial)
	{
		if (rectMaskDepth > 0)
		{
			mat.SetVector(ShaderConfig.ID_ClipBox, clipInfo.clipBox);
			if (clipInfo.soft)
			{
				mat.SetVector(ShaderConfig.ID_ClipSoftness, clipInfo.softness);
			}
		}
		if (stencilReferenceValue > 0)
		{
			mat.SetInt(ShaderConfig.ID_StencilComp, 3);
			mat.SetInt(ShaderConfig.ID_Stencil, stencilCompareValue);
			mat.SetInt(ShaderConfig.ID_Stencil2, stencilCompareValue);
			mat.SetInt(ShaderConfig.ID_StencilOp, 0);
			mat.SetInt(ShaderConfig.ID_StencilReadMask, stencilReferenceValue | (stencilReferenceValue - 1));
			mat.SetInt(ShaderConfig.ID_ColorMask, 15);
		}
		else
		{
			mat.SetInt(ShaderConfig.ID_StencilComp, 8);
			mat.SetInt(ShaderConfig.ID_Stencil, 0);
			mat.SetInt(ShaderConfig.ID_Stencil2, 0);
			mat.SetInt(ShaderConfig.ID_StencilOp, 0);
			mat.SetInt(ShaderConfig.ID_StencilReadMask, 255);
			mat.SetInt(ShaderConfig.ID_ColorMask, 15);
		}
		if (isStdMaterial)
		{
			return;
		}
		if (rectMaskDepth > 0)
		{
			if (clipInfo.soft)
			{
				mat.EnableKeyword("SOFT_CLIPPED");
			}
			else
			{
				mat.EnableKeyword("CLIPPED");
			}
		}
		else
		{
			mat.DisableKeyword("CLIPPED");
			mat.DisableKeyword("SOFT_CLIPPED");
		}
	}

	public void ApplyGrayProperties(Material mat)
	{
		if (grayed)
		{
			mat.EnableKeyword("GRAYED");
		}
		else
		{
			mat.DisableKeyword("GRAYED");
		}
	}

	public void ApplyAlphaMaskProperties(Material mat, bool erasing)
	{
		if (!erasing)
		{
			if (stencilReferenceValue == 1)
			{
				mat.SetInt(ShaderConfig.ID_StencilComp, 8);
				mat.SetInt(ShaderConfig.ID_Stencil, 1);
				mat.SetInt(ShaderConfig.ID_StencilOp, 2);
				mat.SetInt(ShaderConfig.ID_StencilReadMask, 255);
				mat.SetInt(ShaderConfig.ID_ColorMask, 0);
				return;
			}
			if ((stencilReferenceValue != 0) & _clipStack.Peek().reversed)
			{
				mat.SetInt(ShaderConfig.ID_StencilComp, 6);
			}
			else
			{
				mat.SetInt(ShaderConfig.ID_StencilComp, 3);
			}
			mat.SetInt(ShaderConfig.ID_Stencil, stencilReferenceValue | (stencilReferenceValue - 1));
			mat.SetInt(ShaderConfig.ID_StencilOp, 2);
			mat.SetInt(ShaderConfig.ID_StencilReadMask, stencilReferenceValue - 1);
			mat.SetInt(ShaderConfig.ID_ColorMask, 0);
		}
		else if ((stencilReferenceValue != 0) & _clipStack.Peek().reversed)
		{
			int value = stencilReferenceValue | (stencilReferenceValue - 1);
			mat.SetInt(ShaderConfig.ID_StencilComp, 3);
			mat.SetInt(ShaderConfig.ID_Stencil, value);
			mat.SetInt(ShaderConfig.ID_StencilOp, 1);
			mat.SetInt(ShaderConfig.ID_StencilReadMask, value);
			mat.SetInt(ShaderConfig.ID_ColorMask, 0);
		}
		else
		{
			int value2 = stencilReferenceValue - 1;
			mat.SetInt(ShaderConfig.ID_StencilComp, 3);
			mat.SetInt(ShaderConfig.ID_Stencil, value2);
			mat.SetInt(ShaderConfig.ID_StencilOp, 2);
			mat.SetInt(ShaderConfig.ID_StencilReadMask, value2);
			mat.SetInt(ShaderConfig.ID_ColorMask, 0);
		}
	}
}
