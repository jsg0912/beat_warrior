public class Essence : Item
{
    private int number;

    public Essence()
    {
        number = 0;
    }

    public void Initialize()
    {
        number = 0;
    }

    public int Increase(int gain)
    {
        number += gain;
        return number;
    }

    public int GetNumber()
    {
        return number;
    }
}