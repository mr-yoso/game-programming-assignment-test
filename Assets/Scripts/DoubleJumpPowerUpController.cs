using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPowerUpController : MonoBehaviour
{
    public float respawnTime = 30.0f;

    private Vector3 startPosition;
    private Collider powerUpCollider;
    private Renderer powerUpRenderer;
    private ParticleSystem powerUpParticles;

    // Start is called before the first frame update
    private void Start()
    {
        startPosition = transform.position;
        powerUpCollider = GetComponent<Collider>();  // Get the Collider component
        powerUpRenderer = GetComponent<Renderer>();  // Get the Renderer component
        powerUpParticles = GetComponent<ParticleSystem>();
    }

    public void StartRespawn()
    {
        if (powerUpParticles != null)
        {
            powerUpParticles.Play();
        }

        powerUpCollider.enabled = false;
        powerUpRenderer.enabled = false;

        StartCoroutine(RespawnPowerUp());
    }

    private IEnumerator RespawnPowerUp()
    {
        yield return new WaitForSeconds(respawnTime);

        // Respawn the power-up at the original position
        transform.position = startPosition;  // Optionally reset the position if needed
        powerUpCollider.enabled = true;
        powerUpRenderer.enabled = true;
    }

}