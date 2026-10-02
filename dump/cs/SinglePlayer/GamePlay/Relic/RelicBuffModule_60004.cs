using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Relic;

public class RelicBuffModule_60004 : BaseRelicBuffModule
{
	public override void Apply(RelicInfo info)
	{
		float num = (float)info.BuffData.Config.RelicParam.GetSafeByIndex(0) * 0.01f;
		int a = Mathf.CeilToInt((float)Game.GetModel<GameData>().heroProperty.Gold.Value * num);
		a = Mathf.Max(a, 1);
		Game.GetSystem<BoardManager>().characterManager.PlayChangeCharacterAttribute(new AttributeChangeInfo
		{
			Source = (type: AttributeChangeSource.Relic, id: info.BuffData.Id),
			ConfigId = info.BuffData.Id,
			ChangeGold = a
		}).Forget();
	}
}
