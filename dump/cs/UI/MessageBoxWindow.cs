using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityTimer;

namespace UI;

public class MessageBoxWindow : BaseWindow
{
	public struct MessageInfo : IEquatable<MessageInfo>
	{
		public int typeIndex;

		public string content;

		public Action okCallback;

		public Action cancelCallback;

		public string okBtnTitle;

		public string cancelBtnTitle;

		public int doubleStatusId;

		public string doubleStatusTitle;

		public float queueUpTime;

		public int MsgTitleId;

		public string MsgTitle;

		public bool ShowCloseButton;

		public MessageInfo(int _typeIndex, string _content, Action _okCallback = null, Action _cancelCallback = null)
		{
			typeIndex = _typeIndex;
			content = _content;
			okCallback = _okCallback;
			cancelCallback = _cancelCallback;
			okBtnTitle = null;
			cancelBtnTitle = null;
			doubleStatusId = 0;
			doubleStatusTitle = null;
			queueUpTime = -1f;
			MsgTitleId = 1030011;
			MsgTitle = null;
			ShowCloseButton = false;
		}

		public void Reset()
		{
			typeIndex = -1;
			content = null;
			okCallback = null;
			cancelCallback = null;
			okBtnTitle = null;
			cancelBtnTitle = null;
			doubleStatusId = 0;
			doubleStatusTitle = null;
			queueUpTime = -1f;
			MsgTitleId = 0;
			MsgTitle = null;
			ShowCloseButton = false;
		}

		public void SetDoubleStatus(int _doubleStatusId, string _doubleStatusTitle)
		{
			doubleStatusId = _doubleStatusId;
			doubleStatusTitle = _doubleStatusTitle;
		}

		public void SetDoubleStatus(bool doubleStatus)
		{
			SetDoubleStatus(doubleStatus ? 1 : 0, doubleStatus ? 1050.GetLocal(UIStringType.Message) : null);
		}

		public void SetDoubleStatus(int doubleStatusTxtId)
		{
			SetDoubleStatus((doubleStatusTxtId != 0) ? 1 : 0, (doubleStatusTxtId != 0) ? doubleStatusTxtId.GetLocal(UIStringType.Message) : null);
		}

		public void SetBtnTitle(string _okBtnTitle, string _cancelBtnTitle)
		{
			okBtnTitle = _okBtnTitle;
			cancelBtnTitle = _cancelBtnTitle;
		}

		public void SetTitleStr(string title)
		{
			MsgTitle = title;
		}

		public void ChangeCloseButton(bool status)
		{
			ShowCloseButton = status;
		}

