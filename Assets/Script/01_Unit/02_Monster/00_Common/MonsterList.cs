// Monster's Instances List
using System.Collections.Generic;
using System;
using System.Diagnostics;

public static class MonsterList
{
    public static List<MonsterUnit> monsterList = new List<MonsterUnit>()
    {
        new MonsterUnit(new MonsterInfo(MonsterName.DummySoldierHP1,
        "Prototype Test용 근접 쓰레기1"),
        new UnitStat(1, 1),
        new MeleeEnemy()),

         new MonsterUnit(new MonsterInfo(MonsterName.DummySoldierHP2,
        "Prototype Test용 근접 쓰레기2"),
        new UnitStat(2, 1),
        new MeleeEnemy()),

         new MonsterUnit(new MonsterInfo(MonsterName.DummySoldierHP3,
        "Prototype Test용 근접 쓰레기3"),
        new UnitStat(3, 1),
        new MeleeEnemy()),

         new MonsterUnit(new MonsterInfo(MonsterName.DummySoldierHP4,
        "Prototype Test용 근접 쓰레기4"),
        new UnitStat(4, 1),
        new MeleeEnemy()),

         new MonsterUnit(new MonsterInfo(MonsterName.DummySoldierHP5,
        "Prototype Test용 근접 쓰레기5"),
        new UnitStat(5, 1),
        new MeleeEnemy()),

         new MonsterUnit(new MonsterInfo(MonsterName.DummySoldierHP6,
        "Prototype Test용 근접 쓰레기6"),
        new UnitStat(6, 1),
        new MeleeEnemy()),

         new MonsterUnit(new MonsterInfo(MonsterName.DummyArcherHP1,
        "Prototype Test용 원거리 쓰레기1"),
        new UnitStat(1, 1),
        new RangedEnemy()),

        new MonsterUnit(new MonsterInfo(MonsterName.DummyArcherHP2,
        "Prototype Test용 원거리 쓰레기2"),
        new UnitStat(2, 1),
        new RangedEnemy()),

         new MonsterUnit(new MonsterInfo(MonsterName.DummyArcherHP3,
        "Prototype Test용 원거리 쓰레기3"),
        new UnitStat(3, 1),
        new RangedEnemy()),

         new MonsterUnit(new MonsterInfo(MonsterName.DummyArcherHP4,
        "Prototype Test용 원거리 쓰레기4"),
        new UnitStat(4, 1),
        new RangedEnemy()),
    };

    public static MonsterUnit FindMonster(MonsterName name)
    {
        MonsterUnit target = monsterList.Find(monster => (monster.unitInfo as MonsterInfo).monsterName == name);
        if (target == null)
        {
            throw new Exception($"Monster가 List에 없음 {name.ToString()}");
        }

        return new MonsterUnit(target.unitInfo as MonsterInfo, target.unitStat.Copy(), target.pattern.Copy());
    }
}