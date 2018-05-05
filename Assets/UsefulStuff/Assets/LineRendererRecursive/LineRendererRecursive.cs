using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererRecursive : RecursiveObject {

    public LineRenderer lineRenderer {
        get {
            if (_lineRenderer == null) _lineRenderer = GetComponent<LineRenderer>();
            return _lineRenderer;
        }
    }


    protected void Create() {
        var obj = Create<LineRendererRecursive>();
        Vector3 startingPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        obj.lineRenderer.SetPositions(new Vector3[] { startingPos, startingPos + Transforms.GetRandomDirection() });
        obj.lineRenderer.material = lineRenderer.material;
        obj.lineRenderer.widthMultiplier = lineRenderer.widthMultiplier;
    }

    protected void CreateSameRenderer(Func<Vector3> GetPositonMethod) {
        lineRenderer.positionCount = depthLimit;
        Vector3 startingPos = iterator != 0 ? lineRenderer.GetPosition(iterator - 1) : transform.position;
        lineRenderer.SetPosition(iterator, startingPos + GetPositonMethod());
        lineRenderer.material = lineRenderer.material;
        lineRenderer.widthMultiplier = lineRenderer.widthMultiplier;
        iterator++;
        if (iterator < depthLimit) {
            CreateSameRenderer(GetPositonMethod);
        }
    }

    private LineRenderer _lineRenderer;
    private static int iterator = 0;

    private void Start() {
        GenerateRecursively(() => CreateSameRenderer(Transforms.GetRandomDirection));
    }
}
