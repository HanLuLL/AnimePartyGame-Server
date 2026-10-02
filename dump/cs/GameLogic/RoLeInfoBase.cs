using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using party.model;

namespace GameLogic;

public static class RoLeInfoBase
{
	private const int DefaultRemainCoin = 0;

	private const long DefaultRoleCtime = -1L;

	public static void uploadCreateRoleToSdk()
	{
		try
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (account == null)
			{
				return;
			}
			Player playerInfo = account.GetPlayerInfo();
			if (playerInfo != null)
			{
				Dictionary<string, object> dictionary = BuildRoleInfo(account, playerInfo);
				if (dictionary != null)
				{
					((BnGameBase)BnGameSdk.Instance).uploadCreateRole(dictionary);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[SDK] 调用uploadCreateRoleToSdk失败: " + ex.ToString());
		}
	}

	public static void UploadRoleInfoToSDK()
	{
		try
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (account == null)
			{
				return;
			}
			Player playerInfo = account.GetPlayerInfo();
			if (playerInfo != null)
			{
				Dictionary<string, object> dictionary = BuildRoleInfo(account, playerInfo);
				if (dictionary != null)
				{
					Debug.Log("[SDK] 玩家进入游戏，上传角色信息");
					((BnGameBase)BnGameSdk.Instance).uploadRoleInfo(dictionary);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[SDK] 调用UploadRoleInfoToSDK失败: " + ex.ToString());
		}
	}

	public static void UpDateRoleInfoToSDK()
	{
		try
		{
			AccountLogic account = SimpleSingletonProvider<GameLogicManager>.inst.account;
			if (account == null)
			{
				return;
			}
			Player playerInfo = account.GetPlayerInfo();
			if (playerInfo != null)
			{
				Dictionary<string, object> dictionary = BuildRoleInfo(account, playerInfo);
				if (dictionary != null)
				{
					Debug.Log("[SDK] 玩家升级账户");
					((BnGameBase)BnGameSdk.Instance).updateRoleInfo(dictionary);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[SDK] 调用UpDateRoleInfoToSDK失败: " + ex.ToString());
		}
	}

	private static Dictionary<string, object> BuildRoleInfo(AccountLogic account, Player player)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		string text = null;
		if (BnSdkManager.Instance != null)
		{
			text = BnSdkManager.Instance.UserId;
			Debug.Log("[RoLeInfoBase] 尝试获取UserId: Instance存在, UserId=" + (string.IsNullOrEmpty(text) ? "null或空" : text));
		}
		else
		{
			Debug.LogWarning("[RoLeInfoBase] BnSdkManager.Instance 为 null，无法获取UserId");
		}
		string text2 = (string)(dictionary["uid"] = text);
		Debug.Log("[RoLeInfoBase] uid为：" + (text2 ?? "null") + "（来自服务器验证返回的UserId）");
		ulong accountId = account.GetAccountId();
		dictionary["rid"] = accountId.ToString();
		Debug.Log("role id为：" + accountId);
		dictionary["rn"] = account.GetName();
		dictionary["rl"] = player.Level;
		int num = 0;
		if (player?.Hero != null)
		{
			num = player.Hero.PveHeroStrengthen?.Level ?? 0;
		}
		dictionary["vl"] = num;
		string value = "0";
		try
		{
			if (player != null)
			{
				value = ((player.ServerId != null) ? player.ServerId.ToString() : "0");
			}
		}
		catch
		{
			value = "0";
		}
		dictionary["sid"] = value;
		dictionary["rcoin"] = 0;
		dictionary["rctime"] = -1L;
		return dictionary;
	}
}
