using System.Xml.Serialization;

public class Unit
{
    private bool isAlive;
    public UnitInfo unitInfo;
    public UnitStat unitStat;

    public bool GetIsAlive()
    {
        return isAlive;
    }

    public int GetHP()
    {
        return unitStat.hp;
    }

    public void SetHP(int hp)
    {
        unitStat.hp = hp;
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
