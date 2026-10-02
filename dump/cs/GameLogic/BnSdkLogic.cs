using System.Collections.Generic;
using Core;
using Core.Net;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;
using party.model;
using party.protocol;

namespace GameLogic;

public class BnSdkLogic : IRPCSync
{
	private Player _player;

	private OrderInformation _orderInformation;

	private int _orderShopType;

	public void InitFromServer(Player player)
	{
		_player = player;
		_orderInformation = null;
		BnSdkManager.Instance.PayCallback += OnSDKPay;
	}

	public void Connect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ChinaCreateOrderS2C.OnChinaCreateOrderS2CServerCallBackAsync = OnChinaCreateOrderS2CServerCallBackAsync;
	}

	public void Disconnect()
	{
		MonoSingletonProvider<NetManager>.inst.RPC.ChinaCreateOrderS2C.OnChinaCreateOrderS2CServerCallBackAsync = null;
		BnSdkManager.Instance.PayCallback -= OnSDKPay;
	}

	public void CreateOrder(int goodsId, int count, int shopType)
	{
		if (_orderInformation != null)
		{
			Debug.LogError("#BN_SDK LOG# 支付： 还有订单正在进行中... 无法继续购买");
			return;
		}
		_orderShopType = shopType;
		_orderInformation = new OrderInformation(goodsId, shopType);
		MonoSingletonProvider<NetManager>.inst.RPC.ChinaCreateOrderC2S.ChinaCreateOrderC2SCall(new ChinaCreateOrderC2S
		{
			ProductId = goodsId,
			Quantity = count,
			Type = shopType
		});
	}

	private async UniTask OnChinaCreateOrderS2CServerCallBackAsync(ChinaCreateOrderS2C model, int errId, bool isDispatch)
	{
		if (errId != 0)
		{
			Close();
			Debug.LogError($"#BN_SDK LOG# 研发服务器创建订单失败 ErrID：{errId}");
			SimpleSingletonProvider<UIManager>.inst.loadingTip.Hide();
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(1063).Forget();
			return;
		}
		RechargeGoods rechargeGoodsByShopTypeAndGoodsID = SimpleSingletonProvider<GameLogicManager>.inst.store.GetRechargeGoodsByShopTypeAndGoodsID(_orderShopType, model.GoodsId);
		string name = GetName(rechargeGoodsByShopTypeAndGoodsID);
		ShopTabType orderShopType = (ShopTabType)_orderShopType;
		string productType = orderShopType.ToString();
		int result;
		Dictionary<string, object> dictionary = BnSdkManager.Instance.BuildOrderInfo(model.OrderId, _player.Id.ToString(), _player.Nick, _player.Level, 0, int.TryParse(_player.ServerId, out result) ? result : 0, "null", model.Amount, 1, name, productType, model.GoodsId.ToString(), name, 10, "http://" + GameSettings.IP + ":8989/china/pay/callback");
		if (dictionary != null)
		{
			BnSdkManager.Instance.Pay(dictionary);
		}
		else
		{
			Close();
			SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(1063).Forget();
		}
		await UniTask.CompletedTask;
	}

	private string GetName(RechargeGoods rechargeGoods)
	{
		if (rechargeGoods == null)
		{
			return "";
		}
		return rechargeGoods.GetName();
	}

	private void Close()
	{
		_orderInformation = null;
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
	}

	private void OnSDKPay(int code, string msg)
	{
		_orderInformation = null;
		Debug.Log($"#BN_SDK LOG# SDK购买回调 code:{code}| msg:{msg}");
		switch (code)
		{
		case 1:
			Debug.Log("#BN_SDK LOG# 支付成功");
			break;
		case 2:
			Debug.Log("#BN_SDK LOG# 支付已取消");
			break;
		default:
			Debug.LogError("#BN_SDK LOG# 支付失败: " + msg);
			break;
		}
		SimpleSingletonProvider<UIManager>.inst.loadingTip.HideImmediately();
	}
}
