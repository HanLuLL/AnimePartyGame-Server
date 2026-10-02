using UnityEngine;

namespace FairyGUI;

internal class TValue
{
	public float f1;

	public float f2;

	public float f3;

	public float f4;

	public bool b1;

	public bool b2;

	public bool b3;

	public Vector2 vec2
	{
		get
		{
			return new Vector2(f1, f2);
		}
		set
		{
			f1 = value.x;
			f2 = value.y;
		}
	}

	public Vector4 vec4
	{
		get
		{
			return new Vector4(f1, f2, f3, f4);
		}
		set
		{
			f1 = value.x;
			f2 = value.y;
			f3 = value.z;
			f4 = value.w;
		}
	}

	public Color color
	{
		get
		{
			return new Color(f1, f2, f3, f4);
		}
		set
		{
			f1 = value.r;
			f2 = value.g;
			f3 = value.b;
			f4 = value.a;
		}
	}

	public TValue()
	{
		b1 = true;
		b2 = true;
	}

	public void Copy(TValue source)
	{
		f1 = source.f1;
		f2 = source.f2;
		f3 = source.f3;
		f4 = source.f4;
		b1 = source.b1;
		b2 = source.b2;
	}
}
