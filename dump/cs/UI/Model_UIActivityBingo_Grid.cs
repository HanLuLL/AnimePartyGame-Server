using System.Collections.Generic;
using FairyGUI;
using GameLogic;
using Tools;

namespace UI;

public class Model_UIActivityBingo_Grid : BaseModel<UIActivityBingo_Button_Grid>
{
	private int id;

	private BingoCardType cardType;

	private BingoActivityData bingoActivityData;

	private const int btnGridNumId = 6100103;

	private static HashSet<int> showItemId = new HashSet<int> { 1, 2, 4, 5, 6 };

	private int numPerPage => numX * numY;

	private int numX => bingoActivityData.NumX;

	private int numY => bingoActivityData.NumY;

	private int roundCount => bingoActivityData.BingoActivityBingoConfig.RoundCount;

	private int activityId => bingoActivityData.activityConfig.Id;

	public Model_UIActivityBingo_Grid(GObject _com)
		: base(_com as UIActivityBingo_Button_Grid)
	{
	}

	public Model_UIActivityBingo_Grid(UIActivityBingo_Button_Grid _com)
		: base(_com)
	{
	}

	public override void AddEvent()
	{
		base.AddEvent();
		com.onClick.Add(OnClick);
	}

	public override void RemoveEvent()
	{
		base.RemoveEvent();
		com.onClick.Remove(OnClick);
	}

	private bool HasEnoughCost()
	{
		bool flag = true;
		foreach (KeyValuePair<int, int> item in bingoActivityData.BingoActivityBingoConfig.SpendItemID)
		{
			flag &= SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(item.Key) >= item.Value;
		}
		return flag;
	}

	private async void OnClick()
	{
		if (showItemId.Contains(com.state.selectedIndex) && !com.loader_RedPoint.visible)
		{
			BingoActivityGrid bingoActivityGridData = GetBingoActivityGridData();
			com.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(bingoActivityGridData.Config.ItemID, bingoActivityGridData.Config.ItemNum, _Usable: false);
			com.onClick.Release();
			return;
		}
		com.onClick.Retain();
		if (cardType == BingoCardType.FlipCard)
		{
			if (HasEnoughCost())
			{
				SimpleSingletonProvider<GameLogicManager>.inst.activity.RequestFlipCardC2S(activityId, id);
			}
			else
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1054);
			}
		}
		else if (cardType == BingoCardType.RewardCard && com.loader_RedPoint.visible)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.activity.RequestFlipCardProgressRewardC2S(activityId);
		}
		com.onClick.Release();
	}

	public void ChangeCardDesc(BingoActivityData data, int subId, int page, bool isProgressReward)
	{
		bingoActivityData = data;
		id = subId + page * numPerPage;
		if (isProgressReward)
		{
			cardType = BingoCardType.RewardCard;
			return;
		}
		bool flag = subId % numX < numX - 1 && subId / numX < numY - 1;
		cardType = ((!flag) ? BingoCardType.ExtraCard : BingoCardType.FlipCard);
	}

	public BingoActivityGrid GetBingoActivityGridData()
	{
		return bingoActivityData.GetBingoActivityGrid(id, cardType == BingoCardType.RewardCard);
	}

	public override void Refresh()
	{
		base.Refresh();
		BingoActivityGrid bingoActivityGridData = GetBingoActivityGridData();
		int num = (int)cardType;
		if (bingoActivityGridData.IsFlip)
		{
			num += 4;
		}
		bool num2 = showItemId.Contains(num);
		com.state.selectedIndex = num;
		if (num2)
		{
			int itemID = bingoActivityGridData.Config.ItemID;
			int itemNum = bingoActivityGridData.Config.ItemNum;
			com.loader_icon.url = itemID.GetItemInfoConfigure().ShowIcon;
			com.txt_icon.text = $"x{itemNum}";
		}
		if (cardType == BingoCardType.RewardCard)
		{
			com.txt_num.text = string.Format(6100103.GetLocal(UIStringType.Activity), id + 1);
		}
	}
}
