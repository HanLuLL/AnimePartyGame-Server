using System.Collections.Generic;
using System.Linq;
using SinglePlayer.GamePlay.Character;
using UnityEngine;

namespace SinglePlayer.GamePlay;

public class BoardGameManager
{
	private readonly List<int> _dicePoints = new List<int>();

	public IReadOnlyList<int> DicePoints => _dicePoints;

	public void Initialize()
	{
	}

	public void Dispose()
	{
	}

	public void GenerateDicePoint()
	{
		HeroProperty heroProperty = Game.GetModel<GameData>().heroProperty;
		int num = ((heroProperty.DoubleDiceTime.Value <= 0) ? 1 : 2);
		_dicePoints.Clear();
		for (int i = 0; i < num; i++)
		{
			_dicePoints.Add(Random.Range(1, 7));
		}
		int forceFirstDicePoint = heroProperty.ForceFirstDicePoint;
		if (forceFirstDicePoint > 0 && _dicePoints.Count > 0)
		{
			_dicePoints[0] = forceFirstDicePoint;
			heroProperty.ClearForceFirstDicePoint();
		}
		DicePointCheating();
		Game.GetModel<GameData>().heroProperty.MovePoint = _dicePoints.Sum();
		Game.GetModel<GameData>().heroProperty.UpdateDoubleDiceTime(-1);
		Game.GetModel<GlobalSignal>().GenerateDicePoint.Dispatch(_dicePoints);
	}

	private void DicePointCheating()
	{
		int? num = Game.GetModel<GMData>()?.DicePoint;
		if (num.HasValue)
		{
			_dicePoints[0] = num.Value;
		}
	}
}
