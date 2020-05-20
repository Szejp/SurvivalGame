using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour {

	[SerializeField]
	private GameObject cubeToSpawn;
	[SerializeField]
	private float volume = 100;
	[SerializeField]
	private float count = 100;
	[SerializeField]
	private float space = 10;

	private List<GameObject> cubes = new List<GameObject>();

	[ContextMenu("Generate")]
	public void Generate() {
		for(int i = 0; i < count; i++) {
			for (int j = 0; j < 10; j++) {
				Vector3 pos = transform.position + new Vector3(Random.Range(-volume / 2, volume /2 ), Random.Range(-volume / 2, volume / 2), Random.Range(-volume / 2, volume / 2));
				if (Physics.CheckBox(pos, space * Vector3.one / 2)) continue;
				cubes.Add(Instantiate(cubeToSpawn, pos, Quaternion.identity, transform));
			}
		}
	}

	[ContextMenu("Clear")]
	public void Clear() {
		foreach(var t in cubes) {
			DestroyImmediate(t);
		}
		cubes.Clear();
	}
}
