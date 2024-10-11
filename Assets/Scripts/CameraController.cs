using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset distance between player and camera

    public float rotationSpeed = 5f; // Speed of camera rotation
    public float zoomSpeed = 2f; // Speed of camera zooming
    public float minZoomDistance = 2f; // Minimum zoom distance
    public float maxZoomDistance = 10f; // Maximum zoom distance

    private float currentYaw = 0f; // Current rotation angle on the Y-axis
    private float currentPitch = 0f; // Current rotation angle on the X-axis
    public float minPitch = -30f; // Minimum vertical rotation angle
    public float maxPitch = 60f;  // Maximum vertical rotation angle

    private void Start()
    {
        // Ensure the camera starts at the desired offset from the player
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // Handle camera zoom
        HandleZoom();

        // Rotate the camera based on mouse input
        RotateCamera();

        // Follow the player by maintaining the offset position
        FollowPlayer();
    }

    // Method to follow the player's position
    private void FollowPlayer()
    {
        // Recalculate position based on updated offset (for zooming)
        transform.position = player.position + Quaternion.Euler(currentPitch, currentYaw, 0) * offset;

        // Always look at the player
        transform.LookAt(player);
    }

    private void RotateCamera()
    {
        // Get mouse input for both X and Y axes
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Adjust yaw (Y-axis rotation) and pitch (X-axis rotation) based on mouse input
        currentYaw += mouseX * rotationSpeed;

        // Update pitch based on mouse input and clamp it to prevent flipping
        currentPitch -= mouseY * rotationSpeed; // Subtract to reverse the direction for mouse input
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch); // Clamp pitch within range

        // Apply the rotation to the camera
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        transform.position = player.position + rotation * offset;

        // Make the camera look at the player
        transform.LookAt(player);
    }

    // Method to handle camera zooming
    private void HandleZoom()
    {
        // Get the scroll wheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Adjust the offset's magnitude (distance) based on scroll input
        float newDistance = offset.magnitude - scroll * zoomSpeed;

        // Clamp the new distance within the min and max zoom distances
        newDistance = Mathf.Clamp(newDistance, minZoomDistance, maxZoomDistance);

        // Update the offset to match the new distance
        offset = offset.normalized * newDistance;
    }
}
