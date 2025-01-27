using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inventory>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("Inventory");
                    _instance = go.AddComponent<Inventory>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }
    private Soul spirit = new Soul();
    private List<Item> items = new List<Item>();
    private static List<SkillName> mySkillList = new List<SkillName>();

    public void initialize()
    {
        items.Clear();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public Item GetItem<T>()
    {
        return items.Find(item => item.GetType() == typeof(T));
    }

    public void AddSkill(SkillName skill)
    {
        mySkillList.Add(skill);
    }

    public int GetSoulNumber()
    {
        return spirit.GetNumber();
    }

    public int ChangeSoulNumber(int number)
    {
        return spirit.Change(number);
    }

    // TODO: 해당 함수 위치가 여기 있는게 맞는지 검토 필요 - 신동환, 20250126
    public TraitSetButtonStatus GetTraitStatus(SkillName traitName)
    {
        if (IsPaidTrait(traitName))
        {
            if (Player.Instance.IsEquippedTrait(traitName))
            {
                return TraitSetButtonStatus.Equipped;
            }
            else
            {
                return TraitSetButtonStatus.EquipAble;
            }

        }
        else
        {
            if (AltarUIManager.Instance.CheckInSales(traitName))
            {
                return TraitSetButtonStatus.Buyable;
            }
            else
            {
                return TraitSetButtonStatus.Locked;
            }
        }
    }

    public bool IsPaidTrait(SkillName targetSkill)
    {
        return mySkillList.Exists(mySkill => mySkill == targetSkill);
    }
}