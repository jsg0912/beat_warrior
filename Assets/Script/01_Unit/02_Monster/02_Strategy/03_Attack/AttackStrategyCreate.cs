using UnityEngine;

public class AttackStrategyCreate : AttackStrategy
{
    protected GameObject prefab;
    protected GameObject obj;
    protected MonsterAttackCollider monsterAttackCollider;

    public AttackStrategyCreate(string monsterAnimTrigger = MonsterAnimTrigger.attackChargeAnimTrigger) : base(monsterAnimTrigger)
    {
    }

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        if (PrefabRouter.MonsterAttackPrefab.ContainsKey(monster.monsterName))
            prefab = Resources.Load(PrefabRouter.MonsterAttackPrefab[monster.monsterName]) as GameObject;
    }

    protected override void AttackMethod()
    {
        obj = GameObject.Instantiate(prefab);

        monsterAttackCollider = obj.GetComponent<MonsterAttackCollider>();
        monsterAttackCollider.Initiate();
        monsterAttackCollider.SetMonsterAtk(monster.GetFinalStat(StatKind.ATK));
    }
}