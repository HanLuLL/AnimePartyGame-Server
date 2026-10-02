using System.Collections.Generic;
using Core;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityTimer;
using party.model;

namespace GameLogic;

public class CampaignData
{
	private CampaignLevelConfigure CampaignLevel;

	public readonly CampaignTaskData campaignTaskData;

	private int FightCount;

	private int KillCount;

	private int DeadCount;

	private int LevelUp;

	private bool Teaching;

	private Timer ActionTimer;

	private List<CampaignTriggerConfigure> triggerConfigures => CampaignLevel?.CampaignTriggerConfigures;

	public CampaignData(CampaignLevelConfigure campaignLevel)
	{
		CampaignLevel = campaignLevel;
		VictoryCondition victoryCondition = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.info.VictoryCondition;
		if (victoryCondition != null)
		{
			campaignTaskData = new CampaignTaskData(victoryCondition, CampaignLevel);
		}
	}

	public void UpdateCampaignTask(VictoryCondition modelVictoryCondition)
	{
		campaignTaskData?.UpdateCampaignTask(modelVictoryCondition);
	}

	public void TagTimer(Timer actionTimer)
	{
		ActionTimer = actionTimer;
		if (Teaching)
		{
			PauseTimer();
		}
	}

	private void PauseTimer()
	{
		Teaching = true;
		if (ActionTimer != null && (!ActionTimer.isDone || !ActionTimer.isCancelled))
		{
			ActionTimer.Pause();
		}
	}

	private void ResumeTimer()
	{
		Teaching = false;
		if (ActionTimer != null && ActionTimer.isPaused && (!ActionTimer.isDone || !ActionTimer.isCancelled))
		{
			ActionTimer.Resume();
		}
	}

	public void TriggerFirstFight()
	{
		TriggerTutorial(CampaignTriggerType.FirstEnterPk, FightCount);
		FightCount++;
	}

	public void TriggerFirstKill()
	{
		TriggerTutorial(CampaignTriggerType.FirstKill, KillCount);
		KillCount++;
	}

	public void TriggerFirstDead()
	{
		TriggerTutorial(CampaignTriggerType.FirstKilled, DeadCount);
		DeadCount++;
	}

	public void TriggerRound()
	{
		int round = SimpleSingletonProvider<GameLogicManager>.inst.room.curRoomInfo.Round;
		TriggerTutorial(CampaignTriggerType.Round, round);
	}

	public void TriggerStarLevelUp()
	{
		TriggerTutorial(CampaignTriggerType.FirstStarLevelUp, LevelUp);
		LevelUp++;
	}

	public void TriggerProgress()
	{
		int curMaxGameProgress = SimpleSingletonProvider<GameLogicManager>.inst.battle.CurMaxGameProgress;
		TriggerTutorial(CampaignTriggerType.GameProgress, curMaxGameProgress);
	}

	public async void TriggerMapEvent(int mapEventId)
	{
		await SimpleSingletonProvider<DelaySignalManager>.inst.Delay(1500);
		TriggerTutorial(CampaignTriggerType.MapEvent, mapEventId);
	}

	private async void TriggerTutorial(CampaignTriggerType type, int count)
	{
		if (triggerConfigures == null)
		{
			return;
		}
		for (int i = 0; i < triggerConfigures.Count; i++)
		{
			if (triggerConfigures[i].CampaignTriggerType.Contains(type))
			{
				RepeatedField<int> campaignTriggerParams = triggerConfigures[i].CampaignTriggerParams;
				if ((campaignTriggerParams.Count == 0 && count == 0) || (campaignTriggerParams.Count > 0 && campaignTriggerParams[0] == count))
				{
					PauseTimer();
					await SimpleSingletonProvider<UIManager>.inst.explain.ShowTutorial(triggerConfigures[i].TutorialID, ResumeTimer);
				}
			}
		}
	}
}
