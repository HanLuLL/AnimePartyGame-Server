using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using Tools;

namespace UI;

public class UIChat_Com_ExpressionItem : GComponent
{
	public Controller type;

	public GGraph video_exp;

	public GLoader loader_exp;

	public const string URL = "ui://y0luzhk8ednm10";

	public bool PlayExpression(int expressionId)
	{
		if (!StaticConfigure.Chat.ExpressionDic.TryGetValue(expressionId, out var value))
		{
			return false;
		}
		bool flag = !string.IsNullOrEmpty(value.VideoKey);
		if (flag)
		{
			video_exp.visible = false;
			SimpleSingletonProvider<CriMovieManager>.inst.PlaAutoReleaseVideo(value.VideoKey, video_exp, 0, PlayEndImmediatelyStop: false, uiRenderMode: true, 10).Forget();
		}
		else
		{
			ItemInfoConfigure itemInfoConfigure = value.ItemID.GetItemInfoConfigure();
			loader_exp.url = itemInfoConfigure?.ShowIcon ?? "";
			loader_exp.visible = true;
		}
		type.selectedIndex = (flag ? 1 : 0);
		return true;
	}

	public static UIChat_Com_ExpressionItem CreateInstance()
	{
		return (UIChat_Com_ExpressionItem)UIPackage.CreateObject("Chat", "Chat_Com_ExpressionItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		video_exp = (GGraph)GetChildAt(2);
		loader_exp = (GLoader)GetChildAt(3);
	}
}
