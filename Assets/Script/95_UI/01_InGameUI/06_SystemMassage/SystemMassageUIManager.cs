using UnityEngine;

public class SystemMassageUIManager : SingletonObject<SystemMassageUIManager>
{
    public SystemMassagePopup systemMassagePopup;

    public void TurnOnSystemMassageUI(SyetemMassageType syetemMassageType)
    {
        string message = ScriptPool.SystemMassageDictionary[syetemMassageType][GameManager.Instance.Language];
        systemMassagePopup.SetMessageText(message);
        systemMassagePopup.TurnOnPopup();
    }

}
