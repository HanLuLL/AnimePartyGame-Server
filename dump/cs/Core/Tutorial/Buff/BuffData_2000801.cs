using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_2000801 : BuffData
{
	private static readonly OnCreate_BuffData_2000801 _onCreate = new OnCreate_BuffData_2000801();

	private static readonly OnRemove_BuffData_2000801 _onRemove = new OnRemove_BuffData_2000801();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnCreate = _onCreate;
		OnRemove = _onRemove;
	}
}
