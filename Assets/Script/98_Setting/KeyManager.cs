using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Jump,
    Down,
    Left,
    Right,
    Mark_Dash,
    Attack,
    Skill1,
    Skill2,
    Interaction,
    Null
}

public static class KeySetting
{
    public static Dictionary<Action, KeyCode> keys = new Dictionary<Action, KeyCode>();
}

public class KeyManager : MonoBehaviour
{
    private KeyCode[] defaultKeys = new KeyCode[]
    { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Mouse1,
        KeyCode.Space, KeyCode.Q, KeyCode.E, KeyCode.F };

    [SerializeField] private GameObject KeyButtons;
    [SerializeField] private GameObject KeySettingButtonPrefab;

    private void Awake()
    {
        for (int i = 0; i < defaultKeys.Length; i++)
        {
            if (KeySetting.keys.ContainsKey((Action)i) ==  false)
                KeySetting.keys.Add((Action)i, defaultKeys[i]);
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
