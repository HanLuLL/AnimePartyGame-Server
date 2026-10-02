using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;

namespace GameLogic;

public class HeroCardData
{
	private readonly int _HeroId;

	private int _Exp;

	private int _LV;

	private int _UseDressId;

	private bool _CollectStatus;

	private bool _isBreakThrough;

	private SkinStandingPaintingConfigureItem _StandingPainting;

	private readonly CharacterInfoConfigure _InfoConfig;

	private readonly bool _IsHas;

	private readonly HeroPveStrengthenData _PveData;

	private List<PveExpItemData> _PVEExpItems;

	public int HeroId => _HeroId;

	public int Exp => _Exp;

	public int LV => _LV;

	public int UseDressId => _UseDressId;

	public bool CollectStatus => _CollectStatus;

	public bool isBreakThrough => _isBreakThrough;

	public SkinStandingPaintingConfigureItem standingPainting => _StandingPainting;

	public CharacterInfoConfigure InfoConfig => _InfoConfig;

	public bool IsHas => _IsHas;

	public HeroPveStrengthenData PveData => _PveData;

	public List<PveExpItemData> PVEExpItems
	{
		get
		{
			if (_PVEExpItems == null)
			{
				_PVEExpItems = new List<PveExpItemData>();
				RepeatedField<PVENurturanceItemExpConfigure> itemExps = StaticConfigure.PVENurturance.ItemExps;
				for (int i = 0; i < itemExps.Count; i++)
				{
					_PVEExpItems.Add(new PveExpItemData(itemExps[i]));
				}
			}
			else
			{
				foreach (PveExpItemData pVEExpItem in _PVEExpItems)
				{
					pVEExpItem.selectCount = 0;
				}
			}
			_PVEExpItems.Sort((PveExpItemData x, PveExpItemData y) => (x.itemInfo.QualityType != y.itemInfo.QualityType) ? (-x.itemInfo.QualityType.CompareTo(y.itemInfo.QualityType)) : (-x.Count.CompareTo(y.Count)));
			return _PVEExpItems;
		}
	}

	public HeroStatus heroStatus
	{
		get
		{
			RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
			if (room != null && room.IsInRoom && room.curRoomInfo.IsCampaign())
			{
				RepeatedField<CampaignTryOutConfigure> tryOuts = StaticConfigure.Campaign.TryOuts;
				if (!_IsHas && tryOuts.Count != 0 && tryOuts[0].FobiddenTryOut.Contains(_HeroId))
				{
					return HeroStatus.None;
				}
				RepeatedField<int> repeatedField = SimpleSingletonProvider<GameLogicManager>.inst.campaign.CampaignLevel?.HeroesLimited;
				if (repeatedField != null && repeatedField.Count > 0)
				{
					if (repeatedField.Contains(_HeroId))
					{
						if (_IsHas)
						{
							return HeroStatus.Activate;
						}
						return HeroStatus.Levels_TryOut;
					}
					return HeroStatus.None;
				}
			}
			if (_IsHas)
			{
				return HeroStatus.Activate;
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.IsActivityTrialHeroIds(_HeroId))
			{
				return HeroStatus.Activity_TryOut;
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.IsComebackTrialHeroIds(_HeroId))
			{
				return HeroStatus.Comeback_TryOut;
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.IsNoviceTrialHeroIds(_HeroId))
			{
				return HeroStatus.Novice_TryOut;
			}
			return HeroStatus.None;
		}
	}

	public HeroCardData(int heroID, RoleCard info)
	{
		if (info != null)
		{
			_Exp = info.Exp;
			_LV = info.Lv;
			_UseDressId = info.UseAdorn;
			_isBreakThrough = info.IsBreakThrough;
		}
		_HeroId = heroID;
		_InfoConfig = heroID.GetCharacterConfigure();
		_StandingPainting = GetStandingPainting();
		_IsHas = info != null;
		_CollectStatus = info?.Collected ?? false;
		_PveData = new HeroPveStrengthenData(this, _IsHas ? info.PveStrengthen : null);
	}

	public SkinStandingPaintingConfigureItem GetStandingPainting()
	{
		return SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(_HeroId, UseDressId, 0);
	}

	public void UpdateExp(int exp)
	{
		_Exp = exp;
	}

	public void UpdateLV(int LV)
	{
		_LV = LV;
	}

	public void UpdateUseDressId(int dressId)
	{
		_UseDressId = dressId;
		_StandingPainting = GetStandingPainting();
	}

	public void UpdateBreakThough(bool breakThrough)
	{
		_isBreakThrough = breakThrough;
	}

	public void UpdateCollectStatus()
	{
		_CollectStatus = !_CollectStatus;
	}
}
