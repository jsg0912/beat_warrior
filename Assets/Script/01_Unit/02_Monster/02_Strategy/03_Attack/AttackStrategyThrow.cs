using UnityEngine;

public class AttackStrategyThrow : AttackStrategyCreate
{
    private float throwSpeed;
    private float maxHeight;

    public AttackStrategyThrow(float throwSpeed, float maxHeight)
    {
        this.throwSpeed = throwSpeed;
        this.maxHeight = maxHeight;
    }

    protected override void SkillMethod()
    {
        base.SkillMethod();

        Vector3 offset = new Vector3(0, MonsterConstant.ThrowObjectYOffset, 0);
        obj.transform.position = monster.transform.position + offset;

        float gravity = Physics2D.gravity.y;
        // 포물선 운동 시간 공식
        float distance = GetPlayerPos().x - GetMonsterPos().x - MonsterConstant.ThrowObjectYOffset * GetRelativePlayerDirectionFloat();
        float time = distance / throwSpeed;
        Vector3 velocity = new Vector3(throwSpeed, -gravity * time / 2, 0);
        monsterAttackCollider.rb.velocity = velocity;
    }
}