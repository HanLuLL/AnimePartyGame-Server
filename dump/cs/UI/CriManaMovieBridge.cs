using System.Collections.Generic;
using CriWare;
using CriWare.Assets;
using FairyGUI;
using UnityEngine;

namespace UI;

public class CriManaMovieBridge : MonoBehaviour
{
	private static readonly Dictionary<string, CriManaMovieBridge> _bridgeDic = new Dictionary<string, CriManaMovieBridge>();

	private static GameObject _brideRoot;

	private readonly List<GGraph> _videoGraphs = new List<GGraph>();

	private CriManaMovieControllerForGGraph _controller;

	private Renderer _targetRenderer;

	private string _videoKey;

	private const int _delayDelMaxCount = 8;

	private const int _delayDelFrameCount = 200;

	private int _delayFrameTimer;

	private bool _isReleased;

	public CriManaMovieControllerForGGraph Controller => _controller;

	private void Update()
	{
		UpdateBridge();
	}

	public void UpdateBridge()
	{
		if (_videoGraphs.Count == 0)
		{
			_delayFrameTimer++;
			if (_delayFrameTimer >= 200 || _bridgeDic.Count > 8)
			{
				_bridgeDic.Remove(_videoKey);
				ReleasePlayer();
			}
			return;
		}
		for (int num = _videoGraphs.Count - 1; num >= 0; num--)
		{
			GGraph gGraph = _videoGraphs[num];
			MeshRenderer meshRenderer = gGraph?.shape?.graphics?.meshRenderer;
			string text = gGraph?.VideoKey;
			if (meshRenderer == null || !meshRenderer.enabled || _videoKey != text || !gGraph.onStage)
			{
				_videoGraphs.RemoveAt(num);
			}
			else
			{
				Material material = gGraph.shape.graphics.material;
				Material sharedMaterial = _targetRenderer.sharedMaterial;
				material.CopyMatchingPropertiesFromMaterial(sharedMaterial);
			}
		}
		_delayFrameTimer = 0;
	}

	public void AddVideoGraph(GGraph graph)
	{
		if (graph != null && !_videoGraphs.Contains(graph))
		{
			graph.shape.graphics.material = new Material(_targetRenderer.sharedMaterial);
			_videoGraphs.Add(graph);
			graph.VideoKey = _videoKey;
		}
	}

	public static bool GetCriMoveCriManaMovieBridge(string videoKey, CriManaUsmAsset asset, Material originMat, bool uiRenderMode, out CriManaMovieBridge bridge)
	{
		bool result = false;
		if (!_bridgeDic.TryGetValue(videoKey, out bridge))
		{
			bridge = CreateCriManaMovieBridge(videoKey, asset, originMat, uiRenderMode);
			_bridgeDic[videoKey] = bridge;
			result = true;
		}
		return result;
	}

	public static void Release(string videoKey)
	{
		if (!string.IsNullOrEmpty(videoKey) && _bridgeDic.Remove(videoKey, out var value) && value != null)
		{
			value.ReleasePlayer();
		}
	}

	public static void ReleaseAll()
	{
		List<CriManaMovieBridge> list = new List<CriManaMovieBridge>(_bridgeDic.Values);
		_bridgeDic.Clear();
		foreach (CriManaMovieBridge item in list)
		{
			if (item != null)
			{
				item.ReleasePlayer();
			}
		}
	}

	private void ReleasePlayer()
	{
		if (!_isReleased)
		{
			_isReleased = true;
			base.enabled = false;
			_videoGraphs.Clear();
			if ((Object)(object)_controller != null)
			{
				((CriManaMovieMaterialBase)_controller).PlayerManualFinalize();
			}
			Object.Destroy(base.gameObject);
		}
	}

	private static CriManaMovieBridge CreateCriManaMovieBridge(string videoKey, CriManaUsmAsset asset, Material originMat, bool uiRenderMode)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		if (_brideRoot == null)
		{
			_brideRoot = new GameObject("BrideRoot");
			Object.DontDestroyOnLoad(_brideRoot);
		}
		GameObject obj = new GameObject("Bridge_" + videoKey);
		Object.DontDestroyOnLoad(obj);
		obj.transform.parent = _brideRoot.transform;
		MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
		CriManaMovieBridge criManaMovieBridge = obj.AddComponent<CriManaMovieBridge>();
		CriManaMovieControllerForGGraph criManaMovieControllerForGGraph = obj.AddComponent<CriManaMovieControllerForGGraph>();
		meshRenderer.enabled = false;
		criManaMovieControllerForGGraph.SetAsset(videoKey, asset);
		if (((CriManaMovieMaterialBase)criManaMovieControllerForGGraph).material == null)
		{
			((CriManaMovieMaterialBase)criManaMovieControllerForGGraph).material = new Material(originMat);
		}
		if (((CriManaMovieControllerForAsset)criManaMovieControllerForGGraph).Target == null)
		{
			if (uiRenderMode)
			{
				((CriManaMovieControllerForAsset)criManaMovieControllerForGGraph).Target = (ICriManaMovieMaterialTarget)(object)new FManaMovieMaterialFGUIGraphTarget(meshRenderer);
			}
			else
			{
				((CriManaMovieControllerForAsset)criManaMovieControllerForGGraph).Target = (ICriManaMovieMaterialTarget)(object)new ManaMovieMaterialRendererTarget((Renderer)meshRenderer);
			}
		}
		if (uiRenderMode)
		{
			((CriManaMovieMaterialBase)criManaMovieControllerForGGraph).PlayerManualSetup();
		}
		((CriManaMovieMaterialBase)criManaMovieControllerForGGraph).player.Start();
		meshRenderer.sharedMaterial = ((CriManaMovieMaterialBase)criManaMovieControllerForGGraph).material;
		criManaMovieBridge._targetRenderer = meshRenderer;
		criManaMovieBridge._controller = criManaMovieControllerForGGraph;
		criManaMovieBridge._videoKey = videoKey;
		return criManaMovieBridge;
	}
}
