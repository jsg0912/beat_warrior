using TMPro;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class KeySettingButton : MonoBehaviour
{
    private bool isListeningForInput = false;
    private int idx;

    private TextMeshProUGUI ActionText;
    private TextMeshProUGUI KeyText;

    void Update()
    {
        if (isListeningForInput)
        {
            ListenForInput();
        }
    }

    private void ListenForInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetKeyCode(KeyCode.Mouse0);
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetKeyCode(KeyCode.Mouse1);
            return;
        }
        if (Input.GetMouseButtonDown(2))
        {
            SetKeyCode(KeyCode.Mouse2);
            return;
        }

        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                SetKeyCode(keyCode);
                return;
            }
        }
    }

    void SetKeyCode(KeyCode keyCode)
    {
        isListeningForInput = false;

        KeySetting.keys[(Action)idx] = keyCode;
        UpdateKeyText();
    }

    public void OnClick()
    {
        isListeningForInput = true;
    }

    public void UpdateKeyText()
    {
        if (ActionText == null || KeyText == null)
            return;

        ActionText.text = ((Action)idx).ToString();
        KeyText.text = KeySetting.keys[(Action)idx].ToString();
    }

    public void SetKey(int key)
    {
        this.idx = key;

        ActionText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        KeyText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();

        UpdateKeyText();
    }
}
