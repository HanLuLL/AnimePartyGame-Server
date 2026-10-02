using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_5000501 : BuffData
{
	private static readonly OnCreate_BuffData_5000501 _onAfterThrowDice = new OnCreate_BuffData_5000501();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnThrowDice = _onAfterThrowDice;
	}
}
