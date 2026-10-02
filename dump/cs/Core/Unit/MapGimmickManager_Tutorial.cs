using Core.Tutorial;
using Core.Tutorial.SceneConfig;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Unit;

public class MapGimmickManager_Tutorial : MapGimmickManager
{
	public override void Initialize()
	{
	}

	public override void RefreshGimmickData(int groupId, int statusId)
	{
	}

	public override UniTask SwitchGimmick(int groupId, int statusId, bool wait = true)
	{
		return UniTask.CompletedTask;
	}

	protected override void OnDestroy()
	{
		TutorialGame.Dispose();
		ReflectionHelper.Clear();
	}

	public virtual async UniTask InitScene(TutorialSceneConfig config)
	{
		await InitGameLogic();
	}

	private async UniTask InitGameLogic()
	{
		await TutorialGame.RegisterModel<TutorialGlobalSignal>();
		await TutorialGame.RegisterSystem<GamePlayManager>();
		await TutorialGame.RegisterSystem<TutorialPlayerActionFSM>();
		await TutorialGame.RegisterSystem<TutorialBoardManager>();
	}

	public virtual UniTask StartRound(long playerId)
	{
		return UniTask.CompletedTask;
	}

	public virtual UniTask StopRound(long playerId)
	{
		return UniTask.CompletedTask;
	}

	public virtual UniTask DealChooseDir(long playerId)
	{
		return UniTask.CompletedTask;
	}

	public virtual UniTask TutorialEnd()
	{
		return UniTask.CompletedTask;
	}

	public virtual int GetPKAttackPoint(long attackerId, long defenderId)
	{
		return Random.Range(1, 7);
	}

	public virtual int GetPKDefendPoint(long attackerId, long defenderId)
	{
		return Random.Range(1, 7);
	}
}
