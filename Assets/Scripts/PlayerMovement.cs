using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float[] lanePositions = { -10f, 0f, 10f };
    private int currentLane = 1;

    public float jumpForce = 18f;
    private bool isGrounded = true;
    public float laneSwitchSpeed = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move LEFT
        if (Keyboard.current.aKey.wasPressedThisFrame || 
            Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (currentLane > 0) currentLane--;
        }

        // Move RIGHT
        if (Keyboard.current.dKey.wasPressedThisFrame || 
            Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (currentLane < 2) currentLane++;
        }

        // JUMP
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Smoothly move to target lane
        Vector3 targetPosition = new Vector3(
            lanePositions[currentLane],
            transform.position.y,
            transform.position.z
        );
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * laneSwitchSpeed
        );

        // Add extra gravity for better jump feel
            rb.AddForce(Vector3.down * 10f * Time.deltaTime, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}