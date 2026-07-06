using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public int numberOfTrees = 10;
    public float zSpacing = 20f;

    void Start()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            float zPos = i * zSpacing;

            // Spawn left side tree
            Instantiate(treePrefab,
                new Vector3(-25f, 0f, zPos),
                Quaternion.identity);

            // Spawn right side tree
            Instantiate(treePrefab,
                new Vector3(25f, 0f, zPos),
                Quaternion.identity);
        }
    }
}