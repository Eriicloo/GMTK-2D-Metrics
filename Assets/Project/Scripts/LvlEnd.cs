using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlEnd : MonoBehaviour
{
   void OnTriggerEnter2D()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      
      if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
      {
         PlayerPrefs.SetInt("ReachedIndex",SceneManager.GetActiveScene().buildIndex+1);
         PlayerPrefs.SetInt("UnlockedLevels",PlayerPrefs.GetInt("UnlockedLevels")+1);
         PlayerPrefs.Save();
      }
   }
}
