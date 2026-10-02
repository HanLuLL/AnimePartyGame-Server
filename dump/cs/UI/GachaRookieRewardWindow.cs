using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class GachaRookieRewardWindow : BaseWindow
{
	private int SelectedItemId;

	private int PoolId;

	private GachaProgressConfigureItem ProgressItem;

	private readonly List<KeyValuePair<int, int>> RewardItemList = new List<KeyValuePair<int, int>>();

	public GachaRookieRewardWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIGachaRookieRewardWindow.CreateInstance();
		base.OnInit();
	}

	private async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			Show();
		}
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIGachaRookieRewardWindow uIGachaRookieRewardWindow)
		{
			uIGachaRookieRewardWindow.btn_Confirm.onClick.Add(OnClickReceive);
			uIGachaRookieRewardWindow.btn_Cancel.onClick.Add(base.Hide);
			uIGachaRookieRewardWindow.mohu.onClick.Add(base.Hide);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIGachaRookieRewardWindow uIGachaRookieRewardWindow)
		{
			uIGachaRookieRewardWindow.btn_Confirm.onClick.Remove(OnClickReceive);
			uIGachaRookieRewardWindow.btn_Cancel.onClick.Remove(base.Hide);
			uIGachaRookieRewardWindow.mohu.onClick.Remove(base.Hide);
			SelectedItemId = 0;
			PoolId = 0;
			ProgressItem = null;
		}
	}

	public void OnClickReceive()
	{
		GComponent gComponent = base.contentPane;
		UIGachaRookieRewardWindow win = gComponent as UIGachaRookieRewardWindow;
		if (win == null || !StaticConfigure.Gacha.ProgressDict.TryGetValue(PoolId, out var value))
		{
			return;
		}
		GachaPoolProgress poolProgress = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolProgress(PoolId);
		if (poolProgress == null)
		{
			return;
		}
		RepeatedField<GachaProgressConfigureItem> gachaProgressConfigureItems = value.GachaProgressConfigureItems;
		if (gachaProgressConfigureItems != null && gachaProgressConfigureItems.Count != 0 && poolProgress.Progress >= ProgressItem.Count && !poolProgress.IsFinishRewardByProgress(ProgressItem.Count))
		{
			win.btn_Confirm.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestRookieGachaRewardC2S(PoolId, SelectedItemId).OnFinishedOnly.AddOnce(delegate
			{
				win.btn_Confirm.onClick.Release();
				Hide();
			});
		}
	}

	public async UniTask ShowGachaRookieReward(int poolId)
	{
		if (!StaticConfigure.Gacha.ProgressDict.TryGetValue(poolId, out var value))
		{
			return;
		}
		GachaPoolProgress progressData = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetPoolProgress(poolId);
		if (progressData == null)
		{
			return;
		}
		RepeatedField<GachaProgressConfigureItem> gachaProgressConfigureItems = value.GachaProgressConfigureItems;
		if (gachaProgressConfigureItems == null || gachaProgressConfigureItems.Count == 0)
		{
			return;
		}
		ProgressItem = gachaProgressConfigureItems[0];
		PoolId = poolId;
		await TryShowAsync();
		if (!(base.contentPane is UIGachaRookieRewardWindow uIGachaRookieRewardWindow))
		{
			return;
		}
		uIGachaRookieRewardWindow.btn_Confirm.isReceive.selectedIndex = (progressData.IsFinishRewardByProgress(progressData.Progress) ? 2 : ((progressData.Progress < ProgressItem.Count) ? 1 : 0));
		uIGachaRookieRewardWindow.btn_Confirm.txt_progress.SetVar("curCount", progressData.Progress.ToString());
		uIGachaRookieRewardWindow.btn_Confirm.txt_progress.SetVar("maxCount", ProgressItem.Count.ToString());
		uIGachaRookieRewardWindow.btn_Confirm.txt_progress.FlushVars();
		RewardItemList.Clear();
		foreach (KeyValuePair<int, int> item in ProgressItem.Reward)
		{
			RewardItemList.Add(item);
		}
		SelectedItemId = RewardItemList[0].Key;
		RefreshBoxList();
	}

	private void RefreshBoxList()
	{
		if (ProgressItem != null && base.contentPane is UIGachaRookieRewardWindow uIGachaRookieRewardWindow)
		{
			uIGachaRookieRewardWindow.heroList.itemRenderer = RendererListItem;
			uIGachaRookieRewardWindow.heroList.numItems = RewardItemList.Count;
		}
	}

	private void RendererListItem(int index, GObject item)
	{
		if (!(item is UICom_Item uICom_Item))
		{
			return;
		}
		ItemInfoConfigure itemConfig = RewardItemList[index].Key.GetItemInfoConfigure();
		uICom_Item.qualityType.selectedIndex = (int)itemConfig.QualityType;
		uICom_Item.loader_Icon.url = itemConfig.ShowIcon;
		uICom_Item.txt_itemNum.text = RewardItemList[index].Value.ToString();
		uICom_Item.isOwn.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(itemConfig.Id) ? 1 : 0);
		uICom_Item.isSelected.selectedIndex = ((itemConfig.Id == SelectedItemId) ? 1 : 0);
		uICom_Item.onClick.Set((EventCallback0)delegate
		{
			if (SelectedItemId != itemConfig.Id)
			{
				SelectedItemId = itemConfig.Id;
				RefreshBoxList();
			}
		});
	}
}
