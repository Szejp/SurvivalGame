using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

public class FloatLerperExample : MonoBehaviour {

	private FloatLerper floatLerper {
		get {
			if (_floatLerper == null) _floatLerper = GetComponent<FloatLerper>();
			return _floatLerper;
		}
	}

	private FloatLerper _floatLerper;
	private IEnumerable<FieldInfo> _infos;

	// Use this for initialization
	private void Start() {
		_infos = floatLerper.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
		Debug.Log(_infos.Count());
	}


	int increment = 0;
	// Update is called once per frame
	private void Update() {
		foreach (FieldInfo f in _infos) {
			var time1 = Time.realtimeSinceStartup;
			f.SetValue(floatLerper, increment % 2000);
			var time2 = Time.realtimeSinceStartup;
			var time3 = Time.realtimeSinceStartup;
			var d1 = time2 - time1;
			var d2 = time3 - time2;
			Debug.Log(string.Format("times, set value: {0} mathf.lerp {1}", d1, d2));
			Debug.Log(f.GetType());
		}

		increment++;
	}
}
