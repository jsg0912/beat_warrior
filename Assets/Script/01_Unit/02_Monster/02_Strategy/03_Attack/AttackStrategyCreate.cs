using UnityEngine;

public class AttackStrategyCreate : AttackStrategy
{
    protected GameObject prefab;
    protected GameObject obj;

    public override void Initialize(Monster monster)
    {
        base.Initialize(monster);
    }

    protected override void SkillMethod()
    {
        obj = GameObject.Instantiate(prefab);
    }
}
