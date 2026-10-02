using System;
using UnityEngine;

namespace FairyGUI;

[ExecuteInEditMode]
[AddComponentMenu("FairyGUI/UI Content Scaler")]
public class UIContentScaler : MonoBehaviour
{
	public enum ScaleMode
	{
		ConstantPixelSize,
		ScaleWithScreenSize,
		ConstantPhysicalSize
	}

	public enum ScreenMatchMode
	{
		MatchWidthOrHeight,
		MatchWidth,
		MatchHeight
	}

	public ScaleMode scaleMode;

	public ScreenMatchMode screenMatchMode;

	public int designResolutionX;

	public int designResolutionY;

	public int fallbackScreenDPI = 96;

	public int defaultSpriteDPI = 96;

	public float constantScaleFactor = 1f;

	public bool ignoreOrientation;

	[NonSerialized]
	public static float scaleFactor = 1f;

	[NonSerialized]
	public static int scaleLevel = 0;

	[NonSerialized]
	private bool _changed;

	private void OnEnable()
	{
		if (Application.isPlaying)
		{
			UIContentScaler component = Stage.inst.gameObject.GetComponent<UIContentScaler>();
			if (component != this)
			{
				component.scaleMode = scaleMode;
				if (scaleMode == ScaleMode.ScaleWithScreenSize)
				{
					component.designResolutionX = designResolutionX;
					component.designResolutionY = designResolutionY;
					component.screenMatchMode = screenMatchMode;
					component.ignoreOrientation = ignoreOrientation;
				}
				else if (scaleMode == ScaleMode.ConstantPhysicalSize)
				{
					component.fallbackScreenDPI = fallbackScreenDPI;
					component.defaultSpriteDPI = defaultSpriteDPI;
				}
				else
				{
					component.constantScaleFactor = constantScaleFactor;
				}
				component.ApplyChange();
				GRoot.inst.ApplyContentScaleFactor();
			}
		}
		else
		{
			_changed = true;
		}
	}

	private void Update()
	{
		if (_changed)
		{
			_changed = false;
			ApplyChange();
		}
	}

	private void OnDestroy()
	{
		if (!Application.isPlaying)
		{
			scaleFactor = 1f;
			scaleLevel = 0;
		}
	}

	public void ApplyModifiedProperties()
	{
		_changed = true;
	}

	public void ApplyChange()
	{
		float num;
		float num2;
		if (Application.isPlaying)
		{
			num = Stage.inst.width;
			num2 = Stage.inst.height;
		}
		else
		{
			num = Screen.width;
			num2 = Screen.height;
		}
		if (scaleMode == ScaleMode.ScaleWithScreenSize)
		{
			if (designResolutionX == 0 || designResolutionY == 0)
			{
				return;
			}
			int num3 = designResolutionX;
			int num4 = designResolutionY;
			if (!ignoreOrientation && ((num > num2 && num3 < num4) || (num < num2 && num3 > num4)))
			{
				int num5 = num3;
				num3 = num4;
				num4 = num5;
			}
			if (screenMatchMode == ScreenMatchMode.MatchWidthOrHeight)
			{
				float a = num / (float)num3;
				float b = num2 / (float)num4;
				scaleFactor = Mathf.Min(a, b);
			}
			else if (screenMatchMode == ScreenMatchMode.MatchWidth)
			{
				scaleFactor = num / (float)num3;
			}
			else
			{
				scaleFactor = num2 / (float)num4;
			}
		}
		else if (scaleMode == ScaleMode.ConstantPhysicalSize)
		{
			float num6 = Screen.dpi;
			if (num6 == 0f)
			{
				num6 = fallbackScreenDPI;
			}
			if (num6 == 0f)
			{
				num6 = 96f;
			}
			scaleFactor = num6 / (float)((defaultSpriteDPI == 0) ? 96 : defaultSpriteDPI);
		}
		else
		{
			scaleFactor = constantScaleFactor;
		}
		if (scaleFactor > 10f)
		{
			scaleFactor = 10f;
		}
		UpdateScaleLevel();
		StageCamera.screenSizeVer++;
	}

	private void UpdateScaleLevel()
	{
		if (scaleFactor > 3f)
		{
			scaleLevel = 3;
		}
		else if (scaleFactor > 2f)
		{
			scaleLevel = 2;
		}
		else if (scaleFactor > 1f)
		{
			scaleLevel = 1;
		}
		else
		{
			scaleLevel = 0;
		}
	}
}
