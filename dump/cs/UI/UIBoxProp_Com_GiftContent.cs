using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using FairyGUI.Utils;
using Google.Protobuf.Collections;

namespace UI;

public class UIBoxProp_Com_GiftContent : GComponent
{
	private BoxPropWindow Window;

	public GList list_LiveRewrad;

	public const string URL = "ui://crbpicgjaxwdz";

	public void ShowWin(MapField<int, int> reward, BoxPropWindow window)
	{
		Window = window;
		list_LiveRewrad.itemRenderer = delegate(int i, GObject o)
		{
			KeyValuePair<int, int> keyValuePair = reward.ElementAt(i);
			CommonUIManager.RendererLitItem((UICom_LitItem)o, keyValuePair.Key, keyValuePair.Value);
		};
		list_LiveRewrad.numItems = reward.Count;
	}

	public static UIBoxProp_Com_GiftContent CreateInstance()
	{
		return (UIBoxProp_Com_GiftContent)UIPackage.CreateObject("BoxProp", "BoxProp_Com_GiftContent");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_LiveRewrad = (GList)GetChildAt(1);
	}
}
