using UnityEngine;
using UnityEngine.UI;

public abstract class PopupSystem : MonoBehaviour
{
    public Button okayButton;
    public Button cancelButton;

    public virtual void Awake()
    {
        okayButton?.onClick.AddListener(OnClickOkay);
        cancelButton?.onClick.AddListener(OnClickCancel);
    }

    public virtual void OnClickOkay()
    {
        TurnOffPopup();
    }

    public virtual void OnClickCancel()
    {
        TurnOffPopup();
    }

    public virtual void TurnOnPopup()
    {
        bool success = Util.SetActive(gameObject, true);
        if (success) CommandManager.Instance?.popupSystemStack.Add(this);
    }

    public virtual void TurnOffPopup()
    {
        bool success = Util.SetActive(gameObject, false);
        if (success) CommandManager.Instance?.PopPopupSystem();
    }
}