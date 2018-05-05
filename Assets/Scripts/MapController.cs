using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour {

	public Transform _mapPivotReference;

	public Vector3 GetRandomPointOnMap() {
		return NavMeshUtils.RandomPointOnMap(NavMesh.AllAreas, _mapPivotReference.localScale.x, _mapPivotReference.localScale.z, _mapPivotReference.localScale.magnitude, _mapPivotReference.transform.position);
	}
}
