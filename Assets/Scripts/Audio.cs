using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip playerHit;

    public void PlayerHit()
    {
        audioSource.clip = playerHit;
        audioSource.Play();
    }
}
