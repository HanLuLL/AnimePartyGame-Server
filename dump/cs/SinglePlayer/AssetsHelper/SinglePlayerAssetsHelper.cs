using Core;
using Cysharp.Threading.Tasks;
using Tools;

namespace SinglePlayer.AssetsHelper;

public class SinglePlayerAssetsHelper : ISystem, IInitialize, IDispose
{
	public readonly CharacterAssetManager characterAssetManager = new CharacterAssetManager();

	public readonly BuildingAssetManager buildingAssetManager = new BuildingAssetManager();

	public readonly DesignAssetManager designAssetManager = new DesignAssetManager();

	public async UniTask Initialize()
	{
		await buildingAssetManager.Initialize();
		await characterAssetManager.Initialize();
	}

	public void Dispose()
	{
		characterAssetManager.Dispose();
		buildingAssetManager.Dispose();
		designAssetManager.Dispose();
		if (SimpleSingletonProvider<EffectManager>.hasInstance)
		{
			SimpleSingletonProvider<EffectManager>.inst.Dispose();
		}
	}
}
