using System;
using System.Collections.Generic;
using GameLogic;
using Tools;
using UnityEngine;

namespace Core;

[Serializable]
public class GachaManager : MonoBehaviour
{
	public static GachaManager inst;

	private GachaDiceManager gachaDiceManager;

	[SerializeField]
	private GameObject vCamera;

	[SerializeField]
	private GameObject environment;

	private void Awake()
	{
		inst = this;
		gachaDiceManager = GetComponentInChildren<GachaDiceManager>();
	}

	public void ActiveGacha()
	{
		vCamera.SetActiveEx(active: true);
		environment.SetActiveEx(active: true);
		List<GachaItem> itemList = SimpleSingletonProvider<GameLogicManager>.inst.gacha.itemList;
		List<DiceResult> list = new List<DiceResult>(itemList.Count);
		foreach (GachaItem item in itemList)
		{
			list.Add(GetGachaDiceResult(item.itemInfo.QualityType));
		}
		gachaDiceManager.ReadyGachaDice(list.Count, list);
	}

	public void CloseGachaProcess()
	{
		Release();
		vCamera.SetActiveEx(active: false);
		environment.SetActiveEx(active: false);
	}

	public void ThrowDice()
	{
		gachaDiceManager.ThrowDice();
	}

	private DiceResult GetGachaDiceResult(QualityType QualityType)
	{
		return QualityType switch
		{
			QualityType.Blue => DiceResult.FACE_SIX, 
			QualityType.Purple => DiceResult.FACE_ONE, 
			QualityType.Orange => DiceResult.FACE_TWO, 
			_ => DiceResult.FACE_SIX, 
		};
	}

	public void CancelThrowDice()
	{
		gachaDiceManager.CancelThrowDice();
	}

	public void Release()
	{
		gachaDiceManager.ReleaseDice();
	}

	public void Dispose()
	{
		gachaDiceManager.DisposeDice();
	}
}
