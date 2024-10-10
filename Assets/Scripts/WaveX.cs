using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveX : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float speed = 1f;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Floating effect (up and down)
        float newX = startPosition.x + amplitude * Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(newX, startPosition.y, startPosition.z);
    }
}
