using Tools;
using party.model;

namespace GameLogic;

public class GuildSignal
{
	public readonly Signal<PlayerGuildInfo> playerGuildUpdated = new Signal<PlayerGuildInfo>();

	public readonly Signal searchResultsChanged = new Signal();

	public readonly Signal guildCacheChanged = new Signal();

	public readonly Signal applicationsChanged = new Signal();

	public readonly Signal invitationsChanged = new Signal();

	public readonly Signal<GuildJoinedCause> joinedSuccess = new Signal<GuildJoinedCause>();

	public readonly Signal currentGuildChanged = new Signal();

	public readonly Signal guildMembersChanged = new Signal();

	public readonly Signal memberChangeMessagesChanged = new Signal();

	public readonly Signal<GuildManagementOperation> managementSucceeded = new Signal<GuildManagementOperation>();

	public readonly Signal<GuildLeaveCause> leftGuild = new Signal<GuildLeaveCause>();

	public readonly Signal guildTasksChanged = new Signal();

	public readonly Signal<int> guildMissionRewardSucceeded = new Signal<int>();

	public readonly Signal guildApplicationsChanged = new Signal();

	public readonly Signal<GuildInvitationApprovalOperation> invitationApprovalSucceeded = new Signal<GuildInvitationApprovalOperation>();
}
