using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Language language = Language.kr;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

}