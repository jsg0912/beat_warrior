using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : PopupSystem
{
    public static SettingUI Instance;
    public GameObject setting;
    public override void Awake()
    {
        Instance = this;
        base.Awake();
    }

    public override void TurnOnPopup()
    {
        base.TurnOnPopup();
        Util.SetActive(setting, true);
    }

    public void OnClickClose()
    {
        TurnOffPopup();
    }

    public void OnClickSetting()
    {
        Util.SetActive(setting, true);
    }

}
