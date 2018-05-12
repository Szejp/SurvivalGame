using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtils : MonoBehaviour {

	public static Vector3 RandomPointOnMap(int areaMask, float scaleX, float scaleZ, float magnitude, Vector3 centerPosition) {
		bool foundPos = false;
		NavMeshHit navMeshHit = new NavMeshHit();
		int reTryCount = 0;
		while (!foundPos && reTryCount < 10) {
			Vector2 randomCirclePos = Random.insideUnitCircle;
			Vector3 targetPos = new Vector3(randomCirclePos.x * scaleX * 0.5f, 0, randomCirclePos.y * scaleZ * 0.5f) + centerPosition;
			foundPos = NavMesh.SamplePosition(targetPos, out navMeshHit, magnitude * 2, areaMask);
			reTryCount++;
		}

		if (foundPos) {
			return navMeshHit.position;
		}
		else {
			Debug.Log("[NavMeshMovement] Could not find random point on map");
			return Vector3.zero;
		}
	}
}
