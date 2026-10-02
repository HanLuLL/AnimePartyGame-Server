using Google.Protobuf.Collections;
using UI;

namespace GameLogic;

public class Skill_12802 : Skill_128
{
	public Skill_12802()
	{
		skillId = 12802;
		skillConfig = skillId.GetSkillConfigure();
		RepeatedField<int> buffId = skillConfig.BuffId;
		SkillBuffId = buffId[buffId.Count - 1];
	}
}
