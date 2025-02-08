using System.Linq;
using TMPro;
using UnityEngine;

public class AltarUIManager : MonoBehaviour
{
    public static AltarUIManager Instance;

    public AltarPopup altarPopup;
    public AltarDetailPopup altarDetailPopup;
    public TMP_Text PlayerSoulView;

    public SkillName[] salesSkillList;
    int spiritCount => Inventory.Instance.GetSoulNumber();

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        salesSkillList = TraitPriceList.Info.Keys.ToArray();
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