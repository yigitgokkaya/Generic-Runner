using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    [SerializeField] GameObject deathMenu;

    public void Retry()
    { 
        GameManager.score = 1;
        GameManager.savedScore=0;
        Time.timeScale = 1f;
       // SceneManager.LoadScene("NEWPLAYERSCENE");
       SceneManager.LoadScene("NEWPLAYERSCENE 1", LoadSceneMode.Single);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //StartCoroutine(Reload());
        GameManager.score = 0;
        
    }
    public T GetChildComponentByName<T>(string name) where T : Component {
        foreach (T component in GetComponentsInChildren<T>(true)) {
            if (component.gameObject.name == name) {
                return component;
            }
        }
        return null;
    }


    private IEnumerator Reload()
    {
    yield return new WaitForSeconds(2);
    Resources.UnloadUnusedAssets();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }   

    
}
