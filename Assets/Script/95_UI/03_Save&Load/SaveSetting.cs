using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveSetting
{
    public int resolutionWidth;
    public int resolutionHeight;
    public float masterVolume;
    public float backgroundVolume;
    public float effectVolume;
    public string[] keyCode;

    public SaveSetting()
    {
        this.resolutionWidth = 1920;
        this.resolutionHeight = 1080;
        this.masterVolume = 1f;
        this.backgroundVolume = 1f;
        this.effectVolume = 1f;

        KeyCode[] defaultKeys = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Mouse1, KeyCode.Space, KeyCode.Q, KeyCode.E, KeyCode.F };
        string[] temp;
        temp = new string[defaultKeys.Length];
        for (int i = 0; i < defaultKeys.Length; i++)
        {
            temp[i] = defaultKeys[i].ToString();
        }
        this.keyCode = temp;
    }

    /*public SaveSetting(Resolution resolution, Dictionary<AudioList, float> audio, KeyCode[] keys)
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
    */

}