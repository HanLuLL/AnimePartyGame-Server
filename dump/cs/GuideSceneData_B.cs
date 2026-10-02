using System.Collections.Generic;
using GameLogic;
using Google.Protobuf.Collections;
using party.model;

public class GuideSceneData_B : GuideSceneData
{
	public GuideSceneData_B()
	{
		_RoomId = 90001;
		sceneType = GuideSceneType.GuideSceneB;
		CharacterHandle characterHandle = new CharacterHandle(108);
		playerHero = new Hero
		{
			PlayerId = base.playerInfo.Id,
			TeamId = 1,
			HeroId = characterHandle.Id,
			Hp = 7,
			MaxHp = characterHandle.Blood,
			Gold = 30,
			Lv = 2,
			Attack = characterHandle.Attack,
			Defense = characterHandle.Defense,
			UserCardNum = 1,
			UseCardMaxNum = 1,
			Cards = 
			{
				HandCardData.GetTutorialCardInfo(10003),
				HandCardData.GetTutorialCardInfo(20013),
				HandCardData.GetTutorialCardInfo(10003)
			},
			NodeId = 7,
			BackNodeId = 8,
			Affirm = true
		};
		CharacterHandle characterHandle2 = new CharacterHandle(101);
		Hero hero = new Hero
		{
			PlayerId = 1L,
			TeamId = 2,
			HeroId = characterHandle2.Id,
			Hp = 10,
			MaxHp = characterHandle2.Blood,
			Gold = 40,
			Lv = 0,
			Attack = characterHandle2.Attack,
			Defense = characterHandle2.Defense,
			NodeId = 5,
			BackNodeId = 6,
			Affirm = true
		};
		CharacterHandle characterHandle3 = new CharacterHandle(105);
		Hero hero2 = new Hero
		{
			PlayerId = 2L,
			TeamId = 3,
			HeroId = characterHandle3.Id,
			Hp = 6,
			MaxHp = characterHandle3.Blood,
			Gold = 50,
			Lv = 2,
			Attack = characterHandle3.Attack,
			Defense = characterHandle3.Defense,
			NodeId = 2,
			BackNodeId = 1,
			Affirm = true
		};
		Player player = new Player
		{
			Id = 1L,
			Nick = "NPC_01",
			WaitOffLine = false,
			OffLine = false,
			RoomId = _RoomId,
			Slot = 1,
			ChangeSlot = 1,
			Progress = 100,
			Hero = hero,
			FashionPlan = { (IEnumerable<FashionPlan>)new RepeatedField<FashionPlan> { base.fashion } }
		};
		Player player2 = new Player
		{
			Id = 2L,
			Nick = "NPC_02",
			WaitOffLine = false,
			OffLine = false,
			RoomId = _RoomId,
			Slot = 2,
			ChangeSlot = 2,
			Progress = 100,
			Hero = hero2,
			FashionPlan = { (IEnumerable<FashionPlan>)new RepeatedField<FashionPlan> { base.fashion } }
		};
		HeroBarBox box = new HeroBarBox
		{
			Box = { (IDictionary<long, HeroBar>)new MapField<long, HeroBar>
			{
				[playerHero.PlayerId] = new HeroBar
				{
					PlayerId = playerHero.PlayerId,
					HeroId = playerHero.HeroId,
					Affirm = true
				},
				[player.Id] = new HeroBar
				{
					PlayerId = player.Id,
					HeroId = player.Hero.HeroId,
					Affirm = true
				},
				[player2.Id] = new HeroBar
				{
					PlayerId = player2.Id,
					HeroId = player2.Hero.HeroId,
					Affirm = true
				}
			} }
		};
		info = new Room
		{
			Id = _RoomId,
			Name = "Guidance",
			Pwd = "",
			MaxTime = 7200,
			MapId = _MapId,
			MapDataId = 1,
			MasterId = 0L,
			MapType = 2,
			Players = { (IEnumerable<Player>)new Player[3] { base.CustomPlayer, player, player2 } },
			State = Room.Types.State.Running,
			UpgradePlan = 1,
			Box = box,
			Round = 2
		};
	}
}
