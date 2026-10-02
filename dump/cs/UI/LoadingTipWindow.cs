using System;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityTimer;

namespace UI;

public class LoadingTipWindow : BaseWindow
{
	private bool delayStatus;

	private string msg;

	private Timer timer;

	public LoadingTipWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILoadingTipWindow.CreateInstance();
		base.OnInit();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public async void ShowNotWait(bool _delayStaus = true, int _msgId = 0)
	{
		await TryShow(_delayStaus, _msgId);
	}

	private async UniTask ShowWin()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	public async UniTask TryShow(bool _delayStatus = true, int _msgId = 0)
	{
		delayStatus = _delayStatus;
		msg = ((_msgId == 0) ? "" : _msgId.GetLocal(UIStringType.Message));
		await ShowWin();
	}

	public async UniTask TryShowFriend(bool _delayStatus = true, int _friendMsgId = 0)
	{
		delayStatus = _delayStatus;
		msg = ((_friendMsgId == 0) ? "" : _friendMsgId.GetLocal(UIStringType.Friend));
		await ShowWin();
	}

	public async UniTask TryShowMask()
	{
		await ShowWin();
		if (base.contentPane is UILoadingTipWindow uILoadingTipWindow)
		{
			uILoadingTipWindow.type.selectedIndex = 2;
		}
	}

	protected override async void OnShown()
	{
		base.OnShown();
		GComponent gComponent = base.contentPane;
		if (gComponent is UILoadingTipWindow tip)
		{
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			tip.type.selectedIndex = ((!string.IsNullOrEmpty(msg)) ? 1 : 0);
			tip.txt_MessageTips.text = msg;
			if (delayStatus)
			{
				await UniTask.Delay(500);
			}
			if (base.isShowing)
			{
				tip.cutIn.Play();
			}
			Timer obj = timer;
			if (obj != null)
			{
				obj.Cancel();
			}
			timer = Timer.Register(0f, 15f, (Action)base.Hide, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)null, (Action)null, false, -1f, false, (GameObject)null);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILoadingTipWindow uILoadingTipWindow)
		{
			Timer obj = timer;
			if (obj != null)
			{
				obj.Cancel();
			}
			uILoadingTipWindow.com_group.visible = false;
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
		}
	}

	public async UniTask TryShowConnect(int _msgId = 0)
	{
		delayStatus = false;
		msg = ((_msgId == 0) ? "" : _msgId.GetLocal(UIStringType.Message));
		await ShowWin();
		Timer obj = timer;
		if (obj != null)
		{
			obj.Cancel();
		}
	}
}
