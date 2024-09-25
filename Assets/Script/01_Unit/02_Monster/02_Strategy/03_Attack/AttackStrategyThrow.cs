using UnityEngine;

public class AttackStrategyThrow : AttackStrategy
{
    protected GameObject obj;

    protected override void UseSkill()
    {
        Instantiate(obj);
    }
}
