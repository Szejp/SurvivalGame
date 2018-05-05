using UnityEngine;
using System.Collections;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {

	public static T instance { get; private set; }

	public static IEnumerator WaitOnInstance() {
		while (instance == null) {
			yield return null;
		}
	}

	protected virtual void Awake() {
		if (instance != null && instance != this)
			Destroy(gameObject);
		instance = this as T;
	}
}