using System;
using System.Collections.Generic;
using Core.Net;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core;

public static class LocalCache
{
	[Serializable]
	private class TutorialLocalCache
	{
		public long PlayerId;

		public List<int> FinishedGuideIds = new List<int>();
	}

	private static readonly List<int> _StoreOldGoods = new List<int>();

	private static readonly List<int> _ActivityIds = new List<int>();

	private static readonly List<int> _surveyCacheIds = new List<int>();

	private static TutorialLocalCache _tutorialLocalData;

	public static List<int> StoreOldGoods
	{
		get
		{
			if (_StoreOldGoods.Count == 0)
			{
				string storeGoodsCache = GetStoreGoodsCache();
				if (!string.IsNullOrWhiteSpace(storeGoodsCache))
				{
					string[] array = storeGoodsCache.Split("|");
					for (int i = 0; i < array.Length; i++)
					{
						if (int.TryParse(array[i], out var result))
						{
							_StoreOldGoods.Add(result);
						}
					}
				}
			}
			return _StoreOldGoods;
		}
	}

	public static List<int> ActivityIds
	{
		get
		{
			if (_ActivityIds.Count == 0)
			{
				string activityDoubleStatusCache = GetActivityDoubleStatusCache();
				if (!string.IsNullOrWhiteSpace(activityDoubleStatusCache))
				{
					string[] array = activityDoubleStatusCache.Split("|");
					for (int i = 0; i < array.Length; i++)
					{
						if (int.TryParse(array[i], out var result))
						{
							_ActivityIds.Add(result);
						}
					}
				}
			}
			return _ActivityIds;
		}
	}

	public static void UpdateStoreGoodsCache(List<int> goodsData)
	{
		_StoreOldGoods.AddRange(goodsData);
		string value = string.Join("|", _StoreOldGoods.ToArray());
		ES3.Save(LocalStore.StoreGoodsCache, value);
	}

	public static string GetStoreGoodsCache()
	{
		return ES3.LoadString(LocalStore.StoreGoodsCache, "");
	}

	public static void UpdateActivityDoubleStatusCache(int activityId)
	{
		if (!_ActivityIds.Contains(activityId))
		{
			_ActivityIds.Add(activityId);
			string value = string.Join("|", _ActivityIds.ToArray());
			ES3.Save(LocalStore.ActivityDoubleStatusCache, value);
		}
	}

	public static string GetActivityDoubleStatusCache()
	{
		return ES3.LoadString(LocalStore.ActivityDoubleStatusCache, "");
	}

	public static void UpdateComebackTips(long endtime)
	{
		ES3.Save(LocalStore.ComebackEndTimeCache, endtime);
	}

	public static long GetComebackEndTime()
	{
		return ES3.Load(LocalStore.ComebackEndTimeCache, 0L);
	}

	public static void UpdateHeroSortCache(int sortIndex)
	{
		ES3.Save(LocalStore.HeroSortCache, sortIndex);
	}

	public static int GetHeroSortCache()
	{
		return Mathf.Clamp(ES3.Load(LocalStore.HeroSortCache, 0), 0, 2);
	}

	public static void UpdateHeroSortOrderCache(int sortOrder)
	{
		ES3.Save(LocalStore.HeroSortOrderCache, sortOrder);
	}

	public static int GetHeroSortOrderCache()
	{
		return Mathf.Clamp(ES3.Load(LocalStore.HeroSortOrderCache, 0), 0, 1);
	}

	public static void UpdateUserAgreeState(bool state)
	{
		ES3.Save(LocalStore.userAgreeLabel, state);
	}

	public static bool GetUserState()
	{
		return ES3.Load(LocalStore.userAgreeLabel, defaultValue: false);
	}

	public static void UpdateUserAgreeVersion(string version)
	{
		ES3.Save(LocalStore.userAgreeVersionLabel, version);
	}

	public static string GetUserAgreeVersionState()
	{
		return ES3.LoadString(LocalStore.userAgreeVersionLabel, "");
	}

	public static void UpdatePrivacyPolicysVersion(string version)
	{
		ES3.Save(LocalStore.privacyPolicysVersionLabel, version);
	}

	public static string GetPrivacyPolicysVersion()
	{
		return ES3.LoadString(LocalStore.privacyPolicysVersionLabel, "");
	}

	public static void UpdatePVEDifficulty(int index, bool status)
	{
		int pVEDifficulty = GetPVEDifficulty();
		pVEDifficulty = ((!status) ? (pVEDifficulty & ~(1 << index)) : (pVEDifficulty | (1 << index)));
		ES3.Save(LocalStore.PVEDifficulty, pVEDifficulty);
	}

	public static int GetPVEDifficulty()
	{
		return ES3.Load(LocalStore.PVEDifficulty, 0);
	}

