using System.Collections;
using UnityEngine;

public class AttackStrategyThrowMany : AttackStrategyThrow
{
    protected PoolTag poolTag;
    protected float throwInterval;
    protected int throwCountMax;
    protected int throwCountCurrent;
    protected float targetPosOffset = 5.0f;

    public AttackStrategyThrowMany(float throwSpeed, float maxHeight, int throwCountMax, float throwInterval, PoolTag poolTag, string monsterAnimTrigger = MonsterAnimTrigger.attackChargeAnimTrigger) : base(throwSpeed, maxHeight, monsterAnimTrigger)
    {
        this.throwCountMax = throwCountMax;
        this.throwInterval = throwInterval;
        this.poolTag = poolTag;
    }

    protected override void SetTargetPosition()
    {
        targetPosition = GetPlayerPos();

        if (throwCountCurrent == 0) return;
        else if (throwCountCurrent % 2 == 1) targetPosition.x -= Random.Range(0, targetPosOffset);
        else targetPosition.x += Random.Range(0, targetPosOffset);
    }

    protected override void AttackMethod()
    {
        throwCountCurrent = 0;
        monoBehaviour.StartCoroutine(ThrowMany());
    }

    private IEnumerator ThrowMany()
    {
        DebugConsole.Log("ThrowMany");
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

            if (monsterAttackCollider != null) monsterAttackCollider.rb.velocity = GetVelocityConstantFlyTime(distance);
            else obj.GetComponent<BossEgg>().rb.velocity = GetVelocityConstantFlyTime(distance);
            throwCountCurrent++;
        }
    }
}