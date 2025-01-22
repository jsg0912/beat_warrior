public class Unit
{
    public UnitInfo unitInfo;
    public UnitStat unitStat;

    public bool GetIsFullStat(StatKind statKind) { return unitStat.GetIsFullStat(statKind); }

    public int GetCurrentHP() { return unitStat.GetCurrentStat(StatKind.HP); }

    public bool ChangeCurrentHP(int change)
    {
        int currentHP = unitStat.ChangeCurrentStat(StatKind.HP, change);

        bool isAlive = currentHP > 0;
        return isAlive;
    }

    public Unit(UnitInfo unitInfo, UnitStat unitStat)
    {
        this.unitInfo = unitInfo;
        this.unitStat = unitStat;
    }
}