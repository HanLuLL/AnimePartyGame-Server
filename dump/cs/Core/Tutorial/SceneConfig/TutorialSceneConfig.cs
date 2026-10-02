using System.Collections.Generic;
using Core.Tutorial.Tools;
using GameLogic;
using Tools;
using UI;
using party.model;

namespace Core.Tutorial.SceneConfig;

public class TutorialSceneConfig
{
	public Room info;

	protected int _MapId;

	protected int _roomId;

	public UIPanelType SourceSystemType;

	protected FashionPlan fashion => new FashionPlan
	{
		Plan = 1,
		Fashion = { (IDictionary<int, int>)SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap }
	};

	protected TutorialSceneConfig(int selectHeroId)
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel != null)
		{
			SourceSystemType = SimpleSingletonProvider<UIManager>.inst.currentPanel.config.PanelType;
		}
	}

	protected Player BuildServerPlayer(int roleId, int skinItemId)
	{
		int num = UIDGenerator.NextUID();
		Player player = new Player();
		player.Id = num;
		player.Nick = CharacterHandle.GetCharacterNickName(roleId);
		player.WaitOffLine = false;
		player.OffLine = false;
		player.RoomId = _roomId;
		player.Slot = 0;
		player.ChangeSlot = 0;
		player.Progress = 0;
		player.Level = 1;
		player.FashionPlan.Add(fashion);
		player.Hero = BuildHeroData(roleId, skinItemId);
		player.Hero.PlayerId = num;
		return player;
	}

	protected Hero BuildHeroData(int heroId, int skinItemId)
	{
		return new Hero
		{
			TeamId = 1,
			HeroId = heroId,
			StandingPainting = skinItemId,
			MaxHp = CharacterHandle.GetCharacterBlood(heroId),
			UserCardNum = 1,
			UseCardMaxNum = 1,
			Gold = CharacterHandle.TryGetCharacterGold(heroId),
			Affirm = true,
			CanCounter = CharacterHandle.TryGetCharacterCanCounter(heroId)
		};
	}

	protected virtual Player BuildSelfServerPlayer(int heroId, int skinItemId)
	{
		Player playerInfo = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();
		Player player = BuildServerPlayer(heroId, skinItemId);
		player.Id = playerInfo.Id;
		player.Nick = playerInfo.Nick;
		player.FashionPlan.Add(fashion);
		player.Hero.PlayerId = playerInfo.Id;
		return player;
	}

	protected Room BuildRoom()
	{
		return new Room
		{
			Id = _roomId,
			Name = "Tutorial",
			Pwd = "",
			MaxTime = 7200,
			MapId = _MapId,
			MapDataId = 1,
			MapType = 10,
			State = Room.Types.State.Running,
			UpgradePlan = 5,
			Round = 0,
			SpeedType = 2,
			Difficulty = 0,
			MapDifficultyId = _MapId
		};
	}

	protected HeroBarBox GetHeroBarBox(List<Player> players)
	{
		HeroBarBox heroBarBox = new HeroBarBox();
		for (int i = 0; i < players.Count; i++)
		{
			heroBarBox.Box.Add(players[i].Hero.PlayerId, new HeroBar
			{
				PlayerId = players[i].Hero.PlayerId,
				HeroId = players[i].Hero.HeroId,
				Affirm = true
			});
		}
		return heroBarBox;
	}
}
