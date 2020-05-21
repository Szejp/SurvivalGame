using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

	public static Action<Vector3> OnMeleeWeaponHit;

	[SerializeField]
	private float damage = 50;
	private Animation anim;

	public override void Fire() {
		anim.Play();
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
		anim = GetComponent<Animation>();
	}
}
