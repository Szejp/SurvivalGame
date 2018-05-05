using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private Transform target;
	[SerializeField]
	private float lerpFactor = .5f;
	private Vector3 _offset;

	private void Start() {
		_offset = transform.position - target.transform.position;
	}

	private void Update() {
		Vector3 targetPosition = target.transform.position + _offset;
		float modifier = Vector3.Magnitude(targetPosition - transform.position);
		transform.position = Vector3.Lerp(transform.position, targetPosition, lerpFactor * modifier);
	}
}
