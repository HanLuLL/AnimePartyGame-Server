using System;
using UnityEngine;

namespace FairyGUI;

public class TweenValue
{
	public float x;

	public float y;

	public float z;

	public float w;

	public double d;

	public Vector2 vec2
	{
		get
		{
			return new Vector2(x, y);
		}
		set
		{
			x = value.x;
			y = value.y;
		}
	}

	public Vector3 vec3
	{
		get
		{
			return new Vector3(x, y, z);
		}
		set
		{
			x = value.x;
			y = value.y;
			z = value.z;
		}
	}

	public Vector4 vec4
	{
		get
		{
			return new Vector4(x, y, z, w);
		}
		set
		{
			x = value.x;
			y = value.y;
			z = value.z;
			w = value.w;
		}
	}

	public Color color
	{
		get
		{
			return new Color(x, y, z, w);
		}
		set
		{
			x = value.r;
			y = value.g;
			z = value.b;
			w = value.a;
		}
	}

	public float this[int index]
	{
		get
		{
			return index switch
			{
				0 => x, 
				1 => y, 
				2 => z, 
				3 => w, 
				_ => throw new Exception("Index out of bounds: " + index), 
			};
		}
		set
		{
			switch (index)
			{
			case 0:
				x = value;
				break;
			case 1:
				y = value;
				break;
			case 2:
				z = value;
				break;
			case 3:
				w = value;
				break;
			default:
				throw new Exception("Index out of bounds: " + index);
			}
		}
	}

	public void SetZero()
	{
		x = (y = (z = (w = 0f)));
		d = 0.0;
	}
}
