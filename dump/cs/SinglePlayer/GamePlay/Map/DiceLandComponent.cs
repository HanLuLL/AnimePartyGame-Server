using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay.Map;

public abstract class DiceLandComponent
{
	public int LandId;

	public DiceLandEffectType effectType;

	protected DiceLandComponent(int landId)
	{
		LandId = landId;
	}

	public abstract UniTask PassByEffect();

	public abstract UniTask StepOnEffect();

	public virtual UniTask ShowAttributeChange(AttributeChangeInfo message)
	{
		return UniTask.CompletedTask;
	}
}
