using System.Collections;
using UnityEngine;

public class SpawnStrategy : MonoBehaviour
{
    [Header("Enemies")] public Enemy enemy;
    public GameEntity boss;

    [SerializeField] private Transform[] spawnPlaces;

    private void Start()
    {
        StartCoroutine(GenerateObjectsTest());
    }

    private IEnumerator GenerateObjectsTest()
    {
        if (spawnPlaces != null && spawnPlaces.Length > 3)
        {
            while (true)
            {
                int count = 0;
                while (count < 40)
                {
                    if (spawnPlaces != null && spawnPlaces.Length > 3)
                        GameOvermind.instance.spawner.Spawn(enemy, spawnPlaces[count % 4].position);
                    count++;
                    yield return new WaitForSeconds(3);
                }


                GameOvermind.instance.spawner.Spawn(boss, GameOvermind.instance.mapController.GetRandomPointOnMap());
            }
        }
    }
}