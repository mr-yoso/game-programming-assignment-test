using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatAmplitude = 0.5f;  // How high and low the object floats
    public float floatSpeed = 1.0f;  // Speed of the floating motion

    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the object
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Apply a sine wave to the Y-axis to create a floating effect
        float newY = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.localPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
    }
}