		public bool Equals(MessageInfo other)
		{
			if (typeIndex.Equals(other.typeIndex) && doubleStatusId.Equals(other.doubleStatusId) && queueUpTime.Equals(other.queueUpTime) && string.Equals(content, other.content) && string.Equals(okBtnTitle, other.okBtnTitle) && string.Equals(cancelBtnTitle, other.cancelBtnTitle))
			{
				return string.Equals(doubleStatusTitle, other.doubleStatusTitle);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is MessageInfo other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(typeIndex, content, okBtnTitle, cancelBtnTitle, doubleStatusId, doubleStatusTitle, queueUpTime);
		}

		public static bool operator ==(MessageInfo left, MessageInfo right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(MessageInfo left, MessageInfo right)
		{
			return !(left == right);
		}
	}

	private MessageInfo curMsg = new MessageInfo(-1, null);

	private Timer _QueueUpTimer;

	private Stack<MessageInfo> _messagesStack = new Stack<MessageInfo>(8);

	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private bool _nowShow = true;

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	private async UniTask PopWindow(MessageInfo _info)
	{
		if (!curMsg.Equals(_info))
		{
			curMsg = _info;
			await TryShowAsync();
		}
	}

	public void HideWindowHandel()
	{
		if (_messagesStack.TryPop(out var result))
		{
			PopWindow(result);
		}
		else
		{
			Hide();
		}
	}

	private async UniTask TryShow(MessageInfo messageInfo)
	{
		if (curMsg.typeIndex >= 0 || !_nowShow)
		{
			if (!_messagesStack.Contains(messageInfo))
			{
				_messagesStack.Push(messageInfo);
			}
		}
		else
		{
			await PopWindow(messageInfo);
		}
	}

	private async UniTask ForceShow(MessageInfo messageInfo)
	{
		if (curMsg.typeIndex < 0)
		{
			await TryShow(messageInfo);
			return;
		}
		_messagesStack.Push(curMsg);
		await PopWindow(messageInfo);
	}

	public MessageBoxWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIMessageBoxWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
			Show();
		}
		else
		{
			OnShown();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	public async UniTask ShowOKCancel(int msgId, Action okCallback = null, Action cancelCallback = null, bool doubleStatus = false)
	{
		await ShowOKCancel(msgId.GetLocal(UIStringType.Message), okCallback, cancelCallback, doubleStatus);
	}

	public async UniTask ShowOKCancel(string msg, Action okCallback = null, Action cancelCallback = null, bool doubleStatus = false)
	{
		MessageInfo messageInfo = new MessageInfo(1, msg, okCallback, cancelCallback);
		messageInfo.SetDoubleStatus(doubleStatus);
		await TryShow(messageInfo);
	}

	public async UniTask ShowOKCancel(string msg, int doubleStatusId, Action okCallback = null, Action cancelCallback = null)
	{
		MessageInfo messageInfo = new MessageInfo(1, msg, okCallback, cancelCallback);
		messageInfo.SetDoubleStatus(doubleStatusId);
		await TryShow(messageInfo);
	}

	public async UniTask ShowOKCancel(string msg, int doubleStatusId, int okTitle, int cancelTitle, Action okCallback = null, Action cancelCallback = null)
	{
		MessageInfo messageInfo = new MessageInfo(1, msg, okCallback, cancelCallback);
		messageInfo.SetDoubleStatus(doubleStatusId);
		messageInfo.SetBtnTitle(okTitle.GetLocal(UIStringType.Message), cancelTitle.GetLocal(UIStringType.Message));
		await TryShow(messageInfo);
	}

	public async UniTask ShowOKCancelWithTitle(string msg, string msgTitle, string okTitle, string cancelTitle, bool closeButtonStatus, Action okCallback = null, Action cancelCallback = null)
	{
		MessageInfo messageInfo = new MessageInfo(1, msg, okCallback, cancelCallback);
		messageInfo.SetBtnTitle(okTitle, cancelTitle);
		messageInfo.SetTitleStr(msgTitle);
		messageInfo.ChangeCloseButton(closeButtonStatus);
		await TryShow(messageInfo);
	}

	public async UniTask ShowOK(int msgId, Action okCallback = null)
	{
		await ShowOK(msgId.GetLocal(UIStringType.Message), okCallback);
	}

	public async UniTask ShowOK(string msg, Action okCallback = null)
	{
		MessageInfo messageInfo = new MessageInfo(2, msg, okCallback);
		await TryShow(messageInfo);
	}

	public async UniTask ShowQueueUp(string msg, float queueUpTime, Action okCallback = null)
	{
		MessageInfo messageInfo = new MessageInfo(2, msg, okCallback);
		messageInfo.queueUpTime = queueUpTime;
		await TryShow(messageInfo);
	}

	protected override void OnShown()
	{
		base.OnShown();
		base.touchable = true;
		GComponent gComponent = base.contentPane;
		UIMessageBoxWindow win = gComponent as UIMessageBoxWindow;
		if (win != null)
		{
			if (!(win.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom))
			{
				return;
			}
			uICom_PopUpWindow_Bottom.btnsState.selectedIndex = curMsg.typeIndex;
			win.showCancelDouble.selectedIndex = curMsg.doubleStatusId;
			win.content.text = curMsg.content;
			uICom_PopUpWindow_Bottom.btn_Sure.text = (string.IsNullOrEmpty(curMsg.okBtnTitle) ? 1093.GetLocal(UIStringType.Message) : curMsg.okBtnTitle);
			uICom_PopUpWindow_Bottom.btn_Sure_Only.text = (string.IsNullOrEmpty(curMsg.okBtnTitle) ? 1093.GetLocal(UIStringType.Message) : curMsg.okBtnTitle);
			uICom_PopUpWindow_Bottom.btn_Cancel.text = (string.IsNullOrEmpty(curMsg.cancelBtnTitle) ? 1094.GetLocal(UIStringType.Message) : curMsg.cancelBtnTitle);
			if (curMsg.doubleStatusId != 0)
			{
				win.btn_DoubleStatus.title = (string.IsNullOrEmpty(curMsg.doubleStatusTitle) ? 1050.GetLocal(UIStringType.Message) : curMsg.doubleStatusTitle);
			}
			uICom_PopUpWindow_Bottom.title = (string.IsNullOrEmpty(curMsg.MsgTitle) ? curMsg.MsgTitleId.GetLocal(UIStringType.GUI) : curMsg.MsgTitle);
			uICom_PopUpWindow_Bottom.btn_Sure.onClick.Add(OnClickOk);
			uICom_PopUpWindow_Bottom.btn_Sure_Only.onClick.Add(OnClickOk);
			uICom_PopUpWindow_Bottom.btn_Cancel.onClick.Add(OnClickCancel);
			uICom_PopUpWindow_Bottom.closeButton.visible = curMsg.ShowCloseButton;
			uICom_PopUpWindow_Bottom.closeButton.onClick.Add(OnClickCancel);
			Timer queueUpTimer = _QueueUpTimer;
			if (queueUpTimer != null)
			{
				queueUpTimer.Cancel();
			}
			if (curMsg.queueUpTime > 0f)
			{
				_QueueUpTimer = Timer.Register(0f, curMsg.queueUpTime, (Action)null, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)delegate(float t)
				{
					int num = (int)(curMsg.queueUpTime - t);
					win.content.text = string.Format(curMsg.content, num / 60, num % 60);
				}, (Action)null, false, -1f, false, (GameObject)null);
			}
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			win.btn_DoubleStatus.onClick.Add(SwitchDoubleStatus);
		}
		blurBgCtrl.OnShown(this);
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (!(base.contentPane is UIMessageBoxWindow uIMessageBoxWindow) || !base.isShowing || !(uIMessageBoxWindow.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom))
		{
			return;
		}
		if (context.inputEvent.keyCode == KeyCode.Escape)
		{
			if (curMsg.typeIndex == 1)
			{
				return;
			}
			uICom_PopUpWindow_Bottom.btn_Cancel.FireClick(downEffect: true);
			uICom_PopUpWindow_Bottom.closeButton.FireClick(downEffect: true);
			OnClickCancel();
		}
		if (context.inputEvent.keyCode == KeyCode.Return)
		{
			uICom_PopUpWindow_Bottom.btn_Sure.FireClick(downEffect: true);
			uICom_PopUpWindow_Bottom.btn_Sure_Only.FireClick(downEffect: true);
			OnClickOk();
		}
	}

