using Core.Audio;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using party.model;
using party.protocol;

namespace UI;

public class LandRelicWindow : BaseWindow
{
	public LandRelicWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandRelicWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShow()
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
		if (base.contentPane is UILandRelicWindow uILandRelicWindow)
		{
			bool flag = SimpleSingletonProvider<GameLogicManager>.inst.battle.IsContainHero(118);
			uILandRelicWindow.boardRole.selectedIndex = (flag ? 1 : 0);
			BGMHelper.TryPlayBGM(flag ? 612 : 611);
			uILandRelicWindow.txt_HideTip.text = 11023.GetLocal(UIStringType.Message);
			uILandRelicWindow.Hide.selectedIndex = 0;
			uILandRelicWindow.Hide.onChanged.Add(OnHideControllerChange);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILandRelicWindow uILandRelicWindow)
		{
			uILandRelicWindow.Hide.onChanged.Remove(OnHideControllerChange);
		}
	}

	public async UniTask ShowPurchase(BattlePlayerData playerData, Action action)
	{
		long actionSn = action.Sn;
		BuyRelicC2S buyRelicC2S = ByteBuf.ReadObject<BuyRelicC2S>(action.Data.ToByteArray());
		int RelicGold = buyRelicC2S.RelicGold;
		await TryShow();
		GComponent gComponent = base.contentPane;
		UILandRelicWindow win = gComponent as UILandRelicWindow;
		if (win == null)
		{
			return;
		}
		win.cut_in.Play();
		win.btn_Sure.txt_Desc.SetVar("needGold", RelicGold.ToString()).FlushVars();
		win.btn_Cancel.onClick.Release();
		win.btn_Cancel.onClick.Set((EventCallback0)delegate
		{
			if (actionSn != 0L)
			{
				win.btn_Cancel.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.land.RequestBuyRelicC2S(actionSn, 0);
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
					SimpleSingletonProvider<GameLogicManager>.inst.land.RequestBuyRelicC2S(actionSn, 2);
					actionSn = 0L;
				}
			}
		});
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(playerData.player.Id))
		{
			OperationTimer.ActionDownTime(actionSn, 5249, delegate
			{
				win.btn_Cancel.onClick.Call();
			});
		}
	}
}
