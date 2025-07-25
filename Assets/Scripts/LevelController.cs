using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelController : MonoBehaviour
{
    public Button[] buttons;


  
    private void Awake()
    {
        if (buttons.Length != 0)
        {
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }
            for (int i = 0; i < unlockedLevel; i++)
            {
                buttons[i].interactable = true;
            }
        }
       

    }
    public void OpenLevel(int levelID)
    {
        if (levelID == 7)
        {
            SceneManager.LoadScene(7);
        }
        else
        {
            string levelName = "level" + levelID;
            SceneManager.LoadScene(levelName);
        }
        
    }
    
    
}
