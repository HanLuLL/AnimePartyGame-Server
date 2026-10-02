using Core;
using Cysharp.Threading.Tasks;
using SinglePlayer.Scene;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Map;

public class DiceLandComponent_Card : DiceLandComponent
{
	public DiceLandComponent_Card(int landId)
		: base(landId)
	{
		effectType = DiceLandEffectType.StepOnEffect;
	}

	public override UniTask PassByEffect()
	{
		Debug.Log("经过地图格：Building");
		return UniTask.CompletedTask;
	}

	public override async UniTask StepOnEffect()
	{
		Debug.Log("踩中地图格：Gold");
		SimpleSingletonProvider<AudioManager>.inst.SendEvent(10, Game.GetController<SinglePlayerSceneController>().gameObject);
		await Game.GetSystem<BoardManager>().characterManager.PlayChangeCharacterAttribute(new AttributeChangeInfo
		{
			Source = (type: AttributeChangeSource.DiceLand, id: LandId)
		});
	}

	public override async UniTask ShowAttributeChange(AttributeChangeInfo message)
	{
		Land landById = Game.GetModel<GameData>().MapData.GetLandById(LandId);
		if (StaticConfigure.SinglePlayer.LandDict.TryGetValue((int)landById.LandType, out var value))
		{
			int safeByIndex = value.Params.GetSafeByIndex(0);
			BoardCardManager cardManager = Game.GetSystem<BoardManager>().cardManager;
			int safeByIndex2 = cardManager.RandomGetCards(safeByIndex).GetSafeByIndex(0);
			int t = cardManager.AddCardToBag(safeByIndex2);
			Game.GetModel<GlobalSignal>().GetCardPerformance.Dispatch(t);
		}
		await Game.GetSystem<BoardManager>().characterManager.Hero.view.Play("SG_CharacterRoot_Cheer");
	}
}
