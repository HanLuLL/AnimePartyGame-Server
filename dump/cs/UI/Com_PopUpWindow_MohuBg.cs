using Cysharp.Threading.Tasks;

namespace UI;

public class Com_PopUpWindow_MohuBg : BaseWindow
{
	public Com_PopUpWindow_MohuBg(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UICom_PopUpWindow_MohuBg.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
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
		_ = base.contentPane is UICom_PopUpWindow_MohuBg;
	}

	protected override void OnHide()
	{
		base.OnHide();
		_ = base.contentPane is UICom_PopUpWindow_MohuBg;
	}
}
