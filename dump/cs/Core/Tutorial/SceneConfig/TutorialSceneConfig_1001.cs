using System.Collections.Generic;
using GameLogic;
using Tools;
using party.model;

namespace Core.Tutorial.SceneConfig;

public class TutorialSceneConfig_1001 : TutorialSceneConfig
{
	public TutorialSceneConfig_1001(int selectHeroId, int skinItemId)
		: base(selectHeroId)
	{
		_roomId = 77777;
		_MapId = 1001;
		Player item = BuildMimiServerPlayer(108);
		Player player = BuildPlayerServerPlayer(selectHeroId, skinItemId);
		HeroBarBox heroBarBox = GetHeroBarBox(new List<Player> { item, player });
		info = BuildRoom();
		info.MasterId = player.Id;
		info.Players.Add(item);
		info.Players.Add(player);
		info.Box = heroBarBox;
	}

	private Player BuildPlayerServerPlayer(int heroId, int skinItemId)
	{
		Player player = base.BuildSelfServerPlayer(heroId, skinItemId);
		player.Slot = 1;
		player.ChangeSlot = 1;
		player.Hero.NodeId = 0;
		player.Hero.FrontNodeIds.Add(1);
		player.Hero.BeBornNodeId = 0;
		player.Hero.Gold = 12;
		player.Hero.Hp = 6;
		player.Hero.Attack = 1;
		player.Hero.Defense = 1;
		player.Hero.TeamId = 1;
		return player;
	}

	private Player BuildMimiServerPlayer(int heroId)
	{
		Player player = BuildServerPlayer(heroId, 0);
		player.Slot = 0;
		player.ChangeSlot = 0;
		player.Hero.NodeId = 36;
		player.Hero.BeBornNodeId = 36;
		player.Hero.FrontNodeIds.Add(37);
		player.Hero.Gold = 12;
		player.Hero.Hp = 6;
		player.Hero.Attack = 1;
		player.Hero.Defense = 1;
		player.Hero.Cards.Add(HandCardData.GetTutorialCardInfo(21004));
		player.Hero.Cards.Add(HandCardData.GetTutorialCardInfo(10001));
		player.Hero.Cards.Add(HandCardData.GetTutorialCardInfo(10003));
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

	public Player BuildRaccoonServerPlayer(int nodeId, int frontId)
	{
		Player player = BuildServerPlayer(1001, 0);
		player.Hero.NodeId = nodeId;
		player.Hero.FrontNodeIds.Add(frontId);
		player.Hero.Gold = 5;
		player.Hero.Hp = 6;
		player.Hero.TeamId = 2;
		return player;
	}
}
