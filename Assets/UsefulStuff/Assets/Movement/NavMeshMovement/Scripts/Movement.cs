using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float velocity { get; private set; }
	public float angularVelocity { get; private set; }
	public bool isGrounded { get; private set; }
	public bool isMoving { get; private set; }

	protected Vector3 previousPosition;
	protected Vector3 previousForward;

	public virtual void MoveLeft() { }
	public virtual void MoveRight() { }
	public virtual void MoveForwards() { }
	public virtual void MoveBackwards() { }
	public virtual void MoveUp() { }
	public virtual void MoveDown() { }
	public virtual void Teleport(Vector3 position) { }

	protected virtual void LateUpdate() {
		velocity = Vector3.Magnitude(transform.position - previousPosition) / Time.deltaTime;
		angularVelocity = Vector3.SignedAngle(transform.forward, previousForward, transform.up) / Time.deltaTime;
		isGrounded = GroundCheck();
		previousPosition = transform.position;
		previousForward = transform.forward;
		isMoving = Mathf.Abs(velocity) > 0.001f;
	}

	protected bool GroundCheck() {
		if (Mathf.Abs((transform.position - previousPosition).y) < 0.0001) return true;
		else return false;
	}
}
