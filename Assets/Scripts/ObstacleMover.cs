using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 15f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move obstacle toward player
        transform.position += Vector3.back * 
            speed * Time.deltaTime;

        // Destroy when passed player
        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }
}