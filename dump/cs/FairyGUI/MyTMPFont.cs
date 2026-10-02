using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.AddressableAssets;

namespace FairyGUI;

public class MyTMPFont : TMPFont
{
	public MyTMPFont(string fontName)
	{
		name = fontName;
	}

	public async UniTask AsyncLoad(string key)
	{
		base.fontAsset = await Addressables.LoadAssetAsync<TMP_FontAsset>(key);
	}

	public override void Dispose()
	{
		base.Dispose();
		Addressables.Release<TMP_FontAsset>(base.fontAsset);
	}
}
