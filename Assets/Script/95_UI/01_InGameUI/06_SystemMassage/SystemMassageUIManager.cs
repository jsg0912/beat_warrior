using System.Collections;
using TMPro;
using UnityEngine;
public class SystemMassageUIManager : SingletonObject<SystemMassageUIManager>
{
    public SystemMassagePopup systemMassagePopup;
    public SystemMassagePopup MapTitleMassagePopup;

    public void TurnOnSystemMassageUI(SyetemMassageType syetemMassageType)
    {
        string message = ScriptPool.SystemMassageDictionary[syetemMassageType][GameManager.Instance.Language];
        systemMassagePopup.SetMessageText(message);
        systemMassagePopup.TurnOnPopup();
    }
    public void TurnOnMapTitleMassageUI(SceneName sceneName)
    {
        string message = sceneName.ToString();
        MapTitleMassagePopup.SetMessageText(message);
        MapTitleMassagePopup.TurnOnPopup();
    }

    public IEnumerator TriggerTurnOnMapTitleMassage(SceneName sceneName)
    {
        yield return new WaitForSeconds(0f);
        TurnOnMapTitleMassageUI(sceneName );
    }
}
