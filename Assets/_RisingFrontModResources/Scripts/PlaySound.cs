using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
   [SerializeField] AudioClip[] clips;
   [SerializeField] AudioSource aud;
    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void SoundPlay()
    {
        aud.clip = clips[Random.Range(0, clips.Length)];
        aud.Play();
    }
}
