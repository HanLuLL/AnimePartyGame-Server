using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;
using party.model;

namespace UI;

public class UICom_AttrInfo : GComponent
{
	private long _PlayerId;

	private List<Buff> showBuffs;

	private List<PropertyData<int>> propertyBuffList = new List<PropertyData<int>>();

	private BattlePlayerData _PlayerData;

	private Vector2 Offset;

	public Controller type;

	public Controller ShowInfo;

	public Controller showMove;

	public GTextField txt_Name;

	public GTextField txt_Number;

	public GGraph graph_Line;

	public GTextField txt_HP;

	public GGraph graph_Guide;

	public GGraph graph_Sign;

	public GList list_Buff;

	public GTextField txt_ATK;

	public GTextField txt_DEF;

	public GTextField txt_Move;

	public GGraph graph_ShowBuff;

	public const string URL = "ui://1ov1i0v9rmeybs";

	public BattlePlayerData PlayerData
	{
		get
		{
			if (_PlayerData == null)
			{
				_PlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_PlayerId);
			}
			return _PlayerData;
		}
	}

	public void ShowAttrInfo(RoomPlayer player)
	{
		_PlayerId = player.Id;
		txt_Name.text = CharacterHandle.GetCharacterName(player.characterConfig.Id, player.characterConfig.CharacterType);
		Controller controller = type;
		controller.selectedIndex = player.characterConfig.MonsterType switch
		{
			MonsterType.Boss => 1, 
			MonsterType.Elite => 2, 
			_ => 0, 
		};
		txt_Number.text = $"{player.Hero.MonsterIndex}";
		txt_HP.x = txt_Name.width * 0.5f;
		UpdateHP(player.Property.HP.Value, player.Property.maxHP);
		UpdateATK(player.Property.ATK.Value);
		UpdateDEF(player.Property.DEF.Value);
		UpdateExtraMovePoint(player.Property.ExtraMovePoint);
		RefreshBuff();
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.AddListener(RefreshBuff);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo?.RegisterAttrInfo(this);
		graph_ShowBuff.onClick.Set(OpenBuffInfo);
	}

	private void OpenBuffInfo(EventContext context)
	{
		BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_PlayerId);
		if (playerDataById != null)
		{
			var (list, list2) = playerDataById.buffContainer.GetShowBuffs(playerDataById);
			if (list?.Count + list2?.Count != 0)
			{
				bool value = Offset.x <= 0.1f;
				SimpleSingletonProvider<UIManager>.inst.battlePlayerInfo.RefreshMonsterInfo(list, list2, this, value).Forget();
			}
		}
	}

	public override void Dispose()
	{
		_PlayerData = null;
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.RemoveListener(RefreshBuff);
		base.Dispose();
	}

	public void SwitchInfo(bool status)
	{
		if (status)
		{
			txt_HP.x = graph_Line.x;
			ShowInfo.selectedIndex = 1;
		}
		else
		{
			txt_HP.x = txt_Name.width * 0.5f;
			ShowInfo.selectedIndex = 0;
		}
	}

	public void UpdateHP(int hp, int maxHP)
	{
		txt_HP.text = $"{hp}/{maxHP}";
	}

	public void UpdateATK(int ATK)
	{
		txt_ATK.text = $"{ATK}";
	}

	public void UpdateDEF(int DEF)
	{
		txt_DEF.text = $"{DEF}";
	}

	public void UpdateExtraMovePoint(int extraMovePoint)
	{
		Controller controller = showMove;
		int selectedIndex = ((extraMovePoint > 0) ? 2 : ((extraMovePoint < 0) ? 1 : 0));
		controller.selectedIndex = selectedIndex;
		txt_Move.text = ((extraMovePoint > 0) ? $"+{extraMovePoint}" : $"{extraMovePoint}");
	}

	public Vector3 GetOwnerPos()
	{
		if (PlayerData?.Property == null || PlayerData.Property.HP.Value <= 0 || PlayerData.CharacterInst == null || PlayerData.CharacterInst.characterObject == null || PlayerData.CharacterInst.characterAnimator == null || PlayerData.CharacterInst.characterAnimator.IsHide())
		{
			return Vector3.zero;
		}
		return PlayerData.CharacterInst.characterObject.position;
	}

	public void UpdateGuideLine(Vector2 targetPos)
	{
		graph_Sign.xy = targetPos;
		if (Offset.x > 0.1f)
		{
			graph_Guide.x = graph_Line.x;
		}
		else
		{
			graph_Guide.x = graph_Line.x + graph_Line.width;
		}
		graph_Guide.width = Vector2.Distance(graph_Guide.xy, targetPos);
		Vector2 vector = targetPos - graph_Guide.xy;
		graph_Guide.rotation = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
	}

	public void UpdateOffset(Vector2 targetPos)
	{
		Character characterInst = PlayerData.CharacterInst;
		if (characterInst == null || characterInst.transform == null || characterInst.standLand?.transform == null || characterInst.canStep != 0)
		{
			Offset = Vector2.zero;
			targetPos.x -= base.width * 0.8f;
		}
		else
		{
			Vector3 localPosition = characterInst.standLand.transform.localPosition;
			Offset = characterInst.transform.localPosition - localPosition;
			if (Offset.x <= 0.1f)
			{
				targetPos.x -= base.width * 0.8f;
			}
			else
			{
				targetPos.x += base.width * 0.8f;
			}
		}
		base.xy = Vector2.Lerp(base.xy, targetPos, Time.deltaTime * 35f);
	}

	private void RefreshBuff()
	{
		if (PlayerData != null && PlayerData.Property != null)
		{
			list_Buff.itemRenderer = RendererBuff;
			(showBuffs, propertyBuffList) = PlayerData.buffContainer.GetShowBuffs(PlayerData, isRegister: true);
			list_Buff.numItems = showBuffs.Count + propertyBuffList.Count;
			list_Buff.ResizeToFit();
		}
	}

	private void RendererBuff(int index, GObject item)
	{
		if (!(item is UIButton_Buff uIButton_Buff))
		{
			return;
		}
		uIButton_Buff.showframe.selectedIndex = 1;
		if (propertyBuffList.Count > index)
		{
			uIButton_Buff.RefreshProperty(propertyBuffList[index], dynamic: true);
			return;
		}
		int num = index - propertyBuffList.Count;
		if (showBuffs.Count > num)
		{
			uIButton_Buff.RefreshBuff(showBuffs[num]);
		}
	}

	public static UICom_AttrInfo CreateInstance()
	{
		return (UICom_AttrInfo)UIPackage.CreateObject("Common_Internal", "Com_AttrInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		ShowInfo = GetControllerAt(1);
		showMove = GetControllerAt(2);
		txt_Name = (GTextField)GetChildAt(2);
		txt_Number = (GTextField)GetChildAt(3);
		graph_Line = (GGraph)GetChildAt(4);
		txt_HP = (GTextField)GetChildAt(5);
		graph_Guide = (GGraph)GetChildAt(6);
		graph_Sign = (GGraph)GetChildAt(7);
		list_Buff = (GList)GetChildAt(8);
		txt_ATK = (GTextField)GetChildAt(9);
		txt_DEF = (GTextField)GetChildAt(10);
		txt_Move = (GTextField)GetChildAt(11);
		graph_ShowBuff = (GGraph)GetChildAt(16);
	}
}
