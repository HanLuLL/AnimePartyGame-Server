using Cysharp.Threading.Tasks;

namespace UI;

public class LandPrayWindow : BaseWindow
{
	public LandPrayWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandPrayWindow.CreateInstance();
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

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UILandPrayWindow uILandPrayWindow)
		{
			uILandPrayWindow.btn_Cancel.onClick.Add(OnCancelPray);
			uILandPrayWindow.btn_Sure.onClick.Add(OnPray);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILandPrayWindow uILandPrayWindow)
		{
			uILandPrayWindow.btn_Cancel.onClick.Remove(OnCancelPray);
			uILandPrayWindow.btn_Sure.onClick.Remove(OnPray);
		}
	}

	public async void ShowPray()
	{
		await TryShow();
		if (base.contentPane is UILandPrayWindow uILandPrayWindow)
		{
			uILandPrayWindow.btn_Cancel.onClick.Release();
			uILandPrayWindow.btn_Sure.onClick.Release();
		}
	}

	private void OnCancelPray()
	{
		if (base.contentPane is UILandPrayWindow uILandPrayWindow)
		{
			uILandPrayWindow.btn_Cancel.onClick.Retain();
			Hide();
		}
	}

	private void OnPray()
	{
		if (base.contentPane is UILandPrayWindow uILandPrayWindow)
		{
			uILandPrayWindow.btn_Sure.onClick.Retain();
			Hide();
		}
	}
}
