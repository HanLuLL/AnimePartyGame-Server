using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_5002801 : BuffData
{
	private static readonly OnCreate_BuffData_5002801 _onCreate = new OnCreate_BuffData_5002801();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnCreate = _onCreate;
	}
}
