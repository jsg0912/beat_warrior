using UnityEngine;

public class SttackStrategyThrowIsmomi : AttackStrategyThrow
{
    protected override void SkillMethod()
    {
        base.SkillMethod();

        obj.transform.position = new Vector3(GetPlayerPos().x, GetMonsterBottomPos().y, 0);
    }
}
