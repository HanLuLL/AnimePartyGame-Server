using FairyGUI;
using GameLogic;
using GameLogic.Data;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class ActivityLevelPassPanel : BasePanel<UIActivityLevelPassPanel>
{
	private ActivityPassData activityPassData;

	public ActivityLevelPassPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityLevelPassPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		int activityId = 0;
		if (objs == null || objs.Length == 0)
		{
			if (activityPassData == null)
			{
				return;
			}
			activityId = activityPassData.ActivityId;
		}
		else if (objs[0] is RepeatedField<int>)
		{
			activityId = (objs[0] as RepeatedField<int>)[0];
		}
		SimpleSingletonProvider<GameLogicManager>.inst.activityPass.ChangePass(SimpleSingletonProvider<GameLogicManager>.inst.activityPass.GetPassIdByActivityId(activityId));
		ActivityPassLogic activityPass = SimpleSingletonProvider<GameLogicManager>.inst.activityPass;
		activityPassData = activityPass.ActivityPassData;
		RechargeGoods premiumGoods = activityPassData.GetPremiumGoods(ShopTabType.BattlePassLevel);
		base.ui.btn_buy.txt_price.text = premiumGoods?.GetDiscountPriceText();
		base.ui.btn_buy.txt_price.AddCurrencySymbols(base.ui.btn_buy.txt_price.text);
		base.ui.passItemList.itemRenderer = RendererPassItem;
		base.ui.passItemList.numItems = activityPassData.PassItemData.Count;
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		base.ui.btn_buy.enabled = activityPassData.PassGear <= 1;
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.PlayerLabelController.Dispatch(t: false);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.bg.MallScreen();
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.activityPass.signal.updateGear.AddListener(OnUpdateGear);
		SimpleSingletonProvider<GameLogicManager>.inst.activityPass.signal.updateTask.AddListener(OnUpdateTask);
		base.ui.btn_buy.onClick.Set(OnRequestPurchasePremiumPass);
	}

	protected override void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.activityPass.signal.updateGear.RemoveListener(OnUpdateGear);
		SimpleSingletonProvider<GameLogicManager>.inst.activityPass.signal.updateTask.RemoveListener(OnUpdateTask);
		base.ui.btn_buy.onClick.Remove(OnRequestPurchasePremiumPass);
		base.RemoveListener();
	}

	public override void Close()
	{
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void OnRequestPurchasePremiumPass()
	{
		if (activityPassData.PassGear <= 1)
		{
			RechargeGoods premiumGoods = activityPassData.GetPremiumGoods(ShopTabType.BattlePassLevel);
			base.ui.btn_buy.onClick.Retain();
			await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(premiumGoods, 1);
			base.ui.btn_buy.onClick.Release();
		}
	}

	private void OnUpdateGear()
	{
		base.ui.btn_buy.enabled = activityPassData.PassGear <= 1;
		base.ui.passItemList.numItems = activityPassData.PassItemData.Count;
	}

	private void OnUpdateTask()
	{
		base.ui.passItemList.numItems = activityPassData.PassItemData.Count;
	}

	private void RendererPassItem(int index, GObject _item)
	{
		if (_item is UIActivityLevelPass_ListItem item)
		{
			SetItemData(index, item);
		}
	}

	private void SetItemData(int passItemIndex, UIActivityLevelPass_ListItem item)
	{
		if (activityPassData.PassItemData.TryGetValue(passItemIndex, out var value))
		{
			Controller rewardState = item.rewardState;
			rewardState.selectedIndex = (MissionData.TaskState)value.MissionData.Status switch
			{
				MissionData.TaskState.TaskStateLocked => 0, 
				MissionData.TaskState.TaskStateUnlocked => 0, 
				MissionData.TaskState.TaskStateCompleted => 2, 
				MissionData.TaskState.TaskStateClaimed => 3, 
				MissionData.TaskState.TaskStateOneCompleted => (activityPassData.PassGear <= 1) ? 1 : 2, 
				MissionData.TaskState.TaskStateOneClaimed => 4, 
				_ => 0, 
			};
			if (value.MissionData.TaskRunning)
			{
				item.GetLoop.Stop();
			}
			else
			{
				item.GetLoop.Play(-1, 0f, null);
			}
			item.text_ItemLevel.text = string.Format(1001.GetLocal(UIStringType.GUI), value.PassInfoConfig.Param);
			item.freeItem.isLock.selectedIndex = 1;
			item.paidItem.isLock.selectedIndex = ((activityPassData.PassGear > 1) ? 1 : 0);
			RendererBtnItem(item.freeItem, value.FreeReward.Rewards[0], value.FreeReward.GetRewardType(), isPremium: false);
			RendererBtnItem(item.paidItem, value.PremiumReward.Rewards[0], value.PremiumReward.GetRewardType(), isPremium: true);
		}
	}

	private void RendererBtnItem(UIActivityLevelPass_Button_RewardItem btnItem, ActivityPassItemRewardData item, ActivityPassItemReward.RewardType rewardType, bool isPremium)
	{
		ItemInfoConfigure itemConfig = item.ItemConfig;
		btnItem.txt_ItemFreeNum.text = "x" + item.rewardCount;
		btnItem.loader_Item.url = itemConfig.ShowIcon;
		if (btnItem.isLock.selectedIndex == 0)
		{
			btnItem.isClaimed.selectedIndex = 1;
			btnItem.taskComplete.selectedIndex = 1;
		}
		else
		{
			btnItem.isClaimed.selectedIndex = ((rewardType != ActivityPassItemReward.RewardType.Claimed) ? 1 : 0);
			btnItem.taskComplete.selectedIndex = ((rewardType == ActivityPassItemReward.RewardType.Undone) ? 1 : 0);
		}
		btnItem.onClick.Set((EventCallback0)delegate
		{
			if (rewardType != ActivityPassItemReward.RewardType.Completed || btnItem.isLock.selectedIndex != 1)
			{
				OpenPropDetail(item.ItemConfig.Id, item.rewardCount);
			}
			else
			{
				btnItem.onClick.Retain();
				int id = activityPassData.PassInfoConfig.Id;
				SimpleSingletonProvider<GameLogicManager>.inst.activityPass.RequestReceiveReward(id).OnFinishedOnly.AddOnce(delegate
				{
					btnItem.onClick.Release();
				});
			}
		});
	}

	private async void OpenPropDetail(int itemId, int itemNum)
	{
		await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(itemId, itemNum, _Usable: false);
	}
}
