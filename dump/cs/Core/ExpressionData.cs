using CriWare.Assets;
using Cysharp.Threading.Tasks;
using Tools;
using UI;

namespace Core;

public class ExpressionData
{
	private int expressionHeroId;

	public bool isVideo;

	public string textureUrl = "";

	public CriManaUsmAsset videoClip;

	public int expressionItemID => expressionConfig.ItemID;

	public CharacterExpressionPackConfigureItem expressionConfig { get; private set; }

	public ExpressionData(int configId, CharacterExpressionPackConfigureItem expressionConfigItem)
	{
		expressionHeroId = configId;
		expressionConfig = expressionConfigItem;
	}

	public async UniTask LoadResource()
	{
		string videoKey = expressionConfig.VideoKey;
		if (!string.IsNullOrEmpty(videoKey))
		{
			isVideo = true;
			videoClip = await SimpleSingletonProvider<CriMovieManager>.inst.Load(videoKey);
		}
		else
		{
			isVideo = false;
			ItemInfoConfigure itemInfoConfigure = expressionConfig.ItemID.GetItemInfoConfigure();
			textureUrl = ((itemInfoConfigure != null) ? itemInfoConfigure.ShowIcon : "");
		}
	}

	public void Dispose()
	{
	}
}
