using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButtonManager : MonoBehaviour
{
    public void OnClickPopupButton()
    {
        PopupSystem.instance.OpenPopUp("Ȯ���Ͻðڽ��ϱ�?",
            () => { Debug.Log("Okay"); },
            () => { Debug.Log("Cancel"); });
    }
}
