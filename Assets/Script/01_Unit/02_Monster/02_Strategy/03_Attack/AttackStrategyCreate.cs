using UnityEngine;

public class AttackStrategyCreate : AttackStrategy
{
    protected GameObject prefab;
    protected GameObject obj;
    protected MonsterAttackCollider monsterAttackCollider;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
        prefab = Resources.Load(PrefabRouter.AttackPrefab[monster.monsterName]) as GameObject;
    }

    protected override void SkillMethod()
    {
        obj = GameObject.Instantiate(prefab);

        monsterAttackCollider = obj.GetComponent<MonsterAttackCollider>();
        monsterAttackCollider.Initiate();
        monsterAttackCollider.SetMonsterAtk(monster.GetCurrentStat(StatKind.ATK));
    }
}
