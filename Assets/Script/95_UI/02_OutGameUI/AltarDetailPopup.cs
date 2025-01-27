using System.Collections.Generic;
using System.Diagnostics;
using TMPro;

public class AltarDetailPopup : PopupSystem
{
    public TraitUI targetTraitUI;
    public List<TraitUI> equippedTraitUIs;
    public TMP_Text traitNameView;
    public TMP_Text traitInfoDescription;
    private SkillName traitName;

    private void Start()
    {
        targetTraitUI.traitSelectionButton.onClick.AddListener(() => OnClickTraitUI(targetTraitUI));
        foreach (TraitUI traitUI in equippedTraitUIs)
        {
            traitUI.traitSelectionButton.onClick.AddListener(() => OnClickTraitUI(traitUI));
        }
    }

    // TODO: Update문 안써야 함. Button 어떤 식으로 작동할지 기획 나오면 그떄가서 수정 - SDH, 20250127
    private void Update()
    {
        targetTraitUI.SetSkillName(traitName);
        UpdateEquippedTraitUIs();
    }

    private void SetTraitName()
    {
        traitNameView.text = traitName.ToString();
    }

    private void SetTraitDescription()
    {
        traitInfoDescription.text = ScriptPool.TraitUIScript[traitName][GameManager.Instance.Language]; ;
    }

    public void ShowSkillDetail(SkillName traitName)
    {
        this.traitName = traitName;
        targetTraitUI.SetSkillName(traitName);
        SetTraitName();
        SetTraitDescription();
    }

    public void UpdateEquippedTraitUIs()
    {
        SkillName[] equippedTraits = Player.Instance.GetTraits();
        int i = 0;
        for (; i < equippedTraits.Length; i++)
        {
            equippedTraitUIs[i].SetSkillName(equippedTraits[i]);
        }
        for (; i < equippedTraitUIs.Count; i++)
        {
            equippedTraitUIs[i].ShowEmpty();
        }
    }

    private void OnClickTraitUI(TraitUI traitUI)
    {
        switch (traitUI.traitStatus)
        {
            case TraitSetButtonStatus.Buyable:
                Inventory.Instance.ChangeSoulNumber(-TraitPriceList.Info[traitName]);
                Inventory.Instance.AddSkill(traitName);
                AltarUIManager.Instance.UpdatePlayerSoulView();
                Update();
                break;
            case TraitSetButtonStatus.EquipAble:
                if (Player.Instance.CheckFullEquipTrait() == false)
                {
                    Player.Instance.EquipTrait(traitName);
                    Update();
                }
                break;
            case TraitSetButtonStatus.Equipped:
                Player.Instance.RemoveTrait(traitName);
                Update();
                break;
        }
    }
}
