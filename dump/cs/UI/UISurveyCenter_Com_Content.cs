using System;
using Core.Net;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UISurveyCenter_Com_Content : GComponent
{
	public GTextField txt_head;

	public GTextField txt_time;

	public GLoader loader_img;

	public GTextField txt_content;

	public GTextField txt_tip;

	public const string URL = "ui://s9p4q57hmyup2";

	public void RefreshSurvey(SurveyData surveyData)
	{
		txt_head.text = surveyData.surveyInfoConfigure.TitleID.GetLocal(UIStringType.Message);
		loader_img.url = surveyData.surveyInfoConfigure.SurveyImage;
		txt_content.text = surveyData.surveyInfoConfigure.TextID.GetLocal(UIStringType.Message);
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		DateTime endTime;
		switch (surveyData.surveyInfoConfigure.SurveyType)
		{
		case SurveyType.Noob:
		case SurveyType.Return:
			endTime = (surveyData.serverData.CreateTime * 1000).StampMillisecondsToDateTime().AddDays(surveyData.surveyInfoConfigure.Duration);
			break;
		case SurveyType.Version:
			endTime = surveyData.surveyInfoConfigure.EndTime.ToDateTime();
			break;
		default:
			txt_time.text = string.Empty;
			return;
		}
		txt_time.text = TimeHelper.RefreshTimeText(1033, 1034, serverTime, endTime);
	}

	public static UISurveyCenter_Com_Content CreateInstance()
	{
		return (UISurveyCenter_Com_Content)UIPackage.CreateObject("SurveyCenter", "SurveyCenter_Com_Content");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_head = (GTextField)GetChildAt(1);
		txt_time = (GTextField)GetChildAt(2);
		loader_img = (GLoader)GetChildAt(3);
		txt_content = (GTextField)GetChildAt(4);
		txt_tip = (GTextField)GetChildAt(6);
	}
}
