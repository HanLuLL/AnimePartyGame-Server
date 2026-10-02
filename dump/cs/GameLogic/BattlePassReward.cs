using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace GameLogic;

public class BattlePassReward
{
	public BattlePassRewardType rewardType;

	public int BattlePassId;

	public int LV;

	public BattlePassRewardData FreeReward;

	public BattlePassRewardData NormalReward;

	public BattlePassRewardData PremiumReward;

	public BattlePassRewardConfigureItem RewardConfig;

	public bool FreeVailReward
	{
		get
		{
			if (FreeReward.vailLV)
			{
				return !IsFinishFreeReward();
			}
			return false;
		}
	}

	public bool NormalVailReward
	{
		get
		{
			if (NormalReward == null)
			{
				return false;
			}
			if (NormalReward.vailLV)
			{
				return !IsFinishNormalReward();
			}
			return false;
		}
	}

	public bool PremiumVailReward
	{
		get
		{
			if (PremiumReward == null)
			{
				return false;
			}
			if (PremiumReward.vailLV)
			{
				return !IsFinishPremiumReward();
			}
			return false;
		}
	}

	public bool VailReward
	{
		get
		{
			if (rewardType == BattlePassRewardType.COMMON)
			{
				if (!FreeVailReward && !NormalVailReward)
				{
					return PremiumVailReward;
				}
				return true;
			}
			if (rewardType == BattlePassRewardType.SURPASS)
			{
				if (FreeReward.vailLV)
				{
					return !IsFinishSurpassReward();
				}
				return false;
			}
			return false;
		}
	}

	public bool SurpassVailReward
	{
		get
		{
			if (FreeReward.vailLV)
			{
				return !IsFinishSurpassReward();
			}
			return false;
		}
	}

	public int vailSurpassRewardTime
	{
		get
		{
			int lV = SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData.LV;
			int finishRewardLV = SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetFinishRewardLV(BattlePassGearType.FREE);
			if (LV <= lV)
			{
				return lV - Mathf.Max(SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData.MaxLevel, finishRewardLV);
			}
			return 0;
		}
	}

	public BattlePassReward(int _BattlePassId, BattlePassRewardConfigureItem _rewardConfig)
	{
		BattlePassId = _BattlePassId;
		RewardConfig = _rewardConfig;
		LV = _rewardConfig.Level;
		rewardType = BattlePassRewardType.COMMON;
		FreeReward = new BattlePassRewardData(_rewardConfig.FreeRewards, LV, 0, null);
		if (_rewardConfig.NormalRewards != null && _rewardConfig.NormalRewards.Count > 0)
		{
			NormalReward = new BattlePassRewardData(_rewardConfig.NormalRewards, LV, _rewardConfig.NormalInfoType, _rewardConfig.NormalIcon);
		}
		if (_rewardConfig.PremiumRewards != null && _rewardConfig.PremiumRewards.Count > 0)
		{
			PremiumReward = new BattlePassRewardData(_rewardConfig.PremiumRewards, LV, _rewardConfig.PremiumInfoType, _rewardConfig.PremiumIcon);
		}
	}

	public bool IsFinishFreeReward()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetFinishRewardLV(BattlePassGearType.FREE) >= RewardConfig.Level;
	}

	public bool IsFinishNormalReward()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetGearStatus(BattlePassGearType.NORMAL))
		{
			return true;
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetFinishRewardLV(BattlePassGearType.NORMAL) >= RewardConfig.Level;
	}

	public bool IsFinishPremiumReward()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetGearStatus(BattlePassGearType.PREMIUM))
		{
			return true;
		}
		if (PremiumReward == null)
		{
			return true;
		}
		return SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetFinishRewardLV(BattlePassGearType.PREMIUM) >= RewardConfig.Level;
	}

	public BattlePassReward(int _BattlePassId, int _LV, MapField<int, int> RewardPerLvAfterMax)
	{
		BattlePassId = _BattlePassId;
		LV = _LV;
		rewardType = BattlePassRewardType.SURPASS;
		FreeReward = new BattlePassRewardData(RewardPerLvAfterMax, LV, 0, null);
	}

	public bool IsFinishSurpassReward()
	{
		int lV = SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData.LV;
		if (LV <= lV)
		{
			return vailSurpassRewardTime == 0;
		}
		return false;
	}
}
