using System.Linq;
using TMPro;
using UnityEngine;

public class AltarUIManager : SingletonObject<AltarUIManager>
{
    public AltarPopup altarPopup;
    public AltarDetailPopup altarDetailPopup;
    public TMP_Text PlayerSoulView;
    public ParticleSystem altarParticle;

    public SkillName[] salesSkillList;
    int spiritCount => Inventory.Instance.GetSoulNumber();

    void Start()
    {
        salesSkillList = TraitPriceList.Info.Keys.ToArray();
        var Particle = altarParticle.main;
        Particle.useUnscaledTime = true;
    }

    public void TurnOnAltarUI()
    {
        altarPopup.TurnOnPopup();
    }

    public bool CheckInSales(SkillName skillName)
    {
        return salesSkillList.Contains(skillName);
    }

    public void UpdatePlayerSoulView() => PlayerSoulView.text = " : " + spiritCount.ToString();

    public void ShowSkillDetail(SkillName skillName)
    {
        altarDetailPopup.TurnOnPopup();
        altarDetailPopup.ShowSkillDetail(skillName);
    }

    public void RefreshMainAltarPopup()
    {
        altarPopup.Refresh();
    }
}