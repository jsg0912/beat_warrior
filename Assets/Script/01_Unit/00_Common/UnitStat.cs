using System.Collections.Generic;
using System;

public class UnitStat
{
    // Stat
    private Dictionary<StatKind, int> stats = new();
    private Dictionary<StatKind, int> currentStats = new();

    // Buff
    private Dictionary<StatKind, int> buffPlus = new();
    private Dictionary<StatKind, float> buffMultiply = new();

    public UnitStat(Dictionary<StatKind, int> stats)
    {
        this.stats = new Dictionary<StatKind, int>(stats);
        this.currentStats = new Dictionary<StatKind, int>(stats);

        InitializeAllBuff();
        CheckValidStats();
    }

    // Bug 방지 코드
    public void CheckValidStats()
    {
        foreach (StatKind statKind in Enum.GetValues(typeof(StatKind)))
        {
            if (statKind == StatKind.Necessary) break;
            if (stats.ContainsKey(statKind) == false) throw new Exception($"Stat이 없음 ${statKind}");
        }
    }

    public void InitializeAllBuff()
    {
        foreach (StatKind statKind in Enum.GetValues(typeof(StatKind)))
        {
            buffPlus.Add(statKind, 0);
            buffMultiply.Add(statKind, 0);
        }
    }

    public void ResetBuffPlus(StatKind statKind)
    {
        int deltaStat = GetFinalStat(statKind) - GetCurrentStat(statKind);
        buffPlus[statKind] = 0;
        currentStats[statKind] = GetFinalStat(statKind) - deltaStat;
        if (currentStats[statKind] <= 0) currentStats[statKind] = 1;
    }

    public void ResetBuffMultiply(StatKind statKind)
    {
        buffMultiply[statKind] = 0;
        // TODO: ResetBuffPlus에 있는 것처럼 Consumable Stat은 Multiply값 변경에 따라 조정하는 과정이 필요함 - 신동환, 20240814
    }

    public void SetBuffPlus(StatKind statKind, int value)
    {
        buffPlus[statKind] = value;
        currentStats[statKind] += value;    }

    public void SetBuffMultiply(StatKind statKind, int value)
    {
        buffMultiply[statKind] = value;
        // TODO: ResetBuffPlus에 있는 것처럼 Consumable Stat은 Multiply값 변경에 따라 조정하는 과정이 필요함 - 신동환, 20240814
        if (statKind == StatKind.HP)
        {
            currentStats[StatKind.HP] *= (1 + value);
        }
    }

    public bool GetIsFullStat(StatKind statKind) { return currentStats[statKind] == GetFinalStat(statKind); }
    public int GetCurrentStat(StatKind statKind) { return currentStats[statKind]; }

    public int GetFinalStat(StatKind statKind)
    {
        if (stats.ContainsKey(statKind))
        {
            int stat = stats[statKind];
            return (int)((stat + buffPlus[statKind]) * (1 + buffMultiply[statKind]));
        }
        else
        {
            throw new Exception($"Stat이 없음 ${statKind}");
        }
    }

    public int ChangeCurrentStat(StatKind statKind, int change)
    {
        currentStats[statKind] += change;

        if (currentStats[statKind] > GetFinalStat(statKind)) currentStats[statKind] = GetFinalStat(statKind);

        if (currentStats[statKind] < 0) currentStats[statKind] = 0;

        return currentStats[statKind];
    }

    public UnitStat Copy()
    {
        return new UnitStat(stats);
    }
}