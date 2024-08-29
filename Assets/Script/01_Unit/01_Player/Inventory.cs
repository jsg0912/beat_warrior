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

    public int GetSpiritNumber()
    {
        return spirit.GetNumber();
    }

    public int IncreaseSpirit(int number)
    {
        return spirit.Increase(number);
    }
}