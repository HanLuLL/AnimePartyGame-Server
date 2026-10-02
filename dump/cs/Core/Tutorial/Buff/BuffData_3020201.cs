using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_3020201 : BuffData
{
	private static readonly OnCreate_BuffData_3020201 _onCreate = new OnCreate_BuffData_3020201();

	private static readonly OnPkDefendEnd_BuffData_3020201 _onPkDefendEnd = new OnPkDefendEnd_BuffData_3020201();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnCreate = _onCreate;
		OnPkDefendEnd = _onPkDefendEnd;
	}
}
