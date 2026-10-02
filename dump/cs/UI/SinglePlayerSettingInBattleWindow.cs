using Core;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using SinglePlayer;
using Tools;

namespace UI;

public class SinglePlayerSettingInBattleWindow : BaseWindow
{
	private BlurBgTextureController blurBgCtrl = new BlurBgTextureController();

	protected override bool isGeneralFadeIn => true;

	protected override bool isGeneralFadeOut => true;

	public SinglePlayerSettingInBattleWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UISinglePlayerSettingInBattleWindow.CreateInstance();
		base.OnInit();
		if (base.contentPane is UISinglePlayerSettingInBattleWindow uISinglePlayerSettingInBattleWindow)
		{
			GObject child = uISinglePlayerSettingInBattleWindow.GetChild("n60");
			if (child != null)
			{
				child.alpha = 0f;
			}
		}
	}

	public async UniTask TryShowAsync()
	{
		if (!base.isShowing)
		{
			await blurBgCtrl.CreateBlurTex();
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
		if (base.contentPane is UISinglePlayerSettingInBattleWindow uISinglePlayerSettingInBattleWindow)
		{
			uISinglePlayerSettingInBattleWindow.btn_Continue.onClick.Add(base.Hide);
			uISinglePlayerSettingInBattleWindow.btn_OpenSetting.onClick.Add(OpenSettingMenu);
			uISinglePlayerSettingInBattleWindow.btn_QuitGame.onClick.Add(QuitGame);
			uISinglePlayerSettingInBattleWindow.btn_LeaveRoom.onClick.Add(LeaveRoom);
			uISinglePlayerSettingInBattleWindow.btn_Reset.onClick.Add(ResetSinglePlayer);
			uISinglePlayerSettingInBattleWindow.Cut_In.Play();
			blurBgCtrl.OnShown(this);
		}
	}

	protected override void OnHide()
	{
		base.OnHide();
		if (base.contentPane is UISinglePlayerSettingInBattleWindow uISinglePlayerSettingInBattleWindow)
		{
			uISinglePlayerSettingInBattleWindow.btn_Continue.onClick.Remove(base.Hide);
			uISinglePlayerSettingInBattleWindow.btn_OpenSetting.onClick.Remove(OpenSettingMenu);
			uISinglePlayerSettingInBattleWindow.btn_QuitGame.onClick.Remove(QuitGame);
			uISinglePlayerSettingInBattleWindow.btn_LeaveRoom.onClick.Remove(LeaveRoom);
			uISinglePlayerSettingInBattleWindow.btn_Reset.onClick.Remove(ResetSinglePlayer);
			blurBgCtrl.OnHide();
		}
	}

	private async void OpenSettingMenu()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UISinglePlayerSettingInBattleWindow win)
		{
			win.btn_OpenSetting.onClick.Retain();
			Hide();
			await SimpleSingletonProvider<UIManager>.inst.setting.ShowSetting();
			win.btn_OpenSetting.onClick.Release();
		}
	}

	private async void QuitGame()
	{
		GComponent gComponent = base.contentPane;
		if (gComponent is UISinglePlayerSettingInBattleWindow win)
		{
			win.btn_QuitGame.onClick.Retain();
			await SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(1014, delegate
			{
				SimpleSingletonProvider<GameManager>.inst.CloseGame();
			});
			win.btn_QuitGame.onClick.Release();
		}
	}

	private void LeaveRoom()
	{
		GComponent gComponent = base.contentPane;
		UISinglePlayerSettingInBattleWindow win = gComponent as UISinglePlayerSettingInBattleWindow;
		if (win != null)
		{
			win.btn_LeaveRoom.onClick.Retain();
			string local = 1040004.GetLocal(UIStringType.GUI);
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(local, delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.BackSinglePlayerStartPanel(clearData: false);
				win.btn_LeaveRoom.onClick.Release();
			}, delegate
			{
				win.btn_LeaveRoom.onClick.Release();
			}).Forget();
		}
	}

	private void ResetSinglePlayer()
	{
		GComponent gComponent = base.contentPane;
		UISinglePlayerSettingInBattleWindow win = gComponent as UISinglePlayerSettingInBattleWindow;
		if (win != null)
		{
			win.btn_Reset.onClick.Retain();
			string local = 1040003.GetLocal(UIStringType.GUI);
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOKCancel(local, delegate
			{
				SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.PlayerSelectInfoOnGameRestart.SetRestartGameInfo(isNextLevel: false);
				SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.ResetSinglePlayer();
				SimpleSingletonProvider<GameLogicManager>.inst.singlePlayer.UploadLogData(SinglePlayerLogType.Restart);
				win.btn_Reset.onClick.Release();
			}, delegate
			{
				win.btn_Reset.onClick.Release();
			}).Forget();
		}
	}
}
