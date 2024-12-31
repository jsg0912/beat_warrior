using UnityEngine;

public class SttackStrategyThrowIsmomi : AttackStrategyThrow
{
    protected override void SkillMethod()
    {
        base.SkillMethod();

        obj.transform.position = new Vector3(GetPlayerPos().x, GetMonsterPos().y, 0);
    }
}
