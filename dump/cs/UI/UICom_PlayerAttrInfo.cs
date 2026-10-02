using Core.Unit;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UICom_PlayerAttrInfo : GComponent
{
	private long _PlayerId;

	private BattlePlayerData _PlayerData;

	private UICom_NGOCounter com_NGOCounter;

	private UICom_ModifyCounter com_ModifyCounter;

	private UICom_UniqueNum com_UniqueNumCounter;

	private UICom_CrimeNum com_CrimeNumCounter;

	public GList list_AttrChange;

	public GLoader loader_State;

	public const string URL = "ui://1ov1i0v9qle6by";

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
		base.touchable = false;
		_PlayerId = player.Id;
		SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo?.RegisterPlayerAttrInfo(this);
		if (PlayerData?.CharacterInst != null)
		{
			PlayerData.CharacterInst.signal.statusIcon.AddListener(ShowStateIcon);
			PlayerData.CharacterInst.signal.attrChange.AddListener(ShowPlayerAttrChange);
			PlayerData.Property.ModifyNum.property.AddListener(TryUpdateModifyCounter);
			PlayerData.Property.UniqueNum.property.AddListener(TryUpdateUniqueNum);
			PlayerData.Property.CrimeNum.property.AddListener(TryUpdateCrimeNum);
			PlayerData.CharacterInst.scaleChange.AddListener(OnCharacterScaleChange);
		}
	}

	public override void Dispose()
	{
		if (PlayerData?.CharacterInst != null)
		{
			PlayerData.CharacterInst.signal.statusIcon.RemoveListener(ShowStateIcon);
			PlayerData.CharacterInst.signal.attrChange.RemoveListener(ShowPlayerAttrChange);
			PlayerData.Property.ModifyNum.property.RemoveListener(TryUpdateModifyCounter);
			PlayerData.Property.UniqueNum.property.RemoveListener(TryUpdateUniqueNum);
			PlayerData.Property.CrimeNum.property.RemoveListener(TryUpdateCrimeNum);
			PlayerData.CharacterInst.scaleChange.RemoveListener(OnCharacterScaleChange);
		}
		_PlayerData = null;
		base.Dispose();
		com_NGOCounter = null;
		com_ModifyCounter = null;
	}

	public Vector3 GetOwnerPos()
	{
		if (PlayerData?.Property == null || PlayerData.CharacterInst == null || PlayerData.CharacterInst.characterObject == null || PlayerData.CharacterInst.characterAnimator == null || PlayerData.CharacterInst.characterAnimator.IsHide())
		{
			return Vector3.zero;
		}
		return PlayerData.CharacterInst.characterObject.position;
	}

	private void ShowStateIcon(bool state, string url = null)
	{
		loader_State.url = url;
		loader_State.visible = state;
	}

	private void ShowPlayerAttrChange((int, int, int, int) attr, string PN)
	{
		GObject item = list_AttrChange.AddItemFromPool();
		if (item is UICom_AttrTip uICom_AttrTip)
		{
			uICom_AttrTip.visible = false;
			uICom_AttrTip.InitAttrChange(attr.Item1, attr.Item2, attr.Item3, attr.Item4, PN);
			uICom_AttrTip.ShowAttrChange(delegate
			{
				list_AttrChange.RemoveChildToPool(item);
			});
		}
	}

	public void TryUpdateNGOCounter(int count)
	{
		if (com_NGOCounter == null)
		{
			com_NGOCounter = UICom_NGOCounter.CreateInstance();
			AddChild(com_NGOCounter);
			com_NGOCounter.SetXY(200f, 300f);
		}
		com_NGOCounter.txt_Counter.text = count.ToString();
		com_NGOCounter.group_Counter.visible = true;
	}

	private void TryUpdateModifyCounter(int count)
	{
		if (com_ModifyCounter == null)
		{
			com_ModifyCounter = UICom_ModifyCounter.CreateInstance();
			AddChild(com_ModifyCounter);
			RefreshPos(com_ModifyCounter, base.width * 0.5f, com_ModifyCounter.height * 1.3f);
		}
		com_ModifyCounter.Refresh(count);
	}

	private void TryUpdateUniqueNum(int count)
	{
		if (com_UniqueNumCounter == null)
		{
			if (count == 0)
			{
				return;
			}
			com_UniqueNumCounter = UICom_UniqueNum.CreateInstance();
			AddChild(com_UniqueNumCounter);
			RefreshPos(com_UniqueNumCounter, base.width * 0.5f, com_UniqueNumCounter.height * 1.3f);
		}
		com_UniqueNumCounter.Refresh(count);
	}

	private void TryUpdateCrimeNum(int count)
	{
		if (com_CrimeNumCounter == null)
		{
			if (count == 0)
			{
				return;
			}
			com_CrimeNumCounter = UICom_CrimeNum.CreateInstance();
			AddChild(com_CrimeNumCounter);
			RefreshPos(com_CrimeNumCounter, base.width * 0.5f, com_CrimeNumCounter.height * 1.3f);
		}
		com_CrimeNumCounter.Refresh(count);
	}

	private void OnCharacterScaleChange(float newScale)
	{
		if (com_ModifyCounter != null)
		{
			RefreshPos(com_ModifyCounter, base.width * 0.5f, com_ModifyCounter.height * 1.3f);
		}
		if (com_UniqueNumCounter != null)
		{
			RefreshPos(com_UniqueNumCounter, base.width * 0.5f, com_UniqueNumCounter.height * 1.3f);
		}
	}

	private void RefreshPos(GComponent component, float basePos_x, float basePos_y)
	{
		if (component != null && (object)PlayerData?.CharacterInst != null)
		{
			float num = basePos_y * (1f - Mathf.Max(1f, PlayerData.CharacterInst.currentScale));
			component.SetXY(basePos_x, basePos_y + num);
		}
	}

	public void AddBossLabel()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10 && SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager_Tutorial1002 mapGimmickManager_Tutorial && _PlayerId == mapGimmickManager_Tutorial.BossData.player.Id)
		{
			UICom_BossTips uICom_BossTips = UICom_BossTips.CreateInstance();
			uICom_BossTips.txt_Tutorial.text = 3.GetLocal(UIStringType.Tutorial);
			AddChildAt(uICom_BossTips, 0);
			uICom_BossTips.y = 0f - uICom_BossTips.height;
			uICom_BossTips.x = (base.width - uICom_BossTips.width) * 0.5f;
		}
	}

	public static UICom_PlayerAttrInfo CreateInstance()
	{
		return (UICom_PlayerAttrInfo)UIPackage.CreateObject("Common_Internal", "Com_PlayerAttrInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_AttrChange = (GList)GetChildAt(0);
		loader_State = (GLoader)GetChildAt(1);
	}
}
