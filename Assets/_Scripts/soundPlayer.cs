using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPlayer : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> soundClips = new List<AudioClip>();
    [SerializeField]
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
    }
}
