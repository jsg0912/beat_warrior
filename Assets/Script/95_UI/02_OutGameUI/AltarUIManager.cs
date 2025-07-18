using System.Collections.Generic;
using System.Linq;
using TMPro;

public class AltarUIManager : SingletonObject<AltarUIManager>
{
    public AltarPopup altarPopup;
    public AltarDetailPopup altarDetailPopup;
    public EquippedTraitChangePopup equippedTraitChangePopup;
    public TMP_Text PlayerSoulView;

    public SkillName[] salesSkillList;
    int spiritCount => Inventory.Instance.GetSoulNumber();
    public SkillName SelectedTraitName { get; private set; }

    override protected void Awake()
    {
        base.Awake();

        Initialize();
    }

    private void Initialize()
    {
        if (GameManager.Instance.gameMode == GameMode.Infinite)
            salesSkillList = TraitSalesList.SalesListInfinite;
        else salesSkillList = TraitSalesList.SalesListNormal;
    }

    public void TurnOnAltarUI()
    {
        Initialize();
        altarPopup.TurnOnPopup();
    }

    public void TurnOnEquippedTraitChangePopup()
    {
        equippedTraitChangePopup.TurnOnTraitChangePopup(SelectedTraitName);
    }

    public void SetSelectedTraitName(SkillName traitName)
    {
        SelectedTraitName = traitName;
    }

    public bool CheckInSales(SkillName skillName)
    {
        return salesSkillList.Contains(skillName);
    }

    public void UpdatePlayerSoulView() => PlayerSoulView.text = spiritCount.ToString();

    public void UpdateEquippedTraitUIs(List<TraitIcon> equippedTraitUIs)
    {
        SkillName[] equippedTraits = Player.Instance.GetTraits();
        int i = 0;
        for (; i < equippedTraits.Length; i++)
        {
            equippedTraitUIs[i].SetTraitName(equippedTraits[i]);
        }
        for (; i < equippedTraitUIs.Count; i++)
        {
            equippedTraitUIs[i].ShowEmpty();
        }
    }

    public void ShowSkillDetail(SkillName skillName)
    {
        // altarDetailPopup.TurnOnPopup();
        altarDetailPopup.ShowSkillDetail(skillName);
    }

    public void RefreshMainAltarPopup()
    {
        altarPopup.Refresh();
    }

    public void TryBuyTrait(SkillName traitName)
    {
        if (Inventory.Instance.GetSoulNumber() >= TraitPriceList.Info[traitName])
        {
            Inventory.Instance.ChangeSoulNumber(-TraitPriceList.Info[traitName]);
            Inventory.Instance.AddSkill(traitName);
            AltarUIManager.Instance.UpdatePlayerSoulView();
            altarDetailPopup.UpdateTargetTraitInfo();
            AltarUIManager.Instance.RefreshMainAltarPopup();
            SoundManager.Instance.SFXPlay(SoundList.Instance.altarBuy);

            TryEquipTrait(traitName, false);
        }
        else
        {
            SystemMessageUIManager.Instance.TurnOnSystemMassageUI(SystemMessageType.LackSoul);
        }
    }

    public void TryEquipTrait(SkillName traitName, bool showSwapPopup = true)
    {
        if (!Player.Instance.CheckFullEquipTrait())
        {
            Player.Instance.EquipTrait(traitName);
            altarDetailPopup.UpdateTargetTraitInfo();
        }
        else if (showSwapPopup)
        {
            TurnOnEquippedTraitChangePopup();
        }
    }

    public void TryUnEquipTrait(SkillName traitName)
    {
        Player.Instance.RemoveTrait(traitName);
        altarDetailPopup.UpdateTargetTraitInfo();
    }
}