using System.Collections.Generic;
using Google.Protobuf.Collections;
using Tools;

namespace GameLogic;

public class CommunicateExpressionPack
{
	public readonly int PackId;

	private readonly List<int> expressionList = new List<int>();

	private bool isDirtyOrder;

	public int StarId;

	public readonly int PageExpressionId;

	public CommunicateExpressionPack(int packId)
	{
		PackId = packId;
		if (StaticConfigure.Character.ExpressionPackDict.TryGetValue(PackId, out var value))
		{
			RepeatedField<CharacterExpressionPackConfigureItem> characterExpressionPackConfigureItems = value.CharacterExpressionPackConfigureItems;
			PageExpressionId = characterExpressionPackConfigureItems.GetSafeByIndex(0)?.ItemID ?? 0;
		}
	}

	public void AddExpression(int id)
	{
		if (!expressionList.Contains(id))
		{
			expressionList.Add(id);
			isDirtyOrder = true;
		}
	}

	public List<int> GetExpressionList()
	{
		if (isDirtyOrder)
		{
			isDirtyOrder = false;
			expressionList.Sort();
		}
		return expressionList;
	}

	public void UpdateExpressionNoop(List<int> expressionIds)
	{
		expressionList.Clear();
		expressionList.AddRange(expressionIds);
	}
}
