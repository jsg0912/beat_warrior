using Unity.Mathematics;
using UnityEngine;

public class AttackStrategyThrow : AttackStrategy
{
    protected GameObject obj;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        obj = Resources.Load(PrefabRouter.AttackPrefab[monster.monsterName]) as GameObject;
    }

    protected override void SkillMethod()
    {
        GameObject throwObj = GameObject.Instantiate(obj);
        throwObj.GetComponent<MonsterAttackCollider>().Initiate(monster);
    }
}
