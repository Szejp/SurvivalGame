using UnityEngine;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Cube : ProceduralMesh {

	public int xSize, ySize, zSize;

	private Mesh _mesh;
	private Vector3[] _vertices;
	private int[] _faces;

	private const float _noiseSpeed = 0.2f;

	public override void Generate() {
		GenerateSimpleCube();
	}

	private void Start() {
		Generate();
		StartCoroutine(Noise());
	}

	private IEnumerator Noise() {
		GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
		_mesh.name = "Procedural Cube";
		_mesh.vertices = _vertices;
		_mesh.triangles = _faces;
		_vertices = _vertices.OrderByDescending(p => p.y).ToArray();

		while (true) {
			for (int i = 0; i < 4; i++) {
				_vertices[i] = Vector3.Lerp(_vertices[i], new Vector3(_vertices[i].x, _vertices[i].y + 1, _vertices[i].z), Time.deltaTime * _noiseSpeed);
			}

			yield return new WaitForEndOfFrame();
		}
	}

	private void GenerateRoundCube(WaitForSeconds wait) {
		int cornerVertices = 8;
		int edgeVertices = (xSize + ySize + zSize - 3) * 4;
		int faceVertices = (
			(xSize - 1) * (ySize - 1) +
			(xSize - 1) * (zSize - 1) +
			(ySize - 1) * (zSize - 1)) * 2;
		_vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];

		int v = 0;
		for (int y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++) {
				_vertices[v++] = new Vector3(x, y, 0);
			}
			for (int z = 1; z <= zSize; z++) {
				_vertices[v++] = new Vector3(xSize, y, z);
			}
			for (int x = xSize - 1; x >= 0; x--) {
				_vertices[v++] = new Vector3(x, y, zSize);
			}
			for (int z = zSize - 1; z > 0; z--) {
				_vertices[v++] = new Vector3(0, y, z);
			}
		}

		_faces = new int[3] { 0, 1, 2 };
	}

	private void GenerateSimpleCube() {
		int verticesCout = xSize * ySize * zSize;
		_vertices = new Vector3[verticesCout];

		int iterator = 0;
		for (int i = 0; i < xSize; i++) {
			for (int b = 0; b < ySize; b++) {
				for (int c = 0; c < zSize; c++) {
					_vertices[iterator] = new Vector3(i, b, c);
					iterator++;
				}
			}
		}
	}

	private void OnDrawGizmos() {
		if (_vertices == null) {
			return;
		}

		Gizmos.color = Color.black;
		for (int i = 0; i < _vertices.Length; i++) {
			Gizmos.DrawSphere(_vertices[i], 0.1f);
		}
	}
}

