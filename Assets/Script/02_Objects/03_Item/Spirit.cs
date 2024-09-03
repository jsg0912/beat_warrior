public class Spirit : Item
{
    private int number;

    public Spirit()
    {
        number = 0;
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