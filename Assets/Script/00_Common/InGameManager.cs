using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private static InGameManager _instance;

    public static InGameManager Instance
    {
        get
        {
            TryCreateInGameManager();
            return _instance;
        }
    }

    public static void TryCreateInGameManager()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<InGameManager>();
            if (_instance == null)
            {
                GameObject go = new GameObject("InGameManager");
                _instance = go.AddComponent<InGameManager>();
                DontDestroyOnLoad(go);
            }
        }
    }
    public void Start()
    {
        Player.TryCreatePlayer();
    }
}
