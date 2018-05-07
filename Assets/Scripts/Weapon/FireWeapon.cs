using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireWeapon : Weapon {

	[SerializeField]
	protected Transform _gun;
	[SerializeField]
	protected FireWeaponData _currentWeapon;

	protected Action _fireAction;
	protected float _lastFiredTime = 0;

	public override void Fire() {
		if (_lastFiredTime + _currentWeapon.fireRate < Time.realtimeSinceStartup) {
			_fireAction.Invoke();
			_lastFiredTime = Time.realtimeSinceStartup;
		}
	}


}
