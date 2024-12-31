using UnityEngine;

public class SttackStrategyThrowIbkkugi : AttackStrategyThrow
{
    protected override void SkillMethod()
    {
        base.SkillMethod();

        Vector3 offset = new Vector3(0, MonsterConstant.ThrowObjectYOffset, 0);
        obj.transform.position = monster.transform.position + offset;

        float gravity = Physics2D.gravity.y;
        // 포물선 운동 시간 공식
        float time = Mathf.Sqrt(-8 * MonsterConstant.IbkkugiMaxHeight / gravity);
        float distance = GetPlayerPos().x - GetMonsterPos().x - MonsterConstant.ThrowObjectYOffset * GetPlayerDirection();
        Vector3 velocity = new Vector3(distance / time, -gravity * time / 2, 0);
        monsterAttackCollider.rb.velocity = velocity;
    }
}
