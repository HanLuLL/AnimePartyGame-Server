using System;
using System.Collections.Generic;
using CriWare;
using CriWare.Assets;
using CriWare.CriMana;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI;

public class CriMovieManager : SimpleSingletonProvider<CriMovieManager>
{
	private readonly Dictionary<string, CriManaUsmAsset> _loadedAssets = new Dictionary<string, CriManaUsmAsset>();

	public static readonly Material CriMaterial = new Material(Shader.Find("CriMana/SofdecPrimeYuv"));

	public async UniTask PlaAutoReleaseVideo(string key, GGraph graph, int initFrame = 0, bool PlayEndImmediatelyStop = false, bool uiRenderMode = false, int dealyFrame = 0)
	{
		if (!_loadedAssets.TryGetValue(key, out var asset))
		{
			asset = await Load(key);
		}
		if (graph.DisposeCirMovie)
		{
			await UniTask.DelayFrame(1);
			graph.DisposeCirMovie = false;
		}
		CriManaMovieBridge bridge;
		bool criMoveCriManaMovieBridge = CriManaMovieBridge.GetCriMoveCriManaMovieBridge(key, asset, CriMaterial, uiRenderMode, out bridge);
		CriManaMovieControllerForGGraph controller = bridge.Controller;
		if (criMoveCriManaMovieBridge)
		{
			((CriManaMovieMaterialBase)controller).renderMode = (RenderMode)0;
			((CriManaMovieMaterialBase)controller).maxFrameDrop = (MaxFrameDrop)2;
			((CriManaMovieMaterialBase)controller).player.SetSeekPosition(initFrame);
		}
		else
		{
			graph.visible = true;
		}
		bridge.AddVideoGraph(graph);
		graph.shape.graphics.meshRenderer.enabled = true;
		graph.color = Color.white;
		if (criMoveCriManaMovieBridge)
		{
			if (dealyFrame > 0)
			{
				await UniTask.DelayFrame(dealyFrame);
			}
			graph.visible = true;
		}
	}

