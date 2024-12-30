// Monster's Instances List
using System.Collections.Generic;
using System;

public static class MonsterList
{
    public static List<MonsterJSON> monsterList = new List<MonsterJSON>()
    {
        new MonsterJSON(MonsterName.Ibkkugi,
        "입꾹이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.Monster3),

         new MonsterJSON(MonsterName.Jiljili,
        "질질이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.Jiljili),

        new MonsterJSON(MonsterName.Itmomi,
        "잇몸이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.Itmomi),

        new MonsterJSON(MonsterName.Koppulso,
        "코뿔소",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.Monster3),
        new MonsterJSON(MonsterName.Giljjugi,
        "길쭉이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.Monster3),
        new MonsterJSON(MonsterName.Ippali,
        "이빨이",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.Monster3),
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
            patternName
        );
    }
}