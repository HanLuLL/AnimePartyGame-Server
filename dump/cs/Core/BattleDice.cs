using System.Collections.Generic;
using UnityEngine;

namespace Core;

public class BattleDice
{
	private readonly Vector3 initPos = new Vector3(0f, 100000f, 0f);

	private GameObject _Prefab;

	private string _DiceName;

	private readonly List<GameObject> _DiceObjects;

	public BattleDice(GameObject prefab, string diceName)
	{
		_Prefab = prefab;
		_DiceName = diceName;
		_DiceObjects = new List<GameObject>(2);
	}

	public GameObject GetDice()
	{
		if (_Prefab == null)
		{
			Debug.LogError(_DiceName + "预制体不存在请检查");
			return null;
		}
		GameObject gameObject = _DiceObjects.Find((GameObject x) => !x.gameObject.activeSelf);
		if (gameObject == null)
		{
			if (_Prefab == null)
			{
				Debug.LogError(_DiceName + "预制体为空");
				return null;
			}
			gameObject = Object.Instantiate(_Prefab);
			if (gameObject == null)
			{
				Debug.LogError("实例化骰子" + _Prefab.name + "资源失败");
				return null;
			}
			_DiceObjects.Add(gameObject);
		}
		gameObject.gameObject.SetActive(value: true);
		gameObject.transform.localPosition = initPos;
		return gameObject;
	}

	public void StopDice(GameObject showDice)
	{
		if (!(showDice == null))
		{
			showDice.gameObject.SetActive(value: false);
		}
	}

	public void DestroyPool()
	{
		List<GameObject> diceObjects = _DiceObjects;
		if (diceObjects == null || diceObjects.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < _DiceObjects.Count; i++)
		{
			if (_DiceObjects[i] != null)
			{
				Object.Destroy(_DiceObjects[i]);
			}
		}
		_DiceObjects.Clear();
	}
}