	protected override async void OnHide()
	{
		try
		{
			base.OnHide();
			Timer queueUpTimer = _QueueUpTimer;
			if (queueUpTimer != null)
			{
				queueUpTimer.Cancel();
			}
			curMsg.Reset();
			if (base.contentPane is UIMessageBoxWindow uIMessageBoxWindow)
			{
				if (!(uIMessageBoxWindow.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom))
				{
					return;
				}
				uICom_PopUpWindow_Bottom.btn_Sure.onClick.Remove(OnClickOk);
				uICom_PopUpWindow_Bottom.btn_Sure_Only.onClick.Remove(OnClickOk);
				uICom_PopUpWindow_Bottom.btn_Cancel.onClick.Remove(OnClickCancel);
				uICom_PopUpWindow_Bottom.closeButton.onClick.Remove(OnClickCancel);
				SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
				uIMessageBoxWindow.btn_DoubleStatus.onClick.Remove(SwitchDoubleStatus);
			}
			blurBgCtrl.OnHide();
			if (_messagesStack.TryPop(out var _info))
			{
				await UniTask.DelayFrame(1);
				PopWindow(_info);
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	private void SwitchDoubleStatus()
	{
		if (base.contentPane is UIMessageBoxWindow uIMessageBoxWindow)
		{
			uIMessageBoxWindow.btn_DoubleStatus.onClick.Retain();
			uIMessageBoxWindow.btn_DoubleStatus.selectedStatus.selectedIndex = ((uIMessageBoxWindow.btn_DoubleStatus.selectedStatus.selectedIndex == 0) ? 1 : 0);
			uIMessageBoxWindow.btn_DoubleStatus.onClick.Release();
		}
	}

	private void OnClickOk()
	{
		curMsg.okCallback?.Invoke();
		HideWindowHandel();
	}

	private void OnClickCancel()
	{
		curMsg.cancelCallback?.Invoke();
		HideWindowHandel();
	}

	public bool GetDoubleStatus()
	{
		if (base.contentPane is UIMessageBoxWindow uIMessageBoxWindow)
		{
			return uIMessageBoxWindow.btn_DoubleStatus.selectedStatus.selectedIndex == 1;
		}
		return false;
	}

	public void StopNowShow()
	{
		if (curMsg.typeIndex < 0)
		{
			_nowShow = false;
		}
	}

	public void StartShow()
	{
		_nowShow = true;
		if (curMsg.typeIndex < 0 && _messagesStack.TryPop(out var result))
		{
			PopWindow(result);
		}
	}
}
