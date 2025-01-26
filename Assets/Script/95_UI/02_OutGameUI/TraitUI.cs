using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitUI : MonoBehaviour
{
    [SerializeField] private Button traitSelectionButton;
    [SerializeField] private Image traitImage;
    [SerializeField] private Image lockImage;
    [SerializeField] private TMP_Text TierText;

    public void Lock()
    {
        Util.SetActive(lockImage.gameObject, true);
        traitSelectionButton.interactable = false;
    }

    public void Unlock()
    {
        Util.SetActive(lockImage.gameObject, false);
        traitSelectionButton.interactable = true;
    }

    public void SetTraitImage(Sprite sprite)
    {
        traitImage.sprite = sprite;
    }

    public void SetTierText(int tier)
    {
        TierText.text = tier.ToString();
    }
}