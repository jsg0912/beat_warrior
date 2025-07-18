using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public Image getIcon;

    private SkillName traitName => AltarUIManager.Instance.SelectedTraitName;

    private void Start()
    {
        Initialize();
        interactionButton.onClick.AddListener(() => OnClickTraitUI(targetTraitUI));
        foreach (TraitIcon traitUI in equippedTraitUIs)
        {
            traitUI.traitSelectionButton.onClick.AddListener(() => ShowSkillDetail(traitUI.traitName));
        }
    }

    private void Initialize()
    {
        targetTraitUI.ShowEmpty();
    }

    public override bool TurnOnPopup()
    {
        bool success = base.TurnOnPopup();
        if (success)
        {
            Initialize();
            AltarUIManager.Instance.UpdateEquippedTraitUIs(equippedTraitUIs);
            UpdateAltarUIButtons();
        }
        return success;
    }

    public void UpdateTargetTraitInfo()
    {
        targetTraitUI.UpdateTraitStatus(false);
        AltarUIManager.Instance.UpdateEquippedTraitUIs(equippedTraitUIs);
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
        AltarUIManager.Instance.SetSelectedTraitName(traitName);
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
                getIcon.color = Color.gray;
                break;
            case TraitSetButtonStatus.Buyable:
                Util.SetActive(interactionButton, true);
                getIcon.color = Color.gray;
                interactionButtonText.text = "구매";
                break;
            case TraitSetButtonStatus.EquipAble:
                Util.SetActive(interactionButton, true);
                getIcon.color = Color.white;
                interactionButtonText.text = "장착";
                break;
            case TraitSetButtonStatus.Equipped:
                Util.SetActive(interactionButton, true);
                getIcon.color = Color.white;
                interactionButtonText.text = "해제";
                break;
        }
    }

    public void OnClickTraitUI()
    {
        OnClickTraitUI(targetTraitUI);
    }

    private void OnClickTraitUI(TraitIcon traitUI)
    {
        switch (traitUI.traitStatus)
        {
            case TraitSetButtonStatus.Buyable:
                AltarUIManager.Instance.TryBuyTrait(traitName);
                break;
            case TraitSetButtonStatus.EquipAble:
                AltarUIManager.Instance.TryEquipTrait(traitName);
                break;
            case TraitSetButtonStatus.Equipped:
                AltarUIManager.Instance.TryUnEquipTrait(traitName);
                break;
        }
    }
}
