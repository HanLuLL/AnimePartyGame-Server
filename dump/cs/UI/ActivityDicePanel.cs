using System;
using System.Collections.Generic;
using System.Linq;
using Core.Net;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameLogic;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using party.model;
using party.protocol;

namespace UI;

public class ActivityDicePanel : BasePanel<UIActivityDicePanel>
{
	private DiceActivityData activityData;

	private int overrideDicePoint = -1;

	private int overrideProgressExp = -1;

	private List<Model_UIActivityDice_Grid> gridModels = new List<Model_UIActivityDice_Grid>();

	private List<Model_UIActivityDice_DiceMission> diceMissionModels = new List<Model_UIActivityDice_DiceMission>();

	private List<Model_UIActivityDice_Progress> rewardModels = new List<Model_UIActivityDice_Progress>();

	private Model_ActivityDice_Receive receiveBtnModel;

	private bool DiceEffectFinish = true;

	private int gridPerCircle = 24;

	private int currentTotalDicePoint;

	private int targetTotalDicePoint;

	private List<KeyValuePair<int, int>> gridRewardItems = new List<KeyValuePair<int, int>>();

	private int[] dicePoints = new int[0];

	private Sequence playerMoveSeq;

	private Tween playerAniTween;

	private bool isRemoveMissionDataListenerOnPlayerMove;

	private UISingleModelController diceModelCtrl;

	private int diceItemId => activityData.DiceActivityInfoConfig.DiceCost;

	private int progressExpId => activityData.DiceActivityInfoConfig.EXP;

	public ActivityDicePanel(UIPanelConfigure config)
		: base(config)
	{
	}

	private void OnItemCountChanged(int id, int count)
	{
		if (id == diceItemId)
		{
			RefreshDiceCount(count);
		}
		else if (id == progressExpId)
		{
			RefreshProgress(count);
		}
	}

	protected override void Create()
	{
		base.ui = UIActivityDicePanel.CreateInstance();
		base.Create();
		UIActivityDice_Grid[] array = new UIActivityDice_Grid[24]
		{
			base.ui.Grid_1,
			base.ui.Grid_2,
			base.ui.Grid_3,
			base.ui.Grid_4,
			base.ui.Grid_5,
			base.ui.Grid_6,
			base.ui.Grid_7,
			base.ui.Grid_8,
			base.ui.Grid_9,
			base.ui.Grid_10,
			base.ui.Grid_11,
			base.ui.Grid_12,
			base.ui.Grid_13,
			base.ui.Grid_14,
			base.ui.Grid_15,
			base.ui.Grid_16,
			base.ui.Grid_17,
			base.ui.Grid_18,
			base.ui.Grid_19,
			base.ui.Grid_20,
			base.ui.Grid_21,
			base.ui.Grid_22,
			base.ui.Grid_23,
			base.ui.Grid_24
		};
		for (int i = 0; i < array.Length; i++)
		{
			Model_UIActivityDice_Grid item = new Model_UIActivityDice_Grid(array[i]);
			gridModels.Add(item);
		}
		int numItems = base.ui.LIst_Reward.numItems;
		for (int j = 0; j < numItems; j++)
		{
			UIActivityDice_Button_Progress com = base.ui.LIst_Reward.GetChildAt(j) as UIActivityDice_Button_Progress;
			rewardModels.Add(new Model_UIActivityDice_Progress(com));
		}
		base.ui.Txt_Title.text = 6050100.GetLocal(UIStringType.DiceActivity);
		receiveBtnModel = new Model_ActivityDice_Receive(base.ui.Btn_Receive);
		base.ui.Bg.FullScreen();
	}

