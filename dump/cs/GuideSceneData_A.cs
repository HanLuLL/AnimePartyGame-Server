using System.Collections.Generic;
using GameLogic;
using Google.Protobuf.Collections;
using party.model;

public class GuideSceneData_A : GuideSceneData
{
	public GuideSceneData_A()
	{
		_RoomId = 90001;
		sceneType = GuideSceneType.GuideSceneA;
		CharacterHandle characterHandle = new CharacterHandle(108);
		playerHero = new Hero
		{
			PlayerId = base.playerInfo.Id,
			TeamId = 1,
			HeroId = characterHandle.Id,
			Hp = 7,
			MaxHp = characterHandle.Blood,
			Gold = 46,
			Lv = 2,
			Attack = characterHandle.Attack,
			Defense = characterHandle.Defense,
			UserCardNum = 1,
			UseCardMaxNum = 1,
			Cards = 
			{
				HandCardData.GetTutorialCardInfo(10003),
				HandCardData.GetTutorialCardInfo(10003),
				HandCardData.GetTutorialCardInfo(20002)
			},
			NodeId = 36,
			BackNodeId = 35,
			Affirm = true
		};
		HeroBarBox box = new HeroBarBox
		{
			Box = { (IDictionary<long, HeroBar>)new MapField<long, HeroBar> { [playerHero.PlayerId] = new HeroBar
			{
				PlayerId = playerHero.PlayerId,
				HeroId = playerHero.HeroId,
				Affirm = true
			} } }
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
			Players = { (IEnumerable<Player>)new Player[1] { base.CustomPlayer } },
			State = Room.Types.State.Running,
			UpgradePlan = 1,
			Box = box,
			Round = 1
		};
	}
}
