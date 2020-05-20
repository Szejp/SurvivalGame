using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

	[SerializeField]
	private Spring spring;
	public float damp = 1;
	public float k = 1;
	public float mass = 1;
	public Vector3 startingPosition = Vector3.zero;
	[SerializeField]
	private Rigidbody rb;

	public Vector3 deltaPosition {
		get {
			return transform.position - startingPosition;
		}
	}

	private void Start() {
		rb = GetComponent<Rigidbody>();
		if (spring != null) {
			transform.position = spring.transform.position;
			startingPosition = transform.position;
		}
	}

	private void Update() {
		if (spring != null) {
			transform.position += (-1) * spring.k * (Vector3.Distance(transform.position + Vector3.left, spring.transform.position) - 2)
				* Vector3.Normalize(transform.position - spring.transform.position)
				* Time.deltaTime * Time.deltaTime / mass;
			Debug.Log(spring.deltaPosition);
		}
	}
}
