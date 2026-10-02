using System.Collections.Generic;
using UnityEngine;

namespace SpriteDicing;

public class DicedSpriteAtlas : ScriptableObject
{
	[SerializeField]
	private List<Sprite> sprites = new List<Sprite>();

	[SerializeField]
	private List<Texture2D> textures = new List<Texture2D>();

	public IReadOnlyList<Sprite> Sprites => sprites;

	public IReadOnlyList<Texture2D> Textures => textures;

	public Sprite GetSprite(string spriteName)
	{
		return sprites.Find((Sprite s) => s.name == spriteName);
	}
}
