using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip jumpSFX;
    public float jumpVol;

    public AudioClip jumpImpactSFX;
    public float jumpImpactVol;

    public AudioClip deathSFX;
    public float deathVol;

    public AudioClip hurtSFX;
    public float hurtVol;

    public AudioClip healSFX;
    public float healVol;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSFX, jumpVol);
    }

    public void PlayJumpImpactSound()
    {
        PlaySound(jumpImpactSFX, jumpImpactVol);
    }


    public void PlayHurtSound()
    {
        PlaySound(hurtSFX, hurtVol);
    }

    public void PlayHealSound()
    {
        PlaySound(healSFX, healVol);
    }

    public void PlayDeathSound()
    {
        PlaySound(deathSFX, deathVol);
    }

    void PlaySound(AudioClip clip, float vol)
    {
        _audio.PlayOneShot(clip, vol);
    }
}
