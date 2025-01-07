public class Soul : Item
{
    private int number;

    public Soul()
    {
        number = 100;
    }

    public void Initialize()
    {
        number = 0;
    }

    public int Change(int gain)
    {
        number += gain;
        return number;
    }

    public int GetNumber()
    {
        return number;
    }
}