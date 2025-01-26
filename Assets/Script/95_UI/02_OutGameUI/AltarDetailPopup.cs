using TMPro;
using UnityEngine.UI;

public class AltarDetailPopup : PopupSystem
{
    public TMP_Text traitNameView;
    public TMP_Text traitTierView;
    public TMP_Text TraitInfoDescription;

    public void SetTraitName(SkillName traitName)
    {
        traitNameView.text = name.ToString();

    }
}
