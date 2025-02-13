using UnityEngine;

public class CursorController : SingletonObject<CursorController>
{
    [SerializeField] private Animator cursorAnimator;
    [SerializeField] private CursorSlowGaugeController cursorSlowGaugeController;

    void Start()
    {
        Cursor.visible = false; // 기본 커서 숨기기
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
        cursorSlowGaugeController.TurnOff();
    }

    public void SetZoomInCursor()
    {
        cursorAnimator.SetBool(CursorAnimationTrigger.InGameTrigger, false);
        cursorAnimator.SetTrigger(CursorAnimationTrigger.ZoomInTrigger);
        cursorSlowGaugeController.TurnOn();
    }
}