using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData_5004001 : BuffData
{
	private static readonly OnPkDefendEnd_BuffData_5004001 _onPkDefendEnd = new OnPkDefendEnd_BuffData_5004001();

	public override void Initialize(int buffId)
	{
		base.Initialize(buffId);
		OnPkDefendEnd = _onPkDefendEnd;
	}
}
