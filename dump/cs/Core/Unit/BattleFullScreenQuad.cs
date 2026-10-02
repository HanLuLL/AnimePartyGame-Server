using CriWare;
using CriWare.Assets;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using Tools;
using UI;
using UnityEngine;

namespace Core.Unit;

public class BattleFullScreenQuad : Unit
{
	private CriManaMovieControllerForAsset controller;

	private UnityEngine.Camera pkMainCamera;

	private MeshRenderer meshRenderer;

	private bool _keepLastFrame;

	private bool _fullScreen;

	private float scaleRatio;

	private float _movieWidth;

	private float _movieHeight;

	private float _trackSpeed;

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

	public async UniTask TryPlayVideo(string videoKey, int sortingOrder, float ScaleRatio = 1f, float speed = 1f, Vector2 offset = default(Vector2), bool keepLastFrame = false, bool fullScreen = true, float movieWidth = 0f, float movieHeight = 0f)
	{
		_keepLastFrame = keepLastFrame;
		scaleRatio = ScaleRatio;
		_fullScreen = fullScreen;
		_movieWidth = movieWidth;
		_movieHeight = movieHeight;
		_trackSpeed = speed;
		UpdateQuadPosition(offset);
		if (meshRenderer != null)
		{
			meshRenderer.sortingOrder = sortingOrder;
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
		((CriManaMovieMaterialBase)controller).player.SetSpeed(_trackSpeed);
		CriManaPlayerExtentionForAsset.SetAsset(((CriManaMovieMaterialBase)controller).player, val, (SetMode)0);
		((CriManaMovieMaterialBase)controller).player.Start();
	}

	private void UpdateQuadPosition(Vector2 offset = default(Vector2))
	{
		PKCameraManager pKCameraManager = Object.FindAnyObjectByType<PKCameraManager>();
		if (pKCameraManager != null)
		{
			pkMainCamera = pKCameraManager.PKMainCamera;
		}
		base.transform.position = new Vector3(pkMainCamera.transform.position.x + offset.x, pkMainCamera.transform.position.y + offset.y, 1.5f);
	}

	private void Update()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Invalid comparison between Unknown and I4
		if (pkMainCamera != null && (Object)(object)controller != null)
		{
			if (!_keepLastFrame)
			{
				Player player = ((CriManaMovieMaterialBase)controller).player;
				if (player == null || (int)player.status != 5)
				{
					goto IL_00af;
				}
			}
			if (_fullScreen)
			{
				float num = pkMainCamera.orthographicSize * 2f;
				float x = num * pkMainCamera.aspect;
				base.transform.localScale = new Vector3(x, num, 1f) * scaleRatio;
			}
			else
			{
				base.transform.localScale = new Vector3(_movieWidth, _movieHeight, 1f);
			}
			return;
		}
		goto IL_00af;
		IL_00af:
		base.transform.localScale = Vector3.zero;
	}

	protected override void OnDestroy()
	{
		base.transform.localScale = Vector3.zero;
		if ((Object)(object)controller != null)
		{
			((CriManaMovieMaterialBase)controller).Stop();
			if (((CriManaMovieMaterialBase)controller).material != null)
			{
				Object.Destroy(((CriManaMovieMaterialBase)controller).material);
			}
			Object.Destroy((Object)(object)controller);
		}
	}

	public void UpdateVideoSpeed(float trackSpeed)
	{
		if (!Mathf.Approximately(_trackSpeed, trackSpeed) && !((Object)(object)controller == null) && ((CriManaMovieMaterialBase)controller).player != null)
		{
			_trackSpeed = trackSpeed;
			((CriManaMovieMaterialBase)controller).player.SetSpeed(_trackSpeed);
		}
	}
}
