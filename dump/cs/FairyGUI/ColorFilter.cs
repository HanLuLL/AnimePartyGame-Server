using System;
using UnityEngine;

namespace FairyGUI;

public class ColorFilter : IFilter
{
	private DisplayObject _target;

	private float[] _matrix;

	private const float LUMA_R = 0.299f;

	private const float LUMA_G = 0.587f;

	private const float LUMA_B = 0.114f;

	private static float[] IDENTITY = new float[20]
	{
		1f, 0f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f,
		0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 1f, 0f
	};

	private static float[] tmp = new float[20];

	public DisplayObject target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
			if (_target is Image || _target is MovieClip)
			{
				_target.graphics.ToggleKeyword("COLOR_FILTER", enabled: true);
			}
			else
			{
				_target.EnterPaintingMode(1, null);
				_target.paintingGraphics.ToggleKeyword("COLOR_FILTER", enabled: true);
			}
			UpdateMatrix();
		}
	}

	public ColorFilter()
	{
		_matrix = new float[20];
		Array.Copy(IDENTITY, _matrix, _matrix.Length);
	}

	public void Dispose()
	{
		if (!_target.isDisposed)
		{
			if (_target is Image || _target is MovieClip)
			{
				_target.graphics.ToggleKeyword("COLOR_FILTER", enabled: false);
			}
			else
			{
				_target.paintingGraphics.ToggleKeyword("COLOR_FILTER", enabled: false);
				_target.LeavePaintingMode(1);
			}
		}
		_target = null;
	}

	public void Update()
	{
	}

	public void Invert()
	{
		_ConcatValues(0, -1f, 0f, 0f, 0f, 1f);
		_ConcatValues(1, 0f, -1f, 0f, 0f, 1f);
		_ConcatValues(2, 0f, 0f, -1f, 0f, 1f);
		_ConcatValues(3, 0f, 0f, 0f, 1f, 0f);
	}

	public void AdjustSaturation(float sat)
	{
		sat += 1f;
		float num = 1f - sat;
		float num2 = num * 0.299f;
		float num3 = num * 0.587f;
		float num4 = num * 0.114f;
		_ConcatValues(0, num2 + sat, num3, num4, 0f, 0f);
		_ConcatValues(1, num2, num3 + sat, num4, 0f, 0f);
		_ConcatValues(2, num2, num3, num4 + sat, 0f, 0f);
		_ConcatValues(3, 0f, 0f, 0f, 1f, 0f);
	}

	public void AdjustContrast(float value)
	{
		float num = value + 1f;
		float f = 0.5019608f * (1f - num);
		_ConcatValues(0, num, 0f, 0f, 0f, f);
		_ConcatValues(1, 0f, num, 0f, 0f, f);
		_ConcatValues(2, 0f, 0f, num, 0f, f);
		_ConcatValues(3, 0f, 0f, 0f, 1f, 0f);
	}

	public void AdjustBrightness(float value)
	{
		_ConcatValues(0, 1f, 0f, 0f, 0f, value);
		_ConcatValues(1, 0f, 1f, 0f, 0f, value);
		_ConcatValues(2, 0f, 0f, 1f, 0f, value);
		_ConcatValues(3, 0f, 0f, 0f, 1f, 0f);
	}

	public void AdjustHue(float value)
	{
		value *= (float)Math.PI;
		float num = Mathf.Cos(value);
		float num2 = Mathf.Sin(value);
		_ConcatValues(0, 0.299f + num * 0.701f + num2 * -0.299f, 0.587f + num * -0.587f + num2 * -0.587f, 0.114f + num * -0.114f + num2 * 0.886f, 0f, 0f);
		_ConcatValues(1, 0.299f + num * -0.299f + num2 * 0.143f, 0.587f + num * 0.413f + num2 * 0.14f, 0.114f + num * -0.114f + num2 * -0.283f, 0f, 0f);
		_ConcatValues(2, 0.299f + num * -0.299f + num2 * -0.701f, 0.587f + num * -0.587f + num2 * 0.587f, 0.114f + num * 0.886f + num2 * 0.114f, 0f, 0f);
		_ConcatValues(3, 0f, 0f, 0f, 1f, 0f);
	}

	public void Tint(Color color, float amount = 1f)
	{
		float num = 1f - amount;
		float num2 = amount * color.r;
		float num3 = amount * color.g;
		float num4 = amount * color.b;
		_ConcatValues(0, num + num2 * 0.299f, num2 * 0.587f, num2 * 0.114f, 0f, 0f);
		_ConcatValues(1, num3 * 0.299f, num + num3 * 0.587f, num3 * 0.114f, 0f, 0f);
		_ConcatValues(2, num4 * 0.299f, num4 * 0.587f, num + num4 * 0.114f, 0f, 0f);
		_ConcatValues(3, 0f, 0f, 0f, 1f, 0f);
	}

	public void Reset()
	{
		Array.Copy(IDENTITY, _matrix, _matrix.Length);
		UpdateMatrix();
	}

	private void _ConcatValues(int index, float f0, float f1, float f2, float f3, float f4)
	{
		int num = index * 5;
		for (int i = 0; i < 5; i++)
		{
			tmp[num + i] = f0 * _matrix[i] + f1 * _matrix[i + 5] + f2 * _matrix[i + 10] + f3 * _matrix[i + 15] + ((i == 4) ? f4 : 0f);
		}
		if (index == 3)
		{
			Array.Copy(tmp, _matrix, tmp.Length);
			UpdateMatrix();
		}
	}

	public void ConcatValues(params float[] values)
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				tmp[num + j] = values[num] * _matrix[j] + values[num + 1] * _matrix[j + 5] + values[num + 2] * _matrix[j + 10] + values[num + 3] * _matrix[j + 15] + ((j == 4) ? values[num + 4] : 0f);
			}
			num += 5;
		}
		Array.Copy(tmp, _matrix, tmp.Length);
		UpdateMatrix();
	}

	private void UpdateMatrix()
	{
		if (_target != null)
		{
			Matrix4x4 value = default(Matrix4x4);
			value.SetRow(0, new Vector4(_matrix[0], _matrix[1], _matrix[2], _matrix[3]));
			value.SetRow(1, new Vector4(_matrix[5], _matrix[6], _matrix[7], _matrix[8]));
			value.SetRow(2, new Vector4(_matrix[10], _matrix[11], _matrix[12], _matrix[13]));
			value.SetRow(3, new Vector4(_matrix[15], _matrix[16], _matrix[17], _matrix[18]));
			Vector4 value2 = new Vector4(_matrix[4], _matrix[9], _matrix[14], _matrix[19]);
			MaterialPropertyBlock materialPropertyBlock = ((!(_target is Image) && !(_target is MovieClip)) ? _target.paintingGraphics.materialPropertyBlock : _target.graphics.materialPropertyBlock);
			materialPropertyBlock.SetMatrix(ShaderConfig.ID_ColorMatrix, value);
			materialPropertyBlock.SetVector(ShaderConfig.ID_ColorOffset, value2);
		}
	}
}
