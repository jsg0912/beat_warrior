public class Unit
{
    private bool isAlive = true;
    public UnitInfo unitInfo;
    public UnitStat unitStat;

    public bool GetIsAlive()
    {
        return isAlive;
    }

    public bool GetIsFullStat(StatKind statKind) { return unitStat.GetIsFullStat(statKind); }

    public int GetCurrentHP() { return unitStat.GetCurrentStat(StatKind.HP); }

    public bool ChangeCurrentHP(int change)
    {
        int currentHP = unitStat.ChangeCurrentStat(StatKind.HP, change);

        if (currentHP <= 0) isAlive = false;

        return isAlive;
    }

    public Unit(UnitInfo unitInfo, UnitStat unitStat)
    {
        this.unitInfo = unitInfo;
        this.unitStat = unitStat.Copy();
    }
}
