using UnityEngine;

public class AttackStrategyThrow : AttackStrategyCreate
{
    protected MonsterAttackCollider monsterAttackCollider;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        prefab = Resources.Load(PrefabRouter.AttackPrefab[monster.monsterName]) as GameObject;
    }

    protected override void SkillMethod()
    {
        base.SkillMethod();

        monsterAttackCollider = obj.GetComponent<MonsterAttackCollider>();
        monsterAttackCollider.Initiate();
    }
}
