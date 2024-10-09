using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset distance between player and camera

    public float rotationSpeed = 5f; // Speed of camera rotation

    private float currentYaw = 0f; // Current rotation angle on the Y-axis

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

        // Follow the player by maintaining the offset position
        FollowPlayer();

        // Rotate the camera around the player based on mouse input
        RotateCamera();
    }

    // Method to follow the player's position
    private void FollowPlayer()
    {
        transform.position = player.position + offset;
    }

    // Method to rotate the camera around the player
    private void RotateCamera()
    {
        // Get mouse input on the X-axis (Mouse X) and apply it to the camera's Y rotation (yaw)
        float mouseX = Input.GetAxis("Mouse X");

        // Adjust current yaw (Y-axis rotation) based on mouse input
        currentYaw += mouseX * rotationSpeed;

        // Rotate the camera around the player's Y-axis
        Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);

        // Apply rotation to the camera around the player
        transform.position = player.position + rotation * offset;

        // Always look at the player
        transform.LookAt(player);
    }
}
