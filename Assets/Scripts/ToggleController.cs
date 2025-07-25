using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Toggle toggle;
    

    void Start()
    {
       
        toggle.isOn = PlayerPrefs.GetInt("ToggleState", 0) == 1;

      
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });
    }

   
    public void ToggleValueChanged(Toggle change)
    {
      
        PlayerPrefs.SetInt("ToggleState", change.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void MuteHandle(bool mute)
    {

        if (mute)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }

    }
}
