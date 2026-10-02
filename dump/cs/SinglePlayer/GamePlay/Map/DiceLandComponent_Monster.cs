using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Character;
using UnityEngine;

namespace SinglePlayer.GamePlay.Map;

public class DiceLandComponent_Monster : DiceLandComponent
{
	public DiceLandComponent_Monster(int landId)
		: base(landId)
	{
		effectType = DiceLandEffectType.StepOnEffect;
	}

	public override UniTask PassByEffect()
	{
		Debug.Log("经过地图格：Building");
		return UniTask.CompletedTask;
	}

	public override async UniTask StepOnEffect()
	{
		Hero hero = Game.GetSystem<BoardManager>().characterManager.Hero;
		Monster monster = Game.GetSystem<BoardManager>().characterManager.Monster;
		if (monster != null && !monster.IsDead() && !hero.IsDead() && (!(monster.Property is MonsterProperty monsterProperty) || monsterProperty.MonsterInfo.CanAttack))
		{
			int changeHP = -Mathf.Max(monster.Property.ATK - hero.Property.DEF, 0);
			await Game.GetSystem<BoardManager>().characterManager.PlayChangeCharacterAttribute(new AttributeChangeInfo
			{
				Source = (type: AttributeChangeSource.DiceLand, id: LandId),
				ChangeHP = changeHP
			});
		}
	}

	public override async UniTask ShowAttributeChange(AttributeChangeInfo message)
	{
		Monster monster = Game.GetSystem<BoardManager>().characterManager.Monster;
		if (monster.Property is MonsterProperty monsterProperty)
		{
			await monster.view.Play(monsterProperty.MonsterInfo.MonsterAttackTimeline);
		}
	}
}
