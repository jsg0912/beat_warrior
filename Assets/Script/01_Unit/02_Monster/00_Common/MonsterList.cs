// Monster's Instances List
using System.Collections.Generic;
using System;

public static class MonsterList
{
    public static List<MonsterJSON> monsterList = new List<MonsterJSON>()
    {
        new MonsterJSON(MonsterName.DummySoldierHP1,
        "Prototype Test용 근접 쓰레기1",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 1},
            {StatKind.ATK, 1},
        },
        PatternName.MeleeEnemy),

         new MonsterJSON(MonsterName.DummySoldierHP2,
        "Prototype Test용 근접 쓰레기2",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.MeleeEnemy),

         new MonsterJSON(MonsterName.DummySoldierHP3,
        "Prototype Test용 근접 쓰레기3",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 3},
            {StatKind.ATK, 1},
        },
        PatternName.MeleeEnemy),

         new MonsterJSON(MonsterName.DummySoldierHP4,
        "Prototype Test용 근접 쓰레기4",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 4},
            {StatKind.ATK, 1},
        },
        PatternName.MeleeEnemy),

         new MonsterJSON(MonsterName.DummySoldierHP5,
        "Prototype Test용 근접 쓰레기5",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 5},
            {StatKind.ATK, 1},
        },
        PatternName.MeleeEnemy),

         new MonsterJSON(MonsterName.DummySoldierHP6,
        "Prototype Test용 근접 쓰레기6",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 6},
            {StatKind.ATK, 1},
        },
        PatternName.MeleeEnemy),

         new MonsterJSON(MonsterName.DummyArcherHP1,
        "Prototype Test용 원거리 쓰레기1",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 1},
            {StatKind.ATK, 1},
        },
        PatternName.RangedMonster),

        new MonsterJSON(MonsterName.DummyArcherHP2,
        "Prototype Test용 원거리 쓰레기2",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.RangedMonster),

         new MonsterJSON(MonsterName.DummyArcherHP3,
        "Prototype Test용 원거리 쓰레기3",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 3},
            {StatKind.ATK, 1},
        },
        PatternName.RangedMonster),

         new MonsterJSON(MonsterName.DummyArcherHP4,
        "Prototype Test용 원거리 쓰레기4",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 4},
            {StatKind.ATK, 1},
        },
        PatternName.RangedMonster),

        new MonsterJSON(MonsterName.Ibkkugi,
        "Prototype Test용 원거리 쓰레기2",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.Ibkkugi),

        new MonsterJSON(MonsterName.Koppulso,
        "Prototype Test용 원거리 쓰레기2",
        new Dictionary<StatKind, int>{
            {StatKind.HP, 2},
            {StatKind.ATK, 1},
        },
        PatternName.Monster3),
    };

    public static MonsterUnit FindMonster(MonsterName name)
    {
        MonsterJSON target = monsterList.Find(monster => monster.monsterName == name.ToString());
        if (target == null)
        {
            throw new Exception($"Monster가 List에 없음 {name.ToString()}");
        }

        return GetMonsterFromJSON(target);
    }

    public static MonsterUnit GetMonsterFromJSON(MonsterJSON monsterJSON)
    {
        MonsterName monsterName = Util.ParseEnumFromString<MonsterName>(monsterJSON.monsterName);
        PatternName patternName = Util.ParseEnumFromString<PatternName>(monsterJSON.patternName);
        Dictionary<StatKind, int> monsterStats = new Dictionary<StatKind, int>();

        for (int i = 0; i < monsterJSON.statKinds.Length; i++)
        {
            StatKind statKind = Util.ParseEnumFromString<StatKind>(monsterJSON.statKinds[i]);
            monsterStats.Add(statKind, monsterJSON.statValues[i]);
        }

        return new MonsterUnit(
            new MonsterInfo(monsterName, monsterJSON.description),
            new UnitStat(monsterStats),
            patternName
        );
    }
}