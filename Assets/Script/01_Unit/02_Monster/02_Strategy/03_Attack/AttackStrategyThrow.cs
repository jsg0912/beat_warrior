using UnityEngine;
using System.Collections;

public class AttackStrategyThrow : AttackStrategyCreate
{
    private float throwSpeed;
    private float maxHeight;
    private float gravity = Physics2D.gravity.y;
    protected Vector3 targetPosition;

    public AttackStrategyThrow(float throwSpeed, float maxHeight)
    {
        this.throwSpeed = throwSpeed;
        this.maxHeight = maxHeight;
    }

    protected override IEnumerator UseSkill()
    {
        monster.SetStatus(MonsterStatus.Attack);
        yield return new WaitForSeconds(attackStartDelay);

        monster.PlayAnimation(MonsterStatus.Attack);
        yield return new WaitForSeconds(attackActionInterval);
        SetTargetPosition();
        SkillMethod();

        attackCoolTime = attackCoolTimeMax;
        monster.SetStatus(MonsterStatus.Idle);
    }

    private void SetTargetPosition()
    {
        targetPosition = GetPlayerPos();
    }

    protected override void SkillMethod()
    {
        base.SkillMethod();

        Vector3 offset = new Vector3(MonsterConstant.ThrowObjectXOffset, 0, 0);
        obj.transform.position = monster.transform.position + offset;

        float distance = targetPosition.x - GetMonsterPos().x;

        monsterAttackCollider.rb.velocity = GetVelocityConstantFlyTime(distance);
    }

    private Vector3 GetVelocityConstantFlyTime(float distance)
    {
        float time = Mathf.Sqrt(2 * maxHeight / -gravity);
        return new Vector3(distance / time, -gravity * time / 2, 0);
    }

    private Vector3 GetVelocityConstantXSpeed(float distance)
    {
        float time = distance / throwSpeed;
        return new Vector3(throwSpeed, -gravity * time / 2, 0);
    }

    private Vector3 GetVelocityConstantAmplitude(float distance)
    {
        // 포물선 운동 시간 공식
        float value = -gravity * distance / (throwSpeed * throwSpeed);
        float theta = 0.5f * Mathf.Asin(value);

        float sinTheta = Mathf.Sin(theta);
        float cosTheta = Mathf.Cos(theta);

        return new Vector3(throwSpeed * cosTheta * monster.GetRelativePlayerDirectionFloat(), throwSpeed * sinTheta, 0);
    }

}