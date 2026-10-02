using Core.Mark;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIBattleRelicInfo_Button_Relic : GButton, IMarkTarget
{
	public long currentPlayerId;

	public GGraph graph_Hover;

	public GComponent com_Quality;

	public GLoader loader_Relic;

	public const string URL = "ui://ethkhr1hot0wg";

	bool IMarkTarget.HoverWait => false;

	public void OnMarkHoverEnter()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(currentPlayerId))
		{
			graph_Hover.visible = true;
			if (data is int relicId)
			{
				string relicMsg = BattleRelicMessage.GetRelicMsg(relicId);
				SimpleSingletonProvider<GameLogicManager>.inst.communicate?.MarkSignal?.ShowPreview?.Dispatch(relicMsg);
			}
		}
	}

	public void OnMarkHoverExit()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(currentPlayerId))
		{
			graph_Hover.visible = false;
		}
	}

	public void OnMarkSelected()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(currentPlayerId))
		{
			graph_Hover.visible = false;
			base.onClick.Call();
			BattleRelicInfoWindow battleRelicInfo = SimpleSingletonProvider<UIManager>.inst.battleRelicInfo;
			if (battleRelicInfo.isShowing)
			{
				battleRelicInfo.ChatRelic();
			}
		}
	}

	public void TriggerHoverConfirmed()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(currentPlayerId);
	}

	public Vector2 GetPosition()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(currentPlayerId))
		{
			return Vector2.zero;
		}
		return LocalToGlobal(Vector2.zero);
	}

	public static UIBattleRelicInfo_Button_Relic CreateInstance()
	{
		return (UIBattleRelicInfo_Button_Relic)UIPackage.CreateObject("BattleRelicInfo", "BattleRelicInfo_Button_Relic");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_Hover = (GGraph)GetChildAt(0);
		com_Quality = (GComponent)GetChildAt(1);
		loader_Relic = (GLoader)GetChildAt(3);
	}
}
