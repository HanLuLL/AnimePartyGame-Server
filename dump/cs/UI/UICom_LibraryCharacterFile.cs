using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_LibraryCharacterFile : GComponent
{
	public Controller monsterType;

	public UICom_LibraryFileContent com_LibraryFile;

	public GTextField txt_CharacterName;

	public GTextField txt_CharacterNick;

	public GTextField txt_ATK;

	public GTextField txt_DEF;

	public GTextField txt_HP;

	public UICom_Icon_Gold com_Gold;

	public GTextField txt_Gold;

	public const string URL = "ui://xuaw6o8jpj0zq42";

	public static UICom_LibraryCharacterFile CreateInstance()
	{
		return (UICom_LibraryCharacterFile)UIPackage.CreateObject("Common", "Com_LibraryCharacterFile");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		monsterType = GetControllerAt(0);
		com_LibraryFile = (UICom_LibraryFileContent)GetChildAt(2);
		txt_CharacterName = (GTextField)GetChildAt(3);
		txt_CharacterNick = (GTextField)GetChildAt(6);
		txt_ATK = (GTextField)GetChildAt(9);
		txt_DEF = (GTextField)GetChildAt(11);
		txt_HP = (GTextField)GetChildAt(13);
		com_Gold = (UICom_Icon_Gold)GetChildAt(14);
		txt_Gold = (GTextField)GetChildAt(15);
	}

	public void RenderMonster(int monsterId, int difficulty)
	{
		MonsterInfoConfigure info = CharacterHandle.GetMonsterCharacterConfigure(monsterId);
		MonsterAttributeConfigureItem monsterAttributeConfigureByDifficulty = info.GetMonsterAttributeConfigureByDifficulty(difficulty);
		List<SkillInfoConfigure> skillIConfigs;
		if (monsterAttributeConfigureByDifficulty != null)
		{
			txt_ATK.text = monsterAttributeConfigureByDifficulty.Attack.ToString();
			txt_DEF.text = monsterAttributeConfigureByDifficulty.Defense.ToString();
			txt_HP.text = monsterAttributeConfigureByDifficulty.Blood.ToString();
			skillIConfigs = UIHelper.GetSkillConfigs(monsterAttributeConfigureByDifficulty.PveActiveSkill, monsterAttributeConfigureByDifficulty.PvePassiveSkills, monsterAttributeConfigureByDifficulty.PveActiveSkillExtra);
		}
		else
		{
			txt_ATK.text = info.Attack.ToString();
			txt_DEF.text = info.Defense.ToString();
			txt_HP.text = info.Blood.ToString();
			skillIConfigs = UIHelper.GetSkillConfigs(info.ActiveSkill, info.PassiveSkills);
		}
		if (info.Gold > 0)
		{
			txt_Gold.text = info.Gold.ToString();
			UICom_Icon_Gold uICom_Icon_Gold = com_Gold;
			bool flag = (txt_Gold.visible = true);
			uICom_Icon_Gold.visible = flag;
		}
		else
		{
			UICom_Icon_Gold uICom_Icon_Gold2 = com_Gold;
			bool flag = (txt_Gold.visible = false);
			uICom_Icon_Gold2.visible = flag;
		}
		com_LibraryFile.list_Skills.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UICom_LibrarySkillContent uICom_LibrarySkillContent && skillIConfigs != null && index >= 0 && index <= skillIConfigs.Count - 1)
			{
				SkillInfoConfigure skillInfoConfigure = skillIConfigs[index];
				uICom_LibrarySkillContent.txt_CD.text = skillInfoConfigure.Round.ToString();
				uICom_LibrarySkillContent.skillType.selectedIndex = ((skillInfoConfigure.SkillType != SkillType.Active) ? 1 : 0);
				uICom_LibrarySkillContent.txt_SkillName.SetVar("skillName", skillInfoConfigure.NameID.GetLocal(UIStringType.Skill)).FlushVars();
				if (info.HeroType == CharacterType.Monster)
				{
					skillInfoConfigure.DescID.RefreshMonsterSkillHyperlinkDesc(uICom_LibrarySkillContent.txt_SkillDesc, difficulty);
				}
				else
				{
					skillInfoConfigure.DescID.RefreshCharacterSkillHyperlinkDesc(uICom_LibrarySkillContent.txt_SkillDesc);
				}
			}
		};
		com_LibraryFile.txt_Mechanism.text = info.MechanismID.GetLocal(UIStringType.Monster);
		txt_CharacterName.text = CharacterHandle.GetCharacterName(monsterId);
		txt_CharacterNick.text = CharacterHandle.GetCharacterNickName(monsterId);
		Controller controller = monsterType;
		controller.selectedIndex = info.MonsterType switch
		{
			MonsterType.Boss => 1, 
			MonsterType.Elite => 2, 
			_ => 0, 
		};
		com_LibraryFile.list_Skills.numItems = 0;
		com_LibraryFile.list_Skills.numItems = skillIConfigs?.Count ?? 0;
		com_LibraryFile.list_Skills.ResizeToFit();
		com_LibraryFile.txt_Stroy.text = info.BiographyID.GetLocal(UIStringType.Monster);
		com_LibraryFile.scrollPane.percY = 0f;
	}
}
