using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SinglePlayer.Tools;
using Tools;

namespace SinglePlayer.GamePlay.Character;

public class Monster : CharacterLogic
{
	public MonsterView view;

	public Monster(MonsterProperty property)
		: base(property)
	{
		property.SetCharacter(this);
		Game.GetModel<GlobalSignal>().GenerateDicePoint.AddListener(OnGenerateDicePoint);
	}

	public void Dispose()
	{
		Game.GetModel<GlobalSignal>().GenerateDicePoint.RemoveListener(OnGenerateDicePoint);
		Game.GetModel<GameData>().DisposeMonsterProperty();
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

	protected virtual void OnThrowDice()
	{
	}

	protected int GetTriggerParam(int index)
	{
		return _property.child<MonsterProperty>().MonsterInfo.TriggerParam.GetSafeByIndex(index);
	}

	private void OnGenerateDicePoint(IList<int> values)
	{
		if (_property.child<MonsterProperty>().MonsterInfo.TriggerPoint.ContainsAny(values))
		{
			OnThrowDice();
		}
	}
}
