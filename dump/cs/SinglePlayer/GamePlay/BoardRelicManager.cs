using System.Collections.Generic;
using System.Linq;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Character;
using SinglePlayer.GamePlay.Map;
using SinglePlayer.GamePlay.Relic;
using Tools;

namespace SinglePlayer.GamePlay;

public class BoardRelicManager
{
	private HeroProperty heroProperty;

	private int _colorfulRelicLimit;

	public void Initialize()
	{
		_colorfulRelicLimit = StaticConfigure.SinglePlayer.Params.GetSafeByIndex(0).RelicMax;
		heroProperty = Game.GetModel<GameData>().heroProperty;
		Game.GetModel<GlobalSignal>().RoundStart.AddListener(OnRoundStart);
		Game.GetModel<GlobalSignal>().RoundEnd.AddListener(OnRoundEnd);
		Game.GetModel<GlobalSignal>().PassLand.AddListener(OnPassLand);
		Game.GetModel<GlobalSignal>().MoveStop.AddListener(OnMoveStop);
		Game.GetModel<GlobalSignal>().GenerateDicePoint.AddListener(OnGenerateDicePoint);
		Game.GetModel<GlobalSignal>().SellingCard.AddListener(OnSellingCard);
		Game.GetModel<GlobalSignal>().RefreshShop.AddListener(OnRefreshShop);
		Game.GetModel<GlobalSignal>().BuildingTriggerStayEffect.AddListener(OnBuildingTriggerStayEffect);
	}

	public void Dispose()
	{
		Game.GetModel<GlobalSignal>().RoundStart.RemoveListener(OnRoundStart);
		Game.GetModel<GlobalSignal>().RoundEnd.RemoveListener(OnRoundEnd);
		Game.GetModel<GlobalSignal>().PassLand.RemoveListener(OnPassLand);
		Game.GetModel<GlobalSignal>().MoveStop.RemoveListener(OnMoveStop);
		Game.GetModel<GlobalSignal>().GenerateDicePoint.RemoveListener(OnGenerateDicePoint);
		Game.GetModel<GlobalSignal>().SellingCard.RemoveListener(OnSellingCard);
		Game.GetModel<GlobalSignal>().RefreshShop.RemoveListener(OnRefreshShop);
		Game.GetModel<GlobalSignal>().BuildingTriggerStayEffect.RemoveListener(OnBuildingTriggerStayEffect);
	}

	private void OnRoundStart()
	{
		if (Game.GetModel<GameData>().RoundTiming == RoundTiming.RoundStart)
		{
			foreach (RelicInfo relic in heroProperty.RelicList)
			{
				relic.BuffData.OnRoundStart?.Apply(relic);
			}
		}
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	private void OnRoundEnd()
	{
		foreach (RelicInfo relic in heroProperty.RelicList)
		{
			relic.BuffData.OnRoundEnd?.Apply(relic);
		}
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	private void OnPassLand(int arg1, int arg2)
	{
		foreach (RelicInfo relic in heroProperty.RelicList)
		{
			relic.BuffData.OnPassLand?.Apply(relic);
		}
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	private void OnMoveStop(Land land)
	{
		foreach (RelicInfo relic in heroProperty.RelicList)
		{
			relic.BuffData.OnMoveStop?.Apply(relic);
		}
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	private void OnGenerateDicePoint(List<int> obj)
	{
		foreach (RelicInfo relic in heroProperty.RelicList)
		{
			relic.BuffData.OnThrowDice?.TryAddChargeOnThrowDice(relic, obj);
			relic.BuffData.OnThrowDice?.Apply(relic);
		}
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	private void OnSellingCard(int configId, int obj)
	{
		foreach (RelicInfo relic in heroProperty.RelicList)
		{
			relic.BuffData.OnSellBuilding?.Apply(relic);
		}
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	private void OnRefreshShop()
	{
		foreach (RelicInfo relic in heroProperty.RelicList)
		{
			relic.BuffData.OnRefreshShop?.Apply(relic);
		}
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	private void OnBuildingTriggerStayEffect(int configId)
	{
		foreach (RelicInfo relic in heroProperty.RelicList)
		{
			BaseRelicBuffModule onBuildingTriggerStayEffect = relic.BuffData.OnBuildingTriggerStayEffect;
			if (onBuildingTriggerStayEffect != null && onBuildingTriggerStayEffect.TryAddChargeOnBuildTriggerEffect(relic, configId, ParameterType.Stay))
			{
				onBuildingTriggerStayEffect.Apply(relic);
			}
		}
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	public void AddRelic(RelicInfo relicInfo)
	{
		relicInfo.BuffData.OnCreate?.Apply(relicInfo);
		heroProperty.RelicList.Add(relicInfo);
		Game.GetModel<GlobalSignal>().AddRelic.Dispatch(relicInfo.BuffData.Id);
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	public void RemoveRelic(RelicInfo relicInfo)
	{
		relicInfo.BuffData.OnRemove?.Apply(relicInfo);
		heroProperty.RelicList.Remove(relicInfo);
		Game.GetModel<GlobalSignal>().ApplyRelic.Dispatch();
	}

	public bool CanObtainColorfulRelic()
	{
		return heroProperty.RelicList.Count((RelicInfo r) => r.BuffData.Config.Rarity == 4) < _colorfulRelicLimit;
	}

	public bool ExistRelic(int id)
	{
		return heroProperty.RelicList.Any((RelicInfo relicInfo) => relicInfo.BuffData.Id == id);
	}

	private RelicInfo FindRelic(int Id)
	{
		foreach (RelicInfo relic in heroProperty.RelicList)
		{
			if (relic.BuffData.Id == Id)
			{
				return relic;
			}
		}
		return null;
	}
}
