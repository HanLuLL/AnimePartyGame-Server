using System.Collections;
using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityVA11HallAPanel : BasePanel<UIActivityVA11HallAPanel>
{
	private const int BankId = 24;

	private const int EventId = 198;

	private List<int> curActivityTaskIds;

	private TaskActivityData curVA11HallAActivityData;

	private ActivityInfoConfigure _currentActivityConfig;

	private UIActivityVA11HallA_Button_Tab _SelectTab;

	private CoroutineManager.CoroutineState animationCoroutine;

	private ActivityVA11HallATab btn_Task;

	private int TaskTargetCount;

	private ShopTypeData shopTypeData;

	private List<BaseGoodsData> GoodsDatas;

	private int ActivityPropId
	{
		get
		{
			if (curVA11HallAActivityData == null)
			{
				return 0;
			}
			return curVA11HallAActivityData.activityConfig.TaskTargetID;
		}
	}

	public ActivityVA11HallAPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityVA11HallAPanel.CreateInstance();
		animationCoroutine = MonoSingletonProvider<CoroutineManager>.inst.CreateCoroutine(PlayRandomAnimationLoop());
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		int num = ((objs != null && objs.Length != 0) ? ((RepeatedField<int>)objs[0])[0] : ((_currentActivityConfig == null) ? StaticConfigure.Activity.Infos[0].Id : _currentActivityConfig.Id));
		if (!StaticConfigure.Activity.InfoDict.TryGetValue(num, out _currentActivityConfig))
		{
			Debug.LogError($"获取活动id:{num}错误!");
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		RefreshActivity(_currentActivityConfig.Id);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.com_Task.list_Tasks.SetVirtual();
		base.ui.com_Task.list_Tasks.itemRenderer = RendererActivityTask;
		base.ui.com_Store.list_Store_Goods.SetVirtual();
		base.ui.com_Store.list_Store_Goods.itemRenderer = RendererActivityGoods;
		base.ui.btn_Return.title = 2024111.GetLocal(UIStringType.Collaboration);
		base.ui.com_Task.txt_Title.text = 2024112.GetLocal(UIStringType.Collaboration);
		base.ui.com_Task.txt_AwardDesc.text = 2024113.GetLocal(UIStringType.Collaboration);
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.com_Task.btn_TaskToggle.onClick.Add(TaskToggle_);
		base.ui.btn_Return.onClick.Add(OnReturnToLastPanel);
		if (animationCoroutine != null)
		{
			animationCoroutine.Start();
		}
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.com_Task.btn_TaskToggle.onClick.Remove(TaskToggle_);
		base.ui.btn_Return.onClick.Remove(OnReturnToLastPanel);
		if (animationCoroutine != null)
		{
			animationCoroutine.Stop();
		}
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.AddListener(RefreshActivityShopData);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.signal.refreshCurShelf.RemoveListener(RefreshActivityShopData);
	}

	public override void Close()
	{
		base.ui.com_Store.list_Store_Goods.numItems = 0;
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void OnReturnToLastPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		_currentActivityConfig = null;
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}

	private IEnumerator PlayRandomAnimationLoop()
	{
		while (!base.ui.isDisposed)
		{
			yield return new WaitForSeconds(3f);
			int selectedIndex = UnityEngine.Random.Range(0, 11);
			base.ui.com_Task.com_Animation.ShowMovie.selectedIndex = selectedIndex;
		}
	}

	private void GoTargetPanel(BaseTaskData taskData)
	{
		if (taskData.GetWay() != 0 && taskData.TaskRunning && !taskData._FinishStatus)
		{
			SimpleSingletonProvider<UIManager>.inst.GoWayPanel(taskData.GetWay()).Forget();
		}
	}

	private void RefreshActivity(int activityId)
	{
		base.ui.com_Task.btn_TaskToggle.selected = true;
		base.ui.com_Task.btn_TaskToggle.visible = false;
		base.ui.Loader_BG.Background(base.ui.Loader_BG.url);
		curVA11HallAActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityId);
		List<ActivityVA11HallATab> tabs = new List<ActivityVA11HallATab>(3);
		btn_Task = new ActivityVA11HallATab
		{
			TitleId = 20,
			SelectedIndex = 0
		};
		tabs.Add(btn_Task);
		tabs.Add(new ActivityVA11HallATab
		{
			TitleId = 40,
			SelectedIndex = 1
		});
		tabs.Add(new ActivityVA11HallATab
		{
			TitleId = 50,
			SelectedIndex = 2
		});
		base.ui.list_Tabs.itemRenderer = delegate(int index, GObject item)
		{
			if (index < tabs.Count)
			{
				UIActivityVA11HallA_Button_Tab btn_tab = item as UIActivityVA11HallA_Button_Tab;
				if (btn_tab != null)
				{
					ActivityVA11HallATab tabData = tabs[index];
					btn_tab.title = tabData.TitleId.GetLocal(UIStringType.Activity);
					btn_tab.selected = false;
					tabData.Entity = btn_tab;
					tabData.Entity.onClick.Set((EventCallback0)delegate
					{
						tabData.Entity.onClick.Retain();
						if (_SelectTab != null)
						{
							_SelectTab.selected = false;
						}
						btn_tab.selected = true;
						_SelectTab = btn_tab;
						base.ui.tab.selectedIndex = tabData.SelectedIndex;
						RefreshActivityData(tabData);
						tabData.Entity.onClick.Release();
					});
				}
			}
		};
		base.ui.list_Tabs.numItems = tabs.Count;
		UpdateActivityRedStatus();
		tabs[0].Entity.onClick.Call();
	}

	private void RefreshActivityData(ActivityVA11HallATab tabData)
	{
		if (base.ui.tab.selectedIndex == 0)
		{
			RefreshActivityTask();
		}
		else if (base.ui.tab.selectedIndex == 1)
		{
			RefreshActivityStore();
		}
		else if (base.ui.tab.selectedIndex == 2)
		{
			CollaborationInfoConfigure collaborationInfoConfigure = SimpleSingletonProvider<GameLogicManager>.inst.collaborate.TryGetCollaboration();
			if (collaborationInfoConfigure != null)
			{
				SimpleSingletonProvider<UIManager>.inst.VA11HallA.ShowVA11HallASkin(collaborationInfoConfigure).Forget();
			}
			if (btn_Task != null)
			{
				btn_Task.Entity.onClick.Call();
			}
		}
	}

	private void RefreshActivityTask()
	{
		if (curVA11HallAActivityData == null)
		{
			return;
		}
		string url = (GameSettings.angelMode ? curVA11HallAActivityData.activityConfig.ActivityImageSFW : curVA11HallAActivityData.activityConfig.ActivityImage);
		RefreshStandingPainting(url, base.ui.com_Task.loader_Character);
		bool flag = false;
		foreach (KeyValuePair<int, BaseTaskData> item in curVA11HallAActivityData.taskDataDict)
		{
			if (item.Value.ValidityTime())
			{
				flag = true;
				base.ui.com_Task.txt_Task_Time.text = TimeHelper.GetDurationText(item.Value.BeginTime, item.Value.EndTime);
				break;
			}
		}
		if (flag)
		{
			TaskTargetCount = 0;
			RefreshTaskList_();
			base.ui.com_Task.list_Tasks.scrollPane.percY = 0f;
			base.ui.com_Task.loader_ActivityProp.url = ActivityPropId.GetItemInfoConfigure().ShowIcon;
		}
	}

	private void RefreshStandingPainting(string url, GLoader loader)
	{
		Vector2 customOffset = Vector2.zero;
		if (SimpleSingletonProvider<GameLogicManager>.inst.heroCard.heroSkinOffsetDict.TryGetValue(url, out var value))
		{
			customOffset = value.skinOffset;
		}
		loader.customOffset = customOffset;
		loader.url = url;
	}

	private void RendererActivityTask(int index, GObject item)
	{
		if (curActivityTaskIds == null)
		{
			return;
		}
		UIActivityVA11HallA_Com_Label label = item as UIActivityVA11HallA_Com_Label;
		if (label == null)
		{
			return;
		}
		BaseTaskData taskData = curVA11HallAActivityData.taskDataDict[curActivityTaskIds[index]];
		KeyValuePair<int, int> keyValuePair = taskData.rewards[0];
		ItemInfoConfigure itemInfoConfigure = keyValuePair.Key.GetItemInfoConfigure();
		label.btn_Item.url = itemInfoConfigure.ShowIcon;
		label.txt_Count.text = $"x{keyValuePair.Value}";
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.txt_RefreshType.visible = taskData.GetTaskRefreshType() != TaskRefreshType.None;
		label.txt_RefreshType.text = taskData.GetTaskRefreshTypeLocal();
		label.txt_taskTitle.text = taskData.GetTaskDesc();
		label.txt_Progress.text = $"{Mathf.Min(taskData._Progress, taskData.TaskTarget)}/{taskData.TaskTarget}";
		if (taskData._FinishStatus && keyValuePair.Key == ActivityPropId)
		{
			TaskTargetCount += keyValuePair.Value;
		}
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(curVA11HallAActivityData.activityConfig.Id, taskData).OnFinishedOnly.AddOnce(delegate
				{
					TaskTargetCount = 0;
					RefreshTaskList_();
					UpdateActivityRedStatus();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
		label.btn_GoWay.visible = !taskData._FinishStatus && taskData.TaskRunning && taskData.GetWay() != 0;
		label.btn_GoWay.onClick.Set((EventCallback0)delegate
		{
			GoTargetPanel(taskData);
		});
		label.btn_taskStatus.txt_Finish.text = 2024118.GetLocal(UIStringType.Collaboration);
		label.btn_taskStatus.txt_Ok.text = 2024115.GetLocal(UIStringType.Collaboration);
		label.btn_taskStatus.txt_Running.text = 2024114.GetLocal(UIStringType.Collaboration);
	}

	private void UpdateActivityRedStatus()
	{
		if (btn_Task?.Entity != null)
		{
			btn_Task.Entity.redStatus.selectedIndex = (curVA11HallAActivityData.GetActivityStatus() ? 1 : 0);
		}
	}

	private void RefreshTaskList_()
	{
		bool selected = base.ui.com_Task.btn_TaskToggle.selected;
		curActivityTaskIds = curVA11HallAActivityData.GetTaskId(selected);
		base.ui.com_Task.list_Tasks.numItems = curActivityTaskIds.Count;
		int num = 0;
		foreach (KeyValuePair<int, BaseTaskData> item in curVA11HallAActivityData.taskDataDict)
		{
			item.Deconstruct(out var _, out var value);
			BaseTaskData baseTaskData = value;
			KeyValuePair<int, int> keyValuePair = baseTaskData.rewards[0];
			if (baseTaskData._FinishStatus && keyValuePair.Key == ActivityPropId)
			{
				num += keyValuePair.Value;
			}
		}
		base.ui.com_Task.txt_ActivityPropCount.text = $"{num}/{curVA11HallAActivityData.activityConfig.TaskTargetNumb}";
	}

	private void TaskToggle_()
	{
		base.ui.com_Task.btn_TaskToggle.onClick.Retain();
		RefreshTaskList_();
		base.ui.com_Task.btn_TaskToggle.onClick.Release();
	}

	private void RefreshActivityStore()
	{
		if (curVA11HallAActivityData != null)
		{
			ExchangeStoreInfoConfigure exchangeStoreInfoConfigure = ((int)curVA11HallAActivityData.activityConfig.ShopTabType).GetExchangeStoreInfoConfigure();
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

	private void RendererActivityGoods(int index, GObject item)
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

	private void RefreshActivityShopData(int ShopTab)
	{
		base.ui.com_Store.list_Store_Goods.touchable = false;
		RefreshActivityStore();
		base.ui.com_Store.list_Store_Goods.touchable = true;
	}
}
