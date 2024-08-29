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
    private Spirit spirit = new Spirit();
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

    public int GetSpiritNumber()
    {
        return spirit.GetNumber();
    }

    public int ChangeSpiritNumber(int number)
    {
        return spirit.Change(number);
    }

    public bool IsPaidTrait(SkillName targetSkill)
    {
        return mySkillList.Exists(mySkill => mySkill == targetSkill);
    }
}