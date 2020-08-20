using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip click, explosion, lose, timer;
    private AudioSource soundSource;
    public AudioClip[] music;
    public AudioSource musicSource;
    static public SoundManager instance;
    private void Awake()
    {
        if(SoundManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        SoundManager.instance = this;
    }
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource.clip = music[Random.Range(0, music.Length)];
        musicSource.Play();
    }
    void Update()
    {

        soundSource.volume = PlayerPrefs.GetFloat("Sound");
        musicSource.volume = PlayerPrefs.GetFloat("Music");
    }
    public void PlaySound(AudioClip requiredClip)
    {
        soundSource.clip = requiredClip;
        soundSource.Play();
    }
}
