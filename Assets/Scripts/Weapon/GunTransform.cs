using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTransform : MonoBehaviour {

	private Vector3 target;

	public void SetTarget(Vector3 target) {
		this.target = target;
	}
	
	private void Update () {
		transform.LookAt(target);		
	}
}
