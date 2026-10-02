using Core;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class UICom_PlayerInfo : GButton
{
	private string[] SlotName = new string[4] { "1st", "2nd", "3rd", "4th" };

	private BattlePlayerData _playerData;

	public Controller _vertigo;

	public Controller slot;

	public Controller actionNo;

	public Controller isOnline;

	public Controller GameMode;

	public Controller Camp;

	public GMovieClip ani;

	public UICom_playerInfo_Head com_Head;

	public GTextField txt_Slot;

	public GTextField txt_curLife;

	public GTextField txt_maxLife;

	public GTextField txt_Gold;

	public GTextField txt_Gift;

	public GTextField txt_PlayerName;

	public GTextField txt_Team;

	public GList list_Level;

	public Transition actionShow;

	public Transition actionFinish;

	public const string URL = "ui://1ov1i0v9imo37u";

	public void RefreshData(BattlePlayerData playerdata)
	{
		_playerData = playerdata;
		RefreshSlot();
		RefreshPlayerName();
		list_Level.itemRenderer = RendererLevelStar;
		RefreshLevel();
		RefreshGold();
		RefreshLife();
		RefreshScore();
		SetOnlineStatus();
	}

	private void RefreshSlot()
	{
		int num = _playerData.player.Slot;
		slot.selectedIndex = num;
		actionNo.selectedIndex = _playerData.player.ChangeSlot;
		RoomInfo roomInfo = SimpleSingletonProvider<GameLogicManager>.inst?.room?.curRoomInfo;
		if (roomInfo != null && roomInfo.IsLuckyStarBattle())
		{
			txt_Slot.visible = false;
			if (_playerData.player.TeamId == BattleConfig.LuckyStarRedTeamId)
			{
				GTextField gTextField = txt_curLife;
				Color color = (txt_maxLife.color = BattleConfig.LuckyStarRedColor);
				gTextField.color = color;
			}
			else if (_playerData.player.TeamId == BattleConfig.LuckyStarGreenTeamId)
			{
				GTextField gTextField2 = txt_curLife;
				Color color = (txt_maxLife.color = BattleConfig.LuckyStarGreenColor);
				gTextField2.color = color;
			}
		}
		else if (roomInfo != null && roomInfo.IsMutatorPve())
		{
			txt_Slot.visible = false;
		}
		else
		{
			txt_Slot.text = SlotName.GetSafeByIndex(num);
			GTextField gTextField3 = txt_curLife;
			GTextField gTextField4 = txt_maxLife;
			Color color2 = (txt_Slot.color = GameConfig.slotColor[num]);
			Color color = (gTextField4.color = color2);
			gTextField3.color = color;
			txt_Slot.visible = true;
		}
	}

	public void RefreshPlayerName()
	{
		if (_playerData == null)
		{
			txt_PlayerName.text = "";
		}
		else
		{
			txt_PlayerName.text = _playerData.player.GetNick().GetSubString(14, out var _);
		}
	}

	public void PlayActionShow()
	{
		if (!actionShow.playing)
		{
			actionFinish.Stop();
			actionShow.Play();
			ani.playing = true;
		}
	}

	public void StopActionShow()
	{
		actionShow.Stop();
		actionFinish.Play();
		ani.playing = false;
	}

	public void RefreshLevel()
	{
		list_Level.numItems = StaticConfigure.Upgrade.Datas[0].UpgradeDataConfigureItems.Count;
	}

	private void RendererLevelStar(int index, GObject item)
	{
		if (item is UICom_PlayerLevel uICom_PlayerLevel)
		{
			uICom_PlayerLevel.slot.selectedIndex = _playerData.player.Slot;
			uICom_PlayerLevel.avtiveLevel.selectedIndex = ((_playerData.Property.level.Value > index) ? 1 : 0);
		}
	}

	public void RefreshGold()
	{
		txt_Gold.text = _playerData.Property.gold.Value.ToString();
	}

	public void RefreshLife()
	{
		txt_curLife.text = _playerData.Property.HP.Value.ToString();
		txt_maxLife.text = $"/{_playerData.Property.maxHP}";
		com_Head.loader_Icon.url = _playerData.player.standingPainting.ProfilePhoto;
		com_Head.grayed = _playerData.Property.HP.Value == 0;
		_vertigo.selectedIndex = ((_playerData.Property.HP.Value == 0) ? 1 : 0);
	}

	public void SetOnlineStatus()
	{
		isOnline.selectedIndex = ((!_playerData.Property.Online.Value) ? 1 : 0);
	}

	public void RefreshScore()
	{
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo != null)
		{
			if (curRoomInfo.IsAsymmetricalBattle())
			{
				GameMode.selectedIndex = 1;
				Camp.selectedIndex = ((_playerData.player.TeamId == BattleConfig.AsymmetricalDefenderTeamId) ? 1 : 0);
			}
			else if (curRoomInfo.IsLuckyStarBattle())
			{
				GameMode.selectedIndex = 2;
				Camp.selectedIndex = 0;
			}
			else
			{
				GameMode.selectedIndex = 0;
				Camp.selectedIndex = 0;
			}
		}
		else
		{
			GameMode.selectedIndex = 0;
			Camp.selectedIndex = 0;
		}
		txt_Gift.text = _playerData.Property.Score.Value.ToString();
	}

	public static UICom_PlayerInfo CreateInstance()
	{
		return (UICom_PlayerInfo)UIPackage.CreateObject("Common_Internal", "Com_PlayerInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		_vertigo = GetControllerAt(0);
		slot = GetControllerAt(1);
		actionNo = GetControllerAt(2);
		isOnline = GetControllerAt(3);
		GameMode = GetControllerAt(4);
		Camp = GetControllerAt(5);
		ani = (GMovieClip)GetChildAt(1);
		com_Head = (UICom_playerInfo_Head)GetChildAt(4);
		txt_Slot = (GTextField)GetChildAt(5);
		txt_curLife = (GTextField)GetChildAt(6);
		txt_maxLife = (GTextField)GetChildAt(7);
		txt_Gold = (GTextField)GetChildAt(9);
		txt_Gift = (GTextField)GetChildAt(11);
		txt_PlayerName = (GTextField)GetChildAt(14);
		txt_Team = (GTextField)GetChildAt(15);
		list_Level = (GList)GetChildAt(19);
		actionShow = GetTransitionAt(0);
		actionFinish = GetTransitionAt(1);
	}
}
