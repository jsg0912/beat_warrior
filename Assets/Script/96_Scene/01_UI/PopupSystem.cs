using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PopupSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popup;

    public static PopupSystem instance { get; private set; }

    public Text txtContennt;
    Action onClickOkay, onClickCancel;

    private void Awake()
    {
        instance = this;
    }

    public void OpenPopUp(string content, Action onClickOkay, Action onClickCancel)
    {
        
        txtContennt.text = content;
        this.onClickOkay = onClickOkay;
        this.onClickCancel = onClickCancel;
    }

    public void OnClickOkay()
    {
        if (onClickOkay != null)
        {
            onClickOkay();
        }

        ClosePopup();
    }

    public void OnClickCancel()
    {
        if (onClickCancel != null)
        {
            onClickCancel();
        }

        ClosePopup();
    }

    private void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
