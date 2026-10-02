namespace FairyGUI;

internal class TweenPropTypeUtils
{
	internal static void SetProps(object target, TweenPropType propType, TweenValue value)
	{
		if (target is GObject gObject)
		{
			switch (propType)
			{
			case TweenPropType.X:
				gObject.x = value.x;
				break;
			case TweenPropType.Y:
				gObject.y = value.x;
				break;
			case TweenPropType.Z:
				gObject.z = value.x;
				break;
			case TweenPropType.XY:
				gObject.xy = value.vec2;
				break;
			case TweenPropType.Position:
				gObject.position = value.vec3;
				break;
			case TweenPropType.Width:
				gObject.width = value.x;
				break;
			case TweenPropType.Height:
				gObject.height = value.x;
				break;
			case TweenPropType.Size:
				gObject.size = value.vec2;
				break;
			case TweenPropType.ScaleX:
				gObject.scaleX = value.x;
				break;
			case TweenPropType.ScaleY:
				gObject.scaleY = value.x;
				break;
			case TweenPropType.Scale:
				gObject.scale = value.vec2;
				break;
			case TweenPropType.Rotation:
				gObject.rotation = value.x;
				break;
			case TweenPropType.RotationX:
				gObject.rotationX = value.x;
				break;
			case TweenPropType.RotationY:
				gObject.rotationY = value.x;
				break;
			case TweenPropType.Alpha:
				gObject.alpha = value.x;
				break;
			case TweenPropType.Progress:
				gObject.asProgress.Update(value.d, 0.0);
				break;
			}
		}
	}
}
