using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlane : Movement {

	private const float _gravity = 9.86f;
	private const float _airDensity = 1.225f;

	[SerializeField]
	private float _coefficent = 1;
	[SerializeField]
	private float _wingArea = 10;
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _rotationFactor = 1;

	private Vector3 _lift;
	private Rigidbody _rb;
	private float _thrustAcceleration;
	private float _angleOfAttack;

	protected override void Start() {
		base.Start();
		_rb = GetComponent<Rigidbody>();
		_rb.velocity += 50 * transform.forward;
	}

	private void Update() {
		_thrustAcceleration = 0;
#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.LeftShift))
			_thrustAcceleration += Mathf.Clamp(_speed, 0, 300);
		if (Input.GetKey(KeyCode.W))
			_rb.rotation = Quaternion.AngleAxis(_rotationFactor, transform.right) * _rb.rotation;
		if (Input.GetKey(KeyCode.S))
			_rb.rotation = Quaternion.AngleAxis(-_rotationFactor, transform.right) * _rb.rotation;
		if (Input.GetKey(KeyCode.S))
			_rb.rotation = Quaternion.AngleAxis(_rotationFactor, transform.up) * _rb.rotation;
		if (Input.GetKey(KeyCode.S))
			_rb.rotation = Quaternion.AngleAxis(-_rotationFactor, transform.up) * _rb.rotation;
#endif
	}

	private void FixedUpdate() {
		_angleOfAttack = 360 - transform.rotation.eulerAngles.x;
		_coefficent = Mathf.Clamp(-Mathf.Pow((_angleOfAttack - 15) / 13, 2) + 1.75f, 0, 2);
		_rb.velocity += _thrustAcceleration * Time.deltaTime * _rb.velocity.normalized;
		_lift = _coefficent * _airDensity * _wingArea / 2 * Mathf.Pow(_rb.velocity.magnitude * Time.deltaTime, 2) * transform.up;
		Debug.Log("lift: " + _lift + " thrust acceleration: " + _thrustAcceleration + " angle of attack: " + _angleOfAttack);
		_rb.AddForce(_lift + _thrustAcceleration * Time.deltaTime * transform.forward * _rb.mass);
	}
}
