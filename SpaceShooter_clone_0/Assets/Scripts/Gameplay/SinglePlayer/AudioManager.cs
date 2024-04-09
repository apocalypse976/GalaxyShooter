using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private AudioSource _soundSource;
    // Start is called before the first frame update
    void Start()
    {
        instance=this;
        _soundSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAudio(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }
}
