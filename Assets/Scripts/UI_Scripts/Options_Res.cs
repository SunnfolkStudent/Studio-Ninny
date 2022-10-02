using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using UnityEngine.Audio;

public class Options_Res : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;
    
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;

    public TMP_Text resolutionLabel;

    public AudioMixer TheMixer;
    
    public TMP_Text mastLabel, musicLabel, sfxLabel;
    public Slider mastSlider, musicSlider, sfxSlider;
    
    void Start()
    {
       fullscreenTog.isOn = Screen.fullScreen;
       
       if (QualitySettings.vSyncCount == 0)
       { vsyncTog.isOn = false; }
       else
       { vsyncTog.isOn = true; }

       bool foundRes = false;
       for (int i = 0; i < resolutions.Count; i++)
       {
           if (Screen.width == resolutions[i] .horizontal && Screen.height == resolutions[i].vertical)
           {
               foundRes = true;

               selectedResolution = i;
               
               UpdatedResLabel();
           }
       }

       if (!foundRes)
       {
           ResItem newRes = new ResItem();
           newRes.horizontal = Screen.width;
           newRes.vertical = Screen.height;

           resolutions.Add(newRes);
           selectedResolution = resolutions.Count - 1;
           
           UpdatedResLabel();
       }
       float vol = 0f;
       TheMixer.GetFloat("MasterVol", out vol);
       mastSlider.value = vol;
       TheMixer.GetFloat("MusicVol", out vol);
       musicSlider.value = vol;
       TheMixer.GetFloat("SFXVol", out vol);
       sfxSlider.value = vol;
       
       mastLabel.text = Mathf.RoundToInt(mastSlider.value + 80).ToString();
       musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
       sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }
    
    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdatedResLabel();

    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        { selectedResolution = resolutions.Count - 1; }

        UpdatedResLabel();
    }

    public void UpdatedResLabel()
    { resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " +
                             resolutions[selectedResolution].vertical.ToString(); }
    
    public void ApplyGraphics()
    {
        // Screen.fullScreen = fullscreenTog.isOn;
        if (vsyncTog.isOn)
        { QualitySettings.vSyncCount = 1; }
        else { QualitySettings.vSyncCount = 0; }
        
        Screen.SetResolution(resolutions[selectedResolution].horizontal,
            resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }

    public void SetMasterVol()
    { mastLabel.text = Mathf.RoundToInt(mastSlider.value + 80).ToString();

        TheMixer.SetFloat("MasterVol", mastSlider.value);
        PlayerPrefs.SetFloat("MasterVol", mastSlider.value);
        
    }
    public void SetMusicVol()
    { musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        TheMixer.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
    public void SetSFXVol()
    { sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        TheMixer.SetFloat("SFXVol", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
