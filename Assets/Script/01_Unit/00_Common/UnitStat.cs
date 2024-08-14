using System.Collections.Generic;
using System;

public class UnitStat
{
    // TODO: 만약 소모성 Stat이 더 추가가 되면, HP를 StatKind에 분리하고 StatList나 Dictionary도 소모성과 비소모성을 분리해서 따로 두고 바꿔야함 - 신동환, 20240814
    // Stat
    private bool isFullHP = true; // 속도 개선을 위해 존재함.
    private int currentHP;
    private Dictionary<StatKind, int> stats = new Dictionary<StatKind, int>();
    // Buff
    private Dictionary<StatKind, int> buffPlus = new Dictionary<StatKind, int>();
    private Dictionary<StatKind, float> buffMultiply = new Dictionary<StatKind, float>();

    public UnitStat(Dictionary<StatKind, int> stats)
    {
        this.stats = new Dictionary<StatKind, int>(stats);
        InitializeAllBuff();
        CheckValidStats();

        currentHP = GetFinalStat(StatKind.HP);
    }

    // Bug 방지 코드
    public void CheckValidStats()
    {
        foreach (StatKind statKind in Enum.GetValues(typeof(StatKind)))
        {
            if (statKind == StatKind.Necessary)
            {
                break;
            }
            if (stats.ContainsKey(statKind) == false)
            {
                throw new Exception($"Stat이 없음 ${statKind}");
            }
        }
    }

    public void InitializeAllBuff()
    {
        foreach (StatKind statKind in Enum.GetValues(typeof(StatKind)))
        {
            buffPlus.Add(statKind, 0);
            buffMultiply.Add(statKind, 1);
        }
    }

    public void ResetBuffPlus(StatKind statKind)
    {
        buffPlus.Add(statKind, 0);
    }

    public void ResetBuffMultiply(StatKind statKind)
    {
        buffMultiply.Add(statKind, 1);
    }

    public void SetBuffPlus(StatKind statKind, int value)
    {
        buffPlus.Add(statKind, value);
    }

    public void SetBuffMultiply(StatKind statKind, int value)
    {
        buffMultiply.Add(statKind, value);
    }

    // 소모성까지 고려한 Stat을 얻는 함수
    public int GetCurrentStat(StatKind statKind)
    {
        // TODO: 만약 소모성 Stat이 더 추가가 되면, HP를 StatKind에 분리하고 StatList나 Dictionary도 소모성과 비소모성을 분리해서 따로 두고 바꿔야함 - 신동환, 20240814
        if (statKind == StatKind.HP)
        {
            return currentHP;
        }
        else
        {
            return GetFinalStat(statKind);
        }
    }

    public int GetFinalStat(StatKind statKind)
    {
        if (stats.ContainsKey(statKind))
        {
            int stat = stats[statKind];
            return (int)((stat + buffPlus[statKind]) * buffMultiply[statKind]);
        }
        else
        {
            throw new Exception($"Stat이 없음 ${statKind}");
        }
    }

    public bool GetIsFUllHP() { return isFullHP; }

    public int ChangeCurrentHP(int change)
    {
        currentHP += change;
        int maxHP = GetFinalStat(StatKind.HP);
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
            isFullHP = true;
        }
        else
        {
            isFullHP = false;
        }

        if (currentHP < 0)
        {
            currentHP = 0;
        }
        return currentHP;
    }

    public UnitStat Copy()
    {
        return new UnitStat(stats);
    }
}