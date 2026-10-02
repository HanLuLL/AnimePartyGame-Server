using Cysharp.Threading.Tasks;
using GameLogic;
using Tools;
using party.model;

namespace Core.Unit;

public class CharacterShowComponent_128 : CharacterShowComponent
{
	public override void InitComponent(Character character)
	{
		base.InitComponent(character);
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.AddListener(OnBuffChange);
	}

	public override void Dispose()
	{
		base.Dispose();
		SimpleSingletonProvider<GameLogicManager>.inst.buff.buff.RemoveListener(OnBuffChange);
	}

	private void OnBuffChange()
	{
		if (Owner?.skill is Skill_128 skill_)
		{
			Buff buff = Owner.player.buffContainer?.GetBuff(skill_.SkillBuffId);
			if (buff != null && buff.Progress > 0)
			{
				skill_.SetSignalByRoadblock();
			}
		}
	}

	public override void UpdateAfterReconnect()
	{
		base.UpdateAfterReconnect();
		if (Owner != null)
		{
			if (Owner.skill is Skill_128 skill_ && Owner.player.buffContainer?.GetBuff(skill_.SkillBuffId) != null)
			{
				SimpleSingletonProvider<GameLogicManager>.inst.CreatePerform().PlayPlayerShow(Owner.player.Id, skill_.skillConfig.PerformSelf, $"主动技{skill_.skillConfig.Id} 释放演出").Forget();
			}
			else if (Owner.characterAnimator.GetAnimeStatus("Talenting"))
			{
				Owner.characterAnimator.SetAnime("Talenting", status: false);
				Owner.characterAnimator.TriggerAnime("Talent-End");
			}
		}
	}
}
