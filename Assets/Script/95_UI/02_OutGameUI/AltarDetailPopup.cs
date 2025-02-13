using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class AltarDetailPopup : PopupSystem
{
    public TraitIcon targetTraitUI;
    public List<TraitIcon> equippedTraitUIs;
    public TMP_Text traitNameView;
    public TMP_Text traitInfoDescription;
    // TODO: 아래 interactionButton Class 만들면 좋음
    public Button interactionButton;
    public TMP_Text interactionButtonText;

    private SkillName traitName;

    private void Start()
    {
        interactionButton.onClick.AddListener(() => OnClickTraitUI(targetTraitUI));
        foreach (TraitIcon traitUI in equippedTraitUIs)
        {
            traitUI.traitSelectionButton.onClick.AddListener(() => ShowSkillDetail(traitUI.traitName));
        }
    }

    public override bool TurnOnPopup()
    {
        bool success = base.TurnOnPopup();
        if (success)
        {
            UpdateEquippedTraitUIs();
            UpdateAltarUIButtons();
        }
        return success;
    }

    private void UpdateTargetTraitInfo()
    {
        targetTraitUI.UpdateTraitStatus(false);
        UpdateEquippedTraitUIs();
        UpdateAltarUIButtons();
    }

    private void UpdateTraitName()
    {
        traitNameView.text = ScriptPool.TraitNameScript[traitName][GameManager.Instance.Language]; ;
    }

    private void UpdateTraitDescription()
    {
        traitInfoDescription.text = ScriptPool.TraitUIScript[traitName][GameManager.Instance.Language];
    }

    public void ShowSkillDetail(SkillName traitName)
    {
        if (traitName == SkillName.End) return;
        this.traitName = traitName;
        targetTraitUI.SetTraitName(traitName, false);
        UpdateTraitName();
        UpdateTraitDescription();
        UpdateAltarUIButtons();
    }

    private void UpdateAltarUIButtons()
    {
        switch (targetTraitUI.traitStatus)
        {
            case TraitSetButtonStatus.Locked:
                Util.SetActive(interactionButton, false);
                break;
            case TraitSetButtonStatus.Buyable:
                Util.SetActive(interactionButton, true);
                interactionButtonText.text = "구매";
                break;
            case TraitSetButtonStatus.EquipAble:
                Util.SetActive(interactionButton, true);
                interactionButtonText.text = "장착";
                break;
            case TraitSetButtonStatus.Equipped:
                Util.SetActive(interactionButton, true);
                interactionButtonText.text = "해제";
                break;
        }
    }

    private void UpdateEquippedTraitUIs()
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

    private void OnClickTraitUI(TraitIcon traitUI)
    {
        switch (traitUI.traitStatus)
        {
            case TraitSetButtonStatus.Buyable:
                TryBuyTrait();
                break;
            case TraitSetButtonStatus.EquipAble:
                TryEquipTrait();
                break;
            case TraitSetButtonStatus.Equipped:
                TryUnEquipTrait();
                break;
        }
    }

    private void TryBuyTrait()
    {
        if (Inventory.Instance.GetSoulNumber() >= TraitPriceList.Info[traitName])
        {
            Inventory.Instance.ChangeSoulNumber(-TraitPriceList.Info[traitName]);
            Inventory.Instance.AddSkill(traitName);
            AltarUIManager.Instance.UpdatePlayerSoulView();
            UpdateTargetTraitInfo();
            AltarUIManager.Instance.RefreshMainAltarPopup();
        }
    }

    private void TryEquipTrait()
    {
        if (!Player.Instance.CheckFullEquipTrait())
        {
            Player.Instance.EquipTrait(traitName);
            UpdateTargetTraitInfo();
        }
    }

    private void TryUnEquipTrait()
    {
        Player.Instance.RemoveTrait(traitName);
        UpdateTargetTraitInfo();
    }
}
