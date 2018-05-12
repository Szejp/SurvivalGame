using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveObject : MonoBehaviour {

    public int depthLimit = 2;
    public int iterations = 1;
    public int actualDepth = 0;
    public float spawnProbibility = 1f;
    public string objectName = "RecursiveObject";

    protected void GenerateRecursively(Action CreateMethod) {
        if (actualDepth < depthLimit) {
            for (int i = 0; i < iterations; i++) {
                CreateMethod.Invoke();
            }
        }
    }

    protected T Create<T>() where T : RecursiveObject {
        T obj = new GameObject(objectName).AddComponent<T>();
        obj.gameObject.transform.parent = gameObject.transform;
        obj.iterations = iterations;
        obj.depthLimit = depthLimit;
        obj.spawnProbibility = spawnProbibility;
        obj.actualDepth = actualDepth + 1;
        return obj;
    }
}