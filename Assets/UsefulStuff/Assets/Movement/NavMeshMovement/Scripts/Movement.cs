using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Movement : MonoBehaviour {

	public float velocity { get; private set; }
	public float angularVelocity { get; private set; }
	public bool isGrounded { get; private set; }
	public bool isMoving { get; private set; }

	protected Vector3 previousPosition;
	protected Vector3 _previousForward;
	protected Collider _collider;

	public virtual void MoveLeft() { }
	public virtual void MoveRight() { }
	public virtual void MoveForwards() { }
	public virtual void MoveBackwards() { }
	public virtual void MoveUp() { }
	public virtual void MoveDown() { }
	public virtual void Teleport(Vector3 position) { }

	protected virtual void Start() {
		_collider = GetComponent<Collider>();
	}

	protected virtual void LateUpdate() {
		velocity = Vector3.Magnitude(transform.position - previousPosition) / Time.deltaTime;
		angularVelocity = Vector3.SignedAngle(transform.forward, _previousForward, transform.up) / Time.deltaTime;
		isGrounded = GroundCheck();
		previousPosition = transform.position;
		_previousForward = transform.forward;
		isMoving = Mathf.Abs(velocity) > 0.001f;
	}

	protected bool GroundCheck() {
		return Physics.CheckCapsule(_collider.bounds.center, new Vector3(_collider.bounds.center.x, _collider.bounds.min.y + 0.1f, _collider.bounds.center.z), 0.16f, 1 << LayerMask.NameToLayer("Ground"));
	}
}
