using System;
using UnityEngine;

public class MeleeWeapon : Weapon
{
	const string ATTACK = "attack";

	public static Action<Vector3> OnMeleeWeaponHit;

	[SerializeField]
	private float damage = 50;
	private Animator anim;

	public override void Fire() {
		anim?.SetTrigger(ATTACK);
	}

	public void OnCollisionEnter(Collision collision) {
		IDamagable damagable = collision.collider.GetComponent<IDamagable>() as IDamagable;
		Debug.Log("[MeleeWeapon] melee hit" + damagable);
		if (damagable != null && (object)damagable != owner && damagable.GetSideId() != owner.TeamId) {
			damagable.SetDamage(damage);
		}

		OnMeleeWeaponHit?.Invoke(collision.contacts[0].point);
	}

	protected override void Awake() {
		base.Awake();
		anim = GetComponent<Animator>();
	}
}
