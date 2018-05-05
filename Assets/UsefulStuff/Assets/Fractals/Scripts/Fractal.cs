using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    public int fractalsDepthLimit = 10;
    public Mesh[] meshes;
    public Material material;
    public float childScale = 0.5f;
    public int fractalIterations = 10;
    public int fractalsDepth = 0;
    public float _spawnProbibility = 1f;

    private static int _fractalsCount = 0;
    private static int _fractalsLimit;

    private void Start() {
        gameObject.AddComponent<MeshFilter>().mesh = GetRandomMesh();
        gameObject.AddComponent<MeshRenderer>().material = material;
        if (fractalsDepth < fractalsDepthLimit) {
            for (int i = 0; i < fractalIterations; i++) {
                if (i < Transforms.directions.Length && Random.Range(0, 1f) < _spawnProbibility) AddFractal(Transforms.directions[i]);
            }
        }
    }

    private void AddFractal(Vector3 direction) {
        Fractal fractal = new GameObject("Fractal Child").AddComponent<Fractal>();
        fractal.gameObject.transform.parent = gameObject.transform;
        fractal.meshes = meshes;
        fractal.fractalIterations = fractalIterations;
        fractal.fractalsDepthLimit = fractalsDepthLimit;
        fractal._spawnProbibility = _spawnProbibility;
        fractal.fractalsDepth = fractalsDepth + 1;
        fractal.material = new Material(material);
        fractal.material.color = GetRandomColor();
        fractal.transform.position = fractal.transform.localPosition + new Vector3(GetRandomRange(), GetRandomRange(), GetRandomRange());
        fractal.transform.localScale = Vector3.one * childScale;
        fractal.transform.localPosition = direction * (0.5f + 0.5f * childScale);
        _fractalsCount++;
    }

    private int GetRandomRange() {
        return (int)Random.Range(-1, 2);
    }

    private Color GetRandomColor() {
        return new Color(GetRandomColorFloat(), GetRandomColorFloat(), GetRandomColorFloat());
    }

    private float GetRandomColorFloat() {
        return (float)Random.Range(0, 255) / 255;
    }

    private Mesh GetRandomMesh() {
        return meshes[Random.Range(0, meshes.Length)];
    }
}