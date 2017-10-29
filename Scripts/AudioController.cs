using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioClip[] enemySpawn;
    public AudioClip[] playerSpawn;
    public AudioClip[] enemyDeath;
    public AudioClip[] playrDeath;
    public AudioClip coinSpawn;
    public AudioClip coinCatch;


    public void Play(AudioClip sound, GameObject target )
    {
        target.gameObject.GetComponent<AudioSource>().clip = sound;
        target.gameObject.GetComponent<AudioSource>().Play();
    }


}


