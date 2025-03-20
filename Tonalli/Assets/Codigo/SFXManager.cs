using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Audio Sources")]
    private AudioSource audioSource;

    [Header("Sound Effects")]
    public AudioClip attackClip;
    public AudioClip jumpClip;
    public AudioClip damageClip;
    public AudioClip deathClip;
    public AudioClip itemPickupClip;
    // Agrega más clips según necesites...

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional si quieres que se mantenga entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Métodos para reproducir efectos específicos
    public void PlayAttack()
    {
        PlaySFX(attackClip);
    }

    public void PlayJump()
    {
        PlaySFX(jumpClip);
    }

    public void PlayDamage()
    {
        PlaySFX(damageClip);
    }

    public void PlayDeath()
    {
        PlaySFX(deathClip);
    }

    public void PlayItemPickup()
    {
        PlaySFX(itemPickupClip);
    }

    // Método general
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

