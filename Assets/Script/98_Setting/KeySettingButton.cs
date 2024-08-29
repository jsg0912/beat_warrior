using TMPro;
using UnityEngine;

public class KeySettingButton : MonoBehaviour
{
    private bool isListeningForInput = false;
    private KeyCode currentShortcutKey;
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
        // 키보드 입력 감지
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            if (Input.GetKeyDown(keyCode))
            {
                currentShortcutKey = keyCode;
                isListeningForInput = false;
                KeySetting.keys[(Action)idx] = currentShortcutKey;
                UpdateKeyText();
                Debug.Log(currentShortcutKey);
                return;
            }

        // 마우스 버튼 입력 감지
        if (Input.GetMouseButtonDown(0)) currentShortcutKey = KeyCode.Mouse0;
        if (Input.GetMouseButtonDown(1)) currentShortcutKey = KeyCode.Mouse1;
        if (Input.GetMouseButtonDown(2)) currentShortcutKey = KeyCode.Mouse2;
        Debug.Log(currentShortcutKey);
        isListeningForInput = false;
        KeySetting.keys[(Action)idx] = currentShortcutKey;
        UpdateKeyText();
    }

    public void OnClick()
    {
        isListeningForInput = true;
    }

    public void UpdateKeyText()
    {
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