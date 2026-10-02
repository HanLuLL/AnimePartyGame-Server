using Core.Camera;
using Core.Mark;
using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UIBattleInfo_Button_PlayerInfo : GButton, IMarkTarget
{
	public BattlePlayerData PlayerData;

	private UICom_ModifyCounter com_ModifyCounter;

	private UICom_UniqueNum com_UniqueNumCounter;

	private UICom_CrimeNum com_CrimeNumCounter;

	private UICom_SelectTips _com_SelectTips;

	private Vector2 _onclickPos = Vector2.zero;

	public Controller selectPlayer;

	public GImage image_Tip;

	public GImage image_Hover;

	public GButton com_player;

	public GGraph btn_Head;

	public GGraph btn_Label;

	public Transition t0;

	public const string URL = "ui://fxejlqlfsxcpbh";

	bool IMarkTarget.HoverWait => false;

	public void Init()
	{
		if (PlayerData == null)
		{
			base.visible = false;
			return;
		}
		base.visible = true;
		((UICom_PlayerInfo)com_player).RefreshData(PlayerData);
	}

	public void AddListener()
	{
		if (PlayerData != null)
		{
			PlayerData.Property.level.AddListener(UpdateLevel);
			PlayerData.Property.gold.AddListener(UpdateGold);
			PlayerData.Property.HP.AddListener(UpdateLife);
			PlayerData.Property.Online.AddListener(UpdateOnlineStatus);
			PlayerData.Property.Score.AddListener(UpdateScore);
			PlayerData.Property.ModifyNum.property.AddListener(TryUpdateModifyCounter);
			PlayerData.Property.UniqueNum.property.AddListener(TryUpdateUniqueNum);
			PlayerData.Property.CrimeNum.property.AddListener(TryUpdateCrimeNum);
			btn_Head.onClick.Add(SwitchCamera);
			btn_Label.onClick.Add(ShowBattlePlayerInfo);
		}
	}

	public void RemoveListener()
	{
		if (PlayerData != null)
		{
			PlayerData.Property.level.RemoveListener(UpdateLevel);
			PlayerData.Property.gold.RemoveListener(UpdateGold);
			PlayerData.Property.HP.RemoveListener(UpdateLife);
			PlayerData.Property.Online.RemoveListener(UpdateOnlineStatus);
			PlayerData.Property.Score.RemoveListener(UpdateScore);
			PlayerData.Property.ModifyNum.property.RemoveListener(TryUpdateModifyCounter);
			PlayerData.Property.UniqueNum.property.RemoveListener(TryUpdateUniqueNum);
			PlayerData.Property.CrimeNum.property.RemoveListener(TryUpdateCrimeNum);
			btn_Head.onClick.Remove(SwitchCamera);
			btn_Label.onClick.Remove(ShowBattlePlayerInfo);
		}
	}

	public override void Dispose()
	{
		RemoveListener();
		com_ModifyCounter = null;
		base.Dispose();
	}

	public void UpdateName()
	{
		((UICom_PlayerInfo)com_player).RefreshPlayerName();
	}

	private void UpdateLevel(int obj)
	{
		((UICom_PlayerInfo)com_player).RefreshLevel();
	}

	private void UpdateGold(int obj)
	{
		((UICom_PlayerInfo)com_player).RefreshGold();
	}

	private void UpdateLife(int obj)
	{
		((UICom_PlayerInfo)com_player).RefreshLife();
	}

	private void UpdateScore(int obj)
	{
		((UICom_PlayerInfo)com_player).RefreshScore();
	}

	private void UpdateOnlineStatus(bool status)
	{
		((UICom_PlayerInfo)com_player).SetOnlineStatus();
	}

	public void PlayAction(long playerId)
	{
		if (PlayerData == null)
		{
			return;
		}
		if (playerId == PlayerData.player.Id)
		{
			BattlePlayerData playerDataById = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
			if (playerDataById?.CharacterInst != null)
			{
				playerDataById.CharacterInst.SwitchCamera().Forget();
			}
			((UICom_PlayerInfo)com_player).PlayActionShow();
		}
		else
		{
			((UICom_PlayerInfo)com_player).StopActionShow();
		}
	}

	private async void ShowBattlePlayerInfo()
	{
		if (MarkInputConsume.IsConsumed())
		{
			return;
		}
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if ((roomInfo == null || roomInfo.MapType != 10) && PlayerData != null)
		{
			PlayerActionEnum playerAction = SimpleSingletonProvider<GameLogicManager>.inst.action.playerAction;
			if (playerAction != PlayerActionEnum.CHOOSETARGET_CARD && playerAction != PlayerActionEnum.CHOOSETARGET_SKILL)
			{
				btn_Label.onTouchBegin.Retain();
				await SimpleSingletonProvider<UIManager>.inst.battlePlayerInfo.TryShowHeroInfo(PlayerData);
				btn_Label.onTouchBegin.Release();
			}
		}
	}

	private void SwitchCamera()
	{
		if (!MarkInputConsume.IsConsumed() && PlayerData != null)
		{
			btn_Head.onTouchBegin.Retain();
			SimpleSingletonProvider<CameraManager>.inst.MoveCameraToPlayer(PlayerData.CharacterInst);
			btn_Head.onTouchBegin.Release();
		}
	}

	private void TryUpdateModifyCounter(int count)
	{
		if (com_player is UICom_PlayerInfo uICom_PlayerInfo)
		{
			if (com_ModifyCounter == null)
			{
				com_ModifyCounter = UICom_ModifyCounter.CreateInstance();
				com_player.AddChild(com_ModifyCounter);
				com_ModifyCounter.SetScale(0.8f, 0.8f);
				com_ModifyCounter.SetXY(uICom_PlayerInfo.txt_Slot.x + uICom_PlayerInfo.txt_Slot.width * 0.5f, uICom_PlayerInfo.txt_Slot.y + uICom_PlayerInfo.txt_Slot.height * 0.5f);
			}
			com_ModifyCounter.Refresh(count);
		}
	}

	private void TryUpdateUniqueNum(int count)
	{
		if (!(com_player is UICom_PlayerInfo uICom_PlayerInfo))
		{
			return;
		}
		if (com_UniqueNumCounter == null)
		{
			if (count == 0)
			{
				return;
			}
			com_UniqueNumCounter = UICom_UniqueNum.CreateInstance();
			com_player.AddChild(com_UniqueNumCounter);
			com_UniqueNumCounter.SetScale(0.8f, 0.8f);
			com_UniqueNumCounter.SetXY(uICom_PlayerInfo.txt_Slot.x + uICom_PlayerInfo.txt_Slot.width * 0.5f, uICom_PlayerInfo.txt_Slot.y + uICom_PlayerInfo.txt_Slot.height * 0.5f);
		}
		com_UniqueNumCounter.Refresh(count);
	}

	private void TryUpdateCrimeNum(int count)
	{
		if (!(com_player is UICom_PlayerInfo uICom_PlayerInfo))
		{
			return;
		}
		if (com_CrimeNumCounter == null)
		{
			if (count == 0)
			{
				return;
			}
			com_CrimeNumCounter = UICom_CrimeNum.CreateInstance();
			com_player.AddChild(com_CrimeNumCounter);
			com_CrimeNumCounter.SetScale(0.8f, 0.8f);
			com_CrimeNumCounter.SetXY(uICom_PlayerInfo.txt_Slot.x + uICom_PlayerInfo.txt_Slot.width * 0.5f, uICom_PlayerInfo.txt_Slot.y + uICom_PlayerInfo.txt_Slot.height * 0.5f);
		}
		com_CrimeNumCounter.Refresh(count);
	}

	public void ShowSelectTips()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10)
		{
			if (_com_SelectTips == null)
			{
				_com_SelectTips = UICom_SelectTips.CreateInstance();
			}
			base.parent.AddChild(_com_SelectTips);
			_com_SelectTips.SetXY(base.x + base.width, base.y + base.height * 0.7f);
			_com_SelectTips.txt_Tutorial.text = 8.GetLocal(UIStringType.Tutorial);
			_com_SelectTips.visible = true;
		}
	}

	public void CloseSelectTips()
	{
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.MapType == 10 && _com_SelectTips != null)
		{
			_com_SelectTips.visible = false;
		}
	}

	public void OnMarkTipShow()
	{
		image_Tip.visible = true;
	}

	public void OnMarkTipHide()
	{
		image_Tip.visible = false;
	}

	public void OnMarkHoverEnter()
	{
		image_Hover.visible = true;
	}

	public void OnMarkHoverExit()
	{
		image_Hover.visible = false;
	}

	public void OnMarkSelected()
	{
		_onclickPos = Input.mousePosition;
		MarkInputConsume.Consume();
		OnMarkTipHide();
		image_Hover.visible = false;
		if (PlayerData != null && PlayerData.player != null)
		{
			SimpleSingletonProvider<UIManager>.inst.expression.ShowPlayerMenu(this, PlayerData.player.Id);
		}
	}

	public void TriggerHoverConfirmed()
	{
	}

	public Vector2 GetPosition()
	{
		return _onclickPos;
	}

	public static UIBattleInfo_Button_PlayerInfo CreateInstance()
	{
		return (UIBattleInfo_Button_PlayerInfo)UIPackage.CreateObject("BattleInfo", "BattleInfo_Button_PlayerInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		selectPlayer = GetControllerAt(1);
		image_Tip = (GImage)GetChildAt(0);
		image_Hover = (GImage)GetChildAt(1);
		com_player = (GButton)GetChildAt(2);
		btn_Head = (GGraph)GetChildAt(4);
		btn_Label = (GGraph)GetChildAt(5);
		t0 = GetTransitionAt(0);
	}
}
