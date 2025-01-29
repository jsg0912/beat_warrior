using UnityEngine;

public class AttackStrategyThrowItmomi : AttackStrategyCreate
{
    protected override void SkillMethod()
    {
        base.SkillMethod();
        obj.transform.position = new Vector3(GetPlayerPos().x, GetMonsterPos().y, 0);
    }
}