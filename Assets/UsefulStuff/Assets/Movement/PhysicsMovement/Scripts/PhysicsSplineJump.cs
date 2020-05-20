using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSplineJump : Movement {

	[SerializeField]
	private float _defaultForce = 100;
	[SerializeField]
	private float _maxDistance = 2f;
	private Rigidbody _rb;

	private void Awake() {
		_rb = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 direction, Vector3 up, float force = 0) {
		if (!isGrounded) return;
		if (force == 0) force = _defaultForce;
		_rb.velocity = ((direction + up) * force) / _rb.mass;
	}

	private void Update() {
#if UNITY_EDITOR
		if (Input.GetMouseButton(0)) {
			var direction = new Vector3(Mathf.Clamp(Camera.main.ScreenPointToRay(Input.mousePosition).origin.x - transform.position.x, -_maxDistance, _maxDistance), 0, 0);
			Move(direction, Vector3.up);
		}
#endif
	}
}
