using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitUI : MonoBehaviour
{
    public Button traitSelectionButton;
    [SerializeField] private Image traitImage;
    [SerializeField] private Image lockImage;
    [SerializeField] private TMP_Text TierText;
    [SerializeField]
    public TraitSetButtonStatus traitStatus
    {
        get;
        private set;
    }

    private SkillName traitName;

    public void SetSkillName(SkillName traitName)
    {
        if (traitName == SkillName.End)
        {
            ShowLock();
            return;
        }

        this.traitName = traitName;
        traitStatus = Inventory.Instance.GetTraitStatus(traitName);
        switch (traitStatus)
        {
            case TraitSetButtonStatus.Locked:
                ShowLock();
                break;
            case TraitSetButtonStatus.Buyable:
            case TraitSetButtonStatus.EquipAble:
            case TraitSetButtonStatus.Equipped:
                ShowUnlock();
                break;
        }
        SetTraitImage();
        SetTierText(TraitTierList.GetTier(traitName));
    }

    public void ShowEmpty()
    {
        traitName = SkillName.End;
        traitStatus = TraitSetButtonStatus.None;
        SetTraitImage();
        TierText.text = "";
    }

    // TODO: Trait Icon 나오면 작업해야함 - 신동환, 20250126
    private void SetTraitImage()
    {
        traitImage.sprite = Resources.Load<Sprite>(PrefabRouter.TraitIconImages[traitName]);
    }

    private void SetTierText(SkillTier tier)
    {
        TierText.text = ((int)tier).ToString();
    }

    private void ShowLock()
    {
        Util.SetActive(lockImage.gameObject, true);
        traitSelectionButton.interactable = false;
    }

    private void ShowUnlock()
    {
        Util.SetActive(lockImage.gameObject, false);
        traitSelectionButton.interactable = true;
    }

    public void OnClick()
    {
        AltarUIManager.Instance.ShowSkillDetail(traitName);
    }
}