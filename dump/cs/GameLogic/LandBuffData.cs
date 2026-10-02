using Core;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using party.model;

namespace GameLogic;

public class LandBuffData
{
	public readonly long UniqueId;

	public readonly int LandId;

	public readonly Buff buffData;

	public bool FinishSummon { get; private set; }

	public Summon Summon { get; private set; }

	public LandBuffData(long _uniqueId, Buff buff, int _landId)
	{
		UniqueId = _uniqueId;
		LandId = _landId;
		buffData = buff;
		FinishSummon = false;
	}

	public async UniTask CreateSummon()
	{
		if (buffData.Source.S == buff_source.Types.source.Summon && !FinishSummon)
		{
			FinishSummon = true;
			await AddSummon(buffData.Source.Id);
		}
	}

	public int IsNeighbor(int landId)
	{
		if (!SimpleSingletonProvider<LandManager>.inst.GetNeighborLandIds(landId).Contains(LandId))
		{
			return -1;
		}
		return 1;
	}

	public void HideSummon()
	{
		if (buffData.KeepRound == 0)
		{
			CloseSummon();
		}
	}

	public void CloseSummon()
	{
		if (buffData.Source.S == buff_source.Types.source.Summon)
		{
			Summon?.CloseSummon();
			Summon = null;
		}
	}

	private async UniTask AddSummon(int _summonID)
	{
		if (Summon == null)
		{
			Summon = new Summon(_summonID, this);
			await Summon.SetSummonObj();
		}
	}

	public BattlePlayerData TryGetBuffOwner()
	{
		if (buffData.Params != null && buffData.Params.TryGetValue(31, out var value))
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(value);
		}
		return null;
	}
}
