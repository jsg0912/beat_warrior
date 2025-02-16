using System;
using UnityEngine;

public enum BlurType
{
    None,
    MarkSlow,
    BlackBlur
}

[Serializable]
public class BlurTypePair
{
    public BlurType blurType;
    public GameObject gameObject;
}