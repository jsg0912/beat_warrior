using TMPro;
using UnityEngine;

public class KeySettingUI : MonoBehaviour
{
    public TextMeshProUGUI[] txt;
    private void Start()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }

    private void Update()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }
}