using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveX : MonoBehaviour
{
    public float distance = 2.0f;   // Maximum distance for left-right movement
    public float speed = 1.0f;      // Speed of the movement
    public GameObject emptyObject;  // Reference to the empty object

    public bool moveInOppositeDirection = false;  // Flag to toggle between normal and opposite movement

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // Store the initial position of the platform
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the movement offset using PingPong to move smoothly back and forth
        float offset = Mathf.PingPong(Time.time * speed, distance);

        // Set the movement direction based on the flag (normal or opposite)
        Vector3 direction = moveInOppositeDirection ? -transform.right : transform.right;

        // Move the object in the chosen direction based on the calculated offset
        transform.position = startPosition + direction * offset;

        // Synchronize the empty object's position with the current platform position
        emptyObject.transform.position = transform.position;
    }

    // This method can be used to toggle the movement direction at runtime
    public void ToggleDirection()
    {
        moveInOppositeDirection = !moveInOppositeDirection;
    }
}
