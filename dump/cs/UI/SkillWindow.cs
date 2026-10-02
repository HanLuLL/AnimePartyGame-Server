using Core;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;

namespace UI;

public class SkillWindow : BaseWindow
{
	private string _videoKey;

	private CtsInfo skillShowCts;

	public SkillWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UISkillWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask ShowSkillWin(long playerId)
	{
		BattlePlayerData playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(playerId);
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		if (base.contentPane is UISkillWindow uISkillWindow)
		{
			SimpleSingletonProvider<GameLogicManager>.inst.battle.PlayVoice(HeroVoiceType.SKILL, playerId);
			(float, float) tuple = UIHelper.ExpandToAspectRatio(uISkillWindow.loader_Character.width, uISkillWindow.loader_Character.height, 75f, 54f);
			uISkillWindow.loader_Character.SetSize(tuple.Item1, tuple.Item2);
			if (!string.IsNullOrEmpty(_videoKey))
			{
				DisposeVideo();
			}
			_videoKey = playerData.player.standingPainting.GetSkillVideo();
			await SimpleSingletonProvider<CriMovieManager>.inst.Play(_videoKey, uISkillWindow.loader_Character, OnVideoReady, OnVideoFinished);
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntil(() => !base.isShowing);
		}
	}

	private void OnVideoReady(Player source, int status)
	{
		skillShowCts = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
	}

	private void OnVideoFinished(Player source, int status)
	{
		SimpleSingletonProvider<DelaySignalManager>.inst.CancelTask(skillShowCts);
		Hide();
	}

	protected override void OnShown()
	{
		base.OnShown();
		if (base.contentPane is UISkillWindow uISkillWindow)
		{
			uISkillWindow.loader_Character.shape.graphics.material = CriMovieManager.CriMaterial;
		}
		if (SimpleSingletonProvider<UIManager>.inst.cardWindow.isShowing)
		{
			SimpleSingletonProvider<UIManager>.inst.cardWindow.OnCloseWin();
		}
	}

	protected override void OnHide()
	{
		if (base.contentPane is UISkillWindow uISkillWindow)
		{
			SimpleSingletonProvider<DelaySignalManager>.inst.DisposeCts(skillShowCts);
			SimpleSingletonProvider<CriMovieManager>.inst.StopAndDestroy(uISkillWindow.loader_Character);
			uISkillWindow.loader_Character.visible = false;
			base.OnHide();
			DisposeVideo();
		}
	}

	private void DisposeVideo()
	{
		SimpleSingletonProvider<CriMovieManager>.inst.ClearOne(_videoKey);
		_videoKey = null;
	}
}
