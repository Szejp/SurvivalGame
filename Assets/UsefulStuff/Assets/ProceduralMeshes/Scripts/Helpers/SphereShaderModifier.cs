using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SphereShaderModifier : MonoBehaviour {

    public GameObject sphereObj;
    public GameObject grid;
    public float range;

    private Material _mat;

    private void Awake() {
        _mat = grid.GetComponent<Renderer>().material;
    }

    private void Update() {
        _mat.SetVector("_SphereOrigin", sphereObj.transform.position);
        _mat.SetFloat("_Range", range);
    }
}
