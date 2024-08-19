using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class UnitStat
{
    // TODO: 만약 소모성 Stat이 더 추가가 되면, HP를 StatKind에 분리하고 StatList나 Dictionary도 소모성과 비소모성을 분리해서 따로 두고 바꿔야함 - 신동환, 20240814
    // Stat
    private bool isFullHP = true; // 속도 개선을 위해 존재함.
    private int currentHP;
    private Dictionary<StatKind, int> stats = new();
    // Buff
    private Dictionary<StatKind, int> buffPlus = new();
    private Dictionary<StatKind, float> buffMultiply = new();

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
            buffMultiply.Add(statKind, 0);
        }
    }

    public void ResetBuffPlus(StatKind statKind)
    {
        // TODO: 추후 Consumable StatKind이 더 늘어나면 Consumable StatKind인지 확인하는걸로 바꿔야함 - 신동환, 20240814
        if (statKind == StatKind.HP)
        {
            int deltaHP = GetFinalStat(statKind) - GetCurrentStat(statKind);
            buffPlus[statKind] = 0;
            int maxHP = GetFinalStat(statKind);
            currentHP = maxHP - deltaHP;
            if (currentHP <= 0)
            {
                currentHP = 1;
            }
        }
        else
        {
            buffPlus[statKind] = 0;
        }

    }

    public void ResetBuffMultiply(StatKind statKind)
    {
        buffMultiply[statKind] = 0;
        // TODO: ResetBuffPlus에 있는 것처럼 Consumable Stat은 Multiply값 변경에 따라 조정하는 과정이 필요함 - 신동환, 20240814
    }

    public void SetBuffPlus(StatKind statKind, int value)
    {
        buffPlus[statKind] = value;
        if (statKind == StatKind.HP)
        {
            currentHP += value;
        }
    }

    public void SetBuffMultiply(StatKind statKind, int value)
    {
        buffMultiply[statKind] = value;
        // TODO: ResetBuffPlus에 있는 것처럼 Consumable Stat은 Multiply값 변경에 따라 조정하는 과정이 필요함 - 신동환, 20240814
        if (statKind == StatKind.HP)
        {
            currentHP *= (1 + value);
        }
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
            return (int)((stat + buffPlus[statKind]) * (1 + buffMultiply[statKind]));
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