using UnityEngine;

namespace FairyGUI;

public class GlassBlurFilter : IFilter
{
	private DisplayObject _target;

	private Material _material;

	public DisplayObject target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
			_material = new Material(ShaderConfig.GetShader("FairyGUI/FrostedGlass"));
			_material.hideFlags = DisplayObject.hideFlags;
			_target.material = _material;
		}
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
