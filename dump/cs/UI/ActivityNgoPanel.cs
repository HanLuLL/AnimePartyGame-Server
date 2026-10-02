using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Audio;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class ActivityNgoPanel : BasePanel<UIActivityNgoPanel>
{
	private ActivityInfoConfigure _currentActivityConfig;

	private ScratchOffActivityData curType4ActivityData;

	private List<int> curActivityTaskIds;

	private const int id = 605210;

	private RepeatedField<ActivityScratchoffPoolConfigureItem> scratchOffPoolType4;

	private readonly int[] activityCharacter = new int[2] { 301, 302 };

	private readonly int[] activityCharacterVoiceId = new int[2] { 802, 803 };

	public ActivityNgoPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIActivityNgoPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		if (!StaticConfigure.Activity.InfoDict.TryGetValue(605210, out _currentActivityConfig))
		{
			Debug.LogError($"获取活动id:{605210}错误!");
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		if (_currentActivityConfig.UiTab == 4)
		{
			RefreshActivityType4(_currentActivityConfig.Id);
		}
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		InitComponentsForType4();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Return_NGO.onClick.Add(OnReturnToLastPanel);
		AddEventForType4();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Return_NGO.onClick.Remove(OnReturnToLastPanel);
		RemoveEventForType4();
	}

	protected override void AddListener()
	{
		base.AddListener();
		AddListenerForType4();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		RemoveListenerForType4();
	}

	public override void Close()
	{
		CloseForType4();
		base.Close();
		base.ui.com_Type4.com_Task.loader_Icon_Color.texture?.Dispose();
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
		base.ui.btn_Return_NGO.onClick.Retain();
		if (_currentActivityConfig.UiTab == 4)
		{
			Controller pageType = base.ui.com_Type4.pageType;
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
		base.ui.btn_Return_NGO.onClick.Release();
	}

	public async UniTask ShowScratchOffResult(int index)
	{
		if (_currentActivityConfig.UiTab == 4)
		{
			await ShowScratchOffType4Result(index);
		}
	}

	private async void GoTargetPanel(ActivityTaskData taskData)
	{
		if (taskData._Config.Way != 0 && taskData.TaskRunning && !taskData._FinishStatus)
		{
			await SimpleSingletonProvider<UIManager>.inst.GoWayPanel(taskData._Config.Way);
		}
	}

	private void RefreshBar(GProgressBar bar_task, int configParam, int taskDataProgress)
	{
		bar_task.max = configParam;
		bar_task.min = 0.0;
		bar_task.value = Mathf.Min(taskDataProgress, configParam);
	}

	private void InitComponentsForType4()
	{
		base.ui.com_Type4.com_Task.list_Task.itemRenderer = RendererType4ActivityTask;
		base.ui.com_Type4.com_Task.list_Preview.itemRenderer = RendererType4ActivityTaskReward;
		base.ui.com_Type4.com_Scratchoff.list_Preview.itemRenderer = RendererType4ScratchOffPreview;
	}

	private void AddEventForType4()
	{
		base.ui.com_Type4.pageType.onChanged.Add(RefreshDataType4);
		base.ui.com_Type4.com_Task.com_ScratchOff.btn_Open.onClick.Add(OpenScratchOffType4);
	}

	private void RemoveEventForType4()
	{
		base.ui.com_Type4.pageType.onChanged.Remove(RefreshDataType4);
		base.ui.com_Type4.com_Task.com_ScratchOff.btn_Open.onClick.Remove(OpenScratchOffType4);
	}

	private void AddListenerForType4()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.AddListener(UpdateConsume_Type4);
	}

	private void RemoveListenerForType4()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.bagMapChanged.RemoveListener(UpdateConsume_Type4);
	}

	private void CloseForType4()
	{
	}

	private void UpdateConsume_Type4()
	{
		if (_currentActivityConfig.UiTab == 4)
		{
			base.ui.com_Type4.com_Task.com_ScratchOff.redPoint.selectedIndex = (curType4ActivityData.ScratchOffDataLicense() ? 1 : 0);
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(curType4ActivityData.consumePropId);
			base.ui.com_Type4.com_Task.com_ScratchOff.txt_ItemCount.text = $"*{itemCount}";
			base.ui.com_Type4.com_Scratchoff.txt_ItemCount.text = $"*{itemCount}";
		}
	}

	private async void RefreshActivityType4(int activityId)
	{
		curType4ActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetScratchOffActivityData(activityId);
		CommonUIManager.TryAddVideoGraph(UIType.Panel, (int)base.config.PanelType, base.ui.com_Type4.loader_BG);
		await SimpleSingletonProvider<CriMovieManager>.inst.Play(400.GetVideoKey(), base.ui.com_Type4.loader_BG);
		base.ui.com_Type4.pageType.selectedIndex = 0;
		base.ui.com_Type4.pageType.onChanged.Call();
	}

	private void RefreshDataType4()
	{
		if (base.ui.com_Type4.pageType.selectedIndex == 0)
		{
			RefreshActivityType4Task();
		}
		else if (base.ui.com_Type4.pageType.selectedIndex == 1)
		{
			RefreshActivityType4ScratchOff();
		}
		UpdateConsume_Type4();
	}

	private void RefreshActivityType4Task()
	{
		if (curType4ActivityData == null)
		{
			return;
		}
		base.ui.com_Type4.com_Task.loader_Title.url = GetTaskType4Title();
		ActivityScratchoffConfigure scratchOffConfig = curType4ActivityData.ScratchOffConfig;
		base.ui.com_Type4.com_Task.txt_Time.text = TimeHelper.GetDurationText(scratchOffConfig.TaskBeginTime, scratchOffConfig.TaskEndTime);
		bool flag = false;
		foreach (KeyValuePair<int, BaseTaskData> item in curType4ActivityData.taskDataDict)
		{
			if (item.Value.ValidityTime())
			{
				flag = true;
				break;
			}
		}
		RefreshMainHeroType4();
		if (flag)
		{
			RefreshTaskList_Type4();
			base.ui.com_Type4.com_Task.list_Task.scrollPane.percY = 0f;
			base.ui.com_Type4.com_Task.taskValid.selectedIndex = 0;
		}
		else
		{
			base.ui.com_Type4.com_Task.language.selectedIndex = ((GameSettings.languageType != LanguageType.SimplifiedChinese) ? 1 : 0);
			base.ui.com_Type4.com_Task.taskValid.selectedIndex = 1;
		}
		int count = curType4ActivityData.PoolData.Count;
		int count2 = curType4ActivityData.record.Count;
		base.ui.com_Type4.com_Task.com_ScratchOff.txt_Progress.SetVar("value", count2.ToString()).SetVar("max", count.ToString()).FlushVars();
		base.ui.com_Type4.com_Task.list_Preview.numItems = curType4ActivityData.ScratchOffConfig.Rewards.Count;
		CommonUIManager.RendererLitItem((UICom_LitItem)base.ui.com_Type4.com_Task.com_ScratchOff.btn_Item, curType4ActivityData.consumePropId, 0, showCount: false);
	}

	private string GetTaskType4Title()
	{
		ActivityScratchoffConfigure scratchOffConfig = curType4ActivityData.ScratchOffConfig;
		return GameSettings.GetDataForLanguage(scratchOffConfig.TitleImages[1], scratchOffConfig.TitleImages[2], scratchOffConfig.TitleImages[0], scratchOffConfig.TitleImages[3]);
	}

	private void RendererType4ActivityTask(int index, GObject item)
	{
		if (curActivityTaskIds == null)
		{
			return;
		}
		UIActivity_Com_Type4_TaskLabel label = item as UIActivity_Com_Type4_TaskLabel;
		if (label == null)
		{
			return;
		}
		BaseTaskData taskData = curType4ActivityData.taskDataDict[curActivityTaskIds[index]];
		if (!taskData.ValidityTime())
		{
			item.visible = false;
			return;
		}
		item.visible = true;
		KeyValuePair<int, int> keyValuePair = taskData.rewards[0];
		CommonUIManager.RendererLitItem((UICom_LitItem)label.btn_Item, keyValuePair.Key, keyValuePair.Value, showCount: false);
		label.btn_taskStatus.Status.selectedIndex = (taskData._FinishStatus ? 2 : ((!taskData.TaskRunning) ? 1 : 0));
		label.txt_taskTitle.text = taskData.GetTaskTitle();
		label.txt_Progress.text = $"{Mathf.Min(taskData._Progress, taskData.TaskTarget)}/{taskData.TaskTarget}";
		label.txt_ItemCount.text = $"*{keyValuePair.Value}";
		label.btn_taskStatus.onClick.Set((EventCallback0)delegate
		{
			if (!taskData.TaskRunning && !taskData._FinishStatus)
			{
				label.btn_taskStatus.onClick.Retain();
				SimpleSingletonProvider<GameLogicManager>.inst.activity.OnRequestTaskReward(curType4ActivityData.activityConfig.Id, taskData).OnFinishedOnly.AddOnce(delegate
				{
					RefreshTaskList_Type4();
					label.btn_taskStatus.onClick.Release();
				});
			}
		});
	}

	private void RendererType4ActivityTaskReward(int index, GObject item)
	{
		if (item is UIActivity_Button_Type4_MainReward uIActivity_Button_Type4_MainReward)
		{
			KeyValuePair<int, int> keyValuePair = curType4ActivityData.ScratchOffConfig.Rewards.ElementAt(index);
			CommonUIManager.RendererLitItem((UICom_LitItem)uIActivity_Button_Type4_MainReward.btn_Item, keyValuePair.Key, keyValuePair.Value);
		}
	}

	private void OpenScratchOffType4()
	{
		base.ui.com_Type4.com_Task.com_ScratchOff.btn_Open.onClick.Retain();
		base.ui.com_Type4.pageType.selectedIndex = 1;
		base.ui.com_Type4.com_Task.com_ScratchOff.btn_Open.onClick.Release();
	}

	private void RefreshTaskList_Type4()
	{
		curActivityTaskIds = curType4ActivityData.GetTaskId(overView: true);
		base.ui.com_Type4.com_Task.list_Task.numItems = curActivityTaskIds.Count;
	}

	private void RefreshActivityType4ScratchOff()
	{
		if (curType4ActivityData != null)
		{
			scratchOffPoolType4 = curType4ActivityData.PoolData;
			for (int i = 0; i < 14; i++)
			{
				RendererType4ScratchOff(i);
			}
			base.ui.com_Type4.com_Scratchoff.list_Preview.numItems = scratchOffPoolType4.Count;
			base.ui.com_Type4.com_Scratchoff.list_Preview.scrollPane.percX = 0f;
			ActivityScratchoffConfigure scratchOffConfig = curType4ActivityData.ScratchOffConfig;
			base.ui.com_Type4.com_Scratchoff.txt_Time.text = TimeHelper.GetDurationText(scratchOffConfig.BeginTime, scratchOffConfig.EndTime);
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(curType4ActivityData.consumePropId);
			CommonUIManager.RendererLitItem((UICom_LitItem)base.ui.com_Type4.com_Scratchoff.btn_PropItem, curType4ActivityData.consumePropId, itemCount, showCount: false);
		}
	}

	private void RendererType4ScratchOffPreview(int index, GObject item)
	{
		if (item is UIActivity_Button_Type4_ScratchoffPreReward uIActivity_Button_Type4_ScratchoffPreReward)
		{
			ActivityScratchoffPoolConfigureItem activityScratchoffPoolConfigureItem = scratchOffPoolType4[index];
			uIActivity_Button_Type4_ScratchoffPreReward.status.selectedIndex = (curType4ActivityData.IsFinishItem(activityScratchoffPoolConfigureItem.Index) ? 1 : 0);
			CommonUIManager.RendererLitItem((UICom_LitItem)uIActivity_Button_Type4_ScratchoffPreReward.btn_Item, activityScratchoffPoolConfigureItem.ItemID, activityScratchoffPoolConfigureItem.ItemNum);
		}
	}

	private void RendererType4ScratchOff(int index)
	{
		UIActivity_Button_Type4_Scratchoff_Item _item = GetScratchOffItemType4(index);
		if (_item == null)
		{
			return;
		}
		if (curType4ActivityData.record.ContainsKey(index))
		{
			ActivityScratchoffPoolConfigureItem recordConfig = curType4ActivityData.GetRecordConfig(index);
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
				if (SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(curType4ActivityData.consumePropId) < curType4ActivityData.consumeNumber)
				{
					ItemInfoConfigure itemInfoConfigure2 = curType4ActivityData.consumePropId.GetItemInfoConfigure();
					SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(string.Format(1048.GetLocal(UIStringType.Message), itemInfoConfigure2.NameID.GetLocal(UIStringType.Item)));
				}
				else if (!LocalCache.ActivityIds.Contains(curType4ActivityData.activityConfig.Id))
				{
					SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(1127.GetLocal(UIStringType.Message), curType4ActivityData.consumeNumber), delegate
					{
						if (SimpleSingletonProvider<UIManager>.inst.messageBox.GetDoubleStatus())
						{
							LocalCache.UpdateActivityDoubleStatusCache(curType4ActivityData.activityConfig.Id);
						}
						RequestType4ScratchCardC2S(_item, index);
					}, null, doubleStatus: true).Forget();
				}
				else
				{
					RequestType4ScratchCardC2S(_item, index);
				}
			}
		});
	}

	private void RequestType4ScratchCardC2S(UIActivity_Button_Type4_Scratchoff_Item _item, int index)
	{
		_item.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.activity.RequestScratchCardC2S(curType4ActivityData.activityConfig.Id, index).OnFinishedOnly.AddOnce(delegate
		{
			_item.onClick.Release();
			RendererType4ScratchOff(index);
			base.ui.com_Type4.com_Scratchoff.list_Preview.numItems = scratchOffPoolType4.Count;
		});
	}

	private async UniTask ShowScratchOffType4Result(int index)
	{
		ActivityScratchoffPoolConfigureItem recordConfig = curType4ActivityData.GetRecordConfig(index);
		UIActivity_Button_Type4_Scratchoff_Item _item = GetScratchOffItemType4(index);
		if (_item != null)
		{
			ItemInfoConfigure itemInfoConfigure = recordConfig.ItemID.GetItemInfoConfigure();
			_item.loader_Reward.url = itemInfoConfigure.ShowIcon;
			_item.txt_itemNum.text = recordConfig.ItemNum.ToString();
			_item.Status.selectedIndex = 1;
			Stage.inst.PlayOneShotSound(804);
			await UniTask.WaitUntil(() => !_item.showScratchOff.playing);
			_item.Status.selectedIndex = 2;
		}
	}

	private async void RefreshMainHeroType4()
	{
		System.Random random = new System.Random();
		int randomIndex = random.Next(activityCharacter.Length);
		Color32 Color = ((randomIndex == 1) ? new Color32(251, 66, byte.MaxValue, byte.MaxValue) : new Color32(43, 165, byte.MaxValue, byte.MaxValue));
		await SimpleSingletonProvider<TextureManager>.inst.AsyncLoad($"UT_Hero_Card_{activityCharacter[randomIndex]}", delegate(NTexture texture)
		{
			base.ui.com_Type4.com_Task.loader_Icon.texture = texture;
		}, null);
		if (base.ui.com_Type4.com_Task.loader_Icon.texture != null)
		{
			GLoader loader_Icon_Color = base.ui.com_Type4.com_Task.loader_Icon_Color;
			loader_Icon_Color.texture = await ShadowTextureCreator.CreateShadowTexture(base.ui.com_Type4.com_Task.loader_Icon.texture, Color, 1f, 0f, 0);
			base.ui.com_Type4.com_Task.Interaction.SetHook("PlayVoice", delegate
			{
				Stage.inst.PlayOneShotSound(activityCharacterVoiceId[randomIndex]);
			});
			base.ui.com_Type4.com_Task.Interaction.Play();
		}
	}

	private UIActivity_Button_Type4_Scratchoff_Item GetScratchOffItemType4(int index)
	{
		return (UIActivity_Button_Type4_Scratchoff_Item)base.ui.com_Type4.com_Scratchoff.GetChild($"btn_Reward_{index + 1}");
	}
}
