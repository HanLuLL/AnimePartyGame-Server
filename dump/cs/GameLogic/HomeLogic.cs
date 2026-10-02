using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;

namespace GameLogic;

public class HomeLogic
{
	private int messageIndex;

	private List<HomeMessage> homeMessages;

	public HomeLogic()
	{
		messageIndex = 0;
		homeMessages = new List<HomeMessage>();
		homeMessages.Add(new HomeMessage_7DailyGiftPVP());
		homeMessages.Add(new HomeMessage_7DailyGiftPVE());
		homeMessages.Add(new HomeMessage_MonthCard());
		homeMessages.Add(new HomeMessage_Collaborate());
		homeMessages.Add(new HomeMessage_Comeback());
	}

	public void Connect()
	{
	}

	public void Disconnect()
	{
	}

	public void TriggerMessage()
	{
		if (messageIndex < homeMessages.Count && !SimpleSingletonProvider<GameLogicManager>.inst.tutorial.IsNovice && SimpleSingletonProvider<UIManager>.inst.currentPanel is HomePanel)
		{
			HomeMessage homeMessage = homeMessages[messageIndex];
			messageIndex++;
			homeMessage.ShowMessage();
		}
	}

	public async UniTask OpenActivity(ActivityActivityEntrance2Configure ActivityData)
	{
		if (ActivityData != null)
		{
			if (ActivityData.InfoConfig.UiType == UIType.Panel)
			{
				await SimpleSingletonProvider<UIManager>.inst.OpenPanel(ActivityData.InfoConfig.PanelType, new RepeatedField<int> { ActivityData.InfoConfig.Id });
			}
			else if (ActivityData.InfoConfig.UiType == UIType.Window)
			{
				await SimpleSingletonProvider<UIManager>.inst.activityPopup.ShowActivity(ActivityData.InfoConfig.Id);
			}
		}
	}

	public void AddMessage(HomeMessage message)
	{
		for (int i = messageIndex; i < homeMessages.Count; i++)
		{
			if (homeMessages[i].GetType() == message.GetType())
			{
				return;
			}
		}
		homeMessages.Add(message);
		TriggerMessage();
	}
}
