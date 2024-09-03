using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;


public class AlterPopup : MonoBehaviour
{
    SkillName[] salesSkillList;
    private bool isOn = false;
<<<<<<< HEAD
    int spiritCount
    {
=======


    public SkillName SelectTrait;
    int spiritCount;
    /*{
>>>>>>> 8cbec18fd8877a963448b6d027140fb695e1e712
        get
        {
            return Inventory.Instance.GetSoulNumber();
        }
<<<<<<< HEAD
    }

    int abilityNum; // TODO: 제거해야함
=======
    }*/
    TraitSetButtonStatus InfoButton;
>>>>>>> 8cbec18fd8877a963448b6d027140fb695e1e712

    public GameObject Button;
    public GameObject EquipButton;
    public GameObject BuyPanel;
    public TMP_Text PriceView;

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
        spiritCount = 1000;
        isOn = false;
        salesSkillList = TraitPriceList.Info.Keys.ToArray();
        for (int i = 0; i < salesSkillList.Length; i++)
        {
            Button.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = salesSkillList[i].ToString();
        }

    }

    // Update is called once per frame
    public void UpdateUI()
    {
<<<<<<< HEAD
        PriceView.text = "My Price : " + spiritCount.ToString();

        SkillName[] equipTraitList = Player.Instance.GetTraits();
        for (int i = 0; i < PlayerConstant.MaxAdditionalSkillCount; i++)
        {
            if (equipTraitList.Length > i)
            {
                EquipButton.transform.GetChild(i).GetComponent<Image>().color = Color.black;

            }
            else
            {
                EquipButton.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }

=======
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


>>>>>>> 8cbec18fd8877a963448b6d027140fb695e1e712
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


    public void ClickAbility()
    {
        string clickObject = EventSystem.current.currentSelectedGameObject.name;

        for (int i = 0; i < salesSkillList.Length; i++)
        {
            if (Button.transform.GetChild(i).name == clickObject)
            {
                SkillName targetSkill = salesSkillList[i];
                if (Inventory.Instance.IsPaidTrait(targetSkill) == false)
                {
                    abilityNum = i;
                    BuyPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Price : " + TraitPriceList.Info[targetSkill].ToString();
                    BuyPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = GetTraitScript(targetSkill);

                    BuyPanel.SetActive(true);
                }
<<<<<<< HEAD
                // else
                // {
                //     if (EquipList[0] == null)
                //     {
                //         EquipList[0] = ability[i];
                //         EquipButton.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = ability[i].name;
                //     }
                //     else if (EquipList[1] == null)
                //     {
                //         EquipList[1] = ability[i];
                //         EquipButton.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = ability[i].name;
                //     }
                //     else continue;
                // }
=======
                else
                {
                    Information.SetActive(true);
                    SelectTrait = targetSkill;
                }

>>>>>>> 8cbec18fd8877a963448b6d027140fb695e1e712
            }
            else continue;
        }
        UpdateUI();
    }

    public void BuyPanelYes()
    {
        SkillName targetSkillName = salesSkillList[abilityNum];
        int targetSkillPrice = TraitPriceList.Info[targetSkillName];
        if (spiritCount >= targetSkillPrice)
        {
<<<<<<< HEAD
            Inventory.Instance.ChangeSpiritNumber(-targetSkillPrice);
            Inventory.Instance.AddSkill(targetSkillName);
            BuyPanel.SetActive(false);
=======
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

>>>>>>> 8cbec18fd8877a963448b6d027140fb695e1e712
        }
        UpdateUI();
    }

    public void BuyPanelno()
    {
        BuyPanel.SetActive(false);

        UpdateUI();
    }

    public void RemoveEquipList()
    {
        string clickObject = EventSystem.current.currentSelectedGameObject.name;

        for (int i = 0; i < PlayerConstant.MaxAdditionalSkillCount; i++)
        {
            if (EquipButton.transform.GetChild(i).name == clickObject)
            {
                Player.Instance.RemoveTraitByIndex(i);
            }
            else continue;
        }

        UpdateUI();
    }

    public void OnClickPopupView()
    {
        isOn = !isOn;
        gameObject.SetActive(isOn);
    }

    private string GetTraitScript(SkillName skillName)
    {
        return UIScript.TraitUIScript[skillName][UIManager.Instance.language];
    }

    private bool CheckFullEquip()
    {
        return Player.Instance.GetTraits().Length == PlayerConstant.MaxAdditionalSkillCount;
    }
<<<<<<< HEAD
=======

    private bool CheckSelectInSales()
    {
        for (int i = 0; i < salesSkillList.Length; i++)
        {
            if (salesSkillList[i] == SelectTrait) return true;
        }
        return false;
    }

    public void test1()
    {
        SkillName[] a = new SkillName[Player.Instance.GetTraits().Length];
        a = Player.Instance.GetTraits();

        for (int i = 0; i < Player.Instance.GetTraits().Length; i++)
        {
            Debug.Log(a[i].ToString());
        }
    }

    public void test2()
    {
    }
>>>>>>> 8cbec18fd8877a963448b6d027140fb695e1e712
}