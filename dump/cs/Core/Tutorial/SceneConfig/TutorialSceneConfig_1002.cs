using System.Collections.Generic;
using GameLogic;
using Tools;
using UI;
using party.model;

namespace Core.Tutorial.SceneConfig;

public class TutorialSceneConfig_1002 : TutorialSceneConfig
{
	public int MaxProgressLimit;

	public TutorialSceneConfig_1002(int selectHeroId, int skinItemId)
		: base(selectHeroId)
	{
		_roomId = 88888;
		_MapId = 1002;
		Player item = BuildMimiServerPlayer(108);
		Player player = BuildPlayerServerPlayer(selectHeroId, skinItemId);
		HeroBarBox heroBarBox = GetHeroBarBox(new List<Player> { item, player });
		info = BuildRoom();
		info.MasterId = player.Id;
		info.Players.Add(item);
		info.Players.Add(player);
		info.Box = heroBarBox;
		MapGameDifficultyConfigureItem safeByIndex = info.MapId.GetMapDataConfigure().DifficultyIds.GetSafeByIndex(0).GetMapGameDifficultyItems().GetSafeByIndex(0);
		MaxProgressLimit = safeByIndex.ProgressLimit;
	}

	private Player BuildPlayerServerPlayer(int heroId, int skinItemId)
	{
		Player player = base.BuildSelfServerPlayer(heroId, skinItemId);
		player.Slot = 1;
		player.ChangeSlot = 1;
		player.Hero.NodeId = 19;
		player.Hero.FrontNodeIds.Add(0);
		player.Hero.BeBornNodeId = 19;
		player.Hero.Gold = 12;
		player.Hero.Hp = 10;
		player.Hero.MaxHp = 10;
		player.Hero.Attack = 1;
		player.Hero.Defense = 1;
		player.Hero.TeamId = 1;
		player.Hero.Cards.Add(HandCardData.GetTutorialCardInfo(21002));
		return player;
	}

	private Player BuildMimiServerPlayer(int heroId)
	{
		Player player = BuildServerPlayer(heroId, 0);
		player.Slot = 0;
		player.ChangeSlot = 0;
		player.Hero.NodeId = 1;
		player.Hero.BeBornNodeId = 36;
		player.Hero.FrontNodeIds.Add(1);
		player.Hero.Gold = 12;
		player.Hero.Hp = 10;
		player.Hero.MaxHp = 10;
		player.Hero.Attack = 1;
		player.Hero.Defense = 1;
		player.Hero.Cards.Add(HandCardData.GetTutorialCardInfo(21002));
		player.Hero.Cards.Add(HandCardData.GetTutorialCardInfo(20008));
		player.Hero.Cards.Add(HandCardData.GetTutorialCardInfo(10002));
		player.Hero.Cards.Add(HandCardData.GetTutorialCardInfo(21001));
		player.Hero.TeamId = 1;
		player.Progress = 100;
		FashionPlan safeByIndex = player.FashionPlan.GetSafeByIndex(0);
		if (safeByIndex != null)
		{
			safeByIndex.Fashion[1] = 70108;
			safeByIndex.Fashion[2] = 71208;
		}
		return player;
	}

	public Player BuildMonsterServerPlayer1002(int index, int nodeId, int frontId)
	{
		Player player = BuildServerPlayer(1002, 0);
		player.Slot = 0;
		player.ChangeSlot = 0;
		player.Hero.NodeId = nodeId;
		player.Hero.FrontNodeIds.Add(frontId);
		player.Hero.Hp = 8;
		player.Hero.MaxHp = 8;
		player.Hero.Attack = 1;
		player.Hero.Defense = 0;
		player.Hero.TeamId = 2;
		player.Hero.MonsterIndex = index;
		return player;
	}

	public Player BuildMonsterServerPlayer1005(int index, int nodeId, int frontId)
	{
		Player player = BuildServerPlayer(1005, 0);
		player.Slot = 0;
		player.ChangeSlot = 0;
		player.Hero.NodeId = nodeId;
		player.Hero.FrontNodeIds.Add(frontId);
		player.Hero.Hp = 10;
		player.Hero.MaxHp = 10;
		player.Hero.Attack = 1;
		player.Hero.Defense = 0;
		player.Hero.TeamId = 2;
		player.Hero.MonsterIndex = index;
		return player;
	}

	public Player BuildMonsterServerPlayer1004()
	{
		Player player = BuildServerPlayer(1004, 0);
		player.Slot = 0;
		player.ChangeSlot = 0;
		player.Hero.NodeId = 25;
		player.Hero.FrontNodeIds.Add(10);
		player.Hero.Hp = 20;
		player.Hero.MaxHp = 20;
		player.Hero.Attack = 2;
		player.Hero.Defense = 0;
		player.Hero.TeamId = 2;
		return player;
	}
}
