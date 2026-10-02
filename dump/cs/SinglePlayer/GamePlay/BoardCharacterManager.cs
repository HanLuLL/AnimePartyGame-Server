using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Character;
using SinglePlayer.GamePlay.Map;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class BoardCharacterManager
{
	private MapData _mapData;

	private readonly AttributeMessageQueueAsync _attributeMessageQueue = new AttributeMessageQueueAsync();

	public Hero Hero { get; private set; }

	public Monster Monster { get; private set; }

	public bool IsProcessing => _attributeMessageQueue.IsProcessing;

	public void Initialize()
	{
		_mapData = Game.GetModel<GameData>().MapData;
	}

	public void Dispose()
	{
		Hero.Dispose();
		DisposeMonster();
	}

	public void BuildBattleFieldData()
	{
		HeroProperty heroProperty = Game.GetModel<GameData>().heroProperty;
		Hero = new Hero(heroProperty);
		MonsterProperty monsterProperty = Game.GetModel<GameData>().MonsterProperty;
		if (monsterProperty != null)
		{
			Monster = new Monster(monsterProperty);
		}
	}

	public async UniTask BuildBattleFieldGameObject()
	{
		await Hero.CreateInstance();
		if (Monster != null)
		{
			Game.GetModel<GlobalSignal>().MonsterCreated.Dispatch(Monster);
		}
	}

	public async UniTask CreateMonster(int monsterId, int missionId)
	{
		await UniTask.CompletedTask;
		Game.GetSystem<BoardManager>().characterManager.DisposeMonster();
		MonsterProperty monsterProperty = Game.GetModel<GameData>().CreateMonsterProperty(monsterId, missionId);
		if (monsterProperty != null)
		{
			string text = $"SinglePlayer.GamePlay.Character.Monster_{monsterId}";
			Type type = Type.GetType(text);
			if (type == null)
			{
				Debug.LogError("找不到类型 " + text);
				return;
			}
			Monster = (Monster)Activator.CreateInstance(type, monsterProperty);
			Game.GetModel<GlobalSignal>().MonsterCreated.Dispatch(Monster);
		}
		else
		{
			Debug.LogError($"无法通过怪物Id:{monsterId} 获取对应的怪物属性");
		}
	}

	public bool CanMove()
	{
		HeroProperty heroProperty = (HeroProperty)Hero.Property;
		if (heroProperty.MovePoint <= 0)
		{
			return false;
		}
		List<int> nextLandIds = heroProperty.NextLandIds;
		if (nextLandIds == null)
		{
			return false;
		}
		return nextLandIds.Count == 1;
	}

	public Queue<Land> GenerateMovePath()
	{
		if (!CanMove())
		{
			return null;
		}
		Queue<Land> queue = new Queue<Land>();
		HeroProperty heroProperty = (HeroProperty)Hero.Property;
		int preLandId = heroProperty.StandLandId;
		List<int> nextLandIds = heroProperty.NextLandIds;
		int movePoint = heroProperty.MovePoint;
		for (int i = 0; i < movePoint; i++)
		{
			if (nextLandIds == null || nextLandIds.Count == 0)
			{
				Debug.Log($"[GenerateMovePath] Step {i}: 没有可走的下一个节点，停止。");
				break;
			}
			if (nextLandIds.Count != 1)
			{
				Debug.Log($"[GenerateMovePath] Step {i}: 遇到分支 ({nextLandIds.Count})，停止自动前进。");
				break;
			}
			Land landById = _mapData.GetLandById(nextLandIds[0]);
			if (landById == null)
			{
				Debug.LogWarning($"[GenerateMovePath] Step {i}: 无法找到地块 {nextLandIds[0]}");
				break;
			}
			queue.Enqueue(landById);
			nextLandIds = landById.GetNextLandIds(preLandId);
			preLandId = landById.Id;
			DiceLandComponent landComponent = landById.landComponent;
			if (landComponent != null && landComponent.effectType == DiceLandEffectType.StopMoveAndStepOnEffect)
			{
				heroProperty.MovePoint = 0;
				return queue;
			}
		}
		heroProperty.MovePoint -= queue.Count;
		return queue;
	}

	public async UniTask ChangeHeroGold(int changeGold)
	{
		Hero.Property.ChangeGold(changeGold);
		await UniTask.CompletedTask;
	}

	public async UniTask Upgrade()
	{
		if (CanUpgrade())
		{
			int heroStar = Hero.Property.Star + 1;
			SinglePlayerUpgradeConfigureItem heroUpgradeInfo = Game.GetModel<GameData>().GetHeroUpgradeInfo(heroStar);
			await Game.GetSystem<BoardManager>().characterManager.ChangeHeroGold(-heroUpgradeInfo.Goldcost);
			await PlayChangeCharacterAttribute(new AttributeChangeInfo
			{
				Source = (type: AttributeChangeSource.UpgradeStar, id: 0),
				ChangeStar = 1
			});
		}
	}

	public bool CanUpgrade()
	{
		if (MaxStar())
		{
			return false;
		}
		int num = Hero.Property.Star + 1;
		SinglePlayerUpgradeConfigureItem heroUpgradeInfo = Game.GetModel<GameData>().GetHeroUpgradeInfo(num);
		if (heroUpgradeInfo == null)
		{
			Debug.LogError($"无法获取等级为:{num}的配置！");
			return false;
		}
		if (Hero.Property.Gold.Value < heroUpgradeInfo.Goldcost)
		{
			return false;
		}
		return true;
	}

	public int GetUpgradeCost()
	{
		if (MaxStar())
		{
			return 0;
		}
		int num = Hero.Property.Star + 1;
		SinglePlayerUpgradeConfigureItem heroUpgradeInfo = Game.GetModel<GameData>().GetHeroUpgradeInfo(num);
		if (heroUpgradeInfo == null)
		{
			Debug.LogError($"无法获取等级为:{num}的配置！");
			return 0;
		}
		return heroUpgradeInfo.Goldcost;
	}

	public bool MaxStar()
	{
		return Hero.Property.Star >= Game.GetModel<GameData>().GetUpgradeInfoGroup().SinglePlayerUpgradeConfigureItems.Count - 1;
	}

	public void ChangeCharacterAttribute(AttributeChangeInfo attrInfo)
	{
		attrInfo.TryChangeCharacterProperty();
		_attributeMessageQueue.Enqueue(attrInfo);
	}

	public async UniTask PlayChangeCharacterAttribute(AttributeChangeInfo attrInfo)
	{
		ChangeCharacterAttribute(attrInfo);
		await PlayAttributeShow();
	}

	public async UniTask PlayAttributeShow()
	{
		await _attributeMessageQueue.Start();
		Hero.UpdateViewProperty();
		if (Monster != null)
		{
			Monster.UpdateViewProperty();
		}
	}

	public void DisposeMonster()
	{
		if (Monster != null)
		{
			Game.GetModel<GlobalSignal>().MonsterDisposed.Dispatch(Monster);
			Monster.Dispose();
		}
	}
}
