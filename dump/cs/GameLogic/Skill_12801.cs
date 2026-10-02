using Google.Protobuf.Collections;
using UI;

namespace GameLogic;

public class Skill_12801 : Skill_128
{
	public Skill_12801()
	{
		skillId = 12801;
		skillConfig = skillId.GetSkillConfigure();
		RepeatedField<int> buffId = skillConfig.BuffId;
		SkillBuffId = buffId[buffId.Count - 1];
	}
}
