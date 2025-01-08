using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRunner : MonoBehaviour
{
    public float speed = 10f;
    public float forwardSpeed = 15f;
    public float leftLimit = -12f;
    public float rightLimit = 12f;

    public AudioClip collectSound;   // Reference to your existing sound
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        float xMov = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            xMov = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xMov = 1f;
        }

        Vector3 movement = new Vector3(xMov * speed, 0, forwardSpeed) * Time.deltaTime;
        transform.Translate(movement);

        float xLimit = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
        transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Lettuce"))
        {
            // Destroy the lettuce
            Destroy(other.gameObject);

            // Play your sound
            if (audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            else
            {
                Debug.Log("Audio source not found.");
            }
        }

        if (other.gameObject.name.Contains("Apple"))
        {
            // Destroy the lettuce
            Destroy(other.gameObject);

            // Play your sound
            if (audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            else
            {
                Debug.Log("Audio source not found.");
            }
        }


    }
}