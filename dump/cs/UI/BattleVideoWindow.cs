using System;
using Core.Unit;
using CriWare;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Google.Protobuf.Collections;
using Tools;
using UnityEngine;

namespace UI;

public class BattleVideoWindow : BaseWindow
{
	private string _videoKey;

	private int _audioId;

	public BattleVideoWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UIBattleVideoWindow.CreateInstance();
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
		_ = base.contentPane is UIBattleVideoWindow;
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UIBattleVideoWindow uIBattleVideoWindow && !string.IsNullOrEmpty(_videoKey))
		{
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uIBattleVideoWindow.loader_Video);
			SimpleSingletonProvider<CriMovieManager>.inst.ClearOne(_videoKey);
			_videoKey = null;
		}
	}

	public async UniTask PlayCombineVideo(RepeatedField<int> combineMonsterIds)
	{
		if (combineMonsterIds == null || combineMonsterIds.Count != 2)
		{
			Debug.LogWarning($"PveBossCombineS2C Ids Error! {combineMonsterIds}");
		}
		_videoKey = 100105501.GetVideoKey();
		_audioId = 1101;
		UniTaskCompletionSource t = new UniTaskCompletionSource();
		await PlayVideo(_videoKey, delegate
		{
			if (_audioId != 0)
			{
				Stage.inst.PlayOneShotSound(_audioId);
			}
		}, delegate
		{
			t.TrySetResult();
			Hide();
		});
		await t.Task;
		if (SimpleSingletonProvider<LandManager>.inst.MapGimmickManager is MapGimmickManager010 mapGimmickManager)
		{
			mapGimmickManager.PlayEffectFireworks();
		}
	}

	public async UniTask Play21023CardVideo()
	{
		UniTaskCompletionSource t = new UniTaskCompletionSource();
		await PlayVideo(100105502.GetVideoKey(), null, delegate(Player source, int status)
		{
			t.TrySetResult();
			if (source != null)
			{
				((CriDisposable)source).Dispose();
			}
			Hide();
		});
		await t.Task;
	}

	public async UniTask<Player> PlayVideo(string key, Action<Player, int> onPlayReady = null, Action<Player, int> onPlayFinished = null, Action<EventPoint, Player> onPlayCuePoint = null, int initFrame = 0, bool PlayEndImmediatelyStop = false)
	{
		await TryShowAsync();
		if (!(base.contentPane is UIBattleVideoWindow uIBattleVideoWindow))
		{
			await UniTask.CompletedTask;
			onPlayFinished?.Invoke(null, 0);
			return null;
		}
		return await SimpleSingletonProvider<CriMovieManager>.inst.Play(key, uIBattleVideoWindow.loader_Video, onPlayReady, onPlayFinished, onPlayCuePoint, initFrame, PlayEndImmediatelyStop);
	}
}
