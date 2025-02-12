using UnityEngine;

public class SystemMassageUIManager : SingletonObject<SystemMassageUIManager>
{
    public SystemMassagePopup systemMassagePopup;

    public void TurnOnSystemMassageUI()
    {
        systemMassagePopup.TurnOnPopup();
    }

}
