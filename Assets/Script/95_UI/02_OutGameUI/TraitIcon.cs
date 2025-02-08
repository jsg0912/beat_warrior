using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitIcon : MonoBehaviour
{
    private static Color BuyableColor = new Color32(94, 94, 94, 255);
    private static Color OwnColor = new Color32(255, 255, 255, 255);
    public Button traitSelectionButton;
    [SerializeField] private Image traitImage;
    [SerializeField] private Image lockImage;
    [SerializeField] private TMP_Text TierText;
    [SerializeField] private GameObject priceViewUI;
    [SerializeField] private TMP_Text priceText;
    [SerializeField]
    public TraitSetButtonStatus traitStatus
    {
        get;
        private set;
    }

    public SkillName traitName { get; private set; }

    public void ShowEmpty()
    {
        traitName = SkillName.End;
        traitStatus = TraitSetButtonStatus.None;
        SetTraitImage();
        TierText.text = "";
    }

    public void UpdateTraitStatus(bool isUpdateUI)
    {
        traitStatus = Inventory.Instance.GetTraitStatus(traitName);
        if (isUpdateUI) UpdateUIByStatus();
    }

    public void SetTraitName(SkillName traitName, bool isUpdateUI = true)
    {
        this.traitName = traitName;
        UpdateTraitStatus(isUpdateUI);
        SetTraitImage();
        SetTierText();
        SetPriceText();
    }

    private void UpdateUIByStatus()
    {
        switch (traitStatus)
        {
            case TraitSetButtonStatus.None:
                SetActiveLockImage(true);
                SetActivePriceView(false);
                SetInteractable(false);
                break;
            case TraitSetButtonStatus.Locked:
                SetActiveLockImage(true);
                SetActivePriceView(false);
                SetInteractable(true);
                break;
            case TraitSetButtonStatus.Buyable:
                SetActiveLockImage(false);
                SetActivePriceView(true);
                SetInteractable(true);
                traitImage.color = BuyableColor;
                break;
            case TraitSetButtonStatus.EquipAble:
                SetActiveLockImage(false);
                SetActivePriceView(false);
                SetInteractable(true);
                traitImage.color = OwnColor;
                break;
            case TraitSetButtonStatus.Equipped:
                SetActiveLockImage(false);
                SetActivePriceView(false);
                SetInteractable(true);
                traitImage.color = OwnColor;
                break;
        }
    }

    // TODO: Trait Icon 나오면 작업해야함 - 신동환, 20250126
    private void SetTraitImage()
    {
        traitImage.sprite = Resources.Load<Sprite>(PrefabRouter.TraitIconImages[traitName]);
    }

    private void SetTierText()
    {
        if (traitName == SkillName.End)
        {
            TierText.text = "";
            return;
        }
        SkillTier tier = TraitTierList.GetTier(traitName);
        TierText.text = OutGameUIConstant.TraitTireViewText[tier];
    }

    private void SetPriceText()
    {
        if (priceViewUI != null && priceViewUI.activeSelf)
        {
            priceText.text = TraitPriceList.Info[traitName].ToString();
        }
    }

    private void SetActiveLockImage(bool isActive)
    {
        Util.SetActive(lockImage.gameObject, isActive);
    }

    private void SetActivePriceView(bool isActive)
    {
        Util.SetActive(priceViewUI, isActive);
    }

    private void SetInteractable(bool isInteractable)
    {
        if (traitSelectionButton != null) traitSelectionButton.interactable = isInteractable;
    }

    public void OnClick()
    {
        AltarUIManager.Instance.ShowSkillDetail(traitName);
    }
}