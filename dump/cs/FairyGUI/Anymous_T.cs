namespace FairyGUI;

internal class Anymous_T
{
	public float interval;

	public int repeat;

	public TimerCallback callback;

	public object param;

	public float elapsed;

	public bool deleted;

	public void set(float interval, int repeat, TimerCallback callback, object param)
	{
		this.interval = interval;
		this.repeat = repeat;
		this.callback = callback;
		this.param = param;
	}
}
