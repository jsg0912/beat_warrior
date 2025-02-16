using System.Collections;
using UnityEngine;

public class AttackStrategyThrowMany : AttackStrategyThrow
{
    PoolTag poolTag;
    private float throwInterval;
    private int throwCountMax;
    private int throwCountCurrent;

    private int throwCount;

    public AttackStrategyThrowMany(float throwSpeed, float maxHeight, int throwCountMax, float throwInterval, PoolTag poolTag) : base(throwSpeed, maxHeight)
    {
        this.throwCountMax = throwCountMax;
        this.throwInterval = throwInterval;
        this.poolTag = poolTag;
    }

    protected override void AttackMethod()
    {
        throwCountCurrent = 0;
        monoBehaviour.StartCoroutine(ThrowMany());
    }

    private IEnumerator ThrowMany()
    {
        yield return new WaitForSeconds(BossConstantCh2.ThrowAnimationDelay);
        while (throwCountCurrent < throwCount)
        {
            yield return new WaitForSeconds(throwInterval);
            obj = MyPooler.ObjectPooler.Instance.GetFromPool(poolTag, GetMonsterPos(), Quaternion.identity);
            monsterAttackCollider = obj.GetComponent<MonsterAttackCollider>();
            if (monsterAttackCollider != null)
            {
                monsterAttackCollider.Initiate();
                monsterAttackCollider.SetMonsterAtk(monster.GetFinalStat(StatKind.ATK));
            }

            obj.transform.position = monster.attackCollider.transform.position; // 원거리 몬스터들은 attackCollider를 원거리 공격의 사용 위치로 지정

            float distance = targetPosition.x - GetMonsterFrontPos().x;

            monsterAttackCollider.rb.velocity = GetVelocityConstantFlyTime(distance);
            throwCountCurrent++;
        }
    }
}