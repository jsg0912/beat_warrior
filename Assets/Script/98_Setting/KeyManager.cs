using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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

    private List<TextMeshProUGUI> ActionText = new();
    private List<TextMeshProUGUI> KeyText = new();

    int key = -1;

    KeyCode currentShortcutKey;

    [SerializeField] private GameObject Setting;


    private void Awake()
    {
        for (int i = 0; i < defaultKeys.Length; i++)
            KeySetting.keys.Add((Action)i, defaultKeys[i]);

        SetTextList();
    }

    private bool isListeningForInput = false;

    void Update()
    {
        if (isListeningForInput)
        {
            ListenForInput();
        }
    }

    private void ListenForInput()
    {
        // Ű���� �Է� ����
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            if (Input.GetKeyDown(keyCode)) currentShortcutKey = keyCode;

        // ���콺 ��ư �Է� ����
        if (Input.GetMouseButtonDown(0)) currentShortcutKey = KeyCode.Mouse0;
        else if (Input.GetMouseButtonDown(1)) currentShortcutKey = KeyCode.Mouse1;
        else if (Input.GetMouseButtonDown(2)) currentShortcutKey = KeyCode.Mouse2;

        isListeningForInput = false;
        KeySetting.keys[(Action)key] = currentShortcutKey;
        UpdateKeyText();
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
        isListeningForInput = true;
    }

    public void ChangeKey(int num)
    {
        key = num;
    }
}
