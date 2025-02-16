using System.Collections;
using UnityEngine;

public class AttackStrategyThrowMany : AttackStrategyThrow
{
    private int throwCount;
    private float throwInterval;
    private int throwCountMax;
    private int throwCountCurrent;

    public AttackStrategyThrowMany(float throwSpeed, float maxHeight, int throwCount, float throwInterval, GameObject gameObject) : base(throwSpeed, maxHeight)
    {
        this.throwCount = throwCount;
        this.throwInterval = throwInterval;
        obj = gameObject;
    }

    protected override void AttackMethod()
    {
        monsterAttackCollider = obj.GetComponent<MonsterAttackCollider>();
        if (monsterAttackCollider != null)
        {
            monsterAttackCollider.Initiate();
            monsterAttackCollider.SetMonsterAtk(monster.GetFinalStat(StatKind.ATK));
        }

        obj.transform.position = monster.attackCollider.transform.position; // 원거리 몬스터들은 attackCollider를 원거리 공격의 사용 위치로 지정

        float distance = targetPosition.x - GetMonsterFrontPos().x;

        monsterAttackCollider.rb.velocity = GetVelocityConstantFlyTime(distance);
    }

    protected override void AttackMethod()
    {
        base.AttackMethod();

        throwCountCurrent = 0;
        monoBehaviour.StartCoroutine(ThrowMany());
    }

    private IEnumerator ThrowMany()
    {
        while (throwCountCurrent < throwCount)
        {
            yield return new WaitForSeconds(throwInterval);
            monsterAttackCollider.rb.velocity = GetVelocityConstantFlyTime(targetPosition.x - GetMonsterFrontPos().x);
            throwCountCurrent++;
        }
    }
}