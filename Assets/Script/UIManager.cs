using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI[] txt;
    private void Start()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(ACTION)i].ToString();
        }
        
    }

    private void Update()
    {
        for(int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(ACTION)i].ToString();
        }
    }
}
