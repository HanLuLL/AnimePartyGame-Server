using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_5002001 : BuffData
{
	private static readonly OnCreate_BuffData_5002001 _onCreate = new OnCreate_BuffData_5002001();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnCreate = _onCreate;
	}
}
