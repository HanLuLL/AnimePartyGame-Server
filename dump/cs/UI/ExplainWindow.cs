using System;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class ExplainWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private RepeatedField<TutorialinfoConfigureItem> tutorialInfo;

	private Action CloseTutorialEvent;

	private int curExplainIndex;

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public ExplainWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIExplainWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		await blurBgCtrl.CreateBlurTex();
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	private void OnTypeControllerChange()
	{
		if (base.contentPane is UIExplainWindow uIExplainWindow)
		{
			base.BgLoader.visible = uIExplainWindow.type.selectedIndex == 1;
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIExplainWindow uIExplainWindow && uIExplainWindow.com_PVP.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom && uIExplainWindow.com_PVE.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom2)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Add(base.Hide);
			uIExplainWindow.graph_mask.onClick.Add(base.Hide);
			uICom_PopUpWindow_Bottom2.closeButton.onClick.Add(base.Hide);
			uIExplainWindow.com_PVE.btn_Left.onClick.Add(LeftExplain);
			uIExplainWindow.com_PVE.btn_Right.onClick.Add(RightExplain);
			blurBgCtrl.OnShown(this);
			uIExplainWindow.type.onChanged.Add(OnTypeControllerChange);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.SinglePlayer && SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.guide.FinishTutorialStepGuide();
		}
		if (base.contentPane is UIExplainWindow uIExplainWindow && uIExplainWindow.com_PVP.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom && uIExplainWindow.com_PVE.bottom is UICom_PopUpWindow_Bottom uICom_PopUpWindow_Bottom2)
		{
			uICom_PopUpWindow_Bottom.closeButton.onClick.Remove(base.Hide);
			uIExplainWindow.graph_mask.onClick.Remove(base.Hide);
			uICom_PopUpWindow_Bottom2.closeButton.onClick.Remove(base.Hide);
			uIExplainWindow.com_PVE.btn_Left.onClick.Remove(LeftExplain);
			uIExplainWindow.com_PVE.btn_Right.onClick.Remove(RightExplain);
			CloseTutorialEvent?.Invoke();
			blurBgCtrl.OnHide();
			uIExplainWindow.type.onChanged.Remove(OnTypeControllerChange);
		}
	}

	public async UniTask ShowPVPTip(int upgradePlan)
	{
		await TryShowAsync();
		if (base.contentPane is UIExplainWindow uIExplainWindow)
		{
			uIExplainWindow.type.selectedIndex = 0;
			RepeatedField<UpgradeDataConfigureItem> upgradeDataConfigureItems = StaticConfigure.Upgrade.DataDict[upgradePlan].UpgradeDataConfigureItems;
			uIExplainWindow.com_PVP.txt_GoldLV1.text = upgradeDataConfigureItems[0].Gold.ToString();
			uIExplainWindow.com_PVP.txt_GoldLV2.text = upgradeDataConfigureItems[1].Gold.ToString();
			uIExplainWindow.com_PVP.txt_GoldLV3.text = upgradeDataConfigureItems[2].Gold.ToString();
		}
	}

	public async UniTask ShowPVETutorial(int mapId)
	{
		if (StaticConfigure.Map.InfoDict.TryGetValue(mapId, out var mapInfo) && mapInfo.TutorialId != 0)
		{
			await TryShowAsync();
			if (base.contentPane is UIExplainWindow uIExplainWindow)
			{
				uIExplainWindow.type.selectedIndex = 1;
				tutorialInfo = StaticConfigure.Tutorial.InfoDict[mapInfo.TutorialId].TutorialinfoConfigureItems;
				RendererExplain(0);
			}
		}
	}

	public async UniTask ShowTutorial(int tutorialId, Action closeEvent = null)
	{
		CloseTutorialEvent = closeEvent;
		await TryShowAsync();
		if (base.contentPane is UIExplainWindow uIExplainWindow)
		{
			uIExplainWindow.type.selectedIndex = 1;
			if (StaticConfigure.Tutorial.InfoDict.TryGetValue(tutorialId, out var value))
			{
				tutorialInfo = value.TutorialinfoConfigureItems;
				RendererExplain(0);
			}
		}
	}

	private void RendererExplain(int index)
	{
		if (base.contentPane is UIExplainWindow uIExplainWindow)
		{
			curExplainIndex = index;
			UIExplain_Com_PVE com_PVE = uIExplainWindow.com_PVE;
			com_PVE.loader_Image.url = tutorialInfo[index].ImageName.GetImageLocalization();
			com_PVE.txt_Page.SetVar("curIndex", (curExplainIndex + 1).ToString()).SetVar("maxIndex", tutorialInfo.Count.ToString()).FlushVars();
			com_PVE.txt_Explain.text = tutorialInfo[index].MsgIDMobile.GetLocal(UIStringType.Message);
		}
	}

	private void LeftExplain(EventContext context)
	{
		if (base.contentPane is UIExplainWindow uIExplainWindow)
		{
			UIExplain_Com_PVE com_PVE = uIExplainWindow.com_PVE;
			com_PVE.btn_Left.onClick.Retain();
			int count = tutorialInfo.Count;
			int num = curExplainIndex - 1;
			RendererExplain((num < 0) ? (count - 1) : num);
			com_PVE.btn_Left.onClick.Release();
		}
	}

	private void RightExplain(EventContext context)
	{
		if (base.contentPane is UIExplainWindow uIExplainWindow)
		{
			UIExplain_Com_PVE com_PVE = uIExplainWindow.com_PVE;
			com_PVE.btn_Right.onClick.Retain();
			int count = tutorialInfo.Count;
			RendererExplain((curExplainIndex + 1) % count);
			com_PVE.btn_Right.onClick.Release();
		}
	}
}
