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

    public virtual BlurType SetBlur()
    {
        return BlurType.None;
    }

    public virtual bool TurnOnPopup()
    {
        bool success = Util.SetActive(gameObject, true);
        if (success)
        {
            BlurUIManager.Instance.TurnOnActiveBlur(SetBlur());
            PopupManager.Instance.PushPopup(this);
        }
        return success;
    }

    public virtual bool TurnOffPopup()
    {
        bool success = Util.SetActive(gameObject, false);
        if (success)
        {
            if (SetBlur() != BlurType.None)
                BlurUIManager.Instance.TurnOffActiveBlur();
            PopupManager.Instance.RemovePopup(this);
        }
        return success;
    }
}