using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPlayer : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> soundClips = new List<AudioClip>();
    [SerializeField]
    public void PlaySound()
    {
        AudioClip randomSound = soundClips[Random.Range(0, soundClips.Count)];
        audioSource.PlayOneShot(randomSound);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
    }
}
