using System;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityRebatePanel : BasePanel<UIActivityRebatePanel>
{
	private ActivityInfoConfigure _activityConfigure;

	private const int ActivityId = 512192;

	public ActivityRebatePanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityRebatePanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (!StaticConfigure.Activity.InfoDict.TryGetValue(512192, out _activityConfigure))
		{
			Debug.LogError("[ActivityRebatePanel] 初始化数据失败：未找到充值返利活动配置");
		}
		else
		{
			base.ui.Cutin.Play();
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.RebateBackGround.MallScreen();
		if (base.ui.mohu != null)
		{
			base.ui.mohu.SetSize(GRoot.inst.width, GRoot.inst.height);
		}
	}

	public override void Refresh()
	{
		base.Refresh();
		base.ui.Rebate_Time.text = TimeHelper.GetDurationText(_activityConfigure.BeginTime, _activityConfigure.EndTime);
		base.ui.RebateWindow.visible = false;
		int rechargeSum = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().RechargeSum;
		base.ui.Rebate_num_Text.text = (rechargeSum / 100).ToString();
		int num = CalculateRebateStarCountByTier(rechargeSum);
		base.ui.Rebate_num_Text1.text = num.ToString();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.Go_Btn.onClick.Add(OpenRechargePanel);
		base.ui.Rebate_Message_Btn.onClick.Add(OnOpenRebateInfo);
		base.ui.mohu.onClick.Add(CloseRebateInfo);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.Go_Btn.onClick.Remove(OpenRechargePanel);
		base.ui.Rebate_Message_Btn.onClick.Remove(OnOpenRebateInfo);
		base.ui.mohu.onClick.Remove(CloseRebateInfo);
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void OpenRechargePanel()
	{
		base.ui.Go_Btn.onClick.Retain();
		RepeatedField<int> repeatedField = new RepeatedField<int> { 7 };
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Store, repeatedField);
		base.ui.Go_Btn.onClick.Release();
	}

	private void OnOpenRebateInfo()
	{
		base.ui.Rebate_Message_Btn.onClick.Retain();
		base.ui.RebateWindow.visible = true;
		base.ui.Rebate_Message_Btn.onClick.Release();
	}

	private void CloseRebateInfo()
	{
		base.ui.mohu.onClick.Retain();
		base.ui.RebateWindow.visible = false;
		base.ui.mohu.onClick.Release();
	}

	private int CalculateRebateStarCountByTier(int totalRechargeAmountInCents)
	{
		if (totalRechargeAmountInCents <= 0)
		{
			return 0;
		}
		int num = (int)Math.Ceiling((double)totalRechargeAmountInCents * 0.01);
		double num2 = 0.0;
		num2 = ((num > 2000) ? (2930.0 + (double)(num - 2000) * 1.1) : ((num > 800) ? (1490.0 + (double)(num - 800) * 1.2) : ((num > 300) ? (740.0 + (double)(num - 300) * 1.5) : ((num <= 100) ? ((double)num * 3.0) : (300.0 + (double)((float)(num - 100) * 2.2f))))));
		num2 *= 10.0;
		return (int)Math.Ceiling(num2);
	}
}
