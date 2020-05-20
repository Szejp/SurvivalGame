using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 3f;
    [SerializeField] float minDistance = 10f;

    Vector3 _offset;
    Vector3 velocity;
    float time;

    void Start()
    {
        _offset = transform.position - target.transform.position;
    }

    void Update()
    {
        Vector3 targetPosition = target.transform.position + _offset;
        float modifier = Vector3.Magnitude(targetPosition - transform.position);

        if (time < smoothTime)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            time += Time.deltaTime;
        }
        else if (modifier > minDistance)
            time = 0;
    }
}