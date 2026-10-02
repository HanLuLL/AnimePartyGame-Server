namespace FairyGUI;

internal class TransitionItem
{
	public float time;

	public string targetId;

	public TransitionActionType type;

	public TweenConfig tweenConfig;

	public string label;

	public object value;

	public TransitionHook hook;

	public GTweener tweener;

	public GObject target;

	public uint displayLockToken;

	public TransitionItem(TransitionActionType type)
	{
		this.type = type;
		switch (type)
		{
		case TransitionActionType.XY:
		case TransitionActionType.Size:
		case TransitionActionType.Scale:
		case TransitionActionType.Pivot:
		case TransitionActionType.Alpha:
		case TransitionActionType.Rotation:
		case TransitionActionType.Color:
		case TransitionActionType.ColorFilter:
		case TransitionActionType.Skew:
			value = new TValue();
			break;
		case TransitionActionType.Animation:
			value = new TValue_Animation();
			break;
		case TransitionActionType.Shake:
			value = new TValue_Shake();
			break;
		case TransitionActionType.Sound:
			value = new TValue_Sound();
			break;
		case TransitionActionType.Transition:
			value = new TValue_Transition();
			break;
		case TransitionActionType.Visible:
			value = new TValue_Visible();
			break;
		case TransitionActionType.Text:
		case TransitionActionType.Icon:
			value = new TValue_Text();
			break;
		}
	}
}
