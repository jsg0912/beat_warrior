using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class AbilityUI : MonoBehaviour
{
    Ability[] ability;

    int MyPrice;

    Ability[] EquipList;

    int AbilityNum;
    int AbilityPrice;

    public GameObject Button;
    public GameObject EquipButton;
    public GameObject BuyPanel;
    public TMP_Text Price;

    // Start is called before the first frame update
    void Start()
    {
        Initianlize();

    }

    void Initianlize()
    {
        ability = new Ability[7];

        ability[0] = new Ability("AblityName1", 100, "AbilityDescription1");
        ability[1] = new Ability("AblityName2", 110, "AbilityDescription2");
        ability[2] = new Ability("AblityName3", 120, "AbilityDescription3");
        ability[3] = new Ability("AblityName4", 130, "AbilityDescription4");
        ability[4] = new Ability("AblityName5", 140, "AbilityDescription5");
        ability[5] = new Ability("AblityName6", 150, "AbilityDescription6");
        ability[6] = new Ability("AblityName7", 160, "AbilityDescription7");

        MyPrice = 120;

        EquipList = new Ability[2];


        EquipList[0] = null;
        EquipList[1] = null;

        for (int i = 0; i < 7; i++)
        {
            Button.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = ability[i].name;
        }

    }

    void AbilityListInitianlize()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Price.text = "My Price : " + Inventory.Instance.GetSpiritNumber().ToString();

        for (int i = 0; i < 2; i++)
        {
            if (EquipList[i] == null)
            {
                EquipButton.transform.GetChild(i).GetComponent<Image>().color = Color.black;

            }
            else
            {
                EquipButton.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }

        for (int i = 0; i < 7; i++)
        {

            if (ability[i].islock == false)
            {
                if (EquipList[0] == ability[i])
                {
                    Button.transform.GetChild(i).GetComponent<Image>().color = Color.black;
                }
                else if (EquipList[1] == ability[i])
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

        for (int i = 0; i < 7; i++)
        {
            if (Button.transform.GetChild(i).name == clickObject)
            {

                if (ability[i].islock == true)
                {
                    AbilityNum = i;
                    BuyPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Price : " + ability[i].price.ToString();
                    BuyPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = ability[i].description;

                    BuyPanel.SetActive(true);
                }
                else
                {
                    if (EquipList[0] == null)
                    {
                        EquipList[0] = ability[i];
                        EquipButton.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = ability[i].name;

                    }
                    else if (EquipList[1] == null)
                    {
                        EquipList[1] = ability[i];
                        EquipButton.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = ability[i].name;
                    }
                    else continue;
                }
            }
            else continue;
        }
    }

    public void BuyPanelYes()
    {
        if (MyPrice >= ability[AbilityNum].price)
        {

            MyPrice -= ability[AbilityNum].price;
            ability[AbilityNum].islock = false;
            BuyPanel.SetActive(false);
        }

    }

    public void BuyPanelno()
    {
        BuyPanel.SetActive(false);
    }

    public void RemoveEquipList()
    {
        string clickObject = EventSystem.current.currentSelectedGameObject.name;

        for (int i = 0; i < 2; i++)
        {
            if (EquipButton.transform.GetChild(i).name == clickObject)
            {
                EquipList[i] = null;
            }
            else continue;
        }

    }

    bool CheckFullEquip()
    {
        if (EquipList[0] != null && EquipList[1] != null) return true;
        else return false;

    }
}
