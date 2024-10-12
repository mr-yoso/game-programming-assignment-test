using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset;

    public float rotationSpeed = 5f;
    public float zoomSpeed = 2f;
    public float minZoomDistance = 2f;
    public float maxZoomDistance = 10f;

    private float currentYaw = 0f;
    private float currentPitch = 0f;
    public float minPitch = -30f;
    public float maxPitch = 60f;

    private void Start()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        HandleZoom();

        RotateCamera();

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = player.position + Quaternion.Euler(currentPitch, currentYaw, 0) * offset;

        transform.LookAt(player);
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentYaw += mouseX * rotationSpeed;

        currentPitch -= mouseY * rotationSpeed; // Subtract to reverse the direction for mouse input
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch); // Clamp pitch within range

        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        transform.position = player.position + rotation * offset;

        transform.LookAt(player);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        float newDistance = offset.magnitude - scroll * zoomSpeed;

        newDistance = Mathf.Clamp(newDistance, minZoomDistance, maxZoomDistance);

        offset = offset.normalized * newDistance;
    }
}
