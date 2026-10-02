using CriWare;
using CriWare.Assets;
using CriWare.CriMana;

namespace UI;

public class CriManaMovieControllerForGGraph : CriManaMovieControllerForAsset
{
	public string CurrentVideoKey;

	protected override void Awake()
	{
	}

	public void SetAsset(string key, CriManaUsmAsset asset)
	{
		CurrentVideoKey = key;
		if (((CriManaMovieMaterialBase)this).player == null)
		{
			((CriManaMovieMaterialBase)this).PlayerManualInitialize();
		}
		CriManaPlayerExtentionForAsset.SetAsset(((CriManaMovieMaterialBase)this).player, asset, (SetMode)0);
	}
}
