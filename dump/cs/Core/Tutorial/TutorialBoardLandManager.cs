using System.Collections.Generic;

namespace Core.Tutorial;

public class TutorialBoardLandManager
{
	private readonly LandType[] LandTypes = new LandType[11]
	{
		LandType.Born,
		LandType.FillingStation,
		LandType.RollGold,
		LandType.Event,
		LandType.MoveAgain,
		LandType.Pveshop,
		LandType.Heal,
		LandType.MonsterPursuit,
		LandType.DrawCard,
		LandType.Portal,
		LandType.BloodLoss
	};

	private readonly Dictionary<int, BaseTutorialLand> Lands = new Dictionary<int, BaseTutorialLand>();

	public void Initialize()
	{
		for (int i = 0; i < LandTypes.Length; i++)
		{
			BaseTutorialLand classInstance = ReflectionHelper.GetClassInstance<BaseTutorialLand>("Core.Tutorial.TutorialLand" + LandTypes[i]);
			if (classInstance != null)
			{
				Lands.Add((int)LandTypes[i], classInstance);
			}
		}
	}

	public void Dispose()
	{
	}

	public BaseTutorialLand GetLandByType(LandType type)
	{
		return Lands.GetValueOrDefault((int)type);
	}
}
