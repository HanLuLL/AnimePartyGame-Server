using System;
using Core.Audio;
using DG.Tweening;
using UnityEngine;

namespace Core;

[Serializable]
public class GachaDice : MonoBehaviour
{
	public Rigidbody rb;

	[Header("States")]
	public bool isContactWithFloor;

	public bool isContactWithDice;

	public bool isTextureLit;

	[Header("References")]
	[SerializeField]
	public Transform diceObject;

	[SerializeField]
	public GameEffect highQualityEffect;

	public MeshRenderer diceMeshRenderer;

	[SerializeField]
	public GachaDiceData diceData;

	[SerializeField]
	public GameObject[] faceDetectors;

	[Header("Debug")]
	public int defaultFaceResult = -1;

	public int alteredFaceResult = -1;

	private float stopThreshold = 0.01f;

	private Tweener moveTweener;

	private Tweener rotateTweener;

	private Tweener diceRotateTweener;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		diceMeshRenderer = diceObject.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
	}

	public void SetDiceResult(Vector3 initPosition, Quaternion initRotation, int diceResult)
	{
		isTextureLit = false;
		DisplayPhysics();
		FindFaceResult();
		RotateDice(diceResult);
		base.transform.position = initPosition;
		base.transform.rotation = initRotation;
		diceObject.gameObject.SetActive(value: true);
	}

	private void RotateDice(int alteredFace)
	{
		if (alteredFace < diceData.faceRelativeRotation.Count && alteredFace >= 0)
		{
			alteredFaceResult = alteredFace;
			Vector3 eulers = diceData.faceRelativeRotation[defaultFaceResult].rotation[alteredFaceResult];
			diceObject.Rotate(eulers);
		}
		else
		{
			alteredFaceResult = defaultFaceResult;
		}
	}

	private int FindFaceResult()
	{
		int num = 0;
		for (int i = 1; i < faceDetectors.Length; i++)
		{
			if (faceDetectors[num].transform.position.y < faceDetectors[i].transform.position.y)
			{
				num = i;
			}
		}
		defaultFaceResult = num;
		return num;
	}

	public void SetDiceMesh(bool status)
	{
		diceObject.GetChild(0).gameObject.SetActive(status);
		if (alteredFaceResult == 1 && status)
		{
			highQualityEffect.gameObject.SetActive(value: true);
			highQualityEffect.Play("", null);
		}
		else
		{
			highQualityEffect.gameObject.SetActive(value: false);
		}
	}

	public void EnablePhysics()
	{
		rb.useGravity = true;
		rb.isKinematic = false;
	}

	public void DisplayPhysics()
	{
		rb.useGravity = false;
		rb.isKinematic = true;
	}

	public void ShowDiceResult()
	{
		if (!isTextureLit)
		{
			BGMHelper.TryPlayBGM(1);
			ChangeTextureToLit(alteredFaceResult);
			isTextureLit = true;
		}
	}

	private void ChangeTextureToLit(int faceResult)
	{
		switch (faceResult)
		{
		case 0:
			diceMeshRenderer.materials[0].SetFloat("_SelfLitIntensity", 1f);
			break;
		case 1:
			diceMeshRenderer.materials[1].SetFloat("_SelfLitIntensity", 1f);
			break;
		case 2:
			diceMeshRenderer.materials[5].SetFloat("_SelfLitIntensity", 1f);
			break;
		case 3:
			diceMeshRenderer.materials[4].SetFloat("_SelfLitIntensity", 1f);
			break;
		case 4:
			diceMeshRenderer.materials[3].SetFloat("_SelfLitIntensity", 1f);
			break;
		case 5:
			diceMeshRenderer.materials[2].SetFloat("_SelfLitIntensity", 1f);
			break;
		}
	}

	public void Reset()
	{
		KillTweener();
		ResetTexturetoUnlit();
		DisplayPhysics();
		diceObject.gameObject.SetActive(value: false);
		defaultFaceResult = -1;
		alteredFaceResult = -1;
		isContactWithFloor = false;
		isContactWithDice = false;
	}

	private void ResetTexturetoUnlit()
	{
		isTextureLit = false;
		diceMeshRenderer.materials[0].SetFloat("_SelfLitIntensity", 0f);
		diceMeshRenderer.materials[1].SetFloat("_SelfLitIntensity", 0f);
		diceMeshRenderer.materials[5].SetFloat("_SelfLitIntensity", 0f);
		diceMeshRenderer.materials[4].SetFloat("_SelfLitIntensity", 0f);
		diceMeshRenderer.materials[3].SetFloat("_SelfLitIntensity", 0f);
		diceMeshRenderer.materials[2].SetFloat("_SelfLitIntensity", 0f);
	}

	public bool CheckObjectHasStopped()
	{
		if (rb.velocity.magnitude <= stopThreshold && rb.angularVelocity.magnitude <= stopThreshold)
		{
			return true;
		}
		return false;
	}

	public void PlaySoundTouchFloor()
	{
		BGMHelper.TryPlayBGM(15);
	}

	public void PlaySoundTouchDice()
	{
		BGMHelper.TryPlayBGM(18);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.CompareTag("GachaFloor"))
		{
			isContactWithFloor = true;
		}
		if (collision.transform.CompareTag("GachaDice"))
		{
			isContactWithDice = true;
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.transform.CompareTag("GachaFloor"))
		{
			isContactWithFloor = false;
		}
		if (collision.transform.CompareTag("GachaDice"))
		{
			isContactWithDice = false;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.transform.CompareTag("GachaFloor"))
		{
			isContactWithFloor = false;
		}
		if (collision.transform.CompareTag("GachaDice"))
		{
			isContactWithDice = false;
		}
	}

	public Vector3 GetUpDir()
	{
		return diceData.faceRelativeRotation[0].rotation[alteredFaceResult];
	}

	public void ResetDoAnimation(Vector3 _targetPos, float _MoveTime, float _RotateTime, Transform _camera)
	{
		moveTweener = base.transform.DOMove(_targetPos, _MoveTime);
		Quaternion endValue = Quaternion.LookRotation(-_camera.forward.normalized, _camera.up.normalized);
		rotateTweener = base.transform.DORotateQuaternion(endValue, _RotateTime);
		diceRotateTweener = diceObject.DOLocalRotate(GetUpDir(), _RotateTime);
	}

	public void KillTweener()
	{
		moveTweener?.Kill();
		moveTweener = null;
		rotateTweener?.Kill();
		rotateTweener = null;
		diceRotateTweener?.Kill();
		diceRotateTweener = null;
	}
}
