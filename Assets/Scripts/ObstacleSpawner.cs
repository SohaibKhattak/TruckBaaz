using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject busPrefab;
    public GameObject barrierPrefab;
    public float spawnInterval = 3f;
    private float timer;
    private float timeElapsed;

    private float[] lanePositions = { -10f, 0f, 10f };
    public float obstacleSpeed = 15f;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        int score = ScoreManager.instance != null ?
            ScoreManager.instance.GetScore() : 0;

        // Speed = mix of time AND score
        float speedFromTime = timeElapsed * 0.2f;
        float speedFromScore = score * 0.2f;
        obstacleSpeed = 15f + speedFromTime + speedFromScore;

        // Spawn interval = mix of time and score
        float intervalFromTime = timeElapsed * 0.01f;
        float intervalFromScore = score * 0.01f;
        spawnInterval = Mathf.Max(1f,
            3f - intervalFromTime - intervalFromScore);

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBuses(score);
            timer = 0f;
        }
    }

    void SpawnBuses(int score)
    {
        // Decide how many buses — MAXIMUM 2
        int busCount = 1;
        if (score >= 40 && timeElapsed >= 30f)
        {
            if (Random.Range(0, 2) == 0)
                busCount = 2;
        }
        else if (score >= 20 || timeElapsed >= 20f)
        {
            if (Random.Range(0, 3) == 0)
                busCount = 2;
        }

        // Shuffle lanes
        int[] lanes = { 0, 1, 2 };
        ShuffleLanes(lanes);

        // Track which lanes have buses
        bool[] laneOccupied = { false, false, false };

        for (int i = 0; i < busCount; i++)
        {
            laneOccupied[lanes[i]] = true;
            float xPosition = lanePositions[lanes[i]];

            Vector3 spawnPosition = new Vector3(
                xPosition,
                -0.5f,
                transform.position.z
            );

            GameObject bus = Instantiate(
                busPrefab,
                spawnPosition,
                Quaternion.identity
            );

            ObstacleMover mover = bus.AddComponent<ObstacleMover>();
            mover.speed = obstacleSpeed;

            // Random chance to throw barrier — 40% chance per bus
            if (barrierPrefab != null && Random.Range(0, 10) < 4)
            {
                SpawnBarrier(laneOccupied);
            }
        }
    }

    void SpawnBarrier(bool[] laneOccupied)
    {
        // Find a FREE lane for barrier
        // Barrier goes in different lane than bus
        int[] freeLanes = new int[3];
        int freeCount = 0;

        for (int i = 0; i < 3; i++)
        {
            if (!laneOccupied[i])
            {
                freeLanes[freeCount] = i;
                freeCount++;
            }
        }

        if (freeCount == 0) return;

        // Pick random free lane
        int randomFree = Random.Range(0, freeCount);
        int barrierLane = freeLanes[randomFree];

        // Spawn barrier slightly ahead of bus
        Vector3 barrierPosition = new Vector3(
            lanePositions[barrierLane],
            0.5f,
            transform.position.z + 10f
        );

        GameObject barrier = Instantiate(
            barrierPrefab,
            barrierPosition,
            Quaternion.identity
        );

        // Barrier moves at same speed as bus
        ObstacleMover mover = barrier.GetComponent<ObstacleMover>();
        if (mover != null)
            mover.speed = obstacleSpeed;
    }

    void ShuffleLanes(int[] lanes)
    {
        for (int i = 0; i < lanes.Length; i++)
        {
            int randomIndex = Random.Range(i, lanes.Length);
            int temp = lanes[i];
            lanes[i] = lanes[randomIndex];
            lanes[randomIndex] = temp;
        }
    }
}