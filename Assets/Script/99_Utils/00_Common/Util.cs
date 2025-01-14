using System;
using UnityEngine;

public static class Util
{
    public static T ParseEnumFromString<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value);
    }

    public static void SetActive(GameObject gameObject, bool isOn)
    {
        if (gameObject != null && gameObject.activeSelf != isOn) gameObject.SetActive(isOn);
    }
}