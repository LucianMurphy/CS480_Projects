using UnityEngine;

public class AudioTriggerScript : MonoBehaviour
{
    AudioSource audioSource;
    Collider collider;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider collider) {
        audioSource.Play();
    }
}
