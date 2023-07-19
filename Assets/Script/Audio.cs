using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    private AudioSource audioSource;
    public static Audio Instance {get; private set;}

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        } 
        else
        {
            Debug.Log("cuidadoooo");
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonido(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