	public static bool GetPVEDifficultyStatusByIndex(int difficulty)
	{
		return (GetPVEDifficulty() & (1 << difficulty)) != 0;
	}

	public static List<int> GetSelectedPVEDifficulty()
	{
		List<int> validDifficultyConfig = SimpleSingletonProvider<GameLogicManager>.inst.roomList.GetValidDifficultyConfig();
		for (int num = validDifficultyConfig.Count - 1; num >= 0; num--)
		{
			if (!GetPVEDifficultyStatusByIndex(validDifficultyConfig[num]))
			{
				validDifficultyConfig.RemoveAt(num);
			}
		}
		return validDifficultyConfig;
	}

	private static void UpdateTimeCache(bool check, string key)
	{
		if (check)
		{
			ES3.Save(key, MonoSingletonProvider<NetManager>.inst.ServerTime.DateTimeToStampForSeconds());
		}
		else
		{
			ES3.DeleteKey(key);
		}
	}

	private static bool CheckTimeCache(string key)
	{
		int num = ES3.Load(key, 0);
		if (num == 0)
		{
			return true;
		}
		if (MonoSingletonProvider<NetManager>.inst.ServerTime > num.StampToDateTime().Date.AddDays(1.0))
		{
			return true;
		}
		return false;
	}

	public static void UpdateGachaSkinTipStatus(string key, bool check)
	{
		UpdateTimeCache(check, key);
	}

	public static void UpdateGachaTipsStatus(bool check)
	{
		UpdateTimeCache(check, LocalStore.GachaCostTip);
	}

	public static bool GetGachaTipsStatus()
	{
		return CheckTimeCache(LocalStore.GachaCostTip);
	}

	public static bool GetGachaSkinRewardTipStatus()
	{
		return CheckTimeCache(LocalStore.GachaSkinRewardTip);
	}

	public static bool GetGachaSkinFinishTipStatus()
	{
		return CheckTimeCache(LocalStore.GachaSkinFinishTip);
	}

	public static string GetSurveyRedStatus()
	{
		return ES3.LoadString(LocalStore.SurveyCache, "");
	}

	public static bool GetSurveyRedStatusByIndex(int surveyId)
	{
		if (_surveyCacheIds.Count != 0 && _surveyCacheIds.Contains(surveyId))
		{
			return true;
		}
		string surveyRedStatus = GetSurveyRedStatus();
		if (string.IsNullOrEmpty(surveyRedStatus))
		{
			return false;
		}
		string[] array = surveyRedStatus.Split("|");
		if (array == null || array.Length == 0)
		{
			return false;
		}
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			int item = int.Parse(array2[i]);
			if (!_surveyCacheIds.Contains(item))
			{
				_surveyCacheIds.Add(item);
			}
		}
		return _surveyCacheIds.Contains(surveyId);
	}

	public static void AddSurveyRedStatus(int surveyId)
	{
		if (!_surveyCacheIds.Contains(surveyId) && !GetSurveyRedStatusByIndex(surveyId))
		{
			string surveyRedStatus = GetSurveyRedStatus();
			surveyRedStatus = ((!string.IsNullOrEmpty(surveyRedStatus)) ? (surveyRedStatus + $"|{surveyId}") : surveyId.ToString());
			_surveyCacheIds.Add(surveyId);
			ES3.Save(LocalStore.SurveyCache, surveyRedStatus);
		}
	}

	private static TutorialLocalCache LoadTutorialCache()
	{
		string text = ES3.LoadString(LocalStore.TutorialCache, string.Empty);
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		try
		{
			return JsonUtility.FromJson<TutorialLocalCache>(text);
		}
		catch
		{
			Debug.LogError("教程 本地缓存解析失败");
			return null;
		}
	}

	public static bool IsFinishTutorial(int guideId)
	{
		long num = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo()?.Id ?? 0;
		if (_tutorialLocalData == null || _tutorialLocalData.PlayerId != num)
		{
			_tutorialLocalData = LoadTutorialCache();
		}
		if (_tutorialLocalData == null)
		{
			return false;
		}
		return _tutorialLocalData.FinishedGuideIds.Contains(guideId);
	}

	public static void UpdateTutorialCache(int guideType, int step)
	{
		long playerId = SimpleSingletonProvider<GameLogicManager>.inst.account.GetPlayerInfo()?.Id ?? 0;
		if (_tutorialLocalData == null)
		{
			_tutorialLocalData = new TutorialLocalCache
			{
				PlayerId = playerId
			};
		}
		if (!_tutorialLocalData.FinishedGuideIds.Contains(guideType))
		{
			_tutorialLocalData.FinishedGuideIds.Add(guideType);
		}
		string value = JsonUtility.ToJson(_tutorialLocalData);
		ES3.Save(LocalStore.TutorialCache, value);
	}
}
