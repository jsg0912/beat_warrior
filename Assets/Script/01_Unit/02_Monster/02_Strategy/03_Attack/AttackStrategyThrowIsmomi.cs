using UnityEngine;

public class AttackStrategyThrowIsmomi : AttackStrategyCreate
{
    protected override void SkillMethod()
    {
        base.SkillMethod();
        obj.transform.position = new Vector3(GetPlayerPos().x, GetMonsterBottomPos().y, 0);
    }
}