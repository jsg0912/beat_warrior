using UnityEngine;
using UnityEngine.UI;

public class CursorController : SingletonObject<CursorController>
{
    [SerializeField] private Animator cursorAnimator;
    [SerializeField] private Image cursorImage;
    [SerializeField] private Sprite inGameCursor;
    [SerializeField] private Sprite zoomInCursor;

    void Start()
    {
        Cursor.visible = false; // 기본 커서 숨기기
    }

    void Update()
    {
        cursorImage.transform.position = Input.mousePosition;
    }

    public void SetTitleCursor()
    {
        cursorAnimator.SetTrigger(CursorAnimationTrigger.TitleTrigger);
    }

    public void SetInGameCursor()
    {
        cursorAnimator.SetTrigger(CursorAnimationTrigger.ResetTrigger);
        cursorImage.sprite = inGameCursor;
        // cursorAnimator.SetTrigger(CursorAnimationTrigger.InGameTrigger);
    }

    public void SetZoomInCursor()
    {
        cursorAnimator.SetTrigger(CursorAnimationTrigger.ResetTrigger);
        cursorImage.sprite = zoomInCursor;
        // cursorAnimator.SetTrigger(CursorAnimationTrigger.ZoomInTrigger);
    }
}