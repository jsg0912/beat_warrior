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
        PopupManager.Instance.RemovePopup(this);
    }

    public virtual void OnClickCancel()
    {
        TurnOffPopup();
        PopupManager.Instance.RemovePopup(this);
    }

    public virtual bool TurnOnPopup()
    {
        bool success = Util.SetActive(gameObject, true);
        if (success) PopupManager.Instance.PushPopup(this);
        return success;
    }

    public virtual bool TurnOffPopup()
    {
        return Util.SetActive(gameObject, false);
    }
}