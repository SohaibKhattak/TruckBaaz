using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int pointValue = 10;
    public float rotationSpeed = 90f;

    void Update()
    {
        // Spin the collectible
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Destroy if passed player
        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add points
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(pointValue);
                Debug.Log("Collected! +" + pointValue + " points");
            }

            // Play collect sound
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayCollect();
                Debug.Log("Playing collect sound");
            }
            else
            {
                Debug.Log("AudioManager is NULL!");
            }

            Destroy(gameObject);
        }
    }
}