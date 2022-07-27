using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

 
[HideInInspector]
   void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AdButton");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}

