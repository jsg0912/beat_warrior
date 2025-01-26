using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

// TODO: AltarManager로 바꿔서 Hierarchy에서도 AltarUI로 수정 - 정성균, 20241126
public class AltarPopup : PageAblePopupSystem
{
    SkillName[] salesSkillList;
    public bool isOn;

    public SkillName SelectTrait;
    int spiritCount
    {
        get
        {
            return Inventory.Instance.GetSoulNumber();
        }
    }
    TraitSetButtonStatus InfoButton;

    public GameObject Button;
    public TMP_Text PlayerSoulView;

    public GameObject Information;
    //public Image InfoImage;
    public TMP_Text InfoName;
    public TMP_Text InfoDescription;
    public Button InfoSetButton;
    public AudioClip EquipClip;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        UpdateUI();
    }

    private void OnEnable()
    {
        if (salesSkillList == null)
        {
            Initialize();
        }
        UpdateUI();
    }

    void Initialize()
    {
        isOn = true;
        salesSkillList = TraitPriceList.Info.Keys.ToArray();
        SelectTrait = SkillName.End;
        for (int i = 0; i < salesSkillList.Length; i++)
        {
            Util.SetActive(Button.transform.GetChild(i).GetComponent<Button>().gameObject, true);
            Button.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = salesSkillList[i].ToString() + "\n" + TraitPriceList.Info[salesSkillList[i]];
        }

    }

    protected override void ChangePage(int index)
    {

    }

    // Update is called once per frame
    public void UpdateUI()
    {
        PlayerSoulView.text = "Soul : " + spiritCount.ToString();
        InfoName.text = SelectTrait.ToString();
        if (CheckSelectInSales()) InfoDescription.text = GetTraitScript(SelectTrait);

        if (Inventory.Instance.IsPaidTrait(SelectTrait))
        {
            if (Player.Instance.IsEquippedTrait(SelectTrait))
            {
                InfoButton = TraitSetButtonStatus.UnEquip;
                InfoSetButton.interactable = true;

                SoundManager.instance.SFXPlay("Equip", EquipClip);
            }
            else
            {
                InfoButton = TraitSetButtonStatus.Equip;
                if (CheckFullEquip())
                {
                    InfoSetButton.interactable = false;
                }
                else
                {
                    InfoSetButton.interactable = true;
                }
            }
        }
        else
        {
            InfoButton = TraitSetButtonStatus.Buy;
            if (CheckSelectInSales())
            {
                if (spiritCount >= TraitPriceList.Info[SelectTrait])
                {
                    InfoSetButton.interactable = true;
                }
                else
                {
                    InfoSetButton.interactable = false;
                }
            }

        }

        InfoSetButton.GetComponentInChildren<TMP_Text>().text = InfoButton.ToString();


        for (int i = 0; i < salesSkillList.Length; i++)
        {
            SkillName targetSkill = salesSkillList[i];
            if (Inventory.Instance.IsPaidTrait(targetSkill) == true)
            {
                if (Player.Instance.IsEquippedTrait(targetSkill))
                {
                    Button.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                }
                else if (CheckFullEquip())
                {
                    Button.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
                }
                else
                {
                    Button.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                }
            }
            else Button.transform.GetChild(i).GetComponent<Image>().color = Color.red;
        }
    }


    public void OnClickTrait()
    {
        string clickObject = EventSystem.current.currentSelectedGameObject.name;

        for (int i = 0; i < salesSkillList.Length; i++)
        {
            if (Button.transform.GetChild(i).name == clickObject)
            {
                SkillName targetSkill = salesSkillList[i];
                if (SelectTrait == targetSkill)
                {
                    Util.SetActive(Information, false);
                    SelectTrait = SkillName.End;
                }
                else
                {
                    Util.SetActive(Information, true);
                    SelectTrait = targetSkill;
                }

            }
            else continue;
        }
        UpdateUI();
    }

    public void OnClickConfirm()
    {
        switch (InfoButton)
        {
            case TraitSetButtonStatus.Buy:
                Inventory.Instance.ChangeSoulNumber(-TraitPriceList.Info[SelectTrait]);
                Inventory.Instance.AddSkill(SelectTrait);
                UpdateUI();
                break;
            case TraitSetButtonStatus.Equip:
                if (!CheckFullEquip())
                {
                    Player.Instance.EquipTrait(SelectTrait);
                    UpdateUI();
                    break;
                }
                break;
            case TraitSetButtonStatus.UnEquip:
                Player.Instance.RemoveTrait(SelectTrait);
                UpdateUI();
                break;

        }
    }

    public void OnClickPopupView()
    {
        isOn = !isOn;
        Util.SetActive(gameObject, isOn);
    }

    private string GetTraitScript(SkillName skillName)
    {
        return ScriptPool.TraitUIScript[skillName][GameManager.Instance.Language];
    }

    private bool CheckFullEquip()
    {
        return Player.Instance.GetTraits().Length == PlayerConstant.MaxAdditionalSkillCount;
    }

    private bool CheckSelectInSales()
    {
        for (int i = 0; i < salesSkillList.Length; i++)
        {
            if (salesSkillList[i] == SelectTrait) return true;
        }
        return false;
    }

    public bool IsAltarPopupOn() { return isOn; }

    public override void TurnOnPopup()
    {
        base.TurnOnPopup();
        PauseController.instance.PauseGame();
    }

    public override void TurnOffPopup()
    {
        base.TurnOffPopup();
        PauseController.instance.ResumeGame();
    }
}