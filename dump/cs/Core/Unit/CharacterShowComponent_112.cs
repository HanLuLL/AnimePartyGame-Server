using System;

namespace Core.Unit;

public class CharacterShowComponent_112 : CharacterShowComponent
{
	public override void InitComponent(Character character)
	{
		base.InitComponent(character);
		if (Owner.player.buffContainer?.GetBuff(1121103) != null)
		{
			OnCureCountChange(Owner.player.Property.CureCount.property.Value);
			PropertyData<int> cureCount = Owner.player.Property.CureCount;
			cureCount.UpdateAction = (Action<int>)Delegate.Combine(cureCount.UpdateAction, new Action<int>(OnCureCountChange));
		}
	}

	public override void Dispose()
	{
		PropertyData<int> cureCount = Owner.player.Property.CureCount;
		cureCount.UpdateAction = (Action<int>)Delegate.Remove(cureCount.UpdateAction, new Action<int>(OnCureCountChange));
		Owner.RemoveScale(Character.ScaleSourceType.ShiLaiMu);
		base.Dispose();
	}

	private void OnCureCountChange(int count)
	{
		float scale = 0.9f + 1.5f * (float)count / (float)(count + 15);
		Owner.SetScale(Character.ScaleSourceType.ShiLaiMu, scale);
	}
}
