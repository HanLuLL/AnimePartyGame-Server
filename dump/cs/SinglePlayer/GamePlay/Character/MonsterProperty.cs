using UnityEngine;

namespace SinglePlayer.GamePlay.Character;

public class MonsterProperty : UnitProperty
{
	public SinglePlayerMonsterConfigure MonsterInfo;

	public int MissionId { get; private set; }

	public MonsterProperty(int monsterId, int missionId)
	{
		base.CharacterType = CharacterType.Monster;
		Id = monsterId;
		if (!StaticConfigure.SinglePlayer.MonsterDict.TryGetValue(Id, out MonsterInfo))
		{
			Debug.LogError($"通过配置ID:{Id} 无法在SinglePlayerBase.Monster中获取对应的配置");
			return;
		}
		base.Gold.Value = 1000;
		base.MaxHP = MonsterInfo.MaxHp;
		base.HP = base.MaxHP;
		base.ATK = MonsterInfo.Attack;
		base.DEF = MonsterInfo.Defense;
		MissionId = missionId;
	}
}
