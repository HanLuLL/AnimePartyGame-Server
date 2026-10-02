using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core;

public class TestGacha : MonoBehaviour
{
	[SerializeField]
	private List<DiceResult> diceResult;

	private GachaDiceManager _gachaDiceManager;

	private void Awake()
	{
		_gachaDiceManager = GetComponentInChildren<GachaDiceManager>();
	}

	private void OnGUI()
	{
		if (GUILayout.Button("单抽", Array.Empty<GUILayoutOption>()))
		{
			_gachaDiceManager.CancelThrowDice();
			_gachaDiceManager.ReadyGachaDice(1, diceResult);
			_gachaDiceManager.ThrowDice();
		}
		if (GUILayout.Button("10连", Array.Empty<GUILayoutOption>()))
		{
			_gachaDiceManager.CancelThrowDice();
			_gachaDiceManager.ReadyGachaDice(10, diceResult);
			_gachaDiceManager.ThrowDice();
		}
	}
}
