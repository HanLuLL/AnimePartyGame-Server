using Core;
using Cysharp.Threading.Tasks;
using Tools;

namespace UI;

public class LoadingWindow : BaseWindow
{
	public LoadingWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILoadingWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask TryShowAsync()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UILoadingWindow uILoadingWindow)
		{
			uILoadingWindow.type.selectedIndex = 0;
		}
	}

	public async UniTask CutIn()
	{
		await TryShowAsync();
		if (base.contentPane is UILoadingWindow uILoadingWindow)
		{
			uILoadingWindow.type.selectedIndex = 2;
			uILoadingWindow.com_MagicSchool.Cut_in.Play();
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
		}
	}

	public async UniTask CutOut()
	{
		await TryShowAsync();
		if (base.contentPane is UILoadingWindow uILoadingWindow)
		{
			uILoadingWindow.com_MagicSchool.Cut_in.Stop();
			uILoadingWindow.com_MagicSchool.Cut_out.Play(base.Hide);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(900);
		}
	}

	public async UniTask SceneCutIn()
	{
		await TryShowAsync();
		if (base.contentPane is UILoadingWindow uILoadingWindow)
		{
			uILoadingWindow.type.selectedIndex = 1;
			uILoadingWindow.com_SceneCut.language.selectedIndex = GameSettings.GetDataForLanguage(1, 2, 0, 0);
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1000);
		}
	}

	public async UniTask SceneCutOut()
	{
		await TryShowAsync();
		if (base.contentPane is UILoadingWindow)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(900);
			Hide();
		}
	}
}
