using UnityEngine;

public class ProjectileWeapon : FireWeapon {

	protected override void Awake() {
		base.Awake();
		_fireAction = FireProjectile;
	}

	public void FireProjectile() {
		Debug.Log("[ProjectileWeapon] fire projectile function");
		Projectile b = (Projectile)GameOvermind.instance.spawner.Spawn(_currentWeapon.projectile);
		b.transform.position = _gun.position + _gun.transform.forward / 2;
		b.transform.LookAt(b.transform.position);
		b.GetComponent<Rigidbody>().velocity = _gun.transform.forward * _currentWeapon.projectile.Speed;
		b.firedFromSideId = owner.GetSideId();
	}
}
