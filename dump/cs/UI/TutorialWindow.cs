using Core;
using Core.Audio;
using Core.Scene;
using Core.Unit;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using UnityEngine;

namespace UI;

public class TutorialWindow : BaseWindow
{
	private int _currentDialogIndex;

	private TutorialdialogConfigure _dialogConfig;

	private CtsInfo _tutorialTsc;

	private TypingEffect _TypingEffect;

	private uint _audioPlayerId;

	private CtsInfo _maskTsc;

	private CtsInfo _selectHeroTsc;

	private CtsInfo _systemTsc;

	private CtsInfo _skipTsc;

	public TutorialWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UITutorialWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask TryShowAsync()
	{
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Login)
		{
			Hide();
			return;
		}
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
		if (base.contentPane is UITutorialWindow uITutorialWindow)
		{
			uITutorialWindow.com_Tutorial.onClick.Add(TickNextDialog);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UITutorialWindow uITutorialWindow)
		{
			uITutorialWindow.com_Tutorial.onClick.Remove(TickNextDialog);
			_TypingEffect = null;
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public async UniTask TryShowTutorial(int tutorialId)
	{
		SimpleSingletonProvider<WebServerManager>.inst.PostTutorialRecord(tutorialId);
		if (!StaticConfigure.Tutorial.DialogDict.TryGetValue(tutorialId, out _dialogConfig))
		{
			Debug.LogError($"无法通过{tutorialId} 在Tutorial.DialogDict中获取配置");
			return;
		}
		_tutorialTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		await TryShowAsync();
		if (base.contentPane is UITutorialWindow uITutorialWindow)
		{
			uITutorialWindow.tab.selectedIndex = 0;
			uITutorialWindow.com_Tutorial.Cut_in.Play();
			TryShowDialog(0).Forget();
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_tutorialTsc);
		}
	}

	private async UniTask TryShowDialog(int index)
	{
		if (_audioPlayerId != 0)
		{
			SimpleSingletonProvider<AudioManager>.inst.StopPlayingBGM(_audioPlayerId);
			_audioPlayerId = 0u;
		}
		if (_dialogConfig == null)
		{
			FinishTutorial();
			return;
		}
		if (_dialogConfig.TutorialdialogConfigureItems.Count <= index)
		{
			FinishTutorial();
			return;
		}
		_currentDialogIndex = index;
		await RefreshDialog(index);
	}

