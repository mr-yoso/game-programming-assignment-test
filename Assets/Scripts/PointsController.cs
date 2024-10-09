using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float speed = 1f;
    public float rotationSpeed = 30f;
    public int points = 50;

    public GameObject collectiblePrefab; // Reference to the collectible prefab
    public Vector3[] spawnPositions; // An array of predefined positions for spawning

    private Vector3 startPosition; // Position to store the starting position of the collectible
    private ParticleSystem powerUpParticles;
    private Collider powerUpCollider;
    private Renderer powerUpRenderer;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        SpawnCollectibles();
        powerUpParticles = GetComponent<ParticleSystem>();
        powerUpCollider = GetComponent<Collider>();
        powerUpRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Floating effect (up and down)
        float newY = startPosition.y + amplitude * Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Add rotation
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void SpawnCollectibles()
    {
        foreach (Vector3 position in spawnPositions)
        {
            Instantiate(collectiblePrefab, position, Quaternion.identity);
        }
    }

    public void Collect()
    {
        if (powerUpParticles != null)
        { 
            powerUpParticles.Play();
        }

        powerUpCollider.enabled = false;
        powerUpRenderer.enabled = false;

        // Check if GameManager is correctly assigned
        if (GameManager.instance != null)
        {
            GameManager.instance.IncrementScore(points); // Increment score if GameManager exists
        }

        Destroy(gameObject, 1f);
    }
}
