using Tools;

namespace GameLogic;

public class SurveySignal
{
	public readonly Signal<int> surveyStateUpdated = new Signal<int>();
}
