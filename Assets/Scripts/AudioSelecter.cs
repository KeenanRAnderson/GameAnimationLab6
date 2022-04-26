using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Matthew Fawcett
public class AudioSelecter : MonoBehaviour
{
    [SerializeField] List<AudioClip> audios;
    [SerializeField] AudioSource source;
    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        source.clip = audios[Random.Range(0, audios.Count)];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position;
    }
}
