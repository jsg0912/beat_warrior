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
        Vector3 offset = new Vector3(0, 0.5f, 0);
        GameObject throwObj = GameObject.Instantiate(obj, monster.transform.position + offset, quaternion.identity);
        throwObj.GetComponent<MonsterAttackCollider>().Initiate(monster);
    }
}
