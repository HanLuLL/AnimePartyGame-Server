using UnityEngine;
using UnityEngine.Scripting;

namespace Core.Tutorial.Buff;

[Preserve]
public class BuffData
{
	public BuffInfoConfigure Config;

	public BaseBuffModule OnCreate;

	public BaseBuffModule OnRemove;

	public BaseBuffModule OnRoundStart;

	public BaseBuffModule OnActionStart;

	public BaseBuffModule OnActionEnd;

	public BaseBuffModule OnThrowDice;

	public BaseBuffModule OnPKAttackStart;

	public BaseBuffModule OnPKDefendStart;

	public BaseBuffModule OnPkAttackEnd;

	public BaseBuffModule OnPkDefendEnd;

	public BaseBuffModule OnHealthStateChange;

	public virtual void Initialize(int buffId)
	{
		if (!StaticConfigure.Buff.InfoDict.TryGetValue(buffId, out Config))
		{
			Debug.LogError($"无法通过buffId:{buffId}在Buff.InfoDict中获取正确的配置");
		}
	}
}
