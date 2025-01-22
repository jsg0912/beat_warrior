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


    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < defaultKeys.Length; i++)
        {
            if (KeySetting.keys.ContainsKey((PlayerAction)i) == false)
                KeySetting.keys.Add((PlayerAction)i, defaultKeys[i]);
        }
    }

    public KeyCode[] GetKeyCodes() { return defaultKeys; }
}