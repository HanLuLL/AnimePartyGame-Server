using FairyGUI;
using GameLogic;
using GameLogic.Data;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class ActivityMapPassPanel : BasePanel<UIActivityMapPassPanel>
{
	private ActivityPassData activityPassData;

	private int itemPlayAnimCount;

	public ActivityMapPassPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityMapPassPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		int num = 0;
		if (objs == null || objs.Length == 0)
		{
			if (activityPassData == null)
			{
				return;
			}
			num = activityPassData.ActivityId;
		}
		else
		{
			num = (objs[0] as RepeatedField<int>)[0];
		}
		SimpleSingletonProvider<GameLogicManager>.inst.activityPass.ChangePass(SimpleSingletonProvider<GameLogicManager>.inst.activityPass.GetPassIdByActivityId(num));
		ActivityPassLogic activityPass = SimpleSingletonProvider<GameLogicManager>.inst.activityPass;
		activityPassData = activityPass.ActivityPassData;
		RechargeGoods premiumGoods = activityPassData.GetPremiumGoods(ShopTabType.BattlePassMap);
		base.ui.btn_buy.txt_price.text = premiumGoods.GetDiscountPriceText();
		base.ui.btn_buy.txt_price.AddCurrencySymbols(base.ui.btn_buy.txt_price.text);
		itemPlayAnimCount = 0;
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
			RechargeGoods premiumGoods = activityPassData.GetPremiumGoods(ShopTabType.BattlePassMap);
			base.ui.btn_buy.onClick.Retain();
			await SimpleSingletonProvider<GameLogicManager>.inst.store.RequestCreateOrder(premiumGoods, 1);
			base.ui.btn_buy.onClick.Release();
		}
	}

	private void RendererPassItem(int index, GObject _item)
	{
		if (_item is UIActivityMapPass_ListItem item)
		{
			SetItemData(index, item);
		}
	}

	private void SetItemData(int passItemIndex, UIActivityMapPass_ListItem item)
	{
		if (!activityPassData.PassItemData.TryGetValue(passItemIndex, out var value))
		{
			return;
		}
		int mapId = value.MissionTaskConfig.ParamValue[0];
		item.mapInfo.RefreshUI(mapId);
		Controller taskState = item.taskState;
		taskState.selectedIndex = (MissionData.TaskState)value.MissionData.Status switch
		{
			MissionData.TaskState.TaskStateLocked => 0, 
			MissionData.TaskState.TaskStateUnlocked => 0, 
			MissionData.TaskState.TaskStateCompleted => 1, 
			MissionData.TaskState.TaskStateClaimed => 2, 
			MissionData.TaskState.TaskStateOneCompleted => 1, 
			MissionData.TaskState.TaskStateOneClaimed => 2, 
			_ => 0, 
		};
		item.freeItem.isLock.SetSelectedIndex(1);
		item.paidItem.isLock.SetSelectedIndex((activityPassData.PassGear > 1) ? 1 : 0);
		if (passItemIndex >= itemPlayAnimCount)
		{
			float delay = ((passItemIndex < 1) ? 0f : (0.1f + (float)(passItemIndex - 1) * 0.2f));
			item.visible = false;
			item.Cutin.Play(1, delay, delegate
			{
				item.visible = true;
			}, null);
			itemPlayAnimCount++;
		}
		RendererBtnItem(item.freeItem, value.FreeReward.Rewards[0], value.FreeReward.GetRewardType(), isPremium: false);
		RendererBtnItem(item.paidItem, value.PremiumReward.Rewards[0], value.PremiumReward.GetRewardType(), isPremium: true);
	}

	private void RendererBtnItem(UIActivityMapPass_Button_RewardItem btnItem, ActivityPassItemRewardData item, ActivityPassItemReward.RewardType rewardType, bool isPremium)
	{
		ItemInfoConfigure itemConfig = item.ItemConfig;
		btnItem.txt_ItemFreeNum.text = "x" + item.rewardCount;
		btnItem.loader_Item.url = itemConfig.ShowIcon;
		btnItem.isClaimed.selectedIndex = ((rewardType != ActivityPassItemReward.RewardType.Claimed) ? 1 : 0);
		if (isPremium)
		{
			btnItem.rewardState.selectedIndex = 0;
		}
		else
		{
			btnItem.rewardState.selectedIndex = ((rewardType <= ActivityPassItemReward.RewardType.Undone) ? 1 : 2);
		}
		if (rewardType == ActivityPassItemReward.RewardType.Completed && btnItem.isLock.selectedIndex == 1)
		{
			btnItem.GetLoop.Play(-1, 0f, null);
		}
		else
		{
			btnItem.GetLoop.Stop();
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

	private void OnUpdateGear()
	{
		base.ui.btn_buy.enabled = activityPassData.PassGear <= 1;
		base.ui.passItemList.numItems = activityPassData.PassItemData.Count;
	}

	private void OnUpdateTask()
	{
		base.ui.passItemList.numItems = activityPassData.PassItemData.Count;
	}

	private async void OpenPropDetail(int itemId, int itemNum)
	{
		await SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(itemId, itemNum, _Usable: false);
	}
}
