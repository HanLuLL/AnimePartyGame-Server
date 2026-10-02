using Cysharp.Threading.Tasks;

namespace SinglePlayer.GamePlay.Build;

public static class BuildingBaseExtension
{
	public static void ChangeHP(this BuildingBase buildingBase, CharacterType characterType, int changeHP, BuildingShowType buildingShowType)
	{
		Game.GetSystem<BoardManager>().characterManager.ChangeCharacterAttribute(new AttributeChangeInfo
		{
			Source = (type: AttributeChangeSource.Building, id: buildingBase.Id),
			ChangeHP = changeHP,
			BuildingShowType = buildingShowType
		});
	}

	public static void PlayChangeHP(this BuildingBase buildingBase, CharacterType characterType, int changeHP, BuildingShowType buildingShowType)
	{
		Game.GetSystem<BoardManager>().characterManager.PlayChangeCharacterAttribute(new AttributeChangeInfo
		{
			Source = (type: AttributeChangeSource.Building, id: buildingBase.Id),
			ChangeHP = changeHP,
			BuildingShowType = buildingShowType
		}).Forget();
	}

	public static void ChangeGold(this BuildingBase buildingBase, int gold, BuildingShowType buildingShowType)
	{
		AttributeChangeInfo attributeChangeInfo = new AttributeChangeInfo
		{
			Source = (type: AttributeChangeSource.Building, id: buildingBase.Id),
			ChangeGold = gold,
			ConfigId = buildingBase.Card.CardConfigure.Id,
			BuildingShowType = buildingShowType
		};
		Game.GetSystem<BoardManager>().characterManager.ChangeCharacterAttribute(attributeChangeInfo);
		if (buildingShowType == BuildingShowType.Pass)
		{
			Game.GetModel<GlobalSignal>().BuildingShow(attributeChangeInfo);
		}
	}

	public static void PlayChangeGold(this BuildingBase buildingBase, int gold, BuildingShowType buildingShowType)
	{
		Game.GetSystem<BoardManager>().characterManager.PlayChangeCharacterAttribute(new AttributeChangeInfo
		{
			Source = (type: AttributeChangeSource.Building, id: buildingBase.Id),
			ChangeGold = gold,
			ConfigId = buildingBase.Card.CardConfigure.Id,
			BuildingShowType = buildingShowType
		}).Forget();
	}

	public static void ChangeExp(this BuildingBase buildingBase, int targetId, int sourceId, int exp, BuildingShowType buildingShowType)
	{
		Game.GetSystem<BoardManager>().characterManager.ChangeCharacterAttribute(new AttributeChangeInfo
		{
			Target = (type: AttributeChangeTarget.Building, id: targetId),
			Source = (type: AttributeChangeSource.Building, id: sourceId),
			Exp = exp,
			BuildingShowType = buildingShowType
		});
	}

	public static void PlayChangeExp(this BuildingBase buildingBase, int targetId, int sourceId, int exp, BuildingShowType buildingShowType)
	{
		Game.GetSystem<BoardManager>().characterManager.PlayChangeCharacterAttribute(new AttributeChangeInfo
		{
			Target = (type: AttributeChangeTarget.Building, id: targetId),
			Source = (type: AttributeChangeSource.Building, id: sourceId),
			Exp = exp,
			BuildingShowType = buildingShowType
		}).Forget();
	}
}
