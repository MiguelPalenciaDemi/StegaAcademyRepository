using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioPlayer;

    [SerializeField]
    AudioClip[] footsteps;

    [SerializeField]
    AudioClip[] attacks;
    [SerializeField]
    AudioClip[] hurts;
    [SerializeField]
    AudioClip[] death;

    public void PlayFootsteps()
    {
        int randomIndex = Random.Range(0, footsteps.Length);
        audioPlayer.clip = footsteps[randomIndex];
        audioPlayer.Play();

    }

    public void PlayAttacks()
    {
        int randomIndex = Random.Range(0, attacks.Length);
        audioPlayer.clip = attacks[randomIndex];
        audioPlayer.Play();

    }

    public void PlayDeath()
    {
        int randomIndex = Random.Range(0, death.Length);
        audioPlayer.clip = death[randomIndex];
        audioPlayer.Play();

    }

    public void PlayHurt()
    {
        int randomIndex = Random.Range(0, hurts.Length);
        audioPlayer.clip = hurts[randomIndex];
        audioPlayer.Play();

    }
}
