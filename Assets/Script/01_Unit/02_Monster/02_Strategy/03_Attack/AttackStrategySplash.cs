using System;
using UnityEngine;

public abstract class AttackStrategySplash : AttackStrategy
{
    private Vector2 splashRange;

    public AttackStrategySplash(Vector2 splashRange)
    {
        this.splashRange = splashRange;
    }

    protected void SplashAttack()
    {
        Vector2 playerPos = GetPlayerPos();
        Vector2 monsterPos = GetMonsterPos();
        if (Math.Abs(playerPos.x - monsterPos.x) <= splashRange.x && Math.Abs(playerPos.y - monsterPos.y) <= splashRange.y)
        {
            Player.Instance.GetDamaged(monster.GetFinalStat(StatKind.ATK), monster.GetRelativeDirectionToPlayer());
        }
    }
}