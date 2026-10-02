using System;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;

namespace UI;

public class UIGuild_Button_PlayerItem : GButton
{
	private long _playerId;

	private bool _selectable;

	private Action<long> _onSelected;

	public Controller status;

	public GComponent com_PlayerLabel;

	public GGroup group_btn;

	public Transition Cut_in;

	public const string URL = "ui://w5bj58pzhtd12v";

	public void Bind(FriendData friendData, bool selected, Action<long> onSelected)
	{
		if (friendData == null || friendData.playerId == 0L)
		{
			ClearRenderedData();
			return;
		}
		_playerId = friendData.playerId;
		_selectable = !friendData.IsBusy;
		_onSelected = onSelected;
		data = _playerId;
		if (status != null)
		{
			status.selectedIndex = ((!_selectable) ? 2 : (selected ? 1 : 0));
		}
		base.touchable = _selectable;
		base.grayed = !_selectable;
		RenderPlayerLabel(friendData);
		base.onClick.Set(OnClickCandidate);
	}

	private void RenderPlayerLabel(FriendData friendData)
	{
		if (com_PlayerLabel is UICom_PlayerLabel com_Label)
		{
			CommonUIManager.RendererLabelInfo(com_Label, friendData.Nick ?? string.Empty, friendData.LV);
			CommonUIManager.RendererHeadShot(com_Label, friendData.HeadURL, isVideo: false);
			CommonUIManager.RendererLabel(UIType.Panel, 55, com_Label, friendData.Label.Item1, friendData.Label.Item2);
		}
	}

	private void OnClickCandidate()
	{
		if (_selectable && _playerId != 0L)
		{
			_onSelected?.Invoke(_playerId);
		}
	}

	private void ClearRenderedData()
	{
		_playerId = 0L;
		_selectable = false;
		_onSelected = null;
		data = null;
		base.touchable = false;
		base.grayed = true;
		if (status != null)
		{
			status.selectedIndex = 0;
		}
		base.onClick.Clear();
	}

	public static UIGuild_Button_PlayerItem CreateInstance()
	{
		return (UIGuild_Button_PlayerItem)UIPackage.CreateObject("Guild", "Guild_Button_PlayerItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		com_PlayerLabel = (GComponent)GetChildAt(1);
		group_btn = (GGroup)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
