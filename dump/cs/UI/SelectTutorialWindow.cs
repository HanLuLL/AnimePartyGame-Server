using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class SelectTutorialWindow : BaseWindow
{
	public SelectTutorialWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UISelectTutorialWindow.CreateInstance();
		base.OnInit();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UISelectTutorialWindow { bottom: UICom_PopUpWindow_Bottom bottom } uISelectTutorialWindow)
		{
			uISelectTutorialWindow.mohu.onClick.Add(base.Hide);
			bottom.closeButton.onClick.Add(base.Hide);
			uISelectTutorialWindow.btn_GuideLevel1.onClick.Add(OpenTutorialA);
			uISelectTutorialWindow.btn_GuideLevel2.onClick.Add(OpenTutorialB);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UISelectTutorialWindow { bottom: UICom_PopUpWindow_Bottom bottom } uISelectTutorialWindow)
		{
			uISelectTutorialWindow.mohu.onClick.Remove(base.Hide);
			bottom.closeButton.onClick.Remove(base.Hide);
			uISelectTutorialWindow.btn_GuideLevel1.onClick.Remove(OpenTutorialA);
			uISelectTutorialWindow.btn_GuideLevel2.onClick.Remove(OpenTutorialB);
		}
	}

	private void OpenTutorialA()
	{
		if (base.contentPane is UISelectTutorialWindow uISelectTutorialWindow)
		{
			uISelectTutorialWindow.btn_GuideLevel1.onClick.Retain();
			Hide();
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.StartTutorialScene1001().Forget();
			uISelectTutorialWindow.btn_GuideLevel1.onClick.Release();
		}
	}

	private void OpenTutorialB()
	{
		if (base.contentPane is UISelectTutorialWindow uISelectTutorialWindow)
		{
			uISelectTutorialWindow.btn_GuideLevel2.onClick.Retain();
			Hide();
			SimpleSingletonProvider<GameLogicManager>.inst.tutorial.StartTutorialScene1002().Forget();
			uISelectTutorialWindow.btn_GuideLevel2.onClick.Release();
		}
	}

	private void OpenTutorial(GuideSceneType sceneType)
	{
		if (base.contentPane is UISelectTutorialWindow { bottom: UICom_PopUpWindow_Bottom bottom } uISelectTutorialWindow)
		{
			uISelectTutorialWindow.mohu.onClick.Retain();
			bottom.closeButton.onClick.Retain();
			uISelectTutorialWindow.btn_GuideLevel1.onClick.Retain();
			uISelectTutorialWindow.btn_GuideLevel2.onClick.Retain();
			switch (sceneType)
			{
			case GuideSceneType.GuideSceneA:
				SimpleSingletonProvider<GameLogicManager>.inst.guide.LoadingGuideSceneA();
				Hide();
				break;
			case GuideSceneType.GuideSceneB:
				SimpleSingletonProvider<GameLogicManager>.inst.guide.LoadingGuideSceneB();
				Hide();
				break;
			}
			uISelectTutorialWindow.btn_GuideLevel1.onClick.Release();
			uISelectTutorialWindow.btn_GuideLevel1.onClick.Release();
			uISelectTutorialWindow.mohu.onClick.Release();
			bottom.closeButton.onClick.Release();
		}
	}

	public async UniTask TryShow()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	public async void GuideSelectTutorialB()
	{
		if (!base.isShowing)
		{
			await TryShow();
		}
		if (base.contentPane is UISelectTutorialWindow uISelectTutorialWindow)
		{
			Vector2 pt = uISelectTutorialWindow.btn_GuideLevel2.LocalToGlobal(Vector2.zero);
			pt = GRoot.inst.GlobalToLocal(pt);
			await SimpleSingletonProvider<UIManager>.inst.guide.ShowGuideMask(pt, uISelectTutorialWindow.btn_GuideLevel2.width, uISelectTutorialWindow.btn_GuideLevel2.height, _needTransparentMask: false, isRect: true);
		}
	}
}
