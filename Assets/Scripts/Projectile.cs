using System;
using QFramework.GameTools.Entities;
using UnityEngine;

[System.Serializable]
public class Projectile : Entity
{
	public static Action<Projectile> OnDestroyed;
	
	public int Damage { get { return damage; } }
	public float Speed { get { return projectileSpeed; } }
	public int firedFromSideId;

	[SerializeField]
	private float lifeTime = 2;
	private float time;
	[SerializeField]
	private int damage = 1;
	[SerializeField]
	private float projectileSpeed = 7000;

	protected virtual void Update() {
		if (Time.realtimeSinceStartup - time > lifeTime)
			Destroy(null);
	}

	protected void OnEnable() {
		time = Time.realtimeSinceStartup;
	}

	protected virtual void OnTriggerEnter(Collider collider) {
		IDamagable damagable = collider.GetComponent<IDamagable>() as IDamagable;
		if (damagable != null && ((!GameOvermind.instance.friendlyFire && damagable.GetSideId() != firedFromSideId) || GameOvermind.instance.friendlyFire)) {
			damagable.SetDamage(damage);
			Destroy(null);
			OnDestroyed?.Invoke(this);
		}
	}
}
