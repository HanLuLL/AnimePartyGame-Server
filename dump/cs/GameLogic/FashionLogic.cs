using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class FashionLogic : IRPCSync
{
	public MapField<int, int> defaultMap = new MapField<int, int>();

	public readonly Dictionary<int, MapField<int, int>> fashionDict = new Dictionary<int, MapField<int, int>>(10);

	private ShowingFashion showingFashion;

	public int RunningPlan;

	public readonly ReactiveProperty<bool> FashionRedSignal = new ReactiveProperty<bool>(initialValue: false);

	public void InitFromServer(int usePlan, RepeatedField<FashionPlan> _fashionPlan)
	{
		fashionDict.Clear();
		showingFashion = new ShowingFashion();
		foreach (FashionPlan item in _fashionPlan)
		{
			if (!item.Fashion.ContainsKey(6))
			{
				item.Fashion.Add(6, 0);
			}
			fashionDict.TryAdd(item.Plan, item.Fashion);
		}
		RunningPlan = usePlan;
		defaultMap = new MapField<int, int>
		{
			{
				1,
				StaticConfigure.Fashion.AccountHeadShots[0].Id
			},
			{
				2,
				StaticConfigure.Fashion.AccountBackgrounds[0].Id
			},
			{
				3,
				StaticConfigure.Fashion.CardBacks[0].Id
			},
			{
				4,
				StaticConfigure.Fashion.Dices[0].Id
			},
			{
				5,
				StaticConfigure.Fashion.Effects[0].Id
			},
			{
				6,
				GetCurrentKVId()
			}
		};
	}

	private void UpdatePlayerFashion()
	{
		if (showingFashion != null)
		{
			MapField<int, int> mapField = fashionDict[showingFashion.planID];
			mapField[1] = showingFashion.headShotId;
			mapField[2] = showingFashion.labelId;
			mapField[3] = showingFashion.cardId;
			mapField[4] = showingFashion.diceId;
			mapField[5] = showingFashion.killEffectId;
			mapField[6] = showingFashion.KvId;
		}
	}

	private void UpdataPlan()
	{
		if (showingFashion != null)
		{
			RunningPlan = showingFashion.planID;
		}
	}

	public ShowingFashion GetShowingFashion(int plan)
	{
		if (!fashionDict.TryGetValue(plan, out var value))
		{
			Debug.LogError($"当前取出装扮{plan}的配置失败，存在问题");
			return null;
		}
		showingFashion.UpdatePlan(plan, value);
		return showingFashion;
	}

	public ShowingFashion GetCurShowingFashion()
	{
		if (!fashionDict.TryGetValue(RunningPlan, out var value))
		{
			Debug.LogError($"当前取出装扮{RunningPlan}的配置失败，存在问题");
			return null;
		}
		showingFashion.UpdatePlan(RunningPlan, value);
		return showingFashion;
	}

	private int GetReadDefaultKVId()
	{
		if (!ES3.KeyExists(LocalStore.FashionDefaultKvReadCache))
		{
			return 0;
		}
		return ES3.Load<int>(LocalStore.FashionDefaultKvReadCache);
	}

	public void SaveReadDefaultKVId()
	{
		ES3.Save(LocalStore.FashionDefaultKvReadCache, GetCurrentKVId());
	}

	public bool HasDefaultKVUpdateRed()
	{
		return GetReadDefaultKVId() != GetCurrentKVId();
	}

	public string GetHomePanelKV()
	{
		ShowingFashion runningFashion = GetRunningFashion();
		int id = ((runningFashion.KvId == 0) ? GetCurrentKVId() : runningFashion.KvId);
		if (RunTimeRemoteConfigHandler.IsAuditMode)
		{
			id = 76117;
		}
		return id.GetKVVideoKey();
	}

	public int GetCurrentKVId()
	{
		foreach (FashionKVConfigure value in StaticConfigure.Fashion.KVDict.Values)
		{
			if (value.Current)
			{
				return value.Id;
			}
		}
		Debug.LogError("未找到 current=true 的主页KV配置");
		return 76001;
	}

	public MapField<int, int> GetCurFashion()
	{
		if (!fashionDict.TryGetValue(RunningPlan, out var value))
		{
			Debug.LogError($"当前取出装扮{RunningPlan}的配置失败，存在问题");
			return null;
		}
		return value;
	}

	public ShowingFashion GetRunningFashion()
	{
		return GetShowingFashion(RunningPlan);
	}

	public void UpdatePlanByServer(FashionPlan _Plan)
	{
		if (fashionDict.ContainsKey(_Plan.Plan))
		{
			fashionDict.Remove(_Plan.Plan);
		}
		fashionDict.TryAdd(_Plan.Plan, _Plan.Fashion);
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SetFashionS2C.OnSetFashionS2CServerCallBackAsync = OnSetFashionS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.SelectFashionPlanS2C.OnSelectFashionPlanS2CServerCallBackAsync = OnSelectFashionPlanS2CServerCallBack;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.SetFashionS2C.OnSetFashionS2CServerCallBackAsync = null;
	}

	public RPCAsyncResult RequestSetFashionC2S(int plan, MapField<int, int> fashion)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.SetFashionC2S.SetFashionC2SCall(new SetFashionC2S
		{
			Plan = plan,
			Fashion = { (IDictionary<int, int>)fashion }
		});
	}

	private async UniTask OnSetFashionS2CServerCallBack(SetFashionS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			UpdatePlayerFashion();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestSelectFashionPlanC2S(int plan)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.SelectFashionPlanC2S.SelectFashionPlanC2SCall(new SelectFashionPlanC2S
		{
			Plan = plan
		});
	}

	private async UniTask OnSelectFashionPlanS2CServerCallBack(SelectFashionPlanS2C model, int errId, bool isdispatch)
	{
		if (errId == 0)
		{
			UpdataPlan();
			await UniTask.CompletedTask;
		}
	}

	public void RegisterRed()
	{
		FashionRedSignal.Value = GetSystemStatus();
	}

	public void ClearNewChestSetSilent(ItemType itemType)
	{
		SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeToRemoveNewItemData(itemType);
	}

	public bool GetSystemStatus()
	{
		bool num = SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(ItemType.AccountHeadShot);
		bool flag = SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(ItemType.AccountBackground);
		bool flag2 = SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(ItemType.Dice);
		bool flag3 = SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(ItemType.Effect);
		bool flag4 = SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(ItemType.CardBack);
		bool flag5 = SimpleSingletonProvider<GameLogicManager>.inst.account.OnTypeGetNewData(ItemType.Kv);
		bool flag6 = HasDefaultKVUpdateRed();
		return num || flag || flag2 || flag3 || flag4 || flag5 || flag6;
	}

	public void Dispose()
	{
		fashionDict.Clear();
	}
}
