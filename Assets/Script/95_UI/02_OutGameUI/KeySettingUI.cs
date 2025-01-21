using TMPro;
using UnityEngine;

public class KeySettingUI : MonoBehaviour
{
    private void Start()
    {
        UpdateKeySettingUI();
    }

    private void Update()
    {
        UpdateKeySettingUI();
    }

    public TextMeshProUGUI[] txt;
    public void UpdateKeySettingUI() // KeySettingUI로 가야함함
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(PlayerAction)i].ToString();
        }
    }
}