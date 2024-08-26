public class Ability
{
    public string name;
    public int price;
    public bool islock;
    public string description;

    public Ability(string name, int price, string description, bool islock = true)
    {
        this.name = name;
        this.price = price;
        this.description = description;
        this.islock = islock;
    }
}