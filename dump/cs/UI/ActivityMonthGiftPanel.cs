using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityMonthGiftPanel : BasePanel<UIActivityMonthGiftPanel>
{
	private ActivityInfoConfigure _activityInfoConfigure;

	private const int ActivityId = 512193;

	public ActivityMonthGiftPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityMonthGiftPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (!StaticConfigure.Activity.InfoDict.TryGetValue(512193, out _activityInfoConfigure))
		{
			Debug.LogError($"[ActivityMonthGiftPanel] 无法从StaticConfigure.Activity.InfoDict中取出ID:{512193}的数据");
		}
		else
		{
			base.ui.Cut_in.Play();
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.ActivityMonthGift_bg.MallScreen();
		base.ui.mohu.SetSize(GRoot.inst.width, GRoot.inst.height);
	}

	public override void Refresh()
	{
		base.Refresh();
		base.ui.ActivityMonthGift_Window.visible = false;
		base.ui.ActivityMonthGift_Time.text = TimeHelper.GetDurationText(_activityInfoConfigure.BeginTime, _activityInfoConfigure.EndTime);
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo().RechargeSum > 0;
		base.ui.Can_Condition.visible = flag;
		base.ui.Cannot_Condition.visible = !flag;
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.ActivityMonthGift_Btn_Message.onClick.Add(OnOpenMonthGiftInfo);
		base.ui.mohu.onClick.Add(OnCloseMonthGiftInfo);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.ActivityMonthGift_Btn_Message.onClick.Remove(OnOpenMonthGiftInfo);
		base.ui.mohu.onClick.Remove(OnCloseMonthGiftInfo);
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

	private void OnCloseMonthGiftInfo()
	{
		base.ui.mohu.onClick.Retain();
		base.ui.ActivityMonthGift_Window.visible = false;
		base.ui.mohu.onClick.Release();
	}

	private void OnOpenMonthGiftInfo(EventContext context)
	{
		base.ui.ActivityMonthGift_Btn_Message.onClick.Retain();
		base.ui.ActivityMonthGift_Window.visible = true;
		base.ui.ActivityMonthGift_Btn_Message.onClick.Release();
	}
}
