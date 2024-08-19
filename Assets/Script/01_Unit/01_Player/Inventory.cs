using System.Collections.Generic;

public class Inventory
{
    private Essence essence = new Essence();
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

    public int GetEssenceNumber()
    {
        return essence.GetNumber();
    }

    public void IncreaseEssence(int number)
    {
        essence.Increase(number);
    }
}