using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityStoreSeasonPanel : BasePanel<UIActivityStoreSeasonPanel>
{
	private TaskActivityData curActivityData;

	private List<GachaItem> ActivityGachaResultItems;

	private List<int> curActivityTaskIds;

	private int _activityId;

	private GButton _SelectTab;

	private readonly int[] _sportProgressTaskId = new int[5] { 7062501, 7062502, 7062503, 7062504, 7062505 };

	private List<HeroCardData> _sportHeroes;

	private const int _maxSportProgress = 200;

	private ShopTypeData shopTypeData;

	private List<BaseGoodsData> GoodsDatas;

	private GachaPoolConfigure PoolConfigData;

	private ActivityGachaConfigure ActivityGachaConfig => curActivityData?.activityConfig.GachaConfig;

	private GachaBackstageConfigure ActivityBackstageConfig => ActivityGachaConfig?.GachaBackstageConfig;

	private int ActivityPropId
	{
		get
		{
			if (curActivityData == null)
			{
				return 0;
			}
			return curActivityData.activityConfig.TaskTargetID;
		}
	}

	public ActivityStoreSeasonPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityStoreSeasonPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (objs != null && objs.Length != 0)
		{
			_activityId = ((RepeatedField<int>)objs[0])[0];
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.com_Task.list_Tasks.SetVirtual();
		base.ui.com_Task.list_Tasks.itemRenderer = RendererActivityTask;
		base.ui.com_Store.list_Store_Goods.SetVirtual();
		base.ui.com_Store.list_Store_Goods.itemRenderer = RendererActivityGoods;
		base.ui.list_Gacha_Result.itemRenderer = RendererActivity3GachaResult;
		base.ui.com_Sport.list_Hero.SetVirtual();
		base.ui.com_Sport.list_Hero.itemRenderer = RefreshSportHeroItem;
		base.ui.mohu.SetSize(GRoot.inst.width, base.ui.mohu.height);
	}

	public override void Refresh()
	{
		base.Refresh();
		RefreshActivity(_activityId);
		SimpleSingletonProvider<GameLogicManager>.inst.activity.signal.switchTokenList.Dispatch(0);
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.com_Gacha.btn_Gacha_Detail.onClick.Add(ShowActivity3GachaPoolInfo);
		base.ui.com_Gacha.btn_Gacha_Recoard.onClick.Add(RequestGetActivity3GachaRecard);
		base.ui.com_Gacha.btn_Gahca_Once.onClick.Add(RequestActivity3OneGacha);
		base.ui.com_Gacha.btn_Gahca_Multi.onClick.Add(RequestActivity3tMultiGacha);
		base.ui.btn_Gacha_Confirm.onClick.Add(CloseGachaResult);
		base.ui.com_Gacha.btn_Gacha_PreviewSkin.onClick.Add(ShowSkin);
		base.ui.com_Gacha.btn_AddGift.onClick.Add(GoAddGift);
		base.ui.com_Task.btn_Preview.onClick.Add(PreviewSkin);
		base.ui.com_Task.btn_TaskToggle.onClick.Add(TaskToggle);
		base.ui.btn_Return.onClick.Add(OnReturnToLastPanel);
		base.ui.com_Sport.list_Hero.scrollPane.onScroll.Add(OnScrollSignal);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.com_Gacha.btn_Gacha_Detail.onClick.Remove(ShowActivity3GachaPoolInfo);
		base.ui.com_Gacha.btn_Gacha_Recoard.onClick.Remove(RequestGetActivity3GachaRecard);
		base.ui.com_Gacha.btn_Gahca_Once.onClick.Remove(RequestActivity3OneGacha);
		base.ui.com_Gacha.btn_Gahca_Multi.onClick.Remove(RequestActivity3tMultiGacha);
		base.ui.btn_Gacha_Confirm.onClick.Remove(CloseGachaResult);
		base.ui.com_Gacha.btn_Gacha_PreviewSkin.onClick.Remove(ShowSkin);
		base.ui.com_Gacha.btn_AddGift.onClick.Remove(GoAddGift);
		base.ui.com_Task.btn_Preview.onClick.Remove(PreviewSkin);
		base.ui.com_Task.btn_TaskToggle.onClick.Remove(TaskToggle);
		base.ui.btn_Return.onClick.Remove(OnReturnToLastPanel);
		base.ui.com_Sport.list_Hero.scrollPane.onScroll.Remove(OnScrollSignal);
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
		base.Close();
		base.ui.list_Tabs.numItems = 0;
		base.ui.com_Task.list_Tasks.numItems = 0;
		SimpleSingletonProvider<ExternalAssetManager>.inst.StopAnimationInUI(base.ui.com_Gacha.loader_UpAnimation);
		base.ui.com_Store.list_Store_Goods.numItems = 0;
		if (SimpleSingletonProvider<UIManager>.inst.gachaInfo.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.gachaInfo.Hide();
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void PreviewSkin()
	{
		base.ui.com_Task.btn_Preview.onClick.Retain();
		int superReward = curActivityData.activityConfig.SuperReward;
		if (superReward != 0)
		{
			SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(superReward, 0).Forget();
		}
		base.ui.com_Task.btn_Preview.onClick.Release();
	}

	private async void OnReturnToLastPanel()
	{
		base.ui.btn_Return.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Return.onClick.Release();
	}

	private void RefreshActivity(int activityId)
	{
		base.ui.com_Task.btn_TaskToggle.selected = true;
		base.ui.com_Task.btn_TaskToggle.visible = false;
		curActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityId);
		if (curActivityData == null)
		{
			Debug.LogError($"活动数据为空，请检查活动数据；活动ID: {activityId}");
			return;
		}
		List<ActivityTab> tabs = new List<ActivityTab>(4);
		tabs.Add(new ActivityTab
		{
			TitleId = 20,
			SelectedIndex = 0
		});
		if (curActivityData.activityConfig.GachaID != 0)
		{
			tabs.Add(new ActivityTab
			{
				TitleId = 30,
				SelectedIndex = 1
			});
		}
		tabs.Add(new ActivityTab
		{
			TitleId = 40,
			SelectedIndex = 2
		});
		tabs.Add(new ActivityTab
		{
			TitleId = 6062511,
			SelectedIndex = 3
		});
		base.ui.com_SportHeroInfo.visible = false;
		base.ui.list_Tabs.itemRenderer = delegate(int index, GObject item)
		{
			if (index < tabs.Count)
			{
				ActivityTab tabData = tabs[index];
				GButton btn_tab = item as GButton;
				if (btn_tab != null)
				{
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

	private void RefreshActivityData(ActivityTab tabData)
	{
		RepeatedField<string> bgList = curActivityData.activityConfig.BgList;
		if (base.ui.tab.selectedIndex == 0)
		{
			RefreshActivityTask();
			if (bgList.Count > 0)
			{
				SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(curActivityData.activityConfig.BgList[0], delegate(NTexture texture)
				{
					base.ui.Loader_BG.Background(texture);
				}, null).Forget();
			}
		}
		else if (base.ui.tab.selectedIndex == 1)
		{
			RefreshActivityGacha();
			if (bgList.Count > 1)
			{
				SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(curActivityData.activityConfig.BgList[1], delegate(NTexture texture)
				{
					base.ui.Loader_BG.Background(texture);
				}, null).Forget();
			}
		}
		else if (base.ui.tab.selectedIndex == 2)
		{
			RefreshActivityStore();
			if (bgList.Count > 2)
			{
				SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(curActivityData.activityConfig.BgList[2], delegate(NTexture texture)
				{
					base.ui.Loader_BG.Background(texture);
				}, null).Forget();
			}
		}
		else
		{
			if (base.ui.tab.selectedIndex != 3)
			{
				return;
			}
			RefreshSportData(tabData);
			if (bgList.Count > 3)
			{
				SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(bgList[3], delegate(NTexture texture)
				{
					base.ui.Loader_BG.Background(texture);
				}, null).Forget();
			}
		}
	}

	private void RefreshSportData(ActivityTab tabData)
	{
		RefreshSportHero();
		RefreshSportProgress();
		RegisterBtnEventSport();
		OnScrollSignal();
	}

	private void RefreshSportHero()
	{
		HeroCardLogic heroCard = SimpleSingletonProvider<GameLogicManager>.inst.heroCard;
		_sportHeroes = heroCard.GetSportsMeetHeroCards();
		base.ui.com_Sport.list_Hero.numItems = _sportHeroes.Count;
	}

	private void RefreshSportHeroItem(int index, GObject item)
	{
		UIActivityStoreSeason_Button_Hero btn_hero = item as UIActivityStoreSeason_Button_Hero;
		if (btn_hero != null)
		{
			HeroCardData heroData = _sportHeroes.GetSafeByIndex(index);
			btn_hero.Refresh(heroData);
			btn_hero.onClick.Set((EventCallback0)delegate
			{
				btn_hero.onClick.Retain();
				base.ui.com_SportHeroInfo.RefreshHeroSportInfo(heroData);
				base.ui.com_SportHeroInfo.visible = true;
				base.ui.com_SportHeroInfo.Cut_in.Play();
				btn_hero.onClick.Release();
			});
		}
	}

	private void RefreshSportProgress()
	{
		base.ui.com_Sport.btn_GoSport.title = 1080001.GetLocal(UIStringType.GUI);
		RefreshSportProgressItem(base.ui.com_Sport.btn_Reward1, 0);
		RefreshSportProgressItem(base.ui.com_Sport.btn_Reward2, 1);
		RefreshSportProgressItem(base.ui.com_Sport.btn_Reward3, 2);
		RefreshSportProgressItem(base.ui.com_Sport.btn_Reward4, 3);
		RefreshSportProgressItem(base.ui.com_Sport.btn_Reward5, 4);
		SportsMeetData sportsMeetData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData;
		int num = 0;
		foreach (HeroCardData sportHero in _sportHeroes)
		{
			if (sportsMeetData.GetHeroMapRecordById(sportHero.HeroId, SportsMeetData.GetMapIdBySportsMeetRank(1)) > 0)
			{
				num++;
			}
			if (sportsMeetData.GetHeroMapRecordById(sportHero.HeroId, SportsMeetData.GetMapIdBySportsMeetRank(2)) > 0)
			{
				num++;
			}
			if (sportsMeetData.GetHeroMapRecordById(sportHero.HeroId, SportsMeetData.GetMapIdBySportsMeetRank(3)) > 0)
			{
				num++;
			}
		}
		base.ui.com_Sport.progress_Star.max = 200.0;
		base.ui.com_Sport.progress_Star.value = GetProgressValue(num);
	}

	private double GetProgressValue(int progress)
	{
		if (progress <= 3)
		{
			return (double)progress / 3.0 * 26.0;
		}
		if (progress <= 6)
		{
			return (double)progress / 6.0 * 63.0;
		}
		if (progress <= 9)
		{
			return (double)progress / 9.0 * 100.0;
		}
		if (progress <= 12)
		{
			return (double)progress / 12.0 * 137.0;
		}
		if (progress <= 15)
		{
			return (double)progress / 15.0 * 174.0;
		}
		return 200.0;
	}

	private void RefreshSportProgressItem(UIActivityStoreSeason_Button_Reward btn_Reward, int index)
	{
		if (!curActivityData.taskDataDict.TryGetValue(_sportProgressTaskId[index], out var taskData))
		{
			return;
		}
		KeyValuePair<int, int> keyValuePair = taskData.rewards[0];
		CommonUIManager.RendererLitItem((UICom_LitItem)btn_Reward.btn_Item, keyValuePair.Key, keyValuePair.Value);
		bool flag = IsTaskRunning(taskData);
		btn_Reward.status.selectedIndex = (taskData._FinishStatus ? 2 : ((!flag) ? 1 : 0));
		btn_Reward.btn_Item.touchable = btn_Reward.status.selectedIndex != 1;
		btn_Reward.onClick.Set((EventCallback0)delegate
		{
			if (curActivityData == null || curActivityData.activityConfig == null)
			{
				Debug.LogError($"活动数据：{curActivityData == null}， 活动配置：{curActivityData?.activityConfig == null}");
			}
			else if (CanClaimTask(taskData))
			{
				base.ui.com_Sport.touchable = false;
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(curActivityData.activityConfig.Id, taskData).OnFinishedOnly.AddOnce(delegate
				{
					UpdateActivityRedStatus();
					btn_Reward.status.selectedIndex = 2;
					btn_Reward.btn_Item.touchable = true;
					base.ui.com_Sport.touchable = true;
				});
			}
		});
	}

	private void RegisterBtnEventSport()
	{
		base.ui.com_Sport.btn_GoSport.onClick.Set((EventCallback0)async delegate
		{
			base.ui.com_Sport.btn_GoSport.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(1300);
			base.ui.com_Sport.btn_GoSport.onClick.Release();
		});
		base.ui.com_Sport.btn_Detail.onClick.Set((EventCallback0)async delegate
		{
			base.ui.com_Sport.btn_Detail.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.rule.TryShow(1062501, 1062502);
			base.ui.com_Sport.btn_Detail.onClick.Release();
		});
		base.ui.com_SportHeroInfo.btn_Close.onClick.Set((EventCallback0)delegate
		{
			base.ui.com_SportHeroInfo.btn_Close.onClick.Retain();
			base.ui.com_SportHeroInfo.visible = false;
			base.ui.com_SportHeroInfo.btn_Close.onClick.Release();
		});
		base.ui.com_SportHeroInfo.btn_Next.onClick.Set((EventCallback0)delegate
		{
			base.ui.com_SportHeroInfo.btn_Next.onClick.Retain();
			RefreshNextHeroSportInfo(1);
			base.ui.com_SportHeroInfo.btn_Next.onClick.Release();
		});
		base.ui.com_SportHeroInfo.btn_Pre.onClick.Set((EventCallback0)delegate
		{
			base.ui.com_SportHeroInfo.btn_Pre.onClick.Retain();
			RefreshNextHeroSportInfo(-1);
			base.ui.com_SportHeroInfo.btn_Pre.onClick.Release();
		});
	}

	private void RefreshNextHeroSportInfo(int delta)
	{
		if (_sportHeroes.Count == 0)
		{
			return;
		}
		HeroCardData curHeroCard = base.ui.com_SportHeroInfo.HeroCard;
		if (curHeroCard != null)
		{
			int num = _sportHeroes.FindIndex((HeroCardData x) => x.HeroId == curHeroCard.HeroId);
			if (num >= 0)
			{
				int count = _sportHeroes.Count;
				int index = ((num + delta) % count + count) % count;
				HeroCardData safeByIndex = _sportHeroes.GetSafeByIndex(index);
				base.ui.com_SportHeroInfo.RefreshHeroSportInfo(safeByIndex);
			}
		}
	}

	private void OnScrollSignal()
	{
		ScrollPane scrollPane = base.ui.com_Sport.list_Hero.scrollPane;
		if (scrollPane != null)
		{
			if (!(scrollPane.contentWidth > scrollPane.viewWidth + 0.5f))
			{
				base.ui.com_Sport.com_NextSignal.visible = false;
				base.ui.com_Sport.com_PreSignal.visible = false;
			}
			else
			{
				base.ui.com_Sport.com_NextSignal.visible = scrollPane.percX < 1f;
				base.ui.com_Sport.com_PreSignal.visible = scrollPane.percX > 0f;
			}
		}
	}

	private void RefreshActivityTask()
	{
		if (curActivityData == null)
		{
			return;
		}
		string url = (GameSettings.angelMode ? curActivityData.activityConfig.ActivityImageSFW : curActivityData.activityConfig.ActivityImage);
		RefreshStandingPainting(url, base.ui.com_Task.loader_Task_Character);
		bool flag = false;
		foreach (KeyValuePair<int, BaseTaskData> item in curActivityData.taskDataDict)
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
			RefreshTaskList();
			base.ui.com_Task.list_Tasks.scrollPane.percY = 0f;
			base.ui.com_Task.taskValid.selectedIndex = 0;
			base.ui.com_Task.com_Token.loader_ActivityProp.url = ActivityPropId.GetItemInfoConfigure().ShowIcon;
		}
		else
		{
			base.ui.com_Task.taskValid.selectedIndex = 1;
		}
		RepeatedField<string> titleImages = curActivityData.activityConfig.TitleImages;
		base.ui.com_Task.loader_Title.url = GameSettings.GetDataForLanguage(titleImages[1], titleImages[2], titleImages[0], titleImages[3]);
		bool visible = false;
		int superReward = curActivityData.activityConfig.SuperReward;
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
		if (curActivityTaskIds != null && item is UIActivityStoreSeason_Com_Label label)
		{
			RefreshActivityTaskLabel_1(index, label);
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

	private void RefreshActivityTaskLabel_1(int index, UIActivityStoreSeason_Com_Label label)
	{
		BaseTaskData taskData = curActivityData.taskDataDict[curActivityTaskIds[index]];
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
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(curActivityData.activityConfig.Id, taskData).OnFinishedOnly.AddOnce(delegate
				{
					RefreshTaskList();
					UpdateActivityRedStatus();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
		label.group_Go.visible = !taskData._FinishStatus && flag && taskData.GetWay() != 0;
		label.btn_GoWay.onClick.Set((EventCallback0)delegate
		{
			GoTargetPanel(taskData);
		});
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

	private void UpdateActivityRedStatus()
	{
		if (base.ui.list_Tabs._children.GetSafeByIndex(0) is UIActivityStoreSeason_Button_Tab uIActivityStoreSeason_Button_Tab)
		{
			uIActivityStoreSeason_Button_Tab.redStatus.selectedIndex = (GetTaskStatus(seasonTask: false) ? 1 : 0);
		}
		if (base.ui.list_Tabs._children.GetSafeByIndex(3) is UIActivityStoreSeason_Button_Tab uIActivityStoreSeason_Button_Tab2)
		{
			uIActivityStoreSeason_Button_Tab2.redStatus.selectedIndex = (GetTaskStatus(seasonTask: true) ? 1 : 0);
		}
	}

	private bool GetTaskStatus(bool seasonTask)
	{
		foreach (KeyValuePair<int, BaseTaskData> item in curActivityData.taskDataDict)
		{
			if (_sportProgressTaskId.Contains(item.Key) == seasonTask && item.Value.ValidityTime() && !item.Value._FinishStatus && item.Value is MissionData { TaskRunning: false, config: var missionDataConfigure })
			{
				MissionData missionData2 = SimpleSingletonProvider<GameLogicManager>.inst.task.TryGetMissionById(missionDataConfigure.Id);
				if (missionData2 != null && !missionData2._FinishStatus)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void RefreshTaskList()
	{
		bool selected = base.ui.com_Task.btn_TaskToggle.selected;
		curActivityTaskIds = curActivityData.GetTaskId(selected);
		curActivityTaskIds = (from x in curActivityTaskIds
			where !_sportProgressTaskId.Contains(x)
			select (x)).ToList();
		base.ui.com_Task.list_Tasks.numItems = curActivityTaskIds.Count;
		base.ui.com_Task.list_Tasks.RefreshVirtualList();
		int num = 0;
		foreach (KeyValuePair<int, BaseTaskData> item in curActivityData.taskDataDict)
		{
			item.Deconstruct(out var _, out var value);
			BaseTaskData baseTaskData = value;
			KeyValuePair<int, int> keyValuePair = baseTaskData.rewards[0];
			if (baseTaskData._FinishStatus && keyValuePair.Key == ActivityPropId)
			{
				num += keyValuePair.Value;
			}
		}
		base.ui.com_Task.com_Token.txt_ActivityPropCount.text = $"{num}/{curActivityData.activityConfig.TaskTargetNumb}";
	}

	private void TaskToggle()
	{
		base.ui.com_Task.btn_TaskToggle.onClick.Retain();
		RefreshTaskList();
		base.ui.com_Task.btn_TaskToggle.onClick.Release();
	}

	private void RefreshActivityStore()
	{
		if (curActivityData != null)
		{
			ExchangeStoreInfoConfigure exchangeStoreInfoConfigure = ((int)curActivityData.activityConfig.ShopTabType).GetExchangeStoreInfoConfigure();
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
				uIButton_GoodsItem.enabled = false;
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

	private async void ShowSkin(EventContext context)
	{
		base.ui.com_Gacha.btn_Gacha_PreviewSkin.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.showSkin.PreviewSkin(ActivityGachaConfig.SkinID, 0);
		base.ui.com_Gacha.btn_Gacha_PreviewSkin.onClick.Release();
	}

	private async void GoAddGift(EventContext context)
	{
		base.ui.com_Gacha.btn_AddGift.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(ActivityGachaConfig.Way);
		base.ui.com_Gacha.btn_AddGift.onClick.Release();
	}

	private void RefreshActivityGacha()
	{
		GachaBackstageConfigure activityBackstageConfig = ActivityBackstageConfig;
		PoolConfigData = activityBackstageConfig.PoolID.GetGachaPoolConfigure();
		base.ui.com_Gacha.txt_Gacha_SkinName.text = ActivityGachaConfig.SkinID.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item);
		GLoader loader_Icon = base.ui.com_Gacha.btn_Gahca_Once.loader_Icon;
		string url = (base.ui.com_Gacha.btn_Gahca_Multi.loader_Icon.url = ActivityPropId.GetItemInfoConfigure().ShowIcon);
		loader_Icon.url = url;
		base.ui.com_Gacha.btn_Gahca_Once.txt_Name.SetVar("time", "1").FlushVars();
		base.ui.com_Gacha.btn_Gahca_Once.txt_Count.text = "X1";
		base.ui.com_Gacha.btn_Gahca_Multi.txt_Name.SetVar("time", activityBackstageConfig.NTimes.ToString()).FlushVars();
		base.ui.com_Gacha.btn_Gahca_Multi.txt_Count.text = "X10";
		RefreshStandingPainting(GetPoolBG(PoolConfigData), base.ui.com_Gacha.loader_Gacha_BG);
		string downTime = GetDownTime();
		base.ui.com_Gacha.txt_Gacha_timeTip.text = downTime;
		base.ui.com_Gacha.txt_Gacha_timeTip.visible = !string.IsNullOrEmpty(downTime);
		base.ui.com_Gacha.btn_AddGift.loader_Icon.url = ActivityGachaConfig.GiftID.GetItemInfoConfigure().Icon;
		RefreshHeroAnimation();
		base.ui.com_Gacha.btn_Gahca_Multi.onClick.Release();
		base.ui.com_Gacha.btn_Gahca_Once.onClick.Release();
		base.ui.btn_Return.onClick.Release();
	}

	private void RefreshHeroAnimation()
	{
		SkinStandingPaintingConfigureItem configStandingPainting = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetConfigStandingPainting(ActivityGachaConfig.SkinID);
		if (configStandingPainting == null)
		{
			base.ui.com_Gacha.loader_UpAnimation.visible = false;
		}
		else
		{
			SimpleSingletonProvider<ExternalAssetManager>.inst.PlayAnimationInUI(configStandingPainting, "Walk", base.ui.com_Gacha.loader_UpAnimation, 10f).Forget();
		}
	}

	private string GetPoolBG(GachaPoolConfigure poolData)
	{
		return GameSettings.GetDataForLanguage(GameSettings.angelMode ? poolData.BackgroundENSFW : poolData.BackgroundEN, GameSettings.angelMode ? poolData.BackgroundJPSFW : poolData.BackgroundJP, GameSettings.angelMode ? poolData.BackgroundCNSFW : poolData.BackgroundCN, GameSettings.angelMode ? poolData.BackgroundTCSFW : poolData.BackgroundTC);
	}

	private string GetDownTime()
	{
		DateTime serverTime = MonoSingletonProvider<NetManager>.inst.ServerTime;
		Timestamp timestamp = ActivityBackstageConfig?.EndDateTime;
		if (timestamp == null)
		{
			return null;
		}
		DateTime endTime = timestamp.ToDateTime();
		return TimeHelper.RefreshTimeText(1010, 1011, serverTime, endTime);
	}

	private void ShowActivity3GachaPoolInfo()
	{
		base.ui.com_Gacha.btn_Gacha_Detail.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.gachaInfo.ShowGachaPoolInfo(PoolConfigData);
		base.ui.com_Gacha.btn_Gacha_Detail.onClick.Release();
	}

	private void RequestGetActivity3GachaRecard()
	{
		base.ui.com_Gacha.btn_Gacha_Recoard.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaRecordS2C((int)ActivityBackstageConfig.GachaType).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (result.errId == 0)
			{
				SimpleSingletonProvider<UIManager>.inst.gachaInfo.ShowGachaRecord(PoolConfigData);
			}
		});
		base.ui.com_Gacha.btn_Gacha_Recoard.onClick.Release();
	}

	private void RequestActivity3OneGacha()
	{
		if (IsCanGacha(ActivityBackstageConfig.CostOnce))
		{
			SimpleSingletonProvider<UIManager>.inst.ShowTipsBeforeGacha(ActivityBackstageConfig.CostItem, 1, RequestActivity3OneGachaImp);
		}
		void RequestActivity3OneGachaImp()
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
			{
				base.ui.com_Gacha.btn_Gahca_Once.onClick.Retain();
				base.ui.btn_Return.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCSC((int)ActivityBackstageConfig.GachaType, 1).OnFinished.AddOnce(delegate(RPCAsyncResult result)
				{
					if (result.errId != 0)
					{
						base.ui.com_Gacha.btn_Gahca_Once.onClick.Release();
						base.ui.btn_Return.onClick.Release();
					}
					else
					{
						Activity3GachaShow();
					}
				});
			}
		}
	}

	private void RequestActivity3tMultiGacha()
	{
		if (IsCanGacha(ActivityBackstageConfig.NTimes))
		{
			SimpleSingletonProvider<UIManager>.inst.ShowTipsBeforeGacha(ActivityBackstageConfig.CostItem, ActivityBackstageConfig.NTimes, RequestActivity3tMultiGachaImp);
		}
		void RequestActivity3tMultiGachaImp()
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
			{
				base.ui.com_Gacha.btn_Gahca_Multi.onClick.Retain();
				base.ui.btn_Return.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.gacha.RequestGachaCSC((int)ActivityBackstageConfig.GachaType, ActivityBackstageConfig.NTimes).OnFinished.AddOnce(delegate(RPCAsyncResult result)
				{
					if (result.errId != 0)
					{
						base.ui.com_Gacha.btn_Gahca_Multi.onClick.Release();
						base.ui.btn_Return.onClick.Release();
					}
					else
					{
						Activity3GachaShow();
					}
				});
			}
		}
	}

	private bool IsCanGacha(int time)
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(ActivityPropId) < time)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(1054);
			return false;
		}
		return true;
	}

	private void CloseGachaResult()
	{
		base.ui.btn_Gacha_Confirm.onClick.Retain();
		GObject[] children = base.ui.list_Gacha_Result.GetChildren();
		for (int i = 0; i < children.Length; i++)
		{
			if (!(children[i] is UIActivityStoreSeason_Button_Item uIActivityStoreSeason_Button_Item))
			{
				return;
			}
			CloseEffect(uIActivityStoreSeason_Button_Item.graph_qualityEffect);
			CloseEffect(uIActivityStoreSeason_Button_Item.graph_DisplayEffect);
			CloseEffect(uIActivityStoreSeason_Button_Item.graph_ReplaceEffect);
		}
		base.ui.list_Gacha_Result.numItems = 0;
		base.ui.showResult.selectedIndex = 0;
		base.ui.com_Gacha.btn_Gahca_Multi.onClick.Release();
		base.ui.com_Gacha.btn_Gahca_Once.onClick.Release();
		base.ui.btn_Return.visible = true;
		if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
		{
			bottomMenuPanel.ChangeShowStatus(status: true);
		}
		base.ui.btn_Return.onClick.Release();
		base.ui.btn_Gacha_Confirm.onClick.Release();
	}

	private async void Activity3GachaShow()
	{
		base.ui.btn_Return.visible = false;
		await SimpleSingletonProvider<CriMovieManager>.inst.Load(102.GetVideoKey());
		base.ui.showResult.selectedIndex = 1;
		await SimpleSingletonProvider<UIManager>.inst.gachaInfo.ShowGachaVideo_Activity(OnPrepareCompleted, OnLoopPointReached);
	}

	private void OnPrepareCompleted()
	{
		base.ui.com_Gacha.btn_Gahca_Once.onClick.Release();
		base.ui.com_Gacha.btn_Gahca_Multi.onClick.Release();
		base.ui.btn_Gacha_Confirm.onClick.Release();
	}

	private void OnLoopPointReached()
	{
		base.ui.graph_GachaResultMask.FullScreen();
		ActivityGachaResultItems = SimpleSingletonProvider<GameLogicManager>.inst.gacha.itemList;
		base.ui.showResult.selectedIndex = 2;
		base.ui.list_Gacha_Result.numItems = ActivityGachaResultItems.Count;
		SimpleSingletonProvider<UIManager>.inst.gachaInfo.Hide();
	}

	private void RendererActivity3GachaResult(int index, GObject item)
	{
		if (item is UIActivityStoreSeason_Button_Item uIActivityStoreSeason_Button_Item)
		{
			RendererGachaResult(ActivityGachaResultItems[index], uIActivityStoreSeason_Button_Item);
			uIActivityStoreSeason_Button_Item.showResult.Play(1, 0.005f * (float)index, null);
		}
	}

	private void RendererGachaResult(GachaItem _info, UIActivityStoreSeason_Button_Item com_ResultItem)
	{
		ItemInfoConfigure infoItemInfo = _info.itemInfo;
		com_ResultItem.txt_Count.text = _info.Count.ToString();
		com_ResultItem.loader_Icon.url = infoItemInfo.ShowIcon;
		((UICom_QualityType)com_ResultItem.com_QualityType).qualityType.selectedIndex = (int)infoItemInfo.QualityType;
		com_ResultItem.newAcquire_.selectedIndex = (_info.newAcquire ? 1 : 0);
		com_ResultItem.replaceStatus.selectedIndex = 0;
		CloseEffect(com_ResultItem.graph_DisplayEffect);
		com_ResultItem.showResult.SetHook("displayEffect", delegate
		{
			QualityType qualityType = infoItemInfo.QualityType;
			if (qualityType == QualityType.Orange || qualityType == QualityType.Purple)
			{
				ShowDisplayEffect(com_ResultItem.graph_DisplayEffect, infoItemInfo.QualityType);
			}
		});
		CloseEffect(com_ResultItem.graph_qualityEffect);
		com_ResultItem.showResult.SetHook("qualityEffect", delegate
		{
			QualityType qualityType = infoItemInfo.QualityType;
			if (qualityType == QualityType.Orange || qualityType == QualityType.Purple)
			{
				ShowQualityEffect(com_ResultItem.graph_qualityEffect, infoItemInfo.QualityType);
			}
		});
		com_ResultItem.showResult.SetHook("Replace", delegate
		{
			if (_info.replaceItemIds != null && _info.replaceItemIds.Count != 0)
			{
				com_ResultItem.showReplace.Play();
			}
		});
		CloseEffect(com_ResultItem.graph_ReplaceEffect);
		if (_info.replaceItemIds == null || _info.replaceItemIds.Count == 0)
		{
			return;
		}
		com_ResultItem.replaceStatus.selectedIndex = 1;
		KeyValuePair<int, int> keyValuePair = _info.replaceItemIds[0];
		ItemInfoConfigure itemInfoConfigure = keyValuePair.Key.GetItemInfoConfigure();
		com_ResultItem.loader_replaceItem.url = itemInfoConfigure.ShowIcon;
		com_ResultItem.txt_Count.text = keyValuePair.Value.ToString();
		com_ResultItem.showReplace.SetHook("replaceEffect", delegate
		{
			if (_info.replaceItemIds != null && _info.replaceItemIds.Count != 0)
			{
				ShowReplaceEffect(com_ResultItem.graph_ReplaceEffect);
			}
		});
	}

	private void ShowDisplayEffect(GGraph _graph_Effect, QualityType type)
	{
		switch (type)
		{
		case QualityType.Orange:
		{
			EffectInfoConfigure effectDataConfigure2 = 1002.GetEffectDataConfigure();
			SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure2.EffectName, _graph_Effect, 70f).Forget();
			break;
		}
		case QualityType.Purple:
		{
			EffectInfoConfigure effectDataConfigure = 1001.GetEffectDataConfigure();
			SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure.EffectName, _graph_Effect, 70f).Forget();
			break;
		}
		}
	}

	private void ShowQualityEffect(GGraph _graph_Effect, QualityType type)
	{
		switch (type)
		{
		case QualityType.Orange:
		{
			EffectInfoConfigure effectDataConfigure2 = 1004.GetEffectDataConfigure();
			SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure2.EffectName, _graph_Effect, 45f).Forget();
			break;
		}
		case QualityType.Purple:
		{
			EffectInfoConfigure effectDataConfigure = 1003.GetEffectDataConfigure();
			SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure.EffectName, _graph_Effect, 45f).Forget();
			break;
		}
		}
	}

	private void ShowReplaceEffect(GGraph _graph_Effect)
	{
		EffectInfoConfigure effectDataConfigure = 1005.GetEffectDataConfigure();
		SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure.EffectName, _graph_Effect, 45f).Forget();
	}

	private void CloseEffect(GGraph _graph_Effect)
	{
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(_graph_Effect);
	}
}
