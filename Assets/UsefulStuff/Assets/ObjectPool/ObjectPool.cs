using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	public Dictionary<string, Queue<int>> availableIndexes = new Dictionary<string, Queue<int>>();
	public Dictionary<string, List<Component>> pools = new Dictionary<string, List<Component>>();

	public Component GetFreeObject(Component objectToSpawn) {
		var index = GetIndex(objectToSpawn);
		var result = pools[objectToSpawn.gameObject.name][index];
		return result;
	}

	public void Collect(Component entity) {
		var pool = TryGetPool(entity);
		if (!pool.Contains(entity)) {
			pool.Add(entity);
		}

		var queue = TryGetDictionary(entity.gameObject.name);
		var index = pool.IndexOf(entity);
		if (!queue.Contains(index))
			queue.Enqueue(index);
	}

	public int GetIndex(Component objectToSpawn) {
		var queue = TryGetDictionary(objectToSpawn.gameObject.name);
		List<Component> list;

		if (queue.Count < 1) {
			list = TryGetPool(objectToSpawn);
			var newObj = CreateObject(objectToSpawn);
			newObj.gameObject.name = objectToSpawn.gameObject.name;
			list.Add(newObj);
			return list.Count - 1;
		}
		else {
			return queue.Dequeue();
		}
	}

	private Component CreateObject(Component objectToSpawn) {
		return (Component)Instantiate(objectToSpawn.gameObject).GetComponent(objectToSpawn.GetType());
	}

	private List<Component> TryGetPool(Component objectToSpawn) {
		try {
			return pools[objectToSpawn.gameObject.name];
		}
		catch {
			var type = objectToSpawn.gameObject.name;
			var list = new List<Component>();
			pools.Add(type, list);
			return list;
		}
	}

	private Queue<int> TryGetDictionary(string key) {
		if (!availableIndexes.ContainsKey(key)) {
			availableIndexes.Add(key, new Queue<int>());
		}

		return availableIndexes[key];
	}
}
