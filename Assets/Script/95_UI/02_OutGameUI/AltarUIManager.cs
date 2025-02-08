using System.Linq;
using TMPro;

public class AltarUIManager : SingletonObject<AltarUIManager>
{
    public AltarPopup altarPopup;
    public AltarDetailPopup altarDetailPopup;
    public TMP_Text PlayerSoulView;

    public SkillName[] salesSkillList;
    int spiritCount => Inventory.Instance.GetSoulNumber();

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

    public void UpdatePlayerSoulView() => PlayerSoulView.text = "Soul : " + spiritCount.ToString();

    public void ShowSkillDetail(SkillName skillName)
    {
        altarDetailPopup.TurnOnPopup();
        altarDetailPopup.ShowSkillDetail(skillName);
    }

    public void OnClickTraitSelect(SkillName traitName)
    {
        switch (Inventory.Instance.GetTraitStatus(traitName))
        {
            case TraitSetButtonStatus.Buyable:
                Inventory.Instance.ChangeSoulNumber(-TraitPriceList.Info[traitName]);
                Inventory.Instance.AddSkill(traitName);
                break;
            case TraitSetButtonStatus.EquipAble:
                if (!Player.Instance.CheckFullEquipTrait())
                {
                    Player.Instance.EquipTrait(traitName);
                    break;
                }
                // TODO: 가득차있는데 장착하려고 하면, 어떻게 할지 결정해야 함 - 신동환, 20250130
                break;
            case TraitSetButtonStatus.Equipped:
                Player.Instance.RemoveTrait(traitName);
                break;
        }
        altarPopup.Refresh();
    }
}