	private async UniTask RefreshDialog(int index)
	{
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UITutorialWindow win))
		{
			return;
		}
		win.com_Tutorial.touchable = false;
		win.com_Tutorial.loader_Speaker.url = _dialogConfig.ProfilePhoto;
		win.com_Tutorial.txt_Speaker.text = 1080.GetLocal(UIStringType.Character);
		win.com_Tutorial.group_Speaker.visible = true;
		await win.com_Tutorial.RefreshStandingPainting(_dialogConfig, index);
		win.com_BG.visible = true;
		win.com_BG.showBg.selectedIndex = ((SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Battle) ? 1 : 2);
		win.com_Tutorial.visible = true;
		win.com_Tutorial.touchable = true;
		TutorialdialogConfigureItem tutorialdialogConfigureItem = _dialogConfig.TutorialdialogConfigureItems[index];
		string characterName;
		if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
		{
			BattlePlayerData selfPlayerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetSelfPlayerData();
			characterName = CharacterHandle.GetCharacterName(selfPlayerData.player.characterConfig.Id, selfPlayerData.player.characterConfig.CharacterType);
		}
		else
		{
			characterName = SimpleSingletonProvider<GameLogicManager>.inst.account.GetName();
		}
		win.com_Tutorial.com_Dialog.txt_Dialog.text = string.Format(tutorialdialogConfigureItem.ContentId.GetLocal(UIStringType.Tutorial), characterName);
		LanguageType voiceLanguage = GameSettings.GetVoiceLanguage();
		if ((voiceLanguage == LanguageType.SimplifiedChinese || voiceLanguage == LanguageType.TraditionalChinese) && (long)tutorialdialogConfigureItem.AudioId > 0L)
		{
			_audioPlayerId = SimpleSingletonProvider<AudioManager>.inst.SendEvent(tutorialdialogConfigureItem.AudioId, Stage.inst.gameObject);
		}
		float num = 1.5f;
		if (!string.IsNullOrWhiteSpace(win.com_Tutorial.com_Dialog.txt_Dialog.text))
		{
			_TypingEffect = new TypingEffect(win.com_Tutorial.com_Dialog.txt_Dialog);
			_TypingEffect.Start();
			int num2 = _TypingEffect.ChildCount();
			float num3 = 0.03f;
			if (num2 > 0)
			{
				num3 = Mathf.Min(num / (float)_TypingEffect.ChildCount(), num3);
			}
			Timers.inst.Add(num3, 0, PrintDialogText);
		}
		else
		{
			Debug.LogWarning("通过剧情配置获取的是空的对话索引");
		}
	}

	private void PrintDialogText(object param)
	{
		if (_TypingEffect == null || !_TypingEffect.Print())
		{
			Timers.inst.Remove(PrintDialogText);
		}
	}

	private void CancelPrintDialog()
	{
		Timers.inst.Remove(PrintDialogText);
		_TypingEffect?.Cancel();
	}

	private void FinishTutorial()
	{
		if (base.contentPane is UITutorialWindow uITutorialWindow)
		{
			uITutorialWindow.com_Tutorial.visible = false;
			if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value == SceneType.Battle)
			{
				Hide();
			}
		}
		_tutorialTsc?.Cancel();
	}

	private void TickNextDialog()
	{
		if (base.contentPane is UITutorialWindow uITutorialWindow)
		{
			Stage.inst.PlayOneShotSound(1);
			CancelPrintDialog();
			uITutorialWindow.com_Tutorial.touchable = false;
			TryShowDialog(_currentDialogIndex + 1).Forget();
		}
	}

	private async UniTask ShowMaskCom()
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITutorialWindow win = gComponent as UITutorialWindow;
		if (win != null)
		{
			win.tab.selectedIndex = 1;
			win.com_Mask.visible = true;
			_maskTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
			win.btn_Mask.onClick.Set((EventCallback0)delegate
			{
				win.com_Mask.visible = false;
				Stage.inst.PlayOneShotSound(1);
				Hide();
				_maskTsc?.Cancel();
				_maskTsc = null;
			});
		}
	}

	public async UniTask TryShowBattleLabel()
	{
		await ShowMaskCom();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.ShowPlayerBattleLabel();
		if (_maskTsc != null)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_maskTsc);
		}
	}

	public async UniTask TryShowFillingStation(UnitLand land)
	{
		Camera mainCamera = BattleSceneController.inst?.mainCamera;
		if (!(mainCamera == null))
		{
			await ShowMaskCom();
			Vector3 vector = mainCamera.WorldToScreenPoint(land.transform.position);
			vector.y = (float)Screen.height - vector.y;
			float num = 300f;
			float num2 = 280f;
			Vector2 pos = GlobalToLocal(vector) - new Vector2(num * 0.5f, num2 * 0.5f);
			ShowGuideMask(pos, num, num2, isRect: true);
			if (_maskTsc != null)
			{
				await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_maskTsc);
			}
		}
	}

	public async UniTask TryShowPVEProgress()
	{
		await ShowMaskCom();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.ShowPVEProgress();
		if (_maskTsc != null)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_maskTsc);
		}
	}

	public void ShowGuideMask(Vector2 _pos, float w, float h, bool isRect)
	{
		if (base.contentPane is UITutorialWindow uITutorialWindow)
		{
			uITutorialWindow.com_Mask.graph_RectMask.SetSize(w, h);
			uITutorialWindow.com_Mask.graph_RectMask.SetXY(_pos.x, _pos.y);
			uITutorialWindow.com_Mask.graph_CircularMask.SetSize(w, h);
			uITutorialWindow.com_Mask.graph_CircularMask.SetXY(_pos.x, _pos.y);
			if (isRect)
			{
				uITutorialWindow.com_Mask.graph_RectMask.visible = true;
				uITutorialWindow.com_Mask.graph_CircularMask.visible = false;
				uITutorialWindow.com_Mask.mask = uITutorialWindow.com_Mask.graph_RectMask.displayObject;
				uITutorialWindow.com_Mask.graph_RectMask.AddRelation(uITutorialWindow.com_Mask, FairyGUI.RelationType.Center_Center);
				uITutorialWindow.com_Mask.graph_RectMask.AddRelation(uITutorialWindow.com_Mask, FairyGUI.RelationType.Middle_Middle);
			}
			else
			{
				uITutorialWindow.com_Mask.graph_RectMask.visible = false;
				uITutorialWindow.com_Mask.graph_CircularMask.visible = true;
				uITutorialWindow.com_Mask.mask = uITutorialWindow.com_Mask.graph_CircularMask.displayObject;
				uITutorialWindow.com_Mask.graph_CircularMask.AddRelation(uITutorialWindow.com_Mask, FairyGUI.RelationType.Center_Center);
				uITutorialWindow.com_Mask.graph_CircularMask.AddRelation(uITutorialWindow.com_Mask, FairyGUI.RelationType.Middle_Middle);
			}
			if (_pos.y < GRoot.inst.height * 0.5f)
			{
				uITutorialWindow.com_Mask.com_Arrow.rotation = 0f;
				uITutorialWindow.com_Mask.com_Arrow.SetXY(_pos.x, _pos.y + h);
			}
			else
			{
				uITutorialWindow.com_Mask.com_Arrow.rotation = 180f;
				uITutorialWindow.com_Mask.com_Arrow.SetXY(_pos.x + w * 0.5f, _pos.y);
			}
			uITutorialWindow.com_Mask.com_Arrow.visible = true;
			uITutorialWindow.com_Mask.com_Arrow.AddRelation(uITutorialWindow.com_Mask, FairyGUI.RelationType.Center_Center);
			uITutorialWindow.com_Mask.com_Arrow.AddRelation(uITutorialWindow.com_Mask, FairyGUI.RelationType.Middle_Middle);
		}
	}

	public async UniTask ShowOpenBattleInfoMask()
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITutorialWindow win = gComponent as UITutorialWindow;
		if (win == null)
		{
			return;
		}
		win.tab.selectedIndex = 1;
		win.com_Mask.visible = true;
		_maskTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.ShowOpenBattleInfo();
		win.btn_Mask.onClick.Release();
		win.btn_Mask.onClick.Set((EventCallback0)delegate
		{
			if (IsTouchBegin())
			{
				win.btn_Mask.onClick.Retain();
				Stage.inst.PlayOneShotSound(1);
				win.com_Mask.com_Arrow.visible = false;
				win.com_Mask.graph_CircularMask.visible = false;
				win.com_Mask.graph_RectMask.visible = false;
				win.com_Mask.visible = false;
				_maskTsc?.Cancel();
				_maskTsc = null;
			}
		});
		if (_maskTsc != null)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_maskTsc);
		}
	}

	public async UniTask ShowOpenSkillMask()
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITutorialWindow win = gComponent as UITutorialWindow;
		if (win == null)
		{
			return;
		}
		win.tab.selectedIndex = 1;
		win.com_Mask.visible = true;
		_maskTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		SimpleSingletonProvider<UIManager>.inst.battlePlayerInfo.ShowOpenSkillMask();
		win.btn_Mask.onClick.Release();
		win.btn_Mask.onClick.Set((EventCallback0)delegate
		{
			if (IsTouchBegin())
			{
				win.btn_Mask.onClick.Retain();
				Stage.inst.PlayOneShotSound(1);
				win.com_Mask.com_Arrow.visible = false;
				win.com_Mask.graph_CircularMask.visible = false;
				win.com_Mask.graph_RectMask.visible = false;
				win.com_Mask.visible = false;
				SimpleSingletonProvider<UIManager>.inst.battlePlayerInfo.ShowSkillInfoByTutorial().Forget();
				_maskTsc?.Cancel();
				_maskTsc = null;
			}
		});
		if (_maskTsc != null)
		{
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_maskTsc);
		}
	}

	public async UniTask ShowReleaseSkillMask()
	{
		await TryShowAsync();
		if (base.contentPane is UITutorialWindow uITutorialWindow)
		{
			uITutorialWindow.tab.selectedIndex = 1;
			uITutorialWindow.com_Mask.visible = true;
			_maskTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
			SimpleSingletonProvider<GameLogicManager>.inst.battle.handCard.ShowReleaseSkill();
			uITutorialWindow.btn_Mask.onClick.Release();
			uITutorialWindow.btn_Mask.onClick.Set((EventCallback0)delegate
			{
				Stage.inst.PlayOneShotSound(1);
				Hide();
				_maskTsc?.Cancel();
				_maskTsc = null;
			});
			if (_maskTsc != null)
			{
				await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_maskTsc);
			}
		}
	}

	private bool IsTouchBegin()
	{
		if (!(base.contentPane is UITutorialWindow uITutorialWindow))
		{
			return false;
		}
		Vector2 vector = GRoot.inst.GlobalToLocal(Stage.inst.touchPosition);
		Vector2 vector2 = Vector2.one;
		float num = 0f;
		float num2 = 0f;
		if (uITutorialWindow.com_Mask.graph_CircularMask.visible)
		{
			Vector2 pt = uITutorialWindow.com_Mask.graph_CircularMask.LocalToGlobal(Vector2.zero);
			vector2 = GRoot.inst.GlobalToLocal(pt);
			num = uITutorialWindow.com_Mask.graph_CircularMask.width;
			num2 = uITutorialWindow.com_Mask.graph_CircularMask.height;
		}
		else if (uITutorialWindow.com_Mask.graph_RectMask.visible)
		{
			Vector2 pt2 = uITutorialWindow.com_Mask.graph_RectMask.LocalToGlobal(Vector2.zero);
			vector2 = GRoot.inst.GlobalToLocal(pt2);
			num = uITutorialWindow.com_Mask.graph_RectMask.width;
			num2 = uITutorialWindow.com_Mask.graph_RectMask.height;
		}
		if (vector2.x < vector.x && vector2.y < vector.y && vector2.x + num > vector.x)
		{
			return vector2.y + num2 > vector.y;
		}
		return false;
	}

	private async UniTask ShowStartGameMask()
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITutorialWindow win = gComponent as UITutorialWindow;
		if (win == null)
		{
			return;
		}
		win.tab.selectedIndex = 1;
		win.com_Mask.visible = true;
		IBasePanel currentPanel = SimpleSingletonProvider<UIManager>.inst.currentPanel;
		HomePanel panel = currentPanel as HomePanel;
		if (panel == null)
		{
			return;
		}
		Vector2 item = (await panel.ShowStartGameMask()).Item1;
		UITutorial_Com_DialogTip com_DialogTip = ShowDialogTip(item.x, 0f, 21001);
		win.btn_Mask.onClick.Set((EventCallback0)delegate
		{
			if (IsTouchBegin())
			{
				win.btn_Mask.onClick.Retain();
				Stage.inst.PlayOneShotSound(1);
				win.com_Mask.com_Arrow.visible = false;
				win.com_Mask.graph_CircularMask.visible = false;
				win.com_Mask.graph_RectMask.visible = false;
				win.com_Mask.visible = false;
				if (com_DialogTip != null)
				{
					win.RemoveChild(com_DialogTip, dispose: true);
				}
				panel.FireClickStartGame().Forget();
			}
		});
	}

	public async UniTask ShowMatchEntranceMask()
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITutorialWindow win = gComponent as UITutorialWindow;
		if (win == null)
		{
			return;
		}
		win.tab.selectedIndex = 1;
		IBasePanel currentPanel = SimpleSingletonProvider<UIManager>.inst.currentPanel;
		MatchEntrancePanel panel = currentPanel as MatchEntrancePanel;
		if (panel == null)
		{
			return;
		}
		Vector2 item = (await panel.ShowCampaignMask()).Item1;
		win.com_Mask.visible = true;
		UITutorial_Com_DialogTip com_DialogTip = ShowDialogTip(item.x, 0f, 21002);
		win.btn_Mask.onClick.Release();
		win.btn_Mask.onClick.Set((EventCallback0)delegate
		{
			if (IsTouchBegin())
			{
				win.btn_Mask.onClick.Retain();
				Stage.inst.PlayOneShotSound(1);
				win.com_Mask.com_Arrow.visible = false;
				win.com_Mask.graph_CircularMask.visible = false;
				win.com_Mask.graph_RectMask.visible = false;
				win.com_Mask.visible = false;
				if (com_DialogTip != null)
				{
					win.RemoveChild(com_DialogTip, dispose: true);
				}
				panel.FireClickOpenSoloLevel().Forget();
			}
		});
	}

	public async UniTask ShowSoloLevelMask()
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITutorialWindow win = gComponent as UITutorialWindow;
		if (win == null)
		{
			return;
		}
		win.tab.selectedIndex = 1;
		IBasePanel currentPanel = SimpleSingletonProvider<UIManager>.inst.currentPanel;
		SoloLevelPanel panel = currentPanel as SoloLevelPanel;
		if (panel == null)
		{
			return;
		}
		await panel.ShowBasicModeMask();
		win.com_Mask.visible = true;
		win.btn_Mask.onClick.Release();
		win.btn_Mask.onClick.Set((EventCallback0)delegate
		{
			if (IsTouchBegin())
			{
				win.btn_Mask.onClick.Retain();
				Stage.inst.PlayOneShotSound(1);
				win.com_Mask.com_Arrow.visible = false;
				win.com_Mask.graph_CircularMask.visible = false;
				win.com_Mask.graph_RectMask.visible = false;
				win.com_Mask.visible = false;
				panel.FireClickOpenSoloLevel().Forget();
			}
		});
	}

	public async UniTask ShowCampaignLevelMask()
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UITutorialWindow win))
		{
			return;
		}
		win.tab.selectedIndex = 1;
		IBasePanel currentPanel = SimpleSingletonProvider<UIManager>.inst.currentPanel;
		CampaignPanel panel = currentPanel as CampaignPanel;
		if (panel == null)
		{
			return;
		}
		await panel.ShowStartCampaignMask();
		win.com_Mask.visible = true;
		win.btn_Mask.onClick.Release();
		win.btn_Mask.onClick.Set((EventCallback0)delegate
		{
			Stage.inst.PlayOneShotSound(1);
			if (IsTouchBegin())
			{
				SimpleSingletonProvider<GameLogicManager>.inst.tutorial.FinishTutorial(21999);
				Hide();
				panel.FireClickStartCampaignLevel();
			}
		});
	}

	private UITutorial_Com_DialogTip ShowDialogTip(float tipX, float tipY, int tutorialId)
	{
		if (!(base.contentPane is UITutorialWindow uITutorialWindow))
		{
			return null;
		}
		if (StaticConfigure.Tutorial.DialogDict.TryGetValue(tutorialId, out var value))
		{
			SimpleSingletonProvider<WebServerManager>.inst.PostTutorialRecord(tutorialId);
			UITutorial_Com_DialogTip uITutorial_Com_DialogTip = UITutorial_Com_DialogTip.CreateInstance();
			uITutorial_Com_DialogTip.loader_Icon.url = value.ProfilePhoto;
			TutorialdialogConfigureItem safeByIndex = value.TutorialdialogConfigureItems.GetSafeByIndex(0);
			if (safeByIndex != null)
			{
				uITutorial_Com_DialogTip.txt_Explain.text = safeByIndex.ContentId.GetLocal(UIStringType.Tutorial);
			}
			uITutorialWindow.AddChild(uITutorial_Com_DialogTip);
			uITutorial_Com_DialogTip.AddRelation(uITutorialWindow, FairyGUI.RelationType.Center_Center);
			if (uITutorial_Com_DialogTip.width + tipX > _width)
			{
				tipX = _width - uITutorial_Com_DialogTip.width;
			}
			uITutorial_Com_DialogTip.SetXY(tipX, tipY);
			return uITutorial_Com_DialogTip;
		}
		return null;
	}

	public async UniTask<(int, int)> TryShowSelectHero()
	{
		BGMHelper.TryPlayBGM(106);
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		if (!(gComponent is UITutorialWindow win))
		{
			return (0, 0);
		}
		_selectHeroTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
		UITutorial_Com_SelectHero _com_SelectHero = UITutorial_Com_SelectHero.CreateInstance();
		win.AddChild(_com_SelectHero);
		_com_SelectHero.MakeFullScreen();
		_com_SelectHero.visible = true;
		_com_SelectHero.ShowComponent();
		_com_SelectHero.AddRelation(win, FairyGUI.RelationType.Height);
		_com_SelectHero.AddRelation(win, FairyGUI.RelationType.Width);
		_com_SelectHero.btn_SureHero.onClick.Set((EventCallback0)delegate
		{
			_com_SelectHero.btn_SureHero.onClick.Retain();
			string characterNickName = CharacterHandle.GetCharacterNickName(_com_SelectHero.SelectHeroId);
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(string.Format(1.GetLocal(UIStringType.Tutorial), characterNickName), delegate
			{
				_com_SelectHero.OnClose();
				Hide();
				_selectHeroTsc?.Cancel();
			}).Forget();
			_com_SelectHero.btn_SureHero.onClick.Release();
		});
		await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_selectHeroTsc);
		if (_com_SelectHero.SelectHeroId == 0)
		{
			Hide();
			HeroCardData heroCardData = _com_SelectHero.HeroInfos?.GetSafeByIndex(0);
			if (heroCardData == null)
			{
				int[] heroPool = SimpleSingletonProvider<GameLogicManager>.inst.tutorial.HeroPool;
				_com_SelectHero.SelectHeroId = heroPool[0];
				_com_SelectHero.SelectSkinItemId = 0;
			}
			else
			{
				_com_SelectHero.SelectHeroId = heroCardData.HeroId;
				_com_SelectHero.SelectSkinItemId = heroCardData.standingPainting.ItemID;
			}
		}
		win.RemoveChild(_com_SelectHero, dispose: true);
		return (_com_SelectHero.SelectHeroId, _com_SelectHero.SelectSkinItemId);
	}

	public async UniTask TryShowFinishTip()
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITutorialWindow win = gComponent as UITutorialWindow;
		if (win == null)
		{
			return;
		}
		win.tab.selectedIndex = 1;
		win.com_Mask.visible = true;
		win.com_Mask.graph_RectMask.visible = false;
		win.com_Mask.graph_CircularMask.visible = false;
		win.com_Mask.com_Arrow.visible = false;
		UITutorial_Com_FinishTip _com_FinishTip = UITutorial_Com_FinishTip.CreateInstance();
		win.AddChild(_com_FinishTip);
		_com_FinishTip.MakeFullScreen();
		_com_FinishTip.visible = true;
		_com_FinishTip.AddRelation(win, FairyGUI.RelationType.Height);
		_com_FinishTip.AddRelation(win, FairyGUI.RelationType.Width);
		_com_FinishTip.RefreshTips();
		_com_FinishTip.onClick.Set((EventCallback0)delegate
		{
			if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HomePanel homePanel && homePanel.IsOpen())
			{
				_com_FinishTip.onClick.Retain();
				Stage.inst.PlayOneShotSound(1);
				SimpleSingletonProvider<UIManager>.inst.tutorial.TryShowGuideChoose(170).Forget();
				win.RemoveChild(_com_FinishTip, dispose: true);
				_com_FinishTip.onClick.Release();
			}
		});
	}

	public async UniTask TryShowSystemInfo(int infoId, float delaySecond = 0.5f)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		if (gComponent is UITutorialWindow win)
		{
			_systemTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
			UITutorial_Com_SystemInfo _com_SystemInfo = ShowSystemInfo(infoId, delaySecond, win);
			_com_SystemInfo.btn_Comfirm.title = 5.GetLocal(UIStringType.Tutorial);
			_com_SystemInfo.btn_Comfirm.onClick.Set((EventCallback0)delegate
			{
				_com_SystemInfo.btn_Comfirm.onClick.Retain();
				Hide();
				_systemTsc?.Cancel();
				_com_SystemInfo.btn_Comfirm.onClick.Release();
			});
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_systemTsc);
			win.RemoveChild(_com_SystemInfo, dispose: true);
		}
	}

	private static UITutorial_Com_SystemInfo ShowSystemInfo(int infoId, float delaySecond, UITutorialWindow win)
	{
		UITutorial_Com_Tutorial com_Tutorial = win.com_Tutorial;
		UITutorial_Com_Mash com_Mask = win.com_Mask;
		bool flag = (win.com_BG.visible = false);
		bool flag3 = (com_Mask.visible = flag);
		com_Tutorial.visible = flag3;
		UITutorial_Com_SystemInfo uITutorial_Com_SystemInfo = UITutorial_Com_SystemInfo.CreateInstance();
		win.AddChild(uITutorial_Com_SystemInfo);
		uITutorial_Com_SystemInfo.MakeFullScreen();
		uITutorial_Com_SystemInfo.AddRelation(win, FairyGUI.RelationType.Height);
		uITutorial_Com_SystemInfo.AddRelation(win, FairyGUI.RelationType.Width);
		uITutorial_Com_SystemInfo.ShowInfo(infoId, delaySecond).Forget();
		return uITutorial_Com_SystemInfo;
	}

	public async UniTask TryShowGuideChoose(int infoId)
	{
		await TryShowAsync();
		GComponent gComponent = base.contentPane;
		UITutorialWindow win = gComponent as UITutorialWindow;
		if (win != null)
		{
			UITutorial_Com_SystemInfo _com_SystemInfo = ShowSystemInfo(infoId, 0f, win);
			_com_SystemInfo.btn_Comfirm.title = 7.GetLocal(UIStringType.Tutorial);
			_com_SystemInfo.btn_Cancel.title = 6.GetLocal(UIStringType.Tutorial);
			_com_SystemInfo.btn_Comfirm.onClick.Set((EventCallback0)delegate
			{
				_com_SystemInfo.btn_Comfirm.onClick.Retain();
				win.RemoveChild(_com_SystemInfo, dispose: true);
				ShowStartGameMask().Forget();
				_com_SystemInfo.btn_Comfirm.onClick.Release();
			});
			_com_SystemInfo.btn_Cancel.visible = true;
			_com_SystemInfo.btn_Cancel.onClick.Set((EventCallback0)delegate
			{
				_com_SystemInfo.btn_Comfirm.onClick.Retain();
				win.RemoveChild(_com_SystemInfo, dispose: true);
				Hide();
				SimpleSingletonProvider<GameLogicManager>.inst.tutorial.FinishTutorial(21998);
				_com_SystemInfo.btn_Comfirm.onClick.Release();
			});
		}
	}

	public async UniTask ShowSkipGuide()
	{
		await TryShowAsync();
		if (base.contentPane is UITutorialWindow uITutorialWindow)
		{
			_skipTsc = SimpleSingletonProvider<DelaySignalManager>.inst.CreatCts();
			uITutorialWindow.com_BG.visible = true;
			uITutorialWindow.com_BG.showBg.selectedIndex = 1;
			string local = 101.GetLocal(UIStringType.Tutorial);
			string local2 = 102.GetLocal(UIStringType.Tutorial);
			string local3 = 103.GetLocal(UIStringType.Tutorial);
			string local4 = 100.GetLocal(UIStringType.Tutorial);
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancelWithTitle(local, local4, local2, local3, closeButtonStatus: false, delegate
			{
				SimpleSingletonProvider<DelaySignalManager>.inst.CancelTask(_skipTsc);
			}, delegate
			{
				SimpleSingletonProvider<WebServerManager>.inst.PostTutorialRecord(21997);
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(1001, 1);
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(1002, 1);
				SimpleSingletonProvider<GameLogicManager>.inst.account.UpdateGuidedData(10011, 1);
				SimpleSingletonProvider<DelaySignalManager>.inst.CancelTask(_skipTsc);
				Hide();
			}).Forget();
			await SimpleSingletonProvider<DelaySignalManager>.inst.WaitUntilCanceled(_skipTsc);
		}
	}
}
