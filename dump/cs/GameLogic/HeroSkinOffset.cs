using UnityEngine;

namespace GameLogic;

public class HeroSkinOffset
{
	public Vector2 skinOffset;

	public Vector2 bustSkinOffset;

	public float bustSkinScale;

	public bool reversalAxisX;

	public HeroSkinOffset(SkinOffsetConfigure _OffsetData)
	{
		skinOffset = new Vector2(_OffsetData.OffsetX, _OffsetData.OffsetY);
		bustSkinOffset = new Vector2(_OffsetData.CharacterStateSkinOffsetX, _OffsetData.CharacterStateSkinOffsetY);
		bustSkinScale = _OffsetData.CharacterStateSkinScale;
		reversalAxisX = _OffsetData.IsCharacterStateSkinFlipX;
	}
}
