using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceBell : MonoBehaviour
{
    public AudioClip bellRingClip; // Drag your bell ring audio clip here in the inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Auto-add AudioSource if it's missing
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            PlayBellRing();
        }
    }

    private void PlayBellRing()
    {
        if (bellRingClip != null)
        {
            audioSource.PlayOneShot(bellRingClip);
        }
        else
        {
            Debug.LogWarning("Bell ring clip not set!");
        }
    }
}
