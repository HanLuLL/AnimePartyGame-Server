namespace FairyGUI;

internal class TweenConfig
{
	public float duration;

	public EaseType easeType;

	public CustomEase customEase;

	public int repeat;

	public bool yoyo;

	public TValue startValue;

	public TValue endValue;

	public string endLabel;

	public TransitionHook endHook;

	public GPath path;

	public TweenConfig()
	{
		easeType = EaseType.QuadOut;
		startValue = new TValue();
		endValue = new TValue();
	}
}
