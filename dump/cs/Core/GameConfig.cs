using System.Collections.Generic;
using UnityEngine;

namespace Core;

public static class GameConfig
{
	public static readonly List<Color> slotColor = new List<Color>(5)
	{
		new Color(1f, 0.2745098f, 0.2745098f, 1f),
		new Color(0.5803922f, 1f, 0.2745098f, 1f),
		new Color(0.2627451f, 0.5254902f, 0.9568627f, 1f),
		new Color(1f, 0.7019608f, 0.2745098f, 1f),
		new Color(0.6283149f, 0.3250267f, 0.8301887f, 1f)
	};

	[ColorUsage(true, true)]
	public static readonly List<Color> roadColor = new List<Color>(4)
	{
		new Color(1.732764f, 0f, 0.02721619f, 1f),
		new Color(0.3549399f, 2.118547f, 0.0776431f, 1f),
		new Color(0f, 0.6002245f, 6.033835f, 1f),
		new Color(1.720795f, 0.720752f, 0.1441504f, 1f)
	};

	public const float goldInterval = 0.1f;

	public const int goldMax = 5;

	public const float moveDistance = 20f;

	public const float moveHeight = 10f;

	public const float KvMaxWidth = 2304f;

	public const float KvMaxHeight = 1200f;

	public const float KvContentWidth = 1920f;

	public const float KvContentHeight = 1080f;

	public const float DefaultKVScale = 1f;

	public static float HomeKvScale => ES3.Load(LocalStore.HomeKvScaleCache, 1f);

	public static Vector2 HomeKvPosition => ES3.Load(LocalStore.HomeKvPositionCache, Vector2.zero);

	public static string HTMLStringRGB(int slot)
	{
		int index = Mathf.Min(slot, slotColor.Count - 1);
		return ColorUtility.ToHtmlStringRGB(slotColor[index]);
	}

	public static void SaveHomeKvScale(float scale)
	{
		ES3.Save(LocalStore.HomeKvScaleCache, scale);
	}

	public static void SaveHomeKvPosition(Vector2 position)
	{
		ES3.Save(LocalStore.HomeKvPositionCache, position);
	}
}
