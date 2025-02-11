public class Soul : Item
{
    private int number;

    public Soul()
    {
        Initialize();
    }

    public void Initialize()
    {
        number = ItemConstant.InitSoul;
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