using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGlobalModalWaiting : GComponent
{
	private readonly BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	public GLoader mohu;

	public const string URL = "ui://xuaw6o8jth3ls8e";

	public async UniTask SetBlur()
	{
		await blurBgCtrl.CreateBlurTex();
		blurBgCtrl.OnShown(mohu);
	}

	public override void Dispose()
	{
		blurBgCtrl.OnHide();
		base.Dispose();
	}

	public static UIGlobalModalWaiting CreateInstance()
	{
		return (UIGlobalModalWaiting)UIPackage.CreateObject("Common", "GlobalModalWaiting");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GLoader)GetChildAt(0);
	}
}
