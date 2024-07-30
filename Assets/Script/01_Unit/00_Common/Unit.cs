public class Unit
{
    public bool isAlive;
    public UnitInfo unitInfo;
    public UnitStat unitStat;

    public bool getIsAlive()
    {
        return isAlive;
    }

    public void SetDead()
    {
        isAlive = false;
    }

    public Unit(UnitInfo unitInfo, UnitStat unitStat)
    {
        isAlive = true;

        this.unitInfo = unitInfo;
        this.unitStat = unitStat.Copy();
    }
}
