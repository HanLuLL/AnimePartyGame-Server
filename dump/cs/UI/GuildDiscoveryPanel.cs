using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class GuildDiscoveryPanel : BasePanel<UIGuildDiscoveryPanel>
{
	public GuildDiscoveryPanel(UIPanelConfigure config)
		: base(config)
	{
	}

	protected override void Create()
	{
		base.ui = UIGuildDiscoveryPanel.CreateInstance();
		base.Create();
	}

	protected override void InitData(params object[] objs)
	{
	}

	public override void Show(params object[] objs)
	{
		base.Show(objs);
		SetLoadingMask(visible: false);
		base.ui.com_GuildJoinView.OnLoadingMask = SetLoadingMask;
		base.ui.com_GuildJoinView.Init();
		base.ui.com_ApplicationView.Init();
		base.ui.com_InviteView.OnLoadingMask = SetLoadingMask;
		base.ui.com_InviteView.Init();
		base.ui.com_CreateView.OnLoadingMask = SetLoadingMask;
		base.ui.com_CreateView.Init();
		if (base.ui.page.selectedIndex == 0)
		{
			OnTabChanged();
		}
		else
		{
			base.ui.page.selectedIndex = 0;
		}
		RefreshAllRedPoints();
	}

	protected override void AddEvent()
	{
		base.AddEvent();
		base.ui.btn_Back.onClick.Add(OnReturnPanel);
		base.ui.page.onChanged.Add(OnTabChanged);
		base.ui.com_GuildJoinView.AddEvent();
		base.ui.com_ApplicationView.AddEvent();
		base.ui.com_InviteView.AddEvent();
		base.ui.com_CreateView.AddEvent();
	}

	protected override void RemoveEvent()
	{
		base.RemoveEvent();
		base.ui.btn_Back.onClick.Remove(OnReturnPanel);
		base.ui.page.onChanged.Remove(OnTabChanged);
		base.ui.com_GuildJoinView.RemoveEvent();
		base.ui.com_ApplicationView.RemoveEvent();
		base.ui.com_InviteView.RemoveEvent();
		base.ui.com_CreateView.RemoveEvent();
	}

	protected override void AddListener()
	{
		base.AddListener();
		base.ui.com_GuildJoinView.AddListener();
		base.ui.com_ApplicationView.AddListener();
		base.ui.com_InviteView.AddListener();
		base.ui.com_CreateView.AddListener();
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			guild.signal.playerGuildUpdated.AddListener(OnPlayerGuildUpdated);
			guild.signal.applicationsChanged.AddListener(OnApplicationsChanged);
			guild.signal.invitationsChanged.AddListener(OnInvitationsChanged);
			guild.signal.joinedSuccess.AddListener(OnJoinedSuccess);
		}
	}

	protected override void RemoveListener()
	{
		base.RemoveListener();
		base.ui.com_GuildJoinView.RemoveListener();
		base.ui.com_ApplicationView.RemoveListener();
		base.ui.com_InviteView.RemoveListener();
		base.ui.com_CreateView.RemoveListener();
		GuildLogic guild = SimpleSingletonProvider<GameLogicManager>.inst.guild;
		if (guild != null)
		{
			guild.signal.playerGuildUpdated.RemoveListener(OnPlayerGuildUpdated);
			guild.signal.applicationsChanged.RemoveListener(OnApplicationsChanged);
			guild.signal.invitationsChanged.RemoveListener(OnInvitationsChanged);
			guild.signal.joinedSuccess.RemoveListener(OnJoinedSuccess);
		}
	}

	public override void Close()
	{
		if (base.ui != null)
		{
			SetLoadingMask(visible: false);
			base.ui.com_GuildJoinView?.ClearData();
			base.ui?.com_ApplicationView?.ClearData();
			base.ui?.com_InviteView?.ClearData();
			base.ui?.com_CreateView?.ClearData();
			base.Close();
		}
	}

	private bool ComputeSideNavRedPoint(int tabIndex)
	{
		if (tabIndex == 2)
		{
			return SimpleSingletonProvider<GameLogicManager>.inst.guild?.HasValidInvitation ?? false;
		}
		return false;
	}

	private void OnTabChanged()
	{
		switch (base.ui.page.selectedIndex)
		{
		case 0:
			base.ui.com_GuildJoinView.OnShow();
			break;
		case 1:
			base.ui.com_ApplicationView.OnShow();
			break;
		case 2:
			base.ui.com_InviteView.OnShow();
			break;
		case 3:
			base.ui.com_CreateView.OnShow();
			break;
		}
	}

	public void SetLoadingMask(bool visible)
	{
		if (base.ui?.mohu != null)
		{
			base.ui.mohu.visible = visible;
			base.ui.mohu.touchable = visible;
		}
	}

	private async void OnReturnPanel()
	{
		base.ui.btn_Back.onClick.Retain();
		await SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
		base.ui.btn_Back.onClick.Release();
	}

	private void OnPlayerGuildUpdated(PlayerGuildInfo _)
	{
		RefreshAllRedPoints();
	}

	private void OnApplicationsChanged()
	{
		RefreshAllRedPoints();
	}

	private void OnInvitationsChanged()
	{
		RefreshAllRedPoints();
	}

	private void OnJoinedSuccess(GuildJoinedCause cause)
	{
		if (base.ui != null)
		{
			OpenJoinedGuildPanel();
		}
	}

	private void OpenJoinedGuildPanel()
	{
		SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Guild).Forget();
		SimpleSingletonProvider<GameLogicManager>.inst.guild?.ResetSearch();
		SimpleSingletonProvider<UIManager>.inst.ReturnToLastPanel(this);
	}

	public void RefreshAllRedPoints()
	{
		if (base.ui != null)
		{
			base.ui.com_GuildJoinView?.RefreshRedPoints();
			base.ui.com_ApplicationView?.RefreshRedPoints();
			base.ui.com_InviteView?.RefreshRedPoints();
			base.ui.com_CreateView?.RefreshRedPoints();
			RefreshSideNavRedPoints();
		}
	}

	private void RefreshSideNavRedPoints()
	{
		if (base.ui.list_GuildSideNavItems == null)
		{
			return;
		}
		for (int i = 0; i < base.ui.list_GuildSideNavItems.numItems; i++)
		{
			if (base.ui.list_GuildSideNavItems.GetChildAt(i) is UIGuildDiscovery_Button_SideNavItem uIGuildDiscovery_Button_SideNavItem)
			{
				uIGuildDiscovery_Button_SideNavItem.redStatus.selectedIndex = (ComputeSideNavRedPoint(i) ? 1 : 0);
			}
		}
	}
}
