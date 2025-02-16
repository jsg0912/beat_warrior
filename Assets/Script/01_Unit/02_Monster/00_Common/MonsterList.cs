// Monster's Instances List
using System.Collections.Generic;
using System;

public static class MonsterList
{
    public static List<MonsterJSON> monsterList = new List<MonsterJSON>()
    {
        new MonsterJSON(MonsterName.TutorialIppali,
        "더미",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 0},
            {StatKind.Def, 0},
        },
        PatternName.TutorialIppali),
        new MonsterJSON(MonsterName.Ibkkugi,
        "입꾹이", // TODO: 이 이름도 ScriptPool로 이동해야 함 
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
            {StatKind.Def, 0},
        },
        PatternName.Ibkkugi),

         new MonsterJSON(MonsterName.Dulduli,
        "덜덜이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
            {StatKind.Def, 9999},
        },
        PatternName.Dulduli),

        new MonsterJSON(MonsterName.Itmomi,
        "잇몸이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
            {StatKind.Def, 0},
        },
        PatternName.Itmomi,
        isKnockBackAble: false),

        new MonsterJSON(MonsterName.Koppulso,
        "코뿔소",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
            {StatKind.Def, 0},
        },
        PatternName.Koppulso),
        new MonsterJSON(MonsterName.Giljjugi,
        "길쭉이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
            {StatKind.Def, 0},
        },
        PatternName.Giljjugi),
        new MonsterJSON(MonsterName.Ippali,
        "이빨이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
            {StatKind.Def, 0},
        },
        PatternName.Ippali),
        new MonsterJSON(MonsterName.Ch2Boss,
        "황혼의 촉수 빨판이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 100},
            {StatKind.ATK, 1},
            {StatKind.Def, 0},
        },
        PatternName.Ch2Boss,
        isKnockBackAble: false),
    };

    public static MonsterUnit FindMonster(MonsterName name, int anotherHPValue = 0)
    {
        MonsterJSON target = monsterList.Find(monster => monster.monsterName == name.ToString());
        if (target == null)
        {
            throw new Exception($"Monster가 List에 없음 {name.ToString()}");
        }

        return GetMonsterFromJSON(target, anotherHPValue);
    }

    public static MonsterUnit GetMonsterFromJSON(MonsterJSON monsterJSON, int anotherHPValue = 0)
    {
        MonsterName monsterName = Util.ParseEnumFromString<MonsterName>(monsterJSON.monsterName);
        PatternName patternName = Util.ParseEnumFromString<PatternName>(monsterJSON.patternName);
        Dictionary<StatKind, int> monsterStats = new Dictionary<StatKind, int>();

        for (int i = 0; i < monsterJSON.statKinds.Length; i++)
        {
            StatKind statKind = Util.ParseEnumFromString<StatKind>(monsterJSON.statKinds[i]);
            monsterStats.Add(statKind, monsterJSON.statValues[i]);
        }

        if (anotherHPValue != 0)
        {
            monsterStats[StatKind.HP] = anotherHPValue;
        }

        return new MonsterUnit(
            new MonsterInfo(monsterName, monsterJSON.description),
            new UnitStat(monsterStats),
            patternName,
            monsterJSON.isKnockBackAble
        );
    }

    public static void CheckValidStat()
    {
        foreach (MonsterJSON monsterJSON in monsterList)
        {
            Dictionary<StatKind, int> monsterStats = new Dictionary<StatKind, int>();

            for (int i = 0; i < monsterJSON.statKinds.Length; i++)
            {
                StatKind statKind = Util.ParseEnumFromString<StatKind>(monsterJSON.statKinds[i]);
                monsterStats.Add(statKind, monsterJSON.statValues[i]);
            }
            new UnitStat(monsterStats).CheckValidStats();
        }
    }
}