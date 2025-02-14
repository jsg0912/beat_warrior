using System;
using UnityEngine;

public enum BlurType
{
    None,
    MarkSlow,
    MenuStop
}

[Serializable]
public class BlurTypePair
{
    public BlurType blurType;
    public GameObject gameObject;
}