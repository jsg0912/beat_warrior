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

    public void TurnOnSystemMassageUI(SyetemMessageType syetemMessageType)
    {
        string message = ScriptPool.SystemMassageDictionary[syetemMessageType][GameManager.Instance.Language];
        systemMessagePopup.SetMessageText(message);
        systemMessagePopup.TurnOnPopup();
    }
    public void TurnOnMapTitleMassageUI(SceneName sceneName)
    {
        //string message = sceneName.ToString();
        //MapTitleMessagePopup.SetMessageText(message);
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
        }
        mapTitleMessagePopup.TurnOnPopup();
    }

    public IEnumerator TriggerTurnOnMapTitleMassage(SceneName sceneName)
    {
        yield return new WaitForSeconds(0f);
        TurnOnMapTitleMassageUI(sceneName );
    }
}
