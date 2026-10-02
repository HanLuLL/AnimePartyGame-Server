using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.Universal;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI;

public class RenderTextureManager : SimpleSingletonProvider<RenderTextureManager>
{
	private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _modelHandles = new Dictionary<string, AsyncOperationHandle<GameObject>>();

	private Camera _renderCamera;

	private Light _renderLight;

	private Transform _root;

	private async UniTask<GameObject> Load(string key)
	{
		if (string.IsNullOrEmpty(key))
		{
			return null;
		}
		if (_modelHandles.TryGetValue(key, out var value))
		{
			return value.Result;
		}
		AsyncOperationHandle<GameObject> asyncOperationHandle = await AddressableHelper.LoadAssetAsync<GameObject>(key);
		if (asyncOperationHandle.IsDone)
		{
			if (_modelHandles.TryGetValue(key, out var value2))
			{
				Addressables.Release(asyncOperationHandle);
				return value2.Result;
			}
			_modelHandles.Add(key, asyncOperationHandle);
			return asyncOperationHandle.Result;
		}
		Addressables.Release(asyncOperationHandle);
		return null;
	}

	public async UniTask<Transform> ShowTargetModelRenderTexture(GGraph holder, string key, int cullingMask = -1, Vector3? lightDirection = null, Vector3? position = null, Quaternion? modelRotation = null, Vector3? modelScale = null, bool update = false)
	{
		holder?.DestroyRenderModel();
		return await ShowRenderTexture(holder, key, cullingMask, lightDirection, position, modelRotation, modelScale, update);
	}

	public async UniTask<Transform> ShowRenderTexture(GGraph holder, string key, int cullingMask = -1, Vector3? lightDirection = null, Vector3? position = null, Quaternion? modelRotation = null, Vector3? modelScale = null, bool update = false)
	{
		if (holder == null)
		{
			return null;
		}
		holder.color = Color.clear;
		GameObject gameObject = await Load(key);
		if (gameObject == null)
		{
			return null;
		}
		if (_root == null)
		{
			_root = new GameObject("RenderImage").transform;
			_renderCamera = _root.gameObject.AddComponent<Camera>();
			_renderCamera.clearFlags = CameraClearFlags.Color;
			_renderCamera.backgroundColor = Color.clear;
			_renderCamera.enabled = false;
			UniversalAdditionalCameraData universalAdditionalCameraData = CameraExtensions.GetUniversalAdditionalCameraData(_renderCamera);
			universalAdditionalCameraData.SetRenderer(2);
			universalAdditionalCameraData.renderShadows = false;
			Transform transform = new GameObject("RenderLight").transform;
			transform.SetParent(_root, worldPositionStays: false);
			_renderLight = transform.gameObject.AddComponent<Light>();
			_renderLight.type = LightType.Directional;
		}
		ApplyCullingMask(cullingMask);
		ApplyLightDirection(lightDirection ?? Vector3.zero);
		holder.TryCreateDisplayRender(key, _renderCamera);
		holder.RenderModelRoot.SetParent(_root, worldPositionStays: false);
		holder.RenderModelRoot.transform.localPosition = position ?? (Vector3.forward * 5f);
		holder.RenderModelRoot.transform.rotation = modelRotation ?? Quaternion.identity;
		Transform model = Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity).transform;
		model.SetParent(holder.RenderModelRoot, worldPositionStays: false);
		model.localScale = modelScale ?? Vector3.one;
		await UniTask.DelayFrame(1);
		holder.StartRender(update);
		return model;
	}

	private void ApplyLightDirection(Vector3 direction)
	{
		if (!(_renderLight == null) && !(direction.sqrMagnitude <= Mathf.Epsilon))
		{
			_renderLight.transform.rotation = Quaternion.LookRotation(-direction.normalized);
		}
	}

	private void ApplyCullingMask(int cullingMask)
	{
		if (_renderCamera != null)
		{
			_renderCamera.cullingMask = cullingMask;
		}
		if (_renderLight != null)
		{
			_renderLight.cullingMask = cullingMask;
		}
	}

	public void Dispose(GGraph holder, bool releaseHandle = false)
	{
		if (holder != null)
		{
			holder.DestroyRenderTexture();
			if (releaseHandle && !string.IsNullOrEmpty(holder.RenderKey) && _modelHandles.Remove(holder.RenderKey, out var value))
			{
				Addressables.Release(value);
			}
		}
	}

	public void ClearCache()
	{
		if (_root != null)
		{
			Object.Destroy(_root.gameObject);
			_root = null;
			_renderCamera = null;
			_renderLight = null;
		}
		foreach (AsyncOperationHandle<GameObject> value in _modelHandles.Values)
		{
			Addressables.Release(value);
		}
		_modelHandles.Clear();
	}
}
