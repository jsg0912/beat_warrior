using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    int key = -1;
    private void Awake()
    {
        for (int i = 0; i < defaultKeys.Length; i++)
        {
            KeySetting.keys.Add((Action)i, defaultKeys[i]);
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
    }

    public void ChangeKey(int num)
    {
        key = num;
    }
}
