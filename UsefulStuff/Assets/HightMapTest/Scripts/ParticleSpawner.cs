using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject _particleToSpawn;
    [SerializeField]
    private Camera _camera;

    // Use this for initialization
    private void Awake() {
        ScreenInput.OnPointerUpEvent += SpawnParticle;
    }

    private void SpawnParticle(Vector2 screenPosition) {
        Debug.Log("Pointer up: x:" + screenPosition.x + " y: " + screenPosition.y);
        _particleToSpawn.SetActive(false);
        Vector3 pos = _camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10));
        Debug.Log("Pos: " + pos);
        _particleToSpawn.transform.position = pos + new Vector3(0, 0, 10);
        _particleToSpawn.SetActive(true);
    }
}
