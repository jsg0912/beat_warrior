using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCountUI : SingletonObject<AttackCountUI>
{
    [SerializeField] Image AttackChargeGauge;
    [SerializeField] List<Sprite> Max2ChargeImages = new List<Sprite>();
    [SerializeField] List<Sprite> Max3ChargeImages = new List<Sprite>();
    [SerializeField] Sprite InfiniteChargeImage;

    public void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (GameManager.Instance.gameMode == GameMode.Infinite)
        {
            AttackChargeGauge.sprite = InfiniteChargeImage;
        }
        else
        {
            if (Player.Instance.GetFinalStat(StatKind.AttackCount) == 2)
            {
                AttackChargeGauge.sprite = Max2ChargeImages[Player.Instance.GetCurrentStat(StatKind.AttackCount)];
            }

            if (Player.Instance.GetFinalStat(StatKind.AttackCount) == 3)
            {
                AttackChargeGauge.sprite = Max3ChargeImages[Player.Instance.GetCurrentStat(StatKind.AttackCount)];
            }
        }
    }
}
