using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;

namespace UI;

public class InputWindow : BaseWindow
{
	private Action _cancelCallback;

	private Action<string> _inputCallback;

	public List<string> appTestNames = new List<string> { "apptest001", "apptest002", "apptest003", "apptest004", "apptest005", "apptest006", "apptest007", "apptest008", "apptest009", "apptest010" };

	public InputWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIInputWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIInputWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIInputWindow)
		{
			uIInputWindow.mohu.onClick.Add(OnClickCancel);
			bottom.btn_Cancel.onClick.Add(OnClickCancel);
			bottom.closeButton.onClick.Add(OnClickCancel);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIInputWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIInputWindow)
		{
			uIInputWindow.mohu.onClick.Remove(OnClickCancel);
			bottom.btn_Cancel.onClick.Remove(OnClickCancel);
			bottom.closeButton.onClick.Remove(OnClickCancel);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			uIInputWindow.type.selectedIndex = 0;
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIInputWindow uIInputWindow && base.isShowing && uIInputWindow.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom)
		{
			if (context.inputEvent.keyCode == KeyCode.Escape)
			{
				uICom_PopUpWindow_Bottom.btn_Cancel.FireClick(downEffect: true);
				uICom_PopUpWindow_Bottom.closeButton.FireClick(downEffect: true);
				OnClickCancel();
			}
			if (context.inputEvent.keyCode == KeyCode.Return && uIInputWindow.type.selectedIndex == 1)
			{
				uICom_PopUpWindow_Bottom.btn_Sure.FireClick(downEffect: true);
				OnSurePwd();
			}
		}
	}

	public async void OpenPwd(Action<string> inputCallback = null, Action cancelCallback = null)
	{
		await TryShow();
		_inputCallback = inputCallback;
		_cancelCallback = cancelCallback;
		if (base.contentPane is UIInputWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIInputWindow)
		{
			uIInputWindow.type.selectedIndex = 1;
			uIInputWindow.textField_PWD.text = "";
			bottom.btn_Sure.onClick.Set(OnSurePwd);
		}
	}

	private void OnSurePwd()
	{
		if (base.contentPane is UIInputWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIInputWindow)
		{
			if (string.IsNullOrWhiteSpace(uIInputWindow.textField_PWD.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1004);
				return;
			}
			bottom.btn_Sure.onClick.Retain();
			_inputCallback?.Invoke(uIInputWindow.textField_PWD.text);
			Hide();
			bottom.btn_Sure.onClick.Release();
		}
	}

	private void OnClickCancel()
	{
		_cancelCallback?.Invoke();
		Hide();
	}

	public async void OpenInspectionLogin(Action<string> loginCallback)
	{
		await TryShow();
		GComponent gComponent = base.contentPane;
		UIInputWindow win = gComponent as UIInputWindow;
		if (win == null)
		{
			return;
		}
		GLabel bottom = win.bottom;
		UICom_PopUpWindow_Bottom winBottom = bottom as UICom_PopUpWindow_Bottom;
		if (winBottom == null)
		{
			return;
		}
		win.type.selectedIndex = 2;
		win.textField_AccountNick.text = "";
		win.textField_AccountPWD.text = "";
		winBottom.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			if (string.IsNullOrWhiteSpace(win.textField_AccountNick.text) || !appTestNames.Contains(win.textField_AccountNick.text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(21.GetLocal(UIStringType.Message));
			}
			else if (!win.textField_AccountPWD.text.Equals("123456"))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(23.GetLocal(UIStringType.Message));
			}
			else
			{
				winBottom.btn_Sure.onClick.Retain();
				loginCallback(win.textField_AccountNick.text);
				winBottom.btn_Sure.onClick.Release();
			}
		});
	}
}
