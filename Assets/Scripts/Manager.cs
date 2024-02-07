using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton
public class Manager : MonoBehaviour
{
    public static Manager instance;
    //public static GameManager gameManager;
    public static DataManager dataManager;

    //public static GameManager Game { get { return gameManager; } }
    public static DataManager Data { get { return dataManager; } }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Manager: valid instance already exists");
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
    private void InitManagers()
    {
        // DataManager init
        GameObject dataObj = new GameObject() { name = "DataManager" };
        dataObj.transform.SetParent(transform);
        dataManager = dataObj.AddComponent<DataManager>();
    }
}
