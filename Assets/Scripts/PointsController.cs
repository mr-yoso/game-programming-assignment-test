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

    void Start()
    {
        startPosition = transform.position;
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

        if (GameManager.instance != null)
        {
            GameManager.instance.IncrementScore();
        }

        Destroy(gameObject, 1f);
    }
}
