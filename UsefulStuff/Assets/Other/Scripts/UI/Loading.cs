using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

	private Image img;
	private Vector3 angles;

	private void Start() {
		img = GetComponent<Image>();
		angles = new Vector3(0, 0, 5);
	}

	private void Update() {
		img.rectTransform.Rotate(angles);
	}
}
