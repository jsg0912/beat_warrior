using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PopupSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popup;

    public static PopupSystem instance { get; private set; }

    public TextMeshProUGUI txtContent;

    System.Action onClickOkay, onClickCancel;

    private void Awake()
    {
        instance = this;
    }

    public void OpenPopUp(string content, System.Action onClickOkay, System.Action onClickCancel)
    {

        txtContent.text = content;
        this.onClickOkay = onClickOkay;
        this.onClickCancel = onClickCancel;
        Util.SetActive(popup, true);
    }

    public void OnClickOkay()
    {
        if (onClickOkay != null)
        {
            //onClickOkay();
        }

        ClosePopup();
    }

    public void OnClickCancel()
    {
        if (onClickCancel != null)
        {
            //onClickCancel();
        }

        ClosePopup();
    }

    private void ClosePopup()
    {
        Util.SetActive(gameObject, false);
    }
}
