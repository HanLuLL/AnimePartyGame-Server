using Cysharp.Threading.Tasks;
using SinglePlayer.GamePlay.Character;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Map;

public class DiceLandComponent_Gold : DiceLandComponent
{
	public DiceLandComponent_Gold(int landId)
		: base(landId)
	{
		effectType = DiceLandEffectType.StepOnEffect;
	}

	public override UniTask PassByEffect()
	{
		Debug.Log("经过地图格：Gold");
		return UniTask.CompletedTask;
	}

	public override async UniTask StepOnEffect()
	{
		Debug.Log("踩中地图格：Gold");
		if (Game.GetSystem<BoardManager>().characterManager.Hero != null)
		{
			Land landById = Game.GetModel<GameData>().MapData.GetLandById(LandId);
			if (StaticConfigure.SinglePlayer.LandDict.TryGetValue((int)landById.LandType, out var value))
			{
				int safeByIndex = value.Params.GetSafeByIndex(0);
				await Game.GetSystem<BoardManager>().characterManager.PlayChangeCharacterAttribute(new AttributeChangeInfo
				{
					Source = (type: AttributeChangeSource.DiceLand, id: LandId),
					ChangeGold = safeByIndex
				});
			}
		}
	}

	public override async UniTask ShowAttributeChange(AttributeChangeInfo message)
	{
		Hero hero = Game.GetSystem<BoardManager>().characterManager.Hero;
		if (hero != null)
		{
			await hero.view.Play("SG_CharacterRoot_Cheer");
		}
	}
}
