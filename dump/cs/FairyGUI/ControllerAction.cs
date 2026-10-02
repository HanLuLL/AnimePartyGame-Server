using System;
using FairyGUI.Utils;

namespace FairyGUI;

public class ControllerAction
{
	public enum ActionType
	{
		PlayTransition,
		ChangePage
	}

	public string[] fromPage;

	public string[] toPage;

	public static ControllerAction CreateAction(ActionType type)
	{
		return type switch
		{
			ActionType.PlayTransition => new PlayTransitionAction(), 
			ActionType.ChangePage => new ChangePageAction(), 
			_ => null, 
		};
	}

	public void Run(Controller controller, string prevPage, string curPage)
	{
		if ((fromPage == null || fromPage.Length == 0 || Array.IndexOf(fromPage, prevPage) != -1) && (toPage == null || toPage.Length == 0 || Array.IndexOf(toPage, curPage) != -1))
		{
			Enter(controller);
		}
		else
		{
			Leave(controller);
		}
	}

	protected virtual void Enter(Controller controller)
	{
	}

	protected virtual void Leave(Controller controller)
	{
	}

	public virtual void Setup(ByteBuffer buffer)
	{
		int num = buffer.ReadShort();
		fromPage = new string[num];
		for (int i = 0; i < num; i++)
		{
			fromPage[i] = buffer.ReadS();
		}
		num = buffer.ReadShort();
		toPage = new string[num];
		for (int j = 0; j < num; j++)
		{
			toPage[j] = buffer.ReadS();
		}
	}
}
