using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    
    [SerializeField] private Slider _musicvol;
    [SerializeField] private string _musicKey;
    private AudioSource _music;

    private void Start()
    {
     _music= GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey(_musicKey))
        {
            PlayerPrefs.GetFloat(_musicKey, 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void MusicChange()
    {
        _music.volume = _musicvol.value;
        Save();
    }
    private void Load()
    {
        _musicvol.value= PlayerPrefs.GetFloat(_musicKey,1);
    }
    private void Save()
    {
        PlayerPrefs.SetFloat(_musicKey,_musicvol.value);
    }
}
