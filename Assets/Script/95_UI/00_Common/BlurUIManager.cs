using System.Collections.Generic;
using UnityEngine;
public class BlurUIManager : SingletonObject<BlurUIManager>
{
    // TODO: Blur가 현재 UI에 들어가서 Image로서 UI들 사이에 Blur가 들어가는데, Sprite 사이에서 사용하려면 Blur Code를 만들어서 추가관리 필요 - 신동환, 20250214
    [SerializeField] private List<BlurTypePair> blurTypePairs;

    private BlurType currentBlurType = BlurType.None;

    public void TurnOnActiveBlur(BlurType blurType)
    {
        if (blurType == BlurType.None || currentBlurType == blurType)
        {
            return;
        }
        TurnOffActiveBlur();

        bool success = Util.SetActive(blurTypePairs.Find(pair => pair.blurType == blurType)?.gameObject, true);
        if (success) currentBlurType = blurType;
    }

    public void TurnOffActiveBlur()
    {
        if (currentBlurType == BlurType.None) return;
        Util.SetActive(blurTypePairs.Find(pair => pair.blurType == currentBlurType).gameObject, false);
        currentBlurType = BlurType.None;
    }
}