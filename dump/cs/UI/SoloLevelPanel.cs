using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;

namespace UI;

public class SoloLevelPanel : BasePanel<UISoloLevelPanel>
{
	public SoloLevelPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UISoloLevelPanel.CreateInstance();
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
	}

	public override void Refresh()
	{
		base.Refresh();
		AdultMode(inAdultMode: false);
		RefreshTrainingButton();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(ReturnPanel);
		base.ui.btn_BasicMode.onClick.Add(OpenCampaign);
		base.ui.btn_TrainingMode.onClick.Add(OpenTrainingMode);
		base.ui.btn_ToSoloMode.onClick.Add(OnClickToSoloMode);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
		base.ui.btn_BasicMode.onClick.Remove(OpenCampaign);
		base.ui.btn_TrainingMode.onClick.Remove(OpenTrainingMode);
		base.ui.btn_ToSoloMode.onClick.Remove(OnClickToSoloMode);
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
	}

	public override void AdultMode(bool inAdultMode)
	{
		base.ui.btn_TrainingMode.angleMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
		base.AdultMode(inAdultMode);
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}

	private async void OpenCampaign()
	{
		base.ui.btn_BasicMode.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Campaign);
		base.ui.btn_BasicMode.onClick.Release();
	}

	private async void OpenTrainingMode()
	{
		base.ui.btn_TrainingMode.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.TrainingLevel);
		base.ui.btn_TrainingMode.onClick.Release();
	}

	private async void OnClickToSoloMode()
	{
		base.ui.btn_ToSoloMode.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(512070);
		base.ui.btn_ToSoloMode.onClick.Release();
	}

	private void RefreshTrainingButton()
	{
		int num = GetMapCout(MapModeType.PracticePve) + GetMapCout(MapModeType.PracticePvp);
		base.ui.btn_TrainingMode.txt_Tip.SetVar("mapNumb", num.ToString()).FlushVars();
	}

	private int GetMapCout(MapModeType type)
	{
		if (StaticConfigure.GameMode.InfoDict.TryGetValue((int)type, out var value) && TimeHelper.ValidityTime(value.BeginTime, value.EndTime))
		{
			return value.MapID.Count;
		}
		return 0;
	}

	public async UniTask<(Vector2, float, float)> ShowBasicModeMask()
	{
		_FocusStatus = false;
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => base.ui.Cut_in.playing, SimpleSingletonProvider<UIManager>.inst.tutorial.Hide);
		Vector2 pt = base.ui.btn_BasicMode.LocalToGlobal(Vector2.zero);
		Vector2 vector = GRoot.inst.GlobalToLocal(pt);
		float width = base.ui.btn_BasicMode.width;
		float height = base.ui.btn_BasicMode.height;
		SimpleSingletonProvider<UIManager>.inst.tutorial.ShowGuideMask(vector, width, height, isRect: true);
		return (vector, width, height);
	}

	public async UniTask FireClickOpenSoloLevel()
	{
		base.ui.btn_BasicMode.FireClick(downEffect: true);
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Campaign);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.ShowCampaignLevelMask();
	}
}
