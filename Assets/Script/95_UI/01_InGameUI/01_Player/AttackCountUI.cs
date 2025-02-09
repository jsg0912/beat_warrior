using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCountUI : SingletonObject<AttackCountUI>
{
    [SerializeField] Image AttackChargeGauge;
    [SerializeField] List<Sprite> Max2ChargeImages = new List<Sprite>();
    [SerializeField] List<Sprite> Max3ChargeImages = new List<Sprite>(); // TODO: 나중에 Attack을 3번까지로 늘어나면 사용

    public void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (Player.Instance.GetFinalStat(StatKind.AttackCount) == 2)
        {
            AttackChargeGauge.sprite = Max2ChargeImages[Player.Instance.GetCurrentStat(StatKind.AttackCount)];
        }
    }
}
