using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

	public Agent owner;

	public abstract void Fire();

	protected virtual void Awake() {
		owner = GetComponentInParent<Agent>();
	}
}
