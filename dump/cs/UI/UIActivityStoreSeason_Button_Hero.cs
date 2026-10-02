using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIActivityStoreSeason_Button_Hero : GButton
{
	private HeroCardData _heroData;

	public Controller level;

	public GList list_Star;

	public GTextField txt_Times;

	public const string URL = "ui://begz6gfvtaoa32";

	public void Refresh(HeroCardData hero)
	{
		_heroData = hero;
		icon = _heroData.GetStandingPainting().GetCharacterPhoto();
		SportsMeetData sportsMeetData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData;
		int heroMapRecordById = sportsMeetData.GetHeroMapRecordById(_heroData.HeroId, SportsMeetData.GetMapIdBySportsMeetRank(1));
		int heroMapRecordById2 = sportsMeetData.GetHeroMapRecordById(_heroData.HeroId, SportsMeetData.GetMapIdBySportsMeetRank(2));
		int heroMapRecordById3 = sportsMeetData.GetHeroMapRecordById(_heroData.HeroId, SportsMeetData.GetMapIdBySportsMeetRank(3));
		int num = heroMapRecordById + heroMapRecordById2 + heroMapRecordById3;
		txt_Times.text = $"{num}";
		base.title = _heroData.InfoConfig.NickID.GetLocal(UIStringType.Character);
		RefreshStarStatus(heroMapRecordById, heroMapRecordById2, heroMapRecordById3);
		if (num <= 4)
		{
			level.selectedIndex = 0;
		}
		else if (num <= 9)
		{
			level.selectedIndex = 1;
		}
		else if (num <= 14)
		{
			level.selectedIndex = 2;
		}
		else
		{
			level.selectedIndex = 3;
		}
	}

	private void RefreshStarStatus(int finalTimes, int semifinalsTimes, int knockoutStageTimes)
	{
		list_Star.numItems = 3;
		RefreshStar(knockoutStageTimes, 0);
		RefreshStar(semifinalsTimes, 1);
		RefreshStar(finalTimes, 2);
	}

	private void RefreshStar(int times, int index)
	{
		GObject childAt = list_Star.GetChildAt(index);
		if (childAt != null)
		{
			childAt.grayed = times <= 0;
		}
	}

	public static UIActivityStoreSeason_Button_Hero CreateInstance()
	{
		return (UIActivityStoreSeason_Button_Hero)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Button_Hero");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		level = GetControllerAt(1);
		list_Star = (GList)GetChildAt(8);
		txt_Times = (GTextField)GetChildAt(9);
	}
}
