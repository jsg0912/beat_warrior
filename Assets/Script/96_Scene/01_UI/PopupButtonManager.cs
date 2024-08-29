using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButtonManager : MonoBehaviour
{
    public void OnClickPopupButton()
    {
        PopupSystem.instance.OpenPopUp("확인하시겠습니까?",
            () => { Debug.Log("Okay"); },
            () => { Debug.Log("Cancel"); });
    }
}
