using System;
using UnityEngine;

public enum BlurType
{
    None,
    MarkSlow,
    SystemMessageBlackBlur,
    MenuBlackBlur,
    TopPopupBlackBlur,
}

[Serializable]
public class BlurTypePair
{
    public BlurType blurType;
    public GameObject gameObject;
}