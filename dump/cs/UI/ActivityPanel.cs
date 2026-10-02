using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Audio;
using Core.Net;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityPanel : BasePanel<UIActivityPanel>
{
	private ActivityInfoConfigure _currentActivityConfig;

	private List<int> curActivityTaskIds;

	private readonly List<TaskActivityData> activityList = new List<TaskActivityData>();

	private TaskActivityData curType1ActivityData;

	private ScratchOffActivityData curType2ActivityData;

	private RepeatedField<ActivityScratchoffPoolConfigureItem> scratchOffPool;

	private LightActivityData curType5LightGiftInfo;

	private ActivityInfoConfigure _activityCfg;

	private bool IsType5NewYear;

	private GButton SelectedScratchOffItem;

	private SkinStandingPaintingConfigureItem LightActivity5SkinStandingPaintingConfig;

	private Player playerVideo;

	private UIActivity_Com_Type5_TaiDao CurType5TaiDao => base.ui.com_Type5.com_TaiDao;

	private UIActivity_Com_Type5_Lin CurType5Lin => base.ui.com_Type5.com_Lin;

	private UIActivity_Com_Type5_Scratchoff_TaiDao CurType5Scratchoff_TaiDao => base.ui.com_Type5.com_TaiDao.com_Scratchoff;

	private UIActivity_Com_Type5_ShowSkin_TaiDao CurType5ShowSkin_TaiDao => base.ui.com_Type5.com_TaiDao.com_ShowSkin;

	private UIActivity_Com_Type5_Scratchoff_Lin curType5Scratchoff_Lin => base.ui.com_Type5.com_Lin.com_Scratchoff;

	private UIActivity_Com_Type5_ShowSkin_Lin CurType5ShowSkin_Lin => base.ui.com_Type5.com_Lin.com_ShowSkin;

	public ActivityPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityPanel.CreateInstance();
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
		if (_currentActivityConfig.UiTab == 1)
		{
			RefreshActivityTab();
		}
		else if (_currentActivityConfig.UiTab == 2)
		{
			RefreshActivityType2(_currentActivityConfig.Id);
		}
		else if (_currentActivityConfig.UiTab != 3 && _currentActivityConfig.UiTab == 5)
		{
			RefreshActivityType5(_currentActivityConfig.Id);
		}
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.Dispatch(_currentActivityConfig.CurrencyBar);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		InitComponentsForType1();
		InitComponentsForType2();
		InitComponentsForType5();
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return.onClick.Add(OnReturnToLastPanel);
		base.ui.btn_Return_1.onClick.Add(OnReturnToLastPanel);
		AddEventForType1();
		AddEventForType2();
		AddEventForType5();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return.onClick.Remove(OnReturnToLastPanel);
		base.ui.btn_Return_1.onClick.Remove(OnReturnToLastPanel);
		RemoveEventForType1();
		RemoveEventForType2();
		RemoveEventForType5();
	}

	protected override void AddListener()
	{
		base.AddListener();
		AddListenerForType2();
		AddListenerForType5();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		RemoveListenerForType2();
		RemoveListenerForType5();
	}

	public override void Close()
	{
		CloseForType5();
		base.Close();
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public override void PlayBGM()
	{
		if (_currentActivityConfig != null && _currentActivityConfig.BGMConfigID != 0)
		{
			BGMHelper.TryPlayBGM(_currentActivityConfig.BGMConfigID);
		}
		else
		{
			base.PlayBGM();
		}
	}

	private async void OnReturnToLastPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		if (_currentActivityConfig.UiTab == 2)
		{
			Controller pageType = base.ui.com_Type2.pageType;
			if (pageType.selectedIndex == 0)
			{
				_currentActivityConfig = null;
				await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
			}
			else if (pageType.selectedIndex == 1)
			{
				pageType.selectedIndex = 0;
			}
		}
		else
		{
			_currentActivityConfig = null;
			await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		}
		base.ui.btn_Return.onClick.Release();
	}

	public async UniTask ShowScratchOffResult(int index)
	{
		if (_currentActivityConfig.UiTab == 2)
		{
			await ShowScratchOffType2Result(index);
		}
	}

	private async void GoTargetPanel(BaseTaskData taskData)
	{
		int way = taskData.GetWay();
		if (way != 0 && taskData.TaskRunning && !taskData._FinishStatus)
		{
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(way);
		}
	}

	private void RefreshBar(GProgressBar bar_task, int configParam, int taskDataProgress)
	{
		bar_task.max = configParam;
		bar_task.min = 0.0;
		bar_task.value = Mathf.Min(taskDataProgress, configParam);
	}

	private void InitComponentsForType1()
	{
		base.ui.com_Type1.list_ActivityTab.itemRenderer = RendererActivityTab;
		base.ui.com_Type1.list_ActivityTask.itemRenderer = RendererActivityTask;
	}

	private void AddEventForType1()
	{
		base.ui.com_Type1.list_ActivityTab.onClickItem.Add(ShowActivityTask);
		base.ui.com_Type1.btn_TaskToggle.onClick.Add(TaskToggle_Type1);
	}

	private void RemoveEventForType1()
	{
		base.ui.com_Type1.list_ActivityTab.onClickItem.Remove(ShowActivityTask);
		base.ui.com_Type1.btn_TaskToggle.onClick.Remove(TaskToggle_Type1);
	}

	private void RefreshActivityTab()
	{
		curType1ActivityData = null;
		activityList.Clear();
		foreach (ActivityInfoConfigure info in StaticConfigure.Activity.Infos)
		{
			if (info.UiTab == 1 && info.UiType == UIType.Panel && SimpleSingletonProvider<GameLogicManager>.inst.activity.AdjustActivity(info))
			{
				TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(info.Id);
				activityList.Add(taskActivityData);
			}
		}
		if (activityList.Count == 0)
		{
			base.ui.Status.selectedIndex = 0;
			return;
		}
		base.ui.Status.selectedIndex = 1;
		base.ui.com_Type1.list_ActivityTab.SetVirtual();
		base.ui.com_Type1.list_ActivityTab.numItems = activityList.Count;
		GObject[] children = base.ui.com_Type1.list_ActivityTab.GetChildren();
		for (int i = 0; i < children.Length && children[i] is UICacha_Button_Pool uICacha_Button_Pool; i++)
		{
			if ((int)uICacha_Button_Pool.data == _currentActivityConfig.Id)
			{
				base.ui.com_Type1.list_ActivityTab.GetChildAt(i).onClick.Call();
				return;
			}
		}
		base.ui.com_Type1.list_ActivityTab.GetChildAt(0).onClick.Call();
	}

	private void RendererActivityTab(int index, GObject item)
	{
		if (item is UIActivity_Button_Type1_Tab uIActivity_Button_Type1_Tab)
		{
			ActivityInfoConfigure activityConfig = activityList[index].activityConfig;
			uIActivity_Button_Type1_Tab.data = activityConfig.Id;
			uIActivity_Button_Type1_Tab.title = activityConfig.NameID.GetLocal(UIStringType.Activity);
			uIActivity_Button_Type1_Tab.redStatus.selectedIndex = (activityList[index].GetActivityStatus() ? 1 : 0);
		}
	}

	private void ShowActivityTask(EventContext context)
	{
		int selectedIndex = base.ui.com_Type1.list_ActivityTab.selectedIndex;
		if (curType1ActivityData == null || curType1ActivityData.activityConfig.Id != activityList[selectedIndex].activityConfig.Id)
		{
			base.ui.com_Type1.list_ActivityTab.onClickItem.Retain();
			RefreshActivityDetail(activityList[selectedIndex]);
			base.ui.com_Type1.list_ActivityTab.onClickItem.Release();
		}
	}

	private void RefreshActivityDetail(TaskActivityData _data)
	{
		curType1ActivityData = _data;
		base.ui.com_Type1.loader_Activity.url = curType1ActivityData.activityConfig.ActivityImage;
		base.ui.com_Type1.txt_Title.text = curType1ActivityData.activityConfig.TitleID.GetLocal(UIStringType.Activity);
		base.ui.com_Type1.txt_Desc.text = curType1ActivityData.activityConfig.DescriptionID.GetLocal(UIStringType.Activity);
		base.ui.com_Type1.btn_TaskToggle.selected = true;
		base.ui.com_Type1.btn_TaskToggle.visible = false;
		RefreshActicityTime();
		RefreshActivityTask();
	}

	private void RefreshActicityTime()
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		if ((object)curType1ActivityData.activityConfig.EndTime != null)
		{
			DateTime dateTime = curType1ActivityData.activityConfig.EndTime.ToDateTime();
			if (serverTime > dateTime)
			{
				base.ui.com_Type1.showTime.selectedIndex = 0;
				return;
			}
			base.ui.com_Type1.showTime.selectedIndex = 1;
			base.ui.com_Type1.txt_timeTip.text = TimeHelper.RefreshTimeText(1033, 1034, serverTime, dateTime);
		}
		else
		{
			base.ui.com_Type1.showTime.selectedIndex = 0;
		}
	}

	private void RefreshActivityTask()
	{
		base.ui.com_Type1.list_ActivityTask.numItems = 0;
		if (curType1ActivityData != null)
		{
			bool selected = base.ui.com_Type1.btn_TaskToggle.selected;
			curActivityTaskIds = curType1ActivityData.GetTaskId(selected);
			base.ui.com_Type1.list_ActivityTask.numItems = curActivityTaskIds.Count;
			base.ui.com_Type1.list_ActivityTask.scrollPane.percY = 0f;
		}
	}

	private void TaskToggle_Type1()
	{
		base.ui.com_Type1.btn_TaskToggle.onClick.Retain();
		RefreshActivityTask();
		base.ui.com_Type1.btn_TaskToggle.onClick.Release();
	}

	private void RendererActivityTask(int index, GObject item)
	{
		if (curActivityTaskIds == null)
		{
			return;
		}
		UIActivity_Com_Type1_Label label = item as UIActivity_Com_Type1_Label;
		if (label == null)
		{
			return;
		}
		BaseTaskData taskData = curType1ActivityData.taskDataDict[curActivityTaskIds[index]];
		RendererRewardItem(label.list_reward, taskData.rewards);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!taskData.TaskRunning) ? 1 : 0);
		label.txt_taskDesc.text = taskData.GetTaskDesc();
		label.txt_taskTitle.text = taskData.GetTaskTitle();
		label.txt_timeTip.text = taskData.GetTime();
		label.txt_RefreshType.visible = taskData.GetTaskRefreshType() != TaskRefreshType.None;
		label.txt_RefreshType.text = taskData.GetTaskRefreshTypeLocal();
		RefreshBar(label.bar_task, taskData.TaskTarget, taskData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			GoTargetPanel(taskData);
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(curType1ActivityData.activityConfig.Id, taskData).OnFinishedOnly.AddOnce(delegate
				{
					base.ui.com_Type1.list_ActivityTab.numItems = activityList.Count;
					RefreshActivityTask();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
	}

	private void RendererRewardItem(GList list, List<KeyValuePair<int, int>> rewards)
	{
		list.itemRenderer = delegate(int i, GObject o)
		{
			KeyValuePair<int, int> keyValuePair = rewards[i];
			CommonUIManager.RendererLitItem((UICom_LitItem)o, keyValuePair.Key, keyValuePair.Value);
		};
		list.numItems = rewards.Count;
		list.scrollPane.touchEffect = rewards.Count > 3;
	}

	private void InitComponentsForType2()
	{
		base.ui.com_Type2.com_Task.list_Task.SetVirtual();
		base.ui.com_Type2.com_Task.list_Task.itemRenderer = RendererType2ActivityTask;
		base.ui.com_Type2.com_Task.list_Preview.itemRenderer = RendererType2ActivityTaskReward;
		base.ui.com_Type2.com_Scratchoff.list_Scratchoff.itemRenderer = RendererScratchOff;
		base.ui.com_Type2.com_Scratchoff.list_Preview.itemRenderer = RendererScratchOffPreview;
	}

	private void AddEventForType2()
	{
		base.ui.com_Type2.pageType.onChanged.Add(RefreshData);
		base.ui.com_Type2.com_Task.btn_Game.onClick.Add(OpenScratchOff);
		base.ui.com_Type2.com_Scratchoff.btn_Next.onClick.Add(OnRequestNextScratchOff);
		base.ui.com_Type2.com_Task.btn_TaskToggle.onClick.Add(TaskToggle_Type2);
	}

	private void RemoveEventForType2()
	{
		base.ui.com_Type2.pageType.onChanged.Remove(RefreshData);
		base.ui.com_Type2.com_Task.btn_Game.onClick.Remove(OpenScratchOff);
		base.ui.com_Type2.com_Scratchoff.btn_Next.onClick.Remove(OnRequestNextScratchOff);
		base.ui.com_Type2.com_Task.btn_TaskToggle.onClick.Remove(TaskToggle_Type2);
	}

	private void AddListenerForType2()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.AddListener(UpdateConsume);
	}

	private void RemoveListenerForType2()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.RemoveListener(UpdateConsume);
	}

	private void UpdateConsume()
	{
		if (_currentActivityConfig.UiTab == 2)
		{
			base.ui.com_Type2.com_Task.btn_Game.redPoint.selectedIndex = (curType2ActivityData.ScratchOffDataLicense() ? 1 : 0);
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(curType2ActivityData.consumePropId);
			base.ui.com_Type2.com_Task.btn_Game.txt_Num.text = $"x{itemCount}";
			base.ui.com_Type2.com_Scratchoff.txt_NeedNum.text = $"x{itemCount}";
		}
	}

	private void RefreshActivityType2(int activityId)
	{
		base.ui.Status.selectedIndex = 2;
		curType2ActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetScratchOffActivityData(activityId);
		base.ui.com_Type2.loader_BG.Background(curType2ActivityData.ScratchOffConfig.Bg);
		base.ui.com_Type2.pageType.selectedIndex = 0;
		base.ui.com_Type2.pageType.onChanged.Call();
		UpdateConsume();
	}

	private void RefreshData()
	{
		if (base.ui.com_Type2.pageType.selectedIndex == 0)
		{
			RefreshActivityType2Task();
		}
		else if (base.ui.com_Type2.pageType.selectedIndex == 1)
		{
			RefreshActivityType2Game();
		}
	}

	private void RefreshActivityType2Task()
	{
		if (curType2ActivityData == null)
		{
			return;
		}
		ActivityScratchoffConfigure scratchOffConfig = curType2ActivityData.ScratchOffConfig;
		base.ui.com_Type2.com_Task.loader_Title.url = GetTaskTitle();
		base.ui.com_Type2.com_Task.loader_NPC.url = scratchOffConfig.TaskNPC;
		base.ui.com_Type2.com_Task.txt_Time.text = TimeHelper.GetDurationText(scratchOffConfig.TaskBeginTime, scratchOffConfig.TaskEndTime);
		base.ui.com_Type2.com_Task.btn_TaskToggle.selected = true;
		base.ui.com_Type2.com_Task.btn_TaskToggle.visible = false;
		bool flag = false;
		foreach (KeyValuePair<int, BaseTaskData> item in curType2ActivityData.taskDataDict)
		{
			if (item.Value.ValidityTime())
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			RefreshMainHero();
			RefreshTaskList_Type2();
			base.ui.com_Type2.com_Task.list_Task.scrollPane.percY = 0f;
			base.ui.com_Type2.com_Task.list_Preview.numItems = curType2ActivityData.ScratchOffConfig.Rewards.Count;
			base.ui.com_Type2.com_Task.taskValid.selectedIndex = 0;
		}
		else
		{
			RefreshEndHero();
			base.ui.com_Type2.com_Task.language.selectedIndex = ((GameSettings.languageType != LanguageType.SimplifiedChinese) ? 1 : 0);
			base.ui.com_Type2.com_Task.taskValid.selectedIndex = 1;
		}
		base.ui.com_Type2.com_Task.btn_Game.txt_Progress.text = $"{curType2ActivityData.CurPage}/{curType2ActivityData.MaxPage}";
		base.ui.com_Type2.com_Task.btn_Game.loader_Prop.url = curType2ActivityData.consumePropId.GetItemInfoConfigure().Icon;
	}

	private string GetTaskTitle()
	{
		ActivityScratchoffConfigure scratchOffConfig = curType2ActivityData.ScratchOffConfig;
		return GameSettings.GetDataForLanguage(scratchOffConfig.TitleImages[1], scratchOffConfig.TitleImages[2], scratchOffConfig.TitleImages[0], scratchOffConfig.TitleImages[3]);
	}

	private void RendererType2ActivityTask(int index, GObject item)
	{
		if (curActivityTaskIds == null)
		{
			return;
		}
		UIActivity_Com_Type2_Label label = item as UIActivity_Com_Type2_Label;
		if (label == null)
		{
			return;
		}
		BaseTaskData taskData = curType2ActivityData.taskDataDict[curActivityTaskIds[index]];
		if (!taskData.ValidityTime())
		{
			item.visible = false;
			return;
		}
		item.visible = true;
		KeyValuePair<int, int> keyValuePair = taskData.rewards[0];
		CommonUIManager.RendererLitItem((UICom_LitItem)label.btn_Item, keyValuePair.Key, keyValuePair.Value);
		label.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.btn_taskStatus.Status.selectedIndex = ((!taskData.TaskRunning) ? 1 : 0);
		label.txt_taskTitle.text = taskData.GetTaskTitle();
		RefreshBar(label.bar_task, taskData.TaskTarget, taskData._Progress);
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(curType2ActivityData.activityConfig.Id, taskData).OnFinishedOnly.AddOnce(delegate
				{
					RefreshTaskList_Type2();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
		label.btn_GoWay.visible = taskData.GetWay() != 0;
		label.btn_GoWay.onClick.Set((EventCallback0)delegate
		{
			GoTargetPanel(taskData);
		});
	}

	private void RendererType2ActivityTaskReward(int index, GObject item)
	{
		if (item is UIActivity_Button_Type2_Reward uIActivity_Button_Type2_Reward)
		{
			KeyValuePair<int, int> keyValuePair = curType2ActivityData.ScratchOffConfig.Rewards.ElementAt(index);
			CommonUIManager.RendererLitItem((UICom_LitItem)uIActivity_Button_Type2_Reward.btn_Item, keyValuePair.Key, keyValuePair.Value);
		}
	}

	private void OpenScratchOff()
	{
		base.ui.com_Type2.com_Task.btn_Game.onClick.Retain();
		base.ui.com_Type2.pageType.selectedIndex = 1;
		base.ui.com_Type2.com_Task.btn_Game.onClick.Release();
	}

	private void RefreshTaskList_Type2()
	{
		bool selected = base.ui.com_Type2.com_Task.btn_TaskToggle.selected;
		curActivityTaskIds = curType2ActivityData.GetTaskId(selected);
		base.ui.com_Type2.com_Task.list_Task.numItems = curActivityTaskIds.Count;
	}

	private void TaskToggle_Type2()
	{
		base.ui.com_Type2.com_Task.btn_TaskToggle.onClick.Retain();
		RefreshTaskList_Type2();
		base.ui.com_Type2.com_Task.btn_TaskToggle.onClick.Release();
	}

	private void RefreshActivityType2Game()
	{
		if (curType2ActivityData != null)
		{
			ActivityScratchoffConfigure scratchOffConfig = curType2ActivityData.ScratchOffConfig;
			RefreshScratchoffHero();
			base.ui.com_Type2.com_Scratchoff.loader_NPC.url = scratchOffConfig.ScratchoffNPC;
			base.ui.com_Type2.com_Scratchoff.loader_Title.url = GetTaskTitle();
			base.ui.com_Type2.com_Scratchoff.txt_Time.text = TimeHelper.GetDurationText(scratchOffConfig.BeginTime, scratchOffConfig.EndTime);
			int curPage = curType2ActivityData.CurPage;
			int maxPage = curType2ActivityData.MaxPage;
			if (curPage != maxPage)
			{
				base.ui.com_Type2.com_Scratchoff.btn_Next.visible = true;
				base.ui.com_Type2.com_Scratchoff.btn_Next.txt_Progress.text = $"{curPage}/{maxPage}";
				bool flag = curType2ActivityData.NextPageLicense();
				base.ui.com_Type2.com_Scratchoff.btn_Next.status.selectedIndex = (flag ? 1 : 0);
				base.ui.com_Type2.com_Scratchoff.btn_Next.touchable = flag;
				base.ui.com_Type2.com_Scratchoff.btn_Next.grayed = !flag;
			}
			else
			{
				base.ui.com_Type2.com_Scratchoff.btn_Next.visible = false;
			}
			base.ui.com_Type2.com_Scratchoff.loader_Prop.url = curType2ActivityData.consumePropId.GetItemInfoConfigure().Icon;
			scratchOffPool = curType2ActivityData.PoolData;
			base.ui.com_Type2.com_Scratchoff.list_Scratchoff.numItems = scratchOffPool.Count;
			base.ui.com_Type2.com_Scratchoff.list_Preview.numItems = scratchOffPool.Count;
			base.ui.com_Type2.com_Scratchoff.list_Preview.scrollPane.percX = 0f;
		}
	}

	private void RendererScratchOffPreview(int index, GObject item)
	{
		if (item is UIActivity_Button_Type2_Reward uIActivity_Button_Type2_Reward)
		{
			ActivityScratchoffPoolConfigureItem activityScratchoffPoolConfigureItem = scratchOffPool[index];
			uIActivity_Button_Type2_Reward.status.selectedIndex = (curType2ActivityData.IsFinishItem(activityScratchoffPoolConfigureItem.Index) ? 1 : 0);
			CommonUIManager.RendererLitItem((UICom_LitItem)uIActivity_Button_Type2_Reward.btn_Item, activityScratchoffPoolConfigureItem.ItemID, activityScratchoffPoolConfigureItem.ItemNum);
		}
	}

	private void RendererScratchOff(int index, GObject item)
	{
		UIActivity_Button_Type2_Scratchoff_ListItem _item = item as UIActivity_Button_Type2_Scratchoff_ListItem;
		if (_item == null)
		{
			return;
		}
		if (curType2ActivityData.record.ContainsKey(index))
		{
			ActivityScratchoffPoolConfigureItem recordConfig = curType2ActivityData.GetRecordConfig(index);
			ItemInfoConfigure itemInfoConfigure = recordConfig.ItemID.GetItemInfoConfigure();
			_item.loader_Reward.url = itemInfoConfigure.ShowIcon;
			_item.txt_itemNum.text = recordConfig.ItemNum.ToString();
			_item.Status.selectedIndex = 2;
			return;
		}
		_item.Status.selectedIndex = 0;
		_item.onClick.Set((EventCallback0)delegate
		{
			if (_item.Status.selectedIndex != 1 && _item.Status.selectedIndex != 2)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(curType2ActivityData.consumePropId) < curType2ActivityData.consumeNumber)
				{
					ItemInfoConfigure itemInfoConfigure2 = curType2ActivityData.consumePropId.GetItemInfoConfigure();
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(string.Format(1048.GetLocal(UIStringType.Message), itemInfoConfigure2.NameID.GetLocal(UIStringType.Item)));
				}
				else if (!LocalCache.ActivityIds.Contains(curType2ActivityData.activityConfig.Id))
				{
					SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(1049.GetLocal(UIStringType.Message), curType2ActivityData.consumeNumber), delegate
					{
						if (SimpleSingletonProvider<UIManager>.inst.messageBox.GetDoubleStatus())
						{
							LocalCache.UpdateActivityDoubleStatusCache(curType2ActivityData.activityConfig.Id);
						}
						RequestScratchCardC2S(_item, index);
					}, null, doubleStatus: true).Forget();
				}
				else
				{
					RequestScratchCardC2S(_item, index);
				}
			}
		});
	}

	private void RequestScratchCardC2S(UIActivity_Button_Type2_Scratchoff_ListItem _item, int index)
	{
		_item.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.activity.RequestScratchCardC2S(curType2ActivityData.activityConfig.Id, index).OnFinishedOnly.AddOnce(delegate
		{
			_item.onClick.Release();
			base.ui.com_Type2.com_Scratchoff.list_Preview.numItems = scratchOffPool.Count;
			bool flag = curType2ActivityData.NextPageLicense();
			base.ui.com_Type2.com_Scratchoff.btn_Next.status.selectedIndex = (flag ? 1 : 0);
			base.ui.com_Type2.com_Scratchoff.btn_Next.touchable = flag;
			base.ui.com_Type2.com_Scratchoff.btn_Next.grayed = !flag;
		});
	}

	private async UniTask ShowScratchOffType2Result(int index)
	{
		ActivityScratchoffPoolConfigureItem recordConfig = curType2ActivityData.GetRecordConfig(index);
		UIActivity_Button_Type2_Scratchoff_ListItem _item = base.ui.com_Type2.com_Scratchoff.list_Scratchoff.GetChildAt(index) as UIActivity_Button_Type2_Scratchoff_ListItem;
		ItemInfoConfigure itemInfoConfigure = recordConfig.ItemID.GetItemInfoConfigure();
		_item.loader_Reward.url = itemInfoConfigure.ShowIcon;
		_item.txt_itemNum.text = recordConfig.ItemNum.ToString();
		_item.Status.selectedIndex = 1;
		base.ui.com_Type2.com_Scratchoff.Interaction.SetHook("PlaySound", delegate
		{
			SimpleSingletonProvider<AudioManager>.inst.SendEvent(curType2ActivityData.ScratchOffConfig.ScratchVoice, Stage.inst.gameObject);
		});
		base.ui.com_Type2.com_Scratchoff.Interaction.Play();
		await UniTask.WaitUntil(() => !base.ui.com_Type2.com_Scratchoff.Interaction.playing);
		_item.Status.selectedIndex = 2;
	}

	private void OnRequestNextScratchOff()
	{
		if (!curType2ActivityData.NextPageLicense())
		{
			return;
		}
		base.ui.com_Type2.com_Scratchoff.btn_Next.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.activity.RequestNextScratchCardPoolC2S(curType2ActivityData.activityConfig.Id).OnFinished.AddOnce(delegate(RPCAsyncResult _)
		{
			base.ui.com_Type2.com_Scratchoff.btn_Next.onClick.Release();
			if (_.errId == 0)
			{
				RefreshActivityType2Game();
			}
		});
	}

	private void RefreshMainHero()
	{
		RepeatedField<string> mainHeros = curType2ActivityData.ScratchOffConfig.MainHeros;
		RepeatedField<string> mainHerosSFW = curType2ActivityData.ScratchOffConfig.MainHerosSFW;
		int index = new System.Random().Next(mainHeros.Count);
		base.ui.com_Type2.com_Task.loader_Character.url = (GameSettings.angelMode ? mainHerosSFW[index] : mainHeros[index]);
	}

	private void RefreshEndHero()
	{
		base.ui.com_Type2.com_Task.loader_Character.url = (GameSettings.angelMode ? curType2ActivityData.ScratchOffConfig.EndHeroSFW : curType2ActivityData.ScratchOffConfig.EndHero);
	}

	private void RefreshScratchoffHero()
	{
		RepeatedField<string> scratchoffHeros = curType2ActivityData.ScratchOffConfig.ScratchoffHeros;
		RepeatedField<string> scratchoffHerosSFW = curType2ActivityData.ScratchOffConfig.ScratchoffHerosSFW;
		base.ui.com_Type2.com_Scratchoff.loader_Character_1.url = (GameSettings.angelMode ? scratchoffHerosSFW[0] : scratchoffHeros[0]);
		base.ui.com_Type2.com_Scratchoff.loader_Character_2.url = (GameSettings.angelMode ? scratchoffHerosSFW[1] : scratchoffHeros[1]);
	}

	private void InitComponentsForType5()
	{
		CurType5Scratchoff_TaiDao.list_Scratchoff.itemRenderer = RendererType5ActivityItem_TaiDao;
		CurType5Scratchoff_TaiDao.list_Preview.itemRenderer = RendererType5ActivityPreview_TaiDao;
		curType5Scratchoff_Lin.list_Scratchoff.itemRenderer = RendererType5ActivityItem_Lin;
		curType5Scratchoff_Lin.list_Preview.itemRenderer = RendererType5ActivityPreview_Lin;
		CurType5TaiDao.loader_BG.MallScreen();
		CurType5Lin.loader_BG.MallScreen();
		base.ui.com_Type5.changge.onChanged.Set((EventCallback0)delegate
		{
			IsType5NewYear = base.ui.com_Type5.changge.selectedIndex == 1;
			if (IsType5NewYear)
			{
				RefreshShowSkinType5two();
				RefreshScratchNYOffType5();
			}
			else
			{
				RefreshShowSkinType5();
				RefreshScratchOffType5();
			}
		});
	}

	private void AddEventForType5()
	{
		CurType5Scratchoff_TaiDao.btn_Light.onClick.Add(OnLightItem);
		CurType5Scratchoff_TaiDao.btn_Purchase.onClick.Add(OnPurchaseItem);
		CurType5Scratchoff_TaiDao.btn_DetailInfo.onClick.Add(OnShowRule);
		CurType5ShowSkin_TaiDao.btn_ShowSkin.onClick.Add(OnShowSkin);
		CurType5ShowSkin_TaiDao.btn_ShowVideo.onClick.Add(ShowPlatformVideo);
		CurType5ShowSkin_TaiDao.btn_CloseVideo.onClick.Add(ClosePlatformVideo);
		curType5Scratchoff_Lin.btn_Light.onClick.Add(OnLightItem);
		curType5Scratchoff_Lin.btn_Purchase.onClick.Add(OnPurchaseItem);
		curType5Scratchoff_Lin.btn_DetailInfo.onClick.Add(OnShowRule);
		CurType5ShowSkin_Lin.btn_ShowSkin.onClick.Add(OnShowSkin);
		CurType5ShowSkin_Lin.btn_ShowVideo.onClick.Add(ShowPlatformVideo);
		CurType5ShowSkin_Lin.btn_CloseVideo.onClick.Add(ClosePlatformVideo);
	}

	private void RemoveEventForType5()
	{
		CurType5Scratchoff_TaiDao.btn_Light.onClick.Remove(OnLightItem);
		CurType5Scratchoff_TaiDao.btn_Purchase.onClick.Remove(OnPurchaseItem);
		CurType5Scratchoff_TaiDao.btn_DetailInfo.onClick.Remove(OnShowRule);
		CurType5ShowSkin_TaiDao.btn_ShowSkin.onClick.Remove(OnShowSkin);
		CurType5ShowSkin_TaiDao.btn_ShowVideo.onClick.Remove(ShowPlatformVideo);
		CurType5ShowSkin_TaiDao.btn_CloseVideo.onClick.Remove(ClosePlatformVideo);
		curType5Scratchoff_Lin.btn_Light.onClick.Remove(OnLightItem);
		curType5Scratchoff_Lin.btn_Purchase.onClick.Remove(OnPurchaseItem);
		curType5Scratchoff_Lin.btn_DetailInfo.onClick.Remove(OnShowRule);
		CurType5ShowSkin_Lin.btn_ShowSkin.onClick.Remove(OnShowSkin);
		CurType5ShowSkin_Lin.btn_ShowVideo.onClick.Remove(ShowPlatformVideo);
		CurType5ShowSkin_Lin.btn_CloseVideo.onClick.Remove(ClosePlatformVideo);
	}

	private void AddListenerForType5()
	{
	}

	private void RemoveListenerForType5()
	{
	}

	private void CloseForType5()
	{
		SelectedScratchOffItem = null;
		if (IsType5NewYear)
		{
			GObject[] children = curType5Scratchoff_Lin.list_Scratchoff.GetChildren();
			for (int i = 0; i < children.Length; i++)
			{
				if (children[i] is UIActivity_Button_Type5_Scratchoff_ListItem_Lin uIActivity_Button_Type5_Scratchoff_ListItem_Lin && uIActivity_Button_Type5_Scratchoff_ListItem_Lin.ShowSwitchGold.playing)
				{
					uIActivity_Button_Type5_Scratchoff_ListItem_Lin.ShowSwitchGold.Stop();
				}
			}
		}
		else
		{
			GObject[] children = CurType5Scratchoff_TaiDao.list_Scratchoff.GetChildren();
			for (int i = 0; i < children.Length; i++)
			{
				if (children[i] is UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao && uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao.ShowSwitchGold.playing)
				{
					uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao.ShowSwitchGold.Stop();
				}
			}
		}
		curType5Scratchoff_Lin.list_Scratchoff.numItems = 0;
		CurType5Scratchoff_TaiDao.list_Scratchoff.numItems = 0;
		if (SimpleSingletonProvider<UIManager>.inst.gachaInfo.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.gachaInfo.Hide();
		}
	}

	private void OnLightItem()
	{
		if (SelectedScratchOffItem == null)
		{
			return;
		}
		object data = SelectedScratchOffItem.data;
		if (!(data is int))
		{
			return;
		}
		int selectedIndex = (int)data;
		int id = curType5LightGiftInfo.activityConfig.Id;
		if (selectedIndex == -1 || id == 0)
		{
			return;
		}
		if (IsType5NewYear)
		{
			curType5Scratchoff_Lin.btn_Light.onClick.Retain();
		}
		else
		{
			CurType5Scratchoff_TaiDao.btn_Light.onClick.Retain();
		}
		SimpleSingletonProvider<GameLogicManager>.inst.activity.RequestLightGiftC2S(id, selectedIndex).OnFinished.AddOnce(delegate(RPCAsyncResult _)
		{
			if (IsType5NewYear)
			{
				curType5Scratchoff_Lin.btn_Light.onClick.Release();
			}
			else
			{
				CurType5Scratchoff_TaiDao.btn_Light.onClick.Release();
			}
			if (_.errId == 0)
			{
				SelectedScratchOffItem = null;
				ShowLightResult(selectedIndex);
			}
		});
	}

	private void ShowLightResult(int index)
	{
		GObject childAt = (IsType5NewYear ? curType5Scratchoff_Lin.list_Scratchoff : CurType5Scratchoff_TaiDao.list_Scratchoff).GetChildAt(index);
		ActivityScratchoffPoolConfigureItem lightConfig = curType5LightGiftInfo.GetLightData(index);
		if (IsType5NewYear)
		{
			UIActivity_Button_Type5_Scratchoff_ListItem_Lin _item = childAt as UIActivity_Button_Type5_Scratchoff_ListItem_Lin;
			if (_item == null)
			{
				return;
			}
			ItemInfoConfigure itemInfoConfigure = lightConfig.ItemID.GetItemInfoConfigure();
			CommonUIManager.RendererLitItem((UICom_LitItem)_item.com_Item.btn_Item, lightConfig.ItemID, lightConfig.ItemNum, showCount: false);
			_item.com_Name.txt_Title.TryScrollTextField(itemInfoConfigure.NameID.GetLocal(UIStringType.Item) + $"x{lightConfig.ItemNum}");
			_item.Status.selectedIndex = 1;
			_item.ShowSwitchGold.Play(delegate
			{
				_item.selectedStatus.selectedIndex = 0;
				_item.com_Item.touchable = true;
				if (lightConfig.ItemID == curType5LightGiftInfo.LightInfoConfig.TargetItemID)
				{
					SimpleSingletonProvider<UIManager>.inst.gachaInfo.ShowCharacterVideo(LightActivity5SkinStandingPaintingConfig.GetCharacter().Item1, 406.GetVideoKey(), LightActivity5SkinStandingPaintingConfig.Voice, delegate
					{
						BGMHelper.TryPlayBGM(850);
					}, PlayBGM, showSkipButton: false, showCharacter: false).Forget();
				}
				RefreshButton_Lin();
			});
			return;
		}
		UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao _item2 = childAt as UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao;
		if (_item2 == null)
		{
			return;
		}
		ItemInfoConfigure itemInfoConfigure2 = lightConfig.ItemID.GetItemInfoConfigure();
		CommonUIManager.RendererLitItem((UICom_LitItem)_item2.com_Item.btn_Item, lightConfig.ItemID, lightConfig.ItemNum, showCount: false);
		_item2.com_Name.txt_Title.TryScrollTextField(itemInfoConfigure2.NameID.GetLocal(UIStringType.Item) + $"x{lightConfig.ItemNum}");
		_item2.Status.selectedIndex = 1;
		_item2.ShowSwitchGold.Play(delegate
		{
			_item2.selectedStatus.selectedIndex = 0;
			_item2.com_Item.touchable = true;
			if (lightConfig.ItemID == curType5LightGiftInfo.LightInfoConfig.TargetItemID)
			{
				SimpleSingletonProvider<UIManager>.inst.gachaInfo.ShowCharacterVideo(LightActivity5SkinStandingPaintingConfig.GetCharacter().Item1, 402.GetVideoKey(), LightActivity5SkinStandingPaintingConfig.Voice, null, null, showSkipButton: false, showCharacter: false).Forget();
			}
			RefreshButton_TaiDao();
		});
	}

	private void OnPurchaseItem()
	{
		int selectedIndex = curType5LightGiftInfo.LightIndex;
		int activityId = curType5LightGiftInfo.activityConfig.Id;
		ActivityScratchoffPoolConfigureItem lightData = curType5LightGiftInfo.GetLightData(selectedIndex);
		if (selectedIndex == -1 || activityId == 0 || lightData == null)
		{
			return;
		}
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(curType5LightGiftInfo.consumePropId);
		if (itemCount < curType5LightGiftInfo.consumeNumber)
		{
			if (curType5LightGiftInfo.consumePropId == GameSettings.SPECIAL_ITEM_STARDISC_FREE)
			{
				SimpleSingletonProvider<UIManager>.inst.rechargeTip.TryExchangeToken(curType5LightGiftInfo.consumePropId, curType5LightGiftInfo.consumeNumber - itemCount).Forget();
			}
			return;
		}
		if (IsType5NewYear)
		{
			curType5Scratchoff_Lin.btn_Purchase.onClick.Retain();
		}
		else
		{
			CurType5Scratchoff_TaiDao.btn_Purchase.onClick.Retain();
		}
		SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(1065.GetLocal(UIStringType.Message), lightData.ItemID.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item)), SurePurchaseAction, delegate
		{
			if (IsType5NewYear)
			{
				curType5Scratchoff_Lin.btn_Purchase.onClick.Release();
			}
			else
			{
				CurType5Scratchoff_TaiDao.btn_Purchase.onClick.Release();
			}
		}).Forget();
		void SurePurchaseAction()
		{
			SimpleSingletonProvider<GameLogicManager>.inst.activity.RequestBuyLightGiftC2S(activityId, selectedIndex).OnFinished.AddOnce(delegate(RPCAsyncResult _)
			{
				if (IsType5NewYear)
				{
					curType5Scratchoff_Lin.btn_Purchase.onClick.Release();
				}
				else
				{
					CurType5Scratchoff_TaiDao.btn_Purchase.onClick.Release();
				}
				if (_.errId == 0)
				{
					GObject childAt = (IsType5NewYear ? curType5Scratchoff_Lin.list_Scratchoff : CurType5Scratchoff_TaiDao.list_Scratchoff).GetChildAt(selectedIndex);
					if (IsType5NewYear)
					{
						if (childAt is UIActivity_Button_Type5_Scratchoff_ListItem_Lin uIActivity_Button_Type5_Scratchoff_ListItem_Lin)
						{
							uIActivity_Button_Type5_Scratchoff_ListItem_Lin.Status.selectedIndex = 2;
							uIActivity_Button_Type5_Scratchoff_ListItem_Lin.com_Item.status.selectedIndex = 1;
							uIActivity_Button_Type5_Scratchoff_ListItem_Lin.com_Item.touchable = false;
						}
						curType5Scratchoff_Lin.list_Preview.numItems = curType5LightGiftInfo.PoolData.Count;
						RefreshButton_Lin();
					}
					else
					{
						if (childAt is UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao)
						{
							uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao.Status.selectedIndex = 2;
							uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao.com_Item.status.selectedIndex = 1;
							uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao.com_Item.touchable = false;
						}
						CurType5Scratchoff_TaiDao.list_Preview.numItems = curType5LightGiftInfo.PoolData.Count;
						RefreshButton_TaiDao();
					}
				}
			});
		}
	}

	private void OnShowSkin()
	{
		if (IsType5NewYear)
		{
			CurType5ShowSkin_Lin.btn_ShowSkin.onClick.Retain();
		}
		else
		{
			CurType5ShowSkin_TaiDao.btn_ShowSkin.onClick.Retain();
		}
		SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(curType5LightGiftInfo.LightInfoConfig.SkinID, curType5LightGiftInfo.LightInfoConfig.AccountBackgroundID).Forget();
		if (IsType5NewYear)
		{
			CurType5ShowSkin_Lin.btn_ShowSkin.onClick.Release();
		}
		else
		{
			CurType5ShowSkin_TaiDao.btn_ShowSkin.onClick.Release();
		}
	}

	private void OnShowRule()
	{
		if (IsType5NewYear)
		{
			curType5Scratchoff_Lin.btn_DetailInfo.onClick.Retain();
		}
		else
		{
			CurType5Scratchoff_TaiDao.btn_DetailInfo.onClick.Retain();
		}
		List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
		RepeatedField<ActivityScratchoffPoolConfigureItem> poolData = curType5LightGiftInfo.PoolData;
		int num = 0;
		foreach (ActivityScratchoffPoolConfigureItem item in poolData)
		{
			num += item.Weight;
		}
		foreach (ActivityScratchoffPoolConfigureItem item2 in poolData)
		{
			string key = item2.ItemID.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item) + $"x{item2.ItemNum}";
			string value = ((float)item2.Weight * 1f / (float)num).ToString("P2");
			list.Add(new KeyValuePair<string, string>(key, value));
		}
		string local = curType5LightGiftInfo.LightInfoConfig.RuleTitleID.GetLocal(UIStringType.Activity);
		string local2 = curType5LightGiftInfo.LightInfoConfig.RuleDecriptionID.GetLocal(UIStringType.Activity);
		string local3 = curType5LightGiftInfo.LightInfoConfig.RuleSheetField1.GetLocal(UIStringType.Activity);
		string local4 = curType5LightGiftInfo.LightInfoConfig.RuleSheetField2.GetLocal(UIStringType.Activity);
		SimpleSingletonProvider<UIManager>.inst.rule.TryShowExcel01(local, local2, local3, local4, list).Forget();
		if (IsType5NewYear)
		{
			curType5Scratchoff_Lin.btn_DetailInfo.onClick.Release();
		}
		else
		{
			CurType5Scratchoff_TaiDao.btn_DetailInfo.onClick.Release();
		}
	}

	private void ShowPlatformVideo()
	{
		if (IsType5NewYear)
		{
			CurType5ShowSkin_Lin.btn_ShowVideo.onClick.Retain();
			if (CurType5ShowSkin_Lin.CloseVideo.playing)
			{
				CurType5ShowSkin_Lin.CloseVideo.Stop();
			}
			CurType5ShowSkin_Lin.ShowVideo.Play();
			CurType5ShowSkin_Lin.btn_ShowVideo.onClick.Release();
		}
		else
		{
			CurType5ShowSkin_TaiDao.btn_ShowVideo.onClick.Retain();
			if (CurType5ShowSkin_TaiDao.CloseVideo.playing)
			{
				CurType5ShowSkin_TaiDao.CloseVideo.Stop();
			}
			CurType5ShowSkin_TaiDao.ShowVideo.Play();
			CurType5ShowSkin_TaiDao.btn_ShowVideo.onClick.Release();
		}
	}

	private void ClosePlatformVideo()
	{
		if (IsType5NewYear)
		{
			CurType5ShowSkin_Lin.btn_CloseVideo.onClick.Retain();
			if (CurType5ShowSkin_Lin.ShowVideo.playing)
			{
				CurType5ShowSkin_Lin.ShowVideo.Stop();
			}
			CurType5ShowSkin_Lin.CloseVideo.Play();
			CurType5ShowSkin_Lin.btn_CloseVideo.onClick.Release();
		}
		else
		{
			CurType5ShowSkin_TaiDao.btn_CloseVideo.onClick.Retain();
			if (CurType5ShowSkin_TaiDao.ShowVideo.playing)
			{
				CurType5ShowSkin_TaiDao.ShowVideo.Stop();
			}
			CurType5ShowSkin_TaiDao.CloseVideo.Play();
			CurType5ShowSkin_TaiDao.btn_CloseVideo.onClick.Release();
		}
	}

	private void RefreshActivityType5(int activityId)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.PlayerLabelController.Dispatch(t: false);
		base.ui.Status.selectedIndex = 5;
		curType5LightGiftInfo = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetLightGiftActivityData(activityId);
		StaticConfigure.Activity.InfoDict.TryGetValue(activityId, out _activityCfg);
		base.ui.com_Type5.txt_Time.text = TimeHelper.RefreshTimeText(1033, 1034, MonoSingletonProvider<NetManager>.inst.ServerTime, _activityCfg.EndTime.ToDateTime());
		switch (activityId)
		{
		case 606261:
			base.ui.com_Type5.changge.selectedIndex = 0;
			IsType5NewYear = false;
			RefreshShowSkinType5();
			RefreshScratchOffType5();
			break;
		case 606271:
			base.ui.com_Type5.changge.selectedIndex = 1;
			IsType5NewYear = true;
			RefreshShowSkinType5two();
			RefreshScratchNYOffType5();
			break;
		default:
			base.ui.com_Type5.changge.selectedIndex = 0;
			IsType5NewYear = false;
			RefreshShowSkinType5();
			RefreshScratchOffType5();
			break;
		}
	}

	private void RefreshScratchNYOffType5()
	{
		curType5Scratchoff_Lin.list_Scratchoff.numItems = curType5LightGiftInfo.PoolData.Count;
		curType5Scratchoff_Lin.list_Preview.numItems = curType5LightGiftInfo.PoolData.Count;
		curType5Scratchoff_Lin.btn_Purchase.loader_Token.url = curType5LightGiftInfo.consumePropId.GetItemInfoConfigure().Icon;
		curType5Scratchoff_Lin.btn_Purchase.txt_TokenCount.text = $"x{curType5LightGiftInfo.consumeNumber}";
		curType5Scratchoff_Lin.txt_Desc.text = curType5LightGiftInfo.LightInfoConfig.DecriptionID.GetLocal(UIStringType.Activity);
		curType5Scratchoff_Lin.language.selectedIndex = int.Parse(GameSettings.GetDataForLanguage("1", "2", "0", "1"));
		RefreshButton_Lin();
	}

	private void RefreshScratchOffType5()
	{
		CurType5Scratchoff_TaiDao.list_Scratchoff.numItems = curType5LightGiftInfo.PoolData.Count;
		CurType5Scratchoff_TaiDao.list_Preview.numItems = curType5LightGiftInfo.PoolData.Count;
		CurType5Scratchoff_TaiDao.btn_Purchase.loader_Token.url = curType5LightGiftInfo.consumePropId.GetItemInfoConfigure().Icon;
		CurType5Scratchoff_TaiDao.btn_Purchase.txt_TokenCount.text = $"x{curType5LightGiftInfo.consumeNumber}";
		CurType5Scratchoff_TaiDao.txt_Desc.text = curType5LightGiftInfo.LightInfoConfig.DecriptionID.GetLocal(UIStringType.Activity);
		CurType5Scratchoff_TaiDao.language.selectedIndex = int.Parse(GameSettings.GetDataForLanguage("1", "2", "0", "1"));
		RefreshButton_TaiDao();
	}

	private void RefreshButton_Lin()
	{
		bool flag = curType5LightGiftInfo.LightIndex != -1;
		curType5Scratchoff_Lin.btn_Light.grayed = SelectedScratchOffItem == null;
		curType5Scratchoff_Lin.btn_Light.visible = !flag;
		curType5Scratchoff_Lin.btn_Purchase.visible = flag;
	}

	private void RefreshButton_TaiDao()
	{
		bool flag = curType5LightGiftInfo.LightIndex != -1;
		CurType5Scratchoff_TaiDao.btn_Light.grayed = SelectedScratchOffItem == null;
		CurType5Scratchoff_TaiDao.btn_Light.visible = !flag;
		CurType5Scratchoff_TaiDao.btn_Purchase.visible = flag;
	}

	private void RendererType5ActivityItem_TaiDao(int index, GObject item)
	{
		UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao _item = item as UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao;
		if (_item == null)
		{
			return;
		}
		ActivityScratchoffPoolConfigureItem lightData = curType5LightGiftInfo.GetLightData(index);
		if (lightData == null)
		{
			_item.com_Item.touchable = false;
			_item.Status.selectedIndex = 0;
			_item.com_Name.visible = false;
		}
		else
		{
			ItemInfoConfigure itemInfoConfigure = lightData.ItemID.GetItemInfoConfigure();
			_item.com_Name.txt_Title.TryScrollTextField(itemInfoConfigure.NameID.GetLocal(UIStringType.Item) + $"x{lightData.ItemNum}");
			_item.com_Name.visible = true;
			CommonUIManager.RendererLitItem((UICom_LitItem)_item.com_Item.btn_Item, lightData.ItemID, lightData.ItemNum);
			if (curType5LightGiftInfo.IsFinished(lightData.Index))
			{
				_item.Status.selectedIndex = 2;
				_item.com_Item.status.selectedIndex = 1;
				_item.com_Item.touchable = false;
			}
			else
			{
				_item.Status.selectedIndex = 1;
				_item.com_Item.status.selectedIndex = 0;
				_item.com_Item.touchable = true;
			}
		}
		_item.data = index;
		_item.selectedStatus.selectedIndex = 0;
		_item.onClick.Set((EventCallback0)delegate
		{
			if (curType5LightGiftInfo.LightIndex == -1)
			{
				_item.onClick.Retain();
				if (SelectedScratchOffItem != null && SelectedScratchOffItem != _item)
				{
					if (SelectedScratchOffItem is UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao)
					{
						uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao.selectedStatus.selectedIndex = 0;
					}
					else if (SelectedScratchOffItem is UIActivity_Button_Type5_Scratchoff_ListItem_Lin uIActivity_Button_Type5_Scratchoff_ListItem_Lin)
					{
						uIActivity_Button_Type5_Scratchoff_ListItem_Lin.selectedStatus.selectedIndex = 0;
					}
					SelectedScratchOffItem = null;
				}
				if (_item.Status.selectedIndex == 0)
				{
					_item.selectedStatus.selectedIndex = 1;
					SelectedScratchOffItem = _item;
				}
				CurType5Scratchoff_TaiDao.btn_Light.grayed = SelectedScratchOffItem == null;
				_item.onClick.Release();
			}
		});
	}

	private void RendererType5ActivityItem_Lin(int index, GObject item)
	{
		UIActivity_Button_Type5_Scratchoff_ListItem_Lin _item = item as UIActivity_Button_Type5_Scratchoff_ListItem_Lin;
		if (_item == null)
		{
			return;
		}
		ActivityScratchoffPoolConfigureItem lightData = curType5LightGiftInfo.GetLightData(index);
		if (lightData == null)
		{
			_item.com_Item.touchable = false;
			_item.Status.selectedIndex = 0;
			_item.com_Name.visible = false;
			_item.Lantern.Play();
		}
		else
		{
			ItemInfoConfigure itemInfoConfigure = lightData.ItemID.GetItemInfoConfigure();
			_item.com_Name.txt_Title.TryScrollTextField(itemInfoConfigure.NameID.GetLocal(UIStringType.Item) + $"x{lightData.ItemNum}");
			_item.com_Name.visible = true;
			CommonUIManager.RendererLitItem((UICom_LitItem)_item.com_Item.btn_Item, lightData.ItemID, lightData.ItemNum);
			if (curType5LightGiftInfo.IsFinished(lightData.Index))
			{
				_item.Status.selectedIndex = 2;
				_item.com_Item.status.selectedIndex = 1;
				_item.com_Item.touchable = false;
			}
			else
			{
				_item.Status.selectedIndex = 1;
				_item.com_Item.status.selectedIndex = 0;
				_item.com_Item.touchable = true;
			}
		}
		_item.data = index;
		_item.selectedStatus.selectedIndex = 0;
		_item.onClick.Set((EventCallback0)delegate
		{
			if (curType5LightGiftInfo.LightIndex == -1)
			{
				_item.onClick.Retain();
				if (SelectedScratchOffItem != null && SelectedScratchOffItem != _item)
				{
					if (SelectedScratchOffItem is UIActivity_Button_Type5_Scratchoff_ListItem_TaiDao uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao)
					{
						uIActivity_Button_Type5_Scratchoff_ListItem_TaiDao.selectedStatus.selectedIndex = 0;
					}
					else if (SelectedScratchOffItem is UIActivity_Button_Type5_Scratchoff_ListItem_Lin uIActivity_Button_Type5_Scratchoff_ListItem_Lin)
					{
						uIActivity_Button_Type5_Scratchoff_ListItem_Lin.selectedStatus.selectedIndex = 0;
					}
					SelectedScratchOffItem = null;
				}
				if (_item.Status.selectedIndex == 0)
				{
					_item.selectedStatus.selectedIndex = 1;
					SelectedScratchOffItem = _item;
				}
				curType5Scratchoff_Lin.btn_Light.grayed = SelectedScratchOffItem == null;
				_item.onClick.Release();
			}
		});
	}

	private void RendererType5ActivityPreview_TaiDao(int index, GObject item)
	{
		if (item is UIActivity_Com_Type5_PreRevard uIActivity_Com_Type5_PreRevard)
		{
			ActivityScratchoffPoolConfigureItem activityScratchoffPoolConfigureItem = curType5LightGiftInfo.PoolData[index];
			uIActivity_Com_Type5_PreRevard.status.selectedIndex = (curType5LightGiftInfo.IsFinished(activityScratchoffPoolConfigureItem.Index) ? 1 : 0);
			CommonUIManager.RendererLitItem((UICom_LitItem)uIActivity_Com_Type5_PreRevard.btn_Item, activityScratchoffPoolConfigureItem.ItemID, activityScratchoffPoolConfigureItem.ItemNum);
		}
	}

	private void RendererType5ActivityPreview_Lin(int index, GObject item)
	{
		if (item is GComponent gComponent)
		{
			ActivityScratchoffPoolConfigureItem activityScratchoffPoolConfigureItem = curType5LightGiftInfo.PoolData[index];
			Controller controller = gComponent.GetController("status");
			if (controller != null)
			{
				controller.selectedIndex = (curType5LightGiftInfo.IsFinished(activityScratchoffPoolConfigureItem.Index) ? 1 : 0);
			}
			if (gComponent.GetChild("btn_Item") is GComponent gComponent2)
			{
				CommonUIManager.RendererLitItem((UICom_LitItem)gComponent2, activityScratchoffPoolConfigureItem.ItemID, activityScratchoffPoolConfigureItem.ItemNum);
			}
		}
	}

	private void RefreshShowSkinType5two()
	{
		CurType5ShowSkin_Lin.language.selectedIndex = int.Parse(GameSettings.GetDataForLanguage("1", "2", "0", "1"));
		LightActivity5SkinStandingPaintingConfig = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(curType5LightGiftInfo.LightInfoConfig.SkinID);
		ItemInfoConfigure itemInfoConfigure = curType5LightGiftInfo.LightInfoConfig.SkinID.GetItemInfoConfigure();
		CurType5ShowSkin_Lin.txt_SkinName.text = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
		CurType5Lin.loader_Character.url = LightActivity5SkinStandingPaintingConfig.GetCharacter().Item1;
		((UICom_SkinQuality)CurType5ShowSkin_Lin.com_Qulity).appearanceType.selectedIndex = (int)LightActivity5SkinStandingPaintingConfig.SkinAppearanceType;
		string previewVideo = LightActivity5SkinStandingPaintingConfig.PreviewVideo;
		CurType5ShowSkin_Lin.Reset.Play();
		RefreshPlatformVideotwo(previewVideo).Forget();
	}

	private void RefreshShowSkinType5()
	{
		CurType5ShowSkin_TaiDao.language.selectedIndex = int.Parse(GameSettings.GetDataForLanguage("1", "2", "0", "1"));
		LightActivity5SkinStandingPaintingConfig = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(curType5LightGiftInfo.LightInfoConfig.SkinID);
		ItemInfoConfigure itemInfoConfigure = curType5LightGiftInfo.LightInfoConfig.SkinID.GetItemInfoConfigure();
		CurType5ShowSkin_TaiDao.txt_SkinName.text = itemInfoConfigure.NameID.GetLocal(UIStringType.Item);
		CurType5TaiDao.loader_Character.url = LightActivity5SkinStandingPaintingConfig.GetCharacter().Item1;
		((UICom_SkinQuality)CurType5ShowSkin_TaiDao.com_Qulity).appearanceType.selectedIndex = (int)LightActivity5SkinStandingPaintingConfig.SkinAppearanceType;
		string previewVideo = LightActivity5SkinStandingPaintingConfig.PreviewVideo;
		CurType5ShowSkin_TaiDao.Reset.Play();
		RefreshPlatformVideo(previewVideo).Forget();
	}

	private async UniTaskVoid RefreshPlatformVideo(string VideoKey)
	{
		if (!string.IsNullOrEmpty(VideoKey))
		{
			CommonUIManager.TryAddVideoGraph(UIType.Panel, 24, CurType5ShowSkin_TaiDao.graph_Video);
			playerVideo = await SimpleSingletonProvider<CriMovieManager>.inst.Play(VideoKey, CurType5ShowSkin_TaiDao.graph_Video, delegate
			{
			}, delegate(Player criPlayer, int status)
			{
				criPlayer.Start();
			}, null, 5);
			playerVideo.Loop(true);
		}
		else
		{
			Player obj = playerVideo;
			if (obj != null)
			{
				obj.Stop();
			}
		}
		CurType5ShowSkin_TaiDao.ShowVideo.SetHook("ShowVideo", delegate
		{
			Player obj2 = playerVideo;
			if (obj2 != null)
			{
				obj2.Pause(false);
			}
		});
		CurType5ShowSkin_TaiDao.CloseVideo.SetHook("CloseVideo", delegate
		{
			Player obj2 = playerVideo;
			if (obj2 != null)
			{
				obj2.Pause(true);
			}
		});
		CurType5ShowSkin_TaiDao.ShowVideo.Play();
	}

	private async UniTaskVoid RefreshPlatformVideotwo(string VideoKey)
	{
		if (!string.IsNullOrEmpty(VideoKey))
		{
			CommonUIManager.TryAddVideoGraph(UIType.Panel, 24, CurType5ShowSkin_Lin.graph_Video);
			playerVideo = await SimpleSingletonProvider<CriMovieManager>.inst.Play(VideoKey, CurType5ShowSkin_Lin.graph_Video, delegate
			{
			}, delegate(Player criPlayer, int status)
			{
				criPlayer.Start();
			}, null, 5);
			playerVideo.Loop(true);
		}
		else
		{
			Player obj = playerVideo;
			if (obj != null)
			{
				obj.Stop();
			}
		}
		CurType5ShowSkin_Lin.ShowVideo.SetHook("ShowVideo", delegate
		{
			Player obj2 = playerVideo;
			if (obj2 != null)
			{
				obj2.Pause(false);
			}
		});
		CurType5ShowSkin_Lin.CloseVideo.SetHook("CloseVideo", delegate
		{
			Player obj2 = playerVideo;
			if (obj2 != null)
			{
				obj2.Pause(true);
			}
		});
		CurType5ShowSkin_Lin.ShowVideo.Play();
	}
}
