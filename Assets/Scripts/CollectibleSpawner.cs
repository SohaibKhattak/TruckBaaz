using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;
    public float spawnInterval = 4f;
    private float timer;

    private float[] lanePositions = { -10f, 0f, 10f };

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCollectible();
            timer = 0f;
        }
    }

    void SpawnCollectible()
    {
        // Pick random lane
        int randomLane = Random.Range(0, 3);
        float xPosition = lanePositions[randomLane];

        // Pick random collectible type
        int randomType = Random.Range(0, collectiblePrefabs.Length);

        Vector3 spawnPosition = new Vector3(
            xPosition,
            1f,
            transform.position.z
        );

        Instantiate(
            collectiblePrefabs[randomType],
            spawnPosition,
            Quaternion.identity
        );
    }
}