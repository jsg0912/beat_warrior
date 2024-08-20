using System.Collections.Generic;

public class Inventory
{
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