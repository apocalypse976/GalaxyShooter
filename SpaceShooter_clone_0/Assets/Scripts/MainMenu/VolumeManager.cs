using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [Header("BackGround Music")]
    [SerializeField] private Slider _musicvol;
    [SerializeField] private string _musicKey;
    [Header("Sound")]
    [SerializeField] private Slider _soundvol;
    [SerializeField] private string _soundKey;
    private AudioSource _music;

    private void Start()
    {
     _music=GameObject.Find("Music"). GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey(_musicKey))
        {
            PlayerPrefs.GetFloat(_musicKey, 1);
            Load();
        }
        else
        {
            Load();
        }
        if (!PlayerPrefs.HasKey(_soundKey))
        {
            PlayerPrefs.GetFloat(_soundKey, 1);
            LoadVol();
        }
        else
        {
            LoadVol();
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
    public void SoundChange()
    {
        SoundManager.instance. _soundSource.volume = _soundvol.value;
        Savevol();
    }
    private void Savevol()
    {
        PlayerPrefs.SetFloat(_soundKey, _soundvol.value);
    }
    private void LoadVol()
    {
        _soundvol.value = PlayerPrefs.GetFloat(_soundKey, 1);
    }
}
