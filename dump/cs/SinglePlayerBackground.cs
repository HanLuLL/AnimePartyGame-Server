using System;
using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class SinglePlayerBackground : MonoBehaviour
{
	private float _distance;

	private CinemachineVirtualCamera targetCamera;

	public float scrollX = -0.05f;

	public float scrollY = 0.05f;

	private GameObject _scrollGround;

	private Material _scrollMaterial;

	private readonly float _quadAspect = 1.7777778f;

	private void Awake()
	{
		if (targetCamera == null)
		{
			targetCamera = UnityEngine.Object.FindObjectOfType<CinemachineVirtualCamera>();
		}
		_distance = Vector3.Distance(targetCamera.transform.position, base.transform.position);
		_scrollGround = base.transform.GetChild(0).gameObject;
		_scrollMaterial = _scrollGround.GetComponent<Renderer>().material;
		Init();
	}

	private void Update()
	{
		if (_scrollMaterial != null)
		{
			_scrollMaterial.mainTextureOffset += new Vector2(scrollX, scrollY) * Time.deltaTime;
		}
	}

	private void Init()
	{
		if (!(targetCamera == null))
		{
			base.transform.position = targetCamera.transform.position + targetCamera.transform.forward * _distance;
			base.transform.rotation = targetCamera.transform.rotation;
			float aspect = Camera.main.aspect;
			float num = 2f * _distance * Mathf.Tan(targetCamera.m_Lens.FieldOfView * 0.5f * ((float)Math.PI / 180f));
			float num2 = num * aspect;
			float num3;
			float num4;
			if (aspect > _quadAspect)
			{
				num3 = num2;
				num4 = num2 / _quadAspect;
			}
			else
			{
				num4 = num;
				num3 = num * _quadAspect;
			}
			base.transform.localScale = new Vector3(num3 * 5f, num4 * 5f, 1f);
		}
	}
}
