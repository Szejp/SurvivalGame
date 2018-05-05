using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovement : Movement {

	public NavMeshAgent agent {
		get {
			return _agent;
		}
	}

	[SerializeField]
	private float _speed;
	[SerializeField]
	private Transform _mapPivotReference;
	[SerializeField]
	private bool isManual = false;
	private NavMeshAgent _agent;
	private Vector3 _agentDestination;

	public override void MoveLeft() {
		_agentDestination += Vector3.left * _speed;
	}

	public override void MoveRight() {
		_agentDestination += Vector3.right * _speed;
	}

	public override void MoveForwards() {
		_agentDestination += Vector3.forward * _speed;
	}

	public override void MoveBackwards() {
		_agentDestination += Vector3.back * _speed;
	}

	public override void Teleport(Vector3 position) {
		_agent.transform.position = position;
	}

	public override void MoveUp() { }
	public override void MoveDown() { }

	[ContextMenu("TeleportTest")]
	public void TeleporTest() {
		Teleport(Vector3.zero);
	}

	public void MoveToDestination(Vector3 destination) {
		_agentDestination = destination;
	}

	public void RandomTargetPos() {
		Vector3 targetPos = NavMeshUtils.RandomPointOnMap(NavMesh.AllAreas, _mapPivotReference.localScale.x, _mapPivotReference.localScale.z, _mapPivotReference.localScale.magnitude, _mapPivotReference.transform.position);
		_agent.SetDestination(targetPos);
	}

	private Vector3 GetClosestPointOnMap(Vector3 position, float maxDistance) {
		NavMeshHit navMeshHit = new NavMeshHit();
		NavMesh.SamplePosition(position, out navMeshHit, maxDistance, NavMesh.AllAreas);
		return navMeshHit.position;
	}

	private void Awake() {
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		_agent?.SetDestination(_agentDestination);
		transform.position = _agent.transform.position;
		transform.rotation = _agent.transform.rotation;
		if (isManual)
			_agentDestination = transform.position;
	}

	private void OnDrawGizmos() {
		Gizmos.DrawSphere(_agentDestination, 0.1f);
	}
}
