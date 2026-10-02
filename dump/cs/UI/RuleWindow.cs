using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;

namespace UI;

public class RuleWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	private UIRule_Com_Excel_01 Com_Excel_01;

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public RuleWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIRuleWindow.CreateInstance();
		base.OnInit();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIRuleWindow uIRuleWindow)
		{
			uIRuleWindow.mohu.onClick.Add(base.Hide);
			uIRuleWindow.btn_Close.onClick.Add(base.Hide);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.LoseFocus();
			blurBgCtrl.OnShown(this);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIRuleWindow uIRuleWindow)
		{
			if (Com_Excel_01 != null && uIRuleWindow.com_Content != null)
			{
				uIRuleWindow.com_Content.RemoveChild(Com_Excel_01, dispose: true);
			}
			uIRuleWindow.mohu.onClick.Remove(base.Hide);
			uIRuleWindow.btn_Close.onClick.Remove(base.Hide);
			SimpleSingletonProvider<UIManager>.inst.currentPanel?.ResumeFocus();
			blurBgCtrl.OnHide();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		if (base.contentPane is UIRuleWindow && base.isShowing && context.inputEvent.keyCode == KeyCode.Escape)
		{
			Hide();
		}
	}

	public async UniTask TryShow(int titleId, int contentId)
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
		if (base.contentPane is UIRuleWindow uIRuleWindow)
		{
			uIRuleWindow.txt_Title.text = titleId.GetLocal(UIStringType.Message);
			uIRuleWindow.com_Content.txt_Content.text = contentId.GetLocal(UIStringType.Message);
		}
	}

	public async UniTask TryShow(string title, string content)
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
		if (base.contentPane is UIRuleWindow uIRuleWindow)
		{
			uIRuleWindow.txt_Title.text = title;
			uIRuleWindow.com_Content.txt_Content.text = content;
		}
	}

	public async UniTask TryShowExcel01(string title, string content, string excelFirst, string excelSecond, List<KeyValuePair<string, string>> excelDatas)
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
		if (!(base.contentPane is UIRuleWindow uIRuleWindow))
		{
			return;
		}
		int num = excelFirst.Length + 5;
		int num2 = excelSecond.Length + 5;
		foreach (KeyValuePair<string, string> excelData in excelDatas)
		{
			num = ((excelData.Key.Length > num) ? excelData.Key.Length : num);
			num2 = ((excelData.Value.Length > num2) ? excelData.Value.Length : num2);
		}
		uIRuleWindow.txt_Title.text = title;
		uIRuleWindow.com_Content.txt_Content.text = content;
		Com_Excel_01 = UIRule_Com_Excel_01.CreateInstance();
		float firstWidth = Com_Excel_01.width * ((float)num / (float)(num + num2));
		float secondWidth = Com_Excel_01.width - firstWidth;
		Com_Excel_01.txt_First.width = firstWidth;
		Com_Excel_01.txt_Second.width = secondWidth;
		Com_Excel_01.list_Content.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIRule_Com_Excel_01_Item uIRule_Com_Excel_01_Item)
			{
				KeyValuePair<string, string> keyValuePair = excelDatas[index];
				uIRule_Com_Excel_01_Item.txt_Name.text = keyValuePair.Key;
				uIRule_Com_Excel_01_Item.txt_Name.width = firstWidth;
				uIRule_Com_Excel_01_Item.txt_Data.text = keyValuePair.Value;
				uIRule_Com_Excel_01_Item.txt_Data.width = secondWidth;
			}
		};
		Com_Excel_01.txt_First.text = excelFirst;
		Com_Excel_01.txt_Second.text = excelSecond;
		Com_Excel_01.list_Content.numItems = excelDatas.Count;
		Com_Excel_01.list_Content.ResizeToFit();
		uIRuleWindow.com_Content.AddChild(Com_Excel_01);
		Com_Excel_01.y = uIRuleWindow.com_Content.txt_Content.height + 20f;
	}
}
