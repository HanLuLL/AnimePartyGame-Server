using System;
using Core;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;
using party.model;

namespace UI;

public class UIButton_Buff : GButton
{
	public BuffInfoConfigure BuffConfig;

	public Controller showframe;

	public GLoader loader_Icon;

	public GTextField txt_Round;

	public GTextField txt_Num;

	public const string URL = "ui://1ov1i0v9hqftbs";

	public void RefreshBuff(Buff buffData)
	{
		BuffConfig = buffData.BuffId.GetBuffConfigure();
		if (string.IsNullOrEmpty(BuffConfig.Icon))
		{
			Debug.LogError($"buffId: {buffData.BuffId} 没有图标可用于展示，请检查！！");
		}
		loader_Icon.url = BuffConfig.Icon;
		txt_Round.visible = buffData.KeepRound > 0;
		txt_Num.visible = buffData.Progress > 0;
		txt_Num.text = buffData.Progress.ToString();
		txt_Round.text = buffData.KeepRound.ToString();
		base.visible = true;
	}

	public void RefreshProperty(PropertyData<int> PropertyData, bool dynamic)
	{
		if (PropertyData?.property == null)
		{
			base.visible = false;
			return;
		}
		BuffConfig = PropertyData.buffId.GetBuffConfigure();
		txt_Round.visible = false;
		loader_Icon.url = BuffConfig.Icon;
		if (dynamic)
		{
			PropertyData.UpdateAction = (Action<int>)Delegate.Remove(PropertyData.UpdateAction, new Action<int>(UpdateValue));
			PropertyData.UpdateAction = (Action<int>)Delegate.Combine(PropertyData.UpdateAction, new Action<int>(UpdateValue));
		}
		UpdateValue(PropertyData.property.Value);
	}

	private void UpdateValue(int property)
	{
		if (txt_Num != null)
		{
			txt_Num.visible = property > 0;
			txt_Num.text = property.ToString();
			base.visible = property != 0;
		}
		else
		{
			Debug.LogError("尝试更新特殊属性buff信息出现问题，请检查");
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public static UIButton_Buff CreateInstance()
	{
		return (UIButton_Buff)UIPackage.CreateObject("Common_Internal", "Button_Buff");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showframe = GetControllerAt(1);
		loader_Icon = (GLoader)GetChildAt(1);
		txt_Round = (GTextField)GetChildAt(2);
		txt_Num = (GTextField)GetChildAt(3);
	}
}
