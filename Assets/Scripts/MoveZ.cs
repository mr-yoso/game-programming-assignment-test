using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZ : MonoBehaviour
{
    public float distance = 2.0f;   // Maximum distance for forward-backward movement
    public float speed = 1.0f;      // Speed of the movement
    public GameObject emptyObject;  // Reference to the empty object

    public bool moveInOppositeDirection = false;  // Flag to toggle between forward and backward movement

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;  // Store the initial position
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the movement offset using PingPong for forward and backward movement
        float offset = Mathf.PingPong(Time.time * speed, distance);

        // Set the movement direction based on the flag (forward or backward)
        Vector3 direction = moveInOppositeDirection ? -transform.forward : transform.forward;

        // Move the object along the Z-axis (forward or backward) based on the offset and direction
        transform.position = startPosition + direction * offset;

        // Synchronize the empty object's position with the current object's position
        emptyObject.transform.position = transform.position;
    }

    // Method to toggle the movement direction at runtime
    public void ToggleDirection()
    {
        moveInOppositeDirection = !moveInOppositeDirection;
    }
}
