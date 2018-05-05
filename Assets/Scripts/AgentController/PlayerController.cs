﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AgentController {

	private RaycastHit hit;

	private void Update() {
		if (Input.GetKey(KeyCode.W))
			movement?.MoveForwards();
		if (Input.GetKey(KeyCode.S))
			movement?.MoveBackwards();
		if (Input.GetKey(KeyCode.D))
			movement?.MoveRight();
		if (Input.GetKey(KeyCode.A))
			movement?.MoveLeft();
		if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			weapon.Fire();
		}

		gunTransform?.SetTarget(new Vector3(GameOvermind.instance.mouseRaycaster.planeHit.x, gunTransform.transform.position.y, GameOvermind.instance.mouseRaycaster.planeHit.z));
	}
}
