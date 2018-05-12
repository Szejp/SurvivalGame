using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class Grid : MonoBehaviour {

	public int xSize, ySize;
	public float deltaLength = 1;
	public float noiseDelta = 0.5f;

	private Mesh _mesh;

	private void Awake() {
		Generate();
	}

	private void Generate() {
		GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
		_mesh.name = "Procedural Grid";
		_mesh.vertices = GenerateVertices(xSize, ySize, deltaLength);
		_mesh.triangles = GenerateTriangles(xSize, ySize);
		_mesh.RecalculateNormals();
	}

	private void OnDrawGizmos() {
		if (_mesh.vertices == null || _mesh.vertices.Length == 0) {
			return;
		}

		Gizmos.color = Color.black;
		for (int i = 0; i < _mesh.vertices.Length; i++) {
			Gizmos.DrawSphere(_mesh.vertices[i], 0.1f);
		}
	}

	private int[] GenerateTriangles(int xSize, int ySize) {
		int[] triangles = new int[xSize * ySize * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
			for (int x = 0; x < xSize; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
				triangles[ti + 5] = vi + xSize + 2;
			}
		}

		return triangles;
	}

	private Vector3[] NoiseVertices(Vector3[] vertices) {
		for (int i = 0; i < vertices.Length; i += 3) {
			Vector3 v = vertices[i];
			vertices[i] = new Vector3(v.x, v.y, noiseDelta * Mathf.Sin(Time.realtimeSinceStartup));
		}

		return vertices;
	}

	private Vector3[] GenerateVertices(int xSize, int ySize, float deltaLength) {
		Vector3[] vertices = new Vector3[(xSize + 1) * (ySize + 1)];
		for (int i = 0, y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++, i++) {
				vertices[i] = new Vector3(x * deltaLength, y * deltaLength, 0);
			}
		}

		return vertices;
	}
}