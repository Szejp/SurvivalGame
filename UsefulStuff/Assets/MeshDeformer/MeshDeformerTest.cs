using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshDeformerTest : MonoBehaviour {

	private Mesh mesh;
	private MeshFilter filter;

	private void Update() {
		if (Input.GetKey(KeyCode.D)) {
			Deform();
		}
	}

	[ContextMenu("Deform")]
	public void Deform() {
		filter = GetComponent<MeshFilter>();
		mesh = filter.mesh;
		Vector3[] newVerts = new Vector3[mesh.vertices.Length];

		var groups = GetSimilarVectorGroups(mesh.vertices);
		int i = 0;
		foreach (var g in groups) {
			foreach (var v in GetChangedVectorGroup(g, g[0])) {
				newVerts[i] = v;
				i++;
			}
		}

		mesh.vertices = newVerts;
		mesh.RecalculateBounds();
		mesh.RecalculateTangents();
		mesh.RecalculateNormals();
		filter.mesh = mesh;
	}

	private Vector3[] GetChangedVectorGroup(Vector3[] group, Vector3 sample) {
		Vector3[] newGroup = new Vector3[group.Length];
		for (int i = 0; i < group.Length; i++) {
			var v = group[i];
			v = new Vector3(v.x + Mathf.PerlinNoise(sample.x * 1213, sample.y * sample.y) - .5f, v.y, v.z + Mathf.PerlinNoise(sample.z * 1213, sample.z * sample.y));
			newGroup[i] = v;
		}
		return newGroup;
	}

	private List<Vector3[]> GetSimilarVectorGroups(Vector3[] vertices) {
		List<Vector3[]> groups = new List<Vector3[]>();
		List<float> heights = new List<float>();
		foreach (var v in vertices) {
			if (!heights.Contains(v.y)) {
				heights.Add(v.y);
			}
		}

		foreach (var h in heights) {
			groups.Add(vertices.Where(p => p.y == h).ToArray());
		}

		return groups;
	}

	private void OnDrawGizmos() {
		if (mesh != null && mesh.vertices != null)
			foreach (var v in mesh.vertices) {
				//	Gizmos.DrawSphere(v, 0.2f);
			}
	}
}
