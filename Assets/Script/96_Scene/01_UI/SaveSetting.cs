using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveSetting
{
    public int resolutionWidth;
    public int resolutionHeight;
    public string[] audioName;
    public float[] audioValue;
    public string[] keyCode;

    public SaveSetting()
    {
        this.resolutionWidth = Screen.currentResolution.width;
        this.resolutionHeight = Screen.currentResolution.height;
        this.audioName = Enum.GetNames(typeof(AudioList));
        this.audioValue = new float[3] { 1f, 1f, 1f };

        KeyCode[] defaultKeys = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Mouse1, KeyCode.Space, KeyCode.Q, KeyCode.E, KeyCode.F };
        string[] temp;
        temp = new string[defaultKeys.Length];
        for (int i = 0; i < defaultKeys.Length; i++)
        {
            temp[i] = defaultKeys[i].ToString();
        }
        this.keyCode = temp;
    }

    public SaveSetting(Resolution resolution, Dictionary<AudioList, float> audio, KeyCode[] keys)
    {
        this.resolutionWidth = resolution.width;
        this.resolutionHeight = resolution.height;
        audioName = new string[audio.Count];
        audioValue = new float[audio.Count];
        int index = 0;
        foreach (KeyValuePair<AudioList, float> entry in audio)
        {
            audioName[index] = entry.Key.ToString();
            audioValue[index] = entry.Value;
            index++;
        }

        for (int i = 0; i < keys.Length; i++)
        {
            keyCode[i] = keys[i].ToString();
        }
    }

}