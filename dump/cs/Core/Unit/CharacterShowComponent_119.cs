using GameLogic;

namespace Core.Unit;

public class CharacterShowComponent_119 : CharacterShowComponent, IMoveAnimRule
{
	public bool ShouldStopMoveAnim()
	{
		return false;
	}

	public override void UpdateAfterReconnect()
	{
		base.UpdateAfterReconnect();
		if (Owner != null && Owner.skill is Skill_119 skill_ && Owner.player.buffContainer?.GetBuff(skill_.skillConfig.BuffId[0]) != null)
		{
			if (Owner.characterAnimator.GetAnimeStatus("Talenting"))
			{
				Owner.characterAnimator.SetAnime("Talenting", status: false);
			}
			Owner.characterAnimator.Move(walk: true);
		}
	}
}