	protected override void InitData(params object[] objs)
	{
		activityData = SimpleSingletonProvider<GameLogicManager>.inst.activity.GetTaskActivityData(605010) as DiceActivityData;
		for (int i = 0; i < gridModels.Count; i++)
		{
			Model_UIActivityDice_Grid model_UIActivityDice_Grid = gridModels[i];
			model_UIActivityDice_Grid.ItemData = activityData.DiceGrids[i];
			model_UIActivityDice_Grid.TitleId = i + 1;
		}
		List<MissionData> sortedDiceMissionList = activityData.GetSortedDiceMissionList();
		if (diceMissionModels.Count == 0)
		{
			base.ui.List_Mission.numItems = sortedDiceMissionList.Count;
			for (int j = 0; j < sortedDiceMissionList.Count; j++)
			{
				UIActivityDice_Com_DiceMission com = base.ui.List_Mission.GetChildAt(j) as UIActivityDice_Com_DiceMission;
				diceMissionModels.Add(new Model_UIActivityDice_DiceMission(com));
			}
		}
		for (int k = 0; k < diceMissionModels.Count; k++)
		{
			diceMissionModels[k].ChangeMissionData(sortedDiceMissionList[k]);
		}
		List<MissionData> sortedRewardMissionList = activityData.GetSortedRewardMissionList();
		for (int l = 0; l < rewardModels.Count; l++)
		{
			rewardModels[l].ChangeMissionData(sortedRewardMissionList[l]);
		}
		receiveBtnModel.ChangeMissionDatas(sortedRewardMissionList);
		SimpleSingletonProvider<GameLogicManager>.inst.task.LaborActDiceC2S(isUseDice: false);
		DiceEffectFinish = true;
		DateTime dateTime = activityData.activityConfig.BeginTime.ToDateTime();
		DateTime dateTime2 = activityData.activityConfig.EndTime.ToDateTime();
		base.ui.Txt_Data.text = dateTime.ToString("MM.dd") + "-" + dateTime2.ToString("MM.dd");
	}

	private void OnDiceButtonDown()
	{
		bool flag = SimpleSingletonProvider<UIManager>.inst.reward.HasNeedShowReward || SimpleSingletonProvider<UIManager>.inst.reward.isShowing;
		if (!(!DiceEffectFinish || flag) && SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(diceItemId) >= 1)
		{
			overrideProgressExp = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(progressExpId);
			RemoveMissionDataListenerOnMove();
			SimpleSingletonProvider<GameLogicManager>.inst.task.LaborActDiceC2S(isUseDice: true);
			DiceEffectFinish = false;
		}
	}

	public void ResortMissionList()
	{
		List<MissionData> sortedDiceMissionList = activityData.GetSortedDiceMissionList();
		for (int i = 0; i < diceMissionModels.Count; i++)
		{
			Model_UIActivityDice_DiceMission model_UIActivityDice_DiceMission = diceMissionModels[i];
			model_UIActivityDice_DiceMission.ChangeMissionData(sortedDiceMissionList[i]);
			model_UIActivityDice_DiceMission.Refresh();
		}
	}

