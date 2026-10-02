using GameLogic;
using Tools;
using UI;

namespace Core;

public static class GuideEvent
{
	public static void GuideEvent_100301_4()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.GuideOpenVictoryCondition();
	}

	public static void GuideEvent_100303_2()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.GuideOpenPlayerDetail();
	}

	public static void GuideEvent_100303_3()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guide.DealOperateSkill();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.handCard.GuideTriggerSkill();
	}

	public static void GuideEvent_100304_2()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guide.DealOperateMove();
		SimpleSingletonProvider<GameLogicManager>.inst.guide.playerMovePoint = 7;
		SimpleSingletonProvider<GameLogicManager>.inst.battle.handCard.GuideTriggerMove();
	}

	public static void GuideEvent_100305_2()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guide.DealOperateMove();
		SimpleSingletonProvider<GameLogicManager>.inst.guide.playerMovePoint = 10;
		SimpleSingletonProvider<GameLogicManager>.inst.battle.handCard.GuideTriggerMove();
	}

	public static void GuideEvent_100306_2()
	{
		SimpleSingletonProvider<UIManager>.inst.guide.HideMask();
		SimpleSingletonProvider<GameLogicManager>.inst.guide.DealDirSelect().Forget();
	}

	public static void GuideEvent_100307_2()
	{
		SimpleSingletonProvider<UIManager>.inst.landFillingStation.GuideTriggerStop();
	}

	public static void GuideEvent_100400_2()
	{
		(SimpleSingletonProvider<UIManager>.inst.bottomMenuPanel as BottomMenuPanel)?.GuideTriggerOpenNoviceScene();
	}

	public static void GuideEvent_100400_3()
	{
		SimpleSingletonProvider<UIManager>.inst.selectTutorial.GuideSelectTutorialB();
	}

	public static void GuideEvent_100500_2()
	{
	}

	public static void GuideEvent_100500_3()
	{
		SimpleSingletonProvider<UIManager>.inst.guide.HideMask();
		SimpleSingletonProvider<UIManager>.inst.guide.ShowCardArrow();
		SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.DisableCancelSelect();
		SimpleSingletonProvider<GameLogicManager>.inst.guide.DealOperateCard();
	}

	public static void GuideEvent_100501_1()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.battle.battleInfo.GuideSelectPlayer();
	}

	public static void GuideEvent_100502_2()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guide.DealOperateMove();
		SimpleSingletonProvider<GameLogicManager>.inst.guide.playerMovePoint = 7;
		SimpleSingletonProvider<GameLogicManager>.inst.battle.handCard.GuideTriggerMove();
	}

	public static void GuideEvent_100503_2()
	{
		SimpleSingletonProvider<UIManager>.inst.Fight.GuideFightPK();
	}

	public static void GuideEvent_100503_3()
	{
	}

	public static void GuideEvent_100503_4()
	{
		SimpleSingletonProvider<UIManager>.inst.Fight.GuideReadyCard();
	}

	public static void GuideEvent_100504_1()
	{
		SimpleSingletonProvider<UIManager>.inst.Fight.GuideFightResult(1, 6, isDodge: false);
	}

	public static void GuideEvent_100504_2()
	{
	}

	public static void GuideEvent_100505_1()
	{
	}

	public static void GuideEvent_100506_1()
	{
		SimpleSingletonProvider<UIManager>.inst.Fight.GuideFightPK();
	}

	public static void GuideEvent_100506_2()
	{
		SimpleSingletonProvider<UIManager>.inst.Fight.GuideReadyCard();
	}

	public static void GuideEvent_100507_1()
	{
		SimpleSingletonProvider<UIManager>.inst.Fight.GuideFightResult(6, 1, isDodge: true);
	}

	public static void GuideEvent_100507_2()
	{
	}

	public static void GuideEvent_100508_1()
	{
	}

	public static void GuideEvent_100509_4()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.guide.SureMoveStop();
	}

	public static void GuideEvent_100800_1()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HomePanel homePanel)
		{
			homePanel.GuideStartGame();
		}
	}

	public static void GuideEvent_200100_1()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is ProductRecommendationPanel productRecommendationPanel)
		{
			productRecommendationPanel.GuideBuyGoods();
		}
	}

	public static void GuideEvent_200100_2()
	{
		SimpleSingletonProvider<UIManager>.inst.purchase.GuideBuyGoods();
	}

	public static void GuideEvent_200200_1()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.OnGuide200200Step1();
		}
	}

	public static void GuideEvent_200200_2()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideOpenHeroDetailStable();
		}
	}

	public static void GuideEvent_200200_3()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideOpenHeroContract();
		}
	}

	public static void GuideEvent_200200_4()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideOpenHeroGiftTab();
		}
	}

	public static void GuideEvent_200200_5()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideSelectGift();
		}
	}

	public static void GuideEvent_200200_6()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideSureGift();
		}
	}

	public static void GuideEvent_200200_7()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideLockFavorLV();
		}
	}

	public static void GuideEvent_200200_8()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideOpenContractTab();
		}
	}

	public static void GuideEvent_200200_9()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideShowContractReward();
		}
	}

	public static void GuideEvent_200150_1()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideOpenHeroDetailStable();
		}
	}

	public static void GuideEvent_200150_2()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideShowContractUP();
		}
	}

	public static void GuideEvent_200150_3()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideShowContractMaterial();
		}
	}

	public static void GuideEvent_200150_4()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideShowContractUPgrade();
		}
	}

	public static void GuideEvent_200150_5()
	{
		if (SimpleSingletonProvider<UIManager>.inst.currentPanel is HeroPanel heroPanel)
		{
			heroPanel.GuideShowContractLVInfo();
		}
	}
}
