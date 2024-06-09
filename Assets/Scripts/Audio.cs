using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip playerHit,parry,playerMovement,shootLRT;

    public void PlayerHit()
    {
        audioSource.clip = playerHit;
        audioSource.Play();
    }
    public void Parry()
    {
        audioSource.clip = parry;
        audioSource.Play();
    }
    public void PlayerMovement()
    {
        audioSource.clip = playerMovement;
        audioSource.Play();
    }
    public void ShootLRT()
    {
        audioSource.clip = shootLRT;
        audioSource.Play();
    }
}
