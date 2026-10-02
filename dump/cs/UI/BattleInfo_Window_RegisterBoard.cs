using System.Collections.Generic;
using Core;
using FairyGUI;

namespace UI;

public class BattleInfo_Window_RegisterBoard : Window
{
	private List<AttrBoardData> _AttrBoardData;

	protected override void OnInit()
	{
	}

	protected override void OnShown()
	{
		base.OnShown();
	}

	protected override void OnHide()
	{
		base.OnHide();
		RemoveEvent_RegisterBoard();
	}

	private void AddEvent_RegisterBoard()
	{
	}

	private void RemoveEvent_RegisterBoard()
	{
	}

	private void CloseRegisterBoard(EventContext context)
	{
	}

	private void RenderRound(int index, GObject item)
	{
		((GButton)item).title = (index + 1).ToString();
	}

	private void RefreshBoard(EventContext context)
	{
	}

	private void RenderAction(int index, GObject item)
	{
	}
}
