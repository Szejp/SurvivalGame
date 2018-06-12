using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoSingleton<Spawner> {

	[Header("Enemies")]
	public Enemy enemy;
	public GameEntity boss;

	[SerializeField]
	private Transform[] spawnPlaces;
	[SerializeField]
	private ObjectPool pool;

	public Component Spawn(Component component) {
		if (component == null) return null;
		var result = pool.GetFreeObject(component);
		result.gameObject.SetActive(true);
		return result;
	}

	public Component Spawn(Component component, Vector3 position) {
		var result = Spawn(component);
		result.transform.position = position;
		return result;
	}

	public void Collect(GameEntity entity) {
		entity.Reset();
		Collect((Component)entity);
	}

	public void Collect(Component entity) {
		entity.gameObject.SetActive(false);
		pool.Collect(entity);
	}

	protected override void Awake() {
		base.Awake();
		GameEntity.OnEntityDestroyed += Collect;
	}

	private void Start() {
		StartCoroutine(GenerateObjectsTest());
	}

	private IEnumerator GenerateObjectsTest() {
		if (spawnPlaces != null && spawnPlaces.Length > 3) {
			while (true) {
				int count = 0;
				while (count < 40) {
					if (spawnPlaces != null && spawnPlaces.Length > 3)
						Spawn(enemy, spawnPlaces[count % 4].position);
					count++;
					yield return new WaitForSeconds(3);
				}


				Spawn(boss, GameOvermind.instance.mapController.GetRandomPointOnMap());
			}
		}
	}

	private void OnDrawGizmos() {
		foreach (var p in spawnPlaces) {
			Gizmos.DrawSphere(p.transform.position, 0.5f);
		}
	}
}
