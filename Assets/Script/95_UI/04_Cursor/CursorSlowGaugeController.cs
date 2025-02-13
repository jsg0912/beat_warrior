using UnityEngine;
using UnityEngine.UI;

public class CursorSlowGaugeController : MonoBehaviour
{
    [SerializeField] private Image TimeStopBar_Full;
    Timer timeStopTimer = new Timer();

    void FixedUpdate()
    {
        if (timeStopTimer.UnScaledTick())
        {
            TimeStopBar_Full.fillAmount = 1 - timeStopTimer.processRatio;
            DebugConsole.Log("TimeStopBar_Full.fillAmount : " + TimeStopBar_Full.fillAmount);
        }
    }


    public void TurnOn()
    {
        if (Util.SetActive(gameObject, true)) timeStopTimer.Initialize(TimeScaleConstant.ZoomInSlow);
    }

    public void TurnOff()
    {
        if (Util.SetActive(gameObject, false)) TimeStopBar_Full.fillAmount = 1;
    }
}