using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire Weapon Instance", menuName = "Scriptables/Fire Weapon Instance", order = 2)]
[System.Serializable]
public class FireWeaponData : ScriptableObject {
	[SerializeField]
	public Projectile projectile;
	public float fireRate;
	public AudioClip weaponSound;
}
