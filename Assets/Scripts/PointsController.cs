using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    public int points = 50;

    private Vector3 startPosition;
    private ParticleSystem powerUpParticles;
    private Collider powerUpCollider;
    private Renderer powerUpRenderer;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        //SpawnCollectibles();
        powerUpParticles = GetComponent<ParticleSystem>();
        powerUpCollider = GetComponent<Collider>();
        powerUpRenderer = GetComponent<Renderer>();
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
