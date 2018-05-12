using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

public class Test : MonoBehaviour {

	public UnityEngine.UI.Text text;

	private void Start() {
		foreach (Display d in Display.displays) {
			d.Activate();
		}
	}

	private void Update() {

		if (Input.GetKeyUp(KeyCode.Space)) {
			DisplayCounter.Count();
		}

		if (Input.GetKeyUp(KeyCode.M)) {
			DisplayCounter.RefreshUnityDisplaysCount();
		}

		string s = "";
		foreach (var d in Display.displays) {
			s += d.systemWidth + "x" + d.systemHeight + "\n";
		}

		foreach (var ptr in DisplayCounter.windowPointers) {
			s += "ptr: " + ptr.ToString() + "\n";
		}

		s += Display.displays.Length.ToString(); ;
		text.text = s;
	}
}
