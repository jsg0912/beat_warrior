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
        CommandManager.Instance.popupSystemStack.Add(this);
        Util.SetActive(this.gameObject, true);
    }

    public virtual void TurnOffPopup()
    {
        Util.SetActive(gameObject, false);
    }
}