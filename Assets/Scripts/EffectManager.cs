using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public float jumpEffectForce = 40f;
    public float speedEffectForce = 3f;

    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Capsule" && this.gameObject.name == "Meat")
        {
            other.gameObject.GetComponent<EndlessRunner>().speed += speedEffectForce;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            audioSource.Play();
            Destroy(this.gameObject, 2f);
        }
    }
}
