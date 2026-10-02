using Core;
using FairyGUI;
using Tools;

namespace UI;

public class ShoppingPanel : BasePanel<UIShoppingPanel>
{
	public ShoppingPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIShoppingPanel.CreateInstance();
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
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.com_Main.btn_Gacha.onClick.Add(OpenGachaPanel);
		base.ui.com_Main.btn_Mall.onClick.Add(OpenMallPanel);
		base.ui.btn_Return.onClick.Add(ReturnPanel);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.com_Main.btn_Gacha.onClick.Remove(OpenGachaPanel);
		base.ui.com_Main.btn_Mall.onClick.Remove(OpenMallPanel);
		base.ui.btn_Return.onClick.Remove(ReturnPanel);
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

	public override void AdultMode(bool inAdultMode)
	{
		base.ui.com_Main.btn_Gacha.angelMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
		base.ui.com_Main.btn_Mall.angelMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
	}

	private async void ReturnPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		base.ui.com_Main.display.PlayReverse();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Home);
		base.ui.btn_Return.onClick.Release();
	}

	private async void OpenGachaPanel(EventContext context)
	{
		base.ui.com_Main.btn_Gacha.onClick.Retain();
		base.ui.com_Main.display.PlayReverse();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Gacha);
		base.ui.com_Main.btn_Gacha.onClick.Release();
	}

	private async void OpenMallPanel(EventContext context)
	{
		base.ui.com_Main.btn_Mall.onClick.Retain();
		base.ui.com_Main.display.PlayReverse();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.ProductRecommendation);
		base.ui.com_Main.btn_Mall.onClick.Release();
	}
}
