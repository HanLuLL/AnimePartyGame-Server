using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityStoreTwoPanel : BasePanel<UIActivityStoreTwoPanel>
{
	private ActivityInfoConfigure _currentActivityStoreConfig;

	private List<int> curActivityStoreTaskIds;

	private TaskActivityData curTActivityStoreData;

	private ShopTypeData shopTypeData;

	private List<BaseGoodsData> GoodsDatas;

	public ActivityStoreTwoPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityStoreTwoPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		int num = ((objs != null && objs.Length != 0) ? ((RepeatedField<int>)objs[0])[0] : ((_currentActivityStoreConfig == null) ? StaticConfigure.Activity.Infos[0].Id : _currentActivityStoreConfig.Id));
		if (!StaticConfigure.Activity.InfoDict.TryGetValue(num, out _currentActivityStoreConfig))
		{
			Debug.LogError($"获取活动id:{num}错误!");
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		if (base.ui.Loader_BG != null)
		{
			RepeatedField<string> bgList = _currentActivityStoreConfig.BgList;
			if (bgList != null && bgList.Count > 0 && !string.IsNullOrEmpty(bgList[0]))
			{
				SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(bgList[0], delegate(NTexture texture)
				{
					base.ui.Loader_BG.Background(texture);
				}, null).Forget();
			}
		}
		RefreshActivityStore(_currentActivityStoreConfig.Id);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.Dispatch(_currentActivityStoreConfig.CurrencyBar);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.com_Task.list_Taskstype1.SetVirtual();
		base.ui.com_Task.list_Taskstype1.itemRenderer = RendererActivityStoreTask;
		base.ui.com_Store.list_Store_Goods.SetVirtual();
		base.ui.com_Store.list_Store_Goods.itemRenderer = RendererActivityStoreGoods;
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.com_Task.btn_Preview.onClick.Add(PreviewSkin);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.com_Task.btn_Preview.onClick.Remove(PreviewSkin);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshActivityStoreShopData);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(RefreshActivityStoreShopData);
	}

	public override void Close()
	{
		CloseForStore();
		base.Close();
	}

	private void CloseForStore()
	{
		if (SimpleSingletonProvider<UIManager>.inst.gachaInfo.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.gachaInfo.Hide();
		}
	}

	private void RefreshActivityStore(int activityId)
	{
		base.ui.tab.selectedIndex = 1;
		curTActivityStoreData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityId);
		if (curTActivityStoreData == null)
		{
			Debug.LogError($"活动数据为空，请检查活动数据；活动ID: {activityId}");
			return;
		}
		base.ui.ActivityStore_btn_act.onClick.Set((EventCallback0)delegate
		{
			BringToggleToFront(base.ui.ActivityStore_btn_act, base.ui.ActivityStore_btn_str);
			base.ui.tab.selectedIndex = 0;
			RefreshActivityStoreTask();
		});
		base.ui.ActivityStore_btn_str.onClick.Set((EventCallback0)delegate
		{
			BringToggleToFront(base.ui.ActivityStore_btn_str, base.ui.ActivityStore_btn_act);
			base.ui.tab.selectedIndex = 1;
			RefreshActivityStore_Store();
		});
		base.ui.ActivityStore_btn_act.onClick.Call();
	}

	private void BringToggleToFront(GObject active, GObject inactive)
	{
		if (active != null && inactive != null)
		{
			GComponent parent = active.parent;
			if (parent != null && inactive.parent == parent)
			{
				parent.SetChildIndex(active, parent.numChildren - 1);
				parent.SetChildIndex(inactive, 1);
			}
		}
	}

	private void RefreshActivityStore_Store()
	{
		if (curTActivityStoreData != null)
		{
			ExchangeStoreInfoConfigure exchangeStoreInfoConfigure = ((int)curTActivityStoreData.activityConfig.ShopTabType).GetExchangeStoreInfoConfigure();
			shopTypeData = new ShopTypeData
			{
				tabType = exchangeStoreInfoConfigure.ShopTabType,
				tabName = exchangeStoreInfoConfigure.NameID.GetLocal(UIStringType.ExchangeStore),
				currencyBar = exchangeStoreInfoConfigure.CurrencyBar,
				beginTime = exchangeStoreInfoConfigure.BeginTime,
				endTime = exchangeStoreInfoConfigure.EndTime
			};
			GoodsDatas = SimpleSingletonProvider<GameLogicManager>.inst.store.GetGoodsByShopType(exchangeStoreInfoConfigure.ShopTabType);
			GoodsDatas.Sort(ToCompare);
			base.ui.com_Store.list_Store_Goods.numItems = GoodsDatas.Count;
			string tabTime = shopTypeData.GetTabTime();
			if (string.IsNullOrEmpty(tabTime))
			{
				base.ui.com_Store.txt_timeTip.visible = false;
				return;
			}
			base.ui.com_Store.visible = true;
			base.ui.com_Store.txt_timeTip.text = tabTime;
		}
	}

	private int ToCompare(BaseGoodsData x, BaseGoodsData y)
	{
		if (x.SellOut().CompareTo(y.SellOut()) != 0)
		{
			return x.SellOut().CompareTo(y.SellOut());
		}
		if (x.IsOwn().CompareTo(y.IsOwn()) != 0)
		{
			return x.IsOwn().CompareTo(y.IsOwn());
		}
		return x.goodsOrder.CompareTo(y.goodsOrder);
	}

	private void RefreshActivityStoreTask()
	{
		if (curTActivityStoreData == null)
		{
			return;
		}
		string url = (GameSettings.angelMode ? curTActivityStoreData.activityConfig.ActivityImageSFW : curTActivityStoreData.activityConfig.ActivityImage);
		RefreshStandingPainting(url, base.ui.com_Task.loader_Task_Character);
		bool flag = false;
		foreach (KeyValuePair<int, BaseTaskData> item in curTActivityStoreData.taskDataDict)
		{
			if (item.Value.ValidityTime())
			{
				flag = true;
				string durationText = TimeHelper.GetDurationText(item.Value.BeginTime, item.Value.EndTime);
				base.ui.com_Task.txt_Task_Timetype1.text = durationText;
				break;
			}
		}
		if (flag)
		{
			RefreshTaskList();
			base.ui.com_Task.list_Taskstype1.scrollPane.percY = 0f;
			base.ui.com_Task.taskValid.selectedIndex = 0;
		}
		else
		{
			base.ui.com_Task.taskValid.selectedIndex = 1;
		}
		RepeatedField<string> titleImages = curTActivityStoreData.activityConfig.TitleImages;
		base.ui.com_Task.loader_Title.url = GameSettings.GetDataForLanguage(titleImages[1], titleImages[2], titleImages[0], titleImages[3]);
		bool visible = false;
		int superReward = curTActivityStoreData.activityConfig.SuperReward;
		if (superReward != 0)
		{
			ItemInfoConfigure itemInfoConfigure = superReward.GetItemInfoConfigure();
			if (itemInfoConfigure != null)
			{
				visible = itemInfoConfigure.ItemType == ItemType.HeroStandingPainting;
			}
		}
		base.ui.com_Task.btn_Preview.visible = visible;
	}

	private void RefreshTaskList()
	{
		bool overView = false;
		curActivityStoreTaskIds = curTActivityStoreData.GetTaskId(overView);
		base.ui.com_Task.list_Taskstype1.numItems = curActivityStoreTaskIds.Count;
		base.ui.com_Task.list_Taskstype1.RefreshVirtualList();
	}

	private void RefreshStandingPainting(string url, GLoader loader)
	{
		loader.url = url;
	}

	private void RendererActivityStoreGoods(int index, GObject item)
	{
		if (item is UIButton_GoodsItem uIButton_GoodsItem)
		{
			if (index < GoodsDatas.Count)
			{
				uIButton_GoodsItem.isEmpty.selectedIndex = 0;
				uIButton_GoodsItem.InitData((int)shopTypeData.tabType, GoodsDatas[index]);
			}
			else
			{
				uIButton_GoodsItem.grayed = false;
				uIButton_GoodsItem.touchable = false;
				uIButton_GoodsItem.isEmpty.selectedIndex = 1;
			}
		}
	}

	private void RendererActivityStoreTask(int index, GObject item)
	{
		if (curActivityStoreTaskIds != null && item is UIActivityStoretypeTwo_Com_Label label)
		{
			RefreshActivityStoreTaskLabel_type1(index, label);
		}
	}

	private void RefreshActivityStoreTaskLabel_type1(int index, UIActivityStoretypeTwo_Com_Label label)
	{
		BaseTaskData taskData = curTActivityStoreData.taskDataDict[curActivityStoreTaskIds[index]];
		KeyValuePair<int, int> keyValuePair = taskData.rewards[0];
		CommonUIManager.RendererLitItem((UICom_LitItem)label.btn_Item, keyValuePair.Key, keyValuePair.Value);
		bool flag = IsTaskRunning(taskData);
		bool canClaim = CanClaimTask(taskData);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!flag) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!flag) ? 1 : 0);
		label.txt_RefreshType.visible = taskData.GetTaskRefreshType() != TaskRefreshType.None;
		label.txt_RefreshType.text = taskData.GetTaskRefreshTypeLocal();
		label.txt_taskTitle.text = taskData.GetTaskDesc();
		RefreshBar(label.bar_task, taskData.TaskTarget, taskData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (canClaim)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(curTActivityStoreData.activityConfig.Id, taskData).OnFinishedOnly.AddOnce(delegate
				{
					RefreshTaskList();
					SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.activityStatus.Dispatch(curTActivityStoreData.activityConfig.Id);
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
		label.group_Go.visible = !taskData._FinishStatus && flag && taskData.GetWay() != 0;
		label.btn_GoWay.onClick.Set((EventCallback0)delegate
		{
			GoTargetPanelAsync().Forget();
		});
		async UniTask GoTargetPanelAsync()
		{
			label.btn_GoWay.onClick.Retain();
			await GoTargetPanel(taskData);
			label.btn_GoWay.onClick.Release();
		}
	}

	private async UniTask GoTargetPanel(BaseTaskData taskData)
	{
		int way = taskData.GetWay();
		if (way != 0 && taskData.TaskRunning && !taskData._FinishStatus)
		{
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(way);
		}
	}

	private bool CanClaimTask(BaseTaskData taskData)
	{
		if (taskData._FinishStatus)
		{
			return false;
		}
		if (taskData is MissionData missionData)
		{
			bool num = missionData.AchieveProgress >= missionData.TaskTarget;
			bool flag = missionData.Status == 2;
			return num || flag;
		}
		return !taskData.TaskRunning;
	}

	private void RefreshBar(GProgressBar bar_task, int configParam, int taskDataProgress)
	{
		bar_task.max = configParam;
		bar_task.min = 0.0;
		bar_task.value = Mathf.Min(taskDataProgress, configParam);
	}

	private bool IsTaskRunning(BaseTaskData taskData)
	{
		if (taskData._FinishStatus)
		{
			return false;
		}
		if (taskData is MissionData missionData)
		{
			if (missionData.AchieveProgress >= missionData.TaskTarget)
			{
				return false;
			}
			return missionData.Status != 2;
		}
		return taskData.TaskRunning;
	}

	private void RefreshActivityStoreShopData(int obj)
	{
		base.ui.com_Store.list_Store_Goods.touchable = false;
		RefreshActivityStore_Store();
		base.ui.com_Store.list_Store_Goods.touchable = true;
	}

	private void PreviewSkin()
	{
		base.ui.com_Task.btn_Preview.onClick.Retain();
		int superReward = curTActivityStoreData.activityConfig.SuperReward;
		if (superReward != 0)
		{
			SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(superReward, 0).Forget();
		}
		base.ui.com_Task.btn_Preview.onClick.Release();
	}
}
