using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : SingletonObject<CursorController>
{
    [SerializeField] private Animator cursorAnimator;
    [SerializeField] private GameObject TimeStopBar;
    [SerializeField] private Image TimeStopBar_Full;
    Timer timeStopTimer;

    void Start()
    {
        Cursor.visible = false; // 기본 커서 숨기기
        timeStopTimer = new Timer();
    }

    void Update()
    {
        gameObject.transform.position = Input.mousePosition;
    }

    public void SetTitleCursor()
    {
        cursorAnimator.SetTrigger(CursorAnimationTrigger.TitleTrigger);
    }

    public void SetInGameCursor()
    {
        cursorAnimator.SetTrigger(CursorAnimationTrigger.InGameTrigger);
        StopGaugeDecrease();
    }

    public void SetZoomInCursor()
    {
        cursorAnimator.SetBool(CursorAnimationTrigger.InGameTrigger, false);
        cursorAnimator.SetTrigger(CursorAnimationTrigger.ZoomInTrigger);
        StartCoroutine(StartGaugeDecrease(PlayerSkillConstant.MarkSlowDuration));
    }

    private IEnumerator StartGaugeDecrease(float maxTime)
    {
        TimeStopBar.SetActive(true);
        timeStopTimer.Initialize(maxTime);
        while (timeStopTimer.Tick())
        {
            TimeStopBar_Full.fillAmount = timeStopTimer.remainTime / maxTime;
            yield return null;
        }
        TimeStopBar.SetActive(false);
    }

    private void StopGaugeDecrease()
    {
        StopAllCoroutines();
        TimeStopBar.SetActive(false);
    }
}