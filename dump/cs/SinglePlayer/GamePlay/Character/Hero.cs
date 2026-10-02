using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SinglePlayer.AssetsHelper;
using SinglePlayer.GamePlay.BuffSystem;
using SinglePlayer.GamePlay.Build;
using SinglePlayer.GamePlay.Card;
using SinglePlayer.GamePlay.Map;
using UnityEngine;

namespace SinglePlayer.GamePlay.Character;

public class Hero : CharacterLogic
{
	public HeroView view;

	private CharacterMove move;

	private bool isForceStopOnDoMove;

	public List<BuffBase> Buffs { get; private set; }

	public List<BuildingBase> Buildings { get; private set; }

	public List<SinglePlayer.GamePlay.Card.Card> Cards { get; private set; }

	public Hero(HeroProperty property)
		: base(property)
	{
		property.SetCharacter(this);
	}

	public void Dispose()
	{
		if (view != null)
		{
			Object.Destroy(view.gameObject);
		}
	}

	public async UniTask CreateInstance()
	{
		GameObject gameObject = await Game.GetSystem<SinglePlayerAssetsHelper>().characterAssetManager.LoadHero(_property.Id);
		view = gameObject.AddComponent<HeroView>();
		view.Initialize(this);
		move = gameObject.AddComponent<CharacterMove>();
		move.Initialize(this);
		Game.GetModel<GlobalSignal>().HeroCreated.Dispatch(this);
	}

	public override UniTask DoHit(int value)
	{
		return UniTask.CompletedTask;
	}

	public override UniTask DoDamage(int value)
	{
		return UniTask.CompletedTask;
	}

	public override UniTask DoDefend(int value)
	{
		return UniTask.CompletedTask;
	}

	public override UniTask DoDodge(int value)
	{
		return UniTask.CompletedTask;
	}

	public override UniTask DoDead()
	{
		return UniTask.CompletedTask;
	}

	public void ForceStopOnDoMove()
	{
		isForceStopOnDoMove = true;
	}

	public async UniTask DoMove(Queue<Land> moveQueue)
	{
		HeroProperty heroProperty = (HeroProperty)_property;
		int preLandId = heroProperty.StandLandId;
		view.transform.parent = null;
		await view.PlayWalkStart();
		isForceStopOnDoMove = false;
		while (moveQueue.Count > 0)
		{
			Land nextSlotComponent = moveQueue.Dequeue();
			float moveTime = GetMoveTime(move.transform.position, nextSlotComponent.transform.position);
			view.PlayWalkJump(walk: true, moveTime);
			Game.GetModel<GameData>().MapData.GetLandById(heroProperty.StandLandId).PlayStepOnEffect().Forget();
			if (!(await move.Move(nextSlotComponent, moveTime)))
			{
				return;
			}
			preLandId = heroProperty.StandLandId;
			heroProperty.ChangeStandLandId(nextSlotComponent.Id);
			Game.GetModel<GlobalSignal>().PassLand.Dispatch(nextSlotComponent.Id, nextSlotComponent.BuildingFoundationId);
			if (isForceStopOnDoMove)
			{
				moveQueue.Clear();
			}
			Game.GetSystem<BoardManager>().buildingManager.DoEffect();
			Game.GetSystem<BoardManager>().characterManager.PlayAttributeShow().Forget();
		}
		view.PlayWalkStop();
		Land landById = Game.GetModel<GameData>().MapData.GetLandById(heroProperty.StandLandId);
		if (landById != null)
		{
			view.transform.parent = landById.LandGRoot;
			await landById.PlayStepOnEffect(wait: true);
			if (view != null)
			{
				view.transform.parent = null;
			}
		}
		List<int> nextLandIds = Game.GetModel<GameData>().MapData.GetNextLandIds(heroProperty.StandLandId, preLandId);
		heroProperty.ChangeNextLandIds(nextLandIds);
	}

	private float GetMoveTime(Vector3 curPos, Vector3 nextPos)
	{
		curPos.y = 0f;
		nextPos.y = 0f;
		return Vector3.Distance(curPos, nextPos) / move.MoveSpeed;
	}

	protected virtual void OnShowComplete(Dictionary<(AttributeChangeTarget type, int id), AttributeChangeInfoStatistics> data)
	{
		if (data.TryGetValue((AttributeChangeTarget.Character, 1), out var value) && value.ChangeGold != 0)
		{
			base.GoldChange.Dispatch(value.ChangeGold);
		}
	}
}
