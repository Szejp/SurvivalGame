using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour {

	public Animator anim;
	private Vector2 velocity;
	private Vector3 previousPosition;

	public void PlayAnimation(string name) {
		PlayAnimCoroutine(name);
	}

	public IEnumerator PlayAnimCoroutine(string animName) {
		anim.Play(animName);
		yield return new WaitForEndOfFrame();
	}

	public IEnumerator PlayAttackAnimation() {
		anim.SetTrigger("attack");
		yield return true;
	}

	private void Start() {
		anim = GetComponent<Animator>();
	}

	private void Update() {
		Vector3 worldDeltaPosition = transform.position - previousPosition;
		previousPosition = transform.position;
		velocity = worldDeltaPosition / Time.deltaTime;
		anim.SetFloat("speed", velocity.magnitude);
	}
}

