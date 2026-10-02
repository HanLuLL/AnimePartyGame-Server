using UI;
using party.model;

namespace GameLogic;

public class SurveyData
{
	public int id;

	public QuestionModel serverData;

	public string url;

	public SurveyInfoConfigure surveyInfoConfigure => id.GetSurveyInfoConfigure();

	public SurveyData(int _id, QuestionModel serverInfo)
	{
		id = _id;
		serverData = serverInfo;
	}

	public void UpdateData(QuestionModel serverInfo)
	{
		serverData = serverInfo;
	}
}
