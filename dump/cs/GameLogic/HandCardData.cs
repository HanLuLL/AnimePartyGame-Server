using Core.Tutorial.Tools;
using UI;
using party.model;

namespace GameLogic;

public class HandCardData
{
	public int Guid;

	public int CardId;

	public bool IsTemp;

	public int PurifyNum;

	private int _BattleCost;

	private CardInfoConfigure _Config;

	public int BattleCost
	{
		get
		{
			return _BattleCost;
		}
		set
		{
			_BattleCost = ((value < 0) ? Config.Cost : value);
		}
	}

	public CardInfoConfigure Config
	{
		get
		{
			if (_Config == null)
			{
				_Config = CardId.GetCardConfigure();
			}
			return _Config;
		}
	}

	public HandCardData(int _Id, int _Guid, int _purifyNum, bool _isTemp = false, int battleCost = -1)
	{
		Guid = _Guid;
		IsTemp = _isTemp;
		CardId = _Id;
		PurifyNum = _purifyNum;
		BattleCost = battleCost;
	}

	public HandCardData(CardInfo handCardinfo)
	{
		Guid = handCardinfo.UniqueId;
		CardId = handCardinfo.CardId;
		IsTemp = handCardinfo.IsTemp;
		PurifyNum = handCardinfo.PurifyNum;
		BattleCost = handCardinfo.BattleCost;
	}

	public void UpdateCardData(CardInfo handCardId)
	{
		if (CardId != handCardId.CardId)
		{
			_Config = null;
		}
		CardId = handCardId.CardId;
		IsTemp = handCardId.IsTemp;
		PurifyNum = handCardId.PurifyNum;
		BattleCost = handCardId.BattleCost;
	}

	public static HandCardData GetTutorialHandCardData(int _Id, bool _isTemp = false)
	{
		return new HandCardData(_Id, UIDGenerator.NextUID(), 0, _isTemp);
	}

	public static CardInfo GetTutorialCardInfo(int _Id, bool _isTemp = false)
	{
		return new CardInfo
		{
			CardId = _Id,
			UniqueId = UIDGenerator.NextUID(),
			IsTemp = _isTemp,
			BattleCost = -1
		};
	}
}
