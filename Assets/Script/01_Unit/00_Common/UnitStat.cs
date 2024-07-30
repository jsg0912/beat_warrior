public class UnitStat
{
    public int hp;
    public int atk;

    public UnitStat(int hp, int atk)
    {
        this.hp = hp;
        this.atk = atk;
    }

    public UnitStat Copy()
    {
        return new UnitStat(hp, atk);
    }
}