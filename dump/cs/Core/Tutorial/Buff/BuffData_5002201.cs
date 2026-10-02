using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_5002201 : BuffData
{
	private static readonly OnCreate_BuffData_5002201 _onActionStart = new OnCreate_BuffData_5002201();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnActionStart = _onActionStart;
	}
}
