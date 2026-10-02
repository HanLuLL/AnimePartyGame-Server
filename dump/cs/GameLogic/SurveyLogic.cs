using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using party.model;
using party.protocol;

namespace GameLogic;

public class SurveyLogic : IRPCSync
{
	public SurveyCenterData surveyCenterData;

	public SurveySignal signal = new SurveySignal();

	public readonly ReactiveProperty<bool> questionnaireRedSignal = new ReactiveProperty<bool>(initialValue: false);

	public void InitFromServer(MapField<int, QuestionModel> playerQuestionInfo)
	{
		surveyCenterData = new SurveyCenterData();
		surveyCenterData.Init(playerQuestionInfo);
		RegisterRed();
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.NotifyQuestionS2C.OnNotifyQuestionS2CServerCallBackAsync = OnNotifyQuestionS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.GetQuestionUrlS2C.OnGetQuestionUrlS2CServerCallBackAsync = OnGetQuestionUrlS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.NotifyQuestionS2C.OnNotifyQuestionS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.GetQuestionUrlS2C.OnGetQuestionUrlS2CServerCallBackAsync = null;
	}

	public void RegisterRed()
	{
		foreach (int key in surveyCenterData.surveyDatas.Keys)
		{
			if (!LocalCache.GetSurveyRedStatusByIndex(key))
			{
				questionnaireRedSignal.Value = surveyCenterData.CheckView();
				return;
			}
		}
		questionnaireRedSignal.Value = false;
	}

	public void CacheRed()
	{
		foreach (int key in surveyCenterData.surveyDatas.Keys)
		{
			LocalCache.AddSurveyRedStatus(key);
		}
		questionnaireRedSignal.Value = false;
	}

	private async UniTask OnNotifyQuestionS2CServerCallBack(NotifyQuestionS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			surveyCenterData.NotifySurveys(model.QuestionInfo);
			RegisterRed();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestGetQuestionUrl(int surveyId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.GetQuestionUrlC2S.GetQuestionUrlC2SCall(new GetQuestionUrlC2S
		{
			ActivityId = surveyId
		});
	}

	private async UniTask OnGetQuestionUrlS2CServerCallBack(GetQuestionUrlS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			if (string.IsNullOrEmpty(model.Url))
			{
				SimpleSingletonProvider<UIManager>.inst.tips.ShowTips(100051);
			}
			surveyCenterData.SetSurveyUrl(model.Id, model.Url);
			await UniTask.CompletedTask;
		}
	}

	public SurveyData GetSurveyData(int surveyId)
	{
		return surveyCenterData.surveyDatas.GetValueOrDefault(surveyId);
	}
}
