using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_5001901 : BuffData
{
	private static readonly OnCreate_BuffData_5001901 _onCreate = new OnCreate_BuffData_5001901();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnCreate = _onCreate;
	}
}
