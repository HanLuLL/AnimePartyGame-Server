using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class GuildPanel : BasePanel<UIGuildPanel>
{
	private const int HOME_PAGE = 0;

	private const int MEMBER_PAGE = 1;

	private const int TASK_PAGE = 2;

	private const int STORE_PAGE = 3;

	private const int INVITE_HIDDEN = 0;

	private const int INVITE_VISIBLE = 1;

	private const int WINDOW_HIDDEN = 0;

	private const int WINDOW_VISIBLE = 1;

	private bool _routingAfterLeave;

	private bool _leaveSuccessShown;

	public GuildPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIGuildPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
		_routingAfterLeave = false;
		_leaveSuccessShown = false;
		if (base.ui != null)
		{
			base.ui.page.selectedIndex = 0;
			SetInviteVisible(visible: false);
			SetWindowVisible(visible: false);
			InjectCallbacks();
			base.ui.com_MemberView?.OnHide();
			base.ui.com_TaskView?.OnHide();
			base.ui.com_StoreView?.OnHide();
			base.ui.com_InviteView?.OnHide();
		}
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		if (base.ui == null)
		{
			return;
		}
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild == null || !guild.IsJoined)
		{
			RouteAfterLeave().Forget();
			return;
		}
		base.ui.com_GuildHomeView.OnShow();
		if (!guild.HasCurrentGuildSnapshot)
		{
			guild.RequestCurrentGuild();
		}
	}

	protected override void InitComponents()
	{
		base.InitComponents();
		base.ui.com_GuildHomeView?.Init();
		base.ui.com_MemberView?.Init();
		base.ui.com_TaskView?.Init();
		base.ui.com_StoreView?.Init();
		base.ui.com_InviteView?.Init();
		base.ui.com_tips_win?.Init();
	}

	public override void Refresh()
	{
		base.Refresh();
		switch (base.ui?.page?.selectedIndex)
		{
		case 1:
			base.ui.com_MemberView?.OnShow();
			break;
		case 2:
			base.ui.com_TaskView?.OnShow();
			break;
		case 3:
			base.ui.com_StoreView?.OnShow();
			break;
		default:
			base.ui?.com_GuildHomeView?.RefreshView();
			break;
		}
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Back.onClick.Add(OnReturn);
		base.ui.com_GuildHomeView?.AddEvent();
		base.ui.com_MemberView?.AddEvent();
		base.ui.com_TaskView?.AddEvent();
		base.ui.com_StoreView?.AddEvent();
		base.ui.com_InviteView?.AddEvent();
		base.ui.com_tips_win?.AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Back.onClick.Remove(OnReturn);
		base.ui.com_GuildHomeView?.RemoveEvent();
		base.ui.com_MemberView?.RemoveEvent();
		base.ui.com_TaskView?.RemoveEvent();
		base.ui.com_StoreView?.RemoveEvent();
		base.ui.com_InviteView?.RemoveEvent();
		base.ui.com_tips_win?.RemoveEvent();
	}

	protected override void AddListener()
	{
		base.AddListener();
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		guild?.signal.leftGuild.AddListener(OnLeftGuild);
		guild?.signal.playerGuildUpdated.AddListener(OnPlayerGuildUpdated);
		guild?.signal.managementSucceeded.AddListener(OnManagementSucceeded);
		base.ui.com_GuildHomeView?.AddListener();
		base.ui.com_MemberView?.AddListener();
		base.ui.com_TaskView?.AddListener();
		base.ui.com_StoreView?.AddListener();
		base.ui.com_InviteView?.AddListener();
		base.ui.com_tips_win?.AddListener();
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		guild?.signal.leftGuild.RemoveListener(OnLeftGuild);
		guild?.signal.playerGuildUpdated.RemoveListener(OnPlayerGuildUpdated);
		guild?.signal.managementSucceeded.RemoveListener(OnManagementSucceeded);
		base.ui.com_GuildHomeView?.RemoveListener();
		base.ui.com_MemberView?.RemoveListener();
		base.ui.com_TaskView?.RemoveListener();
		base.ui.com_StoreView?.RemoveListener();
		base.ui.com_InviteView?.RemoveListener();
		base.ui.com_tips_win?.RemoveListener();
	}

	public override void Close()
	{
		if (base.ui != null)
		{
			HideCurrentPage(base.ui.page.selectedIndex);
			CloseInviteView();
			SetWindowVisible(visible: false);
			base.ui.com_GuildHomeView?.ClearData();
			base.ui.com_MemberView?.ClearData();
			base.ui.com_TaskView?.ClearData();
			base.ui.com_StoreView?.ClearData();
			base.ui.com_InviteView?.ClearData();
			base.ui.com_tips_win?.ClearData();
			base.Close();
		}
	}

	public override void Dispose()
	{
		base.Dispose();
	}

	private void InjectCallbacks()
	{
		base.ui.com_GuildHomeView.OnOpenMemberPage = OpenMemberPage;
		base.ui.com_GuildHomeView.OnOpenTaskPage = OpenTaskPage;
		base.ui.com_GuildHomeView.OnOpenStorePage = OpenStorePage;
		base.ui.com_GuildHomeView.OnOpenSettings = OpenSettingsWindow;
		base.ui.com_GuildHomeView.OnOpenInternalAnnouncement = OpenInternalAnnouncementWindow;
		base.ui.com_MemberView.OnOpenMemberSettings = OpenMemberSettingsWindow;
		base.ui.com_MemberView.OnOpenMemberChanges = OpenMemberChanges;
		base.ui.com_MemberView.OnOpenInvite = OpenInviteView;
		base.ui.com_MemberView.OnOpenApproval = OpenApprovalWindow;
		base.ui.com_InviteView.OnRequestClose = CloseInviteView;
		base.ui.com_tips_win.OnRequestClose = CloseWindow;
	}

	private void OpenMemberPage()
	{
		SwitchPage(1);
	}

	private void OpenTaskPage()
	{
		SwitchPage(2);
	}

	private void OpenStorePage()
	{
		SwitchPage(3);
	}

	private void SwitchPage(int targetPage)
	{
		if (base.ui != null && base.ui.page.selectedIndex != targetPage)
		{
			HideCurrentPage(base.ui.page.selectedIndex);
			base.ui.page.selectedIndex = targetPage;
			ShowCurrentPage(targetPage);
		}
	}

	private void HideCurrentPage(int pageIndex)
	{
		switch (pageIndex)
		{
		case 0:
			base.ui.com_GuildHomeView?.OnHide();
			break;
		case 1:
			base.ui.com_MemberView?.OnHide();
			break;
		case 2:
			base.ui.com_TaskView?.OnHide();
			break;
		case 3:
			base.ui.com_StoreView?.OnHide();
			break;
		}
	}

	private void ShowCurrentPage(int pageIndex)
	{
		switch (pageIndex)
		{
		case 0:
			base.ui.com_GuildHomeView?.OnShow();
			break;
		case 1:
			base.ui.com_MemberView?.OnShow();
			break;
		case 2:
			base.ui.com_TaskView?.OnShow();
			break;
		case 3:
			base.ui.com_StoreView?.OnShow();
			break;
		}
	}

	private void OpenInviteView()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.CanInvite)
		{
			UIGuildPanel uIGuildPanel = base.ui;
			if (uIGuildPanel != null && uIGuildPanel.page?.selectedIndex == 1)
			{
				base.ui.com_InviteView?.OnShow();
				SetInviteVisible(visible: true);
			}
		}
	}

	private void CloseInviteView()
	{
		if (base.ui != null && base.ui.isInvite.selectedIndex == 1)
		{
			base.ui.com_InviteView?.OnHide();
			SetInviteVisible(visible: false);
		}
	}

	private void SetInviteVisible(bool visible)
	{
		if (base.ui?.isInvite != null)
		{
			base.ui.isInvite.selectedIndex = (visible ? 1 : 0);
		}
	}

	private void OpenApprovalWindow()
	{
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null && guild.CanApproveApplications)
		{
			OpenWindow(4, 0L);
		}
	}

	private void OpenSettingsWindow()
	{
		OpenWindow(0, 0L);
	}

	private void OpenInternalAnnouncementWindow()
	{
		OpenWindow(1, 0L);
	}

	private void OpenMemberSettingsWindow(long playerId)
	{
		OpenWindow(2, playerId);
	}

	private void OpenMemberChanges()
	{
		OpenWindow(3, 0L);
	}

	private void OpenWindow(int stateIndex, long selectedPlayerId = 0L)
	{
		if (base.ui != null)
		{
			SetWindowVisible(visible: true);
			base.ui.com_tips_win?.Open(stateIndex, selectedPlayerId);
		}
	}

	private void CloseWindow()
	{
		if (base.ui != null)
		{
			base.ui.com_tips_win?.ClearData();
			SetWindowVisible(visible: false);
			InjectCallbacks();
		}
	}

	private void SetWindowVisible(bool visible)
	{
		if (base.ui != null)
		{
			base.ui.winType.selectedIndex = (visible ? 1 : 0);
			base.ui.mohu.visible = visible;
			base.ui.mohu.touchable = visible;
		}
	}

	private async void OnReturn()
	{
		if (base.ui != null)
		{
			if (base.ui.winType.selectedIndex == 1)
			{
				CloseWindow();
			}
			else if (base.ui.isInvite.selectedIndex == 1)
			{
				CloseInviteView();
			}
			else if (base.ui.page.selectedIndex != 0)
			{
				SwitchPage(0);
			}
			else
			{
				await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
			}
		}
	}

	private void OnPlayerGuildUpdated(PlayerGuildInfo playerGuild)
	{
		if (playerGuild != null && playerGuild.GuildId == 0L)
		{
			RouteAfterLeave().Forget();
		}
	}

	private void OnManagementSucceeded(GuildManagementOperation operation)
	{
		switch (operation)
		{
		case GuildManagementOperation.EXIT_GUILD:
			ShowLeaveSuccess(GuildLeaveCause.EXIT);
			break;
		case GuildManagementOperation.DISBAND_GUILD:
			ShowLeaveSuccess(GuildLeaveCause.DISBAND);
			break;
		}
	}

	private void OnLeftGuild(GuildLeaveCause cause)
	{
		ShowLeaveSuccess(cause);
		RouteAfterLeave().Forget();
	}

	private void ShowLeaveSuccess(GuildLeaveCause cause)
	{
		if (_leaveSuccessShown)
		{
			return;
		}
		int num = cause switch
		{
			GuildLeaveCause.EXIT => 3131, 
			GuildLeaveCause.DISBAND => 3133, 
			_ => 0, 
		};
		if (num != 0)
		{
			string text = GuildText.Get(num);
			if (!string.IsNullOrEmpty(text))
			{
				SimpleSingletonProvider<UIManager>.inst.systemTips.ShowTips(text);
			}
			_leaveSuccessShown = true;
		}
	}

	private async UniTask RouteAfterLeave()
	{
		if (!_routingAfterLeave && IsOpen())
		{
			_routingAfterLeave = true;
			CloseInviteView();
			CloseWindow();
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.GuildDiscovery);
		}
	}
}
