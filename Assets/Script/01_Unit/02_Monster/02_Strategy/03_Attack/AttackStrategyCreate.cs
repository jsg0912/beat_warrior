using UnityEngine;
using System.Collections.Generic;

public class AttackStrategyCreate : AttackStrategy
{
    protected GameObject prefab;
    protected GameObject obj;
    protected MonsterAttackCollider monsterAttackCollider;
    Dictionary<MonsterName, PoolTag> enemyCreate = new() {
        { MonsterName.Ibkkugi, PoolTag.IbkkugiThrow },
        { MonsterName.Itmomi, PoolTag.ItmomiThrow },
    };

    public AttackStrategyCreate(string monsterAnimTrigger = MonsterAnimTrigger.attackChargeAnimTrigger) : base(monsterAnimTrigger)
    {
    }
    private PoolTag GetEnemyCreatePoolTag(MonsterName monsterName)
    {
        // return PoolTag.EnemyMiniMapIcon;
        return enemyCreate.ContainsKey(monsterName) ? enemyCreate[monsterName] : PoolTag.IbkkugiThrow;
    }

    protected override void AttackMethod()
    {
        obj = MyPooler.ObjectPooler.Instance.GetFromPool(GetEnemyCreatePoolTag(monster.monsterName), GetMonsterPos(), Quaternion.identity);

        monsterAttackCollider = obj.GetComponent<MonsterAttackCollider>();
        monsterAttackCollider.Initialize();
        monsterAttackCollider.SetMonsterAtk(monster.GetFinalStat(StatKind.ATK));
    }
}