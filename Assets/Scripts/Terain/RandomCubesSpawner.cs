using UnityEngine;

public class RandomCubesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private Vector3 axises = new Vector3(45, .5f, 45);
    [SerializeField] private Transform terrainParent;
    [SerializeField] TextureToLevelConverter textureToLevelConverter;
    [SerializeField] Transform cubesParent;

    [ContextMenu("GenerateFromTex")]
    public void GenerateFromTex()
    {
        Clear();
        var data = textureToLevelConverter.GetLevel();

        for (int i = 0; i < data.size.x; i++)
        for (int j = 0; j < data.size.y; j++)
        {
            float result = data.values[j * (int) data.size.x + i];
            if (result != 1)
                InstantiateObject(new Vector3(i, axises.y, j));
        }
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        Clear();
        for (int i = (int) -axises.x; i < axises.x; i++)
        {
            for (int j = (int) -axises.z; j < axises.z; j++)
            {
                float result = Random.Range(0, 10);
                if (result == 1)
                    InstantiateObject(new Vector3(i, axises.y, j));
            }
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        foreach (var c in transform.GetComponentsInChildren<Transform>())
        {
            if (c.name == cube.name + "(Clone)")
                DestroyImmediate(c.gameObject);
        }
    }

    public void InstantiateObject(Vector3 position)
    {
        var transform = Instantiate(cube, Vector3.zero, Quaternion.identity, cubesParent).GetComponent<Transform>();
        transform.localPosition = new Vector3(position.x, position.y, position.z);
    }
}