	public async UniTask<Player> Play(string key, GGraph graph, Action<Player, int> onPlayReady = null, Action<Player, int> onPlayFinished = null, Action<EventPoint, Player> onPlayCuePoint = null, int initFrame = 0, bool PlayEndImmediatelyStop = false, bool isSideTransparent = false, bool uiRenderMode = false)
	{
		if (!_loadedAssets.TryGetValue(key, out var asset))
		{
			asset = await Load(key);
		}
		if (graph.DisposeCirMovie)
		{
			await UniTask.DelayFrame(1);
			graph.DisposeCirMovie = false;
		}
		if (!graph.shape.gameObject.TryGetComponent<CriManaMovieControllerForGGraph>(out var controller))
		{
			controller = graph.shape.gameObject.AddComponent<CriManaMovieControllerForGGraph>();
		}
		else if (!string.IsNullOrEmpty(controller.CurrentVideoKey) && !controller.CurrentVideoKey.Equals(key))
		{
			((CriManaMovieMaterialBase)controller).Stop();
			await UniTask.WaitUntil(() => ((CriManaMovieMaterialBase)controller).player == null || (int)((CriManaMovieMaterialBase)controller).player.status == 0);
		}
		controller.SetAsset(key, asset);
		if (((CriManaMovieMaterialBase)controller).material == null)
		{
			((CriManaMovieMaterialBase)controller).material = new Material(CriMaterial);
			if (isSideTransparent)
			{
				((CriManaMovieMaterialBase)controller).material.EnableKeyword("_SIDE_TRANSPARENT");
			}
		}
		if (((CriManaMovieControllerForAsset)controller).Target == null)
		{
			if (uiRenderMode)
			{
				((CriManaMovieControllerForAsset)controller).Target = (ICriManaMovieMaterialTarget)(object)new FManaMovieMaterialFGUIGraphTarget(graph.shape.graphics.meshRenderer);
			}
			else
			{
				((CriManaMovieControllerForAsset)controller).Target = (ICriManaMovieMaterialTarget)(object)new ManaMovieMaterialRendererTarget((Renderer)graph.shape.graphics.meshRenderer);
			}
		}
		if (uiRenderMode)
		{
			((CriManaMovieMaterialBase)controller).PlayerManualSetup();
		}
		graph.shape.graphics.material = ((CriManaMovieMaterialBase)controller).material;
		((CriManaMovieMaterialBase)controller).renderMode = (RenderMode)0;
		((CriManaMovieMaterialBase)controller).maxFrameDrop = (MaxFrameDrop)2;
		graph.shape.graphics.meshRenderer.enabled = true;
		((CriManaMovieMaterialBase)controller).player.statusChangeCallback = (StatusChangeCallback)delegate(Status status)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Invalid comparison between Unknown and I4
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Invalid comparison between Unknown and I4
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Invalid comparison between Unknown and I4
			if ((int)status == 5)
			{
				onPlayReady?.Invoke(((CriManaMovieMaterialBase)controller).player, 5);
			}
			if ((int)status == 7)
			{
				((CriManaMovieMaterialBase)controller).Stop();
				onPlayFinished?.Invoke(((CriManaMovieMaterialBase)controller).player, 7);
			}
			if ((int)status == 6)
			{
				if (PlayEndImmediatelyStop)
				{
					((CriManaMovieMaterialBase)controller).Stop();
				}
				onPlayFinished?.Invoke(((CriManaMovieMaterialBase)controller).player, 6);
			}
		};
		((CriManaMovieMaterialBase)controller).player.cuePointCallback = (CuePointCallback)delegate(ref EventPoint point)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			onPlayCuePoint?.Invoke(point, ((CriManaMovieMaterialBase)controller).player);
		};
		((CriManaMovieMaterialBase)controller).player.SetSeekPosition(initFrame);
		graph.color = Color.white;
		if ((int)((CriManaMovieMaterialBase)controller).player.status == 0)
		{
			graph.visible = true;
			((CriManaMovieMaterialBase)controller).player.Start();
		}
		else if ((int)((CriManaMovieMaterialBase)controller).player.status == 8)
		{
			await UniTask.WaitUntil(() => (UnityEngine.Object)(object)controller == null || (int)((CriManaMovieMaterialBase)controller).player.status == 0);
			if ((UnityEngine.Object)(object)controller == null)
			{
				return null;
			}
			graph.visible = true;
			((CriManaMovieMaterialBase)controller).player.Start();
		}
		return ((CriManaMovieMaterialBase)controller).player;
	}

	public void StopAuto(GGraph graph)
	{
		if (graph != null)
		{
			MeshRenderer meshRenderer = graph.shape?.graphics?.meshRenderer;
			if (meshRenderer != null)
			{
				meshRenderer.enabled = false;
			}
		}
	}

	public void Stop(GGraph graph)
	{
		if (graph != null)
		{
			CriManaMovieControllerForAsset component = graph.shape.gameObject.GetComponent<CriManaMovieControllerForAsset>();
			if ((UnityEngine.Object)(object)component != null)
			{
				((CriManaMovieMaterialBase)component).Stop();
			}
		}
	}

	public void StopAndDestroy(GGraph graph)
	{
		if (graph == null)
		{
			return;
		}
		graph.DisposeVideo();
		CriManaMovieControllerForAsset component = graph.shape.gameObject.GetComponent<CriManaMovieControllerForAsset>();
		if ((UnityEngine.Object)(object)component != null)
		{
			((CriManaMovieMaterialBase)component).Stop();
			if (((CriManaMovieMaterialBase)component).material != null)
			{
				UnityEngine.Object.Destroy(((CriManaMovieMaterialBase)component).material);
			}
			UnityEngine.Object.Destroy((UnityEngine.Object)(object)component);
		}
	}

	public async UniTask<CriManaUsmAsset> Load(string key)
	{
		if (string.IsNullOrEmpty(key))
		{
			return null;
		}
		if (_loadedAssets.TryGetValue(key, out var value))
		{
			return value;
		}
		AsyncOperationHandle<CriManaUsmAsset> handle = await AddressableHelper.LoadAssetAsync<CriManaUsmAsset>(key);
		if (handle.IsDone && handle.Status == AsyncOperationStatus.Succeeded)
		{
			if (_loadedAssets.TryGetValue(key, out var value2))
			{
				Addressables.Release(handle);
				return value2;
			}
			CriManaUsmAsset result = handle.Result;
			_loadedAssets.Add(key, result);
			return result;
		}
		return null;
	}

	public void ClearOne(string key)
	{
		if (!string.IsNullOrEmpty(key))
		{
			CriManaMovieBridge.Release(key);
			if (_loadedAssets.TryGetValue(key, out var value))
			{
				((CriAssetBase)value).Implementation.OnDisable();
				_loadedAssets.Remove(key);
				Addressables.Release<CriManaUsmAsset>(value);
			}
		}
	}

	public void Clear()
	{
		CriManaMovieBridge.ReleaseAll();
		foreach (CriManaUsmAsset value in _loadedAssets.Values)
		{
			((CriAssetBase)value).Implementation.OnDisable();
			Addressables.Release<CriManaUsmAsset>(value);
		}
		_loadedAssets.Clear();
	}
}
