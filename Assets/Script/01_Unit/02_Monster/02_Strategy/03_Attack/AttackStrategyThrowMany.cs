using System.Collections;
using UnityEngine;

public class AttackStrategyThrowMany : AttackStrategyThrow
{
    protected PoolTag poolTag;
    protected float throwInterval;
    protected int throwCountMax;
    protected int throwCountCurrent;

    public AttackStrategyThrowMany(float throwSpeed, float maxHeight, int throwCountMax, float throwInterval, PoolTag poolTag) : base(throwSpeed, maxHeight)
    {
        this.throwCountMax = throwCountMax;
        this.throwInterval = throwInterval;
        this.poolTag = poolTag;
    }

    protected override void SetTargetPosition()
    {
        /*
            TODO KMJ
            플레이어 방향으로 1개 
            나머지 구체는 양옆으로 1/2개씩 나누어 랜덤한 방향으로 떨어진다.
            기본 3/광폭 5
        */
        targetPosition = GetPlayerPos();
    }

    protected override void AttackMethod()
    {
        throwCountCurrent = 0;
        monoBehaviour.StartCoroutine(ThrowMany());
    }

    private IEnumerator ThrowMany()
    {
        yield return new WaitForSeconds(BossConstantCh2.ThrowAnimationDelay);
        while (throwCountCurrent < throwCountMax)
        {
            yield return new WaitForSeconds(throwInterval);
            SetTargetPosition();
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