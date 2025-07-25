using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishPoint : MonoBehaviour
{
    public PlayerController player;
    AudioController audioController;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("player"))
        {
           
      
            if (SceneManager.GetActiveScene().name == "Tutorial" && player.cherries==1)
            {
                player.frezen();
                audioController.PlaySFX(audioController.finishClip);
                SceneManager.LoadSceneAsync(1);
            }
            else if (player.cherries == (2 * (SceneManager.GetActiveScene().buildIndex) + 3))
            {
                Debug.Log("finish");
                player.frezen();
                audioController.PlaySFX(audioController.finishClip);
                UnlockNewLevel();
                StartCoroutine(DelayedFunction());
              
              
            }
        }
    }
    public void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex <=4)
        {
            Debug.Log("Unlock new level");
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
    IEnumerator DelayedFunction()
    {
        yield return new WaitForSeconds(1f); 
        NextLevel();
  
    }
    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
 
}
