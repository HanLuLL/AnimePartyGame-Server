using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class CampaignPanel : BasePanel<UICampaignPanel>
{
	private List<CampaignLevelConfigure> _currentChapterLevels;

	private int _firstNotPassLevelId;

	private CampaignLevelConfigure _currentLevelConfigure;

	private UICampaign_Button_Level _currentLevelButton;

	private const int UnselectSTATE = 0;

	private const int SelectSTATE = 1;

	private UICampaign_Button_Chapter _curSelectedButton;

	public CampaignPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UICampaignPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		RepeatedField<CampaignChapterConfigure> chapters = StaticConfigure.Campaign.Chapters;
		CampaignChapterConfigure safeByIndex = chapters.GetSafeByIndex(0);
		if (safeByIndex != null)
		{
			RefreshChapterButton(base.ui.btn_Novice, "2 / 2", safeByIndex);
		}
		CampaignChapterConfigure safeByIndex2 = chapters.GetSafeByIndex(1);
		(string, bool) tuple = (null, false);
		if (safeByIndex2 != null)
		{
			tuple = SimpleSingletonProvider<GameLogicManager>.inst.campaign.GetLevelStatus(safeByIndex2.Id, MapModeType.CampaignPve);
			RefreshChapterButton(base.ui.btn_PVE, tuple.Item1, safeByIndex2);
		}
		CampaignChapterConfigure safeByIndex3 = chapters.GetSafeByIndex(2);
		(string, bool) tuple2 = (null, false);
		if (safeByIndex3 != null)
		{
			tuple2 = SimpleSingletonProvider<GameLogicManager>.inst.campaign.GetLevelStatus(safeByIndex3.Id, MapModeType.CampaignPvp);
			RefreshChapterButton(base.ui.btn_PVP, tuple2.Item1, safeByIndex3);
		}
		if (!tuple.Item2)
		{
			base.ui.btn_PVE.onClick.Call();
		}
		else if (!tuple2.Item2)
		{
			base.ui.btn_PVP.onClick.Call();
		}
		else
		{
			base.ui.btn_Novice.onClick.Call();
		}
	}

	private void RefreshChapterButton(UICampaign_Button_Chapter btn, string progress, CampaignChapterConfigure chapterConfigure)
	{
		btn.txt_Progress.text = progress;
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.list_Level.itemRenderer = RendererLevel;
	}

	public override void Refresh()
	{
		base.Refresh();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.Back.onClick.Add(OnReturnPanel);
		base.ui.btn_CreateRoom.onClick.Add(OnRequestCreateRoom);
		base.ui.btn_Novice.onClick.Add(OnShowNoviceLevel);
		base.ui.btn_PVE.onClick.Add(OnShowPVELevel);
		base.ui.btn_PVP.onClick.Add(OnShowPVPLevel);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.Back.onClick.Remove(OnReturnPanel);
		base.ui.btn_CreateRoom.onClick.Remove(OnRequestCreateRoom);
		base.ui.btn_Novice.onClick.Remove(OnShowNoviceLevel);
		base.ui.btn_PVE.onClick.Remove(OnShowPVELevel);
		base.ui.btn_PVP.onClick.Remove(OnShowPVPLevel);
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.campaign.signal.unlockLevel.AddListener(OnUnlockLevel);
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.campaign.signal.unlockLevel.RemoveListener(OnUnlockLevel);
	}

	public override void Close()
	{
		base.Close();
		_currentChapterLevels = null;
		_currentLevelConfigure = null;
		_firstNotPassLevelId = 0;
		_currentLevelButton = null;
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private async void OnReturnPanel()
	{
		base.ui.Back.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.Back.onClick.Release();
	}

	private void OnShowLevelInfo(UICampaign_Button_Level btn, CampaignLevelConfigure levelConfigure)
	{
		if (_currentLevelButton != null)
		{
			_currentLevelButton.state.selectedIndex = 0;
		}
		btn.state.selectedIndex = 1;
		_currentLevelButton = btn;
		_currentLevelConfigure = levelConfigure;
		bool campaignPass = SimpleSingletonProvider<GameLogicManager>.inst.campaign.GetCampaignPass(_currentLevelConfigure.Id);
		if (_currentLevelConfigure.MapModeType == MapModeType.Pvenovice || campaignPass || _firstNotPassLevelId == _currentLevelConfigure.Id)
		{
			base.ui.btn_CreateRoom.visible = true;
		}
		else
		{
			base.ui.btn_CreateRoom.visible = false;
		}
	}

	private void RendererLevel(int index, GObject item)
	{
		UICampaign_Button_Level levelItem = item as UICampaign_Button_Level;
		if (levelItem == null)
		{
			return;
		}
		levelItem.state.selectedIndex = 0;
		CampaignLevelConfigure levelConfig = _currentChapterLevels[index];
		int id = _currentChapterLevels[index].Id;
		if (levelConfig == null)
		{
			Debug.LogError($"关卡ID：{id}未找到配置文件！");
			return;
		}
		levelItem.number.text = levelConfig.SerialNumber.GetLocal(UIStringType.Campaign);
		levelItem.txt_name.text = levelConfig.Name.GetLocal(UIStringType.Campaign);
		levelItem.mainImage.url = levelConfig.Banner;
		levelItem.txt_VictoryConditionDesc.text = levelConfig.BannerVictoryDescription.GetLocal(UIStringType.Campaign);
		if (levelConfig.MapModeType == MapModeType.Pvenovice)
		{
			levelItem.pass.visible = false;
			levelItem.touchable = true;
			levelItem.grayed = false;
		}
		else
		{
			bool campaignPass = SimpleSingletonProvider<GameLogicManager>.inst.campaign.GetCampaignPass(id);
			levelItem.pass.visible = campaignPass;
			levelItem.touchable = campaignPass || id == _firstNotPassLevelId;
			levelItem.grayed = !levelItem.touchable;
		}
		if (levelConfig.FirstPassReward.Count > 0 && levelItem.RewardItem is UICom_LitItem item2)
		{
			KeyValuePair<int, int> keyValuePair = levelConfig.FirstPassReward.First();
			CommonUIManager.RendererLitItem(item2, keyValuePair.Key, keyValuePair.Value);
			levelItem.isReward.selectedIndex = 0;
		}
		else
		{
			levelItem.isReward.selectedIndex = 1;
		}
		levelItem.onClick.Set((EventCallback0)delegate
		{
			levelItem.onClick.Retain();
			OnShowLevelInfo(levelItem, levelConfig);
			levelItem.onClick.Release();
		});
	}

	private void OnUnlockLevel(int level, bool state)
	{
		if (_curSelectedButton != null)
		{
			_curSelectedButton.onClick.Call();
		}
	}

	private void LocateFirstNotPassLevel()
	{
		_firstNotPassLevelId = GetFirstNotPassLevelId();
		int level;
		if (_firstNotPassLevelId == 0)
		{
			List<CampaignLevelConfigure> currentChapterLevels = _currentChapterLevels;
			level = currentChapterLevels[currentChapterLevels.Count - 1].Id;
			_firstNotPassLevelId = level;
		}
		else
		{
			level = _firstNotPassLevelId;
		}
		base.ui.list_Level.numItems = _currentChapterLevels.Count;
		int index = _currentChapterLevels.FindIndex((CampaignLevelConfigure x) => x.Id == level);
		base.ui.list_Level.GetChildAt(index)?.onClick.Call();
	}

	private async void OnRequestCreateRoom()
	{
		if (_currentLevelConfigure.MapModeType == MapModeType.Pvenovice)
		{
			base.ui.btn_CreateRoom.onClick.Retain();
			await SimpleSingletonProvider<GameLogicManager>.inst.tutorial.StartTutorialById(_currentLevelConfigure.Id);
			if (base.ui != null)
			{
				base.ui.btn_CreateRoom.onClick.Release();
			}
		}
		else
		{
			if (_currentLevelConfigure == null || _currentLevelConfigure.MapID == 0 || !SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
			{
				return;
			}
			base.ui.btn_CreateRoom.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.room.CreateCampaignRoom(_currentLevelConfigure.MapID).OnFinished.AddOnce(delegate(RPCAsyncResult result)
			{
				if (result.errId == 0)
				{
					base.ui.btn_CreateRoom.onClick.Release();
				}
			});
		}
	}

	private int GetFirstNotPassLevelId()
	{
		foreach (CampaignLevelConfigure currentChapterLevel in _currentChapterLevels)
		{
			if (!SimpleSingletonProvider<GameLogicManager>.inst.campaign.GetCampaignPass(currentChapterLevel.Id))
			{
				return currentChapterLevel.Id;
			}
		}
		return 0;
	}

	private void OnShowNoviceLevel()
	{
		base.ui.btn_PVP.onClick.Retain();
		RefreshChapterLevel(0, base.ui.btn_Novice);
		_curSelectedButton.status.selectedIndex = 1;
		base.ui.btn_PVP.onClick.Release();
	}

	private void OnShowPVELevel()
	{
		base.ui.btn_PVP.onClick.Retain();
		RefreshChapterLevel(1, base.ui.btn_PVE);
		_curSelectedButton = base.ui.btn_PVE;
		_curSelectedButton.status.selectedIndex = 1;
		base.ui.btn_PVP.onClick.Release();
	}

	private void OnShowPVPLevel()
	{
		base.ui.btn_PVP.onClick.Retain();
		RefreshChapterLevel(2, base.ui.btn_PVP);
		_curSelectedButton = base.ui.btn_PVP;
		_curSelectedButton.status.selectedIndex = 1;
		base.ui.btn_PVP.onClick.Release();
	}

	private void RefreshChapterLevel(int chapterIndex, UICampaign_Button_Chapter selectedButton)
	{
		if (_curSelectedButton != null)
		{
			base.ui.AddChildAt(_curSelectedButton, 0);
			_curSelectedButton.status.selectedIndex = 0;
		}
		_curSelectedButton = selectedButton;
		base.ui.AddChild(_curSelectedButton);
		CampaignChapterConfigure safeByIndex = StaticConfigure.Campaign.Chapters.GetSafeByIndex(chapterIndex);
		if (safeByIndex == null)
		{
			Debug.LogError($"未找到索引Index: {chapterIndex} 的章节配置列表！！！");
			return;
		}
		int id = safeByIndex.Id;
		if (!SimpleSingletonProvider<GameLogicManager>.inst.campaign.chapterLevelsMap.TryGetValue(id, out _currentChapterLevels))
		{
			Debug.LogError($"未找到章节 {id} 的关卡配置列表！！！");
			return;
		}
		_firstNotPassLevelId = GetFirstNotPassLevelId();
		LocateFirstNotPassLevel();
	}

	public async UniTask<(Vector2, float, float)> ShowStartCampaignMask()
	{
		_FocusStatus = false;
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => base.ui.Cut_in.playing, SimpleSingletonProvider<UIManager>.inst.tutorial.Hide);
		Vector2 pt = base.ui.btn_CreateRoom.LocalToGlobal(Vector2.zero);
		Vector2 vector = GRoot.inst.GlobalToLocal(pt);
		float width = base.ui.btn_CreateRoom.width;
		float height = base.ui.btn_CreateRoom.height;
		SimpleSingletonProvider<UIManager>.inst.tutorial.ShowGuideMask(vector, width, height, isRect: true);
		return (vector, width, height);
	}

	public void FireClickStartCampaignLevel()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.match.CheckOperateForMatch())
		{
			return;
		}
		base.ui.btn_CreateRoom.FireClick(downEffect: true);
		base.ui.btn_CreateRoom.onClick.Retain();
		SimpleSingletonProvider<GameLogicManager>.inst.room.CreateCampaignRoom(101).OnFinished.AddOnce(delegate(RPCAsyncResult result)
		{
			if (result.errId == 0)
			{
				base.ui.btn_CreateRoom.onClick.Release();
			}
		});
	}
}
