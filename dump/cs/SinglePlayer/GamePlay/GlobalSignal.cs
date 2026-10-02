using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Character;
using SinglePlayer.GamePlay.Map;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class GlobalSignal : IModel, IDispose
{
	public delegate UniTask ActionAsync();

	public delegate UniTask ActionAsync<T>(T arg);

	public delegate UniTask AttributeChangeInfoActionAsync(AttributeChangeInfo message);

	public Signal<Land> MoveStop = new Signal<Land>();

	public readonly Signal ShopCardChange = new Signal();

	public readonly Signal BagCardChange = new Signal();

	public readonly Signal<int, bool> CardUsed = new Signal<int, bool>();

	public readonly Signal<int> CardUpgrade = new Signal<int>();

	public readonly Signal<int> CardGainExp = new Signal<int>();

	public readonly Signal<int, int> SellingCard = new Signal<int, int>();

	public readonly Signal<int> GetCardPerformance = new Signal<int>();

	public readonly Signal<bool> Card = new Signal<bool>();

	public readonly Signal<bool> ThrowDice = new Signal<bool>();

	public readonly Signal<bool> DevelopLand = new Signal<bool>();

	public readonly Signal RefreshShop = new Signal();

	public readonly Signal<int> DragStart = new Signal<int>();

	public readonly Signal DragMoving = new Signal();

	public readonly Signal<bool> DragEnd = new Signal<bool>();

	public readonly Signal<int> SellingBuilding = new Signal<int>();

	public readonly Signal<int> SelectDevelopLand = new Signal<int>();

	public Signal<IUnitView, AttributeChangeInfo> BuildingAttributeChangeShow = new Signal<IUnitView, AttributeChangeInfo>();

	public Signal Down { get; private set; } = new Signal();

	public Signal Up { get; private set; } = new Signal();

	public Signal GameStartBefore { get; private set; } = new Signal();

	public Signal GameStart { get; private set; } = new Signal();

	public Signal GameStartAfter { get; private set; } = new Signal();

	public Signal GameEndBefore { get; private set; } = new Signal();

	public Signal GameEnd { get; private set; } = new Signal();

	public Signal GameEndAfter { get; private set; } = new Signal();

	public Signal RoundStartBefore { get; private set; } = new Signal();

	public Signal RoundStart { get; private set; } = new Signal();

	public Signal RoundStartAfter { get; private set; } = new Signal();

	public Signal RoundEndBefore { get; private set; } = new Signal();

	public Signal RoundEnd { get; private set; } = new Signal();

	public Signal RoundEndAfter { get; private set; } = new Signal();

	public Signal<Hero> HeroCreated { get; private set; } = new Signal<Hero>();

	public Signal<List<int>> GenerateDicePoint { get; private set; } = new Signal<List<int>>();

	public Signal<int, int> PassLand { get; private set; } = new Signal<int, int>();

	public Signal<Monster> MonsterCreated { get; private set; } = new Signal<Monster>();

	public Signal<Monster> MonsterDeath { get; private set; } = new Signal<Monster>();

	public Signal<Monster> MonsterDisposed { get; private set; } = new Signal<Monster>();

	public ActionAsync MonsterExit { get; set; }

	public ActionAsync MonsterEnter { get; set; }

	public Signal<int, int, int> BuildingCreate { get; private set; } = new Signal<int, int, int>();

	public Signal<BuildingView> BuildingViewCreate { get; private set; } = new Signal<BuildingView>();

	public Signal<int, int, int> BuildingMoveSuccess { get; private set; } = new Signal<int, int, int>();

	public Signal<int, int, int> BuildingMoveFailure { get; private set; } = new Signal<int, int, int>();

	public Signal<int, int, int> BuildingMoveStart { get; private set; } = new Signal<int, int, int>();

	public Signal<int, int, int> BuildingRemove { get; private set; } = new Signal<int, int, int>();

	public Signal BuildingMoveUpgrade { get; private set; } = new Signal();

	public Signal<int> BuildingTriggerStayEffect { get; private set; } = new Signal<int>();

	public Signal<bool, BuildingBase, Vector3> ShowBuildingInfo { get; private set; } = new Signal<bool, BuildingBase, Vector3>();

	public AttributeChangeInfoActionAsync BuildingShow { get; set; }

	public Signal<int> BuildingShowLevel { get; private set; } = new Signal<int>();

	public Signal BuildingHideLevel { get; private set; } = new Signal();

	public Signal<int, int, int> BuildingOperateBonus { get; private set; } = new Signal<int, int, int>();

	public Signal<int> DevelopLandSucceed { get; private set; } = new Signal<int>();

	public Signal ApplyRelic { get; private set; } = new Signal();

	public Signal<int> AddRelic { get; private set; } = new Signal<int>();

	public Signal MissionStart { get; private set; } = new Signal();

	public Signal<int, int> MissionProgressChange { get; private set; } = new Signal<int, int>();

	public Signal<int> MissionStatusChange { get; private set; } = new Signal<int>();

	public Signal SettleMission { get; private set; } = new Signal();

	public void Initialize()
	{
	}

	public void Dispose()
	{
		Dispose_GamePlay();
		Dispose_Action();
		Dispose_Hero();
		Dispose_Monster();
		Dispose_Card();
		Dispose_Map();
		Dispose_Building();
		Dispose_Mission();
		Dispose_AttributeChangeShow();
		Dispose_Relic();
	}

	private void Dispose_GamePlay()
	{
		GameStartBefore.RemoveAllListeners();
		GameStart.RemoveAllListeners();
		GameStartAfter.RemoveAllListeners();
		GameEndBefore.RemoveAllListeners();
		GameEnd.RemoveAllListeners();
		GameEndAfter.RemoveAllListeners();
		RoundStartBefore.RemoveAllListeners();
		RoundStart.RemoveAllListeners();
		RoundStartAfter.RemoveAllListeners();
		RoundEndBefore.RemoveAllListeners();
		RoundEnd.RemoveAllListeners();
		RoundEndAfter.RemoveAllListeners();
	}

	private void Dispose_Hero()
	{
		HeroCreated.RemoveAllListeners();
		MoveStop.RemoveAllListeners();
		GenerateDicePoint.RemoveAllListeners();
		PassLand.RemoveAllListeners();
	}

	private void Dispose_Monster()
	{
		MonsterCreated.RemoveAllListeners();
		MonsterDeath.RemoveAllListeners();
		MonsterDisposed.RemoveAllListeners();
		MonsterExit = null;
		MonsterEnter = null;
	}

	private void Dispose_Card()
	{
		BagCardChange.RemoveAllListeners();
		ShopCardChange.RemoveAllListeners();
		CardUsed.RemoveAllListeners();
		CardUpgrade.RemoveAllListeners();
		GetCardPerformance.RemoveAllListeners();
	}

	private void Dispose_Action()
	{
		Card.RemoveAllListeners();
		ThrowDice.RemoveAllListeners();
		DevelopLand.RemoveAllListeners();
		RefreshShop.RemoveAllListeners();
	}

	private void Dispose_Map()
	{
		DragStart.RemoveAllListeners();
		DragMoving.RemoveAllListeners();
		DragEnd.RemoveAllListeners();
	}

	private void Dispose_Building()
	{
		BuildingViewCreate.RemoveAllListeners();
		BuildingCreate.RemoveAllListeners();
		BuildingRemove.RemoveAllListeners();
		BuildingMoveStart.RemoveAllListeners();
		BuildingMoveSuccess.RemoveAllListeners();
		BuildingMoveFailure.RemoveAllListeners();
		BuildingMoveUpgrade.RemoveAllListeners();
		ShowBuildingInfo.RemoveAllListeners();
		BuildingShow = null;
		BuildingShowLevel.RemoveAllListeners();
		BuildingHideLevel.RemoveAllListeners();
		BuildingOperateBonus.RemoveAllListeners();
		SelectDevelopLand.RemoveAllListeners();
		DevelopLandSucceed.RemoveAllListeners();
	}

	private void Dispose_Relic()
	{
		ApplyRelic.RemoveAllListeners();
		AddRelic.RemoveAllListeners();
	}

	private void Dispose_Mission()
	{
		MissionStatusChange.RemoveAllListeners();
		MissionProgressChange.RemoveAllListeners();
		MissionStart.RemoveAllListeners();
		SettleMission.RemoveAllListeners();
	}

	private void Dispose_AttributeChangeShow()
	{
		BuildingAttributeChangeShow.RemoveAllListeners();
	}
}
