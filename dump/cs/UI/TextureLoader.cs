using System.Collections;
using FairyGUI;
using Tools;

namespace UI;

public class TextureLoader : GLoader
{
	protected override void LoadExternal()
	{
		MonoSingletonProvider<CoroutineManager>.inst.StartCoroutine(LoadTexture());
	}

	private IEnumerator LoadTexture()
	{
		yield return SimpleSingletonProvider<TextureManager>.inst.AsyncLoad(base.url, OnLoadCompleted, OnLoadFailed);
	}

	protected override void FreeExternal(NTexture nTexture)
	{
		nTexture.ReleaseRef();
	}

	private void OnLoadCompleted(NTexture nTexture)
	{
		if (!(base.image?.cachedTransform == null))
		{
			nTexture.AddRef();
			onExternalLoadSuccess(nTexture);
		}
	}

	private void OnLoadFailed()
	{
		onExternalLoadFailed();
	}
}
