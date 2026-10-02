using FairyGUI;

public static class ErasureMaskerUtils
{
	public static void Create(GLoader loader)
	{
		if (!loader.displayObject.gameObject.TryGetComponent<ErasureMasker>(out var component))
		{
			component = loader.displayObject.gameObject.AddComponent<ErasureMasker>();
			component.Init(loader);
		}
	}
}
