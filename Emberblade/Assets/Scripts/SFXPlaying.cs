using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPlaying : MonoBehaviour //Detta är skrivet av: Brandon
{
    [SerializeField] AudioSource parrySFX;
    [SerializeField] AudioSource toungeSFX;
    [SerializeField] AudioSource jumpSFX;
    [SerializeField] AudioSource attackSFX;

    public AudioMixer audioMixer;


    private void Start()
    {
        
    }

    public void PlayParry()
    {
        parrySFX.volume = 0.3f ;
        parrySFX.Play();
    }
    public void PlayTounge()
    {
        toungeSFX.volume = 1f;
        toungeSFX.Play();
    }
    public void PlayJump()
    {
        jumpSFX.volume = 0.5f;
        jumpSFX.Play();
    }
    public void PlayAttack()
    {
        attackSFX.volume = 0.5f;
        attackSFX.Play();
    }
}
