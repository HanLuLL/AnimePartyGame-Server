using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_50011 : BuffData
{
	private static readonly OnHPChange_BuffData_50011 _onHealthStateChange = new OnHPChange_BuffData_50011();

	private static readonly OnCreate_BuffData_50011 _onCreate = new OnCreate_BuffData_50011();

	private static readonly OnPkAttackStart_BuffData_50011 _onPkAttackStart = new OnPkAttackStart_BuffData_50011();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnCreate = _onCreate;
		OnHealthStateChange = _onHealthStateChange;
		OnPKAttackStart = _onPkAttackStart;
	}
}
