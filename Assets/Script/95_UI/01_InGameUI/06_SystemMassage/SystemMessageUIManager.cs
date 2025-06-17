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

    public UIImageAnimation LowerPartBossMapAnimation;

    public bool isTimeLinePlaying = false;

    public void SetIsTimeLindPlaying()
    {
        isTimeLinePlaying = !isTimeLinePlaying;
    }
    public void TurnOnSystemMassageUI(SystemMessageType systemMessageType, float displayDuration = 2.0f, bool isStayWhenPause = false)
    {
        string message = ScriptPool.SystemMassageDictionary[systemMessageType][GameManager.Instance.Language];
        systemMessagePopup.SetMessageText(message);
        systemMessagePopup.TurnOnPopup(displayDuration, isStayWhenPause);
    }

    public void TurnOnTutorialMassageUI(PlayerAction playerAction, bool isStayWhenPause)
    {
        string message = ScriptPool.TutorialText[playerAction][GameManager.Instance.Language];
        systemMessagePopup.SetMessageText(message);
        systemMessagePopup.TurnOnPopup(displayDuration: 4.0f, isStayWhenPause);
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
            case SceneName.Ch2of1:
            case SceneName.Ch2of2:
            case SceneName.Ch2of3:
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

    public void TurnOnBossMapTitle()
    {
        LowerPartBossMapAnimation.gameObject.SetActive(true);
        LowerPartBossMapAnimation.Initialize();
        //LowerPartBossMapAnimation.gameObject.SetActive(true);
        StartCoroutine(LowerPartBossMapAnimation.AnimateSprite());
    }
}
