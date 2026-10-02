using System;
using Core;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityTimer;

namespace SinglePlayer.GamePlay.Character;

public class MonsterView : UnitView, IFireBullet
{
	private UniTaskCompletionSource _task;

	public override void Initialize(CharacterLogic owner)
	{
		base.Initialize(owner);
		_radius = owner.child<Monster>().Property.child<MonsterProperty>().MonsterInfo.MonsterRadius;
	}

	protected override void OnDeath()
	{
		base.OnDeath();
		Game.GetModel<GlobalSignal>().MonsterDeath.Dispatch(_owner.child<Monster>());
	}

	public async UniTask FireBullet(int bulletId)
	{
		if (!StaticConfigure.SinglePlayer.BulletDict.TryGetValue(bulletId, out var bulletConfig))
		{
			Debug.LogError($"#建筑物模块# 不存在子弹配置，子弹ID：{bulletId}");
			return;
		}
		if (!StaticConfigure.Effect.InfoDict.TryGetValue(bulletConfig.BulletEffectid, out var _))
		{
			Debug.LogError($"#建筑物模块# 不存在子弹特效配置，特效ID：{bulletConfig.BulletEffectid}");
			return;
		}
		Transform shootRoot = base.transform.DeepFind("ShootRoot");
		if (shootRoot == null)
		{
			Debug.LogError("怪物不存在子弹发射点ShootRoot");
			return;
		}
		HeroView targetView = Game.GetSystem<BoardManager>().characterManager.Hero.view;
		Effect effect = await SimpleSingletonProvider<EffectManager>.inst.PlayById(bulletConfig.BulletEffectid, shootRoot.position, Quaternion.identity);
		_task = new UniTaskCompletionSource();
		Bullet bullet = effect.gameObject.AddComponent<Bullet>();
		Timer t = Timer.Register(5f, (System.Action)delegate
		{
			UnityEngine.Object.Destroy(bullet);
			effect.ReleaseEffect();
			_task.TrySetResult();
		}, base.gameObject);
		bullet.initialSpeed = bulletConfig.InitialSpeed;
		bullet.acceleration = bulletConfig.Acceleration;
		bullet.initialTurnSpeed = bulletConfig.InitialTurnSpeed;
		bullet.turnAcceleration = bulletConfig.TurnAcceleration;
		bullet.maxSpeed = bulletConfig.MaxSpeed;
		bullet.Init(shootRoot.position, shootRoot.forward, targetView.HitRoot.position, targetView.Radius, delegate(Vector3 hitPosition)
		{
			SimpleSingletonProvider<EffectManager>.inst.PlayById(bulletConfig.HitEffectId, hitPosition, Quaternion.identity).Forget();
			SimpleSingletonProvider<AudioManager>.inst.SendEvent(bulletConfig.HitSoundctId, base.gameObject);
			if (!t.isDone)
			{
				t.Cancel();
				UnityEngine.Object.Destroy(bullet);
				effect.ReleaseEffect();
			}
			_task.TrySetResult();
		});
		await _task.Task;
	}
}
