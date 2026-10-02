using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;

namespace UI;

public class AssistVoteBaseWindow : BaseWindow
{
	protected long ActionSn;

	protected readonly List<PlayerVoteData> LeftVoteData = new List<PlayerVoteData>();

	protected readonly List<PlayerVoteData> RightVoteData = new List<PlayerVoteData>();

	protected const int PKTime = 1500;

	protected const int DiceTime = 2000;

	protected const int PointTime = 1000;

	protected const int ResultTime = 2000;

	protected AssistVoteData AssistVote => SimpleSingletonProvider<GameLogicManager>.inst.assistVote.AssistVote;

	public AssistVoteBaseWindow(UIWindowType type)
		: base(type)
	{
	}

	protected async UniTask TryShowAsync()
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

	public virtual UniTask TryShowAssistVote(Action action)
	{
		return UniTask.CompletedTask;
	}

	public virtual UniTask RefreshVoteData(bool showPoint)
	{
		return UniTask.CompletedTask;
	}

	public virtual async UniTask TryShowAssistVoteAfterReconnect(bool result)
	{
		await TryShowAsync();
		InitAssistVoteWin();
		await RefreshVoteData(result);
	}

	protected virtual void InitAssistVoteWin()
	{
	}

	public virtual void ResetUI(long playerId, int selectId)
	{
	}

	public virtual UniTask VoteOver()
	{
		Hide();
		SimpleSingletonProvider<UIManager>.inst.UnLoadAssistVote();
		return UniTask.CompletedTask;
	}
}
