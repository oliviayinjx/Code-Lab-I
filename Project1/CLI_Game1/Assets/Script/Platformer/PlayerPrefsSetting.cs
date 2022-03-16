using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSetting : MonoBehaviour
{
    private int musicVolume;

    public int MusicVolume
    {
        get
        {
            musicVolume = PlayerPrefs.GetInt("MusicVol", 20);
            return musicVolume;
        }
        set
        {
            musicVolume = value;
            PlayerPrefs.SetInt("MusicVol", sfxVolume);
        }
    }

    private int sfxVolume;
    public int SfxVolume
    {
        get
        {
            sfxVolume = PlayerPrefs.GetInt("SfxVol", 20);
            return musicVolume;
        }
        set
        {
            sfxVolume = value;
            PlayerPrefs.SetInt("sfxVolume", sfxVolume);
        }

    }
    void Start()
    {
        MusicVolume = 10;
        SfxVolume = 15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
