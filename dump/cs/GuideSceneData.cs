using System.Collections.Generic;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using party.model;

public class GuideSceneData
{
	public Room info;

	public GuideSceneType sceneType;

	protected int _RoomId;

	protected readonly int _MapId = 81099;

	protected Hero playerHero;

	protected Player playerInfo => SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo();

	protected FashionPlan fashion => new FashionPlan
	{
		Plan = 1,
		Fashion = { (IDictionary<int, int>)SimpleSingletonProvider<GameLogicManager>.inst.fashion.defaultMap }
	};

	protected Player CustomPlayer => new Player
	{
		Id = playerInfo.Id,
		Nick = playerInfo.Nick,
		WaitOffLine = false,
		OffLine = false,
		RoomId = _RoomId,
		Slot = 0,
		ChangeSlot = 0,
		Progress = 0,
		Hero = playerHero,
		FashionPlan = { (IEnumerable<FashionPlan>)new RepeatedField<FashionPlan> { fashion } }
	};
}
