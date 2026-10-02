using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class HomePanel : BasePanel<UIHomePanel>
{
	private bool systemStatusLock;

	private const float BgScaleStep = 0.02f;

	private bool isDragReady;

	private bool isDragging;

	private bool isPinching;

	private Vector2 touchStartGlobalPos;

	private Vector2 loaderStartPos;

	private float pinchLastDistance;

	private Vector2 pinchContentAnchor;

	private const float CustomMaxScale = 2f;

	public HomePanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIHomePanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		base.ui.Cut_in.Play();
		ShowStartVideo();
	}

	protected override void FullScreen()
	{
		base.FullScreen();
		if (base.ui != null)
		{
			SetBGFullScreen();
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.online)
		{
			RefreshSystemStatus();
		}
		base.ui.com_Banner.Show();
		base.ui.com_ActivityBanner.Show();
		base.ui.com_Activity.Show();
		base.ui.com_expand.Show();
		base.ui.Icon.FullScreen();
		base.ui.Com_Tip.Text_DragTip.text = 1124.GetLocal(UIStringType.Message);
		base.ui.Com_Tip.Text_ScaleTip.text = 1125.GetLocal(UIStringType.Message);
		SimpleSingletonProvider<UIManager>.inst.CreditWarning.ShowExemptedTip().Forget();
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.com_Banner.InitComponent();
		base.ui.com_ActivityBanner.InitComponent();
		base.ui.com_Activity.InitComponent();
		base.ui.com_expand.InitComponent();
	}

	public override void Refresh()
	{
		base.Refresh();
		base.ui.btn_GameOnline.angelMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
		base.ui.btn_GameOnline.country.selectedIndex = GameSettings.COUNTRY;
		base.ui.btn_Mall.angelMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
		base.ui.btn_GameOnline.logoVersion.selectedIndex = ((GameSettings.languageType != LanguageType.SimplifiedChinese) ? 1 : 0);
		RefreshActivityHubData();
		Show_BgSetting();
		RefreshGacha();
		RefreshBattlePass();
		SimpleSingletonProvider<GameLogicManager>.inst.RegisterRed();
		base.ui.com_expand.RefreshItems();
		SimpleSingletonProvider<GameLogicManager>.inst.comeback?.RegisterRed();
		base.ui.btn_DouYinReward.visible = false;
		base.ui.btn_DouYinGroupChat.visible = false;
		base.ui.btn_DouYinShareReward.visible = false;
		base.ui.btn_DouYinSubscribeReward.visible = false;
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_GameOnline.onClick.Add(RequestOpenRoomList);
		base.ui.btn_PlayerGuide.onClick.Add(OpenPlayerGuide);
		base.ui.btn_Dictionary.onClick.Add(OpenDictionary);
		base.ui.btn_Mall.onClick.Add(OpenShoppingPanel);
		base.ui.btn_Gacha.onClick.Add(OpenGachaPanel);
		base.ui.com_Banner.AddEvent();
		base.ui.com_BattlePass.loader_Fold.onClick.Add(OpenBattlePassInfo);
		base.ui.com_BattlePass.btn_OpenBattlePass.onClick.Add(OpenBattlePass);
		base.ui.com_ActivityBanner.AddEvent();
		base.ui.com_Activity.AddEvent();
		base.ui.com_expand.AddEvent();
		base.ui.Hide.onChanged.Add(ChangeHideState);
		base.ui.BGset.onChanged.Add(ChangeBGSettingState);
		base.ui.Icon.onClick.Add(ChangeShowState);
		AddEvent_BgSetting();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_GameOnline.onClick.Remove(RequestOpenRoomList);
		base.ui.btn_PlayerGuide.onClick.Remove(OpenPlayerGuide);
		base.ui.btn_Dictionary.onClick.Remove(OpenDictionary);
		base.ui.btn_Mall.onClick.Remove(OpenShoppingPanel);
		base.ui.btn_Gacha.onClick.Remove(OpenGachaPanel);
		base.ui.com_Banner.RemoveEvent();
		base.ui.com_BattlePass.loader_Fold.onClick.Remove(OpenBattlePassInfo);
		base.ui.com_BattlePass.btn_OpenBattlePass.onClick.Remove(OpenBattlePass);
		base.ui.com_ActivityBanner.RemoveEvent();
		base.ui.com_Activity.RemoveEvent();
		base.ui.com_expand.RemoveEvent();
		base.ui.Hide.onChanged.Remove(ChangeHideState);
		base.ui.BGset.onChanged.Remove(ChangeBGSettingState);
		base.ui.Icon.onClick.Remove(ChangeShowState);
		RemoveEvent_BgSetting();
	}

	protected override void AddListener()
	{
		base.AddListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.storeRedSignal.AddListener(RefreshStoreRed);
		SimpleSingletonProvider<GameLogicManager>.inst.battlePass.signal.updateBattlePass.AddListener(RefreshBattlePass);
		SimpleSingletonProvider<GameLogicManager>.inst.fashion.FashionRedSignal.AddListener(RefreshFashionRed);
		base.ui.com_expand.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		SimpleSingletonProvider<GameLogicManager>.inst.store.storeRedSignal.RemoveListener(RefreshStoreRed);
		SimpleSingletonProvider<GameLogicManager>.inst.battlePass.signal.updateBattlePass.RemoveListener(RefreshBattlePass);
		SimpleSingletonProvider<GameLogicManager>.inst.fashion.FashionRedSignal.RemoveListener(RefreshFashionRed);
		base.ui.com_expand.RemoveListener();
	}

	public override void Close()
	{
		if (base.ui != null)
		{
			base.ui.com_Banner.startScrollStatus = false;
			base.ui.com_ActivityBanner.startScrollStatus = false;
			base.ui.com_Activity.startScrollStatus = false;
			base.ui.com_expand.StopScroll();
			base.ui.Hide.selectedIndex = 0;
			base.ui.BGset.selectedIndex = 0;
		}
		systemStatusLock = false;
		base.Close();
	}

	public override void Dispose()
	{
		if (base.ui != null)
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(base.ui.Com_BGSet.loader_Video);
		}
		base.Dispose();
	}

	public override void AdultMode(bool inAdultMode)
	{
		ShowStartVideo();
		base.ui.btn_GameOnline.angelMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
		base.ui.btn_Mall.angelMode.selectedIndex = (GameSettings.angelMode ? 1 : 0);
	}

	private void ShowStartVideo()
	{
		string homePanelKV = SimpleSingletonProvider<GameLogicManager>.inst.fashion.GetHomePanelKV();
		SimpleSingletonProvider<CriMovieManager>.inst.Play(homePanelKV, base.ui.Com_BGSet.loader_Video).Forget();
	}

	public void RefreshSystemStatus()
	{
		if (!systemStatusLock)
		{
			systemStatusLock = true;
			if (SimpleSingletonProvider<GameLogicManager>.inst.connectRoomId > 0)
			{
				SetTouchable(touchable: false);
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(10000);
				SimpleSingletonProvider<GameLogicManager>.inst.room.RequestSyncRoomC2S(SimpleSingletonProvider<GameLogicManager>.inst.connectRoomId);
			}
			else
			{
				SimpleSingletonProvider<GameLogicManager>.inst.home.TriggerMessage();
				TriggerChatTip();
			}
		}
	}

	private async void RequestOpenRoomList()
	{
		if (SimpleSingletonProvider<UIManager>.inst.guide.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.guide.HideGuideMask();
		}
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.StartGameLicense())
		{
			base.ui.btn_GameOnline.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.MatchEntrance);
			base.ui.btn_GameOnline.onClick.Release();
		}
	}

	private async void OpenPlayerGuide()
	{
		base.ui.btn_PlayerGuide.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Fashion);
		base.ui.btn_PlayerGuide.onClick.Release();
	}

	private async void OpenDictionary()
	{
		base.ui.btn_Dictionary.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Hero);
		base.ui.btn_Dictionary.onClick.Release();
	}

	private async void OpenShoppingPanel()
	{
		base.ui.btn_Mall.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.ProductRecommendation);
		base.ui.btn_Mall.onClick.Release();
	}

	private void ChangeHideState()
	{
		base.ui.Btn_Hide.onClick.Retain();
		if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
		{
			bottomMenuPanel.ChangeShowStatus(base.ui.Hide.selectedIndex == 0);
		}
		base.ui.Btn_Hide.onClick.Release();
	}

	private void ChangeBGSettingState()
	{
		base.ui.Btn_BGSet.onClick.Retain();
		if (SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel is BottomMenuPanel bottomMenuPanel)
		{
			bottomMenuPanel.ChangeBGSetStatus(base.ui.BGset.selectedIndex == 0);
		}
		base.ui.Btn_BGSet.onClick.Release();
	}

	private void ChangeShowState()
	{
		base.ui.Hide.selectedIndex = 0;
	}

	private async void OpenGachaPanel(EventContext context)
	{
		base.ui.btn_Gacha.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Gacha);
		base.ui.btn_Gacha.onClick.Release();
	}

	private void RefreshStoreRed(bool status)
	{
		base.ui.btn_Mall.redPoint.selectedIndex = (status ? 1 : 0);
	}

	private void RefreshFashionRed(bool status)
	{
		base.ui.btn_PlayerGuide.redPoint.selectedIndex = (status ? 1 : 0);
	}

	private void TriggerChatTip()
	{
		SessionData sessionData = SimpleSingletonProvider<GameLogicManager>.inst.communicate.IsHaveUnReadMsg();
		if (sessionData != null)
		{
			SimpleSingletonProvider<UIManager>.inst.systemTips.ShowChatSignal(sessionData.targetPlayerId);
		}
	}

	private void RefreshBattlePass()
	{
		BattlePassData battlePassData = SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData;
		if (battlePassData == null || !TimeHelper.ValidityTime(battlePassData.BattlePassInfo.BeginTime, battlePassData.BattlePassInfo.EndTime))
		{
			base.ui.com_BattlePass.visible = false;
			return;
		}
		base.ui.com_BattlePass.visible = true;
		base.ui.com_BattlePass.loader_Fold.url = battlePassData.BattlePassInfo.EntranceFold;
		base.ui.com_BattlePass.loader_UnFold.url = battlePassData.BattlePassInfo.EntranceUnfold;
		if (battlePassData.GetPopupRewardData().VailReward || battlePassData.GetTaskSystemStatus())
		{
			base.ui.com_BattlePass.loader_Fold.visible = true;
			base.ui.com_BattlePass.loader_Fold.onClick.Release();
			base.ui.com_BattlePass.loader_Fold.onRollOver.Release();
			base.ui.com_BattlePass.onRollOut.Release();
			OpenBattlePassInfo();
		}
		else if (base.ui.com_BattlePass.status.selectedIndex == 1)
		{
			base.ui.com_BattlePass.loader_Fold.visible = false;
			base.ui.com_BattlePass.loader_Fold.onClick.Retain();
			base.ui.com_BattlePass.loader_Fold.onRollOver.Retain();
			base.ui.com_BattlePass.onRollOut.Retain();
			TryCloseBattlePassInfo();
		}
	}

	private void OpenBattlePassInfo()
	{
		base.ui.com_BattlePass.loader_Fold.onClick.Retain();
		base.ui.com_BattlePass.loader_Fold.onRollOver.Retain();
		base.ui.com_BattlePass.onRollOut.Retain();
		BattlePassData battlePassData = SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData;
		BattlePassReward popupRewardData = battlePassData.GetPopupRewardData();
		base.ui.com_BattlePass.redStatus.selectedIndex = ((popupRewardData.VailReward || battlePassData.GetTaskSystemStatus()) ? 1 : 0);
		base.ui.com_BattlePass.txt_nextLV.text = (battlePassData.LV + 1).ToString();
		base.ui.com_BattlePass.progress_Exp.max = battlePassData.BattlePassInfo.ExpPerLv;
		base.ui.com_BattlePass.progress_Exp.min = 0.0;
		base.ui.com_BattlePass.progress_Exp.value = battlePassData.Exp;
		base.ui.com_BattlePass.txt_Title.text = battlePassData.BattlePassInfo.Topic.GetLocal(UIStringType.BattlePass);
		base.ui.com_BattlePass.txt_Time.text = battlePassData.GetTimeText();
		if (popupRewardData.rewardType == BattlePassRewardType.COMMON)
		{
			RendererBattleReward(popupRewardData.FreeReward, base.ui.com_BattlePass.com_FreeReward, popupRewardData.IsFinishFreeReward(), SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetGearStatus(BattlePassGearType.FREE));
			RendererBattleReward(popupRewardData.NormalReward, base.ui.com_BattlePass.com_NormalReward, popupRewardData.IsFinishNormalReward(), SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetGearStatus(BattlePassGearType.NORMAL));
			RendererBattleReward(popupRewardData.PremiumReward, base.ui.com_BattlePass.com_PremiumReward, popupRewardData.IsFinishPremiumReward(), SimpleSingletonProvider<GameLogicManager>.inst.battlePass.GetGearStatus(BattlePassGearType.PREMIUM));
		}
		else if (popupRewardData.rewardType == BattlePassRewardType.SURPASS)
		{
			RendererBattleReward(popupRewardData.FreeReward, base.ui.com_BattlePass.com_FreeReward, popupRewardData.IsFinishSurpassReward(), unlock: true);
			base.ui.com_BattlePass.com_NormalReward.visible = false;
			base.ui.com_BattlePass.com_PremiumReward.visible = false;
		}
		base.ui.com_BattlePass.status.selectedIndex = 1;
		base.ui.com_BattlePass.showDetail.Play(delegate
		{
			base.ui.com_BattlePass.loader_Fold.visible = false;
			base.ui.com_BattlePass.onRollOut.Release();
		});
	}

	private void RendererBattleReward(BattlePassRewardData rewardData, UIHome_Com_BattlePassReward comFreeReward, bool finish, bool unlock)
	{
		if (rewardData == null)
		{
			comFreeReward.visible = false;
			return;
		}
		comFreeReward.visible = true;
		comFreeReward.status.selectedIndex = (unlock ? 1 : 0);
		comFreeReward.btn_Item.grayed = finish;
		((UICom_LitItem)comFreeReward.btn_Item).loader_Icon.url = rewardData.itemConfig.ShowIcon;
		((UICom_LitItem)comFreeReward.btn_Item).qualityType.selectedIndex = (int)rewardData.itemConfig.QualityType;
	}

	private void TryCloseBattlePassInfo()
	{
		BattlePassData battlePassData = SimpleSingletonProvider<GameLogicManager>.inst.battlePass.BattlePassData;
		if (!battlePassData.GetPopupRewardData().VailReward && !battlePassData.GetTaskSystemStatus())
		{
			base.ui.com_BattlePass.showDetail.Stop();
			base.ui.com_BattlePass.loader_Fold.visible = true;
			base.ui.com_BattlePass.status.selectedIndex = 0;
			base.ui.com_BattlePass.hideDetail.Play(delegate
			{
				base.ui.com_BattlePass.loader_Fold.onClick.Release();
				base.ui.com_BattlePass.loader_Fold.onRollOver.Release();
			});
		}
	}

	private async void OpenBattlePass()
	{
		base.ui.com_BattlePass.btn_OpenBattlePass.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.BattlePass);
		base.ui.com_BattlePass.btn_OpenBattlePass.onClick.Release();
	}

	private async UniTask OpenActivityType(int EntranceType)
	{
		ActivityActivityEntrance2Configure activityInfo = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetActivityInfo(EntranceType);
		if (activityInfo != null)
		{
			if (activityInfo.InfoConfig.UiType == UIType.Panel)
			{
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(activityInfo.InfoConfig.PanelType, new RepeatedField<int> { activityInfo.InfoConfig.Id });
			}
			else if (activityInfo.InfoConfig.UiType == UIType.Window)
			{
				await SimpleSingletonProvider<UIManager>.inst.activityPopup.ShowActivity(activityInfo.InfoConfig.Id);
			}
		}
	}

	private void RefreshActivity(GButton btn_Activity, int EntranceType)
	{
		ActivityActivityEntrance2Configure activityInfo = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetActivityInfo(EntranceType);
		if (activityInfo == null)
		{
			btn_Activity.visible = false;
			return;
		}
		if (btn_Activity is UIHome_Button_Activity uIHome_Button_Activity && activityInfo.InfoConfig.UiTab == 1)
		{
			List<ActivityActivityEntrance2Configure> activityInfos = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetActivityInfos(EntranceType);
			bool flag = false;
			foreach (ActivityActivityEntrance2Configure item in activityInfos)
			{
				if (SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(item.InfoConfig.Id).GetActivityStatus())
				{
					flag = true;
					break;
				}
			}
			uIHome_Button_Activity.redPoint.selectedIndex = (flag ? 1 : 0);
		}
		else if (btn_Activity is UIHome_Button_SpecialActivity uIHome_Button_SpecialActivity)
		{
			UIType uiType = activityInfo.InfoConfig.UiType;
			int uiTab = activityInfo.InfoConfig.UiTab;
			switch (uiType)
			{
			case UIType.Window:
			{
				TaskActivityData taskActivityData2 = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityInfo.InfoConfig.Id);
				uIHome_Button_SpecialActivity.redPoint.selectedIndex = (taskActivityData2.GetActivityStatus() ? 1 : 0);
				uIHome_Button_SpecialActivity.txt_Time.text = taskActivityData2.GetDurationText();
				uIHome_Button_SpecialActivity.title = activityInfo.Title.GetLocal(UIStringType.Activity);
				uIHome_Button_SpecialActivity.loader_Icon.url = activityInfo.Icon;
				break;
			}
			case UIType.Panel:
				if (uiTab == 5)
				{
					LightActivityData lightGiftActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetLightGiftActivityData(activityInfo.InfoConfig.Id);
					uIHome_Button_SpecialActivity.redPoint.selectedIndex = (lightGiftActivityData.GetActivityStatus() ? 1 : 0);
					uIHome_Button_SpecialActivity.txt_Time.text = lightGiftActivityData.GetDurationText();
					uIHome_Button_SpecialActivity.title = activityInfo.Title.GetLocal(UIStringType.Activity);
					uIHome_Button_SpecialActivity.loader_Icon.url = activityInfo.Icon;
				}
				else
				{
					TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(activityInfo.InfoConfig.Id);
					uIHome_Button_SpecialActivity.redPoint.selectedIndex = (taskActivityData.GetActivityStatus() ? 1 : 0);
					uIHome_Button_SpecialActivity.txt_Time.text = taskActivityData.GetDurationText();
					uIHome_Button_SpecialActivity.title = activityInfo.Title.GetLocal(UIStringType.Activity);
					uIHome_Button_SpecialActivity.loader_Icon.url = activityInfo.Icon;
				}
				break;
			}
		}
		btn_Activity.visible = true;
	}

	private void RefreshGacha()
	{
		base.ui.btn_Gacha.tips.selectedIndex = SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetGachaStatus();
		base.ui.btn_Gacha.redPoint.selectedIndex = (SimpleSingletonProvider<GameLogicManager>.inst.gacha.GetRedPointStatus() ? 1 : 0);
	}

	private void RefreshActivityHubData()
	{
		List<ActivityActivityEntrance2Configure> entranceInfos = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetActivityHubData();
		if (entranceInfos.Count == 0)
		{
			base.ui.btn_Activity.visible = false;
			return;
		}
		base.ui.btn_Activity.visible = true;
		base.ui.btn_Activity.redPoint.selectedIndex = 0;
		for (int i = 0; i < entranceInfos.Count; i++)
		{
			int id = entranceInfos[i].InfoConfig.Id;
			if (StaticConfigure.Activity.InfoDict.TryGetValue(id, out var _))
			{
				TaskActivityData taskActivityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(id);
				if (taskActivityData != null && taskActivityData.GetActivityStatus())
				{
					base.ui.btn_Activity.redPoint.selectedIndex = 1;
					break;
				}
			}
		}
		base.ui.btn_Activity.onClick.Set((EventCallback0)delegate
		{
			if (entranceInfos.Count != 0)
			{
				OpenActivityHub(entranceInfos[0]).Forget();
			}
		});
	}

	private async UniTask OpenActivityHub(ActivityActivityEntrance2Configure entranceInfo)
	{
		base.ui.btn_Activity.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.OpenActivityHub(entranceInfo.InfoConfig.Id);
		base.ui.btn_Activity.onClick.Release();
	}

	public void GuideStartGame()
	{
	}

	public async UniTask<(Vector2, float, float)> ShowStartGameMask()
	{
		_FocusStatus = false;
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitWhile(() => base.ui.Cut_in.playing, SimpleSingletonProvider<UIManager>.inst.tutorial.Hide);
		Vector2 pt = base.ui.btn_GameOnline.LocalToGlobal(Vector2.zero);
		Vector2 vector = GRoot.inst.GlobalToLocal(pt);
		float width = base.ui.btn_GameOnline.width;
		float height = base.ui.btn_GameOnline.height;
		SimpleSingletonProvider<UIManager>.inst.tutorial.ShowGuideMask(vector, width, height, isRect: true);
		return (vector, width, height);
	}

	public async UniTask FireClickStartGame()
	{
		base.ui.btn_GameOnline.FireClick(downEffect: true);
		await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.MatchEntrance);
		await SimpleSingletonProvider<UIManager>.inst.tutorial.ShowMatchEntranceMask();
	}

	private void Show_BgSetting()
	{
		SetBGFullScreen();
	}

	private void AddEvent_BgSetting()
	{
		base.ui.Com_BGSet.loader_Video.onTouchBegin.Add(BeginDrag);
		base.ui.Com_BGSet.loader_Video.onTouchMove.Add(MoveDrag);
		base.ui.Com_BGSet.loader_Video.onTouchEnd.Add(EndDrag);
		Stage.inst.onTouchBegin.Add(OnStageTouchBegin);
		Stage.inst.onTouchMove.Add(OnStageTouchMove);
		Stage.inst.onTouchEnd.Add(OnStageTouchEnd);
		Stage.inst.onMouseWheel.Add(OnMouseWheelScale);
		base.ui.Com_Tip.Btn_Reset.onClick.Add(ResetBgDragPosition);
		base.ui.Com_Tip.Btn_Confirm.onClick.Add(ConfirmKVPositionAndScale);
		base.ui.Com_Tip.Btn_Cancel.onClick.Add(RevertToLastSavedParameters);
	}

	private void RemoveEvent_BgSetting()
	{
		base.ui.Com_BGSet.loader_Video.onTouchBegin.Remove(BeginDrag);
		base.ui.Com_BGSet.loader_Video.onTouchMove.Remove(MoveDrag);
		base.ui.Com_BGSet.loader_Video.onTouchEnd.Remove(EndDrag);
		Stage.inst.onTouchBegin.Remove(OnStageTouchBegin);
		Stage.inst.onTouchMove.Remove(OnStageTouchMove);
		Stage.inst.onTouchEnd.Remove(OnStageTouchEnd);
		Stage.inst.onMouseWheel.Remove(OnMouseWheelScale);
		base.ui.Com_Tip.Btn_Reset.onClick.Remove(ResetBgDragPosition);
		base.ui.Com_Tip.Btn_Confirm.onClick.Remove(ConfirmKVPositionAndScale);
		base.ui.Com_Tip.Btn_Cancel.onClick.Remove(RevertToLastSavedParameters);
	}

	private void BeginDrag(EventContext context)
	{
		if (base.ui.BGset.selectedIndex != 0 && !isPinching && Input.touchCount <= 1)
		{
			context.CaptureTouch();
			isDragReady = true;
			isDragging = false;
			touchStartGlobalPos = context.inputEvent.position;
			loaderStartPos = new Vector2(base.ui.Com_BGSet.loader_Video.x, base.ui.Com_BGSet.loader_Video.y);
		}
	}

	private void MoveDrag(EventContext context)
	{
		if (base.ui.BGset.selectedIndex != 0 && !isPinching && Input.touchCount <= 1 && isDragReady)
		{
			if (!isDragging)
			{
				isDragging = true;
				touchStartGlobalPos = context.inputEvent.position;
				loaderStartPos = new Vector2(base.ui.Com_BGSet.loader_Video.x, base.ui.Com_BGSet.loader_Video.y);
			}
			Vector2 vector = context.inputEvent.position - touchStartGlobalPos;
			Vector2 position = ClampLoaderPosition(loaderStartPos + vector, base.ui.Com_BGSet.loader_Video.scaleX);
			ApplyLoaderTransform(base.ui.Com_BGSet.loader_Video.scaleX, position);
		}
	}

	private void EndDrag(EventContext context)
	{
		if (base.ui.BGset.selectedIndex != 0)
		{
			isDragReady = false;
			isDragging = false;
		}
	}

	private void OnStageTouchBegin(EventContext context)
	{
		if (base.ui.BGset.selectedIndex != 0)
		{
			TryBeginPinch();
		}
	}

	private void OnStageTouchMove(EventContext context)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (base.ui.BGset.selectedIndex == 0)
		{
			return;
		}
		if (Input.touchCount != 2)
		{
			isPinching = false;
			pinchLastDistance = 0f;
			return;
		}
		Touch touch = Input.GetTouch(0);
		Touch touch2 = Input.GetTouch(1);
		Vector2 touchPosition = Stage.inst.GetTouchPosition(((Touch)(ref touch)).fingerId);
		Vector2 touchPosition2 = Stage.inst.GetTouchPosition(((Touch)(ref touch2)).fingerId);
		float num = Vector2.Distance(touchPosition, touchPosition2);
		if (!isPinching)
		{
			TryBeginPinch();
		}
		else if (!Mathf.Approximately(pinchLastDistance, 0f))
		{
			float scaleX = base.ui.Com_BGSet.loader_Video.scaleX;
			Vector2 vector = (touchPosition + touchPosition2) * 0.5f;
			pinchContentAnchor = GetContentAnchor(base.ui.Com_BGSet.GlobalToLocal(vector), scaleX);
			float num2 = num / pinchLastDistance;
			float clampedScale = GetClampedScale(scaleX * num2);
			pinchLastDistance = num;
			ApplyScaleWithCenter(scaleX, clampedScale, vector);
		}
	}

	private void OnStageTouchEnd(EventContext context)
	{
		if (Input.touchCount < 2)
		{
			isPinching = false;
			pinchLastDistance = 0f;
		}
	}

	private void TryBeginPinch()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (Input.touchCount == 2)
		{
			Touch touch = Input.GetTouch(0);
			Touch touch2 = Input.GetTouch(1);
			Vector2 touchPosition = Stage.inst.GetTouchPosition(((Touch)(ref touch)).fingerId);
			Vector2 touchPosition2 = Stage.inst.GetTouchPosition(((Touch)(ref touch2)).fingerId);
			pinchLastDistance = Vector2.Distance(touchPosition, touchPosition2);
			isPinching = true;
			isDragReady = false;
			isDragging = false;
		}
	}

	private void OnMouseWheelScale(EventContext context)
	{
		if (base.ui.BGset.selectedIndex != 0 && !Application.isMobilePlatform)
		{
			float mouseWheelDelta = context.inputEvent.mouseWheelDelta;
			if (!Mathf.Approximately(mouseWheelDelta, 0f))
			{
				float scale = base.ui.Com_BGSet.loader_Video.scaleX - mouseWheelDelta * 0.02f;
				ApplyLoaderScale(scale);
			}
		}
	}

	private void ResetBgDragPosition()
	{
		base.ui.Com_Tip.Btn_Reset.onClick.Retain();
		float x = base.ui.Com_BGSet.width * 0.5f - base.ui.Com_BGSet.loader_Video.width * 0.5f;
		float y = base.ui.Com_BGSet.height * 0.5f - base.ui.Com_BGSet.loader_Video.height * 0.5f;
		ApplyLoaderTransform(1f, ClampLoaderPosition(new Vector2(x, y), 1f));
		base.ui.Com_Tip.Btn_Reset.onClick.Release();
	}

	private async void ConfirmKVPositionAndScale()
	{
		base.ui.Com_Tip.Btn_Confirm.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1126.GetLocal(UIStringType.Message), delegate
		{
			GameConfig.SaveHomeKvPosition(GetNormalizedLoaderOffset(new Vector2(base.ui.Com_BGSet.loader_Video.x, base.ui.Com_BGSet.loader_Video.y), base.ui.Com_BGSet.loader_Video.scaleX));
			GameConfig.SaveHomeKvScale(base.ui.Com_BGSet.loader_Video.scaleX);
			base.ui.BGset.selectedIndex = 0;
		});
		base.ui.Com_Tip.Btn_Confirm.onClick.Release();
	}

	private void RevertToLastSavedParameters()
	{
		base.ui.Com_Tip.Btn_Cancel.onClick.Retain();
		float homeKvScale = GameConfig.HomeKvScale;
		float clampedScale = GetClampedScale(homeKvScale);
		Vector2 homeKvPosition = GameConfig.HomeKvPosition;
		Vector2 positionFromNormalizedOffset = GetPositionFromNormalizedOffset(homeKvPosition, clampedScale);
		ApplyLoaderTransform(clampedScale, ClampLoaderPosition(positionFromNormalizedOffset, clampedScale));
		base.ui.BGset.selectedIndex = 0;
		base.ui.Com_Tip.Btn_Cancel.onClick.Release();
	}

	private void ApplyLoaderScale(float scale)
	{
		float clampedScale = GetClampedScale(scale);
		Vector2 position = ClampLoaderPosition(new Vector2(base.ui.Com_BGSet.loader_Video.x, base.ui.Com_BGSet.loader_Video.y), clampedScale);
		ApplyLoaderTransform(clampedScale, position);
	}

	private Vector2 GetNormalizedLoaderOffset(Vector2 position, float scale)
	{
		GetClampMetrics(scale, out var centerX, out var centerY, out var extraHalfWidth, out var extraHalfHeight);
		float value = (Mathf.Approximately(extraHalfWidth, 0f) ? 0f : ((position.x - centerX) / extraHalfWidth));
		return new Vector2(y: Mathf.Clamp(Mathf.Approximately(extraHalfHeight, 0f) ? 0f : ((position.y - centerY) / extraHalfHeight), -1f, 1f), x: Mathf.Clamp(value, -1f, 1f));
	}

	private Vector2 GetPositionFromNormalizedOffset(Vector2 normalizedOffset, float scale)
	{
		GetClampMetrics(scale, out var centerX, out var centerY, out var extraHalfWidth, out var extraHalfHeight);
		return new Vector2(centerX + Mathf.Clamp(normalizedOffset.x, -1f, 1f) * extraHalfWidth, centerY + Mathf.Clamp(normalizedOffset.y, -1f, 1f) * extraHalfHeight);
	}

	private void GetClampMetrics(float scale, out float centerX, out float centerY, out float extraHalfWidth, out float extraHalfHeight)
	{
		extraHalfWidth = Mathf.Max(0f, base.ui.Com_BGSet.loader_Video.width * scale - base.ui.Com_BGSet.width) * 0.5f;
		extraHalfHeight = Mathf.Max(0f, base.ui.Com_BGSet.loader_Video.height * scale - base.ui.Com_BGSet.height) * 0.5f;
		centerX = base.ui.Com_BGSet.width * 0.5f - base.ui.Com_BGSet.loader_Video.width * 0.5f;
		centerY = base.ui.Com_BGSet.height * 0.5f - base.ui.Com_BGSet.loader_Video.height * 0.5f;
	}

	private void ApplyScaleWithCenter(float oldScale, float newScale, Vector2 screenPoint)
	{
		if (!Mathf.Approximately(oldScale, 0f) && !Mathf.Approximately(oldScale, newScale))
		{
			Vector2 localPoint = base.ui.Com_BGSet.GlobalToLocal(screenPoint);
			Vector2 positionFromContentAnchor = GetPositionFromContentAnchor(localPoint, pinchContentAnchor, newScale);
			ApplyLoaderTransform(newScale, ClampLoaderPosition(positionFromContentAnchor, newScale));
		}
	}

	private void ApplyLoaderTransform(float scale, Vector2 position)
	{
		base.ui.Com_BGSet.loader_Video.SetScale(scale, scale);
		base.ui.Com_BGSet.loader_Video.SetPosition(position.x, position.y, 0f);
	}

	private Vector2 ClampLoaderPosition(Vector2 position, float scale)
	{
		float num = Mathf.Max(0f, base.ui.Com_BGSet.loader_Video.width * scale - base.ui.Com_BGSet.width) * 0.5f;
		float num2 = Mathf.Max(0f, base.ui.Com_BGSet.loader_Video.height * scale - base.ui.Com_BGSet.height) * 0.5f;
		float num3 = base.ui.Com_BGSet.width * 0.5f - base.ui.Com_BGSet.loader_Video.width * 0.5f;
		float num4 = base.ui.Com_BGSet.height * 0.5f - base.ui.Com_BGSet.loader_Video.height * 0.5f;
		return new Vector2(Mathf.Clamp(position.x, num3 - num, num3 + num), Mathf.Clamp(position.y, num4 - num2, num4 + num2));
	}

	private float GetClampedScale(float scale)
	{
		float minLoaderScale = GetMinLoaderScale();
		float maxLoaderScale = GetMaxLoaderScale(minLoaderScale);
		return Mathf.Clamp(scale, minLoaderScale, maxLoaderScale);
	}

	private float GetMinLoaderScale()
	{
		float a = GRoot.inst.width / base.ui.Com_BGSet.loader_Video.width;
		float b = GRoot.inst.height / base.ui.Com_BGSet.loader_Video.height;
		return Mathf.Max(a, b);
	}

	private float GetMaxLoaderScale(float minScale)
	{
		return Mathf.Max(minScale, 2f);
	}

	private Vector2 GetContentAnchor(Vector2 localPoint, float scale)
	{
		float num = (Mathf.Approximately(scale, 0f) ? 1f : scale);
		float num2 = base.ui.Com_BGSet.loader_Video.width * base.ui.Com_BGSet.loader_Video.pivotX;
		float num3 = base.ui.Com_BGSet.loader_Video.height * base.ui.Com_BGSet.loader_Video.pivotY;
		if (base.ui.Com_BGSet.loader_Video.pivotAsAnchor)
		{
			return new Vector2((localPoint.x - base.ui.Com_BGSet.loader_Video.x) / num + num2, (localPoint.y - base.ui.Com_BGSet.loader_Video.y) / num + num3);
		}
		return new Vector2((localPoint.x - base.ui.Com_BGSet.loader_Video.x - num2 * (1f - num)) / num, (localPoint.y - base.ui.Com_BGSet.loader_Video.y - num3 * (1f - num)) / num);
	}

	private Vector2 GetPositionFromContentAnchor(Vector2 localPoint, Vector2 contentAnchor, float scale)
	{
		float num = base.ui.Com_BGSet.loader_Video.width * base.ui.Com_BGSet.loader_Video.pivotX;
		float num2 = base.ui.Com_BGSet.loader_Video.height * base.ui.Com_BGSet.loader_Video.pivotY;
		if (base.ui.Com_BGSet.loader_Video.pivotAsAnchor)
		{
			return new Vector2(localPoint.x - (contentAnchor.x - num) * scale, localPoint.y - (contentAnchor.y - num2) * scale);
		}
		return new Vector2(localPoint.x - contentAnchor.x * scale - num * (1f - scale), localPoint.y - contentAnchor.y * scale - num2 * (1f - scale));
	}

	private void SetBGFullScreen()
	{
		base.ui.Com_BGSet.SetSize(GRoot.inst.width, GRoot.inst.height);
		(float, float) tuple = UIHelper.ExpandToAspectRatio(base.ui.Com_BGSet.width, base.ui.Com_BGSet.height);
		float num = Mathf.Max(tuple.Item1 / base.ui.Com_BGSet.width, tuple.Item2 / base.ui.Com_BGSet.height);
		base.ui.Com_BGSet.loader_Video.SetSize(2304f * num, 1200f * num);
		float homeKvScale = GameConfig.HomeKvScale;
		ApplyLoaderScale(homeKvScale);
		Vector2 homeKvPosition = GameConfig.HomeKvPosition;
		Vector2 positionFromNormalizedOffset = GetPositionFromNormalizedOffset(homeKvPosition, base.ui.Com_BGSet.loader_Video.scaleX);
		Vector2 vector = ClampLoaderPosition(positionFromNormalizedOffset, base.ui.Com_BGSet.loader_Video.scaleX);
		base.ui.Com_BGSet.loader_Video.SetPosition(vector.x, vector.y, 0f);
	}
}
