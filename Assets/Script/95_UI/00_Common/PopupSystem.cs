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

    public virtual bool TurnOnPopup()
    {
        DebugConsole.Log($"{gameObject.name}.TurnOnSettingUI");
        bool success = Util.SetActive(gameObject, true);
        if (success) PopupManager.Instance.PushPopup(this);
        return success;
    }

    public virtual bool TurnOffPopup()
    {
        return Util.SetActive(gameObject, false);
    }
}