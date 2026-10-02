using CriWare;
using CriWare.Assets;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;

namespace Core.Unit;

public class BattleCustomVideoQuad : Unit
{
	[SerializeField]
	private int videoConfigId;

	[SerializeField]
	private float speed = 1f;

	[SerializeField]
	private int sortingOrder;

	private string cacheViedoKey;

	private CriManaMovieControllerForAsset controller;

	private UnityEngine.Camera _pkMainCamera;

	private MeshRenderer meshRenderer;

	private UnityEngine.Camera pkMainCamera
	{
		get
		{
			if (_pkMainCamera == null)
			{
				PKCameraManager pKCameraManager = Object.FindAnyObjectByType<PKCameraManager>();
				if (pKCameraManager != null)
				{
					_pkMainCamera = pKCameraManager.PKMainCamera;
				}
			}
			return _pkMainCamera;
		}
	}

	protected override void Awake()
	{
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		MeshFilter meshFilter = base.gameObject.AddComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[4]
		{
			new Vector3(-0.5f, -0.5f, 0f),
			new Vector3(0.5f, -0.5f, 0f),
			new Vector3(-0.5f, 0.5f, 0f),
			new Vector3(0.5f, 0.5f, 0f)
		};
		mesh.vertices = vertices;
		int[] triangles = new int[6] { 0, 2, 1, 2, 3, 1 };
		mesh.triangles = triangles;
		Vector2[] uv = new Vector2[4]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		mesh.uv = uv;
		meshFilter.mesh = mesh;
		if (!base.gameObject.TryGetComponent<CriManaMovieControllerForAsset>(out controller))
		{
			controller = base.gameObject.AddComponent<CriManaMovieControllerForAsset>();
		}
		if (((CriManaMovieMaterialBase)controller).player == null)
		{
			((CriManaMovieMaterialBase)controller).PlayerManualInitialize();
		}
		if (((CriManaMovieMaterialBase)controller).material == null)
		{
			((CriManaMovieMaterialBase)controller).material = new Material(CriMovieManager.CriMaterial);
		}
		if (controller.Target == null)
		{
			controller.Target = (ICriManaMovieMaterialTarget)(object)new ManaMovieMaterialRendererTarget((Renderer)meshRenderer);
			meshRenderer.material = ((CriManaMovieMaterialBase)controller).material;
		}
		((CriManaMovieMaterialBase)controller).renderMode = (RenderMode)0;
		((CriManaMovieMaterialBase)controller).maxFrameDrop = (MaxFrameDrop)0;
	}

	private void Clear()
	{
		if ((Object)(object)controller != null)
		{
			((CriManaMovieMaterialBase)controller).Stop();
			if (((CriManaMovieMaterialBase)controller).material != null)
			{
				Object.Destroy(((CriManaMovieMaterialBase)controller).material);
			}
			Object.Destroy((Object)(object)controller);
		}
		cacheViedoKey = null;
	}

	private void OnEnable()
	{
		TryPlay().Forget();
	}

	public async UniTask TryPlay()
	{
		string videoKey = videoConfigId.GetVideoKey();
		if ((bool)meshRenderer)
		{
			meshRenderer.sortingOrder = sortingOrder;
		}
		if (!(cacheViedoKey != videoKey))
		{
			return;
		}
		if ((int)((CriManaMovieMaterialBase)controller).player.status != 0)
		{
			((CriManaMovieMaterialBase)controller).Stop();
			await UniTask.WaitUntil(() => (Object)(object)controller == null || ((CriManaMovieMaterialBase)controller).player == null || (int)((CriManaMovieMaterialBase)controller).player.status == 0);
			if ((Object)(object)controller == null || ((CriManaMovieMaterialBase)controller).player == null)
			{
				return;
			}
		}
		CriManaUsmAsset val = await SimpleSingletonProvider<CriMovieManager>.inst.Load(videoKey);
		((CriManaMovieMaterialBase)controller).player.SetSpeed(speed);
		CriManaPlayerExtentionForAsset.SetAsset(((CriManaMovieMaterialBase)controller).player, val, (SetMode)0);
		((CriManaMovieMaterialBase)controller).player.Start();
		((CriManaMovieMaterialBase)controller).player.uiRenderMode = true;
		cacheViedoKey = videoKey;
	}

	private void Update()
	{
	}

	protected override void OnDestroy()
	{
		Clear();
	}
}
