using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Action
{
    Jump,
    Down,
    Left,
    Right,
    Mark,
    Dash,
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
    { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Mouse1, KeyCode.Mouse0,
        KeyCode.Space, KeyCode.Q, KeyCode.E, KeyCode.F };

    [SerializeField] private GameObject Setting;

    private List<TextMeshProUGUI> ActionText = new();
    private List<TextMeshProUGUI> KeyText = new();

    int key = -1;


    private void Awake()
    {
        for (int i = 0; i < defaultKeys.Length; i++)
            KeySetting.keys.Add((Action)i, defaultKeys[i]);

        SetTextList();
    }

    private void SetTextList()
    {
        foreach (Transform child in Setting.transform.GetChild(0).GetComponentInChildren<Transform>())
        {
            ActionText.Add(child.GetChild(0).GetComponent<TextMeshProUGUI>());
            KeyText.Add(child.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>());
        }
        foreach (Transform child in Setting.transform.GetChild(1).GetComponentInChildren<Transform>())
        {
            ActionText.Add(child.GetChild(0).GetComponent<TextMeshProUGUI>());
            KeyText.Add(child.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>());
        }

        UpdateKeyText();
    }

    private void UpdateKeyText()
    {
        for (int i = 0; i < ActionText.Count; i++)
        {
            ActionText[i].text = ((Action)i).ToString();
        }

        for (int i = 0; i < KeyText.Count; i++)
        {
            KeyText[i].text = KeySetting.keys[(Action)i].ToString();
        }
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;

        if (keyEvent.isKey)
        {
            KeySetting.keys[(Action)key] = keyEvent.keyCode;
            key = -1;
        }

        UpdateKeyText();
    }

    public void ChangeKey(int num)
    {
        key = num;
    }
}
