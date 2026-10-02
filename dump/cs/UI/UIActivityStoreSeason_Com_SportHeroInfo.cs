using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIActivityStoreSeason_Com_SportHeroInfo : GComponent
{
	public HeroCardData HeroCard;

	public GGraph mohu;

	public GButton btn_Next;

	public GButton btn_Pre;

	public GLoader loader_Hero;

	public GTextField txt_HeroName;

	public GList list_Info;

	public GButton btn_Close;

	public Transition Cut_in;

	public const string URL = "ui://begz6gfvtaoa3a";

	public void RefreshHeroSportInfo(HeroCardData hero)
	{
		HeroCard = hero;
		loader_Hero.url = hero.GetStandingPainting().GetCharacterReady();
		string local = hero.InfoConfig.NameID.GetLocal(UIStringType.Character);
		string local2 = hero.InfoConfig.NickID.GetLocal(UIStringType.Character);
		txt_HeroName.text = local + "[size=36]-" + local2 + "[/size]";
		SportsMeetData sportsMeetData = SimpleSingletonProvider<GameLogicManager>.inst.heroCard.sportsMeetData;
		List<SportHeroInfo> infos = new List<SportHeroInfo>
		{
			new SportHeroInfo
			{
				data = sportsMeetData.GetHeroMapRecordById(hero.HeroId, SportsMeetData.GetMapIdBySportsMeetRank(1)),
				textId = 1080002
			},
			new SportHeroInfo
			{
				data = sportsMeetData.GetHeroMapRecordById(hero.HeroId, SportsMeetData.GetMapIdBySportsMeetRank(2)),
				textId = 1080003
			},
			new SportHeroInfo
			{
				data = sportsMeetData.GetHeroMapRecordById(hero.HeroId, SportsMeetData.GetMapIdBySportsMeetRank(3)),
				textId = 1080004
			}
		};
		sportsMeetData.heroChallengeData.TryGetValue(hero.HeroId, out var value);
		infos.Add(new SportHeroInfo
		{
			data = (value?.GameStats?.MaxTotalDamage).GetValueOrDefault(),
			textId = 1080005
		});
		infos.Add(new SportHeroInfo
		{
			data = (value?.GameStats?.MaxSingleDamage).GetValueOrDefault(),
			textId = 1080006
		});
		infos.Add(new SportHeroInfo
		{
			data = (value?.GameStats?.MaxGetGold).GetValueOrDefault(),
			textId = 1080007
		});
		infos.Add(new SportHeroInfo
		{
			data = (value?.GameStats?.TotalTransferGold).GetValueOrDefault(),
			textId = 1080008
		});
		infos.Add(new SportHeroInfo
		{
			data = (value?.GameStats?.TotalDamage).GetValueOrDefault(),
			textId = 1080009
		});
		infos.Add(new SportHeroInfo
		{
			data = (value?.GameStats?.TreatmentScore).GetValueOrDefault(),
			textId = 1080010
		});
		list_Info.itemRenderer = delegate(int index, GObject item)
		{
			if (item is UIActivityStoreSeason_Com_SportInfoItem uIActivityStoreSeason_Com_SportInfoItem)
			{
				uIActivityStoreSeason_Com_SportInfoItem.txt_Times.text = infos[index].data.ToString();
				uIActivityStoreSeason_Com_SportInfoItem.txt_Title.text = infos[index].textId.GetLocal(UIStringType.GUI);
			}
		};
		list_Info.numItems = infos.Count;
	}

	public static UIActivityStoreSeason_Com_SportHeroInfo CreateInstance()
	{
		return (UIActivityStoreSeason_Com_SportHeroInfo)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_SportHeroInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		btn_Next = (GButton)GetChildAt(1);
		btn_Pre = (GButton)GetChildAt(2);
		loader_Hero = (GLoader)GetChildAt(5);
		txt_HeroName = (GTextField)GetChildAt(6);
		list_Info = (GList)GetChildAt(7);
		btn_Close = (GButton)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
	}
}
