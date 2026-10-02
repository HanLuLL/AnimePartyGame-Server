namespace FairyGUI;

public class GearTweenConfig
{
	public bool tween;

	public EaseType easeType;

	public CustomEase customEase;

	public float duration;

	public float delay;

	internal uint _displayLockToken;

	internal GTweener _tweener;

	public GearTweenConfig()
	{
		tween = true;
		easeType = EaseType.QuadOut;
		duration = 0.3f;
		delay = 0f;
	}
}
