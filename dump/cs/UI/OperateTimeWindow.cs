using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class OperateTimeWindow : BaseWindow
{
	private int warningEffectId = 1014;

	private bool isCreateEffect;

	private float warningTimePoint = 3f;

	private int audioEventId = 37;

	private float audioInterval = 1.25f;

	private float audioStartTimePoint = 9999f;

	private ParticleSystem particleSystem;

	public OperateTimeWindow(UIWindowType type)
		: base(type)
	{
	}

	private void ClearAudioStartPoint()
	{
		audioStartTimePoint = 9999f;
	}

	protected override void OnInit()
	{
		base.contentPane = UIOperateTimeWindow.CreateInstance();
		base.OnInit();
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
		if (base.contentPane is UIOperateTimeWindow uIOperateTimeWindow)
		{
			uIOperateTimeWindow.graph_warning.visible = false;
		}
	}

	public void UpdateOperationProgress(int operationTimeTotal, float _, string headUrl, bool showWarning = false)
	{
		TryShowAsync().Forget();
		if (base.contentPane is UIOperateTimeWindow uIOperateTimeWindow)
		{
			uIOperateTimeWindow.progress_OperationTime.type.selectedIndex = 0;
			uIOperateTimeWindow.progress_OperationTime.loader_Head.url = headUrl;
			uIOperateTimeWindow.progress_OperationTime.loader_Head.visible = _ > 0f;
			RefreshProgress(operationTimeTotal, _, showWarning);
		}
	}

	private void RefreshProgress(int time, float f, bool showWarning = false)
	{
		if (base.contentPane is UIOperateTimeWindow uIOperateTimeWindow)
		{
			uIOperateTimeWindow.progress_OperationTime.max = time;
			uIOperateTimeWindow.progress_OperationTime.min = 0.0;
			uIOperateTimeWindow.progress_OperationTime.value = f;
			ShowWarningEffect((float)time - f, showWarning).Forget();
		}
	}

	private async UniTask ShowWarningEffect(float remainTime, bool showWarning = false)
	{
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UIOperateTimeWindow win))
		{
			return;
		}
		bool isShowing = showWarning && remainTime < warningTimePoint && remainTime > 0f && GameSettings.ActionSuggest;
		if (isShowing && !isCreateEffect)
		{
			isCreateEffect = true;
			particleSystem = (await SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(warningEffectId.GetEffectDataConfigure().EffectName, win.graph_warning)).GetComponentInChildren<ParticleSystem>();
			win.graph_warning.visible = true;
			ClearAudioStartPoint();
		}
		if (!isShowing && (Object)(object)particleSystem != null)
		{
			if (particleSystem.isPlaying)
			{
				particleSystem.Stop(true, (ParticleSystemStopBehavior)1);
			}
			ClearAudioStartPoint();
		}
		if (isShowing && (Object)(object)particleSystem != null)
		{
			if (!particleSystem.isPlaying)
			{
				particleSystem.Simulate(0f, true);
				particleSystem.Play(true);
			}
			if (audioStartTimePoint - remainTime > audioInterval)
			{
				audioStartTimePoint = remainTime;
				Stage.inst.PlayOneShotSound(audioEventId);
			}
		}
	}

	public void UpdateShowProgress(int ShowTimeTotal, float _, string headUrl)
	{
		RoomLogic room = SimpleSingletonProvider<GameLogicManager>.inst.room;
		if (room == null)
		{
			return;
		}
		RoomController roomController = room.roomController;
		if (roomController != null && roomController.roomStateType == RoomStateType.RUNNING)
		{
			TryShowAsync().Forget();
			if (base.contentPane is UIOperateTimeWindow uIOperateTimeWindow)
			{
				uIOperateTimeWindow.progress_OperationTime.type.selectedIndex = 1;
				uIOperateTimeWindow.progress_OperationTime.loader_Head.url = headUrl;
				uIOperateTimeWindow.progress_OperationTime.loader_Head.visible = _ > 0f;
				RefreshProgress(ShowTimeTotal, _);
			}
		}
	}
}
