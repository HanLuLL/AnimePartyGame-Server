using Core.Audio;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class LandHospitalWindow : BaseWindow
{
	private BattlePlayerData playerData;

	private long _hospitalSn;

	public LandHospitalWindow(UIWindowType type)
		: base(type)
	{
	}

	protected override void OnInit()
	{
		base.contentPane = UILandHospitalWindow.CreateInstance();
		base.OnInit();
	}

	public async UniTask<LandHospitalWindow> ShowLand()
	{
		Show();
		if (!base.initialized)
		{
			await UniTask.WaitUntil(() => base.initialized);
		}
		return this;
	}

	protected override void OnShown()
	{
		base.OnShown();
		_ = base.contentPane is UILandHospitalWindow;
	}

	protected override void OnHide()
	{
		base.OnHide();
		_ = base.contentPane is UILandHospitalWindow;
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	public async void DealLand_TriggerHospital(Action _action)
	{
		playerData = SimpleSingletonProvider<GameLogicManager>.inst.battle.GetPlayerDataById(_action.PlayerId);
		if (SimpleSingletonProvider<GameLogicManager>.inst.account.IsSelf(_action.PlayerId))
		{
			if (playerData.CharacterInst != null)
			{
				await playerData.CharacterInst.SwitchCamera();
			}
			await ShowLand();
			InitHospital(_action.Sn);
		}
		else
		{
			SimpleSingletonProvider<UIManager>.inst.tips.ShowThinkingTip(_action.PlayerId, 11010);
		}
	}

	private void InitHospital(long sn)
	{
		_hospitalSn = sn;
		GComponent gComponent = base.contentPane;
		UILandHospitalWindow win = gComponent as UILandHospitalWindow;
		if (win != null)
		{
			win.com_Content.btn_noSick.onClick.Set(OnClickNoSickButton);
			win.com_Content.btn_check.onClick.Set(OnClickCheckButton);
			win.com_Content.viewType.selectedIndex = 0;
			OperationTimer.ActionDownTime(_hospitalSn, 5093, delegate
			{
				win.com_Content.btn_check.onClick.Call();
			});
		}
	}

	private void OnClickNoSickButton()
	{
		if (base.contentPane is UILandHospitalWindow uILandHospitalWindow)
		{
			uILandHospitalWindow.com_Content.viewType.selectedIndex = 1;
		}
	}

	private void OnClickCheckButton()
	{
		GComponent gComponent = base.contentPane;
		UILandHospitalWindow win = gComponent as UILandHospitalWindow;
		if (win != null)
		{
			win.com_Content.btn_check.onClick.Retain();
			SimpleSingletonProvider<GameLogicManager>.inst.land.RequestTriggerHospitalC2S(_hospitalSn).OnFinishedOnly.AddOnce(delegate
			{
				win.com_Content.btn_check.onClick.Release();
			});
		}
	}

	public void SwitchCheckingView(bool isInHospital)
	{
		GComponent gComponent = base.contentPane;
		UILandHospitalWindow win = gComponent as UILandHospitalWindow;
		if (win == null)
		{
			return;
		}
		win.com_Content.viewType.selectedIndex = 2;
		win.com_Content.checking.Play(delegate
		{
			if (isInHospital)
			{
				win.com_Content.viewType.selectedIndex = 3;
				win.com_Content.checkIn.Play();
				BGMHelper.TryBattleRoleVoice(605);
			}
			else
			{
				win.com_Content.viewType.selectedIndex = 4;
				win.com_Content.checkOut.Play();
				BGMHelper.TryBattleRoleVoice(604);
			}
		});
	}
}
