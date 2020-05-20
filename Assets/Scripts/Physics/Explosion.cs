using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	[SerializeField]
	private float explosionRadius = 1f;
	[SerializeField]
	private float explosionForce = 1f;
	[SerializeField]
	private bool testing = false;

	[ContextMenu("Explode")]
	public void Explode() {
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null)
				rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, 3.0F);
		}

		Debug.Log("[Explosion] Exploded");
	}

	private void Update() {
		if (testing && Input.GetKeyUp(KeyCode.E))
			Explode();
	}
}
