using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire Weaponds DataBase", menuName = "Scriptables/Fire Weapons DataBase", order = 1)]
[System.Serializable]
public class ProjectileWeaponsTemplates : ScriptableObject {
	[SerializeField]
	public FireWeaponData[] fireWeaponTemplates;
}


