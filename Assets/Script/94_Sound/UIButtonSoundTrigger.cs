using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSoundTrigger : MonoBehaviour, 
    IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 호버링 효과음 재생
        SoundManager.Instance.SFXPlay(SoundList.Instance.buttonHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭 효과음 재생
        SoundManager.Instance.SFXPlay(SoundList.Instance.buttonClick);
    }
}