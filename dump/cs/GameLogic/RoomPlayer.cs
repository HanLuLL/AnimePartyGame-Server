using System.Collections.Generic;
using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace GameLogic;

public class RoomPlayer
{
	public Player serverPlayer;

	private string _nick;

	private readonly int _coverNameId;

	private int _PVEHeroLV;

	private int _changeSlot;

	private RepeatedField<FashionPlan> _fashionPlan;

	private SkinStandingPaintingConfigureItem _StandingPainting;

	public CharacterHandle characterConfig;

	public int NodeId;

	public readonly List<int> FrontIds = new List<int>();

	public int BackNodeId;

	public int Progress;

	public bool OffLine;

	private bool _roomReady;

	public CharacterType characterType;

	public BattleProperty Property;

	public CardContainer cardContainer;

	public BuffContainer buffContainer;

	public Dictionary<int, bool> SelectedRelics;

	public List<int> ShowSelectedRelics = new List<int>();

	private readonly List<LuckyStarMissionData> _luckyStarMissions = new List<LuckyStarMissionData>(4);

	public long Id => serverPlayer.Id;

	public long TeamId
	{
		get
		{
			if (serverPlayer?.Hero != null)
			{
				return serverPlayer.Hero.TeamId;
			}
			return 0L;
		}
	}

	public bool IsBot => serverPlayer.IsBot;

	public BattlePassGearType gearType
	{
		get
		{
			if (serverPlayer.BattlePass != null)
			{
				return (BattlePassGearType)serverPlayer.BattlePass.Gear;
			}
			return BattlePassGearType.NONE;
		}
	}

	public int Level => serverPlayer.Level;

	public int coverNameId => _coverNameId;

	public int PVEHeroLV => _PVEHeroLV;

	public int Slot => serverPlayer.Slot;

	public int ChangeSlot => _changeSlot;

	public Hero Hero => serverPlayer.Hero;

	public RepeatedField<FashionPlan> FashionPlan => _fashionPlan;

	public SkinStandingPaintingConfigureItem standingPainting => _StandingPainting;

	public bool RoomReady
	{
		get
		{
			return _roomReady;
		}
		set
		{
			if (!value.Equals(_roomReady))
			{
				_roomReady = value;
			}
		}
	}

	public List<LuckyStarMissionData> LuckyStarMissions => _luckyStarMissions;

