using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyAudioControl : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void PlaySoundByPath(string path)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        PlaySound(clip);
    }
}
