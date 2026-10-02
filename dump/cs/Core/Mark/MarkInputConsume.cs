using UnityEngine;

namespace Core.Mark;

public static class MarkInputConsume
{
	private static int _consumeFrame = -1;

	public static void Consume()
	{
		_consumeFrame = Time.frameCount;
	}

	public static bool IsConsumed()
	{
		return _consumeFrame == Time.frameCount;
	}
}
