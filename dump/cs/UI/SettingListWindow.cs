using Cysharp.Threading.Tasks;
using FairyGUI;

namespace UI;

public class SettingListWindow : BaseWindow
{
	public enum SettingListType
	{
		Developer,
		Thanks
	}

	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public SettingListWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UISettingListWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		Thanks_InitComponent();
		AddEvent();
		blurBgCtrl.OnShown(this);
	}

	protected override void OnHide()
	{
		RemoveEvent();
		base.OnHide();
		blurBgCtrl.OnHide();
	}

	public async UniTask ShowList(SettingListType type)
	{
		await TryShowAsync();
		if (base.contentPane is UISettingListWindow uISettingListWindow)
		{
			uISettingListWindow.showDeveloper.selectedIndex = (int)type;
			switch (type)
			{
			case SettingListType.Developer:
				uISettingListWindow.developer.title.text = StaticConfigure.Developer.Developers[0].Content;
				break;
			case SettingListType.Thanks:
				Thanks_Refresh();
				break;
			}
		}
	}

	private void Thanks_InitComponent()
	{
		if (base.contentPane is UISettingListWindow uISettingListWindow)
		{
			uISettingListWindow.list.SetVirtual();
			uISettingListWindow.list.itemRenderer = OnRenderThanksList;
		}
	}

	private void Thanks_Refresh()
	{
		if (base.contentPane is UISettingListWindow uISettingListWindow)
		{
			uISettingListWindow.tab.selectedIndex = 0;
			uISettingListWindow.tab.onChanged.Call();
		}
	}

	private void AddEvent()
	{
		if (base.contentPane is UISettingListWindow uISettingListWindow)
		{
			uISettingListWindow.tab.onChanged.Add(OnThanksTabChanged);
			uISettingListWindow.mohu.onClick.Add(CloseWindow);
		}
	}

	private void RemoveEvent()
	{
		if (base.contentPane is UISettingListWindow uISettingListWindow)
		{
			uISettingListWindow.tab.onChanged.Remove(OnThanksTabChanged);
			uISettingListWindow.mohu.onClick.Remove(CloseWindow);
		}
	}

	private void OnThanksTabChanged()
	{
	}

	private void OnRenderThanksList(int index, GObject gObj)
	{
	}

	private void CloseWindow()
	{
		if (base.contentPane is UISettingListWindow uISettingListWindow)
		{
			uISettingListWindow.mohu.onClick.Retain();
			Hide();
			uISettingListWindow.mohu.onClick.Release();
		}
	}
}
