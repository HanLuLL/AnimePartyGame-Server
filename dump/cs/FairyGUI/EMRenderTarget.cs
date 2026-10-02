namespace FairyGUI;

public interface EMRenderTarget
{
	int EM_sortingOrder { get; }

	void EM_BeforeUpdate();

	void EM_Update(UpdateContext context);

	void EM_Reload();
}
