using TMPro;
using UnityEngine;

public class KeySettingUI : MonoBehaviour
{
    [SerializeField] private GameObject KeyButtons;
    [SerializeField] private GameObject KeySettingButtonPrefab;
    private void Start()
    {
        CreateKeySettingButtons();
        UpdateKeySettingUI();
    }

    private void Update()
    {
        UpdateKeySettingUI();
    }

    public TextMeshProUGUI[] txt;
    public void UpdateKeySettingUI()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(PlayerAction)i].ToString();
        }
    }

    private void CreateKeySettingButtons()
    {
        for (int i = 0; i < KeyManager.Instance.GetKeyCodes().Length; i++)
        {
            GameObject KeySettingButton = Instantiate(KeySettingButtonPrefab);
            KeySettingButton.transform.SetParent(KeyButtons.transform, false);
            KeySettingButton.GetComponent<KeySettingButton>().SetKey(i);
        }
    }
}