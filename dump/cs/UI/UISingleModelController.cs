using FairyGUI;
using UnityEngine;

namespace UI;

public class UISingleModelController : MonoBehaviour
{
	[SerializeField]
	private Transform diceGroupRoot;

	[SerializeField]
	private int width = 800;

	[SerializeField]
	private int height = 800;

	[SerializeField]
	private Camera viewCamera;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private RenderTexture renderTexture;

	private NTexture nTexture;

	public int Width => Mathf.Clamp(width, 1, 2048);

	public int Height => Mathf.Clamp(height, 1, 2048);

	public RenderTexture RenderTexture => renderTexture;

	public NTexture NTexture
	{
		get
		{
			if (nTexture == null && renderTexture != null)
			{
				nTexture = new NTexture(renderTexture);
				nTexture.destroyMethod = DestroyMethod.ReleaseTemp;
			}
			return nTexture;
		}
	}

	public void OnEnable()
	{
		if (renderTexture == null && viewCamera != null)
		{
			RenderTextureDescriptor desc = new RenderTextureDescriptor(Width, Height, RenderTextureFormat.ARGB32, 16);
			desc.sRGB = true;
			renderTexture = RenderTexture.GetTemporary(desc);
			viewCamera.targetTexture = renderTexture;
		}
	}

	public void OnDisable()
	{
		if (renderTexture != null)
		{
			if (nTexture != null)
			{
				nTexture.destroyMethod = DestroyMethod.ReleaseTemp;
				nTexture.Unload();
				nTexture = null;
			}
			else
			{
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = null;
			}
		}
	}

	public void PlayDefalutAnimation(int point)
	{
		animator.Play("defalut", 0, 0f);
		for (int i = 0; i < diceGroupRoot.childCount; i++)
		{
			diceGroupRoot.GetChild(i).gameObject.SetActive(point - 1 == i);
		}
	}

	public void DestroySelf()
	{
		Object.Destroy(base.gameObject);
	}
}
