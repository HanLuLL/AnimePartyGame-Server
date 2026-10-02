using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class UIBattleInfo_Com_PVEProgress : GComponent
{
	public readonly List<PVEProgressData> PVE_ProgressData = new List<PVEProgressData>();

	private List<UICom_Card> mapEventCards = new List<UICom_Card>();

	private int progressMultiple = 1;

	private List<MapMissionTargetData> mapMissionTargetsData;

	public Controller taskStatus;

	public Controller showTaskInfo;

	public GList list_Task;

	public UIBattleInfo_Button_PVETask btn_Task;

	public UIBattleInfo_Com_PVEProgressList com_list;

	public UIBattleinfo_Com_Speedani com_CutShow;

	public const string URL = "ui://fxejlqlfr1pj8m";

	private int gameProgress => SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.GameProgress;

	private int gameMaxProgress => SimpleSingletonProvider<GameLogicManager>.inst.battle.CurMaxGameProgress;

	private int mapID
	{
		get
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
			{
				return SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.MapId;
			}
			return 0;
		}
	}

	private int difficultyIndex
	{
		get
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.room.IsInRoom)
			{
				return SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Difficulty;
			}
			return 0;
		}
	}

	public MapInfoConfigure mapInfo
	{
		get
		{
			if (!StaticConfigure.Map.InfoDict.ContainsKey(mapID))
			{
				return null;
			}
			return StaticConfigure.Map.InfoDict[mapID];
		}
	}

	private MapGameDifficultyConfigureItem difficultyConfigure => SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo?.MapDifficultyId.GetMapGameDifficultyItems()?[difficultyIndex];

	public void InitComponents()
	{
		com_list.list_Progress.SetVirtual();
		com_list.list_Progress.itemRenderer = RendererProgressItem;
		list_Task.itemRenderer = RendererTaskInfo;
	}

	public void Refresh()
	{
		if (difficultyConfigure != null)
		{
			InitPVEProgressEvent();
			RefreshProgress();
			UpdateTaskStatus();
		}
	}

	public void AddEvent()
	{
		btn_Task.onClick.Add(SwitchTaskShow);
	}

	public void RemoveEvent()
	{
		btn_Task.onClick.Remove(SwitchTaskShow);
	}

	public void AddListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.progressChange.AddListener(UpdateProgress);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.mapMissionChange.AddListener(UpdateTaskStatus);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.pveProgressMultiple.AddListener(ChangePveProgressMultiple);
	}

	public void RemoveListener()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.progressChange.RemoveListener(UpdateProgress);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.mapMissionChange.RemoveListener(UpdateTaskStatus);
		SimpleSingletonProvider<GameLogicManager>.inst.battle.signal.pveProgressMultiple.RemoveListener(ChangePveProgressMultiple);
	}

	public void Close()
	{
		foreach (UICom_Card mapEventCard in mapEventCards)
		{
			if (mapEventCard != null)
			{
				GRoot.inst.HidePopup(mapEventCard);
			}
		}
		mapEventCards.Clear();
	}

	public void DisposeProgress()
	{
		foreach (UICom_Card mapEventCard in mapEventCards)
		{
			if (mapEventCard != null)
			{
				GRoot.inst.HidePopup(mapEventCard);
				mapEventCard?.Dispose();
			}
		}
		mapEventCards.Clear();
	}

	private void RendererProgressItem(int index, GObject item)
	{
		UIBattleInfo_Button_PVEProgressItem progress = item as UIBattleInfo_Button_PVEProgressItem;
		if (progress == null)
		{
			return;
		}
		if (index < PVE_ProgressData.Count)
		{
			PVEProgressData progressData = PVE_ProgressData[index];
			progress.title = (progressMultiple * progressData.progress).ToString();
			progress.Refresh(progressData, gameProgress, gameMaxProgress);
			List<PVEProgressData> pVE_ProgressData = PVE_ProgressData;
			if (pVE_ProgressData[pVE_ProgressData.Count - 1].progress == progressData.progress)
			{
				progress.type.selectedIndex = 2;
			}
			progress.onClick.Set((EventCallback0)delegate
			{
				ShowMapEventCard(progress, progressData);
			});
		}
		else
		{
			progress.type.selectedIndex = 0;
			progress.title = (progressMultiple * index).ToString();
		}
	}

	private void ShowMapEventCard(UIBattleInfo_Button_PVEProgressItem progress, PVEProgressData progressData)
	{
		if (progressData?.mapEventIds == null || progressData.mapEventIds.Count == 0 || progress.type.selectedIndex != 1)
		{
			return;
		}
		progress.onClick.Retain();
		float num = 450 * progressData.mapEventIds.Count;
		for (int i = 0; i < progressData.mapEventIds.Count; i++)
		{
			int key = progressData.mapEventIds[i];
			if (StaticConfigure.MapEvent.InfoDict.TryGetValue(key, out var value) && StaticConfigure.MapEvent.MapEventCardDict.TryGetValue(value.CardID, out var value2))
			{
				if (mapEventCards.Count <= i)
				{
					mapEventCards.Add(UICom_Card.CreateInstance());
				}
				UICom_Card uICom_Card = mapEventCards[i];
				string cardIndex = ((value2.CardNumb == 0) ? "" : value2.CardNumb.GetLocal(UIStringType.MapEvent));
				string cardTips = ((value2.CommentId == 0) ? "" : value2.CommentId.GetLocal(UIStringType.MapEvent));
				CommonUIManager.RendererCard(uICom_Card, value2.GetImage(), value2.NameID.GetLocal(UIStringType.MapEvent), value2.DescId.GetLocal(UIStringType.MapEvent), cardIndex, cardTips, 0, value2.CardType);
				GRoot.inst.ShowPopup(uICom_Card, progress, PopupDirection.Down);
				uICom_Card.SetXY(uICom_Card.x - num / 2f + (float)(i * 450), uICom_Card.y);
			}
		}
		progress.onClick.Release();
	}

	private void UpdateProgress()
	{
		if (PVE_ProgressData.Count <= 0)
		{
			return;
		}
		float num = com_list.list_Progress.GetChildAt(0).width;
		for (int i = 0; i < PVE_ProgressData.Count; i++)
		{
			int num2 = com_list.list_Progress.ItemIndexToChildIndex(i);
			if (num2 >= 0 && com_list.list_Progress._children.Count > num2 && com_list.list_Progress.GetChildAt(num2) is UIBattleInfo_Button_PVEProgressItem uIBattleInfo_Button_PVEProgressItem)
			{
				uIBattleInfo_Button_PVEProgressItem.status.selectedIndex = ((gameProgress == PVE_ProgressData[i].progress) ? 1 : 0);
				uIBattleInfo_Button_PVEProgressItem.grayed = PVE_ProgressData[i].progress < gameProgress;
				if (PVE_ProgressData[i].progress < gameMaxProgress)
				{
					uIBattleInfo_Button_PVEProgressItem.type.selectedIndex = 0;
				}
			}
			if (gameProgress == PVE_ProgressData[i].progress)
			{
				int num3 = 0;
				if (gameProgress > 3)
				{
					num3 = i - 2;
				}
				com_list.list_Progress.scrollPane.SetPosX((float)num3 * num, ani: true);
			}
		}
	}

	private void ChangePveProgressMultiple(int multiple)
	{
		if (multiple == progressMultiple)
		{
			return;
		}
		com_CutShow.visible = true;
		com_CutShow.Cut_in.SetHook("ChangeEffect", delegate
		{
			progressMultiple = multiple;
			com_list.list_Progress.RefreshVirtualList();
		});
		com_CutShow.Cut_in.Play(delegate
		{
			if (progressMultiple == 1)
			{
				com_CutShow.visible = false;
			}
		});
	}

	private void UpdateTaskStatus()
	{
		List<MapMissionData> mapMissionsData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetMapMissionsData();
		if (mapMissionsData == null || mapMissionsData.Count == 0)
		{
			taskStatus.selectedIndex = 0;
			showTaskInfo.selectedIndex = 0;
			btn_Task.selected = false;
		}
		else
		{
			mapMissionTargetsData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetMapMissionsData(mapMissionsData);
			taskStatus.selectedIndex = 1;
			list_Task.numItems = mapMissionTargetsData.Count;
			list_Task.ResizeToFit();
		}
	}

	private void SwitchTaskShow()
	{
		btn_Task.onClick.Retain();
		showTaskInfo.selectedIndex = (btn_Task.selected ? 1 : 0);
		btn_Task.onClick.Release();
	}

	private void RendererTaskInfo(int index, GObject item)
	{
		if (item is UIBattleInfo_Com_PVETaskItem uIBattleInfo_Com_PVETaskItem)
		{
			MapMissionTargetData mapMissionTargetData = mapMissionTargetsData[index];
			PVEMissionInfoConfigure pVEMissionInfo = mapMissionTargetData.PVEMissionInfo;
			uIBattleInfo_Com_PVETaskItem.MapMissionId = mapMissionTargetData.MapMissionId;
			uIBattleInfo_Com_PVETaskItem.txt_Title.text = mapMissionTargetData.GetTitle();
			uIBattleInfo_Com_PVETaskItem.txt_Progress.text = mapMissionTargetData.GetProgressDesc();
			uIBattleInfo_Com_PVETaskItem.txt_Desc.text = pVEMissionInfo.RewardDescID.GetLocal(UIStringType.PVEMission);
			if (pVEMissionInfo.FailedDescID > 0)
			{
				uIBattleInfo_Com_PVETaskItem.txt_Desc_Failed.visible = true;
				uIBattleInfo_Com_PVETaskItem.txt_Desc_Failed.text = pVEMissionInfo.FailedDescID.GetLocal(UIStringType.PVEMission);
			}
			else
			{
				uIBattleInfo_Com_PVETaskItem.txt_Desc_Failed.visible = false;
				uIBattleInfo_Com_PVETaskItem.txt_Desc_Failed.text = "";
			}
		}
	}

	private void InitPVEProgressEvent()
	{
		PVE_ProgressData.Clear();
		int progressLimit = difficultyConfigure.ProgressLimit;
		for (int i = 0; i < progressLimit; i++)
		{
			PVEProgressData item = new PVEProgressData(i + 1);
			PVE_ProgressData.Add(item);
		}
		List<MapEventInfoConfigure> mapEventInfoConfigures = difficultyConfigure.MapEventInfoConfigures;
		for (int j = 0; j < mapEventInfoConfigures.Count; j++)
		{
			if (mapEventInfoConfigures[j].Triggerparams.Count == 0)
			{
				continue;
			}
			int progress = mapEventInfoConfigures[j].Triggerparams[0];
			PVEProgressData pVEProgressData = PVE_ProgressData.Find((PVEProgressData progressData) => progressData.progress == progress);
			if (pVEProgressData != null)
			{
				PVEProgressData pVEProgressData2 = pVEProgressData;
				if (pVEProgressData2.mapEventIds == null)
				{
					pVEProgressData2.mapEventIds = new List<int>();
				}
				pVEProgressData.mapEventIds.Add(mapEventInfoConfigures[j].Id);
			}
		}
		RoomInfo curRoomInfo = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo;
		if (curRoomInfo?.info.DelayProgressMapEvent != null)
		{
			AddProgressMapEvent(curRoomInfo.info.DelayProgressMapEvent);
		}
	}

	public void RefreshProgress()
	{
		com_list.list_Progress.numItems = 0;
		com_list.list_Progress.numItems = PVE_ProgressData.Count;
		com_list.list_Progress.scrollPane.touchEffect = false;
		com_list.list_Progress.scrollPane.mouseWheelEnabled = false;
		UpdateProgress();
	}

	public void AddProgressMapEvent(MapField<int, int> delayProgressMapEvent)
	{
		if (delayProgressMapEvent == null || delayProgressMapEvent.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<int, int> item in delayProgressMapEvent)
		{
			int key = item.Key;
			int progress = item.Value;
			PVEProgressData pVEProgressData = PVE_ProgressData.Find((PVEProgressData progressData) => progressData.progress == progress);
			if (pVEProgressData != null)
			{
				PVEProgressData pVEProgressData2 = pVEProgressData;
				if (pVEProgressData2.mapEventIds == null)
				{
					pVEProgressData2.mapEventIds = new List<int>();
				}
				if (!pVEProgressData.mapEventIds.Contains(key))
				{
					pVEProgressData.mapEventIds.Add(key);
				}
			}
		}
	}

	public void OnMarkTipShow()
	{
		btn_Task?.OnMarkTipShow();
		List<GObject> children = com_list.list_Progress._children;
		for (int i = 0; i < children.Count; i++)
		{
			if (children[i] is UIBattleInfo_Button_PVEProgressItem uIBattleInfo_Button_PVEProgressItem)
			{
				uIBattleInfo_Button_PVEProgressItem.OnMarkTipShow();
			}
		}
	}

	public void OnMarkTipHide()
	{
		btn_Task?.OnMarkTipHide();
		List<GObject> children = com_list.list_Progress._children;
		for (int i = 0; i < children.Count; i++)
		{
			if (children[i] is UIBattleInfo_Button_PVEProgressItem uIBattleInfo_Button_PVEProgressItem)
			{
				uIBattleInfo_Button_PVEProgressItem.OnMarkTipHide();
			}
		}
	}

	public static UIBattleInfo_Com_PVEProgress CreateInstance()
	{
		return (UIBattleInfo_Com_PVEProgress)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_PVEProgress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		taskStatus = GetControllerAt(0);
		showTaskInfo = GetControllerAt(1);
		list_Task = (GList)GetChildAt(2);
		btn_Task = (UIBattleInfo_Button_PVETask)GetChildAt(4);
		com_list = (UIBattleInfo_Com_PVEProgressList)GetChildAt(5);
		com_CutShow = (UIBattleinfo_Com_Speedani)GetChildAt(6);
	}
}
