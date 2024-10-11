using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZ : MonoBehaviour
{
    public float distance = 2.0f;   // Maximum distance for forward-backward movement
    public float speed = 1.0f;      // Speed of the movement
    public GameObject emptyObject;  // Reference to the empty object

    public bool moveInOppositeDirection = false;  // Flag to toggle between forward and backward movement
    public float delayBeforeMoving = 0f;        // Delay before movement starts

    private Vector3 startPosition;
    private bool isMoving = false;  // Flag to check if movement should start

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;  // Store the initial position
        StartCoroutine(StartMovementAfterDelay());  // Start the coroutine to delay the movement
    }

    // Coroutine to delay the start of the movement
    IEnumerator StartMovementAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeMoving);  // Wait for the specified delay
        isMoving = true;  // Allow the platform to start moving after the delay
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // Calculate the movement offset using PingPong for forward and backward movement
            float offset = Mathf.PingPong(Time.time * speed, distance);

            // Set the movement direction based on the flag (forward or backward)
            Vector3 direction = moveInOppositeDirection ? -transform.forward : transform.forward;

            // Move the object along the Z-axis (forward or backward) based on the offset and direction
            transform.position = startPosition + direction * offset;

            // Synchronize the empty object's position with the current object's position
            if (emptyObject != null)
            {
                emptyObject.transform.position = transform.position;
            }
        }
    }

    // Method to toggle the movement direction at runtime
    public void ToggleDirection()
    {
        moveInOppositeDirection = !moveInOppositeDirection;
    }
}
