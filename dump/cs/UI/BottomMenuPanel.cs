using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class BottomMenuPanel : BasePanel<UIBottomMenuPanel>
{
	private readonly List<int> tokenlist = new List<int>();

	private readonly RepeatedField<int> curFunctionIndex = new RepeatedField<int>();

	public BottomMenuPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIBottomMenuPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.list_Token.itemRenderer = RendererToken;
		base.ui.list_Function.itemRenderer = RendererFunction;
	}

	public override void Refresh()
	{
		base.Refresh();
		SetMakeWidth(base.ui.graph_1);
		SetMakeWidth(base.ui.graph_2);
		ChangeShowStatus(status: true);
		base.ui.showPlayerLabel.selectedIndex = ((!SimpleSingletonProvider<UIManager>.inst.currentPanel.config.NeedBottomPlayerLabel) ? 1 : 0);
		UICom_PlayerLabel com_Label = (UICom_PlayerLabel)base.ui.com_Label;
		ShowingFashion runningFashion = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetRunningFashion();
		string nick = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
		int LV = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().Level;
		CommonUIManager.RendererLabelInfo(com_Label, nick, LV);
		(string, bool) labelData = runningFashion.labelId.GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
		CommonUIManager.RendererLabel(UIType.Panel, (int)base.config.PanelType, com_Label, labelData.Item1, labelData.Item2);
		string headIcon = runningFashion.headShotId.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
		CommonUIManager.RendererHeadShot(com_Label, headIcon, isVideo: false);
		base.ui.com_Label.onClick.Set((EventCallback0)delegate
		{
			base.ui.com_Label.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.account.RequestGetShowPlayerC2S(0L, nick, LV, headIcon, labelData, delegate
			{
				base.ui.com_Label.onClick.Release();
			});
		});
		ChangeTokenList((SimpleSingletonProvider<UIManager>.inst.currentPanel.config.ShowCurrencies.Count > 1) ? (-1) : 0);
		ChangeFunctionList();
		LevelChange();
		RefreshToken();
	}

	private void SetMakeWidth(GGraph graph)
	{
		graph.width = UIHelper.ExpandToAspectRatio(GRoot.inst.width, GRoot.inst.height).width;
		graph.x = (int)((base.ui.width - graph.width) * 0.5f);
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.list_Function.onClickItem.Add(ExecuteOperate);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.list_Function.onClickItem.Remove(ExecuteOperate);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.AddListener(RefreshToken);
		SimpleSingletonProvider<GameLogicManager>.inst.account.signal.levelChanged.AddListener(LevelChange);
		SimpleSingletonProvider<GameLogicManager>.inst.mail.signal.mailStatus.AddListener(ChangeFunctionList);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.AddListener(ChangeTokenList);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.PlayerLabelController.AddListener(PlayerLabelController);
		SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeName.AddListener(OnChangeNameComplete);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.bagRedSignal.AddListener(UpdateFunc);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.applyStatus.AddListener(UpdateFunc);
		SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.OnFinishShowCard.AddListener(UpdateFunc);
		SimpleSingletonProvider<GameLogicManager>.inst.task.taskRedSignal.AddListener(UpdateFunc);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.RemoveListener(RefreshToken);
		SimpleSingletonProvider<GameLogicManager>.inst.account.signal.levelChanged.RemoveListener(LevelChange);
		SimpleSingletonProvider<GameLogicManager>.inst.mail.signal.mailStatus.RemoveListener(ChangeFunctionList);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.RemoveListener(ChangeTokenList);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.PlayerLabelController.RemoveListener(PlayerLabelController);
		SimpleSingletonProvider<GameLogicManager>.inst.account.signal.changeName.RemoveListener(OnChangeNameComplete);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.bagRedSignal.RemoveListener(UpdateFunc);
		SimpleSingletonProvider<GameLogicManager>.inst.friend.applyStatus.RemoveListener(UpdateFunc);
		SimpleSingletonProvider<GameLogicManager>.inst.task.taskRedSignal.RemoveListener(UpdateFunc);
		SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.OnFinishShowCard.RemoveListener(UpdateFunc);
	}

	public override void Close()
	{
		if (base.ui?.com_Label is UICom_PlayerLabel uICom_PlayerLabel)
		{
			GGraph loader_Video = uICom_PlayerLabel.loader_Label.com_Loader.loader_Video;
			if (loader_Video != null)
			{
				SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(loader_Video);
			}
		}
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void RefreshToken()
	{
		base.ui.list_Token.numItems = tokenlist.Count;
		base.ui.list_Token.ResizeToFit();
	}

	private void UpdateFunc(bool obj = false)
	{
		base.ui.list_Function.numItems = curFunctionIndex.Count;
		base.ui.list_Function.ResizeToFit();
	}

	private void OnChangeNameComplete(bool result)
	{
		if (result)
		{
			Refresh();
		}
	}

	public void ChangeShowStatus(bool status)
	{
		if (base.ui != null)
		{
			base.ui.hide.selectedIndex = ((!status) ? 1 : 0);
		}
	}

	public void ChangeBGSetStatus(bool status)
	{
		if (base.ui != null)
		{
			base.ui.BGSet.selectedIndex = ((!status) ? 1 : 0);
		}
	}

	private void RendererToken(int index, GObject item)
	{
		UIBottomMenu_Button_Token tokenCom = item as UIBottomMenu_Button_Token;
		if (tokenCom != null)
		{
			int itemId = tokenlist[index];
			ItemInfoConfigure itemInfoConfigure = itemId.GetItemInfoConfigure();
			tokenCom.loader_Token.url = itemInfoConfigure.ShowIcon;
			int _count = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(itemId);
			string arg = ((_count >= 0) ? "[color=#FFFFFF]" : "[color=#FF0000]");
			tokenCom.txt_Token.text = $"{arg}{_count}[/color]";
			tokenCom.data = itemId;
			tokenCom.canRecharge.selectedIndex = ((itemInfoConfigure.WayList.Count > 0) ? 1 : 0);
			tokenCom.onClick.Set((EventCallback0)delegate
			{
				tokenCom.onClick.Retain();
				OnClickToken(itemId, _count);
				tokenCom.onClick.Release();
			});
		}
	}

	private void OnClickToken(int tokenId, int count)
	{
		ItemInfoConfigure itemInfoConfigure = tokenId.GetItemInfoConfigure();
		if (tokenId == GameSettings.SPECIAL_ITEM_STARDISC_FREE)
		{
			SimpleSingletonProvider<UIManager>.inst.rechargeTip.TryExchangeToken(tokenId, 1).Forget();
			return;
		}
		int count2 = itemInfoConfigure.WayList.Count;
		if (count2 == 0 || count2 > 1)
		{
			SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(tokenId, count, _Usable: false).Forget();
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.GoWayPanel(itemInfoConfigure.WayList[0]).Forget();
		}
	}

	private void ChangeTokenList(int tab)
	{
		tokenlist.Clear();
		RefreshToken();
		if (tab >= 0)
		{
			MapField<int, UIPanelConfigureShowCurrenciess> showCurrencies = SimpleSingletonProvider<UIManager>.inst.currentPanel.config.ShowCurrencies;
			if (showCurrencies != null && showCurrencies.Count != 0 && showCurrencies.ContainsKey(tab))
			{
				tokenlist.AddRange(showCurrencies[tab].Values);
				RefreshToken();
			}
		}
	}

	private void RendererFunction(int index, GObject item)
	{
		if (!(item is UIBottomMenu_Button_Function uIBottomMenu_Button_Function))
		{
			return;
		}
		uIBottomMenu_Button_Function.type.selectedIndex = ((SimpleSingletonProvider<UIManager>.inst.currentPanel.config.PanelType != UIPanelType.Home) ? 1 : 0);
		uIBottomMenu_Button_Function.functionController.selectedIndex = curFunctionIndex[index];
		if (curFunctionIndex[index] == 3)
		{
			uIBottomMenu_Button_Function.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.mail._container.MailStatus() ? 1 : 0);
		}
		else if (curFunctionIndex[index] == 2)
		{
			uIBottomMenu_Button_Function.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.bag.GetSystemStatus() ? 1 : 0);
		}
		else if (curFunctionIndex[index] == 7)
		{
			uIBottomMenu_Button_Function.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.friend.applyStatus.Value ? 1 : 0);
		}
		else if (curFunctionIndex[index] == 8)
		{
			uIBottomMenu_Button_Function.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.task.GetSystemStatus() ? 1 : 0);
		}
		else if (curFunctionIndex[index] == 5)
		{
			uIBottomMenu_Button_Function.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.altArtCardLogic.HasAnyNewAltArtCard() ? 1 : 0);
		}
		else if (curFunctionIndex[index] == 0)
		{
			bool flag = RunTimeRemoteConfigHandler.EnableContact;
			if (!flag)
			{
				flag = BnSdkInit.Instance.AppID != "110001939";
			}
			uIBottomMenu_Button_Function.visible = flag;
		}
		else
		{
			uIBottomMenu_Button_Function.redPoint.selectedIndex = 0;
		}
	}

	private void ChangeFunctionList()
	{
		curFunctionIndex.Clear();
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel?.config != null)
		{
			base.ui.type.selectedIndex = ((SimpleSingletonProvider<UIManager>.inst.currentPanel.config.PanelType != UIPanelType.Home) ? 1 : 0);
			curFunctionIndex.AddRange(SimpleSingletonProvider<UIManager>.inst.currentPanel.config.ShowFunction);
			UpdateFunc();
			base.ui.list_Function.opaque = false;
		}
	}

	private async void ExecuteOperate(EventContext context)
	{
		if (!(context.data is UIBottomMenu_Button_Function uIBottomMenu_Button_Function))
		{
			return;
		}
		base.ui.list_Function.onClickItem.Retain();
		switch (uIBottomMenu_Button_Function.functionController.selectedIndex)
		{
		case 0:
			if (Application.platform != RuntimePlatform.Android)
			{
				await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1014, delegate
				{
					BnSdkManager.Instance.ExitSdk();
					SimpleSingletonProvider<GameManager>.inst.CloseGame();
				});
			}
			else
			{
				BnSdkManager.Instance.ExitSdk();
			}
			break;
		case 1:
			await SimpleSingletonProvider<UIManager>.inst.setting.ShowSetting();
			break;
		case 2:
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Bag);
			break;
		case 3:
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Mail);
			break;
		case 4:
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				await SimpleSingletonProvider<UIManager>.inst.guide.ShowTransparent();
			}
			await SimpleSingletonProvider<UIManager>.inst.selectTutorial.TryShow();
			if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
			{
				SimpleSingletonProvider<UIManager>.inst.guide.TriggerNext();
			}
			break;
		case 5:
			await SimpleSingletonProvider<UIManager>.inst.NewGameLibrary.TryShowAsync();
			break;
		case 6:
			await SimpleSingletonProvider<WebServerManager>.inst.ShowNoticeWindow();
			break;
		case 7:
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Friend);
			break;
		case 8:
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Task);
			break;
		case 9:
			if (SimpleSingletonProvider<GameLogicManager>.inst.guild?.IsJoined ?? false)
			{
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Guild);
			}
			else
			{
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.GuildDiscovery);
			}
			break;
		}
		base.ui.list_Function.onClickItem.Release();
	}

	private void LevelChange()
	{
		UICom_PlayerLabel obj = (UICom_PlayerLabel)base.ui.com_Label;
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		int needExp = playerInfo.Level.GetPlayerLevelConfigure().NeedExp;
		base.ui.slider_Exp.max = needExp;
		base.ui.slider_Exp.value = playerInfo.Exp;
		obj.txt_lv.text = playerInfo.Level.ToString();
	}

	public async void GuideTriggerOpenNoviceScene()
	{
		List<GObject> functionCom = base.ui.list_Function._children;
		for (int i = 0; i < functionCom.Count; i++)
		{
			if (functionCom[i] is UIBottomMenu_Button_Function uIBottomMenu_Button_Function && uIBottomMenu_Button_Function.functionController.selectedIndex == 4)
			{
				Vector2 pt = uIBottomMenu_Button_Function.LocalToGlobal(Vector2.zero);
				pt = GRoot.inst.GlobalToLocal(pt);
				SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideArrow(pt, uIBottomMenu_Button_Function.width, 0f, 180f);
				await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pt, uIBottomMenu_Button_Function.width, uIBottomMenu_Button_Function.height, _needTransparentMask: false, isRect: true);
			}
		}
	}

	private void PlayerLabelController(bool status)
	{
		base.ui.showPlayerLabel.selectedIndex = ((!status) ? 1 : 0);
	}
}
