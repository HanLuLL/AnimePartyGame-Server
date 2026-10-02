using System;
using System.Collections.Generic;
using Core;
using Core.Net;
using Core.Scene;
using Cysharp.Threading.Tasks;
using FairyGUI;
using GameLogic;
using SimpleJSON;
using Tools;
using UI;
using UnityEngine;

public class BnSdkManager : MonoBehaviour
{
	private DateTime _loginServerTimeStamp;

	private SystemLanguage _language;

	public string Sid { get; private set; }

	public string Extra { get; private set; }

	public string UserId { get; private set; }

	public static BnSdkManager Instance { get; private set; }

	public event Action<int, string> PayCallback;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void Initialize(SystemLanguage language)
	{
		_language = language;
	}

	public void LoginSDK(bool forceLogin = false, Action Success = null, Action fail = null)
	{
		try
		{
			Debug.Log("[BnSdkManager] ========== 开始调用SDK登录接口 ==========");
			Debug.Log(string.Format("[BnSdkManager] 调用前状态: Sid={0}, Extra={1}, UI交互={2}", string.IsNullOrEmpty(Sid) ? "空" : Sid, string.IsNullOrEmpty(Extra) ? "空" : Extra, GRoot.inst.touchable));
			if (!string.IsNullOrEmpty(Sid) || !string.IsNullOrEmpty(Extra) || !string.IsNullOrEmpty(UserId))
			{
				ClearLoginInfo();
				Debug.Log("[BnSdkManager] 已清除旧的登录信息");
			}
			GRoot.inst.touchable = false;
			Debug.Log("[BnSdkManager] 已禁用UI交互和ESC键，准备调用SDK登录接口");
			((BnGameBase)BnGameSdk.Instance).login((Action<int, string, BnLoginData>)async delegate(int ret, string msg, BnLoginData data)
			{
				GRoot.inst.touchable = true;
				switch (ret)
				{
				case 1:
				{
					Debug.Log("[登录流程] 步骤6: SDK返回登录验证的sid和extra");
					Debug.Log("SDK登录成功: sid=" + data.sid + ", extra=" + data.extra);
					Sid = data.sid;
					Extra = data.extra;
					_ = Sid;
					_ = Extra;
					if (LoginServiceHelper.connectToken != null)
					{
						LoginServiceHelper.connectToken.China.Sid = data.sid;
					}
					if (MonoSingletonProvider<NetManager>.inst != null)
					{
						_loginServerTimeStamp = MonoSingletonProvider<NetManager>.inst.ServerTime;
					}
					Success?.Invoke();
					Debug.Log("[登录流程] 步骤7: 获取SDK参数并准备发送到服务器");
					string message;
					switch (_language)
					{
					case SystemLanguage.Chinese:
					case SystemLanguage.ChineseSimplified:
						message = "登录成功";
						break;
					case SystemLanguage.Japanese:
						message = "ログイン成功";
						break;
					case SystemLanguage.ChineseTraditional:
						message = "登錄成功";
						break;
					default:
						message = "Login successful";
						break;
					}
					ShowLoginMessage(message);
					if (SimpleSingletonProvider<UIManager>.inst.currentPanel is LoginPanel loginPanel2)
					{
						if (BnSdkInit.Instance.AppID == "110001939")
						{
							loginPanel2.ChangeSDKLoginStatus(status: false);
						}
						else
						{
							loginPanel2.ChangeSDKLoginStatus(status: true);
						}
					}
					break;
				}
				case 2:
				{
					Debug.Log("[BnSdkManager] SDK登录取消");
					ClearLoginInfo();
					Debug.Log($"[BnSdkManager] UI交互已恢复: touchable={GRoot.inst.touchable}");
					SystemLanguage language = _language;
					string message = ((language != SystemLanguage.Chinese && language != SystemLanguage.ChineseSimplified) ? "Login cancelled" : "登录已取消");
					ShowLoginMessage(message);
					if (SimpleSingletonProvider<UIManager>.inst.currentPanel is LoginPanel loginPanel3)
					{
						loginPanel3.ChangeSDKLoginStatus(status: false);
					}
					Debug.Log("[BnSdkManager] 登录取消处理完成，用户可以再次点击'开始游戏'按钮");
					break;
				}
				case 4:
				{
					Debug.Log("SDK注销账号");
					SystemLanguage language = _language;
					string message = ((language != SystemLanguage.Chinese && language != SystemLanguage.ChineseSimplified) ? "Account logged out" : "账号已注销");
					ShowLoginMessage(message);
					if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Login)
					{
						await SimpleSingletonProvider<GameManager>.inst.ReLogin();
					}
					else
					{
						LoginSDK(forceLogin: true);
					}
					break;
				}
				default:
					Debug.LogError($"[BnSdkManager] SDK登录失败: ret={ret}, msg={msg}");
					Debug.Log("[BnSdkManager] 已清除Sid、Extra和UserId，当前Sid=null, Extra=null, UserId=null");
					Debug.Log($"[BnSdkManager] UI交互已恢复: touchable={GRoot.inst.touchable}");
					SimpleSingletonProvider<UIManager>.inst.messageBox.ShowOK(msg, delegate
					{
						Debug.Log("国服SDK 登录 失败：" + msg);
						fail?.Invoke();
					}).Forget();
					if (SimpleSingletonProvider<UIManager>.inst.currentPanel is LoginPanel loginPanel)
					{
						loginPanel.ChangeSDKLoginStatus(status: false);
					}
					Debug.Log("[BnSdkManager] 登录失败处理完成，用户可以再次点击'开始游戏'按钮");
					break;
				}
			});
		}
		catch (Exception ex)
		{
			Debug.LogError("========== SDK登录异常 ==========");
			Debug.LogError("[BnSdkManager] 异常类型: " + ex.GetType().Name);
			Debug.LogError("[BnSdkManager] 异常消息: " + ex.Message);
			Debug.LogError("[BnSdkManager] 异常堆栈: " + ex.StackTrace);
			if (ex.InnerException != null)
			{
				Debug.LogError("[BnSdkManager] 内部异常: " + ex.InnerException.Message);
				Debug.LogError("[BnSdkManager] 内部异常堆栈: " + ex.InnerException.StackTrace);
			}
			Debug.LogError(string.Format("[BnSdkManager] 异常前状态: Sid={0}, Extra={1}, UI交互={2}", string.IsNullOrEmpty(Sid) ? "空" : Sid, string.IsNullOrEmpty(Extra) ? "空" : Extra, GRoot.inst.touchable));
			ClearLoginInfo();
			Debug.LogError("[BnSdkManager] 异常处理：已清除登录信息");
			Debug.LogError($"[BnSdkManager] 异常后状态: Sid=null, Extra=null, UserId=null, UI交互={GRoot.inst.touchable}");
			Debug.LogError("====================================");
		}
	}

	private void ShowLoginMessage(string message)
	{
		Debug.Log("[登录消息]: " + message);
	}

	public void HandleBnSdkLoginResponse(string serverResponse)
	{
		Debug.Log("[登录流程] 步骤10: 处理游戏服务器验证结果");
		Debug.Log("服务器返回的验证数据: " + serverResponse);
		try
		{
			if (string.IsNullOrEmpty(serverResponse))
			{
				Debug.LogError("服务器响应为空，无法进行验证");
				SystemLanguage language = _language;
				string message = ((language != SystemLanguage.Chinese && language != SystemLanguage.ChineseSimplified) ? "Server response is empty" : "服务器响应为空");
				ShowLoginMessage(message);
				return;
			}
			try
			{
				JSONNode val = JSON.Parse(serverResponse);
				if (val != (object)null)
				{
					if (val.HasKey("content"))
					{
						JSONNode val2 = val["content"];
						if (val2 != (object)null && val2.HasKey("data"))
						{
							JSONNode val3 = val2["data"];
							if (val3 != (object)null && val3.HasKey("userId"))
							{
								string value = val3["userId"].Value;
								if (!string.IsNullOrEmpty(value))
								{
									UserId = value;
									Debug.Log("[登录流程] 成功提取并保存UserId: " + UserId);
									Debug.Log("[BnSdkManager] UserId已设置，当前值: " + UserId);
								}
								else
								{
									Debug.LogWarning("[登录流程] 服务器响应中userId字段为空");
								}
							}
							else
							{
								Debug.LogWarning("[登录流程] 服务器响应中content.data.userId路径不存在或userId字段缺失");
							}
						}
						else
						{
							Debug.LogWarning("[登录流程] 服务器响应中content.data路径不存在");
						}
					}
				}
				else
				{
					Debug.LogError("服务器响应JSON格式无效");
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("解析服务器响应JSON失败: " + ex.Message);
			}
			Debug.Log("[登录流程]成功接收到服务器的发送过来的UserId");
		}
		catch (Exception ex2)
		{
			Debug.LogError("处理服务器验证响应异常: " + ex2.Message);
			SystemLanguage language = _language;
			string message = ((language != SystemLanguage.Chinese && language != SystemLanguage.ChineseSimplified) ? "Failed to process server response" : "处理服务器响应失败");
			ShowLoginMessage(message);
		}
	}

	public void CheckTimeDifferenceOnReconnect(Action succ)
	{
		if (MonoSingletonProvider<NetManager>.inst.ServerTime >= _loginServerTimeStamp.AddHours(12.0))
		{
			ClearLoginInfo();
			SimpleSingletonProvider<GameManager>.inst.ReLogin().Forget();
			return;
		}
		if (LoginServiceHelper.connectToken != null)
		{
			LoginServiceHelper.connectToken.China.Sid = Sid;
		}
		succ?.Invoke();
	}

	private void ClearLoginInfo()
	{
		Sid = null;
		Extra = null;
		UserId = null;
	}

	public void OnLogout()
	{
		try
		{
			Sid = null;
			Extra = null;
			UserId = null;
			Debug.Log("[BnSdkManager] 已清除Sid、Extra和UserId");
			ClearLoginInfo();
			Debug.Log("[BnSdkManager] 已清除登录信息");
			GRoot.inst.touchable = false;
			Debug.Log("[BnSdkManager] 已禁用UI交互和ESC键");
			Debug.Log("[BnSdkManager] 调用 BnGameSdk.Instance.logout");
			((BnGameBase)BnGameSdk.Instance).logout((Action<int, string>)async delegate(int ret, string msg)
			{
				GRoot.inst.touchable = true;
				Debug.Log($"[BnSdkManager] SDK注销回调: ret={ret}, msg={msg}, UI交互已恢复: touchable={GRoot.inst.touchable}");
				if (ret == 1)
				{
					Debug.Log("[BnSdkManager] SDK注销成功");
					SystemLanguage language = _language;
					string message = ((language != SystemLanguage.Chinese && language != SystemLanguage.ChineseSimplified) ? "Account logged out" : "账号已注销");
					ShowLoginMessage(message);
					if (SimpleSingletonProvider<SceneManager>.inst.currentType.Value != SceneType.Login)
					{
						Debug.Log("[BnSdkManager] 当前不在登录场景，执行重新登录");
						await SimpleSingletonProvider<GameManager>.inst.ReLogin();
					}
					else
					{
						Debug.Log("[BnSdkManager] 当前在登录场景，重新调用SDK登录");
						LoginSDK();
					}
				}
				else
				{
					Debug.LogError($"[BnSdkManager] SDK注销失败: ret={ret}, msg={msg}");
					ClearLoginInfo();
					Debug.Log("[BnSdkManager] 注销失败，已清除登录信息");
					SystemLanguage language = _language;
					string message = ((language != SystemLanguage.Chinese && language != SystemLanguage.ChineseSimplified) ? ("Logout failed: " + msg) : ("注销失败：" + msg));
					ShowLoginMessage(message);
					if (SimpleSingletonProvider<UIManager>.inst.currentPanel is LoginPanel loginPanel)
					{
						loginPanel.ChangeSDKLoginStatus(status: false);
					}
				}
			});
		}
		catch (Exception ex)
		{
			Debug.LogError("[BnSdkManager] SDK注销异常: " + ex.Message + "\n" + ex.StackTrace);
		}
	}

	public void CheckText(string text, Action<int, string> callback)
	{
		try
		{
			callback?.Invoke(1, text);
		}
		catch (Exception ex)
		{
			Debug.LogError("[SDK] CheckText 调用异常: " + ex.Message);
			callback?.Invoke(0, text);
		}
	}

	public UniTask<CheckTextResult> CheckTextAsync(string text)
	{
		UniTaskCompletionSource<CheckTextResult> completionSource = new UniTaskCompletionSource<CheckTextResult>();
		if (Instance == null)
		{
			completionSource.TrySetResult(new CheckTextResult
			{
				passed = true,
				checkedText = text
			});
			return completionSource.Task;
		}
		CheckText(text, delegate(int code, string newText)
		{
			bool passed = code == 1;
			CheckTextResult result = new CheckTextResult
			{
				passed = passed,
				checkedText = (newText ?? text)
			};
			completionSource.TrySetResult(result);
		});
		return completionSource.Task;
	}

	public void ExitSdk()
	{
		try
		{
			((BnGameBase)BnGameSdk.Instance).exit();
			Debug.Log("BNSDK成功退出游戏");
		}
		catch (Exception ex)
		{
			Debug.LogError("[SDK] Exit 调用异常: " + ex.Message);
		}
	}

	public Dictionary<string, object> BuildOrderInfo(string cpOrderId, string roleId, string roleName, int roleLevel, int vipLevel, int serverId, string serverName, int amount, int productCount, string productName, string productType, string productId, string desc, int rate = 10, string notifyUrl = "http://8.211.158.75:10999/pay/callback")
	{
		try
		{
			if (amount <= 0)
			{
				Debug.LogError($"构建订单参数失败：金额amount必须为大于0的分值，当前amount={amount}。请检查调用方传入的金额。");
				return null;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["cp_order_id"] = cpOrderId;
			dictionary["role_id"] = roleId;
			dictionary["role_name"] = roleName;
			dictionary["role_level"] = roleLevel;
			dictionary["vip_level"] = vipLevel;
			dictionary["server_id"] = serverId;
			dictionary["server_name"] = serverName;
			dictionary["amount"] = amount;
			dictionary["product_count"] = productCount;
			dictionary["product_name"] = productName;
			dictionary["product_type"] = productType;
			dictionary["product_id"] = productId;
			dictionary["desc"] = desc;
			dictionary["rate"] = rate;
			if (!string.IsNullOrEmpty(notifyUrl))
			{
				dictionary["notify_url"] = notifyUrl;
			}
			Debug.Log("BnSdk订单参数构建完成: cpOrderId=" + cpOrderId + ", productName=" + productName + ", amount=" + amount);
			return dictionary;
		}
		catch (Exception ex)
		{
			Debug.LogError("构建订单参数异常: " + ex.Message + "\n" + ex.StackTrace);
			return null;
		}
	}

	public void Pay(Dictionary<string, object> orderInfo)
	{
		Pay(orderInfo, null);
	}

	public void Pay(Dictionary<string, object> orderInfo, Action<int, string> callback)
	{
		try
		{
			if (orderInfo == null || orderInfo.Count == 0)
			{
				Debug.LogError("[SDK] Pay 调用失败，orderInfo 为空");
				callback?.Invoke(0, "orderInfo is empty");
				return;
			}
			Debug.Log("[SDK] 调用支付接口");
			GRoot.inst.touchableAll = false;
			((BnGameBase)BnGameSdk.Instance).pay(orderInfo, (Action<int, string>)delegate(int ret, string msg)
			{
				switch (ret)
				{
				case 1:
					Debug.Log("[SDK] 支付成功");
					break;
				case 2:
					Debug.LogWarning("[SDK] 支付取消");
					break;
				default:
					Debug.LogError("[SDK] 支付失败: " + msg);
					break;
				}
				callback?.Invoke(ret, msg);
				this.PayCallback?.Invoke(ret, msg);
				GRoot.inst.touchableAll = true;
			});
		}
		catch (Exception ex)
		{
			GRoot.inst.touchableAll = true;
			Debug.LogError("[SDK] Pay 调用异常: " + ex.Message + "\n" + ex.StackTrace);
			callback?.Invoke(0, ex.Message);
			this.PayCallback?.Invoke(0, ex.Message);
		}
	}

	public void ReportCustomEvent(Dictionary<string, object> roleInfo, string eventName, Dictionary<string, object> eventValue = null)
	{
		try
		{
			if (string.IsNullOrEmpty(eventName))
			{
				Debug.LogError("[SDK] 自定义事件 eventName 为空");
				return;
			}
			Debug.Log("[SDK] 上报自定义事件: " + eventName);
			((BnGameBase)BnGameSdk.Instance).onCustomEvent(roleInfo, eventName, eventValue);
		}
		catch (Exception ex)
		{
			Debug.LogError("[SDK] ReportCustomEvent 调用异常: " + ex.Message + "\n" + ex.StackTrace);
		}
	}
}
