using UnityEngine;

public class PlayParticlesOnTouch : MonoBehaviour
{
    public ParticleSystem particleEffect; // Reference to the particle system

    void Start()
    {
        // Ensure the particle system exists and is attached
        if (particleEffect == null)
        {
            particleEffect = GetComponentInChildren<ParticleSystem>();

            if (particleEffect == null)
            {
                Debug.LogError("No ParticleSystem found on " + gameObject.name + ". Please attach one.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Play the particle effect
            if (particleEffect != null)
            {
                particleEffect.Play();
            }
        }
    }
}
