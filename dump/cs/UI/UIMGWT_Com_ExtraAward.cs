using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using Tools;

namespace UI;

public class UIMGWT_Com_ExtraAward : GComponent
{
	private List<KeyValuePair<int, int>> item_List = new List<KeyValuePair<int, int>>();

	public GList Item_List;

	public const string URL = "ui://2p754tqkrc482f";

	public void Init()
	{
		Item_List.itemRenderer = OnItemRender;
	}

	public void SetData(int GoodsId)
	{
		CollaborationGoodsConfigure collaborationGoodsConfigure = GoodsId.GetCollaborationGoodsConfigure();
		base.visible = collaborationGoodsConfigure != null;
		if (collaborationGoodsConfigure == null)
		{
			base.visible = false;
			return;
		}
		base.visible = true;
		item_List.Clear();
		foreach (KeyValuePair<int, int> extraItemReward in collaborationGoodsConfigure.ExtraItemRewards)
		{
			item_List.Add(extraItemReward);
		}
		foreach (KeyValuePair<int, int> extraBackgroundReward in collaborationGoodsConfigure.ExtraBackgroundRewards)
		{
			item_List.Add(extraBackgroundReward);
		}
		Item_List.numItems = item_List.Count;
	}

	private void OnItemRender(int index, GObject item)
	{
		if (item is UIMGWT_Com_CommonItem uIMGWT_Com_CommonItem && index <= item_List.Count)
		{
			KeyValuePair<int, int> itemDate = item_List[index];
			uIMGWT_Com_CommonItem.txt_title.text = $"x{itemDate.Value}";
			uIMGWT_Com_CommonItem.loader_Icon.url = itemDate.Key.GetItemInfoConfigure().ShowIcon;
			uIMGWT_Com_CommonItem.onClick.Set((EventCallback0)delegate
			{
				SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(itemDate.Key, itemDate.Value, _Usable: false).Forget();
			});
		}
	}

	public static UIMGWT_Com_ExtraAward CreateInstance()
	{
		return (UIMGWT_Com_ExtraAward)UIPackage.CreateObject("MGWTStore", "MGWT_Com_ExtraAward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Item_List = (GList)GetChildAt(1);
	}
}
