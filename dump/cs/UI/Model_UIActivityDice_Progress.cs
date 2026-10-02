using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;

namespace UI;

public class Model_UIActivityDice_Progress : BaseModel<UIActivityDice_Button_Progress>
{
	private MissionData missionData;

	private bool isAddMissionDataListener;

	public int Target => missionData.TaskTarget;

	public Model_UIActivityDice_Progress(UIActivityDice_Button_Progress _com)
		: base(_com)
	{
	}

	public void ChangeMissionData(MissionData _data)
	{
		if (_data != missionData)
		{
			if (isAddMissionDataListener)
			{
				missionData?.StateChange?.RemoveListener(OnMissionDataChange);
				missionData = _data;
				missionData?.StateChange?.AddListener(OnMissionDataChange);
			}
			else
			{
				missionData = _data;
			}
		}
	}

	private void OnMissionDataChange(MissionData data)
	{
		Refresh();
	}

	public override void AddEvent()
	{
		com.Btn_Compelete.onClick.Add(OnBtnClick);
		com.rewardItem.onClick.Add(OnLitItemClick);
		AddMissionDataListener();
	}

	public override void RemoveEvent()
	{
		com.Btn_Compelete.onClick.Remove(OnBtnClick);
		com.rewardItem.onClick.Remove(OnLitItemClick);
		RemoveMissionDataListener();
	}

	public void AddMissionDataListener()
	{
		if (!isAddMissionDataListener)
		{
			isAddMissionDataListener = true;
			missionData?.StateChange?.AddListener(OnMissionDataChange);
		}
	}

	public void RemoveMissionDataListener()
	{
		if (isAddMissionDataListener)
		{
			isAddMissionDataListener = false;
			missionData?.StateChange?.RemoveListener(OnMissionDataChange);
		}
	}

	private void OnLitItemClick()
	{
		KeyValuePair<int, int> keyValuePair = missionData.rewards[0];
		int key = keyValuePair.Key;
		GButton rewardItem = com.rewardItem;
		int value = keyValuePair.Value;
		rewardItem.onClick.Retain();
		SimpleSingletonProvider<UIManager>.inst.propDetail.ShowWin(key, value, _Usable: true).Forget();
		rewardItem.onClick.Release();
	}

	private void OnBtnClick()
	{
		int num = ((!missionData.TaskRunning) ? 1 : 0);
		num = (missionData._FinishStatus ? 2 : num);
		if (num != 0 && num != 2)
		{
			com.Btn_Compelete.onClick.Retain();
			int id = missionData._Id;
			SimpleSingletonProvider<GameLogicManager>.inst.task.RequestActivityMissionRewardC2S(id).OnFinishedOnly.AddOnce(delegate
			{
				com.Btn_Compelete.onClick.Release();
			});
		}
	}

	public override void Refresh()
	{
		int num = ((!missionData.TaskRunning) ? 1 : 0);
		num = (missionData._FinishStatus ? 2 : num);
		bool touchable = num != 2;
		bool grayed = num == 2;
		com.Status.selectedIndex = num;
		com.Btn_Compelete.Status.selectedIndex = num;
		com.touchable = touchable;
		com.grayed = grayed;
		com.title.text = $"{missionData.TaskTarget}";
		KeyValuePair<int, int> keyValuePair = missionData.rewards[0];
		int key = keyValuePair.Key;
		int value = keyValuePair.Value;
		UICom_LitItem obj = (UICom_LitItem)com.rewardItem;
		ItemInfoConfigure itemInfoConfigure = key.GetItemInfoConfigure();
		obj.qualityType.selectedIndex = (int)itemInfoConfigure.QualityType;
		obj.loader_Icon.url = itemInfoConfigure.ShowIcon;
		obj.txt_itemNum.text = value.ToString();
		obj.isShowNum.selectedIndex = 0;
	}
}