	private async UniTask OnLaborActDiceS2C(LaborActDiceS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			return;
		}
		dicePoints = model.DiceInfo.CurrDice?.ToArray() ?? new int[0];
		gridRewardItems.Clear();
		if (model.DiceInfo.Item != null)
		{
			foreach (ItemEtc item in model.DiceInfo.Item)
			{
				gridRewardItems.Add(new KeyValuePair<int, int>(item.ItemId, item.Count));
			}
		}
		bool num = dicePoints.Length != 0;
		int totalDicePoint = model.DiceInfo.TotalDicePoint;
		if (num)
		{
			targetTotalDicePoint = totalDicePoint;
			StartPlayerMove();
		}
		else
		{
			targetTotalDicePoint = totalDicePoint;
			currentTotalDicePoint = totalDicePoint;
			RefreshDiceInfoOnEnter();
		}
		await UniTask.CompletedTask;
	}

	private void RefreshDiceInfoOnEnter()
	{
		int index = currentTotalDicePoint % gridPerCircle;
		RefreshPlayerPosition(gridModels[index].Com.position);
		RefreshCircleNum(currentTotalDicePoint);
		UIActivityDice_Grid midGrid = gridModels[index].Com;
		playerAniTween = DOTween.To(() => 0, delegate(int x)
		{
			for (int i = 0; i < gridModels.Count; i++)
			{
				UIActivityDice_Grid com = gridModels[i].Com;
				if (!com.visible && Vector3.Distance(com.position, midGrid.position) < (float)x)
				{
					com.visible = true;
					com.Cut_in.Play();
				}
			}
		}, 2000, 3f).SetEase(Ease.Linear);
	}

	private async UniTask ShowDiceAnimation(int point)
	{
		if (diceModelCtrl == null)
		{
			diceModelCtrl = (await Addressables.InstantiateAsync("DiceActivity_Dice", Vector3.one * 1000f, Quaternion.identity)).GetComponent<UISingleModelController>();
		}
		base.ui.loader_dice.visible = true;
		base.ui.loader_dice.texture = diceModelCtrl.NTexture;
		diceModelCtrl.PlayDefalutAnimation(point);
		base.ui.DiceFade.Play();
	}

	private void StartPlayerMove()
	{
		playerMoveSeq = DOTween.Sequence();
		int num = currentTotalDicePoint;
		int showId = 0;
		for (int i = 0; i < dicePoints.Length; i++)
		{
			int point = dicePoints[i];
			playerMoveSeq.AppendCallback(delegate
			{
				ShowDiceAnimation(point).Forget();
			});
			playerMoveSeq.Append(DOTween.To(() => 0, delegate
			{
			}, 0, 2.85f));
			playerMoveSeq.AppendCallback(delegate
			{
				base.ui.PointChange.Play();
			});
			for (int num2 = 0; num2 < point; num2++)
			{
				Vector3 startPos = gridModels[num % gridPerCircle].Com.position;
				Vector3 position = gridModels[(num + 1) % gridPerCircle].Com.position;
				num++;
				playerMoveSeq.Append(DOTween.To(() => startPos, delegate(Vector3 pos)
				{
					RefreshPlayerPosition(pos);
				}, position, 0.2f));
				if (num % gridPerCircle != 0)
				{
					continue;
				}
				int cacheTotalPointCount = num;
				playerMoveSeq.AppendCallback(delegate
				{
					playerMoveSeq.Pause();
					RefreshCircleNum(cacheTotalPointCount);
					string loadedKey = StaticConfigure.Video.GlobalDict[405].LoadedKey;
					base.ui.loader_video.alpha = 0f;
					SimpleSingletonProvider<CriMovieManager>.inst.Stop(base.ui.loader_video);
					SimpleSingletonProvider<CriMovieManager>.inst.Play(loadedKey, base.ui.loader_video, delegate
					{
						base.ui.loader_video.alpha = 1f;
					}, delegate(Player player, int status)
					{
						player.Stop();
						base.ui.loader_video.alpha = 0f;
						playerMoveSeq?.Play();
					}).Forget();
				});
				playerMoveSeq.AppendInterval(0.2f);
				playerMoveSeq.AppendCallback(delegate
				{
					if (showId < gridRewardItems.Count)
					{
						KeyValuePair<int, int> keyValuePair = gridRewardItems[showId];
						showId++;
						SimpleSingletonProvider<UIManager>.inst.reward.ShowSingleReward(keyValuePair.Key, keyValuePair.Value);
						if (keyValuePair.Key == progressExpId)
						{
							overrideProgressExp += keyValuePair.Value;
							RefreshProgress(overrideProgressExp);
						}
					}
				});
			}
		}
		playerMoveSeq.AppendCallback(delegate
		{
			DiceEffectFinish = true;
			if (showId < gridRewardItems.Count)
			{
				KeyValuePair<int, int> keyValuePair = gridRewardItems[showId];
				showId++;
				SimpleSingletonProvider<UIManager>.inst.reward.ShowSingleReward(keyValuePair.Key, keyValuePair.Value);
			}
			overrideProgressExp = -1;
			SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(progressExpId);
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(progressExpId);
			RefreshProgress(itemCount);
			AddMissionDataListenerOnMove();
		});
		playerMoveSeq.OnUpdate(delegate
		{
			if (SimpleSingletonProvider<UIManager>.inst.reward.HasNeedShowReward)
			{
				playerMoveSeq.Pause();
			}
		});
		currentTotalDicePoint = targetTotalDicePoint;
	}

	private void RefreshPlayerPosition(Vector3 pos)
	{
		pos.y += (base.ui.Player.pivotY - 0.5f) * base.ui.Player.size.y;
		base.ui.Player.position = pos;
	}

	private void RemoveMissionDataListenerOnMove()
	{
		if (isRemoveMissionDataListenerOnPlayerMove)
		{
			return;
		}
		isRemoveMissionDataListenerOnPlayerMove = true;
		foreach (Model_UIActivityDice_DiceMission diceMissionModel in diceMissionModels)
		{
			diceMissionModel.RemoveMissionDataListener();
		}
		foreach (Model_UIActivityDice_Progress rewardModel in rewardModels)
		{
			rewardModel.RemoveMissionDataListener();
		}
		receiveBtnModel.RemoveMissionDataListener();
	}

	private void AddMissionDataListenerOnMove()
	{
		if (!isRemoveMissionDataListenerOnPlayerMove)
		{
			return;
		}
		isRemoveMissionDataListenerOnPlayerMove = false;
		foreach (Model_UIActivityDice_DiceMission diceMissionModel in diceMissionModels)
		{
			diceMissionModel.AddMissionDataListener();
			diceMissionModel.Refresh();
		}
		foreach (Model_UIActivityDice_Progress rewardModel in rewardModels)
		{
			rewardModel.AddMissionDataListener();
			rewardModel.Refresh();
		}
		receiveBtnModel.AddMissionDataListener();
		receiveBtnModel.Refresh();
	}

	private void OnRewardWindowClose()
	{
		if (playerMoveSeq != null && !playerMoveSeq.IsPlaying())
		{
			playerMoveSeq.Play();
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		playerMoveSeq?.Kill();
		playerAniTween?.Kill();
		if (overrideProgressExp >= 0)
		{
			overrideProgressExp = -1;
			int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(progressExpId);
			RefreshProgress(itemCount);
		}
		overrideDicePoint = -1;
		for (int i = 0; i < gridModels.Count; i++)
		{
			gridModels[i].Com.visible = false;
		}
		base.ui.loader_video.visible = false;
		if ((bool)diceModelCtrl)
		{
			Addressables.Release(diceModelCtrl.gameObject);
			diceModelCtrl = null;
		}
		if (isRemoveMissionDataListenerOnPlayerMove)
		{
			AddMissionDataListenerOnMove();
		}
		Debug.Log("骰骰乐界面：打开界面");
	}

	protected override void InitComponents()
	{
		base.InitComponents();
	}

	public override void InitTouchable()
	{
		base.InitTouchable();
		base.ui.loader_dice.touchable = false;
		base.ui.loader_video.touchable = false;
	}

	private void RefreshDiceCount(int count)
	{
		count = ((overrideDicePoint >= 0) ? overrideDicePoint : count);
		base.ui.Button_Throw.Txt_Dice_1.text = 6050102.GetLocal(UIStringType.DiceActivity);
		base.ui.Button_Throw.Txt_Dice_2.text = $"<img src=ui://lypih982rae11u width='45' height='45'/> {count}";
		base.ui.Button_Throw.grayed = count <= 0;
	}

	private void RefreshProgress(int count)
	{
		count = ((overrideProgressExp >= 0) ? overrideProgressExp : count);
		base.ui.Txt_Progress.text = string.Format(6050104.GetLocal(UIStringType.DiceActivity), count);
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < rewardModels.Count; i++)
		{
			int target = rewardModels[i].Target;
			if (count > target)
			{
				num += 100;
				num2 = target;
				continue;
			}
			num += (count - num2) * 100 / (target - num2);
			break;
		}
		base.ui.Bar_Progress.value = num;
	}

	private void RefreshCircleNum(int totalGridCount)
	{
		int num = totalGridCount / gridPerCircle;
		base.ui.Txt_CricleNum.text = string.Format(6050101.GetLocal(UIStringType.DiceActivity), num);
	}

	public override void Refresh()
	{
		base.Refresh();
		foreach (Model_UIActivityDice_Grid gridModel in gridModels)
		{
			gridModel.Refresh();
		}
		foreach (Model_UIActivityDice_DiceMission diceMissionModel in diceMissionModels)
		{
			diceMissionModel.Refresh();
		}
		foreach (Model_UIActivityDice_Progress rewardModel in rewardModels)
		{
			rewardModel.Refresh();
		}
		receiveBtnModel.Refresh();
		int itemCount = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(diceItemId);
		int itemCount2 = SimpleSingletonProvider<GameLogicManager>.inst.bag.GetItemCount(progressExpId);
		RefreshDiceCount(itemCount);
		RefreshProgress(itemCount2);
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		foreach (Model_UIActivityDice_Grid gridModel in gridModels)
		{
			gridModel.AddEvent();
		}
		foreach (Model_UIActivityDice_DiceMission diceMissionModel in diceMissionModels)
		{
			diceMissionModel.AddEvent();
			diceMissionModel.ResortMissionList = ResortMissionList;
		}
		foreach (Model_UIActivityDice_Progress rewardModel in rewardModels)
		{
			rewardModel.AddEvent();
		}
		receiveBtnModel.AddEvent();
		base.ui.Button_Throw.onClick.Add(OnDiceButtonDown);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.onItemCountChanged.AddListener(OnItemCountChanged);
		LaborActDiceS2CRPC laborActDiceS2C = MonoSingletonProvider<NetManager>.inst.RPC.LaborActDiceS2C;
		laborActDiceS2C.OnLaborActDiceS2CServerCallBackAsync = (LaborActDiceS2CRPC.OnLaborActDiceS2CServerDelegate)Delegate.Combine(laborActDiceS2C.OnLaborActDiceS2CServerCallBackAsync, new LaborActDiceS2CRPC.OnLaborActDiceS2CServerDelegate(OnLaborActDiceS2C));
		SimpleSingletonProvider<UIManager>.inst.reward.onClose.AddListener(OnRewardWindowClose);
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		foreach (Model_UIActivityDice_Grid gridModel in gridModels)
		{
			gridModel.RemoveEvent();
		}
		foreach (Model_UIActivityDice_DiceMission diceMissionModel in diceMissionModels)
		{
			diceMissionModel.RemoveEvent();
			diceMissionModel.ResortMissionList = null;
		}
		foreach (Model_UIActivityDice_Progress rewardModel in rewardModels)
		{
			rewardModel.RemoveEvent();
		}
		receiveBtnModel.RemoveEvent();
		base.ui.Button_Throw.onClick.Remove(OnDiceButtonDown);
		SimpleSingletonProvider<GameLogicManager>.inst.bag.signal.onItemCountChanged.RemoveListener(OnItemCountChanged);
		LaborActDiceS2CRPC laborActDiceS2C = MonoSingletonProvider<NetManager>.inst.RPC.LaborActDiceS2C;
		laborActDiceS2C.OnLaborActDiceS2CServerCallBackAsync = (LaborActDiceS2CRPC.OnLaborActDiceS2CServerDelegate)Delegate.Remove(laborActDiceS2C.OnLaborActDiceS2CServerCallBackAsync, new LaborActDiceS2CRPC.OnLaborActDiceS2CServerDelegate(OnLaborActDiceS2C));
		SimpleSingletonProvider<UIManager>.inst.reward.onClose.RemoveListener(OnRewardWindowClose);
	}

	protected override void AddListener()
	{
		base.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
	}

	public override void Close()
	{
		base.Close();
		playerMoveSeq?.Kill();
		playerAniTween?.Kill();
		Debug.Log("骰骰乐界面：关闭界面");
		if ((bool)diceModelCtrl)
		{
			Addressables.Release(diceModelCtrl.gameObject);
			diceModelCtrl = null;
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}
}
