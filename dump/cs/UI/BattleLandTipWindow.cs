using Cysharp.Threading.Tasks;

namespace UI;

public class BattleLandTipWindow : BaseWindow
{
	public BattleLandTipWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIBattleLandTipWindow.CreateInstance();
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
		_ = base.contentPane is UIBattleLandTipWindow;
	}

	protected override void OnHide()
	{
		base.OnHide();
		_ = base.contentPane is UIBattleLandTipWindow;
	}

	public async void ShowLandTips(LandType landType)
	{
		await TryShowAsync();
		if (base.contentPane is UIBattleLandTipWindow { com_land: var com_land } && com_land is UICom_LandCard landCard && StaticConfigure.Land.InfoDict.TryGetValue((int)landType, out var landConfig))
		{
			await TryShowAsync();
			landCard.loader_FrontCard.url = landConfig.LandIcon;
			landCard.txt_Name.text = landConfig.NameID.GetLocal(UIStringType.Land);
			landCard.txt_Content.text = landConfig.DescriptionID.GetLocal(UIStringType.Land);
		}
	}

	public void HideLandTips()
	{
		if (base.contentPane is UIBattleLandTipWindow)
		{
			Hide();
		}
	}
}
