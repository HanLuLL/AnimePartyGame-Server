using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;

namespace UI;

public class RookieTaskInfoWindow : BaseWindow
{
	private Dictionary<int, int> _rewardItems = new Dictionary<int, int>();

	private List<int> _showRewardItems;

	public RookieTaskInfoWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIRookieTaskInfoWindow.CreateInstance();
		base.OnInit();
		_rewardItems.Clear();
		for (int i = 0; i < SimpleSingletonProvider<GameLogicManager>.inst.task.rookieTaskData.TaskGroupDatas.Count; i++)
		{
			RookieGroupTaskData rookieGroupTaskData = SimpleSingletonProvider<GameLogicManager>.inst.task.rookieTaskData.TaskGroupDatas[i];
			foreach (MissionDataConfigure groupTaskConfig in rookieGroupTaskData.GetGroupTaskConfigs())
			{
				GetRewardItems(groupTaskConfig);
			}
			GetRewardItems(rookieGroupTaskData.GetFinalTaskConfig());
		}
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
		if (base.contentPane is UIRookieTaskInfoWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIRookieTaskInfoWindow)
		{
			uIRookieTaskInfoWindow.list_Item.itemRenderer = RendererItem;
			bottom.btn_Sure_Only.onClick.Add(OnClickHide);
			bottom.closeButton.onClick.Add(OnClickHide);
			bottom.btn_Cancel.onClick.Add(OnClickHide);
			uIRookieTaskInfoWindow.mohu.onClick.Add(OnClickHide);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIRookieTaskInfoWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIRookieTaskInfoWindow)
		{
			uIRookieTaskInfoWindow.list_Item.itemRenderer = null;
			bottom.btn_Sure_Only.onClick.Remove(OnClickHide);
			bottom.closeButton.onClick.Remove(OnClickHide);
			bottom.btn_Cancel.onClick.Remove(OnClickHide);
			uIRookieTaskInfoWindow.mohu.onClick.Remove(OnClickHide);
		}
	}

	public void OnClickHide()
	{
		if (base.contentPane is UIRookieTaskInfoWindow { bottom: UICom_PopUpWindow_Bottom bottom } uIRookieTaskInfoWindow)
		{
			uIRookieTaskInfoWindow.mohu.onClick.Retain();
			bottom.btn_Sure_Only.onClick.Retain();
			bottom.closeButton.onClick.Retain();
			bottom.btn_Cancel.onClick.Retain();
			Hide();
			bottom.btn_Sure_Only.onClick.Release();
			bottom.closeButton.onClick.Release();
			bottom.btn_Cancel.onClick.Release();
			uIRookieTaskInfoWindow.mohu.onClick.Release();
		}
	}

	public async UniTask ShowRewardDetail()
	{
		await TryShowAsync();
		if (base.contentPane is UIRookieTaskInfoWindow uIRookieTaskInfoWindow)
		{
			_showRewardItems = new List<int>(_rewardItems.Keys);
			uIRookieTaskInfoWindow.list_Item.numItems = _showRewardItems.Count;
		}
	}

	private void RendererItem(int index, GObject item)
	{
		if (item is UICom_LitItem item2)
		{
			CommonUIManager.RendererLitItem(item2, _showRewardItems[index], _rewardItems[_showRewardItems[index]]);
		}
	}

	private void GetRewardItems(MissionDataConfigure missionDataConfigure)
	{
		foreach (KeyValuePair<int, int> item in missionDataConfigure.Reward)
		{
			ItemInfoConfigure itemInfoConfigure = item.Key.GetItemInfoConfigure();
			if (!itemInfoConfigure.IsAutoOpen)
			{
				_rewardItems.TryAdd(item.Key, 0);
				_rewardItems[item.Key] += item.Value;
				continue;
			}
			foreach (int item2 in itemInfoConfigure.SubMeterID.GetChestInfoConfigure().RandomReward)
			{
				foreach (ChestRandomRewardConfigureItem chestRandomRewardConfigureItem in item2.GetChestRandomRewardConfigure().ChestRandomRewardConfigureItems)
				{
					_rewardItems.TryAdd(chestRandomRewardConfigureItem.ItemID, 0);
					_rewardItems[chestRandomRewardConfigureItem.ItemID] += chestRandomRewardConfigureItem.MaxCount;
				}
			}
		}
	}
}
