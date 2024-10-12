using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] points;
    public int pointNumber = 0;
    private Vector3 currentTarget;

    public float tolerance = 0.1f;
    public float speed;
    public float delayTime;

    private float delayStart;

    public bool automatic;

    private Transform platformTransform;

    // Dictionary to store the characters and their previous positions on the platform
    private Dictionary<Transform, Vector3> characterLastPositions = new Dictionary<Transform, Vector3>();

    private Vector3 lastPlatformPosition;

    // Start is called before the first frame update
    void Start()
    {
        platformTransform = transform;

        if (points.Length > 0)
        {
            currentTarget = points[0];
        }

        tolerance = Mathf.Max(tolerance, 0.1f);  // Ensure tolerance is always positive
        lastPlatformPosition = platformTransform.position;  // Track the initial position of the platform
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        if (Vector3.Distance(platformTransform.position, currentTarget) > tolerance)
        {
            MovePlatform();
            MoveCharacters();  // Update characters' positions based on platform movement
        }
        else
        {
            UpdateTarget();
        }

        lastPlatformPosition = platformTransform.position;  // Update the platform's last position for the next frame
    }

    private void MovePlatform()
    {
        platformTransform.position = Vector3.MoveTowards(platformTransform.position, currentTarget, speed * Time.deltaTime);

        if (Vector3.Distance(platformTransform.position, currentTarget) < tolerance)
        {
            platformTransform.position = currentTarget;
            delayStart = Time.time;
        }
    }

    private void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - delayStart > delayTime)
            {
                NextPlatform();
            }
        }
    }

    private void NextPlatform()
    {
        pointNumber++;
        if (pointNumber >= points.Length)
        {
            pointNumber = 0;
        }
        currentTarget = points[pointNumber];
    }

    private void MoveCharacters()
    {
        Vector3 platformDelta = platformTransform.position - lastPlatformPosition;  // Calculate how much the platform has moved

        foreach (var entry in characterLastPositions)
        {
            Transform character = entry.Key;
            Vector3 lastPosition = entry.Value;

            // Only update the character's position based on the platform's movement
            character.position += platformDelta;  // Apply the platform's movement to the character

            // The player can still move freely because we aren't overriding their input
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!characterLastPositions.ContainsKey(other.transform))
        {
            // Add the character to the tracking system
            characterLastPositions.Add(other.transform, other.transform.position);

            // Check if the object is a player and update the isFalling parameter in the PlayerAnimationManager
            Animator playerAnimator = other.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("IsFalling", false);  // Character is on the platform
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (characterLastPositions.ContainsKey(other.transform))
        {
            characterLastPositions.Remove(other.transform);
        }
    }

}
