using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sound : MonoBehaviour
{
    [Header("Som")]
    public AudioSource sfxSourceEffect;
    public AudioSource musicSource;
    [Space]
    [Header("Sfx Game")]
    public AudioClip SfxEffect;
    public AudioClip SfxMusic;


    // Start is called before the first frame update
    /*void Start()
    {
       // sfxSourceEffect = gameObject.GetComponent<AudioSource>();
    }*/

    // Update is called once per frame
    void Update()
    {
        // PlaySfx(SfxEffect, 1);

        // musicSource.Pause(); pausar a musica
        //musicSource.Play();
        //musicSource.Stop();
        // musicSource.UnPause(); despausar a musica
    }
    /// <summary>
    /// resposavel por receber execultar um som
    /// </summary>
    /// <param name="sfx">Recebe um AudioClip, é o audio que sera tocado</param>
    /// <param name="volume">Recebe um float, é o volume que o audio toca de 0 a 1.</param>
    public void PlaySfx(AudioClip sfx, float volume)
    {
        sfxSourceEffect.PlayOneShot(sfx, volume);
    }
}
