using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public enum QuaternionAxis
    {
        X,
        Y,
        Z
    }

    public Transform TransformToFollow { get; set; }

    [SerializeField]
    [Range(0, 360)]
    private float eulerX;

    [SerializeField]
    [Range(0, 360)]
    private float eulerY;

    [SerializeField]
    [Range(0, 360)]
    private float eulerZ;

    [SerializeField]
    private QuaternionAxis axis = QuaternionAxis.X;

    [SerializeField]
    private float range;

    [SerializeField]
    private Transform transformToFollow;

    private Vector3 pointToMove;
    private Vector3 positionToFollow = Vector3.zero;

    private Quaternion Quaternion
    {
        get
        {
            return Quaternion.Euler(this.eulerX, this.eulerY, this.eulerZ);
        }
    }

    public void ForceMoveCamera(Func<Vector3> CalculatePosition)
    {
        if (transformToFollow != null) positionToFollow = transformToFollow.position;
        transform.position = CalculatePosition();
        transform.LookAt(positionToFollow);
    }

    public Vector3 GetPositionInRange(float r, Vector3 center, QuaternionAxis quaternionAxis = QuaternionAxis.X)
    {
        switch (quaternionAxis)
        {
            case QuaternionAxis.X:
                return center + Quaternion * new Vector3((float)r, 0, 0);
            case QuaternionAxis.Y:
                return center + Quaternion * new Vector3(0, (float)r, 0);
            case QuaternionAxis.Z:
                return center + Quaternion * new Vector3(0, 0, (float)r);
        }

        return new Vector3(0, 0, 0);
    }

    public Vector3 GetPositionInRange()
    {
        switch (axis)
        {
            case QuaternionAxis.X:
                return positionToFollow + Quaternion * new Vector3(range, 0, 0);
            case QuaternionAxis.Y:
                return positionToFollow + Quaternion * new Vector3(0, range, 0);
            case QuaternionAxis.Z:
                return positionToFollow + Quaternion * new Vector3(0, 0, range);
        }

        return new Vector3(0, 0, 0);
    }

    public void SetTransformToFollow(Transform transf)
    {
        transformToFollow = transf;
        positionToFollow = transformToFollow.position;
    }

    public void SetPositionToFollow(Vector3 position)
    {
        positionToFollow = position;
    }

    public void SetAngles(float angleX, float angleY, float angleZ)
    {
        eulerX = angleX;
        eulerY = angleY;
        eulerZ = angleZ;
    }

    public void SetRange(float r)
    {
        range = r;  
    }
}
