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
        Debug.Log(message);
    }

    public IEnumerator TriggerTurnOnMapTitleMassage(SceneName sceneName)
    {
        Debug.Log("코루틴 ㅅ작");
        yield return new WaitForSeconds(1);
        TurnOnMapTitleMassageUI(sceneName );
    }
}
