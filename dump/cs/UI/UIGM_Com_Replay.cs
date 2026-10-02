using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIGM_Com_Replay : GComponent
{
	public GList list_Server;

	public const string URL = "ui://725vhs9y7g9qx";

	public void Refresh()
	{
		list_Server.onClickItem.Add((EventCallback0)delegate
		{
			SimpleSingletonProvider<GameLogicManager>.inst.replay.ChangeReplayCDNUrl(list_Server.selectedIndex);
		});
	}

	public static UIGM_Com_Replay CreateInstance()
	{
		return (UIGM_Com_Replay)UIPackage.CreateObject("GM", "GM_Com_Replay");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Server = (GList)GetChildAt(1);
	}
}
