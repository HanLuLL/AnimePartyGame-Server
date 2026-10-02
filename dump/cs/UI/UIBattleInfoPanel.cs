using System;
using System.Collections.Generic;
using Core;
using Core.Scene;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIBattleInfoPanel : GComponent
{
	private BattlePlayerData _SelfPlayer;

	public Controller showCard;

	public Controller showBuff;

	public Controller GameMode;

	public Controller platform;

	public GGraph graph_ShowTime;

	public GComponent com_PlayerAttrInfos;

	public GComponent com_AttrInfos;

	public GComponent com_UpgradeTips;

	public GTextField txt_Round;

	public GButton btn_ESC;

	public UIBattleInfo_Button_Victory btn_VictoryCondition;

	public UIBattleInfo_Com_LandTip com_LandTip;

	public GComponent loader_Skin;

	public UIBattleInfo_Button_ShowRelic btn_ShowRelic;

	public GList list_Buff;

	public GLoader loader_BuffIcon;

	public GTextField txt_BuffTitle;

	public GRichTextField txt_Buff;

	public GTextField txt_ATK;

	public GTextField txt_DEF;

	public UIBattleInfo_Com_MapInfo com_MapInfo;

	public GComponent com_GameModeComponent;

	public UIBattleInfo_Com_CampaignTask com_CampaignTask;

	public UIBattleInfo_Com_Player com_BattlePlayer;

	public GTextField txt_DicePoint;

	public GButton btn_terms;

	public UIBattleInfo_Com_Clue com_Clue;

	public UIBattleInfo_Com_Watch_FollowSwitch btn_WatchFollowSwitch;

	public Transition showDicePoint;

	public Transition CT_Cut_out;

	public Transition CT_Cut_in;

	public const string URL = "ui://fxejlqlfbqj63";

	public void Refresh()
	{
		_SelfPlayer = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
		{
			UpdateAttrInfoPosition();
			UpdateUpgradeTipPos();
			UpdatePlayerAttrInfoPosition();
		}
	}

	private void UpdatePlayerAttrInfoPosition()
	{
		List<GObject> children = com_PlayerAttrInfos._children;
		if (children == null || children.Count == 0)
		{
			return;
		}
		Camera camera = BattleSceneController.inst?.mainCamera;
		if (camera == null)
		{
			return;
		}
		for (int i = 0; i < children.Count; i++)
		{
			if (children[i] is UICom_PlayerAttrInfo uICom_PlayerAttrInfo)
			{
				Vector3 ownerPos = uICom_PlayerAttrInfo.GetOwnerPos();
				if (ownerPos == Vector3.zero)
				{
					uICom_PlayerAttrInfo.visible = false;
					continue;
				}
				Vector3 vector = camera.WorldToScreenPoint(ownerPos);
				vector.y = (float)Screen.height - vector.y;
				uICom_PlayerAttrInfo.xy = com_AttrInfos.GlobalToLocal(vector);
				uICom_PlayerAttrInfo.visible = true;
			}
		}
	}

	private void UpdateAttrInfoPosition()
	{
		List<GObject> children = com_AttrInfos._children;
		if (children == null || children.Count == 0)
		{
			return;
		}
		Camera camera = BattleSceneController.inst?.mainCamera;
		if (camera == null)
		{
			return;
		}
		for (int i = 0; i < children.Count; i++)
		{
			if (!(children[i] is UICom_AttrInfo uICom_AttrInfo))
			{
				continue;
			}
			Vector3 ownerPos = uICom_AttrInfo.GetOwnerPos();
			if (ownerPos == Vector3.zero)
			{
				uICom_AttrInfo.visible = false;
				continue;
			}
			Vector3 vector = camera.WorldToScreenPoint(ownerPos);
			if (vector.z <= 0f)
			{
				uICom_AttrInfo.visible = false;
				continue;
			}
			vector.y = (float)Screen.height - vector.y;
			Vector2 targetPos = com_AttrInfos.GlobalToLocal(vector) - Vector2.up * 200f;
			if (!(targetPos.x > 0f) || !(targetPos.y > 0f) || !(targetPos.x < GRoot.inst.width) || !(targetPos.y < GRoot.inst.height))
			{
				uICom_AttrInfo.visible = false;
				continue;
			}
			uICom_AttrInfo.UpdateOffset(targetPos);
			uICom_AttrInfo.visible = true;
		}
		for (int j = 0; j < children.Count; j++)
		{
			if (!children[j].visible || !(children[j] is UICom_AttrInfo uICom_AttrInfo2))
			{
				continue;
			}
			for (int k = j + 1; k < children.Count; k++)
			{
				if (!(children[k] is UICom_AttrInfo uICom_AttrInfo3) || uICom_AttrInfo2 == uICom_AttrInfo3 || !uICom_AttrInfo3.visible)
				{
					continue;
				}
				float num = Math.Abs(uICom_AttrInfo2.x - uICom_AttrInfo3.x);
				float num2 = Math.Abs(uICom_AttrInfo2.y - uICom_AttrInfo3.y);
				float num3 = (uICom_AttrInfo2.width + uICom_AttrInfo3.width) * 0.5f - num;
				float num4 = (uICom_AttrInfo2.height + uICom_AttrInfo3.height) * 0.5f - num2;
				if (num3 > 0f && num4 > 0f)
				{
					float num5 = num4 * 0.5f;
					if (uICom_AttrInfo2.y + 0.1f < uICom_AttrInfo3.y)
					{
						uICom_AttrInfo2.y -= num5;
						uICom_AttrInfo3.y += num5;
					}
					else
					{
						uICom_AttrInfo2.y += num5;
						uICom_AttrInfo3.y -= num5;
					}
				}
			}
		}
		List<MoveArrow> arrowPool = SimpleSingletonProvider<MoveArrowManager>.inst.ArrowPool;
		for (int l = 0; l < children.Count; l++)
		{
			if (!(children[l] is UICom_AttrInfo uICom_AttrInfo4))
			{
				continue;
			}
			if (!uICom_AttrInfo4.visible)
			{
				uICom_AttrInfo4.alpha = 1f;
				uICom_AttrInfo4.touchable = true;
				continue;
			}
			bool flag = false;
			if (arrowPool != null && arrowPool.Count > 0)
			{
				for (int m = 0; m < arrowPool.Count; m++)
				{
					MoveArrow moveArrow = arrowPool[m];
					if (moveArrow == null || !moveArrow.gameObject.activeInHierarchy || moveArrow.Arrow == null)
					{
						continue;
					}
					Vector3 vector2 = camera.WorldToScreenPoint(moveArrow.transform.position);
					if (!(vector2.z <= 0f))
					{
						vector2.y = (float)Screen.height - vector2.y;
						Vector2 vector3 = com_AttrInfos.GlobalToLocal(vector2);
						float num6 = Math.Abs(uICom_AttrInfo4.x - vector3.x);
						float num7 = Math.Abs(uICom_AttrInfo4.y - vector3.y);
						float num8 = (uICom_AttrInfo4.width + moveArrow.Arrow.width) * 0.5f - num6;
						float num9 = (uICom_AttrInfo4.height + moveArrow.Arrow.height) * 0.5f - num7;
						if (num8 > 0f && num9 > 0f)
						{
							flag = true;
							break;
						}
					}
				}
			}
			uICom_AttrInfo4.alpha = (flag ? 0.5f : 1f);
			uICom_AttrInfo4.touchable = !flag;
		}
		bool flag2 = _SelfPlayer != null && _SelfPlayer.CharacterInst != null && _SelfPlayer.CharacterInst.canStep > 0;
		for (int n = 0; n < children.Count; n++)
		{
			if (children[n] is UICom_AttrInfo { visible: not false } uICom_AttrInfo5)
			{
				Vector3 ownerPos2 = uICom_AttrInfo5.GetOwnerPos();
				Vector3 vector4 = camera.WorldToScreenPoint(ownerPos2);
				if (vector4.z <= 0f)
				{
					uICom_AttrInfo5.visible = false;
					continue;
				}
				vector4.y = (float)Screen.height - vector4.y;
				Vector2 targetPos2 = TransformPoint(com_AttrInfos.GlobalToLocal(vector4), uICom_AttrInfo5) - Vector2.up * 120f;
				uICom_AttrInfo5.UpdateGuideLine(targetPos2);
				uICom_AttrInfo5.graph_ShowBuff.visible = !flag2;
			}
		}
	}

	public void SwitchMonsterInfo(bool status)
	{
		List<GObject> children = com_AttrInfos._children;
		if (children == null || children.Count == 0)
		{
			return;
		}
		for (int i = 0; i < children.Count; i++)
		{
			if (children[i] is UICom_AttrInfo uICom_AttrInfo)
			{
				uICom_AttrInfo.SwitchInfo(status);
			}
		}
	}

	private void UpdateUpgradeTipPos()
	{
		List<GObject> children = com_UpgradeTips._children;
		if (children == null || children.Count == 0)
		{
			return;
		}
		Camera camera = BattleSceneController.inst?.mainCamera;
		if (camera == null)
		{
			return;
		}
		for (int i = 0; i < children.Count; i++)
		{
			if (children[i] != null && children[i] is UIBattleInfo_Com_UpgradeTips uIBattleInfo_Com_UpgradeTips && !(uIBattleInfo_Com_UpgradeTips.Target == null))
			{
				Vector3 vector = camera.WorldToScreenPoint(uIBattleInfo_Com_UpgradeTips.Target.position);
				vector.y = (float)Screen.height - vector.y;
				uIBattleInfo_Com_UpgradeTips.xy = GlobalToLocal(vector);
			}
		}
	}

	public static UIBattleInfoPanel CreateInstance()
	{
		BindAll();
		return (UIBattleInfoPanel)UIPackage.CreateObject("BattleInfo", "BattleInfoPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfbqj63", typeof(UIBattleInfoPanel));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfcz3799", typeof(UIBattleInfo_Com_Asymmetrical));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfcz379a", typeof(UIBattleInfo_Com_AsymmetricalItem));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfcz379b", typeof(UIBattleInfo_Com_Asymmetrical_Progress));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfg1i48n", typeof(UIBattleInfo_Com_MapInfo));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfg1i48p", typeof(UIBattleInfo_Button_PVETask));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfg1i48s", typeof(UIBattleInfo_Com_PVETaskItem));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfg8sb83", typeof(UIBattleInfo_Button_Victory));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfiyw3c8", typeof(UIBattleInfo_Com_Watch_FollowSwitch));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfjcp5bb", typeof(UIBattleInfo_Com_UpgradeTips));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfjf4abl", typeof(UIBattleinfo_Com_Speedani));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfkqgj92", typeof(UIBattleInfo_Com_CampaignTask));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfmgj001", typeof(UIBattleInfo_Com_Clue));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfmgj007", typeof(UIBattleInfo_Button_Clue));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfmgj008", typeof(UIBattleInfo_Com_ClueItem));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfot0w91", typeof(UIBattleInfo_Button_ShowRelic));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfp4oi5t", typeof(UIBattleInfo_Com_LandTip));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfr1pj8l", typeof(UIBattleInfo_Button_PVEProgressItem));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfr1pj8m", typeof(UIBattleInfo_Com_PVEProgress));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfr4n9c7", typeof(UIBattleInfo_Com_PVEProgressList));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfshy231", typeof(UIBattleInfo_Com_MiniMap_Land));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfsxcpbf", typeof(UIBattleInfo_Com_Player));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfsxcpbg", typeof(UIBattleInfo_Com_PlayerContainer));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfsxcpbh", typeof(UIBattleInfo_Button_PlayerInfo));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfu5uzbs", typeof(UIBattleInfo_Com_LuckyStar));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfvi802u", typeof(UIBattleInfo_Com_Minimap));
		UIObjectFactory.SetPackageItemExtension("ui://fxejlqlfvi802w", typeof(UIBattleInfo_Com_MiniMap_Icon));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showCard = GetControllerAt(0);
		showBuff = GetControllerAt(1);
		GameMode = GetControllerAt(2);
		platform = GetControllerAt(3);
		graph_ShowTime = (GGraph)GetChildAt(0);
		com_PlayerAttrInfos = (GComponent)GetChildAt(1);
		com_AttrInfos = (GComponent)GetChildAt(2);
		com_UpgradeTips = (GComponent)GetChildAt(3);
		txt_Round = (GTextField)GetChildAt(4);
		btn_ESC = (GButton)GetChildAt(7);
		btn_VictoryCondition = (UIBattleInfo_Button_Victory)GetChildAt(8);
		com_LandTip = (UIBattleInfo_Com_LandTip)GetChildAt(9);
		loader_Skin = (GComponent)GetChildAt(12);
		btn_ShowRelic = (UIBattleInfo_Button_ShowRelic)GetChildAt(14);
		list_Buff = (GList)GetChildAt(15);
		loader_BuffIcon = (GLoader)GetChildAt(17);
		txt_BuffTitle = (GTextField)GetChildAt(18);
		txt_Buff = (GRichTextField)GetChildAt(19);
		txt_ATK = (GTextField)GetChildAt(22);
		txt_DEF = (GTextField)GetChildAt(24);
		com_MapInfo = (UIBattleInfo_Com_MapInfo)GetChildAt(26);
		com_GameModeComponent = (GComponent)GetChildAt(27);
		com_CampaignTask = (UIBattleInfo_Com_CampaignTask)GetChildAt(28);
		com_BattlePlayer = (UIBattleInfo_Com_Player)GetChildAt(29);
		txt_DicePoint = (GTextField)GetChildAt(30);
		btn_terms = (GButton)GetChildAt(31);
		com_Clue = (UIBattleInfo_Com_Clue)GetChildAt(32);
		btn_WatchFollowSwitch = (UIBattleInfo_Com_Watch_FollowSwitch)GetChildAt(33);
		showDicePoint = GetTransitionAt(0);
		CT_Cut_out = GetTransitionAt(1);
		CT_Cut_in = GetTransitionAt(2);
	}
}
