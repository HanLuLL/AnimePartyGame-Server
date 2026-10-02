using System;
using Core;
using Core.Net;
using GameLogic;
using Tools;

namespace UI;

public class ActivityAdvertPanel : BasePanel<UIActivityAdvertPanel>
{
	private int _activityId = 609032;

	public ActivityAdvertPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityAdvertPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Refresh()
	{
		base.Refresh();
		base.ui.loader.MallScreen();
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		if (base.ui == null)
		{
			return;
		}
		int dataForLanguage = GameSettings.GetDataForLanguage(1, 2, 0, 3);
		base.ui.language.selectedIndex = dataForLanguage;
		base.ui.btn_buy.language.selectedIndex = dataForLanguage;
		base.ui.btn_activity.language.selectedIndex = dataForLanguage;
		if (StaticConfigure.Activity.InfoDict.TryGetValue(_activityId, out var value))
		{
			DateTime dateTime = value.EndTime.ToDateTime();
			DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
			if (serverTime > dateTime)
			{
				base.ui.txt_timeTip.visible = false;
				return;
			}
			TimeSpan timeSpan = dateTime - serverTime;
			base.ui.txt_timeTip.text = string.Format(1141.GetLocal(UIStringType.Message), timeSpan.Days.ToString().PadLeft(2, '0'), (timeSpan.Hours + 1).ToString().PadLeft(2, '0'));
			base.ui.txt_timeTip.visible = true;
		}
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_buy.onClick.Add(OnBtnBuyClick);
		base.ui.btn_activity.onClick.Add(OnBtnActivityClick);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_buy.onClick.Remove(OnBtnBuyClick);
		base.ui.btn_activity.onClick.Remove(OnBtnActivityClick);
	}

	private async void OnBtnBuyClick()
	{
		base.ui.btn_buy.onClick.Retain();
		CollaborationInfoConfigure infoConfig = SimpleSingletonProvider<GameLogicManager>.inst.collaborate.TryGetCollaboration();
		await SimpleSingletonProvider<UIManager>.inst.mgwtStoreWindow.ShowMGWTStore(infoConfig);
		base.ui.btn_buy.onClick.Release();
	}

	private async void OnBtnActivityClick()
	{
		base.ui.btn_activity.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.ActivityMgwt);
		base.ui.btn_activity.onClick.Release();
	}
}
