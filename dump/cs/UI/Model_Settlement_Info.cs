using System.Collections.Generic;
using SinglePlayer;
using SinglePlayer.GamePlay;
using UnityEngine;

namespace UI;

public class Model_Settlement_Info : BaseModel<UISettlement_Com_Info>
{
	public Model_Settlement_Info(UISettlement_Com_Info _com)
		: base(_com)
	{
	}

	public override void Refresh()
	{
		base.Refresh();
		RefreshScore();
		RefreshCoin();
	}

	private void RefreshScore()
	{
		int[] array = Game.GetModel<GameData>().heroProperty.CalculateSubScores();
		com.btn_score_1.txt_score.text = $"{array[0]}";
		com.btn_score_2.txt_score.text = $"{array[1]}";
		com.btn_score_3.txt_score.text = $"{array[2]}";
	}

	private void RefreshCoin()
	{
		GameData model = Game.GetModel<GameData>();
		Dictionary<int, int> buildingGlodDic = model.heroProperty.BuildingGlodDic;
		Dictionary<int, int> relicGlodDic = model.heroProperty.RelicGlodDic;
		int count = buildingGlodDic.Count;
		int count2 = relicGlodDic.Count;
		com.list_coin.numItems = count + count2;
		int num = 0;
		List<Vector3Int> list = new List<Vector3Int>(buildingGlodDic.Count + relicGlodDic.Count);
		foreach (KeyValuePair<int, int> item in buildingGlodDic)
		{
			list.Add(new Vector3Int(item.Key, item.Value, 0));
		}
		foreach (KeyValuePair<int, int> item2 in relicGlodDic)
		{
			list.Add(new Vector3Int(item2.Key, item2.Value, 1));
		}
		list.Sort((Vector3Int x, Vector3Int y) => (x.y <= y.y) ? 1 : (-1));
		for (int num2 = 0; num2 < list.Count; num2++)
		{
			Vector3Int vector3Int = list[num2];
			UISettlement_Btn_Coin uISettlement_Btn_Coin = com.list_coin.GetChildAt(num) as UISettlement_Btn_Coin;
			num++;
			if (uISettlement_Btn_Coin == null)
			{
				continue;
			}
			SinglePlayerRelicConfigure value2;
			if (vector3Int.z == 0)
			{
				if (StaticConfigure.SinglePlayer.CardDict.TryGetValue(vector3Int.x, out var value))
				{
					uISettlement_Btn_Coin.title = value.NameID.GetLocal(UIStringType.SinglePlayer);
					uISettlement_Btn_Coin.txt_gold.text = $"{vector3Int.y}";
					uISettlement_Btn_Coin.icon = value.Icon;
				}
			}
			else if (StaticConfigure.SinglePlayer.RelicDict.TryGetValue(vector3Int.x, out value2))
			{
				uISettlement_Btn_Coin.title = value2.NameID.GetLocal(UIStringType.SinglePlayer);
				uISettlement_Btn_Coin.txt_gold.text = $"{vector3Int.y}";
				uISettlement_Btn_Coin.icon = value2.Icon;
			}
		}
	}
}
