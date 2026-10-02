using UnityEngine;

namespace FairyGUI;

public class GImageBlurFilter : IFilter
{
	private static readonly int _blurSizeVar = Shader.PropertyToID("_Size");

	private DisplayObject _target;

	private Material _material;

	private readonly float _blurSize;

	public DisplayObject target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
			_material = new Material(ShaderConfig.GetShader("FairyGUI/GImageBlur"));
			_material.SetFloat(_blurSizeVar, _blurSize);
			_material.hideFlags = DisplayObject.hideFlags;
			_target.material = _material;
		}
	}

	public GImageBlurFilter(float blurSize)
	{
		_blurSize = blurSize;
	}

	public void Update()
	{
	}

	public void Dispose()
	{
		_target = null;
		if (Application.isPlaying)
		{
			Object.Destroy(_material);
		}
		else
		{
			Object.DestroyImmediate(_material);
		}
	}
}
