using System.Collections.Generic;
using UnityEngine;

public static class KeySetting
{
    public static Dictionary<PlayerAction, KeyCode> keys = new Dictionary<PlayerAction, KeyCode>();
}

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;
    private KeyCode[] defaultKeys = new KeyCode[]
    { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Mouse1,
        KeyCode.Space, KeyCode.Q, KeyCode.E, KeyCode.F };

    [SerializeField] private GameObject KeyButtons;
    [SerializeField] private GameObject KeySettingButtonPrefab;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < defaultKeys.Length; i++)
        {
            if (KeySetting.keys.ContainsKey((PlayerAction)i) == false)
                KeySetting.keys.Add((PlayerAction)i, defaultKeys[i]);
        }

        CreateKeySettingButtons();
    }


    private void CreateKeySettingButtons()
    {
        for (int i = 0; i < defaultKeys.Length; i++)
        {
            GameObject KeySettingButton = Instantiate(KeySettingButtonPrefab);
            KeySettingButton.transform.SetParent(KeyButtons.transform, false);
            KeySettingButton.GetComponent<KeySettingButton>().SetKey(i);
        }
    }
}
