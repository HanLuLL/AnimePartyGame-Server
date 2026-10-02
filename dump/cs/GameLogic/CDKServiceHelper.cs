using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using party.protocol;

namespace GameLogic;

public static class CDKServiceHelper
{
	public static RPCAsyncResult RequestGiftCdkC2S(string _cdk)
	{
		string cdk = _cdk.Replace(" ", "");
		MonoSingletonProvider<NetManager>.inst.RPC.GiftCdkS2C.OnGiftCdkS2CServerCallBackAsync = OnGiftCdkS2CServerCallBack;
		return MonoSingletonProvider<NetManager>.inst.RPC.GiftCdkC2S.GiftCdkC2SCall(new GiftCdkC2S
		{
			Cdk = cdk
		});
	}

	private static async UniTask OnGiftCdkS2CServerCallBack(GiftCdkS2C model, int errid, bool isdispatch)
	{
		if (errid == 0)
		{
			await UniTask.CompletedTask;
		}
	}
}
