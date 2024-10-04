using UnityEngine;

public class PopupButtonManager : MonoBehaviour
{
    public void OnClickPopupButton()
    {
        PopupSystem.instance.OpenPopUp("",
            () => { Debug.Log("Okay"); },
            () => { Debug.Log("Cancel"); });
    }
}
