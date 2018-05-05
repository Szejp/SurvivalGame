using UnityEngine;

[System.Serializable]
public class Projectile : GameEntity {

	public int Damage { get { return damage; } }
	public float Speed { get { return projectileSpeed; } }
	public int firedFromSideId;

	[SerializeField]
	private float lifeTime = 2;
	private float time;
	[SerializeField]
	private int damage = 1;
	[SerializeField]
	private GameObject destroyEffect;
	[SerializeField]
	private float projectileSpeed = 7000;

	protected virtual void Update() {
		if (Time.realtimeSinceStartup - time > lifeTime)
			Destroy();
	}

	protected void OnEnable() {
		time = Time.realtimeSinceStartup;
	}

	protected void OnDestroy() {
		if (destroyEffect)
			Instantiate(destroyEffect, gameObject.transform.position, destroyEffect.transform.rotation);
	}

	protected virtual void OnTriggerEnter(Collider collider) {
		IDamagable damagable = collider.GetComponent<IDamagable>() as IDamagable;
		if (damagable != null && ((!GameOvermind.instance.friendlyFire && damagable.GetSideId() != firedFromSideId) || GameOvermind.instance.friendlyFire)) {
			damagable.SetDamage(damage);
			Destroy();
		}
	}
}
