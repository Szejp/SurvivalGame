using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureToHightMap : MonoBehaviour {

	public Camera cam;

	[SerializeField]
	private string _propertyName;

	private Material _mainMat;
	private Material _mainMaterial {
		get {
			if (_mainMat == null) _mainMat = GetComponent<Renderer>().material;
			return _mainMat;
		}
	}

	private RenderTexture _rt;
	private Texture2D _tex;

	private void Start() {
		_rt = new RenderTexture(1024, 1024, 0);
		cam.targetTexture = _rt;
		cam.Render();
		_tex = new Texture2D(_rt.width, _rt.height, TextureFormat.ARGB32, false);
	}

	private void Update() {
		_mainMaterial.SetTexture(_propertyName, _tex);
		RenderTexture.active = _rt;
		_tex.ReadPixels(new Rect(0, 0, _rt.width, _rt.height), 0, 0);
		_tex.Apply();
		RenderTexture.active = null;
	}
}
