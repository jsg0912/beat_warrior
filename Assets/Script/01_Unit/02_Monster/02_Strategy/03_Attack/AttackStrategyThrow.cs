using UnityEngine;

public class AttackStrategyThrow : AttackStrategy
{
    protected GameObject obj;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);

        obj = Resources.Load(PrefabRouter.AttackPrefab[monster.monsterName]) as GameObject;
    }

    protected override void UseSkill()
    {
        GameObject.Instantiate(obj);
    }
}
