using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public AudioClip collectSound;
    public AudioSource audioSource;

    private EndlessRunner playerScript; // Reference to the EndlessRunner script
    private Coroutine speedEffectCoroutine; // To manage speed effects (boost or slow down)

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Find the EndlessRunner script attached to the player (capsule)
        playerScript = FindObjectOfType<EndlessRunner>();
        if (playerScript == null)
        {
            Debug.LogError("EndlessRunner script not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerScript != null)
        {
            if (other.gameObject.name.Contains("Meat"))
            {
                // Destroy the meat object
                Destroy(other.gameObject);

                // Play the collect sound
                PlayCollectSound();

                // Apply the speed boost
                if (speedEffectCoroutine != null)
                {
                    StopCoroutine(speedEffectCoroutine); // Stop any ongoing speed effect
                }
                speedEffectCoroutine = StartCoroutine(ApplySpeedEffect(5f, 2f)); // 5 seconds, 2x speed
            }
            else if (other.gameObject.name.Contains("Cake"))
            {
                // Destroy the cake object
                Destroy(other.gameObject);

                // Play the collect sound
                PlayCollectSound();

                // Apply the slow-down effect
                if (speedEffectCoroutine != null)
                {
                    StopCoroutine(speedEffectCoroutine); // Stop any ongoing speed effect
                }
                speedEffectCoroutine = StartCoroutine(ApplySpeedEffect(5f, 0.5f)); // 5 seconds, 0.5x speed
            }
        }
    }

    private IEnumerator ApplySpeedEffect(float duration, float speedMultiplier)
    {
        float originalSpeed = playerScript.forwardSpeed; // Save the original speed

        // Adjust the player's speed
        playerScript.forwardSpeed *= speedMultiplier;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Reset the player's speed to the original value
        playerScript.forwardSpeed = originalSpeed;
    }

    private void PlayCollectSound()
    {
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
