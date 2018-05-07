using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimationController : MonoBehaviour {

	private Animator anim;
	private Movement movement;

	public void Shoot() {
		anim.SetTrigger("shoot");
	}

	private void Awake() {
		movement = GetComponent<Movement>();
		anim = GetComponent<Animator>();
	}

	private void Update() {
		anim.SetFloat("velocity", movement.velocity);
		anim.SetFloat("angularVelocity", movement.angularVelocity);
		anim.SetBool("isMoving", movement.isMoving);
	}
}
