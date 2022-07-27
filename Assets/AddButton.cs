using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    static AddButton Instance;
    private void Start()
{
    if (Instance != null) { Debug.Log("instance not null"); Destroy(gameObject); }
    else
    {
        Debug.Log("instance null");
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
}
}
