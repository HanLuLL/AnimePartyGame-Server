using Cinemachine;
using Core.Unit;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Core.Camera;

public class CharacterCamera
{
	public readonly CinemachineVirtualCamera vCamera;

	public readonly Character owner;

	public readonly CinemachineImpulseSource impulseSource;

	public CharacterCamera(Character _character)
	{
		owner = _character;
		GameObject freeCameraInstance = SimpleSingletonProvider<InternalAssetManager>.inst.GetFreeCameraInstance(owner.player.GetNick());
		vCamera = freeCameraInstance.GetComponent<CinemachineVirtualCamera>();
		vCamera.m_Follow = _character.transform;
		impulseSource = freeCameraInstance.GetComponentInChildren<CinemachineImpulseSource>();
		SimpleSingletonProvider<CameraManager>.inst.Register(this);
	}

	public void SetMoveDamping(bool moving)
	{
		CinemachineTransposer cinemachineComponent = vCamera.GetCinemachineComponent<CinemachineTransposer>();
		cinemachineComponent.m_XDamping = (moving ? 0f : 0.3f);
		cinemachineComponent.m_YDamping = (moving ? 0f : 0.3f);
		cinemachineComponent.m_ZDamping = (moving ? 0f : 0.3f);
	}

	public UniTask<bool> SwitchCamera()
	{
		return SimpleSingletonProvider<CameraManager>.inst.SwitchCamera(this);
	}

	public void Dispose()
	{
		SimpleSingletonProvider<CameraManager>.inst.UnRegister(this);
		if (vCamera != null)
		{
			Object.Destroy(vCamera.gameObject);
		}
	}
}
