using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_5003101 : BuffData
{
	private static readonly OnCreate_BuffData_5003101 _onActionEnd = new OnCreate_BuffData_5003101();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnActionEnd = _onActionEnd;
	}
}
