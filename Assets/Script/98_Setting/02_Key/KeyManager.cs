using System.Collections.Generic;
using UnityEngine;

public static class KeySetting
{
    private static Dictionary<PlayerAction, KeyCode> keys = new Dictionary<PlayerAction, KeyCode>();

    public static KeyCode GetKey(PlayerAction action)
    {
        if (keys.ContainsKey(action))
        {
            return keys[action];
        }
        switch (action)
        {
            case PlayerAction.Tutorial_Dash:
                return GetKey(PlayerAction.Mark_Dash);
            case PlayerAction.Tutorial_Mark:
                return GetKey(PlayerAction.Mark_Dash);
            case PlayerAction.Tutorial_SkillReset:
                return GetKey(PlayerAction.Interaction);
            case PlayerAction.Tutorial_Altar:
                return GetKey(PlayerAction.Interaction);
            case PlayerAction.Tutorial_Portal:
                return GetKey(PlayerAction.Interaction);
            default:
                Debug.LogError("Key for " + action + " is not set.");
                return KeyCode.None;
        }
    }

    public static void SetKey(PlayerAction action, KeyCode key)
    {
        if (keys.ContainsKey(action))
        {
            keys[action] = key;
        }
        else
        {
            keys.Add(action, key);
        }
    }
}

public class KeyManager : SingletonObject<KeyManager>
{
    private KeyCode[] defaultKeys = new KeyCode[]
    { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Mouse1,
        KeyCode.Space, KeyCode.Q, KeyCode.E, KeyCode.F };


    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < defaultKeys.Length; i++)
        {
            KeySetting.SetKey((PlayerAction)i, defaultKeys[i]);
        }
    }

    public KeyCode[] GetKeyCodes() { return defaultKeys; }
}