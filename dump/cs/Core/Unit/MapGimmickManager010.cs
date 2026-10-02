using Cysharp.Threading.Tasks;
using FairyGUI;
using UnityEngine;

namespace Core.Unit;

public class MapGimmickManager010 : MapGimmickManager
{
	[SerializeField]
	private Transform _effectFireworksRoot;

	private const int _effectFireworksAudioId = 1102;

	[SerializeField]
	private Transform _effectFireworksProRoot;

	private const int _effectFireworksProAudioId = 1102;

	private const float FIREWORKS_EFFECT_TIME = 4f;

	private const float FIREWORKSPRO_EFFECT_TIME = 6f;

	private float _fireworksEffectTimer;

	private float _fireworksProEffectTimer;

	private bool _isFireworksEffectPlaying;

	private bool _isFireworksProEffectPlaying;

	protected override void Awake()
	{
		Initialize();
		base.Awake();
	}

	private void Update()
	{
		UpdateEffect();
	}

	public override void Initialize()
	{
	}

	public override void RefreshGimmickData(int groupId, int statusId)
	{
	}

	public override UniTask SwitchGimmick(int groupId, int statusId, bool wait = true)
	{
		return UniTask.CompletedTask;
	}

	public void PlayEffectFireworks()
	{
		if (_effectFireworksRoot != null)
		{
			_effectFireworksRoot.gameObject.SetActive(value: false);
			_effectFireworksRoot.gameObject.SetActive(value: true);
			_fireworksEffectTimer = 4f;
			_isFireworksEffectPlaying = true;
			Stage.inst.PlayOneShotSound(1102);
		}
	}

	public void PlayEffectFireworksPro()
	{
		if (_effectFireworksProRoot != null)
		{
			_effectFireworksProRoot.gameObject.SetActive(value: false);
			_effectFireworksProRoot.gameObject.SetActive(value: true);
			_fireworksProEffectTimer = 6f;
			_isFireworksProEffectPlaying = true;
			Stage.inst.PlayOneShotSound(1102);
		}
	}

	private void UpdateEffect()
	{
		if (_isFireworksEffectPlaying)
		{
			_fireworksEffectTimer -= Time.deltaTime;
			if (_fireworksEffectTimer <= 0f)
			{
				_effectFireworksRoot.gameObject.SetActive(value: false);
				_isFireworksEffectPlaying = false;
			}
		}
		if (_isFireworksProEffectPlaying)
		{
			_fireworksProEffectTimer -= Time.deltaTime;
			if (_fireworksProEffectTimer <= 0f)
			{
				_effectFireworksProRoot.gameObject.SetActive(value: false);
				_isFireworksProEffectPlaying = false;
			}
		}
	}
}
