using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    public float speed = 10f;
    public float resetZ = -100f;
    public float startZ = 0f;

    void Update()
    {
        // Move road backward to create forward movement illusion
        transform.position += Vector3.back * speed * Time.deltaTime;

        // When road goes too far back, reset it to front
        if (transform.position.z <= resetZ)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                startZ
            );
        }
    }
}