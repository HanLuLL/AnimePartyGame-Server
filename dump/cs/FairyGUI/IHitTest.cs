using UnityEngine;

namespace FairyGUI;

public interface IHitTest
{
	bool HitTest(Rect contentRect, Vector2 localPoint);
}
