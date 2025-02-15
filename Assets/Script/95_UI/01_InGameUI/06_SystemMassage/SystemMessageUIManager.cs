using System.Collections;
using UnityEngine;
public class SystemMessageUIManager : SingletonObject<SystemMessageUIManager>
{
    public SystemMessagePopup systemMessagePopup;
    public SystemMessagePopup MapTitleMessagePopup;

    public void TurnOnSystemMassageUI(SyetemMessageType syetemMessageType)
    {
        string message = ScriptPool.SystemMassageDictionary[syetemMessageType][GameManager.Instance.Language];
        systemMessagePopup.SetMessageText(message);
        systemMessagePopup.TurnOnPopup();
    }
    public void TurnOnMapTitleMassageUI(SceneName sceneName)
    {
        string message = sceneName.ToString();
        //MapTitleMessagePopup.SetMessageText(message);
        MapTitleMessagePopup.TurnOnPopup();
    }

    public IEnumerator TriggerTurnOnMapTitleMassage(SceneName sceneName)
    {
        yield return new WaitForSeconds(0f);
        TurnOnMapTitleMassageUI(sceneName );
    }
}
