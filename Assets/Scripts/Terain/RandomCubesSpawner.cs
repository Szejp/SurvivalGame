using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCubesSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject cube;
	[SerializeField]
	private Vector3 axises = new Vector3(45, .5f, 45);
	[SerializeField]
	private Transform terrainParent;

	[ContextMenu("Generate")]
	public void Generate() {
		for (int i = (int)-axises.x; i < axises.x; i++) {
			for (int j = (int)-axises.z; j < axises.z; j++) {
				float result = Random.Range(0, 10);
				if (result == 1)
					Instantiate(cube, new Vector3(i, axises.y, j), Quaternion.identity, terrainParent);
			}
		}
	}

	[ContextMenu("Clear")]
	public void Clear() {
		foreach (var c in transform.GetComponentsInChildren<Transform>()) {
			if (c.name == cube.name + "(Clone)")
				DestroyImmediate(c.gameObject);
		}
	}
}
