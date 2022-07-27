using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPause : MonoBehaviour
{
        [SerializeField] GameObject pauseMenu;
        public GameObject manager;

    public void Pause()
    {
        pauseMenu.SetActive(true); 
        Time.timeScale = 0f;
    }
    public void Resume() 
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home(int SceneID)
    {
        Time.timeScale = 1f;
        GameManager.score = 1;
        SceneManager.LoadScene(SceneID);
        
    }

}
