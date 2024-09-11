using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;


public class AlterPopup : MonoBehaviour
{
    SkillName[] salesSkillList;
    private bool isOn = false;


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
    public TMP_Text PriceView;

    public GameObject Information;
    //public Image InfoImage;
    public TMP_Text InfoName;
    public TMP_Text InfoDescription;
    public Button InfoSetButton;

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
        isOn = false;
        salesSkillList = TraitPriceList.Info.Keys.ToArray();
        SelectTrait = SkillName.End;
        for (int i = 0; i < salesSkillList.Length; i++)
        {
            Button.transform.GetChild(i).GetComponent<Button>().gameObject.SetActive(true);
            Button.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = salesSkillList[i].ToString() + "\n" + TraitPriceList.Info[salesSkillList[i]];
        }

    }

    // Update is called once per frame
    public void UpdateUI()
    {
        PriceView.text = "Soul : " + spiritCount.ToString();
        InfoName.text = SelectTrait.ToString();
        if (CheckSelectInSales()) InfoDescription.text = GetTraitScript(SelectTrait);

        if (Inventory.Instance.IsPaidTrait(SelectTrait))
        {
            if (Player.Instance.IsEquippedTrait(SelectTrait))
            {
                InfoButton = TraitSetButtonStatus.Unequip;
                InfoSetButton.interactable = true;
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
                    Information.SetActive(false);
                    SelectTrait = SkillName.End;
                }
                else
                {
                    Information.SetActive(true);
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
            case TraitSetButtonStatus.Unequip:
                Player.Instance.RemoveTrait(SelectTrait);
                UpdateUI();
                break;

        }
    }

    public void OnClickPopupView()
    {
        isOn = !isOn;
        gameObject.SetActive(isOn);
        //PauseControl.instance.SetPauseActive();
    }

    private string GetTraitScript(SkillName skillName)
    {
        return UIScript.TraitUIScript[skillName][UIManager.Instance.language];
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

}