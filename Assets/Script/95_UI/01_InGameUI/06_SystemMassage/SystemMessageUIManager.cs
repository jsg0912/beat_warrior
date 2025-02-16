using System.Collections;
using UnityEngine;
public class SystemMessageUIManager : SingletonObject<SystemMessageUIManager>
{
    public SystemMessagePopup systemMessagePopup;
    public SystemMessagePopup mapTitleMessagePopup;

    public Sprite TitleMapImage;
    public Sprite TutorialMapImage;
    public Sprite LowerPartMapImage;
    public Sprite LowerPartBossMapImage;

    public void TurnOnSystemMassageUI(SystemMessageType systemMessageType)
    {
        string message = ScriptPool.SystemMassageDictionary[systemMessageType][GameManager.Instance.Language];
        systemMessagePopup.SetMessageText(message);
        systemMessagePopup.TurnOnPopup();
    }

    public void TurnOnTutorialMassageUI(PlayerAction playerAction)
    {
        string message = ScriptPool.TutorialText[playerAction][GameManager.Instance.Language];
        systemMessagePopup.SetMessageText(message);
        systemMessagePopup.TurnOnPopup();
    }

    public void TurnOnMapTitleMassageUI(SceneName sceneName)
    {
        bool isExist = true;
        switch (sceneName)
        {
            case SceneName.Title:
                mapTitleMessagePopup.SetBackgroundImage(TitleMapImage);
                break;
            case SceneName.Tutorial2:
                mapTitleMessagePopup.SetBackgroundImage(TutorialMapImage);
                break;
            case SceneName.Ch2:
                mapTitleMessagePopup.SetBackgroundImage(LowerPartMapImage);
                break;
            case SceneName.Ch2BossStage:
                mapTitleMessagePopup.SetBackgroundImage(LowerPartBossMapImage);
                break;
            default:
                isExist = false;
                break;
        }
        if (isExist) mapTitleMessagePopup.TurnOnPopup();
    }

    public IEnumerator TriggerTurnOnMapTitleMassage(SceneName sceneName)
    {
        yield return new WaitForSeconds(0f);
        TurnOnMapTitleMassageUI(sceneName);
    }
}
