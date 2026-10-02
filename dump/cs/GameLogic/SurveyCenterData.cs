using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;

namespace GameLogic;

public class SurveyCenterData
{
	public Dictionary<int, SurveyData> surveyDatas = new Dictionary<int, SurveyData>();

	public void Init(MapField<int, QuestionModel> playerQuestionInfo)
	{
		surveyDatas.Clear();
		foreach (int key in playerQuestionInfo.Keys)
		{
			SurveyInfoConfigure surveyInfoConfigure = key.GetSurveyInfoConfigure();
			QuestionModel serverInfo = playerQuestionInfo[key];
			if (surveyInfoConfigure == null)
			{
				Debug.LogError($"问卷ID {key} 与 不存在问卷配置");
			}
			else
			{
				surveyDatas.Add(key, new SurveyData(key, serverInfo));
			}
		}
	}

	public void NotifySurveys(MapField<int, QuestionModel> modelQuestionInfo)
	{
		foreach (int key in modelQuestionInfo.Keys)
		{
			if (surveyDatas.TryGetValue(key, out var value))
			{
				value.UpdateData(modelQuestionInfo[key]);
			}
			else
			{
				surveyDatas.Add(key, new SurveyData(key, modelQuestionInfo[key]));
			}
			SimpleSingletonProvider<GameLogicManager>.inst.survey.signal.surveyStateUpdated.Dispatch(key);
		}
	}

	public bool CheckView()
	{
		foreach (SurveyData value in surveyDatas.Values)
		{
			if (value.serverData.State == QuestionModel.Types.QuestionState.None)
			{
				return true;
			}
		}
		return false;
	}

	public void SetSurveyUrl(int surveyId, string url)
	{
		if (surveyDatas.TryGetValue(surveyId, out var value))
		{
			value.url = url;
		}
	}
}
