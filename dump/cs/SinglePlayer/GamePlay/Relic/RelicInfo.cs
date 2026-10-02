using Cysharp.Threading.Tasks;
using Tools;

namespace SinglePlayer.GamePlay.Relic;

public class RelicInfo
{
	public RelicBuff BuffData;

	public RelicInfo(int id)
	{
		BuffData = ReflectionHelper.GetClassInstance<RelicBuff>($"SinglePlayer.GamePlay.Relic.RelicInfo_{id}");
		BuffData?.Initialize(id);
	}

	public void ChangeGold(int value)
	{
		Game.GetSystem<BoardManager>().characterManager.PlayChangeCharacterAttribute(new AttributeChangeInfo
		{
			Source = (type: AttributeChangeSource.Relic, id: BuffData.Id),
			ConfigId = BuffData.Id,
			ChangeGold = value
		}).Forget();
	}

	public int GetRelicParam(int index)
	{
		return BuffData.Config.RelicParam.GetSafeByIndex(index);
	}
}
