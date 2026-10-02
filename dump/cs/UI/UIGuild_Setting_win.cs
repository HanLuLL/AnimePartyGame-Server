using System;
using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuild_Setting_win : GComponent
{
	public const int SETTING_STATE = 0;

	public const int NOTICE_STATE = 1;

	public const int MEMBER_SETTING_STATE = 2;

	public const int MEMBER_CHANGE_STATE = 3;

	public const int APPROVAL_STATE = 4;

	private bool _initialized;

	private bool _isOpen;

	private int _openVersion;

	private long _selectedPlayerId;

	public Action OnRequestClose;

	public Controller state;

	public UIGuild_Setting_Com setting_com;

	public UIGuild_Notice_Com notice_com;

	public UIGuild_MemberSetting_Com membersetting_com;

	public UIGuild_MemberChange_Com memberchange_com;

	public UIGuild_Approval_Com approval_com;

	public const string URL = "ui://w5bj58pzg04j1b";

	public int OpenVersion => _openVersion;

	public bool IsWindowOpen => _isOpen;

	public void Init()
	{
		if (!_initialized)
		{
			setting_com?.Init();
			notice_com?.Init();
			membersetting_com?.Init();
			memberchange_com?.Init();
			approval_com?.Init();
			InjectCloseCallbacks();
			_initialized = true;
		}
	}

	public void Open(int stateIndex, long selectedPlayerId = 0L)
	{
		if (stateIndex >= 0 && stateIndex <= 4)
		{
			if (_isOpen)
			{
				ClearCurrentState();
			}
			_openVersion++;
			_isOpen = true;
			_selectedPlayerId = ((stateIndex == 2) ? selectedPlayerId : 0);
			InjectCloseCallbacks();
			state.selectedIndex = stateIndex;
			switch (stateIndex)
			{
			case 0:
				setting_com?.Open(_openVersion);
				break;
			case 1:
				notice_com?.Open(_openVersion);
				break;
			case 2:
				membersetting_com?.Open(_selectedPlayerId, _openVersion);
				break;
			case 3:
				memberchange_com?.Open(_openVersion);
				break;
			case 4:
				approval_com?.Open(_openVersion);
				break;
			}
		}
	}

	public void AddEvent()
	{
		setting_com?.AddEvent();
		notice_com?.AddEvent();
		membersetting_com?.AddEvent();
		memberchange_com?.AddEvent();
		approval_com?.AddEvent();
	}

	public void RemoveEvent()
	{
		setting_com?.RemoveEvent();
		notice_com?.RemoveEvent();
		membersetting_com?.RemoveEvent();
		memberchange_com?.RemoveEvent();
		approval_com?.RemoveEvent();
	}

	public void AddListener()
	{
		setting_com?.AddListener();
		notice_com?.AddListener();
		membersetting_com?.AddListener();
		memberchange_com?.AddListener();
		approval_com?.AddListener();
	}

	public void RemoveListener()
	{
		setting_com?.RemoveListener();
		notice_com?.RemoveListener();
		membersetting_com?.RemoveListener();
		memberchange_com?.RemoveListener();
		approval_com?.RemoveListener();
	}

	public void ClearData()
	{
		_openVersion++;
		_isOpen = false;
		_selectedPlayerId = 0L;
		setting_com?.ClearData();
		notice_com?.ClearData();
		membersetting_com?.ClearData();
		memberchange_com?.ClearData();
		approval_com?.ClearData();
		OnRequestClose = null;
	}

	private void ClearCurrentState()
	{
		switch (state.selectedIndex)
		{
		case 0:
			setting_com?.ClearData();
			break;
		case 1:
			notice_com?.ClearData();
			break;
		case 2:
			membersetting_com?.ClearData();
			break;
		case 3:
			memberchange_com?.ClearData();
			break;
		case 4:
			approval_com?.ClearData();
			break;
		}
	}

	private void InjectCloseCallbacks()
	{
		if (setting_com != null)
		{
			setting_com.OnRequestClose = RequestClose;
		}
		if (notice_com != null)
		{
			notice_com.OnRequestClose = RequestClose;
		}
		if (membersetting_com != null)
		{
			membersetting_com.OnRequestClose = RequestClose;
		}
		if (memberchange_com != null)
		{
			memberchange_com.OnRequestClose = RequestClose;
		}
		if (approval_com != null)
		{
			approval_com.OnRequestClose = RequestClose;
		}
	}

	private void RequestClose()
	{
		OnRequestClose?.Invoke();
	}

	public static UIGuild_Setting_win CreateInstance()
	{
		return (UIGuild_Setting_win)UIPackage.CreateObject("Guild", "Guild_Setting_win");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		setting_com = (UIGuild_Setting_Com)GetChildAt(0);
		notice_com = (UIGuild_Notice_Com)GetChildAt(1);
		membersetting_com = (UIGuild_MemberSetting_Com)GetChildAt(2);
		memberchange_com = (UIGuild_MemberChange_Com)GetChildAt(3);
		approval_com = (UIGuild_Approval_Com)GetChildAt(4);
	}
}
