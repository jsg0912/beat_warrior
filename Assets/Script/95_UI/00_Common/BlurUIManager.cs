using System.Collections.Generic;
using UnityEngine;
public class BlurUIManager : SingletonObject<BlurUIManager>
{
    [SerializeField] private List<BlurTypePair> blurTypePairs;

    private BlurType currentBlurType = BlurType.None;

    public void TurnOnActiveBlur(BlurType blurType)
    {
        if (currentBlurType == blurType)
        {
            return;
        }
        TurnOffActiveBlur();

        bool success = Util.SetActive(blurTypePairs.Find(pair => pair.blurType == blurType).gameObject, true);
        if (success) currentBlurType = blurType;
    }

    public void TurnOffActiveBlur()
    {
        if (currentBlurType == BlurType.None) return;
        Util.SetActive(blurTypePairs.Find(pair => pair.blurType == currentBlurType).gameObject, false);
        currentBlurType = BlurType.None;
    }
}