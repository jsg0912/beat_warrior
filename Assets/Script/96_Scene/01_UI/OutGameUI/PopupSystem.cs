using UnityEngine;
using TMPro;

public class PopupSystem : MonoBehaviour
{
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
        onClickOkay?.Invoke();

        ClosePopup();
    }

    public void OnClickCancel()
    {
        onClickCancel?.Invoke();

        ClosePopup();
    }

    private void ClosePopup()
    {
        Util.SetActive(gameObject, false);
    }
}