	public string GetNick(bool showRemark = false)
	{
		if (GameSettings.IncoverMode && _coverNameId > 0 && !SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(Id))
		{
			if (!StaticConfigure.Player.CoverNameDict.TryGetValue(_coverNameId, out var value))
			{
				return "";
			}
			return value.CoverNameID.GetLocal(UIStringType.Player);
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.friend.GetDisplayNick(Id, _nick, showRemark);
	}

	public RoomPlayer(Player player, int coverNameId = 0)
	{
		_coverNameId = coverNameId;
		UpdateFromServer(player);
		UpdateProgress(player.Progress);
	}

	public void UpdateFromServer(Player player)
	{
		serverPlayer = player;
		UpdateNick();
		OffLine = player.OffLine;
		_changeSlot = player.ChangeSlot;
		_fashionPlan = player.FashionPlan;
		_roomReady = player.RoomReady;
		if (Hero != null)
		{
			UpdateHeroPlace(Hero.NodeId, Hero.FrontNodeIds, Hero.BackNodeId);
		}
		UpdateBattleData();
	}

	public void UpdateNick()
	{
		int result;
		if (!serverPlayer.IsBot)
		{
			_nick = serverPlayer.Nick;
		}
		else if (int.TryParse(serverPlayer.Nick, out result) && StaticConfigure.STRBot.LocalDict.ContainsKey(result))
		{
			_nick = result.GetLocal(UIStringType.Bot);
		}
		else
		{
			_nick = serverPlayer.Nick;
		}
	}

	public void UpdateProgress(int _progress)
	{
		Progress = _progress;
	}

	public void UpdateChangeSlot(int _ChangeSlot)
	{
		_changeSlot = _ChangeSlot;
	}

	public void UpdateSlot(int slot)
	{
		serverPlayer.Slot = slot;
	}

	public void UpdateRoomSlot(int slot)
	{
		UpdateSlot(slot);
		UpdateChangeSlot(slot);
	}

	public void UpdateRoomReady(bool isReady)
	{
		_roomReady = isReady;
	}

	public void UpdatePVEHeroLV(int Lv)
	{
		_PVEHeroLV = Lv;
	}

	public void UpdateStandingPainting(int heroID, int UseAdorn)
	{
		if (_StandingPainting != null && _StandingPainting.ItemID == UseAdorn)
		{
			return;
		}
		_StandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(heroID, UseAdorn, 0);
		if (!HackerConfig.IsValid() || serverPlayer == null || CharacterHandle.GetCharacterType(heroID) != CharacterType.Hero)
		{
			return;
		}
		int safeByIndex = GMConfig.HeroStandingPainting.GetSafeByIndex(serverPlayer.Slot);
		if (safeByIndex != 0)
		{
			SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(safeByIndex);
			if (configStandingPainting != null)
			{
				_StandingPainting = configStandingPainting;
			}
		}
	}

	public void UpdateHeroPlace(int heroNodeId, RepeatedField<int> frontNodeIds, int backNodeId)
	{
		NodeId = heroNodeId;
		UpdateFrontIds(frontNodeIds);
		BackNodeId = backNodeId;
	}

	public void UpdateFrontIds(RepeatedField<int> frontNodeIds)
	{
		FrontIds.Clear();
		FrontIds.AddRange(frontNodeIds);
	}

	private MapField<int, int> GetFashionPlan()
	{
		if (FashionPlan == null || FashionPlan.Count == 0)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap;
		}
		return FashionPlan[0].Fashion;
	}

	public string HeadURL()
	{
		if (!GetFashionPlan().TryGetValue(1, out var value))
		{
			Debug.LogError($"当前玩家{Id}数据中，通过key值{1}无法取出击杀特效Id");
			return "";
		}
		return value.GetItemInfoConfigure().SubMeterID.GetFashionAccountHeadShot();
	}

	public (string, bool) AccountBackgroundURL()
	{
		if (!GetFashionPlan().TryGetValue(2, out var value))
		{
			Debug.LogError($"当前玩家{Id}数据中，通过key值{2}无法取出击杀特效Id");
			return ("", false);
		}
		return value.GetItemInfoConfigure().SubMeterID.GetFashionAccountBackgroundConfigure().GetPlayerLabel();
	}

	public FashionEffectConfigure OverKillResultConfig()
	{
		if (!GetFashionPlan().TryGetValue(5, out var value))
		{
			Debug.LogError($"当前玩家{Id}数据中，通过key值{5}无法取出击杀特效Id");
			return null;
		}
		return value.GetItemInfoConfigure().SubMeterID.GetFashionFashionEffectConfigure();
	}

	public FashionCardBackConfigure CardBackConfig()
	{
		if (!GetFashionPlan().TryGetValue(3, out var value))
		{
			Debug.LogError($"当前玩家{Id}数据中，通过key值{3}无法取出击杀特效Id");
			return null;
		}
		return value.GetItemInfoConfigure().SubMeterID.GetFashionCardBackConfigure();
	}

	public FashionDiceConfigure PlayerDiceConfig()
	{
		if (!GetFashionPlan().TryGetValue(4, out var value))
		{
			Debug.LogError($"当前玩家{Id}数据中，通过key值{4}无法取出击杀特效Id");
			return null;
		}
		return value.GetItemInfoConfigure().SubMeterID.GetFashionDiceConfigure();
	}

