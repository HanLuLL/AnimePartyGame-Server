using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_CharacterFile : GComponent
{
	public UICom_FileContent com_File;

	public const string URL = "ui://xuaw6o8jpx78j9m";

	public static UICom_CharacterFile CreateInstance()
	{
		return (UICom_CharacterFile)UIPackage.CreateObject("Common", "Com_CharacterFile");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_File = (UICom_FileContent)GetChildAt(1);
	}

	public void RenderMonster(int monsterId, int difficulty)
	{
		MonsterInfoConfigure info = CharacterHandle.GetMonsterCharacterConfigure(monsterId);
		MonsterAttributeConfigureItem monsterAttributeConfigureByDifficulty = info.GetMonsterAttributeConfigureByDifficulty(difficulty);
		List<SkillInfoConfigure> skillIConfigs;
		if (monsterAttributeConfigureByDifficulty != null)
		{
			com_File.txt_ATK.text = monsterAttributeConfigureByDifficulty.Attack.ToString();
			com_File.txt_DEF.text = monsterAttributeConfigureByDifficulty.Defense.ToString();
			com_File.txt_HP.text = monsterAttributeConfigureByDifficulty.Blood.ToString();
			skillIConfigs = UIHelper.GetSkillConfigs(monsterAttributeConfigureByDifficulty.PveActiveSkill, monsterAttributeConfigureByDifficulty.PvePassiveSkills, monsterAttributeConfigureByDifficulty.PveActiveSkillExtra);
		}
		else
		{
			com_File.txt_ATK.text = info.Attack.ToString();
			com_File.txt_DEF.text = info.Defense.ToString();
			com_File.txt_HP.text = info.Blood.ToString();
			skillIConfigs = UIHelper.GetSkillConfigs(info.ActiveSkill, info.PassiveSkills);
		}
		if (info.Gold > 0)
		{
			com_File.txt_Gold.text = info.Gold.ToString();
			UICom_Icon_Gold com_Gold = com_File.com_Gold;
			bool flag = (com_File.txt_Gold.visible = true);
			com_Gold.visible = flag;
		}
		else
		{
			UICom_Icon_Gold com_Gold2 = com_File.com_Gold;
			bool flag = (com_File.txt_Gold.visible = false);
			com_Gold2.visible = flag;
		}
		com_File.list_Skills.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UICom_SkillContent uICom_SkillContent && skillIConfigs != null && index >= 0 && index <= skillIConfigs.Count - 1)
			{
				SkillInfoConfigure skillInfoConfigure = skillIConfigs[index];
				uICom_SkillContent.txt_CD.text = skillInfoConfigure.Round.ToString();
				uICom_SkillContent.skillType.selectedIndex = ((skillInfoConfigure.SkillType != SkillType.Active) ? 1 : 0);
				uICom_SkillContent.txt_SkillName.SetVar("skillName", skillInfoConfigure.NameID.GetLocal(UIStringType.Skill)).FlushVars();
				if (info.HeroType == CharacterType.Monster)
				{
					skillInfoConfigure.DescID.RefreshMonsterSkillHyperlinkDesc(uICom_SkillContent.txt_SkillDesc, difficulty);
				}
				else
				{
					skillInfoConfigure.DescID.RefreshCharacterSkillHyperlinkDesc(uICom_SkillContent.txt_SkillDesc);
				}
			}
		};
		com_File.txt_Mechanism.text = info.MechanismID.GetLocal(UIStringType.Monster);
		com_File.txt_CharacterName.text = CharacterHandle.GetCharacterName(monsterId);
		com_File.txt_CharacterNick.text = CharacterHandle.GetCharacterNickName(monsterId);
		com_File.list_Skills.numItems = 0;
		com_File.list_Skills.numItems = skillIConfigs?.Count ?? 0;
		com_File.list_Skills.ResizeToFit();
		com_File.txt_Stroy.text = info.BiographyID.GetLocal(UIStringType.Monster);
		com_File.scrollPane.percY = 0f;
	}
}
