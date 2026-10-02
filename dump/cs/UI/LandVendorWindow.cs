using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace UI;

public class LandVendorWindow : BaseWindow
{
	public LandVendorWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandVendorWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask TryShow()
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

	private void OnHideControllerChange()
	{
		if (base.contentPane is UILandRelicWindow uILandRelicWindow)
		{
			base.BgLoader.visible = uILandRelicWindow.Hide.selectedIndex == 0;
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UILandVendorWindow uILandVendorWindow)
		{
			uILandVendorWindow.Cut_in.Play();
			uILandVendorWindow.txt_HideTip.text = 11023.GetLocal(UIStringType.Message);
			uILandVendorWindow.Hide.selectedIndex = 0;
			uILandVendorWindow.Hide.onChanged.Add(OnHideControllerChange);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILandVendorWindow uILandVendorWindow)
		{
			uILandVendorWindow.Hide.onChanged.Remove(OnHideControllerChange);
		}
	}

	public async UniTask ShowVendorBuyCard(BattlePlayerData playerData, Action action)
	{
		long actionSn = action.Sn;
		VendorBuyCardC2S vendorBuyCardC2S = ByteBuf.ReadObject<VendorBuyCardC2S>(action.Data.ToByteArray());
		int RelicGold = vendorBuyCardC2S.Gold;
		await TryShow();
		GComponent gComponent = base.contentPane;
		UILandVendorWindow win = gComponent as UILandVendorWindow;
		if (win == null)
		{
			return;
		}
		win.btn_Sure.txt_Desc.SetVar("needGold", RelicGold.ToString()).FlushVars();
		win.txt_Gold.text = playerData.Property.gold.Value.ToString();
		win.btn_Cancel.onClick.Release();
		win.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			if (actionSn != 0L)
			{
				win.btn_Cancel.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestVendorBuyCardC2S(actionSn, isBuy: false);
				actionSn = 0L;
			}
		});
		win.btn_Sure.onClick.Release();
		win.btn_Sure.onClick.Set((EventCallback0)delegate
		{
			if (actionSn != 0L)
			{
				if (playerData.Property.gold.Value < RelicGold)
				{
					SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(10013);
				}
				else
				{
					win.btn_Sure.onClick.Retain();
					SimpleSingletonProvider<GameLogicManager>.inst.land.RequestVendorBuyCardC2S(actionSn, isBuy: true);
					actionSn = 0L;
				}
			}
		});
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id))
		{
			OperationTimer.ActionDownTime(actionSn, 5323, delegate
			{
				win.btn_Cancel.onClick.Call();
			});
		}
	}
}