	private void UpdateBattleData()
	{
		if (serverPlayer?.Hero != null)
		{
			characterType = CharacterHandle.GetCharacterType(serverPlayer.Hero.HeroId);
			characterConfig = new CharacterHandle(serverPlayer.Hero.HeroId);
			UpdateStandingPainting(Hero.HeroId, Hero.StandingPainting);
			UpdatePVEHeroLV(Hero.PveHeroStrengthen?.Level ?? 0);
			cardContainer = new CardContainer(serverPlayer.Id, serverPlayer.Hero.Cards);
			buffContainer = new BuffContainer(serverPlayer.Hero.Buffs);
			Property = new BattleProperty(characterType, serverPlayer);
			UpdateSelectedRelic(serverPlayer.Hero.SelectRelics);
		}
	}

	public int GetTalentId()
	{
		if (serverPlayer == null)
		{
			return 0;
		}
		RepeatedField<int> repeatedField = Hero?.PveHeroStrengthen?.Talent;
		if (repeatedField == null || repeatedField.Count == 0)
		{
			return 0;
		}
		return repeatedField[0];
	}

	public int GetBattleActiveSkillId()
	{
		int talentId = GetTalentId();
		return CharacterHandle.GetBattleActiveSkillId(Hero.HeroId, characterType, talentId);
	}

	public RepeatedField<int> GetBattlePassiveSkillId()
	{
		int talentId = GetTalentId();
		return CharacterHandle.GetBattlePassiveSkills(Hero.HeroId, characterType, talentId);
	}

	public void InitLuckyStarMissions(MapField<int, LuckyStarMissionInfo> missionInfo)
	{
		_luckyStarMissions.Clear();
		if (missionInfo == null || missionInfo.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<int, LuckyStarMissionInfo> item in missionInfo)
		{
			if (item.Value.LuckyStarMission.TryGetValue(Id, out var value))
			{
				UpdateLuckyStarMission(value).Forget();
			}
		}
		_luckyStarMissions.Sort(delegate(LuckyStarMissionData mission1, LuckyStarMissionData mission2)
		{
			if (mission1.Config == null || mission2.Config == null)
			{
				return 0;
			}
			int luckyStarMissionType = (int)mission1.Config.LuckyStarMissionType;
			int luckyStarMissionType2 = (int)mission2.Config.LuckyStarMissionType;
			return (luckyStarMissionType == luckyStarMissionType2) ? mission1.Config.Id.CompareTo(mission2.Config.Id) : luckyStarMissionType.CompareTo(luckyStarMissionType2);
		});
	}

	public async UniTask UpdateLuckyStarMission(LuckyStarMission mission)
	{
		if (mission == null)
		{
			Debug.LogError("服务器下发的幸运星任务数据为空，不可用");
			return;
		}
		LuckyStarMissionData luckyStarMissionData = _luckyStarMissions.Find((LuckyStarMissionData x) => x.MissionId == mission.DefId);
		if (luckyStarMissionData == null)
		{
			luckyStarMissionData = new LuckyStarMissionData(Id, mission);
			_luckyStarMissions.Add(luckyStarMissionData);
		}
		else
		{
			await luckyStarMissionData.UpdateMission(Id, mission);
		}
	}

	public int GetMoveEffectIdBySkin()
	{
		if (standingPainting == null)
		{
			return 0;
		}
		return standingPainting.MoveVfxId;
	}

	public void UpdateSelectedRelic(MapField<int, bool> selectedRelics)
	{
		if (SelectedRelics == null)
		{
			SelectedRelics = new Dictionary<int, bool>(selectedRelics);
			return;
		}
		SelectedRelics.Clear();
		foreach (int key in selectedRelics.Keys)
		{
			SelectedRelics[key] = selectedRelics[key];
		}
		UpdateShowSelectedRelic();
	}

	public void TryUpdateSelectedRelic(int relicId, bool isSelected)
	{
		if (SelectedRelics == null)
		{
			SelectedRelics = new Dictionary<int, bool>();
		}
		SelectedRelics[relicId] = isSelected;
		UpdateShowSelectedRelic();
	}

	private void UpdateShowSelectedRelic()
	{
		ShowSelectedRelics.Clear();
		if (SelectedRelics == null)
		{
			return;
		}
		foreach (int key in SelectedRelics.Keys)
		{
			if (SelectedRelics[key])
			{
				ShowSelectedRelics.Add(key);
			}
		}
	}
}
