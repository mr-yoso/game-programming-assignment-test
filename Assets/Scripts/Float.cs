using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float amplitude = 0.5f;  // How far the object moves up and down
    public float speed = 1f;        // Speed of movement

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the sine wave for Y axis movement
        float waveOffset = amplitude * Mathf.Sin(Time.time * speed);

        // Move the object along its local y-axis (the direction it is facing)
        Vector3 direction = transform.up;  // Local y-axis direction
        transform.position = startPosition + direction * waveOffset;
    }
}
