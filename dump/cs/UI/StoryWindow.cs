using System;
using System.Collections.Generic;
using Core;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using GameLogic.PlotTree;
using Tools;
using UI.Extend;
using UnityEngine;
using UnityTimer;

namespace UI;

public class StoryWindow : BaseWindow
{
	public PlotTreeBase CurrentStoryRoot;

	private DialogNode CurrentDialogNode;

	private float wScale;

	private float hScale;

	private readonly Dictionary<int, Story_PerformLoader> _PerformLoaderDict = new Dictionary<int, Story_PerformLoader>();

	private TypingEffect _TypingEffect;

	private Timer AutoTimer;

	private Action FinishAction;

	public StoryWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIStoryWindow.CreateInstance();
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
		UIContentScaler component = Stage.inst.gameObject.GetComponent<UIContentScaler>();
		wScale = base.width / (float)component.designResolutionX;
		hScale = base.height / (float)component.designResolutionY;
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UIStoryWindow uIStoryWindow)
		{
			if (SimpleSingletonProvider<GameLogicManager>.inst.fight != null)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: true, UIPanelType.None);
			}
			if (SimpleSingletonProvider<GameLogicManager>.inst.battle != null)
			{
				Vector2 eSCPosition = SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.GetESCPosition();
				uIStoryWindow.btn_ESC.SetXY(eSCPosition.x, eSCPosition.y);
			}
			SimpleSingletonProvider<UIManager>.inst.expression.visible = false;
			uIStoryWindow.com_Dialog.onClick.Add(TickNextDialog);
			uIStoryWindow.btn_Skip.onClick.Add(FinishCurrentStory);
			uIStoryWindow.btn_ESC.onClick.Add(OpenSettingPanel);
			uIStoryWindow.btn_Skip.txt_Title.text = 1082.GetLocal(UIStringType.Message);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (!(base.contentPane is UIStoryWindow uIStoryWindow))
		{
			return;
		}
		SimpleSingletonProvider<GameLogicManager>.inst.fight.signal.readyFight.Dispatch(t1: false, UIPanelType.None);
		SimpleSingletonProvider<UIManager>.inst.expression.visible = true;
		uIStoryWindow.com_Dialog.txt_Dialog.text = "";
		uIStoryWindow.com_Dialog.loader_Style.visible = false;
		uIStoryWindow.com_Dialog.onClick.Remove(TickNextDialog);
		uIStoryWindow.btn_Skip.onClick.Remove(FinishCurrentStory);
		uIStoryWindow.btn_ESC.onClick.Remove(OpenSettingPanel);
		uIStoryWindow.com_Dialog.touchable = false;
		CurrentStoryRoot = null;
		CurrentDialogNode = null;
		FinishAction = null;
		foreach (Story_PerformLoader value in _PerformLoaderDict.Values)
		{
			value.Dispose();
		}
		_PerformLoaderDict.Clear();
		CancelPrintDialog();
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uIStoryWindow.com_Video.graph_CutInVideo);
		SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uIStoryWindow.com_Video.graph_LoopVideo);
	}

	private async void OpenSettingPanel()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UIStoryWindow win)
		{
			win.btn_ESC.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.settingInBattle.ShowSettingInBattle();
			win.btn_ESC.onClick.Release();
		}
	}

	private void FinishCurrentStory()
	{
		if (base.contentPane is UIStoryWindow uIStoryWindow)
		{
			uIStoryWindow.btn_Skip.onClick.Retain();
			Timer autoTimer = AutoTimer;
			if (autoTimer != null)
			{
				autoTimer.Cancel();
			}
			CancelPrintDialog();
			CurrentStoryRoot?.ChangeFailState();
			FinishStory();
			uIStoryWindow.btn_Skip.onClick.Release();
		}
	}

	protected override void OnKeyDown(EventContext context)
	{
		KeyCode keyCode = context.inputEvent.keyCode;
		if (keyCode == KeyCode.Space || keyCode == KeyCode.Return)
		{
			TickNextDialog();
		}
	}

	private void CancelPrintDialog()
	{
		Timers.inst.Remove(PrintDialogText);
		_TypingEffect?.Cancel();
	}

	private void TickNextDialog()
	{
		if (base.contentPane is UIStoryWindow uIStoryWindow)
		{
			Timer autoTimer = AutoTimer;
			if (autoTimer != null)
			{
				autoTimer.Cancel();
			}
			CancelPrintDialog();
			PlotState nodeState = CurrentStoryRoot.NodeState;
			if (nodeState == PlotState.Success || nodeState == PlotState.Failure)
			{
				FinishStory();
				return;
			}
			uIStoryWindow.com_Dialog.touchable = false;
			CurrentStoryRoot.Tick();
		}
	}

	public async UniTask TryShowStory(StoryData storyData, Action finishAction)
	{
		if (storyData == null || storyData.Root == null)
		{
			Debug.LogError("当前剧情数据无法使用");
			return;
		}
		FinishAction = finishAction;
		CurrentStoryRoot = storyData.Root;
		await TryShowAsync();
		CurrentStoryRoot.Tick();
	}

	public async UniTask TryShowDialog(DialogNode dialogNode)
	{
		CurrentDialogNode = dialogNode;
		await TryShowAsync();
		RefreshVideo().Forget();
		RefreshSpeaker();
		await RefreshPerform();
		RefreshDialog();
		PlayAudio();
	}

	private void PlayAudio()
	{
		if (CurrentDialogNode == null || string.IsNullOrWhiteSpace(CurrentDialogNode.AudioIds))
		{
			return;
		}
		string[] array = CurrentDialogNode.AudioIds.Split(",");
		for (int i = 0; i < array.Length; i++)
		{
			if (int.TryParse(array[i], out var result))
			{
				if ((long)result > 0L)
				{
					Stage.inst.PlayOneShotSound(result);
				}
			}
			else
			{
				Debug.LogError("读取音效Id失败，请检查剧情节点" + CurrentDialogNode.Guid);
			}
		}
	}

	private void RefreshDialog()
	{
		if (CurrentDialogNode == null)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UIStoryWindow win = gComponent as UIStoryWindow;
		if (win == null)
		{
			return;
		}
		win.com_Dialog.txt_Dialog.text = CurrentDialogNode.DialogId.GetLocal(UIStringType.Story);
		win.com_Dialog.touchable = true;
		float readingTime = StoryConfig.MinReadingTime;
		if (CurrentDialogNode.readTimes.Count > 0)
		{
			DialogReadTime dialogReadTime = CurrentDialogNode.readTimes.Find((DialogReadTime timeData) => timeData.Language == GameSettings.languageType);
			if (dialogReadTime != null)
			{
				readingTime = dialogReadTime.Time;
				Debug.Log($"成功获取当前对话所需要的阅读时长{readingTime}");
			}
			else
			{
				Debug.LogWarning("未能获取当前对话的阅读时长，将采用最小阅读时间");
			}
		}
		if (!string.IsNullOrWhiteSpace(win.com_Dialog.txt_Dialog.text))
		{
			_TypingEffect = new TypingEffect(win.com_Dialog.txt_Dialog);
			_TypingEffect.Start();
			int num = _TypingEffect.ChildCount();
			float num2 = 0.03f;
			if (num > 0)
			{
				num2 = Mathf.Min(readingTime / (float)_TypingEffect.ChildCount(), num2);
			}
			Timers.inst.Add(num2, 0, PrintDialogText);
		}
		else
		{
			Debug.LogWarning("通过剧情配置获取的是空的对话索引");
		}
		win.progress_Time.max = readingTime;
		win.progress_Time.value = readingTime;
		Timer autoTimer = AutoTimer;
		if (autoTimer != null)
		{
			autoTimer.Cancel();
		}
		AutoTimer = Timer.Register(0f, readingTime, (Action)TickNextDialog, (Action)null, (Action)null, (Action)null, (Action)null, (Action<float>)delegate(float t)
		{
			if (CurrentStoryRoot != null)
			{
				PlotState nodeState = CurrentStoryRoot.NodeState;
				if (nodeState != PlotState.Success && nodeState != PlotState.Failure)
				{
					win.progress_Time.value = readingTime - t;
				}
			}
		}, (Action)null, false, -1f, false, win.displayObject.gameObject);
	}

	private void RefreshSpeaker()
	{
		if (CurrentDialogNode == null || !(base.contentPane is UIStoryWindow uIStoryWindow))
		{
			return;
		}
		if (StaticConfigure.Story.SpeakerDict.TryGetValue(CurrentDialogNode.SpeakerId, out var value))
		{
			uIStoryWindow.loader_Speaker.url = value.ProfilePhoto;
			uIStoryWindow.txt_Speaker.text = value.CharacterID.GetLocal(UIStringType.Story);
			if (ColorUtility.TryParseHtmlString(value.ProfileColor, out var color))
			{
				uIStoryWindow.graph_Speaker.color = color;
			}
			uIStoryWindow.group_Speaker.visible = true;
			uIStoryWindow.com_Dialog.loader_Style.visible = true;
			uIStoryWindow.com_Dialog.frame.selectedIndex = value.FrameIndex;
		}
		else
		{
			uIStoryWindow.group_Speaker.visible = false;
			Debug.LogWarning($"通过剧情配置发言人id{CurrentDialogNode.SpeakerId}，无法从Story.SpeakerDict获取数据");
		}
	}

	private void PrintDialogText(object param)
	{
		if (!_TypingEffect.Print())
		{
			Timers.inst.Remove(PrintDialogText);
		}
	}

	private async UniTask RefreshPerform()
	{
		foreach (Story_PerformLoader value in _PerformLoaderDict.Values)
		{
			value.Stop();
		}
		if (CurrentDialogNode == null)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UIStoryWindow win))
		{
			return;
		}
		List<DialogNode.DialogPerform> performs = CurrentDialogNode.Performs;
		List<Story_PerformLoader> list = new List<Story_PerformLoader>();
		for (int i = 0; i < performs.Count; i++)
		{
			int key = performs[i].id;
			if (!_PerformLoaderDict.Remove(key, out var loader))
			{
				loader = new Story_PerformLoader();
			}
			await loader.Refresh(performs[i], win.com_Perform, wScale, hScale);
			list.Add(loader);
			loader = null;
		}
		foreach (Story_PerformLoader value2 in _PerformLoaderDict.Values)
		{
			value2.HideLoader();
		}
		foreach (Story_PerformLoader item in list)
		{
			_PerformLoaderDict.TryAdd(item.PerformId, item);
		}
		win.InvalidateBatchingState();
	}

	private async UniTask RefreshVideo()
	{
		if (CurrentDialogNode == null || CurrentDialogNode.VideoType == VideoType.None)
		{
			return;
		}
		GComponent gComponent = base.contentPane;
		UIStoryWindow win = gComponent as UIStoryWindow;
		if (win == null)
		{
			return;
		}
		if (CurrentDialogNode.VideoType == VideoType.End && CurrentDialogNode.VideoId == 0)
		{
			SimpleSingletonProvider<CriMovieManager>.inst.Stop(win.com_Video.graph_CutInVideo);
			SimpleSingletonProvider<CriMovieManager>.inst.Stop(win.com_Video.graph_LoopVideo);
		}
		else
		{
			if (!StaticConfigure.Video.VideoQueueDict.TryGetValue(CurrentDialogNode.VideoId, out var videoQueue))
			{
				return;
			}
			if (CurrentDialogNode.VideoType == VideoType.Start)
			{
				Player loopPlayer = null;
				if (!string.IsNullOrWhiteSpace(videoQueue.LoadedKeyLoop))
				{
					win.com_Video.graph_LoopVideo.SetScale(0f, 0f);
					loopPlayer = await SimpleSingletonProvider<CriMovieManager>.inst.Play(videoQueue.LoadedKeyLoop, win.com_Video.graph_LoopVideo);
					loopPlayer.Pause(true);
				}
				if (string.IsNullOrWhiteSpace(videoQueue.LoadedKeyStart))
				{
					return;
				}
				SimpleSingletonProvider<CriMovieManager>.inst.Play(videoQueue.LoadedKeyStart, win.com_Video.graph_CutInVideo, delegate
				{
					win.com_Video.graph_CutInVideo.SetScale(1f, 1f);
				}, delegate
				{
					if (loopPlayer != null)
					{
						win.com_Video.graph_LoopVideo.SetScale(1f, 1f);
						loopPlayer.Pause(false);
						win.com_Video.graph_CutInVideo.SetScale(0f, 0f);
						SimpleSingletonProvider<CriMovieManager>.inst.Stop(win.com_Video.graph_CutInVideo);
					}
				}).Forget();
			}
			else
			{
				if (CurrentDialogNode.VideoType != VideoType.End)
				{
					return;
				}
				if (!string.IsNullOrWhiteSpace(videoQueue.LoadedKeyEnd))
				{
					SimpleSingletonProvider<CriMovieManager>.inst.Play(videoQueue.LoadedKeyEnd, win.com_Video.graph_CutInVideo, delegate
					{
						win.com_Video.graph_CutInVideo.SetScale(1f, 1f);
						win.com_Video.graph_LoopVideo.SetScale(0f, 0f);
					}, delegate
					{
						win.com_Video.graph_CutInVideo.SetScale(0f, 0f);
						SimpleSingletonProvider<CriMovieManager>.inst.Stop(win.com_Video.graph_CutInVideo);
						SimpleSingletonProvider<CriMovieManager>.inst.Stop(win.com_Video.graph_LoopVideo);
					}).Forget();
				}
				else
				{
					SimpleSingletonProvider<CriMovieManager>.inst.Stop(win.com_Video.graph_CutInVideo);
					SimpleSingletonProvider<CriMovieManager>.inst.Stop(win.com_Video.graph_LoopVideo);
				}
			}
		}
	}

	private void TryShowLoopVideo(string startKey, string loopKey)
	{
		_ = base.contentPane is UIStoryWindow;
	}

	private void FinishStory()
	{
		FinishAction?.Invoke();
		Hide();
	}
}
