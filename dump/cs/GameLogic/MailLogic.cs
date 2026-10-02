using System.Collections.Generic;
using Core.Net;
using Cysharp.Threading.Tasks;
using Google.Protobuf.Collections;
using Tools;
using party.model;
using party.protocol;

namespace GameLogic;

public class MailLogic : IRPCSync
{
	public MailContainer _container;

	public MailSignal signal;

	public void InitFromServer(MapField<int, party.model.MailData> Mails)
	{
		_container = new MailContainer();
		signal = new MailSignal();
		foreach (KeyValuePair<int, party.model.MailData> Mail in Mails)
		{
			_container.TryAdd(Mail.Value);
		}
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.MailAddS2C.OnMailAddS2CServerCallBackAsync = OnMailAddS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MailReadS2C.OnMailReadS2CServerCallBackAsync = OnMailReadS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MailDelReadS2C.OnMailDelReadS2CServerCallBackAsync = OnMailDelReadS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MailDelS2C.OnMailDelS2CServerCallBackAsync = OnMailDelS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MailGetRewardS2C.OnMailGetRewardS2CServerCallBackAsync = OnMailGetRewardS2CServerCallBack;
		MonoSingletonProvider<NetManager>.inst.RPC.MailStarS2C.OnMailStarS2CServerCallBackAsync = OnMailStarS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.MailAddS2C.OnMailAddS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MailReadS2C.OnMailReadS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MailDelReadS2C.OnMailDelReadS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MailDelS2C.OnMailDelS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MailGetRewardS2C.OnMailGetRewardS2CServerCallBackAsync = null;
		MonoSingletonProvider<NetManager>.inst.RPC.MailStarS2C.OnMailStarS2CServerCallBackAsync = null;
	}

	private async UniTask OnMailAddS2CServerCallBack(MailAddS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			_container.TryAdd(model.Mail);
			signal.mailStatus.Dispatch();
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestMailReadC2S(int mailId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.MailReadC2S.MailReadC2SCall(new MailReadC2S
		{
			Id = mailId
		});
	}

	private async UniTask OnMailReadS2CServerCallBack(MailReadS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}

	public RPCAsyncResult RequestMailDelReadC2S(int mailId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.MailDelReadC2S.MailDelReadC2SCall(new MailDelReadC2S
		{
			Id = mailId
		});
	}

	private async UniTask OnMailDelReadS2CServerCallBack(MailDelReadS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		foreach (int id in model.Ids)
		{
			_container.TryRemove(id);
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestMailGetRewardC2S(int mailId)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.MailGetRewardC2S.MailGetRewardC2SCall(new MailGetRewardC2S
		{
			Id = mailId
		});
	}

	private async UniTask OnMailGetRewardS2CServerCallBack(MailGetRewardS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		foreach (int id in model.Ids)
		{
			_container.UpdataStatus(id);
		}
		await UniTask.CompletedTask;
	}

	private async UniTask OnMailDelS2CServerCallBack(MailDelS2C model, int errid, bool isdispatch)
	{
		if (errid != 0)
		{
			return;
		}
		foreach (int id in model.Ids)
		{
			_container.TryRemove(id);
		}
		await UniTask.CompletedTask;
	}

	public RPCAsyncResult RequestMailStarS2C(int mailId, bool collectedStatus)
	{
		return MonoSingletonProvider<NetManager>.inst.RPC.MailStarC2S.MailStarC2SCall(new MailStarC2S
		{
			Id = mailId,
			IsStarMail = collectedStatus
		});
	}

	private async UniTask OnMailStarS2CServerCallBackAsync(MailStarS2C model, int errId, bool isDispatch)
	{
		if (errId == 0)
		{
			(_container?.TryGetMail(model.Id))?.UpdateCollectedStatus(model.IsStarMail);
			await UniTask.CompletedTask;
		}
	}
}
