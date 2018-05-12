using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DragonNavMeshMovement : MonoBehaviour {

    public Transform mapPivotReference;
    [HideInInspector]
    public NavMeshAgent agent;

    public bool IsDestinationReached(float distanceThreashold) {
        if (Vector3.SqrMagnitude(agent.destination - transform.position) < distanceThreashold * distanceThreashold) return true;
        else return false;
    }

    public void MoveToClosestPosition(Vector3 position) {
        try {
            agent.SetDestination(GetClosestPointOnMap(position, mapPivotReference.localScale.magnitude * 2));
        }
        catch { }
    }

    [ContextMenu("SetRandomPosition")]
    public void RandomTargetPos() {
        Vector3 targetPos = RandomPointOnMap(3 << 4);
        agent.SetDestination(targetPos);
    }

    private Vector3 GetClosestPointOnMap(Vector3 position, float maxDistance) {
        NavMeshHit navMeshHit = new NavMeshHit();
        NavMesh.SamplePosition(position, out navMeshHit, maxDistance, NavMesh.AllAreas);
        return navMeshHit.position;
    }

    private Vector3 RandomPointOnMap(int areaMask) {
        bool foundPos = false;
        NavMeshHit navMeshHit = new NavMeshHit();
        int reTryCount = 0;
        while (!foundPos || reTryCount < 10) {
            Vector2 randomCirclePos = Random.insideUnitCircle;
            Vector3 targetPos = new Vector3(randomCirclePos.x * mapPivotReference.localScale.x * 0.5f, 0, randomCirclePos.y * mapPivotReference.localScale.z * 0.5f) + mapPivotReference.position;
            foundPos = NavMesh.SamplePosition(targetPos, out navMeshHit, mapPivotReference.localScale.magnitude * 2, areaMask);
            reTryCount++;
        }
        return foundPos ? navMeshHit.position : Vector3.zero;
    }

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
}
