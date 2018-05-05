using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algebry : MonoBehaviour {

	private Vector3 point;

	[ContextMenu("GetSpherePoint")]
	public void GetSpherePoint() {
		point = new Vector3(110, 100, (float)GetSpherePoint(new float[] { 110, 100 }, new float[] { 100, 100, 100 }, 10));
		Debug.Log("[MathUtis] " + point);
	}

	public static float? GetSpherePoint(float[] knownValues, float[] origin, float range) {
		float sqrtEq = Mathf.Pow(range, 2) - Mathf.Pow(knownValues[0] - origin[0], 2) - Mathf.Pow(knownValues[1] - origin[1], 2);
		if (sqrtEq >= 0) {
			return Mathf.Sqrt(sqrtEq) + origin[2];
		}
		else {
			Debug.LogError("[Algebry] That point is not part of the sphere ");
			return null;
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(point, 0.1f);
	}
}
