using Tools;

namespace UI;

public class BackgroundPanel : BasePanel<UIBackgroundPanel>
{
	public BackgroundPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIBackgroundPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override async void InitComponents()
	{
		base.InitComponents();
		await SimpleSingletonProvider<CriMovieManager>.inst.Play(50.GetVideoKey(), base.ui.loader_BG);
		base.ui.loader_Line.visible = true;
	}

	public override void Refresh()
	{
		base.Refresh();
		ChangeShowStatus(status: true);
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
		if (base.ui != null)
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(base.ui.loader_BG);
		}
		base.Dispose();
	}

	public void ChangeShowStatus(bool status)
	{
		if (base.ui != null)
		{
			base.ui.hide.selectedIndex = ((!status) ? 1 : 0);
		}
	}
}
