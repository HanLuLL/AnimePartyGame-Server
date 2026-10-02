using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using UnityEngine;

namespace UI;

public class BoxPropWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public BoxPropWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIBoxPropWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIBoxPropWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIBoxPropWindow)
		{
			uIBoxPropWindow.mohu.onClick.Add(base.Hide);
			bottom.btn_Cancel.onClick.Add(base.Hide);
			bottom.closeButton.onClick.Add(base.Hide);
			bottom.btn_Sure_Only.onClick.Add(base.Hide);
			bottom.btn_Sure.onClick.Add(OnClickConfirm);
			uIBoxPropWindow.com_Chest.AddEvent();
			blurBgCtrl.OnShown(this);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIBoxPropWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIBoxPropWindow)
		{
			uIBoxPropWindow.mohu.onClick.Remove(base.Hide);
			bottom.btn_Cancel.onClick.Remove(base.Hide);
			bottom.closeButton.onClick.Remove(base.Hide);
			bottom.btn_Sure_Only.onClick.Remove(base.Hide);
			bottom.btn_Sure.onClick.Remove(OnClickConfirm);
			uIBoxPropWindow.type.selectedIndex = 0;
			uIBoxPropWindow.com_Chest.RemoveEvent();
			blurBgCtrl.OnHide();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIBoxPropWindow && base.isShowing && context.inputEvent.keyCode == KeyCode.Escape)
		{
			Hide();
		}
	}

	public async UniTask ShowWin(BagItem _item)
	{
		await TryShowAsync();
		if (base.contentPane is UIBoxPropWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIBoxPropWindow)
		{
			uIBoxPropWindow.type.selectedIndex = 1;
			bottom.btnsState.selectedIndex = 1;
			uIBoxPropWindow.com_Chest.ShowWin(_item, this);
		}
	}

	public async UniTask ShowGift(MapField<int, int> rewardDict)
	{
		await TryShowAsync();
		if (base.contentPane is UIBoxPropWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIBoxPropWindow)
		{
			uIBoxPropWindow.type.selectedIndex = 2;
			bottom.btnsState.selectedIndex = 2;
			uIBoxPropWindow.com_Gift.ShowWin(rewardDict, this);
		}
	}

	private void OnClickConfirm()
	{
		if (!(base.contentPane is UIBoxPropWindow { bottom: var bottom } uIBoxPropWindow))
		{
			return;
		}
		UICom_PopUpWindow_Bottom winBottom = bottom as UICom_PopUpWindow_Bottom;
		if (winBottom != null && uIBoxPropWindow.type.selectedIndex == 1)
		{
			winBottom.btn_Sure.onClick.Retain();
			uIBoxPropWindow.com_Chest.OnOpenBox(delegate
			{
				winBottom.btn_Sure.onClick.Release();
				Hide();
			});
		}
	}
}
