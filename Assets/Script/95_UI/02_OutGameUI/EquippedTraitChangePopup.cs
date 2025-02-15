using System.Collections.Generic;

public class EquippedTraitChangePopup : PopupSystem
{
    public List<TraitIcon> equippedTraitUIs;
    public SkillName targetSkill;

    void Start()
    {
        foreach (TraitIcon traitUI in equippedTraitUIs)
        {
            traitUI.traitSelectionButton.onClick.AddListener(() => TraitSwap(traitUI.traitName));
        }
    }

    public void TurnOnTraitChangePopup(SkillName targetSkill)
    {
        this.targetSkill = targetSkill;
        AltarUIManager.Instance.UpdateEquippedTraitUIs(equippedTraitUIs);
        TurnOnPopup();
    }

    public void TraitSwap(SkillName skillName)
    {
        AltarUIManager.Instance.TryUnEquipTrait(skillName);
        AltarUIManager.Instance.TryEquipTrait(targetSkill);
        OnClickOkay();
    }
}