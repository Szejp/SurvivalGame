using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 1f;
    [SerializeField] float velocityModifier = 1f;
    [SerializeField] float minDistance = 2f;

    Vector3 _offset;
    Vector3 velocity;
    Vector3 targetVelocity;
    Vector3 previousTargetPos;
    Vector3 movePosition;

    float time;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Start()
    {
        _offset = transform.position - target.transform.position;
    }

    void Update()
    {
        if (previousTargetPos.Equals(Vector3.zero))
            previousTargetPos = target.transform.position;

        targetVelocity = target.transform.position - previousTargetPos;
        movePosition = target.transform.position + _offset + velocityModifier * targetVelocity.normalized;
        previousTargetPos = target.transform.position;

        float modifier = Vector3.Magnitude(movePosition - transform.position);

        if (time < smoothTime)
        {
            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, smoothTime);
            time += Time.deltaTime;
        }
        else if (modifier > minDistance)
            time = 0;
    }
}