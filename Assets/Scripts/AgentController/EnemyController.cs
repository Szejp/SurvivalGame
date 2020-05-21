using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AgentController {

	[SerializeField]
	private float _shootTimer = 2;
	private float _timer;
	private float _distance = 10;
	private NavMeshMovement navMeshMovement {
		get {
			return ((NavMeshMovement)movement);
		}
	}

	private void OnEnable() {
		_timer = Time.realtimeSinceStartup;
	}

	private void Update() {
		gunTransform?.SetTarget(GameOvermind.instance.players[0].transform.position);
		((NavMeshMovement)movement).MoveToDestination(GameOvermind.instance.players[0].transform.position);

		if (weapon.GetType() == typeof(FireWeapon)) {
			_distance = 10;
		}
		else
			_distance = 2;

		if (navMeshMovement.agent.remainingDistance < _distance) {
			navMeshMovement.agent.isStopped = true;
		}
		else {
			navMeshMovement.agent.isStopped = false;
		}

		if (weapon.GetType() == typeof(FireWeapon) && Time.realtimeSinceStartup - _timer > _shootTimer) {
			_timer = Time.realtimeSinceStartup;
			weapon.Fire();
		}
		else if (navMeshMovement.agent.remainingDistance < _distance) {
			weapon.Fire();
		}
	}
}
