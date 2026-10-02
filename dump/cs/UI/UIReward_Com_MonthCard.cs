using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIReward_Com_MonthCard : GComponent
{
	private MonthlyCardData monthlyCardData;

	private readonly List<KeyValuePair<int, int>> itemList = new List<KeyValuePair<int, int>>();

	public GGraph mohu;

	public GList list_Prop;

	public GTextField txt_Name;

	public GTextField txt_Time;

	public const string URL = "ui://r5u0087tm1i7o";

	public void AddEvent()
	{
	}

	public void RemoveEvent()
	{
		list_Prop.numItems = 0;
	}

	public void ShowMonthCardAllReward()
	{
		itemList.Clear();
		monthlyCardData = SimpleSingletonProvider<GameLogicManager>.inst.store.GetMonthlyCardConfig();
		txt_Time.text = string.Format(1039.GetLocal(UIStringType.Message), monthlyCardData.deadlineDay);
		KeyValuePair<int, int> item = monthlyCardData.monthlyCardConfig.ImmediatelyReward.ElementAt(0);
		KeyValuePair<int, int> item2 = monthlyCardData.monthlyCardConfig.DailyReward.ElementAt(0);
		itemList.Add(item);
		itemList.Add(item2);
		list_Prop.itemRenderer = RendererReward;
		list_Prop.numItems = itemList.Count;
	}

	public void ShowMonthCardPurchaseReward()
	{
		itemList.Clear();
		monthlyCardData = SimpleSingletonProvider<GameLogicManager>.inst.store.GetMonthlyCardConfig();
		txt_Time.text = string.Format(1039.GetLocal(UIStringType.Message), monthlyCardData.deadlineDay);
		KeyValuePair<int, int> item = monthlyCardData.monthlyCardConfig.ImmediatelyReward.ElementAt(0);
		itemList.Add(item);
		list_Prop.itemRenderer = RendererReward;
		list_Prop.numItems = itemList.Count;
	}

	public void RendererReward(int index, GObject item)
	{
		if (item is UICom_LitItem uICom_LitItem)
		{
			ItemInfoConfigure itemInfoConfigure = itemList[index].Key.GetItemInfoConfigure();
			uICom_LitItem.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
			uICom_LitItem.loader_Icon.url = itemInfoConfigure.ShowIcon;
			uICom_LitItem.txt_itemNum.text = itemList[index].Value.ToString();
		}
	}

	public static UIReward_Com_MonthCard CreateInstance()
	{
		return (UIReward_Com_MonthCard)UIPackage.CreateObject("Reward", "Reward_Com_MonthCard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		list_Prop = (GList)GetChildAt(2);
		txt_Name = (GTextField)GetChildAt(3);
		txt_Time = (GTextField)GetChildAt(4);
	}
}
