using UnityEngine;

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

    protected virtual void SetTargetPosition()
    {
        targetPosition = GetPlayerPos();
    }

    protected override void AttackMethod()
    {
        base.AttackMethod();

        SetTargetPosition();

        obj.transform.position = monster.attackCollider.transform.position; // 원거리 몬스터들은 attackCollider를 원거리 공격의 사용 위치로 지정

        float distance = targetPosition.x - GetMonsterFrontPos().x;

        monsterAttackCollider.rb.velocity = GetVelocityConstantFlyTime(distance);
    }

    protected Vector3 GetVelocityConstantFlyTime(float distance)
    {
        float time = Mathf.Sqrt(2 * maxHeight / -gravity);
        return new Vector3(distance / time, -gravity * time / 2, 0);
    }

    protected Vector3 GetVelocityConstantXSpeed(float distance)
    {
        float time = distance / throwSpeed;
        return new Vector3(throwSpeed, -gravity * time / 2, 0);
    }

    protected Vector3 GetVelocityConstantAmplitude(float distance)
    {
        // 포물선 운동 시간 공식
        float value = -gravity * distance / (throwSpeed * throwSpeed);
        float theta = 0.5f * Mathf.Asin(value);

        float sinTheta = Mathf.Sin(theta);
        float cosTheta = Mathf.Cos(theta);

        return new Vector3(throwSpeed * cosTheta * monster.GetRelativePlayerDirectionFloat(), throwSpeed * sinTheta, 0);
    }
}