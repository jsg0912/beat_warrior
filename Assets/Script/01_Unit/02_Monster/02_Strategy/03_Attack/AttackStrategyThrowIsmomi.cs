using UnityEngine;

public class AttackStrategyThrowIsmomi : AttackStrategyThrow
{
    protected override void SkillMethod()
    {
        base.SkillMethod();

        obj.transform.position = new Vector3(GetPlayerPos().x, GetMonsterBottomPos().y, 0);
    }
}
