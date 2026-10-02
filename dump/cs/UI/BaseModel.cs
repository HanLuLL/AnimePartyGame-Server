using FairyGUI;

namespace UI;

public class BaseModel<T> where T : GComponent
{
	protected T com;

	private bool hasAddedEventOnShow;

	public T Com => com;

	public BaseModel(T _com)
	{
		com = _com;
	}

	public void Show()
	{
		com.visible = true;
		InitDataOrView();
		if (!hasAddedEventOnShow)
		{
			hasAddedEventOnShow = true;
			AddEvent();
		}
		Refresh();
		OnShow();
	}

	protected virtual void OnShow()
	{
	}

	public void Hide()
	{
		com.visible = false;
		if (hasAddedEventOnShow)
		{
			RemoveEvent();
			hasAddedEventOnShow = false;
		}
	}

	public virtual void InitDataOrView()
	{
	}

	public virtual void Refresh()
	{
	}

	public virtual void AddEvent()
	{
	}

	public virtual void RemoveEvent()
	{
	}
}
