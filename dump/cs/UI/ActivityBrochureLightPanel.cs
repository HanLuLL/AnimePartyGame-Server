using Core;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class ActivityBrochureLightPanel : BasePanel<UIActivityBrochureLightPanel>
{
	private const int _leftActivityId = 606271;

	private const int _rightActivityId = 606261;

	public ActivityBrochureLightPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityBrochureLightPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.loader.MallScreen();
		RefreshActivityInfo(606271, base.ui.btn_LeftGoWay);
		RefreshActivityInfo(606261, base.ui.btn_RightGoWay);
		RefreshAngelMode();
	}

	private void RefreshActivityInfo(int activityId, GButton btnGoWay)
	{
		btnGoWay.onClick.Set((EventCallback0)async delegate
		{
			btnGoWay.onClick.Retain();
			RepeatedField<ActivityActivityEntrance2Configure> activityEntrance2S = StaticConfigure.Activity.ActivityEntrance2S;
			foreach (ActivityActivityEntrance2Configure item in activityEntrance2S)
			{
				if (item.ActivityID == activityId)
				{
					await SimpleSingletonProvider<GameLogicManager>.inst.home.OpenActivity(item);
					break;
				}
			}
			btnGoWay.onClick.Release();
		});
	}

	protected override void InitComponents()
	{
		base.InitComponents();
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
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

	private void RefreshAngelMode()
	{
		base.ui.angelMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
	}
}
